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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        





        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("EditProfile")]
        public async Task<IActionResult> EditProfile(EditUserProfileDTO editUserProfileDTO)
        {
            if (ModelState.IsValid)
            {

                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                editUserProfileDTO = await accountService.EditUserProfile(editUserProfileDTO, userId);
                return Ok(editUserProfileDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUserData()
        {
            var UserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine("UserId");
            Console.WriteLine(UserId);
            if (!UserId.IsNullOrEmpty())
            {
                EditUserProfileDTO? editUserProfileDTO = await accountService.GetUserProfile(UserId!);
                if (editUserProfileDTO != null)
                {
                    return Ok(editUserProfileDTO);
                }
                return BadRequest("No data was found");
            }
           return BadRequest(ModelState);
        }

    }
}

