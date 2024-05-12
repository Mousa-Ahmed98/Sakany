using AutoMapper;
using Sakany.Application.DTOS;
using Sakany.Application.Interfaces;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Services
{
    public class MessageService : IMessageService
    {
        private IMessageRepository messageRepository;
        private IMapper mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            this.messageRepository = messageRepository;
            this.mapper = mapper;
        }

        public CustomResponseDTO Get(int id)
        {
            return messageRepository.Get(id);
        }

        public CustomResponseDTO GetAll()
        {
            return messageRepository.GetAll();
        }

        public CustomResponseDTO Insert(MessageDTO messageDTO)
        {
            Message message = mapper.Map<Message>(messageDTO);

            return messageRepository.Insert(message);
        }
    }
}
