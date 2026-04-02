using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentACar.Data.Models;
using RentACar.Services;

namespace RentACar.Pages.Reservations;

public class MyReservationsModel : PageModel
{
    private readonly IReservationService _reservationService;

    public MyReservationsModel(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public IEnumerable<Reservation> Reservations { get; set; } = Enumerable.Empty<Reservation>();

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        Reservations = await _reservationService.GetReservationsByUserAsync(userId);
    }

    public async Task<IActionResult> OnPostCancelAsync(int id)
    {
        await _reservationService.DeleteReservationAsync(id);
        TempData["Success"] = "Заявката е отменена.";
        return RedirectToPage();
    }
}
