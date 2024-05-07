﻿using Microsoft.AspNetCore.Http;
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

    }
}

