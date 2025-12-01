using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Galaxy.Keycloak.Blazor.Services.Interceptor
{
    public class AuthInterceptor : AuthorizationMessageHandler
    {
        public AuthInterceptor(IAccessTokenProvider provider, NavigationManager navigation) : base(provider, navigation)
        {
            ConfigureHandler(
                authorizedUrls: ["https://localhost:7079"],
                scopes: ["openid", "profile"]
            );
        }
    }
}
