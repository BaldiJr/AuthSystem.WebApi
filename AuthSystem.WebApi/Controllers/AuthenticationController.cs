using AuhtSystem.Business.Interfaces;
using AuthSystem.Repository.DTO;
using AuthSystem.WebApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AuthSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost, Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                var auth = await _authenticationService.Login(login);

                if (auth.IsAuthenticated)
                {
                    return Ok(auth);
                }

                return Unauthorized("Usuário ou senha incorretos!");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost, Route("ValidateRulesPassword")]
        [Authorize]
        public async Task<IActionResult> ValidateRulesRulesPassword([FromBody] PwdValidation password)
        {
            try
            {
                return Ok(await _authenticationService.ValidatePassword(password.Password));
            } 
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }
    }
}
