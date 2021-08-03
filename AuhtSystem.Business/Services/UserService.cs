using AuhtSystem.Business.Interfaces;
using AuhtSystem.Business.Util;
using AuthSystem.Repository.Interfaces;
using AuthSystem.Repository.Models;
using System.Threading.Tasks;

namespace AuhtSystem.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public UserService(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public async Task<string> GeneratePassword()
        {
            var password = PasswordTool.GeneratePassword();

            if (await _authenticationService.ValidatePassword(password))
            {
                return password;
            }

            return null;
        }

        public async Task<int> CreateUser(User user)
        {
            var existUser = await GetUserByEmail(user.Email);

            if (existUser is null)
            {
                if (PasswordTool.ValidatePassword(user.Password))
                {
                    user.Password = Encryptor.EncryptPass(user.Password);

                    var idUser = await _userRepository.CreateUser(user);

                    return idUser;
                }
            }

            return 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<bool> UpdateUser(User user)
        {
            if (PasswordTool.ValidatePassword(user?.Password))
            {
                user.Password = Encryptor.EncryptPass(user.Password);

                return await _userRepository.UpdateUser(user);
            }

            return false;
        }
    }
}
