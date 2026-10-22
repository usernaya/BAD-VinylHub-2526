using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Application.Interfaces;
using VinylHub.Application.Interfaces.Services;
using VinylHub.Domain.Entities;

namespace VinylHub.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Genre genre)
        {
            _unitOfWork.Genres.Add(genre);
            _unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            Genre? genre = _unitOfWork.Genres.GetById(id);
            if (genre != null)
            {
                _unitOfWork.Genres.Delete(genre);
                _unitOfWork.SaveChanges();
            }
        }

        public IEnumerable<Genre> GetAll()
        {
            return _unitOfWork.Genres.GetAll();
        }

        public Genre? GetById(int id)
        {
            return _unitOfWork.Genres.GetById(id);
        }

        public void Update(Genre genre)
        {
            _unitOfWork.Genres.Update(genre);
            _unitOfWork.SaveChanges();
        }
    }
}
