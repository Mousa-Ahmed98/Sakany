using Microsoft.AspNetCore.Identity;
using Sakany.Application.DTOS;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Interfaces
{
    public interface IAccountService
    {
        public Task<IdentityResult> Register(RegisterUserDTO registerUserDTO);
        public Task<dynamic> Login(LoginUserDTO userDTO);
        public Task<EditUserProfileDTO> EditUserProfile(EditUserProfileDTO editUserProfileDTO, string userId);
        public Task<EditUserProfileDTO?> GetUserProfile(string UserName);

    }
}
