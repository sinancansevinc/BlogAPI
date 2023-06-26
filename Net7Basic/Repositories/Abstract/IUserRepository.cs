using Microsoft.AspNetCore.Identity;
using Net7Basic.Dtos;
using Net7Basic.Models;

namespace Net7Basic.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<ResponseDto<IdentityRole>> AddRole(string roleName);
        Task<ResponseDto<ApplicationUser>> AddRoleToUser(string userName, string roleName);
        string GetToken(string username, List<string> roles);
        bool IsUserExist(string userName);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> Register(RegistrationRequestDto registerRequestDto);
    }
}