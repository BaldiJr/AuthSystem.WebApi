using AuhtSystem.Business.Interfaces;
using AuthSystem.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AuthSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService UserService)
        {
            _userService = UserService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                var createUser = await _userService.CreateUser(user);

                if (createUser.Equals(0))
                {
                    return UnprocessableEntity("E-mail já cadastrado!");
                }

                return Ok(createUser);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] User User)
        {
            try
            {
                if (await _userService.UpdateUser(User))
                {
                    return Ok();
                }
                return BadRequest("Erro ao alterar usuário");

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                return Ok(await _userService.DeleteUser(id));

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("GeneratePassword")]
        [Authorize]
        public async Task<IActionResult> GeneratePassword()
        {
            try
            {
                var password = await _userService.GeneratePassword();
                return Ok(password);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
