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

    }
}
