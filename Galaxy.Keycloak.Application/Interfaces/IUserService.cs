using Galaxy.Keycloak.Application.Dto;
using Galaxy.Keycloak.AuthApi.Dto;

namespace Galaxy.Keycloak.Application.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<List<UserResponse>>> GetAllUsersAsync();
        Task<BaseResponse<bool>> CreateUserAsync(CreateUserRequest createUserRequest);
    }
}
