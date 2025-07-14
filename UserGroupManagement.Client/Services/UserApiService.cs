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

        public async Task<UserDto> CreateUserAsync(UserDto user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Users/CreateUser", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        public async Task<UserDto> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Users/DeleteUser/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserDto>>("api/Users/GetUsers");
            return result ?? new List<UserDto>();
        }

        public async Task<UserDto> GetUserByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<UserDto>($"api/Users/GetUserbyId/{id}") ?? new UserDto();
        

        public async Task<UserDto> UpdateUserAsync(UserDto user)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Users/UpdateUser/{user.Id}", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }
    }
}
