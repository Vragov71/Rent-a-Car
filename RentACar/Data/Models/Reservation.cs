using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Models;

/// <summary>
/// Заявка за наемане на автомобил.
/// </summary>
public class Reservation
{
    public int Id { get; set; }

    /// <summary>Начална дата на наема</summary>
    [Required]
    public DateTime StartDate { get; set; }

    /// <summary>Крайна дата на наема</summary>
    [Required]
    public DateTime EndDate { get; set; }

    /// <summary>Избран автомобил</summary>
    [Required]
    public int CarId { get; set; }
    public Car Car { get; set; } = null!;

    /// <summary>Потребител, наемащ автомобила</summary>
    [Required]
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}
