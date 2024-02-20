using AutoMapper;
using Emc2.Core.DtoModels;
using Emc2.Core.Models;
using Emc2.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Emc2.Core.IRepositories;
using Emc2.EF.Repositories;
using NPOI.SS.Formula.Functions;
using Emc2.Api.Helpers;

namespace Emc2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;  
        private readonly IMapper _mapper;

        public IndustryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork; 
            _mapper = mapper;
        } 
        [HttpGet("GetAlIndustries")]
        public async Task<IActionResult> GetAllWithDescriptionsAsync()
        {
            try
            {
                var industries = await  _unitOfWork.Industries.GetAllAsync();
                var dtoIndustries = _mapper.Map<List<DtoIndustryDetails>>(industries);
                return Ok(dtoIndustries);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            } 

        } 

        [HttpGet("GetPagedIndustries")]
        public async Task<IActionResult> GetPagedAsync(int page, int pageSize)
        {
            try
            {
                var industries = await _unitOfWork.Industries.GetPagedAsync(page, pageSize);
                var dtoIndustries = _mapper.Map<List<DtoIndustryDetails>>(industries);
                return Ok(dtoIndustries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetIndustryById/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var industry = await _unitOfWork.Industries.GetByIdAsync(id);
                if (industry == null)
                    return NotFound($"No industry was found with ID {id}");
                var dtoIndustry = _mapper.Map<DtoIndustryDetails>(industry);
                return Ok(dtoIndustry);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("AddIndustry")]
        public async Task<IActionResult> AddAsync([FromForm] DtoIndustry dto)
        {
            try
            {
                ImageOperations.ValidateImage(dto.Icon);
                var industry = _mapper.Map<Industry>(dto);
                industry.Icon = await ImageOperations.ConvertImageToByteArray(dto.Icon); 
                 
                await _unitOfWork.Industries.AddAsync(industry);
                _unitOfWork.Complete();
                return Ok("Industry added successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateIndustry/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] DtoIndustry dto)
        {
            try
            {
                var industry = await _unitOfWork.Industries.GetByIdAsync(id);

                if (industry == null)
                    return NotFound($"No industry was found with ID {id}");

                _mapper.Map(dto, industry);

                if (dto.Icon != null)
                {
                    ImageOperations.ValidateImage(dto.Icon);
                    industry.Icon = await ImageOperations.ConvertImageToByteArray(dto.Icon); 
                } 
                _unitOfWork.Industries.Update(industry);
                _unitOfWork.Complete();
                return Ok("Industry updated successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("DeleteIndustry/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var industry = await _unitOfWork.Industries.GetByIdAsync(id);
                if (industry == null)
                    return NotFound($"No industry was found with ID {id}");

                _unitOfWork.Industries.Delete(industry);
                _unitOfWork.Complete();
                return Ok("Industry deleted successfully");


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
