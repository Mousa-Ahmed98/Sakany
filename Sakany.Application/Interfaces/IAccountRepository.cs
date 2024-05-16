using Sakany.Application.DTOS;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakany.Core.Entities;

namespace Sakany.Application.Interfaces
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> Register(ApplicationUser user , RegisterUserDTO registerUserDTO);
        public Task<dynamic> Login(LoginUserDTO userDTO);

        public Task<ApplicationUser> EditUserProfile(ApplicationUser applicationUser);
        public Task<ApplicationUser?> GetUserProfile(string UserId);
        public Task<IdentityResult> ChangePassword(ChangePasswordDTO model, ApplicationUser user);

    }
}
