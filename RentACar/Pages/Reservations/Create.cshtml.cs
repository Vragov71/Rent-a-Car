using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentACar.Data.Models;
using RentACar.Services;

namespace RentACar.Pages.Reservations;

public class CreateModel : PageModel
{
    private readonly IReservationService _reservationService;

    public CreateModel(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [BindProperty]
    [Required(ErrorMessage = "Изберете начална дата.")]
    [Display(Name = "Начална дата")]
    public DateTime StartDate { get; set; } = DateTime.Today;

    [BindProperty]
    [Required(ErrorMessage = "Изберете крайна дата.")]
    [Display(Name = "Крайна дата")]
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);

    public IEnumerable<Car>? AvailableCars { get; set; }

    public void OnGet(int? carId)
    {
        // Ако е дошли от бутон "Резервирай" на конкретна кола
    }

    public async Task<IActionResult> OnPostSearchAsync()
    {
        if (EndDate <= StartDate)
        {
            ModelState.AddModelError(string.Empty, "Крайната дата трябва да е след началната.");
            return Page();
        }

        if (StartDate < DateTime.Today)
        {
            ModelState.AddModelError(string.Empty, "Началната дата не може да е в миналото.");
            return Page();
        }

        AvailableCars = await _reservationService.GetAvailableCarsAsync(StartDate, EndDate);
        return Page();
    }

    public async Task<IActionResult> OnPostReserveAsync(int carId, DateTime startDate, DateTime endDate)
    {
        // Проверяваме дали колата все още е свободна
        var isAvailable = await _reservationService.IsCarAvailableAsync(carId, startDate, endDate);
        if (!isAvailable)
        {
            TempData["Error"] = "Тази кола вече е резервирана за избрания период.";
            return RedirectToPage();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var reservation = new Reservation
        {
            CarId = carId,
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate
        };

        await _reservationService.CreateReservationAsync(reservation);
        TempData["Success"] = "Заявката е подадена успешно!";
        return RedirectToPage("/Reservations/MyReservations");
    }
}
