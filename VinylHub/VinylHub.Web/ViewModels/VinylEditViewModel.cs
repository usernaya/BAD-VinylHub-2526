using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace VinylHub.Web.ViewModels
{
    public class VinylEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Titel")]
        public string Title { get; set; } = default!;

        [Required]
        [Display(Name = "Artiest")]
        public int ArtistId { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [Required]
        [Display(Name = "Jaar")]
        public int ReleaseYear { get; set; }

        [Display(Name = "Label")]
        public string? Label { get; set; }

        [Display(Name = "Beschrijving")]
        public string? Description { get; set; }

        public string? ExistingImageUrl { get; set; }

        [Display(Name = "Afbeelding")]
        public IFormFile? Image { get; set; }

        public IEnumerable<SelectListItem> Artists { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Genres { get; set; } = new List<SelectListItem>();


    }
}
