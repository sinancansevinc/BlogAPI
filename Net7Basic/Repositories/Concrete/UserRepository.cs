using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Net7Basic.Data;
using Net7Basic.Dtos;
using Net7Basic.Models;
using Net7Basic.Repositories.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Net7Basic.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly string secretKey;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserRepository(IConfiguration _configuration, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.secretKey = _configuration.GetSection("JWTParameters:SecretKey").Value;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public bool IsUserExist(string userName)
        {
            var user = _context.Users.FirstOrDefault(p => p.UserName.ToLower() == userName.ToLower());
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (user == null || !isValid)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    UserDto = null
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = GetToken(user.UserName, roles.ToList());

            return new LoginResponseDto()
            {
                Token = token,
                UserDto = new UserDto()
                {
                    Id = user.Id,
                    FullName = user.FirstName + " " + user.LastName,
                    UserName = user.UserName
                }
            };

        }

        public string GetToken(string username, List<string> roles)
        {
            

            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> Register(RegistrationRequestDto registerRequestDto)
        {
            var isExist = IsUserExist(registerRequestDto.UserName);
            if (isExist)
            {
                throw new Exception();
            }

            var applicationUser = new ApplicationUser
            {
                UserName = registerRequestDto.UserName,
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                Email = registerRequestDto.UserName,
                NormalizedEmail = registerRequestDto.UserName.ToUpper()
            };

            try
            {
                var registerResult = await _userManager.CreateAsync(applicationUser, registerRequestDto.Password);
                if (registerResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(applicationUser, "human");

                    return true;
                }

                return false;

            }
            catch (Exception)
            {

                throw new Exception("Cannot register");
            }
        }

        public async Task<ResponseDto<IdentityRole>> AddRole(string roleName)
        {

            var isRoleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!isRoleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
                return new ResponseDto<IdentityRole>
                {
                    IsSuccess = true,
                    Data = await _roleManager.FindByNameAsync(roleName),
                    Errors = null

                };
            }

            return new ResponseDto<IdentityRole>
            {
                IsSuccess = false,
                Data = null,
                Errors = new List<string>() { "Role already exist" }
            };
        }

        public async Task<ResponseDto<ApplicationUser>> AddRoleToUser(string userName, string roleName)
        {

            var isUserExist = IsUserExist(userName);
            var isRoleExist = await _roleManager.FindByNameAsync(roleName);

            if (!isUserExist || isRoleExist == null)
            {
                return new ResponseDto<ApplicationUser>
                {
                    IsSuccess = false,
                    Data = null,
                    Errors = new List<string>
                    {
                        "User or role is not exist"
                    }
                };
            }

            var applicationUser = await _context.Users.FirstOrDefaultAsync(p => p.UserName.ToLower() == userName.ToLower());

            var rolesFromUser = await _userManager.GetRolesAsync(applicationUser);
            await _userManager.RemoveFromRolesAsync(applicationUser,rolesFromUser);

            await _userManager.AddToRoleAsync(applicationUser, roleName);

            return new ResponseDto<ApplicationUser>
            {
                IsSuccess = true,
                Data = applicationUser,
                Errors = null
            };

        }

    }
}
