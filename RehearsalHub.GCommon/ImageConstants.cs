namespace RehearsalHub.Common
{
    public static class ImageConstants
    {
        private const string BasePath = "/images/defaults/";

        private static readonly string[] BandDefaults = { "band1.jpg", "band2.jpg", "band3.jpg" };
        private static readonly string[] MemberDefaults = { "member1.png", "member2.png", "member3.png"};
        private static readonly string[] UserDefaults = { "user1.png", "user2.png", "user3.png"};
        public static string GetRandomBandImage()
            => $"{BasePath}bands/{BandDefaults[Random.Shared.Next(BandDefaults.Length)]}";

        public static string GetRandomMemberImage()
            => $"{BasePath}members/{MemberDefaults[Random.Shared.Next(MemberDefaults.Length)]}";

        public static string GetRandomUserImage()
            => $"{BasePath}users/{UserDefaults[Random.Shared.Next(UserDefaults.Length)]}";
    }
}
