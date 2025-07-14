using Microsoft.EntityFrameworkCore;
using UserGroupManagement.Repository;
using UserGroupManagement.Repository.Entities;
using UserGroupManagement.Repository.Implementations;

namespace UserGroupManagement.Tests
{
    public class UserRepositoryTest
    {
        private DataContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                            .UseInMemoryDatabase(databaseName: $"Test1")
                            .Options;

            return new DataContext(options);
        }


        [Fact]
        public async Task AddAsync_ShouldAddUser()
        {
            var context = GetInMemoryDbContext();
            var repo = new UserRepository(context);

            var user = new User { FirstName = "A", LastName = "B", Email = "a@b.com", Age = 20 };

            var result = await repo.CreateAsync(user);

            Assert.Equal("A", result.FirstName);
            Assert.Equal(1, context.Users.Count());
        }

        [Fact]
        public async Task GetAllAsync_ShouldreturnAll()
        {
            var context = GetInMemoryDbContext();
            context.Users.Add(new User { FirstName = "A", LastName = "B" });
            context.Users.Add(new User { FirstName = "C", LastName = "D" });
            await context.SaveChangesAsync();

            var repo = new UserRepository(context);
            var result = await repo.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser()
        {
            var context = GetInMemoryDbContext();
            var user = new User { FirstName = "X", LastName = "Y" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var repo = new UserRepository(context);
            var found = await repo.GetAsync(user.Id);

            Assert.NotNull(found);
            Assert.Equal("X", found.FirstName);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUser()
        {
            var context = GetInMemoryDbContext();
            var user = new User { FirstName = "Old", LastName = "Name" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var repo = new UserRepository(context);

            user.FirstName = "New";
            var updated = await repo.UpdateAsync(user);

            Assert.Equal("New", updated.FirstName);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveUser()
        {
            var context = GetInMemoryDbContext();
            var user = new User { FirstName = "Delete", LastName = "Me" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var repo = new UserRepository(context);

            var deleted = await repo.DeleteAsync(user.Id);

            Assert.Equal(user.Id, deleted.Id);
            Assert.Empty(context.Users);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnNull_WhenNotFound()
        {
            var context = GetInMemoryDbContext();
            var repo = new UserRepository(context);

            var deleted = await repo.DeleteAsync(999);

            Assert.Null(deleted);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenNotFound()
        {
            var context = GetInMemoryDbContext();
            var repo = new UserRepository(context);
            var user = new User { Id = 123, FirstName = "X" };

            await Assert.ThrowsAsync<KeyNotFoundException>(() => repo.UpdateAsync(user));
        }
    }
}
