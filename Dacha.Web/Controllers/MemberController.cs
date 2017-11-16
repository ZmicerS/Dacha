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
    public class MemberController : ApiController
    {

        IMemberService _memberService;// = new MemberService();

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
       
        [Route("api/GetMembersCompanionship/{id}")]
        public IHttpActionResult GetMembersCompanionship(string id)
        {
            IEnumerable<MemberUi> membersList = new List<MemberUi>();
          var listDto=  _memberService.GetMembersCompanionship(id);            
            membersList = listDto.Select(s => new MemberUi() {
                Id = s.Id.ToString(),
                Owner = s.Owner,
                OwnerAddress = s.OwnerAddress,
                PlotNumber = s.PlotNumber,
                PlotAddress = s.PlotAddress,
                PlotSquare = s.PlotSquare,
                Addition = s.Addition,
                CompanionshipId = s.CompanionshipId ?? Guid.NewGuid()
            });            
            return Ok(membersList);
        }

           
        public async Task <IHttpActionResult> Post([FromBody] MemberUi member)
        {
            if (!ModelState.IsValid)
            {

                return InternalServerError();
            };

            MemberDto memberDto = new MemberDto();
            memberDto.Owner = member.Owner ?? "";
            memberDto.PlotNumber = member.PlotNumber ?? "";
            memberDto.PlotSquare = member.PlotSquare ?? "";
            memberDto.PlotAddress = member.PlotAddress ?? "";
            memberDto.OwnerAddress = member.OwnerAddress ?? "";
            memberDto.CompanionshipId = member.CompanionshipId;
            memberDto.Addition = member.Addition ?? "";
            try
            { 
            await  _memberService.WriteMemberAsync(memberDto);
            }
            catch(Exception e)
            {

            }
            return Ok(member);
        }

        public async Task<IHttpActionResult> Put(string id, [FromBody] MemberUi member)
        {
            Guid guid;
            if (!ModelState.IsValid)
            {

                return InternalServerError();
            };
            if (!Guid.TryParse(id, out guid))
            {
                if (!Guid.TryParse(member.Id, out guid))
                {
                    return InternalServerError();
                }
            }

            MemberDto memberDto = new MemberDto();
            memberDto.Id = guid;
            memberDto.Owner = member.Owner ?? "";
            memberDto.PlotNumber = member.PlotNumber ?? "";
            memberDto.PlotSquare = member.PlotSquare ?? "";
            memberDto.PlotAddress = member.PlotAddress ?? "";
            memberDto.OwnerAddress = member.OwnerAddress ?? "";
            memberDto.CompanionshipId = member.CompanionshipId;
            memberDto.Addition = member.Addition ?? "";

            try
            { 
            await  _memberService.UpdateMemberAsync(memberDto);
            }
            catch(Exception e)
            {

            }
            return Ok("member");
        }

        public async Task<IHttpActionResult> Delete(string id)
        {
            Guid guid;
            if (!Guid.TryParse(id, out guid))
            {               
               return InternalServerError();             
            }

            try
            {
                await _memberService.DeleteMemberAsync(id);
            }
            catch(Exception e)
            {

            }

            return Ok();
        }



    }
}
