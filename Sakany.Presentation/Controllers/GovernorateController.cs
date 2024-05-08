using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sakany.Application.DTOS;
using Sakany.Application.Interfaces;

namespace Sakany.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernorateController : ControllerBase
    {
        private readonly IGovernorateServices governorateServices;

        public GovernorateController(IGovernorateServices governorateServices)
        {
            this.governorateServices = governorateServices;
        }

        [HttpGet]
        public ActionResult GetAllGovernorate()
        {
            List<GovernorateDTO> governorateDTOs = governorateServices.GetAll();

            CustomResponseDTO customResponseDTO = new CustomResponseDTO()
            {
                Success = true,
                Data = governorateDTOs,
                Message = "Data Successfully retrieved",
                Errors = null
            };

            return Ok(customResponseDTO);
        }
        [HttpGet("{id:int}")]
        public ActionResult GetGovernorateById(int id)
        {
            GovernorateDTO governorateDTO = governorateServices.GetById(id);

            if (governorateDTO == null)
            {
                CustomResponseDTO errorResponse = new CustomResponseDTO()
                {
                    Success = false,
                    Data = null,
                    Message = "Governorate with ID " + id + " not found",
                    Errors = new List<string> { "Governorate not found" }
                };

                return NotFound(errorResponse);
            }

            CustomResponseDTO customResponseDTO = new CustomResponseDTO()
            {
                Success = true,
                Data = governorateDTO,
                Message = "Data Successfully retrieved",
                Errors = null
            };

            return Ok(customResponseDTO);
        }


    }
}
