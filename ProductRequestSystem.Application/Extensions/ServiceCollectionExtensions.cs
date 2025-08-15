using Microsoft.Extensions.DependencyInjection;
using ProductRequestSystem.Application.Interfaces;
using ProductRequestSystem.Application.Mappings;
using ProductRequestSystem.Application.Services;
using ProductRequestSystem.Application.Validators;

using AutoMapper;
using FluentValidation;


namespace ProductRequestSystem.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // FluentValidation
            services.AddValidatorsFromAssembly(typeof(CreateProductRequestValidator).Assembly);

            // Application Services - USAR LAS INTERFACES CORRECTAS
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductRequestService, ProductRequestService>();
            services.AddScoped<IOfferService, OfferService>();

            return services;
        }
    }
}
