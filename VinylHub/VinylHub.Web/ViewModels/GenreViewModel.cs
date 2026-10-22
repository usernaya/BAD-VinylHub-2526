using System.ComponentModel.DataAnnotations;

namespace VinylHub.Web.ViewModels
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Naam")]
        public string Name { get; set; } = default!;

        [Display(Name = "Beschrijving")]
        public string? Description { get; set; }
    }
}
