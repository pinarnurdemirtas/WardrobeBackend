using System.Threading.Tasks;
using WardrobeBackend.Model;

namespace WardrobeBackend.Repositories
{
    public interface IUserRepository
    {
        Task<Users> GetUserByUsernameAsync(string username);
        Task<Users> GetUserByIdAsync(int id);
        Task<bool> AddUserAsync(Users user);
        Task<bool> RemoveUserAsync(int id);
        Task<bool> UpdateUserAsync(Users user);
        Task<bool> SaveChangesAsync();
        Task<List<Users>> GetAllUsersAsync();
    }
}