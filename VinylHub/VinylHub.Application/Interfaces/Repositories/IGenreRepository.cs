using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Domain.Entities;

namespace VinylHub.Application.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAll();
        Genre? GetById(int id);
        void Add(Genre genre);
        void Update(Genre genre);
        void Delete(Genre genre);
    }
}
