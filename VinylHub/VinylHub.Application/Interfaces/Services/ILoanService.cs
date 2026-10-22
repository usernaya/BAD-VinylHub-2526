using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Domain.Entities;

namespace VinylHub.Application.Interfaces.Services
{
    public interface ILoanService
    {
        IEnumerable<Loan> GetAll();
        Loan? GetById(int id);
        IEnumerable<Loan> GetByUser(string userId);
        bool IsVinylAvailable(int vinylId);
        void Borrow(int vinylId, string userId, string borrowerName, int loanDays = 14);
        void Return(int loanId);
        void CheckOverdueLoans();

    }
}
