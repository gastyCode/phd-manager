using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using PhDManager.Core.IServices;
using PhDManager.Web.Components;
using PhDManager.Web.Services;
using Radzen;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddLocalization();
//builder.Services.AddAuthentication()
//    .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("JWT Scheme", options => { });

HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

builder.Services.AddScoped(sp => new HttpClient(clientHandler)
{
    BaseAddress = new Uri("https://phdmanager.api:8081")
});

builder.Services.AddScoped<PhDManager.Web.Services.AuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

string[] supportedCultures = [ "sk-SK", "en-US" ];
app.UseRequestLocalization(new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures)
);

app.MapGet("Culture/Set", (string culture, string redirectUri, HttpContext context) =>
{
    if (culture is not null)
    {
        context.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, culture)),
            new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
    }

    return Results.LocalRedirect(redirectUri);
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
