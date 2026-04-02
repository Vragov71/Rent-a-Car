using RentACar.Data.Models;

namespace RentACar.Services;

/// <summary>
/// Интерфейс за административно управление на потребители.
/// </summary>
public interface IUserService
{
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    Task<ApplicationUser?> GetUserByIdAsync(string id);
    Task UpdateUserAsync(ApplicationUser user);
    Task DeleteUserAsync(string id);
}
