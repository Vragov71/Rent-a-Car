using RentACar.Data.Models;

namespace RentACar.Services;

/// <summary>
/// Интерфейс за управление на заявки за наем.
/// </summary>
public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetAllReservationsAsync();
    Task<IEnumerable<Reservation>> GetReservationsByUserAsync(string userId);
    Task<Reservation?> GetReservationByIdAsync(int id);
    Task CreateReservationAsync(Reservation reservation);
    Task DeleteReservationAsync(int id);

    /// <summary>
    /// Връща списък с автомобили, свободни за дадения период.
    /// </summary>
    Task<IEnumerable<Car>> GetAvailableCarsAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Проверява дали даден автомобил е свободен за периода.
    /// </summary>
    Task<bool> IsCarAvailableAsync(int carId, DateTime startDate, DateTime endDate, int? excludeReservationId = null);
}
