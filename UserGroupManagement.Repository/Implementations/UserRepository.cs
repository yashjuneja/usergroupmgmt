using Microsoft.Data.SqlClient;
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

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users
                    .Include(u => u.UserGroups)
                    .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> CreateAsync(User user)
        {

            _context.Users.AddAsync(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (IsUniqueConstraintViolation(ex))
                    throw new ArgumentException("Email address must be unique.");

                throw;
            }
            return user;

        }

        private bool IsUniqueConstraintViolation(DbUpdateException ex)
        {
            if (ex.InnerException is SqlException sqlEx)
            {
                // 2627: Violation of PRIMARY KEY or UNIQUE constraint
                // 2601: Cannot insert duplicate key row in object
                return sqlEx.Number == 2627 || sqlEx.Number == 2601;
            }

            return false;
        }


        public async Task<User> UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
                throw new KeyNotFoundException($"User with Id {user.Id} not found");

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Age = user.Age;

            await _context.SaveChangesAsync();

            return existingUser;
        }

        public async Task<User> DeleteAsync(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                throw new KeyNotFoundException($"User with Id {id} not found");

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();

            return existingUser;
        }
    }
}
