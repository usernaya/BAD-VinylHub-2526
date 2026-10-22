using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Domain.Entities;

namespace VinylHub.Application.Interfaces.Repositories
{
    public interface IVinylRepository
    {
        IEnumerable<Vinyl> GetAll();
        Vinyl? GetById(int id);
        IEnumerable<Vinyl> GetByArtist(int artistId);
        IEnumerable<Vinyl> GetByGenre(int genreId);
        void Add(Vinyl vinyl);
        void Update(Vinyl vinyl);
        void Delete(Vinyl vinyl);
    }
}
