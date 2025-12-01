using Galaxy.Keycloak.Application.Models;
using Galaxy.Keycloak.Application.Services;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class KeycloakUserService : IKeycloakUserService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public KeycloakUserService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<KeycloakUser>> GetAllUsersAsync()
    {
        var accessToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

        accessToken = accessToken!.Substring("Bearer ".Length);

        var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:8080/admin/realms/galaxy_realm/users");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.Forbidden)
            throw new UnauthorizedAccessException("No tienes permisos para ver los usuarios.");

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Error al obtener usuarios: {response.StatusCode}");

        var json = await response.Content.ReadAsStringAsync();
        var users = JsonSerializer.Deserialize<List<KeycloakUser>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return users ?? new List<KeycloakUser>();
    }
    public async Task<bool> CreateUsersAsync(KeycloakUser keycloakUser)
    {
        var accessToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

        accessToken = accessToken!.Substring("Bearer ".Length);
        var userPayload = new
        {
            username = keycloakUser.Username,
            email = keycloakUser.Email,  
            enabled = true,
            emailVerified = true,
            credentials = string.IsNullOrEmpty(keycloakUser.Password) ? null : new[]
            {
                new
                {
                    type = "password",
                    value = keycloakUser.Password,
                    temporary = true 
                }
            }
        };

        var content = new StringContent(
            JsonSerializer.Serialize(userPayload),
            Encoding.UTF8,
            "application/json"
        );
        var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:8080/admin/realms/galaxy_realm/users");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Content = content;

        var response = await _httpClient.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.Forbidden)
            throw new UnauthorizedAccessException("No tienes permisos para crear usuarios.");

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Error al crear usuarios: {response.StatusCode}");

        return true;
    }
}