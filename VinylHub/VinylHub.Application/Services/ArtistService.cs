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
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArtistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Artist artist)
        {
            _unitOfWork.Artists.Add(artist);
            _unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            Artist? artist = _unitOfWork.Artists.GetById(id);
            if (artist != null)
            {
                _unitOfWork.Artists.Delete(artist);
                _unitOfWork.SaveChanges();
            }
        }

        public IEnumerable<Artist> GetAll()
        {
            return _unitOfWork.Artists.GetAll();
        }

        public Artist? GetById(int id)
        {
            return _unitOfWork.Artists.GetById(id);
        }

        public void Update(Artist artist)
        {
            _unitOfWork.Artists.Update(artist);
            _unitOfWork.SaveChanges();
        }
    }
}
