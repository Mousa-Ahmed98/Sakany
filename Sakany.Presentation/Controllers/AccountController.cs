using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sakany.Core.Entities;
using Sakany.Application.DTOS;

namespace Sakany.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            if (registerUserDTO != null)
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new ApplicationUser()
                    {
                        UserName = registerUserDTO.UserName,
                        Email = registerUserDTO.Email,
                        PasswordHash = registerUserDTO.Password,
                    };

                    IdentityResult result = await userManager.CreateAsync(user, registerUserDTO.Password);

                    if (result.Succeeded)
                    {
                        return Ok(result);
                    }
                    return BadRequest(result.Errors);
                }

                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);
        }


    }
}
