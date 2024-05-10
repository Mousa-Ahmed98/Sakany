using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sakany.Application.DTOS;
using Sakany.Application.Interfaces;
using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyServices propertyServices;

        public PropertyController(IPropertyServices propertyServices)
        {
            this.propertyServices = propertyServices;
        }


        [HttpPost]
        public async Task<ActionResult> AddProperty(PropertyDTO propertyDTO)
        {
            if (!ModelState.IsValid)
            {
                var customResponse = new CustomResponseDTO
                {
                    Success = false,
                    Data = null,
                    Message = "PropertyDTO is null",
                    Errors = ModelState.Values.SelectMany(v => v.Errors
                                              .Select(e => e.ErrorMessage))
                                              .ToList(),
                };
                return BadRequest(customResponse);
            }
            PropertiesDetilesDTO detilesDTO= await propertyServices.Add(propertyDTO);
            if (detilesDTO!=null)
            {
                return Ok(new CustomResponseDTO
                {
                    Success = true,
                    Data = detilesDTO,
                    Message = "Property added successfully",
                    Errors = null
                });
            }
            return Ok(new CustomResponseDTO
            {
                Success = false,
                Data = null,
                Message = "Error when Add Property",
                Errors = null
            });
        }

        [HttpPost("{id:int}")]
        public async Task<ActionResult> AddImages(int id,IFormFile file)
        {

            return Ok();
        }
    }
}
