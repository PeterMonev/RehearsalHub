using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RehearsalHub.Models;
using static RehearsalHub.Common.ImageConstants;

namespace RehearsalHub.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        private static readonly PasswordHasher<ApplicationUser> _hasher = new PasswordHasher<ApplicationUser>();

        private readonly IEnumerable<ApplicationUser> Users = new List<ApplicationUser>()
        {
                new ApplicationUser
                {
                    Id = "seed-user-1",
                    UserName = "rockstar@test.com",
                    NormalizedUserName = "ROCKSTAR@TEST.COM",
                    Email = "rockstar@test.com",
                    NormalizedEmail = "ROCKSTAR@TEST.COM",
                    EmailConfirmed = true,
                    SecurityStamp = "59846067-8896-4874-9160-5582f3c306d1",
                    ProfilePictureUrl = GetRandomUserImage(),
                    PasswordHash = _hasher.HashPassword(null!, "P@ssword123"),
                    CreatedOn = DateTime.UtcNow
                },
                new ApplicationUser
                {
                    Id = "seed-user-2",
                    UserName = "metalhead@test.com",
                    NormalizedUserName = "METALHEAD@TEST.COM",
                    Email = "metalhead@test.com",
                    NormalizedEmail = "METALHEAD@TEST.COM",
                    EmailConfirmed = true,
                    SecurityStamp = "f4c9448a-6f4e-4f0e-9180-2a86d2358899",
                    ProfilePictureUrl = GetRandomUserImage(),
                    PasswordHash = _hasher.HashPassword(null!, "P@ssword123"),
                    CreatedOn = DateTime.UtcNow
                },
                new ApplicationUser
                {
                    Id = "seed-user-3",
                    UserName = "jazzman@test.com",
                    NormalizedUserName = "JAZZMAN@TEST.COM",
                    Email = "jazzman@test.com",
                    NormalizedEmail = "JAZZMAN@TEST.COM",
                    EmailConfirmed = true,
                    SecurityStamp = "788019a3-5c56-4b8c-8f96-339832679f22",
                    ProfilePictureUrl = GetRandomUserImage(),
                    PasswordHash = _hasher.HashPassword(null!, "P@ssword123"),
                    CreatedOn = DateTime.UtcNow
                }
        };

        public void Configure(EntityTypeBuilder<ApplicationUser> entity)
        {
            entity.HasData(Users);
        }
    }
}