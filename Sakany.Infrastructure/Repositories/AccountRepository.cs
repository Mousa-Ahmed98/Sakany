﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sakany.Application.DTOS;
using Sakany.Application.Interfaces;
using Sakany.Core.Entities;

namespace Sakany.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private UserManager<ApplicationUser> userManager;
        private IConfiguration configuration;
        private RoleManager<IdentityRole> RoleManager;
        private ApplicationDbContext dbContext;
        private IMapper mapper;


        public AccountRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext, IMapper mapper)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.RoleManager = roleManager;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IdentityResult> Register(ApplicationUser user, RegisterUserDTO registerUserDTO)
        {

            ApplicationUser UserDb = await userManager.FindByEmailAsync(registerUserDTO.Email);
            if (UserDb == null)
            {
                user.UserName = GenerateUsernameFromEmail(registerUserDTO.Email);
                IdentityResult result = await userManager.CreateAsync(user, registerUserDTO.Password);

                if (result.Succeeded)
                {
                    //addrole
                    AddRole();
                    IdentityResult ResultRole = new IdentityResult();
                    try
                    {
                        ResultRole = await userManager.AddToRoleAsync(user, registerUserDTO.Role);
                    }
                    catch (Exception e)
                    {
                        //default is customer
                        ResultRole = await userManager.AddToRoleAsync(user, "customer");
                    }
                }
                return result;
            }
            return null;
        }
        private string GenerateUsernameFromEmail(string email)
        {
            int atIndex = email.IndexOf('@');
            string username;
            if (atIndex >= 0)
            {
                username = email.Substring(0, atIndex);
            }
            else
            {
                username = email;
            }
            return username;
        }
        bool AddRole()
        {

            if (!RoleManager.RoleExistsAsync("VENDOR").Result)
            {
                var VendorRole = new IdentityRole
                {
                    Name = "VENDOR"
                };
                RoleManager.CreateAsync(VendorRole).Wait();
            }

            if (!RoleManager.RoleExistsAsync("CUSTOMER").Result)
            {
                var CustomerRole = new IdentityRole
                {
                    Name = "CUSTOMER"
                };
                RoleManager.CreateAsync(CustomerRole).Wait();
            }
            return true;
        }

        /////////////
        ///
        public async Task<dynamic> Login(LoginUserDTO userDTO)
        {
            //ApplicationUser UserDb = await userManager.FindByNameAsync(userDTO.UserName);
            ApplicationUser UserDb = await userManager.FindByEmailAsync(userDTO.Email);
            if (UserDb != null)
            {
                bool found = await userManager.CheckPasswordAsync(UserDb, userDTO.Password);
                if (found)
                {
                    //create token

                    //create claims
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, UserDb.Name));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, UserDb.Id));
                    //claims.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    var roles = await userManager.GetRolesAsync(UserDb);

                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    //security

                    var SignKey = new SymmetricSecurityKey(Encoding

                        .UTF8.GetBytes("asjiohwvwoihfwvbuvvdiKDEKDEWJAeDEdic237732JFE2343£R3je£"));

                    SigningCredentials signingCredentials = new SigningCredentials(SignKey,
                        SecurityAlgorithms.HmacSha256);

                    JwtSecurityToken MyToken = new JwtSecurityToken(
                        issuer: "http://localhost:5019/",
                        audience: "http://localhost:4200",
                        expires: DateTime.Now.AddHours(3),
                        claims: claims,
                        signingCredentials: signingCredentials
                        );

                    return (new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(MyToken),
                        Role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                        Expired = MyToken.ValidTo
                    });
                }
                return null;
            }
            return null;
        }

        public async Task<ApplicationUser> EditUserProfile(ApplicationUser applicationUser)
        {
            var user = await userManager.FindByIdAsync(applicationUser.Id);
            Console.WriteLine(applicationUser.Id);
            if (user != null)
            {
                user = user.ExteractInfo(applicationUser);
                IdentityResult identityResult = await userManager.UpdateAsync(user);
            }
            return user!;
        }

        public async Task<ApplicationUser?> GetUserProfile(string UserId)
        {
            return await userManager.FindByIdAsync(UserId);
        }



        public async Task<IdentityResult> ChangePassword(ChangePasswordDTO model, ApplicationUser user)
        {

            return await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

        }


        public CustomResponseDTO DisplayUser(ClaimsPrincipal User , string jwtToken)
        {
            var response = new CustomResponseDTO();

            var user = userManager.GetUserAsync(User).Result;

            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;


            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
                return response;
            }

            response.Success = true;
            response.Message = "Password changed successfully";
            response.Data = new { User = user, Token = jwtToken, Role = roleClaim };
            return response;
        }

    }


}
