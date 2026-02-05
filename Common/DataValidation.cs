using RehearsalHub.Models;

namespace RehearsalHub.Common
{
    public static class DataValidation
    {
        public static class Profile {
            public const int ImageUrlMaxLength = 2048;
        }
        public static class Band {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 150;

            public const int ImageUrlMaxLength = 2048;
        }

        public static class BandMember
        {
            public const int AvatarUrlMaxLength = 2048;
        }

        public static class Rehearsal
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 200;

            public const int NotesMinLength = 10;
            public const int NotesMaxLength = 500;
        }

        public static class Setlist
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 150;
        }

        public static class Song
        {
            public const int ArtistMinLength = 2;
            public const int ArtistMaxLength = 150;

            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 150;

            public const int DurationMinLength = 4;
            public const int DurationMaxLength = 5;

            public const string DurationRegex = @"^\d{1,2}:\d{2}$";
        }
    }
}
