using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RehearsalHub.Data;

namespace RehearsalHub.Tests.Helpers
{
    public static class TestDbContextFactory
    {
        /// <summary>
        /// Creates a fresh, empty InMemory database for one test.
        /// No seed data — only what the test explicitly adds.
        /// Each call with a unique name gives a completely isolated database.
        /// </summary>
        public static ApplicationDbContext Create(string? databaseName = null)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName ?? Guid.NewGuid().ToString())
                .ConfigureWarnings(w =>
                    w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
