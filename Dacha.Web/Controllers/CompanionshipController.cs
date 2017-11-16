using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Dacha.Web.Models;
using Dacha.Bll.Models;
using System.Threading.Tasks;
using Dacha.Bll.Interfaces;

namespace Dacha.Web.Controllers
{
    [Authorize]
    public class CompanionshipController : ApiController
    {
        ICompanionshipService _companionshipService;     
        
       public CompanionshipController(ICompanionshipService companionshipService)
        {
            _companionshipService = companionshipService;
        }
        
        // GET: api/Companionship
        public IHttpActionResult Get()
        {
            IEnumerable<CompanionshipUi> list = new List<CompanionshipUi>();
            try
            {
                var listDto = _companionshipService.GetOnlyAllCompanionship();
                if (listDto!=null)
                {
                    list = listDto.Select(s=> new CompanionshipUi() {
                        Id = s.Id.ToString(),
                        Name = s.Name,
                        Address = s.Address,
                        Membership = s.Membership,
                        Registration = s.Registration,
                        Chairman = ( s.Chairman != null)? s.Chairman : "",
                        Addition = s.Addition
                    }).ToList();
                }
            }
            catch (AggregateException e)
            {
                return BadRequest("Can't read data.");
            }
                return Ok(list);
        }
     
         public async Task<IHttpActionResult> Post([FromBody]CompanionshipUi data)
        {
            if (!ModelState.IsValid)
            {             
                return InternalServerError();
            };
            var companionship = new CompanionshipDto()
            {
                Name =data.Name,
                Address= (data.Address != null) ? data.Address : "",
                Registration = (data.Registration != null) ? data.Registration : "",
                Chairman = (data.Chairman != null) ? data.Chairman : "",
                Membership = (data.Membership != null) ? data.Membership : "",
                Addition = (data.Addition != null) ? data.Addition : ""
            };
            try
            {
              await  _companionshipService.WriteCompanionshipAync(companionship);
            }
            catch(Exception e)
            {

            }
            return Ok();
        }

        public async Task<IHttpActionResult> Put(string id, [FromBody]CompanionshipUi data)
        {
            if (!ModelState.IsValid)
            {           
                return InternalServerError();
            };
            Guid guid;
            Guid.TryParse(id, out guid);
            var companionship = new CompanionshipDto()
            {
                Id =guid,
                Name = data.Name,
                Address = (data.Address != null) ? data.Address : "",
                Registration = (data.Registration != null) ? data.Registration : "",
                Chairman = (data.Chairman != null) ? data.Chairman : "",
                Membership = (data.Membership != null) ? data.Membership : "",
                Addition = (data.Addition != null) ? data.Addition : ""
            };
            try
            {
                await _companionshipService.UpdateCompanionshipAync(companionship);
            }
            catch (Exception e)
            {

            } 
            return Ok();
        }

        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                await _companionshipService.DeleteCompanionshipAync(id);
            }
            catch (Exception e)
            {
            }           
            return Ok();
        }
    }
}
