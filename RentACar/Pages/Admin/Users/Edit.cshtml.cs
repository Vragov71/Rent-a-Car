using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentACar.Data.Models;
using RentACar.Services;

namespace RentACar.Pages.Admin.Users;

public class EditModel : PageModel
{
    private readonly IUserService _userService;

    public EditModel(IUserService userService)
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
        // Only validate the fields we care about; clear Identity-managed fields
        ModelState.Remove("AppUser.UserName");
        ModelState.Remove("AppUser.NormalizedUserName");
        ModelState.Remove("AppUser.NormalizedEmail");
        ModelState.Remove("AppUser.PasswordHash");
        ModelState.Remove("AppUser.SecurityStamp");
        ModelState.Remove("AppUser.ConcurrencyStamp");
        ModelState.Remove("AppUser.Egn");

        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _userService.UpdateUserAsync(AppUser);
        TempData["Success"] = "Потребителят е обновен.";
        return RedirectToPage("Index");
    }
}
