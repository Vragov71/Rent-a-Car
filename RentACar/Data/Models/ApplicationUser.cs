using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string Egn { get; set; } = string.Empty;

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
