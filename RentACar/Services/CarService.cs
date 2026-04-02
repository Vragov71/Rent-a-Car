using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;

namespace RentACar.Services;

public class CarService : ICarService
{
    private readonly ApplicationDbContext _context;

    public CarService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Car>> GetAllCarsAsync()
    {
        return await _context.Cars.ToListAsync();
    }

    public async Task<Car?> GetCarByIdAsync(int id)
    {
        return await _context.Cars.FindAsync(id);
    }

    public async Task CreateCarAsync(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCarAsync(Car car)
    {
        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCarAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car != null)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> CarExistsAsync(int id)
    {
        return await _context.Cars.AnyAsync(c => c.Id == id);
    }
}
