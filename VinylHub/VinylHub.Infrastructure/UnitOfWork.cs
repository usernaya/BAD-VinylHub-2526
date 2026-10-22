using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Application.Interfaces;
using VinylHub.Application.Interfaces.Repositories;
using VinylHub.Infrastructure.Data;
using VinylHub.Infrastructure.Repositories;

namespace VinylHub.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VinylHubDbContext _context;
        
        public IVinylRepository Vinyls { get; }
        public IArtistRepository Artists { get; }
        public IGenreRepository Genres { get; }
        public ILoanRepository Loans { get; }

        public UnitOfWork(VinylHubDbContext context)
        {
            _context = context;
            Vinyls = new VinylRepository(context);
            Artists = new ArtistRepository(context);
            Genres = new GenreRepository(context);
            Loans = new LoanRepository(context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
