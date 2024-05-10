﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sakany.Application.DTOS;
using Sakany.Application.Interfaces;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyServices propertyServices;
        private readonly IPropertyFeaturesServices propertyFeaturesServices;
        private readonly IImageServices imageServices;

        public PropertyController(IPropertyServices propertyServices
                                 , IPropertyFeaturesServices propertyFeaturesServices
                                  , IImageServices imageServices)
        {
            this.propertyServices = propertyServices;
            this.propertyFeaturesServices = propertyFeaturesServices;
            this.imageServices = imageServices;
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
            List<string> fetsures = new List<string>();
            fetsures.AddRange(propertyDTO.featsures);
            //foreach (string feature in propertyDTO.featsures)
            //{
            //    fetsures.Add(feature);
            //}
             int propertyId= await propertyServices.Add(propertyDTO);
            if (propertyId != 0)
            {
                foreach (var fetsure in fetsures)
                {
                    PropertyFeatures propertyFeatures = new PropertyFeatures()
                    {
                        FeaturesName = fetsure,
                        PropertyId = propertyId
                    };
                    propertyFeaturesServices.Add(propertyFeatures);
                }
                return Ok(new CustomResponseDTO
                {
                    Success = true,
                    Data = propertyId ,
                    Message = "Property added successfully",
                    Errors = null
                });
            }
            return BadRequest(new CustomResponseDTO
            {
                Success = false,
                Data = null,
                Message = "Error when Add Property",
                Errors = null
            });
        }

        [HttpPost("{id:int}")]
        public async Task<ActionResult> AddImages(int id, List<IFormFile> file)
        {
            if (file.Count > 0)
            {
                foreach (var item in file)
                {
                    if (item != null)
                    {
                        if (file != null && item.Length > 0)
                        {
                            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);

                            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                            var filePath = Path.Combine(uploads, uniqueFileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                item.CopyTo(fileStream);
                            }

                            PropertyImage propertyImage = new PropertyImage()
                            {
                                ImageUrl = "/images/" + uniqueFileName,
                                PropertyId = id
                            };
                            imageServices.Add(propertyImage);
                        }
                    }
                }
                return Ok(new CustomResponseDTO
                {
                    Success = true,
                    Data = null,
                    Message = "images added successfully",
                    Errors = null
                });
            }
            return BadRequest(new CustomResponseDTO
            {
                Success = false,
                Data = null,
                Message = "No images hir",
                Errors = null
            });

        }
    }
}
