using UserGroupManagement.Common.DTOs;

namespace UserGroupManagement.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> AddAsync(UserDto userDto);
    }
}
