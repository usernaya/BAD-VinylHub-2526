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
    public class ArtistRepository : IArtistRepository
    {
        private readonly VinylHubDbContext _context;
        public ArtistRepository(VinylHubDbContext context)
        {
            _context = context;
        }

        public void Add(Artist artist)
        {
            _context.Artists.Add(artist);
        }

        public void Delete(Artist artist)
        {
            _context.Artists.Remove(artist);
        }

        public IEnumerable<Artist> GetAll()
        {
            return _context.Artists.ToList();
        }

        public Artist? GetById(int id)
        {
            return _context.Artists
                .Include(a => a.Vinyls)
                .ThenInclude(v => v.Genre)
                .FirstOrDefault(a => a.Id == id);
        }

        public void Update(Artist artist)
        {
            _context.Artists.Update(artist);
        }
    }
}
