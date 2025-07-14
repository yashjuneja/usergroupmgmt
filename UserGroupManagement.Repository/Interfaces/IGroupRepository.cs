using UserGroupManagement.Repository.Entities;

namespace UserGroupManagement.Repository.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> AddAsync(Group group, List<int> memberIds);
        Task<IEnumerable<Group>> GetAllAsync();
        Task<Group?> GetByIdAsync(int id);
        Task<Group> UpdateAsync(Group group, List<int> memberIds);
        Task<Group> DeleteAsync(int id);
    }
}
