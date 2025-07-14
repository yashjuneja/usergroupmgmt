using UserGroupManagement.Common.DTOs;

namespace UserGroupManagement.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetAsync(int id);
        Task<UserDto> CreateAsync(UserDto dto);
        Task<UserDto> UpdateAsync(UserDto dto);
        Task<UserDto> DeleteAsync(int id);
    }
}
