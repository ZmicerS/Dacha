using Dacha.Bll.Interfaces;
using Dacha.Bll.Models;
using Dacha.Web.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dacha.Web.Controllers
{
    public class AccountController : ApiController
    {
        IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [Route("api/account/register")]
        public async Task<IHttpActionResult> RegisterAsync([FromBody]RegisterViewModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return InternalServerError();
            };
            RegisterModelDto registerModelDto = new RegisterModelDto() {
            Email=registerModel.Email,
            Password=registerModel.Password,
            ConfirmPassword=registerModel.ConfirmPassword
            };
            try
            {
                await _accountService.RegisterUserAsync(registerModelDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                    
            }
            return Ok();
        }
    }
}
