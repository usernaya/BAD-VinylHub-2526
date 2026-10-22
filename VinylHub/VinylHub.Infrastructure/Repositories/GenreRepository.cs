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
    public class GenreRepository : IGenreRepository
    {
        private readonly VinylHubDbContext _context;

        public GenreRepository(VinylHubDbContext context)
        {
            _context = context;
        }

        public void Add(Genre genre)
        {
            _context.Genres.Add(genre);
        }

        public void Delete(Genre genre)
        {
            _context.Genres.Remove(genre);
        }

        public IEnumerable<Genre> GetAll()
        {
            return _context.Genres.ToList();
        }

        public Genre? GetById(int id)
        {
            return _context.Genres
                .Include(g => g.Vinyls)
                .ThenInclude(v => v.Artist)
                .FirstOrDefault(g => g.Id == id);
        }

        public void Update(Genre genre)
        {
            _context.Genres.Update(genre);
        }
    }
}
