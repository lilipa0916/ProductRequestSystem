using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ProductRequestSystem.Application.Interfaces;
using ProductRequestSystem.Application.Services;
using ProductRequestSystem.Client.Components;
using ProductRequestSystem.Client.Services;

namespace ProductRequestSystem.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7000") // API URL
            });

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IProductRequestService, ProductRequestService>();
            builder.Services.AddScoped<IOfferService, OfferService>();


            builder.Services.AddBlazoredLocalStorage();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
