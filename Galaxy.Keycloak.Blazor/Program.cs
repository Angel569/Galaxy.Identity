using Galaxy.Keycloak.Blazor;
using Galaxy.Keycloak.Blazor.Services;
using Galaxy.Keycloak.Blazor.Services.Interceptor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "http://localhost:8080/realms/galaxy_realm";
    options.ProviderOptions.ClientId = "blazor_client";
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");
});

builder.Services.AddScoped<AuthInterceptor>();
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<AuthInterceptor>();
    handler.InnerHandler = new HttpClientHandler();

    return new HttpClient(handler) { BaseAddress = new Uri("https://localhost:7079") };
});
builder.Services.AddScoped<UsuarioService>();

await builder.Build().RunAsync();
