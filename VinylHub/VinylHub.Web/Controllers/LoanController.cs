using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VinylHub.Application.Interfaces.Services;
using VinylHub.Domain.Entities;
using VinylHub.Domain.Enums;
using VinylHub.Infrastructure.Identity;
using VinylHub.Web.ViewModels;

namespace VinylHub.Web.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IVinylService _vinylService;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoanController(ILoanService loanService, IVinylService vinylService, UserManager<ApplicationUser> userManager)
        {
            _loanService = loanService;
            _vinylService = vinylService;
            _userManager = userManager;
        }

        // eigen lening bekijken
        public IActionResult Index()
        {
            _loanService.CheckOverdueLoans();
            string? userId = _userManager.GetUserId(User);
            var loans = _loanService.GetByUser(userId);
            return View(loans);
        }

        // vinyl lenen
        public IActionResult Create(int vinylId)
        {
            Vinyl? vinyl = _vinylService.GetById(vinylId);
            if (vinyl == null) return NotFound();

            if (!_loanService.IsVinylAvailable(vinylId))
            {
                TempData["Error"] = "Deze vinyl is momenteel niet beschikbaar.";
                return RedirectToAction("Details", "Vinyl", new { id = vinylId });
            }

            LoanCreateViewModel vm = new LoanCreateViewModel
            {
                VinylId = vinyl.Id,
                VinylTitle = vinyl.Title,
                ArtistName = vinyl.Artist.Name
            };

            return View(vm);
        }

        // vinyl lenen (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanCreateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string? userId = _userManager.GetUserId(User);
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            string borrowerName = user?.Email ?? "Onbekend";

            _loanService.Borrow(vm.VinylId, userId, borrowerName, vm.LoanDays);

            TempData["Success"] = "Vinyl succesvol geleend!";
            return RedirectToAction(nameof(Index));
        }

        // vinyl terugbrengen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Return(int loanId)
        {
            _loanService.Return(loanId);
            TempData["Success"] = "Vinyl succesvol teruggegeven!";
            return RedirectToAction(nameof(Index));
        }

        // alle leningen bekijken (admin)
        [Authorize(Policy = "CanManageVinyls")]
        public IActionResult All(LoanStatus? status)
        {
            _loanService.CheckOverdueLoans();
            var loans = _loanService.GetAll();

            if (status.HasValue)
            { 
                loans = loans.Where(x => x.Status == status);
            }

            LoanFilterViewModel vm = new LoanFilterViewModel
            {
                Loans = loans,
                SelectedStatus = status,
                Statuses = new List<SelectListItem>
                {
                    new SelectListItem("Actief", LoanStatus.Active.ToString()),
                    new SelectListItem("Teruggegeven", LoanStatus.Returned.ToString()),
                    new SelectListItem("Te laat", LoanStatus.Overdue.ToString()),
                }
            };

            return View(vm);
        }

    }
}
