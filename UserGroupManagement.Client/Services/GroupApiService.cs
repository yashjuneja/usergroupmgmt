using System.Net.Http.Json;
using UserGroupManagement.Common.DTOs;

namespace UserGroupManagement.Client.Services
{
    public class GroupApiService : IGroupApiService
    {
        private readonly HttpClient _httpClient;
        public GroupApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GroupDto> CreateGroupAsync(GroupDto group)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Groups/CreateGroup", group);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GroupDto>();
        }

        public async Task<GroupDto> DeleteGroupAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Groups/DeleteGroup/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GroupDto>();
        }

        public async Task<List<GroupDto>> GetAllGroupsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<GroupDto>>("/api/Groups/GetGroups");
            return response ?? new List<GroupDto>();
        }

        public async Task<GroupDto?> GetGroupByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<GroupDto>($"api/Groups/GetGroupById/{id}");
            return response ?? new GroupDto();
        }

        public async Task<GroupDto> UpdateGroupAsync(GroupDto group)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Groups/UpdateGroup/{group.Id}", group);
            response?.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GroupDto>();
        }
    }
}
