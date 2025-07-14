using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserGroupManagement.Api.Controllers;
using UserGroupManagement.Common.DTOs;
using UserGroupManagement.Service.Interfaces;

namespace UserGroupManagement.Tests
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _mockService;
        private readonly UsersController _controller;
        private readonly Mock<IMapper> _mapper;

        //public UserControllerTests()
        //{
        //    _mockService = new Mock<IUserService>();
        //    _mapper = new Mock<IMapper>();
        //    _controller = new UsersController(_mockService.Object, _mapper.Object);
        //}

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithListOfUsers()
        {
            // Arrange
            var users = new List<UserDto> {
            new UserDto { Id = 1, FirstName = "A", LastName = "B" },
            new UserDto { Id = 2, FirstName = "C", LastName = "D" }
        };

            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUsers = Assert.IsType<List<UserDto>>(okResult.Value);
            Assert.Equal(2, returnUsers.Count);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenUserExists()
        {
            var user = new UserDto { Id = 1, FirstName = "Test", LastName = "User" };
            _mockService.Setup(s => s.GetAsync(1)).ReturnsAsync(user);

            var result = await _controller.Get(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal(user.Id, returned.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenUserNotExists()
        {
            _mockService.Setup(s => s.GetAsync(1)).ReturnsAsync((UserDto?)null);

            var result = await _controller.Get(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction()
        {
            var dto = new UserDto { FirstName = "X", LastName = "Y" };
            var created = new UserDto { Id = 10, FirstName = "X", LastName = "Y" };

            _mockService.Setup(s => s.CreateAsync(dto)).ReturnsAsync(created);

            var result = await _controller.Create(dto);

            var createdAt = Assert.IsType<CreatedAtActionResult>(result);
            var returned = Assert.IsType<UserDto>(createdAt.Value);
            Assert.Equal(10, returned.Id);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenIdMatches()
        {
            var dto = new UserDto { Id = 5, FirstName = "Edit", LastName = "Name" };
            _mockService.Setup(s => s.UpdateAsync(dto)).ReturnsAsync(dto);

            var result = await _controller.Update(5, dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<UserDto>(ok.Value);
            Assert.Equal(5, returned.Id);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenIdMismatch()
        {
            var dto = new UserDto { Id = 3, FirstName = "X" };

            var result = await _controller.Update(1, dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Id mismatch", badRequest.Value);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenDeleted()
        {
            var user = new UserDto { Id = 1, FirstName = "X" };
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(user);

            var result = await _controller.Delete(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<UserDto>(ok.Value);
            Assert.Equal(1, returned.Id);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenNotFound()
        {
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync((UserDto?)null);

            var result = await _controller.Delete(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
