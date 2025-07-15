using UserGroupManagement.Common.DTOs;

namespace UserGroupManagement.Client.Services
{
    public interface IGroupApiService
    {
        Task<List<GroupDto>> GetAllGroupsAsync();
        Task<GroupDto?> GetGroupByIdAsync(int id);
        Task<GroupDto> CreateGroupAsync(GroupDto group);
        Task<GroupDto> UpdateGroupAsync(GroupDto group);
        Task<GroupDto> DeleteGroupAsync(int id);
    }
}
