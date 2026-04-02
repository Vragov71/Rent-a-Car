using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;

namespace RentACar.Services;

/// <summary>
/// Имплементация на услугата за управление на заявки.
/// </summary>
public class ReservationService : IReservationService
{
    private readonly ApplicationDbContext _context;

    public ReservationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        return await _context.Reservations
            .Include(r => r.Car)
            .Include(r => r.User)
            .OrderByDescending(r => r.StartDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByUserAsync(string userId)
    {
        return await _context.Reservations
            .Include(r => r.Car)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.StartDate)
            .ToListAsync();
    }

    public async Task<Reservation?> GetReservationByIdAsync(int id)
    {
        return await _context.Reservations
            .Include(r => r.Car)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task CreateReservationAsync(Reservation reservation)
    {
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReservationAsync(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation != null)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Car>> GetAvailableCarsAsync(DateTime startDate, DateTime endDate)
    {
        // Намираме ID-та на заети коли за периода
        var occupiedCarIds = await _context.Reservations
            .Where(r => r.StartDate < endDate && r.EndDate > startDate)
            .Select(r => r.CarId)
            .Distinct()
            .ToListAsync();

        // Връщаме само свободните коли
        return await _context.Cars
            .Where(c => !occupiedCarIds.Contains(c.Id))
            .ToListAsync();
    }

    public async Task<bool> IsCarAvailableAsync(int carId, DateTime startDate, DateTime endDate, int? excludeReservationId = null)
    {
        var query = _context.Reservations
            .Where(r => r.CarId == carId
                     && r.StartDate < endDate
                     && r.EndDate > startDate);

        if (excludeReservationId.HasValue)
        {
            query = query.Where(r => r.Id != excludeReservationId.Value);
        }

        return !await query.AnyAsync();
    }
}
