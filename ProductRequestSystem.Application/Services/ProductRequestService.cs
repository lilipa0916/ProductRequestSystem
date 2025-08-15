using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductRequestSystem.Application.DTOs;
using ProductRequestSystem.Application.Interfaces;
using ProductRequestSystem.Domain.Entities;
using ProductRequestSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Application.Services
{
    public class ProductRequestService : IProductRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductRequestService> _logger;

        public ProductRequestService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<ProductRequestService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductRequestDto> CreateAsync(CreateProductRequestDto dto, string clientId)
        {
            var productRequest = _mapper.Map<ProductRequest>(dto);
            productRequest.ClientId = clientId;

            var created = await _unitOfWork.ProductRequests.AddAsync(productRequest);
            await _unitOfWork.SaveChangesAsync();

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
