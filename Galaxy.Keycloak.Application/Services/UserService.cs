using Galaxy.Keycloak.Application.Dto;
using Galaxy.Keycloak.Application.Interfaces;
using Galaxy.Keycloak.Application.Models;
using Galaxy.Keycloak.AuthApi.Dto;

namespace Galaxy.Keycloak.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IKeycloakUserService _keycloakUserService;

        public UserService(IKeycloakUserService keycloakUserService)
        {
            _keycloakUserService = keycloakUserService;
        }
    
        public async Task<BaseResponse<List<UserResponse>>> GetAllUsersAsync()
        {
            try
            {
                var keycloakUsers = await _keycloakUserService.GetAllUsersAsync();

                var users = keycloakUsers.Select(u => new UserResponse
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(u.CreatedTimestamp).DateTime,
                }).ToList();
                return BaseResponse<List<UserResponse>>.Success(users);
            }
            catch (UnauthorizedAccessException)
            {
                return BaseResponse<List<UserResponse>>.Failure("No tienes permisos para ver los usuarios.", "FORBIDDEN");
            }
            catch (Exception ex)
            {
                return BaseResponse<List<UserResponse>>.Failure($"Error al obtener usuarios: {ex.Message}", "ERROR");
            }
        }
        public async Task<BaseResponse<bool>> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            try
            {
                var keycloakUser = new KeycloakUser
                {
                    Username = createUserRequest.Username,
                    Email = createUserRequest.Email,
                    Password = createUserRequest.Password
                };
                var result = await _keycloakUserService.CreateUsersAsync(keycloakUser);
                return BaseResponse<bool>.Success(result);
            }
            catch (UnauthorizedAccessException)
            {
                return BaseResponse<bool>.Failure("No tienes permisos para crear usuarios.", "FORBIDDEN");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.Failure($"Error al crear usuario: {ex.Message}", "ERROR");
            }
        }

    }
}
