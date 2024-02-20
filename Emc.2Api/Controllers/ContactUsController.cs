using AutoMapper;
using Emc2.Core.DtoModels;
using Emc2.Core.Models;
using Emc2.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.Execution;
using System.Reflection.Metadata.Ecma335;

namespace Emc2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactUsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

         
        [HttpGet("GetAllContacts")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var contacts=await _unitOfWork.Contacts.GetAllAsync();
                var dtoContacts = _mapper.Map<List<DtoContactDetails>>(contacts);
                return Ok(dtoContacts);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }


        }
        [HttpGet("GetPagedContacts")]
        public async Task<IActionResult> GetPagedAsync(int page, int pageSize)
        {
            try
            {
                var contacts = await _unitOfWork.Contacts.GetPagedAsync(page, pageSize);
                var dtoContacts = _mapper.Map<List<DtoContactDetails>>(contacts);
                return Ok(dtoContacts);
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetContactById/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var contact = await _unitOfWork.Contacts.GetByIdAsync(id);
                if (contact == null)
                    return NotFound($"No contact was found with ID {id}");
                var dtoContact = _mapper.Map<DtoContactDetails>(contact);
                return Ok(dtoContact);
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpPost("AddContact")]
        public async Task<IActionResult> AddAsync([FromForm] DtoContact dto)
        {
            try
            {   
                var contact = _mapper.Map<ContactUs>(dto); 
                await _unitOfWork.Contacts.AddAsync(contact);
                _unitOfWork.Complete();
                return Ok("Contact added successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateContact/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] DtoContact dto)
        {
            try
            {
                var contact = await _unitOfWork.Contacts.GetByIdAsync(id);

                if (contact == null)
                    return NotFound($"No contact was found with ID {id}");
                contact = _mapper.Map(dto,contact);
                _unitOfWork.Contacts.Update(contact);
                _unitOfWork.Complete();
                return Ok("Contact updated successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("DeleteContact/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var contact = await _unitOfWork.Contacts.GetByIdAsync(id);
                if (contact == null)
                    return NotFound($"No contact was found with ID {id}");

                _unitOfWork.Contacts.Delete(contact);
                _unitOfWork.Complete();
                return Ok("Contact deleted successfully"); 
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }
    }
}
