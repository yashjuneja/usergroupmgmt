using System.Net.Http.Json;
using UserGroupManagement.Common.DTOs;

namespace UserGroupManagement.Client.Services
{
    public class UserApiService : IUserApiService
    {
        private readonly HttpClient _httpClient;
        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserDto>>("api/Users/GetUsers");
            return result ?? new List<UserDto>();
        }
    }
}
