using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Models;

/// <summary>
/// Потребител на системата – разширява стандартния IdentityUser.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>Собствено име</summary>
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>Фамилно име</summary>
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    /// <summary>ЕГН – уникален идентификатор</summary>
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string Egn { get; set; } = string.Empty;

    /// <summary>Резервации на потребителя</summary>
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
