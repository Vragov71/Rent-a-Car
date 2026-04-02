using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentACar.Data.Models;

namespace RentACar.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "Полето е задължително.")]
        [MaxLength(50)]
        [Display(Name = "Собствено име")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително.")]
        [MaxLength(50)]
        [Display(Name = "Фамилно име")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл адрес.")]
        [Display(Name = "Имейл адрес")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "ЕГН трябва да бъде точно 10 цифри.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "ЕГН трябва да съдържа само цифри.")]
        [Display(Name = "ЕГН")]
        public string Egn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително.")]
        [Phone(ErrorMessage = "Невалиден телефонен номер.")]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Паролата трябва да е поне {2} символа.")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
        [Display(Name = "Потвърди паролата")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = new ApplicationUser
        {
            UserName = Input.UserName,
            Email = Input.Email,
            FirstName = Input.FirstName,
            LastName = Input.LastName,
            Egn = Input.Egn,
            PhoneNumber = Input.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, Input.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToPage("/Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }
}
