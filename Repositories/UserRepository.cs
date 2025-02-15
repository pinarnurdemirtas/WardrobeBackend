using Microsoft.EntityFrameworkCore;
using WardrobeBackend.Model;
using WardrobeBackend.Data;

namespace WardrobeBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WardrobeDbContext _context;

        public UserRepository(WardrobeDbContext context)
        {
            _context = context;
        }

        public async Task<Users> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<Users> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> AddUserAsync(Users user)
        {
            await _context.Users.AddAsync(user);
            return await SaveChangesAsync();
        }

        public async Task<bool> RemoveUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateUserAsync(Users user)
        {
            _context.Users.Update(user);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}