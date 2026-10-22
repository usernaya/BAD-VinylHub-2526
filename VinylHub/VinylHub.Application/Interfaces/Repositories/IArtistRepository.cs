using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Domain.Entities;

namespace VinylHub.Application.Interfaces.Repositories
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> GetAll();
        Artist? GetById(int id);
        void Add(Artist artist);
        void Update(Artist artist);
        void Delete(Artist artist);
    }
}
