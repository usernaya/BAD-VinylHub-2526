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
    public class VinylService : IVinylService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VinylService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Vinyl vinyl)
        {
            _unitOfWork.Vinyls.Add(vinyl);
            _unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            Vinyl? vinyl = _unitOfWork.Vinyls.GetById(id);
            if(vinyl != null)
            {
                _unitOfWork.Vinyls.Delete(vinyl);
                _unitOfWork.SaveChanges();
            }
        }

        public IEnumerable<Vinyl> GetAll()
        {
            return _unitOfWork.Vinyls.GetAll();
        }

        public IEnumerable<Artist> GetAllArtists()
        {
            return _unitOfWork.Artists.GetAll();
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return _unitOfWork.Genres.GetAll();
        }

        public IEnumerable<Vinyl> GetByArtist(int artistId)
        {
            return _unitOfWork.Vinyls.GetByArtist(artistId);
        }

        public IEnumerable<Vinyl> GetByGenre(int genreId)
        {
            return _unitOfWork.Vinyls.GetByGenre(genreId);
        }

        public Vinyl? GetById(int id)
        {
            return _unitOfWork.Vinyls.GetById(id);
        }

        public void Update(Vinyl vinyl)
        {
            _unitOfWork.Vinyls.Update(vinyl);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<Vinyl> GetFiltered(string? searchTerm, int? artistId, int? genreId)
        {
            IEnumerable<Vinyl> vinyls = _unitOfWork.Vinyls.GetAll();

            if (!string.IsNullOrEmpty(searchTerm))
                vinyls = vinyls.Where(
                    v => v.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                    || v.Artist.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            if (artistId.HasValue)
                vinyls = vinyls.Where(v => v.Artist.Id == artistId);

            if (genreId.HasValue)
                vinyls = vinyls.Where(v => v.Genre.Id == genreId);

            return vinyls;
        }

    }
}
