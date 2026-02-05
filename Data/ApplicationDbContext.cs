using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RehearsalHub.Models;
using RehearsalHub.Models.Interfaces;

namespace RehearsalHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public virtual DbSet<Band> Bands { get; set; } = null!;

        public virtual DbSet<BandMember> BandMembers { get; set; } = null!;

        public virtual DbSet<Rehearsal> Rehearsals { get; set; } = null!;

        public virtual DbSet<Setlist> Setlists { get; set; } = null!;

        public virtual DbSet<SetlistSong> SetlistSongs { get; set; } = null!;

        public virtual DbSet<Song> Songs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Важно: Винаги викай base.OnModelCreating за Identity таблиците
            base.OnModelCreating(builder);

            // Уникален индекс за членовете на бандата
            builder.Entity<BandMember>()
                .HasIndex(bm => new { bm.UserId, bm.BandId })
                .IsUnique();

            // Конфигурация на връзката BandMember - ApplicationUser
            builder.Entity<BandMember>()
                .HasOne(bm => bm.User)
                .WithMany(u => u.BandMembers)
                .HasForeignKey(bm => bm.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Композитен ключ за SetlistSong
            builder.Entity<SetlistSong>()
                .HasKey(ss => new { ss.SetlistId, ss.SongId });

            // Автоматично прилагане на всички конфигурации (Seed данни, специфични настройки) от асемблито
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        // Пренаписваме всички методи за запис, за да сме сигурни, че логиката ще се изпълни
        public override int SaveChanges()
        {
            this.ApplyAuditInfo();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfo();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfo();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyAuditInfo()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditInfo &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (entity.CreatedOn == default)
                    {
                        entity.CreatedOn = DateTime.UtcNow;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}