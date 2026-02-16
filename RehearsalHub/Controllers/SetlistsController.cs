using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehearsalHub.Services.Data.Setlists;
using RehearsalHub.Web.ViewModels.Setlist;
using System.Security.Claims;

namespace RehearsalHub.Controllers
{
    [Authorize]
    public class SetlistsController : Controller
    {
        private readonly ISetlistService setlistService;
        private readonly ILogger<SetlistsController> logger;

        public SetlistsController(
            ISetlistService setlistService,
            ILogger<SetlistsController> logger)
        {
            this.setlistService = setlistService;
            this.logger = logger;
        }

        /// <summary>
        /// Display form to create a new setlist
        /// </summary>
        [HttpGet]
        public IActionResult Create(int bandId, string? bandName)
        {
            var model = new SetlistInputModel
            {
                BandId = bandId,
                BandName = bandName
            };

            return View(model);
        }

        /// <summary>
        /// Process setlist creation
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SetlistInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetCurrentUserId();

            try
            {
                int setlistId = await setlistService.CreateSetlistAsync(model, userId);

                logger.LogInformation("User {UserId} created setlist {SetlistId}", userId, setlistId);
                TempData["SuccessMessage"] = "Setlist created successfully! 🎵";

                return RedirectToAction(nameof(Details), new { id = setlistId });
            }
            catch (UnauthorizedAccessException)
            {
                TempData["ErrorMessage"] = "Only band owners can create setlists.";
                return RedirectToAction("Details", "Bands", new { id = model.BandId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating setlist for user {UserId}", userId);
                ModelState.AddModelError(string.Empty, "An error occurred while creating the setlist.");
                return View(model);
            }
        }

        /// <summary>
        /// Display setlist details with all songs
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetCurrentUserId();

            try
            {
                var model = await setlistService.GetSetlistDetailsAsync(id, userId);

                if (model == null)
                {
                    TempData["ErrorMessage"] = "Setlist not found or you don't have access to it.";
                    return RedirectToAction("Index", "Home");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading setlist {SetlistId}", id);
                TempData["ErrorMessage"] = "An error occurred while loading the setlist.";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Display form to edit setlist
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetCurrentUserId();

            var model = await setlistService.GetSetlistForEditAsync(id, userId);

            if (model == null)
            {
                logger.LogWarning("User {UserId} tried to edit unauthorized setlist {SetlistId}", userId, id);
                TempData["ErrorMessage"] = "You don't have permission to edit this setlist.";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        /// <summary>
        /// Process setlist update
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SetlistInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetCurrentUserId();

            try
            {
                bool isUpdated = await setlistService.UpdateSetlistAsync(model, userId);

                if (!isUpdated)
                {
                    TempData["ErrorMessage"] = "You don't have permission to edit this setlist.";
                    return RedirectToAction("Index", "Home");
                }

                logger.LogInformation("User {UserId} updated setlist {SetlistId}", userId, model.Id);
                TempData["SuccessMessage"] = "Setlist updated successfully! 🎵";

                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating setlist {SetlistId}", model.Id);
                ModelState.AddModelError(string.Empty, "An error occurred while updating the setlist.");
                return View(model);
            }
        }

        /// <summary>
        /// Delete a setlist
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId();

            try
            {
                var setlist = await setlistService.GetSetlistDetailsAsync(id, userId);

                if (setlist == null)
                {
                    TempData["ErrorMessage"] = "Setlist not found.";
                    return RedirectToAction("Index", "Home");
                }

                int bandId = setlist.BandId;

                bool isDeleted = await setlistService.DeleteSetlistAsync(id, userId);

                if (!isDeleted)
                {
                    TempData["ErrorMessage"] = "You don't have permission to delete this setlist.";
                    return RedirectToAction("Details", "Bands", new { id = bandId });
                }

                logger.LogInformation("User {UserId} deleted setlist {SetlistId}", userId, id);
                TempData["SuccessMessage"] = "Setlist deleted successfully.";

                return RedirectToAction("Details", "Bands", new { id = bandId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting setlist {SetlistId}", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the setlist.";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Display available songs to add to setlist
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> AddSongs(int id)
        {
            var userId = GetCurrentUserId();

            try
            {
                var model = await setlistService.GetAvailableSongsAsync(id, userId);

                if (model == null)
                {
                    TempData["ErrorMessage"] = "You don't have permission to add songs to this setlist.";
                    return RedirectToAction(nameof(Details), new { id });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading songs for setlist {SetlistId}", id);
                TempData["ErrorMessage"] = "An error occurred while loading available songs.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        /// <summary>
        /// Add selected songs to setlist
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSongs(int id, List<int> selectedSongIds)
        {
            var userId = GetCurrentUserId();

            try
            {
                if (selectedSongIds == null || !selectedSongIds.Any())
                {
                    TempData["WarningMessage"] = "No songs were selected.";
                    return RedirectToAction(nameof(AddSongs), new { id });
                }

                bool isAdded = await setlistService.AddSongsToSetlistAsync(id, selectedSongIds, userId);

                if (!isAdded)
                {
                    TempData["ErrorMessage"] = "You don't have permission to add songs to this setlist.";
                    return RedirectToAction(nameof(Details), new { id });
                }

                logger.LogInformation("User {UserId} added {Count} songs to setlist {SetlistId}",
                    userId, selectedSongIds.Count, id);

                TempData["SuccessMessage"] = $"{selectedSongIds.Count} song(s) added to setlist! 🎵";

                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding songs to setlist {SetlistId}", id);
                TempData["ErrorMessage"] = "An error occurred while adding songs.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        /// <summary>
        /// Remove a song from setlist
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveSong(int setlistId, int songId)
        {
            var userId = GetCurrentUserId();

            try
            {
                bool isRemoved = await setlistService.RemoveSongFromSetlistAsync(setlistId, songId, userId);

                if (!isRemoved)
                {
                    TempData["ErrorMessage"] = "You don't have permission to remove songs from this setlist.";
                }
                else
                {
                    logger.LogInformation("User {UserId} removed song {SongId} from setlist {SetlistId}",
                        userId, songId, setlistId);
                    TempData["SuccessMessage"] = "Song removed from setlist.";
                }

                return RedirectToAction(nameof(Details), new { id = setlistId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error removing song {SongId} from setlist {SetlistId}", songId, setlistId);
                TempData["ErrorMessage"] = "An error occurred while removing the song.";
                return RedirectToAction(nameof(Details), new { id = setlistId });
            }
        }

        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}