using Net7Basic.Dtos;

namespace Net7Basic.Repositories.Abstract
{
    public interface IUserRepository
    {
        string GetToken(string username, List<string> roles);
        bool IsUniqueUser(string userName);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> Register(RegistrationRequestDto registerRequestDto);
    }
}