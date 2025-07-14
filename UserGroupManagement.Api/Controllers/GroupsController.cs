using Microsoft.AspNetCore.Mvc;
using UserGroupManagement.Common.DTOs;
using UserGroupManagement.Service.Interfaces;

namespace UserGroupManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("GetGroups")]
        public async Task<IActionResult> GetAll()
        {
            var groups = await _groupService.GetAllAsync();
            return Ok(groups);
        }

        [HttpGet("GetGroupById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var group = await _groupService.GetByIdAsync(id);
            if (group == null)
                return NotFound();

            return Ok(group);
        }

        [HttpPost("CreateGroup")]
        public async Task<IActionResult> Create([FromBody] GroupCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdGroup = await _groupService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdGroup.Id }, createdGroup);
        }

        [HttpPut("UpdateGroup/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]GroupUpdateDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id) 
                return BadRequest("Id mismatch.");

            var result = await _groupService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("DeleteGroup/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedGroup = await _groupService.DeleteAsync(id);
                return Ok(deletedGroup);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}