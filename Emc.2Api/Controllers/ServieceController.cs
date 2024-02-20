using AutoMapper;
using Emc2.Api.Helpers;
using Emc2.Core;
using Emc2.Core.DtoModels;
using Emc2.Core.IRepositories;
using Emc2.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using System.Drawing;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Emc2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServieceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
         
        public ServieceController(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 
        [HttpGet("GetAllServices")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
               
                var services = await _unitOfWork.Servieces.GetAllAsync();
                var dtoServices= _mapper.Map<List<DtoServiceDetails>>(services);
                return Ok(dtoServices);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }
        [HttpGet("GetPagedServices")]
        public async Task<IActionResult> GetPagedAsync(int page, int pageSize)
        {
            try
            {
                var services = await _unitOfWork.Servieces.GetPagedAsync(page, pageSize);
                var dtoServices = _mapper.Map<List<DtoServiceDetails>>(services);
                return Ok(dtoServices);
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetServiceById/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var service = await _unitOfWork.Servieces.GetByIdAsync(id);
                if (service == null)
                    return NotFound($"No Service was found with ID {id}");
                var dtoService = _mapper.Map<DtoServiceDetails>(service);
                return Ok(dtoService);
              
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpPost("AddService")]
        public async Task<IActionResult> AddAsync([FromForm]DtoService dto)
        {
            try
            { 
                ImageOperations.ValidateImage(dto.Icon);
                var service = _mapper.Map<Service>(dto);
                service.Icon = await ImageOperations.ConvertImageToByteArray(dto.Icon);

                await _unitOfWork.Servieces.AddAsync(service);
                _unitOfWork.Complete();
                return Ok("Serveice added successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateService/{id}")]
        public async Task<IActionResult> UpdateAsync(int id , [FromForm]DtoService dto) 
        {
            try
            {
                var service = await _unitOfWork.Servieces.GetByIdAsync(id);

                if (service == null)
                    return NotFound($"No Service was found with ID {id}");

                _mapper.Map(dto, service);

                if (dto.Icon != null)
                {
                    ImageOperations.ValidateImage(dto.Icon);
                    service.Icon = await ImageOperations.ConvertImageToByteArray(dto.Icon);
                } 
                _unitOfWork.Servieces.Update(service);
                _unitOfWork.Complete();
                return Ok("Serveice updated successfully");
            }
            catch (Exception ex)
            {
                 
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("DeleteService/{id}")]
        public async Task<IActionResult>DeleteAsync (int id)
        {
            try
            {
                var service = await _unitOfWork.Servieces.GetByIdAsync(id);
                if (service == null)
                    return NotFound($"No Service was found with ID {id}");
               
                _unitOfWork.Servieces.Delete(service);
                _unitOfWork.Complete();
                return Ok("Serveice deleted successfully");

                 
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
