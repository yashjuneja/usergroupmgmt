using Microsoft.EntityFrameworkCore;
using UserGroupManagement.Repository.Entities;
using UserGroupManagement.Repository.Interfaces;

namespace UserGroupManagement.Repository.Implementations
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;

        public GroupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Group> AddAsync(Group group, List<int> memberIds)
        {
            foreach (var userId in memberIds)
            {
                group.UserGroups.Add(new UserGroup { UserId = userId });
            }
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            await _context.Entry(group)
                    .Collection(g => g.UserGroups)
                    .Query()
                    .Include(ug => ug.User)
                    .LoadAsync();

            return group;
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            var result = await _context.Groups
                                .Include(g => g.UserGroups)
                                .ThenInclude(ug => ug.User)
                                .ToListAsync();
            return result;
        }

        public async Task<Group?> GetByIdAsync(int id)
        {
            return await _context.Groups.Include(g => g.UserGroups).ThenInclude(ug => ug.User)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Group> UpdateAsync(Group group, List<int> memberIds)
        {
            var existing = await _context.Groups
                                    .Include(g => g.UserGroups)
                                    .FirstOrDefaultAsync(g => g.Id == group.Id);

            if (existing == null) 
                throw new Exception($"Group with id {group.Id} not found");

            existing.GroupName = group.GroupName;

            _context.UserGroups.RemoveRange(existing.UserGroups);
            foreach (var userId in memberIds)
            {
                existing.UserGroups.Add(new UserGroup { UserId = userId });
            }

            await _context.SaveChangesAsync();

            await _context.Entry(existing)
                .Collection(g => g.UserGroups)
                .Query()
                .Include(ug => ug.User)
                .LoadAsync();

            return existing;
        }

        public async Task<Group> DeleteAsync(int id)
        {
            var group = await _context.Groups
                                .Include(g => g.UserGroups)
                                .FirstOrDefaultAsync(g => g.Id == id);

            if (group == null)
                throw new KeyNotFoundException($"Group with Id {id} not found.");

            _context.UserGroups.RemoveRange(group.UserGroups);

            _context.Groups.Remove(group);

            await _context.SaveChangesAsync();

            return group;
        }
    }
}
