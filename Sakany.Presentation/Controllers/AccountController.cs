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
        private  UserManager<ApplicationUser> userManager;
        private  IConfiguration configuration;
        private  RoleManager<IdentityRole> roleManager;
        private IAccountService accountService;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, IAccountService accountService)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
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

            if (userDTO != null && ModelState.IsValid)
            {
                ApplicationUser UserDb = await userManager.FindByNameAsync(userDTO.UserName);
                if (UserDb != null)
                {
                    bool found = await userManager.CheckPasswordAsync(UserDb, userDTO.Password);
                    if (found)
                    {
                        //create token

                        //create claims
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, UserDb.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, UserDb.Id));
                        claims.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var roles = await userManager.GetRolesAsync(UserDb);

                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        //security

                        var SignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("any words w khalas"));
                        SigningCredentials signingCredentials = new SigningCredentials(SignKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken MyToken = new JwtSecurityToken(
                            issuer: configuration["JWT,ValidIss"],
                            audience: configuration["JWT,ValidAud"],
                            expires: DateTime.Now.AddHours(3),
                            claims: claims,
                            signingCredentials: signingCredentials
                            );

                        return Ok(new
                        {
                            Token = MyToken,
                            Expired = MyToken.ValidTo
                        });
                    }
                    return BadRequest(ModelState);
                    // return Ok(accountService.Login(userDTO));
                }
               

            } 
            return BadRequest(ModelState);
        }

    }
}

