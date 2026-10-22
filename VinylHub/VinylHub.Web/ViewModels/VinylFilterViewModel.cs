using Microsoft.AspNetCore.Mvc.Rendering;
using VinylHub.Domain.Entities;

namespace VinylHub.Web.ViewModels
{
    public class VinylFilterViewModel
    {
        public string? SearchTerm { get; set; }
        public int? ArtistId { get; set; }
        public int? GenreId { get; set; }

        public IEnumerable<SelectListItem> Artists { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Genres { get; set; } = new List<SelectListItem>();
        public IEnumerable<Vinyl> Vinyls { get; set; } = new List<Vinyl>();
    }
}
