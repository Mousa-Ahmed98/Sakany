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
        private UserManager<ApplicationUser> userManager;

        public AccountController(IAccountService accountService, UserManager<ApplicationUser> userManager)
        {
            this.accountService = accountService;
            this.userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            var response = new CustomResponseDTO();

            if (registerUserDTO == null)
            {
                response.Success = false;
                response.Message = "User data is null.";
                return BadRequest(response);
            }

            if (ModelState.IsValid)
            {
                IdentityResult? result = await accountService.Register(registerUserDTO);
                if (result != null)
                {
                    if (result.Succeeded)
                    {
                        response.Success = true;
                        response.Message = "User registered successfully.";
                        response.Data = null;
                        return Ok(response);
                    }

                    response.Success = false;
                    response.Data = null;
                    response.Errors = result.Errors.Select(error => error.Description).ToList();
                    return BadRequest(response);
                }
                response.Success = false;
                response.Message = "Email already exist.";
                response.Data = null;
                return BadRequest(response);

            }

            response.Success = false;
            response.Data = null;
            response.Errors = ModelState.Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                .ToList();
            response.Message = "Invalid model state.";
            return BadRequest(response);
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
                    var customResponse = new CustomResponseDTO
                    {
                        Success = true,
                        Message = "Login successful",
                        Data = res 
                    };

                    return Ok(customResponse);
                }
                else
                {
                    var unauthorizedResponse = new CustomResponseDTO
                    {
                        Success = false,
                        Message = "Invalid UserName or Password",
                        Errors = new List<string> { "Invalid credentials" },
                        Data=null
                    };

                    return Unauthorized(unauthorizedResponse);
                }
            }
            else
            {
                var badRequestResponse = new CustomResponseDTO
                {
                    Success = false,
                    Message = "Validation failed",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList(),
                    Data = null
                };

                return BadRequest(badRequestResponse);
            }
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


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var changePasswordResult = await accountService.ChangePassword(model ,user);

            if (changePasswordResult.Succeeded)
            {
                return Ok("Password changed successfully");
            }
            else
            {
                return BadRequest(changePasswordResult.Errors);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetUserInfo")] 
        public async Task<ActionResult> GetInformationOfUserAsync()
        {
            var UserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!UserId.IsNullOrEmpty())
            {
                CustomDataOfUserDTO? customDataOfUserDTO = await accountService.GetCustomData(UserId!);
                if (customDataOfUserDTO != null)
                {
                    var goodResponse = new CustomResponseDTO
                    {
                        Success = true,
                        Message = " get data successfully",
                        Errors = new List<string> { "Invalid credentials" },
                        Data = new {
                            name = customDataOfUserDTO.Name,
                            email = customDataOfUserDTO.Email,
                            userName = customDataOfUserDTO.UserName,
                            phoneNumber = customDataOfUserDTO.PhoneNumber,
                        }
                    };
                    return Ok(goodResponse);
                }
                var Response = new CustomResponseDTO
                {
                    Success = false,
                    Message = "No data was found",
                    Errors = new List<string> { "No User logened" },
                    Data = null
                };
                return BadRequest(Response);
            }
            var badRequestResponse = new CustomResponseDTO
            {
                Success = false,
                Message = "No User logened",
                Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList(),
                Data = null
            };
            return BadRequest(badRequestResponse);


        }


        //trying to use get user inherted from controller base
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("DisplayUser")]
        public IActionResult DisplayUser()
        {
            var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];

            var user = userManager.GetUserAsync(User).Result;

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(new { User = user, Token = jwtToken });
        }


    }
}

