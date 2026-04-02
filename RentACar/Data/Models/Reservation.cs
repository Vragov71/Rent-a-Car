using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Models;

public class Reservation
{
    public int Id { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int CarId { get; set; }
    public Car Car { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}
