using AuhtSystem.Business.Token;
using AuthSystem.Repository.DTO;
using System.Threading.Tasks;

namespace AuhtSystem.Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthInformation> Login(LoginDTO login);
        Task<bool> ValidatePassword(string password);
    }
}
