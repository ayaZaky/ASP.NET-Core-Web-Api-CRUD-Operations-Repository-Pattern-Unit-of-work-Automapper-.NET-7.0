using AutoMapper;
using Emc2.Core.DtoModels;
using Emc2.Core.Models;
using Emc2.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Emc2.Api.Helpers;

namespace Emc2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
 
        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var clients= await _unitOfWork.Clients.GetAllAsync();
                var dtoClients = _mapper.Map<List<DtoClientDetails>>(clients);
                return Ok(dtoClients);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }


        }
        [HttpGet("GetPagedClients")]
        public async Task<IActionResult> GetPagedAsync(int page, int pageSize)
        {
            try
            {
                var clients = await _unitOfWork.Clients.GetPagedAsync(page, pageSize);
                var dtoClients = _mapper.Map<List<DtoClientDetails>>(clients);
                return Ok(dtoClients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetClientById/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var client = await _unitOfWork.Clients.GetByIdAsync(id);
                if (client == null)
                    return NotFound($"No client was found with ID {id}");
                var dtoClient = _mapper.Map<DtoClientDetails>(client);
                return Ok(dtoClient);
                 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("AddClient")]
        public async Task<IActionResult> AddAsync([FromForm] DtoClient dto)
        {
            try
            {
                ImageOperations.ValidateImage(dto.Logo);
                var client = _mapper.Map<Client>(dto);
                client.Logo = await ImageOperations.ConvertImageToByteArray(dto.Logo);

                await _unitOfWork.Clients.AddAsync(client);
                _unitOfWork.Complete();
                return Ok("Client added successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateClient/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] DtoClient dto)
        {
            try
            {
                var client = await _unitOfWork.Clients.GetByIdAsync(id);

                if (client == null)
                    return NotFound($"No client was found with ID {id}");
                _mapper.Map(dto, client);
                if (dto.Logo != null)
                {
                    ImageOperations.ValidateImage(dto.Logo); 
                    client.Logo = await ImageOperations.ConvertImageToByteArray(dto.Logo); 
                } 
                _unitOfWork.Clients.Update(client);
                _unitOfWork.Complete();
                return Ok("Client updated successfully");
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("DeleteClient/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var client = await _unitOfWork.Clients.GetByIdAsync(id);
                if (client == null)
                    return NotFound($"No client was found with ID {id}");

                _unitOfWork.Clients.Delete(client);
                _unitOfWork.Complete();
                return Ok("Client deleted successfully"); 

            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }
    }
}
