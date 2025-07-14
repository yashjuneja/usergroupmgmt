using UserGroupManagement.Repository.Entities;

namespace UserGroupManagement.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> AddAsync(User user);
    }
}
