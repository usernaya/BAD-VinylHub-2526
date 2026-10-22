using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylHub.Application.Interfaces.Services;
using VinylHub.Application.Services;
using VinylHub.Domain.Entities;
using VinylHub.Web.ViewModels;

namespace VinylHub.Web.Controllers
{
    
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly FileUploadService _fileUploadService;
        private readonly IWebHostEnvironment _env;

        public ArtistController(IArtistService artistService, FileUploadService fileUploadService, IWebHostEnvironment env)
        {
            _artistService = artistService;
            _fileUploadService = fileUploadService;
            _env = env;
        }

        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Index()
        {
            var artists = _artistService.GetAll();
            return View(artists);
        }

        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Create()
        {
            return View(new ArtistViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Create(ArtistViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            Artist artist = new Artist { Name = vm.Name, Bio = vm.Bio };

            if (vm.Image != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(vm.Image.FileName);
                string filepath = Path.Combine(_env.WebRootPath, "img", filename);
                _fileUploadService.UploadFile(vm.Image.OpenReadStream(), filepath);
                artist.ImageUrl = "/img/" + filename;
            }

            _artistService.Add(artist);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Edit(int id)
        {
            Artist? artist = _artistService.GetById(id);
            if (artist == null) return NotFound();

            ArtistViewModel vm = new ArtistViewModel
            {
                Id = artist.Id,
                Name = artist.Name,
                Bio = artist.Bio,
                ExistingImageUrl = artist.ImageUrl
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Edit(ArtistViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            Artist? artist = _artistService.GetById(vm.Id);
            if (artist == null) return NotFound();

            artist.Name = vm.Name;
            artist.Bio = vm.Bio;

            if (vm.Image != null)
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(vm.Image.FileName);
                string filepath = Path.Combine(_env.WebRootPath, "img", filename);
                _fileUploadService.UploadFile(vm.Image.OpenReadStream(), filepath);
                artist.ImageUrl = "/img/" + filename;
            }

            _artistService.Update(artist);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            Artist? artist = _artistService.GetById(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Delete(int id)
        {
            _artistService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
