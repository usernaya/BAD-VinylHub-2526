using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylHub.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<Vinyl> Vinyls { get; set; } = new List<Vinyl>();
    }
}
