using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Interfaces;

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
            base.OnModelCreating(builder);

            builder.Entity<BandMember>()
                .HasIndex(bm => new { bm.UserId, bm.BandId })
                .IsUnique();

            builder.Entity<BandMember>()
                .HasOne(bm => bm.User)
                .WithMany(u => u.BandMembers)
                .HasForeignKey(bm => bm.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SetlistSong>()
                .HasKey(ss => new { ss.SetlistId, ss.SongId });

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

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