using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net7Basic.Dtos;
using Net7Basic.Repositories.Abstract;

namespace Net7Basic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            var result = await _userRepository.Register(registrationRequestDto);
            if (result)
            {
                return Ok(result);

            }

            return BadRequest("Cannot register !");

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var result = await _userRepository.Login(loginRequestDto);
            if (result.UserDto != null && result.Token != null)
            {
                return Ok(result);

            }

            return BadRequest("Cannot login !");

        }

        [HttpPost("Role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddRole(string role)
        {
            var result = await _userRepository.AddRole(role);
            return Ok(result);

        }

        [HttpPost("AddRoleToPerson")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddRoleToUser(string username,string role)
        {
            var result = await _userRepository.AddRoleToUser(username, role);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
