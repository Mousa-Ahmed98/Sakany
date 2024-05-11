using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sakany.Application.DTOS;
using Sakany.Application.Interfaces;

namespace Sakany.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityServices cityServices;

        public CityController(ICityServices cityServices)
        {
            this.cityServices = cityServices;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id:int}")]
        public IActionResult GetAllCities(int id)
        {
            try
            {
                List<CityDTO> cityDTOs = cityServices.GetAll(id);

                if (cityDTOs == null || cityDTOs.Count == 0)
                {
                    var customResponse = new CustomResponseDTO
                    {
                        Success = false,
                        Message = $"No cities found for governorate with ID {id}",
                        Data = null,
                        Errors = null
                    };
                    return NotFound(customResponse);
                }
                var customResponseWithData = new CustomResponseDTO
                {
                    Success = true,
                    Message = "Cities successfully retrieved",
                    Data = cityDTOs,
                    Errors = null
                };
                return Ok(customResponseWithData);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                var customErrorResponse = new CustomResponseDTO
                {
                    Success = false,
                    Message = "An error occurred while processing the request",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(customErrorResponse);
            }
        }

    }
}
