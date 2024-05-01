using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sakany.Core.Entities;
using Sakany.Application.DTOS;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Sakany.Application.Interfaces;

namespace Sakany.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserDTO registerUserDTO)
        {

            if (registerUserDTO != null && ModelState.IsValid)
            {

                //add role

                IdentityResult result = await accountService.Register(registerUserDTO);

                if (result.Succeeded)
                {
                    return Ok(result);
                }
                return BadRequest(result.Errors);

            }
            return BadRequest(ModelState);
        }



        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO userDTO)
        {

            if (ModelState.IsValid)
            {
                var res = await accountService.Login(userDTO);
                if (res != null)
                {
                    return Ok(res);
                }
                return Unauthorized();
            }
            return BadRequest(ModelState);
        }

    }
}

