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

        // POST: api/Groups
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]GroupCreateDto dto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var createdGroup = await _groupService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = createdGroup.Id}, createdGroup);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var group = await _groupService.GetByIdAsync(id);
            if (group == null)
                return NotFound();

            return Ok(group);
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var groups = await _groupService.GetAllAsync();
            return Ok(groups);
        }

        // GET: api/Groups/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    var group = await _groupService.GetByIdAsync(id);
        //    return Ok(group);
        //}

        // PUT: api/Groups
        [HttpPut]
        public async Task<IActionResult> Update(GroupDto dto)
        {
            if (dto == null) return BadRequest("Invalid group data.");
            var result = await _groupService.UpdateAsync(dto);
            return Ok(result);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _groupService.DeleteAsync(id);
            return Ok();
        }
    }
}