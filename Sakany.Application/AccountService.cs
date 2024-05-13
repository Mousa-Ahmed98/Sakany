using Sakany.Application.DTOS;
using Sakany.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakany.Core.Entities;
using AutoMapper;

namespace Sakany.Application
{
    public class AccountService : IAccountService
    {
        private IAccountRepository AccountRepository;
        private IMapper mapper;
        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            AccountRepository = accountRepository;
            this.mapper = mapper;
        }
        public async Task<IdentityResult> Register(RegisterUserDTO registerUserDTO)   //must this function be async or not ??
        {
            ApplicationUser user = mapper.Map<ApplicationUser>(registerUserDTO);
            return await AccountRepository.Register(user, registerUserDTO);
        }
        public async Task<dynamic> Login(LoginUserDTO userDTO)
        {
            return await AccountRepository.Login(userDTO);
        }
    }
}
