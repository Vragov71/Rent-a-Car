using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentACar.Data.Models;
using RentACar.Services;

namespace RentACar.Pages.Cars.Manage;

public class EditModel : PageModel
{
    private readonly ICarService _carService;

    public EditModel(ICarService carService)
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
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _carService.UpdateCarAsync(Car);
        TempData["Success"] = "Автомобилът е обновен успешно.";
        return RedirectToPage("Index");
    }
}
