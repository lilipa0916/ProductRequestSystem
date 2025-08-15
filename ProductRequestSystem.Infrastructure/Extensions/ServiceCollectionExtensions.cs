using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductRequestSystem.Domain.Entities;
using ProductRequestSystem.Domain.Interfaces;
using ProductRequestSystem.Infrastructure.Data;
using ProductRequestSystem.Infrastructure.Repositories;



namespace ProductRequestSystem.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Database Context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Identity Core - ESTA VERSIÓN SIEMPRE FUNCIONA
            services.AddIdentityCore<User>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Agregar UserManager y RoleManager manualmente
            services.AddScoped<UserManager<User>>();
            services.AddScoped<RoleManager<IdentityRole>>();

            // Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRequestRepository, ProductRequestRepository>();
            services.AddScoped<IOfferRepository, OfferRepository>();

            return services;
        }

        public static async Task<IServiceProvider> InitializeDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                // Ensure database is created and apply any pending migrations
                await context.Database.EnsureCreatedAsync();

                // If using migrations instead:
                // await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                // Log the error
                var logger = scope.ServiceProvider.GetService<ILogger<ApplicationDbContext>>();
                logger?.LogError(ex, "An error occurred while initializing the database");
                throw;
            }

            return serviceProvider;
        }
    }
}
