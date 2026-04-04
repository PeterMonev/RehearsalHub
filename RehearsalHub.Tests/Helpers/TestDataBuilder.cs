using RehearsalHub.Data.Models;
using RehearsalHub.Data.Models.Enums;

namespace RehearsalHub.Tests.Helpers
{
    public static class TestDataBuilder
    {
        /// <summary>
        /// Creates a valid ApplicationUser with unique ID by default.
        /// </summary>
        public static ApplicationUser CreateUser(
            string? id = null,
            string? userName = null,
            string? email = null)
        {
            var userId = id ?? Guid.NewGuid().ToString();
            return new ApplicationUser
            {
                Id = userId,
                UserName = userName ?? $"user_{userId[..8]}",
                Email = email ?? $"{userId[..8]}@test.com",
                NormalizedEmail = (email ?? $"{userId[..8]}@test.com").ToUpper(),
                NormalizedUserName = (userName ?? $"user_{userId[..8]}").ToUpper(),
                ProfilePictureUrl = "https://example.com/avatar.png",
                CreatedOn = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Creates a valid Band owned by the given user.
        /// </summary>
        public static Band CreateBand(
            string ownerId,
            string? name = null,
            MusicGenre genre = MusicGenre.Rock,
            bool isDeleted = false)
        {
            return new Band
            {
                Name = name ?? "Test Band",
                Genre = genre,
                OwnerId = ownerId,
                ImageUrl = "https://example.com/band.png",
                IsDeleted = isDeleted,
                CreatedOn = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Creates a BandMember. Role defaults to Member, instrument to Guitar.
        /// </summary>
        public static BandMember CreateMember(
            string userId,
            BandRole role = BandRole.Member,
            InstrumentType instrument = InstrumentType.Guitar,
            bool isConfirmed = true,
            bool isDeleted = false)
        {
            return new BandMember
            {
                UserId = userId,
                Role = role,
                Instrument = instrument,
                IsConfirmed = isConfirmed,
                IsDeleted = isDeleted
            };
        }

        /// <summary>
        /// Creates a valid Song created by the given user.
        /// </summary>
        public static Song CreateSong(
            string creatorId,
            string? title = null,
            string? artist = null,
            bool isPrivate = false,
            MusicGenre genre = MusicGenre.Rock,
            MusicalKey musicalKey = MusicalKey.C,
            int? tempo = 120,
            int? ownerBandId = null)
        {
            return new Song
            {
                Title = title ?? "Test Song",
                Artist = artist ?? "Test Artist",
                Duration = "03:30",
                Genre = genre,
                MusicalKey = musicalKey,
                Tempo = tempo,
                IsPrivate = isPrivate,
                CreatorId = creatorId,
                OwnerBandId = ownerBandId,
                CreatedOn = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Creates a Rehearsal for the given band. Defaults to starting tomorrow.
        /// </summary>
        public static Rehearsal CreateRehearsal(
            int bandId,
            string? name = null,
            DateTime? start = null,
            DateTime? end = null,
            bool isDeleted = false)
        {
            var startTime = start ?? DateTime.UtcNow.AddDays(1);
            return new Rehearsal
            {
                Name = name ?? "Test Rehearsal",
                BandId = bandId,
                StartRehearsal = startTime,
                EndRehearsal = end ?? startTime.AddHours(2),
                IsDeleted = isDeleted,
                CreatedOn = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Creates a Setlist for the given band.
        /// </summary>
        public static Setlist CreateSetlist(
            int bandId,
            string? name = null,
            bool isDeleted = false)
        {
            return new Setlist
            {
                Name = name ?? "Test Setlist",
                BandId = bandId,
                IsDeleted = isDeleted,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
