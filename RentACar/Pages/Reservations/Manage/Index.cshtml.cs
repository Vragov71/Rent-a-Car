using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentACar.Data.Models;
using RentACar.Services;

namespace RentACar.Pages.Reservations.Manage;

public class IndexModel : PageModel
{
    private readonly IReservationService _reservationService;

    public IndexModel(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public IEnumerable<Reservation> Reservations { get; set; } = Enumerable.Empty<Reservation>();

    public async Task OnGetAsync()
    {
        Reservations = await _reservationService.GetAllReservationsAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _reservationService.DeleteReservationAsync(id);
        return RedirectToPage();
    }
}
