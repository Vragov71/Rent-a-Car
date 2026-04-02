using Microsoft.AspNetCore.Mvc.RazorPages;
using RentACar.Data.Models;
using RentACar.Services;

namespace RentACar.Pages.Cars;

public class IndexModel : PageModel
{
    private readonly ICarService _carService;

    public IndexModel(ICarService carService)
    {
        _carService = carService;
    }

    public IEnumerable<Car> Cars { get; set; } = Enumerable.Empty<Car>();

    public async Task OnGetAsync()
    {
        Cars = await _carService.GetAllCarsAsync();
    }
}
