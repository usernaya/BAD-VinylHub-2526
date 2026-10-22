using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylHub.Domain.Enums;

namespace VinylHub.Domain.Entities
{
    public class Loan : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.Active;
        
        public string UserId { get; set; } = default!;
        public string BorrowerName { get; set; } = default!;

        public Vinyl Vinyl { get; set; } = null!;
    }
}
