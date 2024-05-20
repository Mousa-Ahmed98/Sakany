using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sakany.Application.DTOS;
using Sakany.Application.Services;
using Sakany.Core.Entities;
using Sakany.Infrastructure;

using Sakany.Application.Interfaces;

namespace Sakany.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private ApplicationDbContext applicationDbContext;
        private IMessageService messageService;

        public MessageController(ApplicationDbContext applicationDbContext , IMessageService messageService) 
        {
            this.applicationDbContext = applicationDbContext;
            this.messageService = messageService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            CustomResponseDTO response = messageService.GetAll();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public ActionResult Get(int id)
        {
            CustomResponseDTO response = messageService.Get(id);
            
            return Ok(response);
        }

        [HttpPost]
        public ActionResult Insert(MessageDTO message)
        {
            CustomResponseDTO response = messageService.Insert(message);

            if (ModelState.IsValid)
            {
                return Ok(response);
            }

            response.Success = false;
            response.Message = "Error happened while adding the message";
            response.Data = null;
            response.Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            return BadRequest(response);
        }

    }
}
