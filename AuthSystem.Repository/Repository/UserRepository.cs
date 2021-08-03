using AuthSystem.Repository.Interfaces;
using AuthSystem.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuthSystem.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly ApplicationContext _applicationContext;
        public UserRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<int> CreateUser(User user)
        {
            _applicationContext.User.Add(user);
            await _applicationContext.SaveChangesAsync();

            return user.UserId;

        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await GetUserById(id);
            EntityState entity = _applicationContext.Remove(user).State;
            await _applicationContext.SaveChangesAsync();

            return entity == EntityState.Deleted;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _applicationContext.User.FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<User> GetUserById(int id)
        {
            return await _applicationContext.User.FirstAsync(u => u.UserId == id);
        }
        public async Task<bool> UpdateUser(User user)
        {
            var result = await _applicationContext.User.SingleOrDefaultAsync(u => u.UserId == user.UserId);

            if (result != null)
            {
                try
                {
                    result.UserName = user.UserName;
                    result.Email = user.Email;
                    result.Password = user.Password;
                    await _applicationContext.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}
