namespace RehearsalHub.Common.Helpers
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Validates that end time is after start time
        /// </summary>
        public static bool IsValidTimeRange(DateTime start, DateTime end)
        {
            return end > start;
        }

        /// <summary>
        /// Validates that rehearsal start time is not in the past
        /// </summary>
        public static bool IsNotInPast(DateTime rehearsalStart)
        {
            return rehearsalStart >= DateTime.Now;
        }

        /// <summary>
        /// Calculates duration between two dates
        /// </summary>
        public static TimeSpan GetDuration(DateTime start, DateTime end)
        {
            return end - start;
        }

        /// <summary>
        /// Formats duration to human-readable string (e.g., "2h 30m")
        /// </summary>
        public static string FormatDuration(TimeSpan duration)
        {
            if (duration.TotalHours >= 1)
                return $"{(int)duration.TotalHours}h {duration.Minutes}m";

            return $"{duration.Minutes}m";
        }

        /// <summary>
        /// Checks if a rehearsal is upcoming (in the future)
        /// </summary>
        public static bool IsUpcoming(DateTime rehearsalStart)
        {
            return rehearsalStart > DateTime.Now;
        }

        /// <summary>
        /// Checks if a rehearsal is happening right now
        /// </summary>
        public static bool IsHappeningNow(DateTime start, DateTime end)
        {
            var now = DateTime.Now;
            return now >= start && now <= end;
        }

        public static string FormatDateForDisplay(DateTime date)
            => date.ToString("ddd, MMM dd, yyyy");

        public static string FormatTimeForDisplay(DateTime date)
            => date.ToString("HH:mm");

        public static string FormatFullDateTimeForDisplay(DateTime date)
            => date.ToString("ddd, MMM dd, yyyy 'at' HH:mm");
    }
}