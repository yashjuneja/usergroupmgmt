using UserGroupManagement.Common.DTOs;

namespace UserGroupManagement.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetAsync(int id);
        Task<UserDto> AddAsync(UserDto userDto);
        Task<UserDto> UpdateAsync(UserDto userDto);
        Task<UserDto> DeleteAsync(int id);
    }
}
