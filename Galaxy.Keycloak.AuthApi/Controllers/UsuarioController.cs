using Galaxy.Keycloak.Application.Dto;
using Galaxy.Keycloak.Application.Interfaces;
using Galaxy.Keycloak.Application.Services;
using Galaxy.Keycloak.AuthApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Galaxy.Keycloak.AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase 
    {
        private readonly IUserService _userService;

        public UsuarioController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();   
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            var result = await _userService.CreateUserAsync(createUserRequest);
            return Ok(result);
        }
    }
}
