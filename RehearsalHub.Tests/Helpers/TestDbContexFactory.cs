using Microsoft.EntityFrameworkCore;
using RehearsalHub.Data;

namespace RehearsalHub.Tests.Helpers
{
    public static class TestDbContextFactory
    {
        /// <summary>
        /// Creates a fresh isolated InMemory database for one test.
        /// Each call with a unique name gives a completely empty database.
        /// </summary>
        public static ApplicationDbContext Create(string? databaseName = null)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName ?? Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();

            return context;
        }
    }
}
