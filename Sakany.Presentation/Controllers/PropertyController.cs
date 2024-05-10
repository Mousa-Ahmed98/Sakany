    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Sakany.Application.DTOS;
    using System;
    using System.Threading.Tasks;

    namespace YourNamespace.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class PropertyController : ControllerBase
        {
            [HttpPost]
            public async Task<ActionResult> AddProperty(PropertyDTO propertyDTO)
            {
                // Handle propertyDTO and images
                if (propertyDTO == null)
                {
                    var customResponse = new CustomResponseDTO
                    {
                        Success = false,
                        Data = null,
                        Message = "PropertyDTO is null",
                        Errors ={ "PropertyDTO is null" }
                    };
                    return BadRequest(customResponse);
                }

                // Your existing logic to process propertyDTO and images

                return Ok(new CustomResponseDTO
                {
                    Success = true,
                    Data = propertyDTO,
                    Message = "Property added successfully",
                    Errors = null
                });
            }
        }
    }
