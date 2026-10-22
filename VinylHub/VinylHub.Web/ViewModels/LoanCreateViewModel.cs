using System.ComponentModel.DataAnnotations;

namespace VinylHub.Web.ViewModels
{
    public class LoanCreateViewModel
    {
        public int VinylId { get; set; }
        public string VinylTitle { get; set; } = default!;
        public string ArtistName { get; set; } = default!;

        [Required]
        [Display(Name = "Leenduur")]
        [Range(1, 30, ErrorMessage = "Leenduur moet tussen 1 en 30 dagen zijn")]
        public int LoanDays { get; set; } = 14; 
    }
}
