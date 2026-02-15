using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Interfaces;
using RehearsalHub.Data.Models.Models;

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

        public virtual DbSet<Notification> Notifications { get; set; } = null!;

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

            builder.Entity<SetlistSong>()
                .HasQueryFilter(ss => !ss.Setlist.IsDeleted);

            builder.Entity<Notification>()
               .HasOne(n => n.Recipient)
               .WithMany(u => u.Notifications)
               .HasForeignKey(n => n.RecipientId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();

            builder.Entity<Notification>()
                .HasQueryFilter(n => !n.Recipient.IsDeleted);


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
                            (e.State == EntityState.Added ||
                             e.State == EntityState.Modified ||
                             e.State == EntityState.Deleted))
                .ToList();

            foreach (var entry in entries)
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (entity.CreatedOn == default)
                    {
                        entity.CreatedOn = DateTime.UtcNow;
                    }
                    entity.IsDeleted = false;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entity.IsDeleted = true;
                    entity.DeletedOn = DateTime.UtcNow;

                 
                    if (entity is Band band)
                    {
                        var rehearsals = this.Rehearsals.Where(r => r.BandId == band.Id);
                        foreach (var rehearsal in rehearsals)
                        {
                            rehearsal.IsDeleted = true;
                            rehearsal.DeletedOn = DateTime.UtcNow;
                        }

                        var setlists = this.Setlists.Where(s => s.BandId == band.Id);
                        foreach (var setlist in setlists)
                        {
                            setlist.IsDeleted = true;
                            setlist.DeletedOn = DateTime.UtcNow;
                        }
                    }
                }
            }
        }
    }
}