using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylHub.Domain.Entities
{
    public class Vinyl : BaseEntity
    {
        public string Title { get; set; } = default!;
        public int ReleaseYear { get; set; }
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? Label { get; set; }
        public bool IsAvailable { get; set; } = true;

        public Artist Artist { get; set; } = default!;
        public Genre Genre { get; set; } = default!;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();


    }
}
