using AutoMapper;
using Emc2.Api.Helpers;
using Emc2.Core.DtoModels;
using Emc2.Core.Models;
using Emc2.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emc2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeamMemberController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllMembers")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var members = await _unitOfWork.TeamMembers.GetAllAsync();
                var dtoMembers = _mapper.Map<List<DtoTeamMemberDetails>>(members);
                return Ok(dtoMembers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }
        [HttpGet("GetPagedMembers")]
        public async Task<IActionResult> GetPagedAsync(int page, int pageSize)
        {
            try
            {
                var members = await _unitOfWork.TeamMembers.GetPagedAsync(page, pageSize);
                var dtoMembers = _mapper.Map<List<DtoTeamMemberDetails>>(members);
                return Ok(dtoMembers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetMemberById/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var member = await _unitOfWork.TeamMembers.GetByIdAsync(id);
                if (member == null)
                    return NotFound($"No member was found with ID {id}");
                var dtoClient = _mapper.Map<DtoTeamMemberDetails>(member);
                return Ok(dtoClient);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("AddMember")]
        public async Task<IActionResult> AddAsync([FromForm] DtoTeamMember dto)
        {
            try
            {
                ImageOperations.ValidateImage(dto.Image);
                var member = _mapper.Map<TeamMember>(dto);
                member.Image = await ImageOperations.ConvertImageToByteArray(dto.Image);

                await _unitOfWork.TeamMembers.AddAsync(member);
                _unitOfWork.Complete();
                return Ok("Member added successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateMember/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] DtoTeamMember dto)
        {
            try
            {
                var member = await _unitOfWork.TeamMembers.GetByIdAsync(id);

                if (member == null)
                    return NotFound($"No member was found with ID {id}");
                _mapper.Map(dto, member);
                if (dto.Image != null)
                {
                    ImageOperations.ValidateImage(dto.Image);
                    member.Image = await ImageOperations.ConvertImageToByteArray(dto.Image);
                }
                _unitOfWork.TeamMembers.Update(member);
                _unitOfWork.Complete();
                return Ok("Member updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("DeleteMember/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var member = await _unitOfWork.TeamMembers.GetByIdAsync(id);
                if (member == null)
                    return NotFound($"No member was found with ID {id}");

                _unitOfWork.TeamMembers.Delete(member);
                _unitOfWork.Complete();
                return Ok("Member deleted successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
