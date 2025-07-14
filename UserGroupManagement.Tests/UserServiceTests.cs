using AutoMapper;
using Moq;
using System.Linq.Expressions;
using UserGroupManagement.Common;
using UserGroupManagement.Common.DTOs;
using UserGroupManagement.Repository.Entities;
using UserGroupManagement.Repository.Interfaces;
using UserGroupManagement.Service.Implementations;

namespace UserGroupManagement.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task AddAsync_ShouldAddUser()
        {
            var mockRepo = new Mock<IUserRepository>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>());
            //var config = new MapperConfiguration(cfg =>
            //{

            //});
            var mapper = config.CreateMapper();
            var service = new UserService(mockRepo.Object, mapper);

            var dto = new UserDto { FirstName = "Test", LastName = "User", Age = 50, Email = "test.user@sample.com" };
            var user = await service.CreateAsync(dto);

            mockRepo.Verify(p => p.AddAsync(It.IsAny<User>()), Times.Once);
            Assert.Equal("Test", user.FirstName);
        }
    }
}