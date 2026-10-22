using System.ComponentModel.DataAnnotations;

namespace VinylHub.Web.ViewModels
{
    public class ArtistViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Naam")]
        public string Name { get; set; } = default!;

        [Display(Name = "Biografie")]
        public string? Bio { get; set; }

        public string? ExistingImageUrl { get; set; }

        [Display(Name = "Afbeelding")]
        public IFormFile? Image { get; set; }
    }
}
