using RentACar.Data.Models;

namespace RentACar.Services;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetAllReservationsAsync();
    Task<IEnumerable<Reservation>> GetReservationsByUserAsync(string userId);
    Task<Reservation?> GetReservationByIdAsync(int id);
    Task CreateReservationAsync(Reservation reservation);
    Task DeleteReservationAsync(int id);
    Task<IEnumerable<Car>> GetAvailableCarsAsync(DateTime startDate, DateTime endDate);
    Task<bool> IsCarAvailableAsync(int carId, DateTime startDate, DateTime endDate, int? excludeReservationId = null);
}
