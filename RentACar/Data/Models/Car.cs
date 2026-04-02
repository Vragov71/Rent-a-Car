using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Data.Models;

/// <summary>
/// Автомобил в автопарка на компанията.
/// </summary>
public class Car
{
    public int Id { get; set; }

    /// <summary>Марка</summary>
    [Required]
    [MaxLength(50)]
    public string Make { get; set; } = string.Empty;

    /// <summary>Модел</summary>
    [Required]
    [MaxLength(50)]
    public string Model { get; set; } = string.Empty;

    /// <summary>Година на производство</summary>
    [Required]
    [Range(1900, 2100)]
    public int Year { get; set; }

    /// <summary>Брой пасажерски места</summary>
    [Required]
    [Range(1, 20)]
    public int Seats { get; set; }

    /// <summary>Кратко описание (по избор)</summary>
    [MaxLength(500)]
    public string? Description { get; set; }

    /// <summary>Цена за наем на ден (лв.)</summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0.01, 10000)]
    public decimal PricePerDay { get; set; }

    /// <summary>Резервации за този автомобил</summary>
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
