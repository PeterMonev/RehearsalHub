namespace RehearsalHub.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RehearsalHub.Services.Data.Songs;
    using RehearsalHub.Web.ViewModels.Song;
    using System.Security.Claims;

    /// <summary>
    /// Controller for managing song-related operations including creation, listing, and deletion.
    /// </summary>
    [Authorize]
    public class SongsController : Controller
    {
        private const int PageSize = 10;
        private readonly ISongService songService;
        private readonly ILogger<SongsController> logger;

        public SongsController(ISongService songService, ILogger<SongsController> logger)
        {
            this.songService = songService;
            this.logger = logger;
        }

        /// <summary>
        /// Displays a paginated list of songs. Can be filtered by search term or band.
        /// </summary>
        /// <param name="searchTerm">String to search for in titles or artists.</param>
        /// <param name="bandId">Optional filter for songs belonging to a specific band.</param>
        /// <param name="page">The current page index.</param>
        [HttpGet]
        /// <summary>
        /// Displays a paginated list of songs. Can be filtered by search term, band, genre, key, or tempo.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index(
            string? searchTerm,
            int? bandId,
            string? genre,
            string? key,
            string? tempo,
            int page = 1)
        {
            if (page < 1) page = 1;
            var userId = GetCurrentUserId();

            try
            {
                var viewModel = await songService.GetSongsPagedAsync(
                    userId, page, PageSize, searchTerm, bandId, genre, key, tempo);

                ViewData["CurrentSearch"] = searchTerm;
                ViewData["CurrentBandFilter"] = bandId;
                ViewData["CurrentGenre"] = genre;
                ViewData["CurrentKey"] = key;
                ViewData["CurrentTempo"] = tempo;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading songs for user {UserId} on page {Page}", userId, page);
                TempData["ErrorMessage"] = "Unable to load the song library. Please try again later.";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Displays the form for creating a new song.
        /// </summary>
        /// <param name="bandId">Optional band ID to pre-bind the song to a specific band.</param>
        [HttpGet]
        public IActionResult Create(int? bandId)
        {
            return View(new SongInputModel { BandId = bandId });
        }

        /// <summary>
        /// Processes the creation of a new song.
        /// </summary>
        /// <param name="model">The song input data.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SongInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetCurrentUserId();

            try
            {
                int songId = await songService.CreateSongAsync(model, userId);

                logger.LogInformation("User {UserId} created song {SongId}", userId, songId);
                TempData["SuccessMessage"] = "Song successfully added to the library! 🎸";

                if (model.BandId.HasValue)
                {
                    return RedirectToAction("Details", "Bands", new { id = model.BandId });
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating song for user {UserId}", userId);
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while saving the song.");
                return View(model);
            }
        }

        /// <summary>
        /// Displays detailed information about a specific song.
        /// </summary>
        /// <param name="id">The song ID.</param>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var model = await songService.GetSongDetailsAsync(id);

                if (model == null)
                {
                    TempData["ErrorMessage"] = "Song not found!";
                    return RedirectToAction("Index");
                }

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.CanEdit = currentUserId != null && model.CreatorId == currentUserId;

                return View(model);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the song.";
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Deletes a song from the system.
        /// </summary>
        /// <param name="id">The ID of the song to delete.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId();

            try
            {
                bool isDeleted = await songService.DeleteSongAsync(id, userId);

                if (!isDeleted)
                {
                    TempData["ErrorMessage"] = "You do not have permission to delete this song.";
                    return RedirectToAction(nameof(Index));
                }

                logger.LogInformation("User {UserId} deleted song {SongId}", userId, id);
                TempData["SuccessMessage"] = "Song removed successfully.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting song {SongId} for user {UserId}", id, userId);
                TempData["ErrorMessage"] = "A technical error occurred during deletion.";
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the form for editing an existing song.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetCurrentUserId();

            var model = await songService.GetSongForEditAsync(id, userId);

            if (model == null)
            {
                logger.LogWarning("User {UserId} tried to edit non-existent or unauthorized song {SongId}", userId, id);
                return NotFound();
            }

            return View(model);
        }

        /// <summary>
        /// Processes the update of an existing song.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SongInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetCurrentUserId();

            try
            {
                bool isUpdated = await songService.UpdateSongAsync(model, userId);

                if (!isUpdated)
                {
                    TempData["ErrorMessage"] = "You do not have permission to edit this song.";
                    return RedirectToAction(nameof(Index));
                }

                logger.LogInformation("User {UserId} updated song {SongId}", userId, model.Id);
                TempData["SuccessMessage"] = "Song updated successfully! 🎸";

                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating song {SongId} for user {UserId}", model.Id, userId);
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while updating the song.");
                return View(model);
            }
        }

        /// <summary>
        /// Helper method to extract the current user's unique identifier.
        /// </summary>
        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}