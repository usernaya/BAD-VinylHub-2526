using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Application.Interfaces.Repositories;

namespace VinylHub.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IVinylRepository Vinyls { get; }
        IArtistRepository Artists { get; }
        IGenreRepository Genres { get; }
        ILoanRepository Loans { get; }
        void SaveChanges();
    }
}
