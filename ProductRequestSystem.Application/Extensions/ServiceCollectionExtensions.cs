using Microsoft.Extensions.DependencyInjection;
using ProductRequestSystem.Application.DTOs;
using ProductRequestSystem.Application.Interfaces;
using ProductRequestSystem.Application.Mappings;
using ProductRequestSystem.Application.Services;
using ProductRequestSystem.Application.Validators;
using ProductRequestSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Application Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductRequestService, ProductRequestService>();
            services.AddScoped<IOfferService, OfferService>();

            return services;
        }
    }.SaveChangesAsync();

    var result = await _unitOfWork.ProductRequests.GetWithOffersAsync(created.Id);
        return _mapper.Map<ProductRequestDto>(result);
    }

public async Task<IEnumerable<ProductRequestDto>> GetByClientIdAsync(string clientId)
{
    var requests = await _unitOfWork.ProductRequests.GetByClientIdAsync(clientId);
    return _mapper.Map<IEnumerable<ProductRequestDto>>(requests);
}

public async Task<IEnumerable<ProductRequestDto>> GetOpenRequestsAsync()
{
    var requests = await _unitOfWork.ProductRequests.GetOpenRequestsAsync();
    return _mapper.Map<IEnumerable<ProductRequestDto>>(requests);
}

public async Task<ProductRequestDto?> GetByIdAsync(int id)
{
    var request = await _unitOfWork.ProductRequests.GetWithOffersAsync(id);
    return request != null ? _mapper.Map<ProductRequestDto>(request) : null;
}
}
}
