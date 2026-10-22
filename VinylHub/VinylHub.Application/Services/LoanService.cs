using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Application.Interfaces;
using VinylHub.Application.Interfaces.Services;
using VinylHub.Domain.Entities;
using VinylHub.Domain.Enums;

namespace VinylHub.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void Borrow(int vinylId, string userId, string borrowerName, int loanDays = 14)
        {
            if (!IsVinylAvailable(vinylId))
                throw new Exception("Deze vinyl is momenteel niet beschikbaar.");

            Vinyl? vinyl = _unitOfWork.Vinyls.GetById(vinylId);
            if (vinyl == null)
                throw new Exception("Vinyl niet gevonden.");

            vinyl.IsAvailable = false;
            _unitOfWork.Vinyls.Update(vinyl);

            Loan loan = new Loan
            {
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(loanDays),
                UserId = userId,
                BorrowerName = borrowerName,
                Vinyl = vinyl
            };

            _unitOfWork.Loans.Add(loan);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<Loan> GetAll()
        {
            return _unitOfWork.Loans.GetAll();
        }

        public Loan? GetById(int id)
        {
            return _unitOfWork.Loans.GetById(id);
        }

        public IEnumerable<Loan> GetByUser(string userId)
        {
            return _unitOfWork.Loans.GetByUser(userId);
        }

        public bool IsVinylAvailable(int vinylId)
        {
            Loan? activeLoan = _unitOfWork.Loans.GetByVinyl(vinylId);
            return activeLoan == null;
        }

        public void Return(int loanId)
        {
            Loan? loan = _unitOfWork.Loans.GetById(loanId);

            if (loan == null)
                throw new Exception("Lening niet gevonden.");
            
            loan.ReturnDate = DateTime.Now;
            loan.Status = LoanStatus.Returned;

            Vinyl? vinyl = _unitOfWork.Vinyls.GetById(loan.Vinyl.Id);
            if (vinyl != null)
            {
                vinyl.IsAvailable = true;
                _unitOfWork.Vinyls.Update(vinyl);
            }

            _unitOfWork.Loans.Update(loan);
            _unitOfWork.SaveChanges();
        }

        public void CheckOverdueLoans()
        {
            var loans = _unitOfWork.Loans.GetAll();

            foreach (Loan loan in loans)
            {
                if (loan.ReturnDate == null && loan.DueDate < DateTime.Now && loan.Status != LoanStatus.Overdue)
                {
                    loan.Status = LoanStatus.Overdue;
                    _unitOfWork.Loans.Update(loan);
                }
            }

            _unitOfWork.SaveChanges();
        }

        
    }
}
