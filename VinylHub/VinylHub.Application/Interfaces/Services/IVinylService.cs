using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Domain.Entities;

namespace VinylHub.Application.Interfaces.Services
{
    public interface IVinylService
    {
        IEnumerable<Vinyl> GetAll();
        Vinyl? GetById(int id);
        IEnumerable<Vinyl> GetByArtist(int artistId);
        IEnumerable<Vinyl> GetByGenre(int genreId);
        IEnumerable<Vinyl> GetFiltered(string? searchTerm, int? artistId, int? genreId);
        IEnumerable<Artist> GetAllArtists();
        IEnumerable<Genre> GetAllGenres();
        void Add(Vinyl vinyl);
        void Update(Vinyl vinyl);
        void Delete(int id);
    }
}
