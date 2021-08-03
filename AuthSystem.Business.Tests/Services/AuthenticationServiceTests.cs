using AuhtSystem.Business.Interfaces;
using AuhtSystem.Business.Services;
using AuhtSystem.Business.Token;
using AuthSystem.Repository.Interfaces;
using AuthSystem.Repository.Models;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace AuthSystem.Business.Tests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly User _user;

        public AuthenticationServiceTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _tokenConfiguration = new TokenConfiguration
            {
                Audience = "Teste",
                Issuer = "Teste",
                Key = "fedaf7d8863b48e197b9287d492b708e",
                TokenExpiresMinutes = 5
            };
            _authenticationService = new AuthenticationService(_tokenConfiguration, _userRepository);
            _user = new User { UserId = 1, UserName = "Teste", Email = "teste@teste.com.br", 
                Password = "00004300009400003900007b0000d400005b0000160000710000d900004b0000160000900000fd00002b0000a2000055" };
        }

        [Fact]
        public async Task LoginWithSuccessReturnsTrue()
        {
            _userRepository.GetUserByEmail(_user.Email).Returns(_user);
            var ret = await _authenticationService.Login(new Repository.DTO.LoginDTO { Email = _user.Email, Password = "Jo@oCarlosBaldi" });

            Assert.True(ret.IsAuthenticated);

        }

        [Fact]
        public async Task LoginWithErrorReturnsFalse()
        {
            User user = null;
            _userRepository.GetUserByEmail(_user.Email).Returns(user);
            var ret = await _authenticationService.Login(new Repository.DTO.LoginDTO { Email = _user.Email, Password = "Jo@oCarlosBaldi" });

            Assert.False(ret.IsAuthenticated);
        }
        [Fact]
        public async Task ValidatePassordWithSuccessReturnsTrue()
        {
            Assert.True(await _authenticationService.ValidatePassword("Jo@oCarlosBaldi"));
        }

        [Fact]
        public async Task ValidatePassordFailedReturnsFalse()
        {
            Assert.False(await _authenticationService.ValidatePassword(""));
        }

    }
}
