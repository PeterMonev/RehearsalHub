namespace RehearsalHub.GCommon.Helpers
{
    /// <summary>
    /// Helper class for music-related business logic
    /// </summary>
    public static class MusicHelper
    {
        /// <summary>
        /// Categorizes tempo into slow/medium/fast
        /// </summary>
        /// <param name="tempo">Tempo in BPM (beats per minute)</param>
        /// <returns>Category: slow, medium, fast, or unknown</returns>
        public static string GetTempoCategory(int? tempo)
        {
            if (!tempo.HasValue)
                return "unknown";

            if (tempo < 80)
                return "slow";

            if (tempo <= 120)
                return "medium";

            return "fast";
        }

        /// <summary>
        /// Parses duration string (mm:ss) to total seconds
        /// </summary>
        /// <param name="duration">Duration in format "mm:ss"</param>
        /// <returns>Total seconds</returns>
        public static int ParseDurationToSeconds(string duration)
        {
            if (string.IsNullOrEmpty(duration))
                return 0;

            var parts = duration.Split(':');
            if (parts.Length != 2)
                return 0;

            if (int.TryParse(parts[0], out int minutes) && int.TryParse(parts[1], out int seconds))
            {
                return minutes * 60 + seconds;
            }

            return 0;
        }

        /// <summary>
        /// Formats total seconds to duration string (mm:ss)
        /// </summary>
        /// <param name="totalSeconds">Total seconds</param>
        /// <returns>Formatted duration "mm:ss"</returns>
        public static string FormatSecondsToTimeString(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            return $"{minutes}:{seconds:D2}";
        }

        /// <summary>
        /// Calculates total duration for a list of songs
        /// </summary>
        /// <param name="durations">List of duration strings</param>
        /// <returns>Total duration as formatted string "mm:ss"</returns>
        public static string CalculateTotalDuration(IEnumerable<string> durations)
        {
            int totalSeconds = durations.Sum(ParseDurationToSeconds);
            return FormatSecondsToTimeString(totalSeconds);
        }
    }
}
