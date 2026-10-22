using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Application.Interfaces.Repositories;
using VinylHub.Domain.Entities;
using VinylHub.Infrastructure.Data;

namespace VinylHub.Infrastructure.Repositories
{
    public class VinylRepository : IVinylRepository
    {
        private readonly VinylHubDbContext _context;

        public VinylRepository(VinylHubDbContext context)
        {
            _context = context;
        }

        public void Add(Vinyl vinyl)
        {
            _context.Vinyls.Add(vinyl);
        }

        public void Delete(Vinyl vinyl)
        {
            _context.Vinyls.Remove(vinyl);
        }
        

        public IEnumerable<Vinyl> GetAll()
        {
            return _context.Vinyls
                .Include(x => x.Artist)
                .Include(x => x.Genre)
                .ToList();
        }

        public IEnumerable<Vinyl> GetByArtist(int artistId)
        {
            return _context.Vinyls
                .Include(x => x.Artist)
                .Include(x => x.Genre)
                .Where(x => x.Artist.Id == artistId)
                .ToList();
        }

        public IEnumerable<Vinyl> GetByGenre(int genreId)
        {
            return _context.Vinyls
                .Include(x => x.Artist)
                .Include(x => x.Genre)
                .Where(x => x.Genre.Id == genreId)
                .ToList();
        }

        public Vinyl? GetById(int id)
        {
            return _context.Vinyls
                .Include(x => x.Artist)
                .Include(x => x.Genre)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Vinyl vinyl)
        {
            _context.Vinyls.Update(vinyl);
        }
    }
}
