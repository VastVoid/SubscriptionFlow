using Application.Services;
using Application.Services.Abstractions;
using Application.StateMachines;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Persistence;
using Persistence.Common.Configuration.Connection;
using Persistence.Common.Configuration.Connection.PostgreSql;
using SubscriptionAutomaton.Common.Clock;
using SubscriptionAutomaton.Common.Simulation;
using SubscriptionAutomaton.Components;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddSingleton<SubscriptionStateMachine>()
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });

AddAuthorization(services);

services.AddScoped<ProtectedSessionStorage>();
services.AddSingleton<IClock, SimulatedClock>();
services.AddSingleton<CalendarSimulator>();
services.AddHttpContextAccessor();

RegisterPostgres(services, configuration);
AddServices(services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

static void RegisterPostgres(IServiceCollection serviceCollection, IConfiguration configuration)
{
    var postgresConfiguration = configuration.GetSection("PostgreSql").Get<PostgreSqlConfiguration>()
        ?? throw new ArgumentException(nameof(PostgreSqlConfiguration));
    serviceCollection.AddSingleton(postgresConfiguration);
    serviceCollection.AddScoped<IConnectionStringFactory, PostgreSqlConnectionStringFactory>();
    serviceCollection.AddScoped<PostgreDbContext>();
}

static void AddServices(IServiceCollection serviceCollection)
{
    var types = typeof(AuthService).Assembly.GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));

    foreach (var type in types)
    {
        var ifaces = type.GetInterfaces();
        foreach (var iface in ifaces)
        {
            serviceCollection.AddScoped(iface, type);
        }
    }
}

static void AddAuthorization(IServiceCollection serviceCollection)
{
    serviceCollection
        .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/login";
            options.AccessDeniedPath = "/login";
            options.ExpireTimeSpan = new TimeSpan(7, 0, 0, 0);
        });
    serviceCollection.AddAuthorization();
    serviceCollection.AddCascadingAuthenticationState();
}