using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VinylHub.Domain.Entities
{
    public class Artist : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Vinyl> Vinyls { get; set; } = new List<Vinyl>();

    }
}
