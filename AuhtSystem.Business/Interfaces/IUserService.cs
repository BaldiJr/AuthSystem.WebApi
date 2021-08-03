using AuthSystem.Repository.Models;
using System.Threading.Tasks;

namespace AuhtSystem.Business.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<string> GeneratePassword();
        Task<User> GetUserByEmail(string email);
    }
}
