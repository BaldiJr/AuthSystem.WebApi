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
    public class UserServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserService _userService;
        private readonly User _user;

        public UserServiceTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _authenticationService = new AuthenticationService(new TokenConfiguration(), _userRepository);

            _userService = new UserService(_userRepository, _authenticationService);
            _user = new User { UserId = 1, UserName = "Teste", Email = "teste@teste.com.br", Password = "_vH~@cN@_yI_-hN" };
        }

        [Fact]
        public async Task CreateUserWithSucessReturnsTrue()
        {
            User user = null;
            _userRepository.GetUserByEmail(_user.Email).Returns(user);
            _userRepository.CreateUser(_user).Returns(1);

            var sucess = await _userService.CreateUser(_user);
            Assert.True(sucess > 0);
        }

        [Fact]
        public async Task CreateUserWithExistsEmailReturnFalse()
        {
            _userRepository.GetUserByEmail(_user.Email).Returns(_user);
            _userRepository.CreateUser(_user).Returns(0);
            var sucess = await _userService.CreateUser(_user);

            Assert.True(sucess.Equals(0));
        }

        [Fact]
        public async Task CreateUserWithInvalidPasswordReturnsFalse()
        {
            var user = new User { UserId = 1, UserName = "Teste", Email = "teste@teste.com.br", Password = "123" };
            User existsUser = null;

            _userRepository.GetUserByEmail(_user.Email).Returns(existsUser);
            _userRepository.CreateUser(_user).Returns(1);
            var sucess = await _userService.CreateUser(user);

            Assert.True(sucess.Equals(0));
        }

        [Fact]
        public async Task UpdateUserWithSuccessReturnsTrue()
        {
            _userRepository.UpdateUser(_user).Returns(true);
            var sucess = await _userService.UpdateUser(_user);

            Assert.True(sucess);
        }

        [Fact]
        public async Task UpdateUserWithNullUserReturnsFalse()
        {
            User user = null;
            _userRepository.UpdateUser(_user).Returns(true);
            var sucess = await _userService.UpdateUser(user);

            Assert.False(sucess);
        }

        [Fact]
        public async Task DeleteUserWithSuccessReturnsTrue()
        {
            _userRepository.DeleteUser(1).Returns(true);
            var sucess = await _userService.DeleteUser(1);

            Assert.True(sucess);
        }

        [Fact]
        public async Task DeleteUserWithNullUserReturnsFalse()
        {
            User user = null;
            _userRepository.UpdateUser(user).Returns(false);
            Assert.False(await _userService.UpdateUser(user));
        }

        [Fact]
        public async Task GetUserByEmailSucessReturnsTrue()
        {
            _userRepository.GetUserByEmail(_user.Email).Returns(_user);

            var sucess = await _userService.GetUserByEmail(_user.Email);

            Assert.True(!(sucess is null));
        }

        [Fact]
        public async Task GetUserByNullEmailReturnsFalse()
        {
            User user = null;
            _userRepository.GetUserByEmail(null).Returns(user);

            var sucess = await _userService.GetUserByEmail(null);

            Assert.False(!(sucess is null));
        }

        [Fact]
        public async Task GeneratePasswordReturnsTrue()
        {
            var ret = await _userService.GeneratePassword();

            Assert.True(ret != null);
        }
    }
}
