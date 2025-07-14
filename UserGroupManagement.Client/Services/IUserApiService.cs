using UserGroupManagement.Common.DTOs;

namespace UserGroupManagement.Client.Services
{
    public interface IUserApiService
    {
        Task<List<UserDto>> GetAllUsersAsync();
    }
}
