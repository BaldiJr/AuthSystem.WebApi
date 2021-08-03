using AuthSystem.Repository.Models;
using System.Threading.Tasks;

namespace AuthSystem.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
    }
}
