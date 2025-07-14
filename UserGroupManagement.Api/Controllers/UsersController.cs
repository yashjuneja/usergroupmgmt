using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserGroupManagement.Common.DTOs;
using UserGroupManagement.Service.Interfaces;

namespace UserGroupManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("GetUserbyId/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetAsync(id);
            return user == null ? NotFound("User does not exist") : Ok(user);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create(UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDto = _mapper.Map<UserDto>(dto);
            
            var result = await _userService.CreateAsync(userDto);
            return Ok(result);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != userDto.Id)
                return BadRequest("Id mismatch");

            var user = await _userService.UpdateAsync(userDto);
            return Ok(user);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.DeleteAsync(id);
            return Ok(user);
        }
    }
}
