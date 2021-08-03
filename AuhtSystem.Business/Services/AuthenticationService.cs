using AuhtSystem.Business.Interfaces;
using AuhtSystem.Business.Token;
using AuhtSystem.Business.Util;
using AuthSystem.Repository.DTO;
using AuthSystem.Repository.Interfaces;
using AuthSystem.Repository.Models;
using System.Threading.Tasks;

namespace AuhtSystem.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(TokenConfiguration tokenConfiguration, IUserRepository userRepository)
        {
            _tokenConfiguration = tokenConfiguration;
            _userRepository = userRepository;
        }

        public async Task<AuthInformation> Login(LoginDTO login)
        {
            var user = await _userRepository.GetUserByEmail(login.Email);
            bool verification = Encryptor.EncryptPass(login?.Password) == user?.Password;

            if (user == null || !verification)
                return new AuthInformation { IsAuthenticated = false };

            var token = GetToken(user);

            return new AuthInformation { IsAuthenticated = true, Token = token.Token, TokenExpiresIn = token.ExpiresIn, User = user.UserName };
        }

        public Task<bool> ValidatePassword(string password)
        {
            var reponse = PasswordTool.ValidatePassword(password);
            return Task.FromResult(reponse);
        }

        private TokenInformation GetToken(User user)
        {
            var token = TokenService.GenerateToken(user, _tokenConfiguration);

            return token;
        }
    }
}
