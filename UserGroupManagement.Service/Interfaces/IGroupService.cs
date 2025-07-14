using UserGroupManagement.Common.DTOs;

namespace UserGroupManagement.Service.Interfaces
{
    public interface IGroupService
    {
        Task<GroupDto> CreateAsync(GroupCreateDto dto);
        Task<IEnumerable<GroupDto>> GetAllAsync();
        Task<GroupDto> GetByIdAsync(int id);
        Task<GroupDto> UpdateAsync(GroupUpdateDto dto);
        Task<GroupDto> DeleteAsync(int id);
    }
}
