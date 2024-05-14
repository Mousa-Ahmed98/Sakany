using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sakany.Application.DTOS;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Interfaces
{
    public interface IAccountService
    {
        public Task<IdentityResult> Register(RegisterUserDTO registerUserDTO);
        public Task<dynamic> Login(LoginUserDTO userDTO);
        public Task<EditUserProfileDTO> EditUserProfile(EditUserProfileDTO editUserProfileDTO, string userId);
        public Task<EditUserProfileDTO?> GetUserProfile(string UserId);
        public Task<IdentityResult> ChangePassword(ChangePasswordDTO model, ApplicationUser user);
        public CustomResponseDTO DisplayUser(ClaimsPrincipal User, string jwtToken);
        public Task<CustomDataOfUserDTO?> GetCustomData(string UserId);


    }
}
