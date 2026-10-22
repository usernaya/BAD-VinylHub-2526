using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VinylHub.Application.Interfaces.Services;
using VinylHub.Application.Services;
using VinylHub.Domain.Entities;
using VinylHub.Web.ViewModels;

namespace VinylHub.Web.Controllers
{
    public class VinylController : Controller
    {
        private readonly IVinylService _vinylService;
        private readonly IWebHostEnvironment _env;
        private readonly FileUploadService _fileUploadService;

        public VinylController(IVinylService vinylService, IWebHostEnvironment env, FileUploadService fileUploadService)
        {
            _vinylService = vinylService;
            _env = env;
            _fileUploadService = fileUploadService;
        }
        public IActionResult Index(string? searchTerm, int? artistId, int? genreId)
        {
            VinylFilterViewModel vm = new VinylFilterViewModel
            {
                SearchTerm = searchTerm,
                ArtistId = artistId,
                GenreId = genreId,
                Vinyls = _vinylService.GetFiltered(searchTerm, artistId, genreId),
                Artists = _vinylService.GetAllArtists()
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                    .ToList(),
                Genres = _vinylService.GetAllGenres()
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                    .ToList()
            };

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            Vinyl? vinyl = _vinylService.GetById(id);
            if (vinyl == null)
            {
                return NotFound();
            }
            return View(vinyl);
        }

        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Create()
        {
            var vm = new VinylCreateViewModel
            {
                Artists = GetArtistSelectList(),
                Genres = GetGenreSelectList()
            };
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Create(VinylCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Artists = GetArtistSelectList();
                vm.Genres = GetGenreSelectList();
                return View(vm);
            }

            Artist? artist = _vinylService.GetAllArtists().First(x => x.Id == vm.ArtistId);
            Genre? genre = _vinylService.GetAllGenres().First(x => x.Id == vm.GenreId);

            var vinyl = new Vinyl
            {
                Title = vm.Title,
                ReleaseYear = vm.ReleaseYear,
                Description = vm.Description,
                Label = vm.Label,
                IsAvailable = true,
                Artist = artist,
                Genre = genre
            };

            if (vm.Image != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(vm.Image.FileName);
                string filepath = Path.Combine(_env.WebRootPath, "img", filename);
                _fileUploadService.UploadFile(vm.Image.OpenReadStream(), filepath);
                vinyl.CoverImageUrl = "/img/" + filename;
            }

            _vinylService.Add(vinyl);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Edit(int id)
        {
            Vinyl? vinyl = _vinylService.GetById(id);
            if (vinyl == null)
            {
                return NotFound();
            }

            var vm = new VinylEditViewModel
            {
                Id = vinyl.Id,
                Title = vinyl.Title,
                ReleaseYear = vinyl.ReleaseYear,
                Description = vinyl.Description,
                Label = vinyl.Label,
                ArtistId = vinyl.Artist.Id,
                GenreId = vinyl.Genre.Id,
                ExistingImageUrl = vinyl.CoverImageUrl,
                Artists = GetArtistSelectList(),
                Genres = GetGenreSelectList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Edit(VinylEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Artists = GetArtistSelectList();
                vm.Genres = GetGenreSelectList();
                return View(vm);
            }

            Vinyl? vinyl = _vinylService.GetById(vm.Id);
            if (vinyl == null) return NotFound();

            vinyl.Title = vm.Title;
            vinyl.ReleaseYear = vm.ReleaseYear;
            vinyl.Description = vm.Description;
            vinyl.Label = vm.Label;
            vinyl.Artist = _vinylService.GetAllArtists().First(x => x.Id == vm.ArtistId);
            vinyl.Genre = _vinylService.GetAllGenres().First(x => x.Id == vm.GenreId);

            if (vm.Image != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(vm.Image.FileName);
                string filepath = Path.Combine(_env.WebRootPath, "img", filename);
                _fileUploadService.UploadFile(vm.Image.OpenReadStream(), filepath);
                vinyl.CoverImageUrl = "/img/" + filename;
            }

            _vinylService.Update(vinyl);
            return RedirectToAction(nameof(Details), new { id = vm.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Delete(int id)
        {
            _vinylService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        private IEnumerable<SelectListItem> GetArtistSelectList()
        {
            return _vinylService.GetAllArtists()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        private IEnumerable<SelectListItem> GetGenreSelectList()
        {
            return _vinylService.GetAllGenres()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

    }
}
