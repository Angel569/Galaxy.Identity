using Galaxy.Keycloak.Application.Models;

namespace Galaxy.Keycloak.Application.Services
{
    public interface IKeycloakUserService
    {
        Task<List<KeycloakUser>> GetAllUsersAsync();
        Task<bool> CreateUsersAsync(KeycloakUser keycloakUser);
    }
}
