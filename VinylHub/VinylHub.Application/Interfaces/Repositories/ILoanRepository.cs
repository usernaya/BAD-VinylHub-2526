using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Domain.Entities;

namespace VinylHub.Application.Interfaces.Repositories
{
    public interface ILoanRepository
    {
        IEnumerable<Loan> GetAll();
        Loan? GetById(int id);
        IEnumerable<Loan> GetByUser(string userId);
        Loan? GetByVinyl(int vinylId);
        void Add(Loan loan);
        void Update(Loan loan);
    }
}
