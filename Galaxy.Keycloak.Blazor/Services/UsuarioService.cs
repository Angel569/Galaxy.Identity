using Galaxy.Keycloak.Blazor.Dto;
using System.Net.Http.Json;

namespace Galaxy.Keycloak.Blazor.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponse<List<UserResponse>>> GetUsuariosAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<BaseResponse<List<UserResponse>>>("api/usuario") ?? new();
            return response;
        }
        public async Task<BaseResponse<bool>> CrearUsuarioAsync(CreateUserRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/usuario", request);
            var result = await response.Content.ReadFromJsonAsync<BaseResponse<bool>>() ?? new();
            return result;
        }
    }
}
