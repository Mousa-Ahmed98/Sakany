using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sakany.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private ApplicationDbContext applicationDbContext;

        public MessageRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public CustomResponseDTO Get(int id)
        {
            Message message = applicationDbContext.Set<Message>().Find(id);
            CustomResponseDTO response = new CustomResponseDTO();

            if (message == null)
            {
                response.Success = false;
                response.Message = " Message is not found there is no such id in database";
                return response;
            }
            response.Success = true;
            response.Message = " Message request success";
            response.Data = message;
            
            return response;
        }

        public CustomResponseDTO GetAll()
        {
            List<Message> messages = applicationDbContext.Set<Message>().ToList();

            var response = new CustomResponseDTO
            {
                Success = true,
                Message = "all Messages request success",
                Data = messages
            };
            return response;
        }

        public CustomResponseDTO Insert(Message message)
        {
            var response = new CustomResponseDTO();

            applicationDbContext.Set<Message>().Add(message);

            applicationDbContext.SaveChanges();

            response.Success = true;
            response.Message = "Message added successfully.";
            response.Data = message;
            return response;
        }

    }
}
