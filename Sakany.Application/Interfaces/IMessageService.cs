using Microsoft.AspNetCore.Mvc;
using Sakany.Application.DTOS;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Interfaces
{
    public interface IMessageService
    {
        public CustomResponseDTO Insert(MessageDTO message);
        public CustomResponseDTO GetAll();
        public CustomResponseDTO Get(int id);



    }
}
