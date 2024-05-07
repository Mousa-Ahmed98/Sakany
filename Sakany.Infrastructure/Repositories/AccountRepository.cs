using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sakany.Application.DTOS;
using Sakany.Application.Interfaces;
using Sakany.Core.Entities;

namespace Sakany.Infrastructure.Repositories
{
    public class AccountRepository: IAccountRepository
    {
        private UserManager<ApplicationUser> userManager;
        private IConfiguration configuration;
        private RoleManager<IdentityRole> RoleManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.RoleManager = roleManager;
        }

        public async Task<IdentityResult> Register(ApplicationUser user , RegisterUserDTO registerUserDTO)
        {

            IdentityResult result = await userManager.CreateAsync(user , registerUserDTO.Password);

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
                    claims.Add(new Claim(ClaimTypes.Name, UserDb.UserName));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, UserDb.Id));
                    claims.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    var roles = await userManager.GetRolesAsync(UserDb);

                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    //security

                    var SignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asjiohwvwoihfwvbuvvdiKDEKDEWJAeDEdic237732JFE2343£R3je£"));

                    SigningCredentials signingCredentials = new SigningCredentials(SignKey, SecurityAlgorithms.HmacSha256);

                    JwtSecurityToken MyToken = new JwtSecurityToken(
                        issuer: configuration["JWT,ValidIss"],
                        audience: configuration["JWT,ValidAud"],
                        expires: DateTime.Now.AddHours(3),
                        claims: claims,
                        signingCredentials: signingCredentials
                        );

                    return (new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(MyToken),
                        Expired = MyToken.ValidTo
                    });
                }
                return null;
            }
            return null;
        }

    }
}
