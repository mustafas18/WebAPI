using WebAPI.Core.DTO;

namespace Logic.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponceDTO> GetAuthorization(UserDTO LoginUser);
    }
}
