using SHOP.DAL.Models;

namespace SHOP.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string userId);
        Task<bool> BlockUserAsync(string userId, int days);
        Task<bool> UnBlockUserAsync(string userId);
        Task<bool> IsBlockUserAsync(string userId);
        Task<bool> ChangeUserRoleAsync(string userId, string roleName);
    }
}
