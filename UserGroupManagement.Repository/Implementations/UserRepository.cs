using Microsoft.EntityFrameworkCore;
using UserGroupManagement.Repository.Entities;
using UserGroupManagement.Repository.Interfaces;

namespace UserGroupManagement.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;                
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Include(u => u.UserGroups).ThenInclude(ug => ug.Group).ToListAsync();
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
