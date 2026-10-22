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
    public class LoanRepository : ILoanRepository
    {
        private readonly VinylHubDbContext _context;

        public LoanRepository(VinylHubDbContext context)
        {
            _context = context;
        }

        public void Add(Loan loan)
        {
            _context.Loans.Add(loan);
        }

        public IEnumerable<Loan> GetAll()
        {
            return _context.Loans
                .Include(x => x.Vinyl)
                .ThenInclude(x => x.Artist)
                .ToList();
        }

        public Loan? GetById(int id)
        {
            return _context.Loans
                .Include(x => x.Vinyl)
                .ThenInclude(x => x.Artist)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Loan> GetByUser(string userId)
        {
            return _context.Loans
                .Include(x => x.Vinyl)
                .ThenInclude(x => x.Artist)
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public Loan? GetByVinyl(int vinylId)
        {
            return _context.Loans
                .Include(x => x.Vinyl)
                .ThenInclude(x => x.Artist)
                .FirstOrDefault(x => x.Vinyl.Id == vinylId && x.ReturnDate == null);
        }

        public void Update(Loan loan)
        {
            _context.Loans.Update(loan);
        }
        
    }
}
