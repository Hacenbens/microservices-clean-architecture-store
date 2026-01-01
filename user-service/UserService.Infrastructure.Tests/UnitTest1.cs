using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistance; // match your folder/namespace
using Xunit;

namespace UserService.Infrastructure.Tests
{
    public class UserDbContextTests
    {
        private DbContextOptions<UserDbContext> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<UserDbContext>()
                .UseSqlite("DataSource=:memory:") // in-memory SQLite
                .Options;
        }

        [Fact]
        public void Can_Create_Database_And_Add_User()
        {
            // Arrange
            var options = CreateInMemoryOptions();

            using (var context = new UserDbContext(options))
            {
                context.Database.OpenConnection();   // required for SQLite in-memory
                context.Database.EnsureCreated();    // creates schema

                // Act: create a user via factory
                var userResult = User.Create("test@example.com", "Test User");

                Assert.True(userResult.IsSuccess); // ensure creation succeeded

                context.Users.Add(userResult.Value!); // add user to context
                context.SaveChanges();
            }

            // Assert: verify user was saved
            using (var context = new UserDbContext(options))
            {
                var user = context.Users.FirstOrDefault();
                Assert.NotNull(user);
                Assert.Equal("test@example.com", user.Email);
                Assert.Equal("Test User", user.Name);
            }
        }
    }
}