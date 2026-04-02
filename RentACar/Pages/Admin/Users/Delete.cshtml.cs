using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentACar.Data.Models;
using RentACar.Services;

namespace RentACar.Pages.Admin.Users;

public class DeleteModel : PageModel
{
    private readonly IUserService _userService;

    public DeleteModel(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty]
    public ApplicationUser AppUser { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        AppUser = user;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _userService.DeleteUserAsync(AppUser.Id);
        TempData["Success"] = "Потребителят е изтрит.";
        return RedirectToPage("Index");
    }
}
