using RentACar.Data.Models;

namespace RentACar.Services;

public interface IUserService
{
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    Task<ApplicationUser?> GetUserByIdAsync(string id);
    Task UpdateUserAsync(ApplicationUser user);
    Task DeleteUserAsync(string id);
}
