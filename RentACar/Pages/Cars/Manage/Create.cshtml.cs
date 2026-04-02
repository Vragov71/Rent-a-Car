using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentACar.Data.Models;
using RentACar.Services;

namespace RentACar.Pages.Cars.Manage;

public class CreateModel : PageModel
{
    private readonly ICarService _carService;

    public CreateModel(ICarService carService)
    {
        _carService = carService;
    }

    [BindProperty]
    public Car Car { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _carService.CreateCarAsync(Car);
        TempData["Success"] = "Автомобилът е добавен успешно.";
        return RedirectToPage("Index");
    }
}
