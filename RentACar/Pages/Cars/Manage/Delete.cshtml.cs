using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentACar.Data.Models;
using RentACar.Services;

namespace RentACar.Pages.Cars.Manage;

public class DeleteModel : PageModel
{
    private readonly ICarService _carService;

    public DeleteModel(ICarService carService)
    {
        _carService = carService;
    }

    [BindProperty]
    public Car Car { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var car = await _carService.GetCarByIdAsync(id);
        if (car == null)
        {
            return NotFound();
        }

        Car = car;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _carService.DeleteCarAsync(Car.Id);
        TempData["Success"] = "Автомобилът е изтрит успешно.";
        return RedirectToPage("Index");
    }
}
