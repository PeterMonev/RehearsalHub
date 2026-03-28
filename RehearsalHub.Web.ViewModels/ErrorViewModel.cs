namespace RehearsalHub.Web.ViewModels
{
    /// <summary>
    /// ViewModel used for displaying HTTP error pages.
    /// Carries all information needed for the view to render
    /// an appropriate, user-friendly error message.
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>HTTP status code, e.g. 400, 401, 403, 404, 500.</summary>
        public int StatusCode { get; set; }

        /// <summary>Short human-readable title shown as the page heading.</summary>
        public string Title { get; set; } = "Error";

        /// <summary>Descriptive message explaining what went wrong.</summary>
        public string Message { get; set; } = "An unexpected error has occurred.";

        /// <summary>
        /// Font Awesome icon class rendered inside the icon ring.
        /// Example: "fa-solid fa-magnifying-glass"
        /// </summary>
        public string IconClass { get; set; } = "fa-solid fa-circle-exclamation";

        /// <summary>
        /// Whether to show a "Go back" button.
        /// Hidden on 401 to avoid revealing protected route existence.
        /// </summary>
        public bool ShowBackButton { get; set; } = true;

        /// <summary>
        /// Creates a pre-configured <see cref="ErrorViewModel"/> for the given
        /// HTTP status code. Unknown codes fall back to a generic 500-style message.
        /// </summary>
        public static ErrorViewModel ForStatusCode(int code) => code switch
        {
            400 => new ErrorViewModel
            {
                StatusCode = 400,
                Title = "Bad Request",
                Message = "The request could not be understood by the server. " +
                                 "Please check your input and try again.",
                IconClass = "fa-solid fa-triangle-exclamation",
                ShowBackButton = true
            },
            401 => new ErrorViewModel
            {
                StatusCode = 401,
                Title = "Authentication Required",
                Message = "You need to be signed in to access this page. " +
                                 "Please log in and try again.",
                IconClass = "fa-solid fa-lock",
                ShowBackButton = false
            },
            403 => new ErrorViewModel
            {
                StatusCode = 403,
                Title = "Access Denied",
                Message = "You do not have permission to perform this action. " +
                                 "If you believe this is a mistake, please contact an administrator.",
                IconClass = "fa-solid fa-ban",
                ShowBackButton = true
            },
            404 => new ErrorViewModel
            {
                StatusCode = 404,
                Title = "Page Not Found",
                Message = "The page you are looking for does not exist, " +
                                 "has been removed, or has been moved to a different location.",
                IconClass = "fa-solid fa-magnifying-glass",
                ShowBackButton = true
            },
            _ => new ErrorViewModel
            {
                StatusCode = 500,
                Title = "Internal Server Error",
                Message = "Something went wrong on our end. " +
                                 "Our team has been notified. Please try again later.",
                IconClass = "fa-solid fa-server",
                ShowBackButton = true
            }
        };
    }
}
