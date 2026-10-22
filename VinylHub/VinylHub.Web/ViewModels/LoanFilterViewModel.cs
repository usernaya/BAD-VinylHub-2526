using Microsoft.AspNetCore.Mvc.Rendering;
using VinylHub.Domain.Entities;
using VinylHub.Domain.Enums;

namespace VinylHub.Web.ViewModels
{
    public class LoanFilterViewModel
    {
        public IEnumerable<Loan> Loans { get; set; } = new List<Loan>();
        public LoanStatus? SelectedStatus { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; } = new List<SelectListItem>();
    }
}
