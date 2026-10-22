using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylHub.Application.Interfaces.Services;
using VinylHub.Domain.Entities;
using VinylHub.Web.ViewModels;

namespace VinylHub.Web.Controllers
{
    
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Index()
        {
            var genres = _genreService.GetAll();
            return View(genres);
        }

        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Create()
        {
            return View(new GenreViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Create(GenreViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            Genre genre = new Genre
            {
                Name = vm.Name,
                Description = vm.Description
            };

            _genreService.Add(genre);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Edit(int id)
        {
            Genre? genre = _genreService.GetById(id);
            if (genre == null) return NotFound();

            GenreViewModel vm = new GenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Edit(GenreViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            Genre? genre = _genreService.GetById(vm.Id);
            if (genre == null) return NotFound();

            genre.Name = vm.Name;
            genre.Description = vm.Description;

            _genreService.Update(genre);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult Delete(int id)
        {
            _genreService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            Genre? genre = _genreService.GetById(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }
    }
}
