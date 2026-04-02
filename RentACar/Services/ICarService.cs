using RentACar.Data.Models;

namespace RentACar.Services;

/// <summary>
/// Интерфейс за управление на автомобили.
/// </summary>
public interface ICarService
{
    Task<IEnumerable<Car>> GetAllCarsAsync();
    Task<Car?> GetCarByIdAsync(int id);
    Task CreateCarAsync(Car car);
    Task UpdateCarAsync(Car car);
    Task DeleteCarAsync(int id);
    Task<bool> CarExistsAsync(int id);
}
