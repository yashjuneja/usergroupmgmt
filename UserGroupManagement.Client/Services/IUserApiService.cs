using UserGroupManagement.Common.DTOs;

namespace UserGroupManagement.Client.Services
{
    public interface IUserApiService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(UserDto user);
        Task<UserDto> UpdateUserAsync(UserDto user);
        Task<UserDto> DeleteUserAsync(int id);
    }
}
