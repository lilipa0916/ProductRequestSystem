using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductRequestSystem.Application.DTOs;
using ProductRequestSystem.Application.Interfaces;
using ProductRequestSystem.Domain.Entities;
using ProductRequestSystem.Domain.Enums;
using ProductRequestSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OfferService> _logger;

        public OfferService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<OfferService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OfferDto> CreateAsync(CreateOfferDto dto, string providerId)
        {
            // Verificar que la solicitud existe y está abierta
            var productRequest = await _unitOfWork.ProductRequests.GetByIdAsync(dto.ProductRequestId);
            if (productRequest == null)
            {
                throw new ArgumentException("Solicitud de producto no encontrada");
            }

            if (productRequest.Status != ProductRequestStatus.Open)
            {
                throw new InvalidOperationException("No se pueden crear ofertas para esta solicitud");
            }

            var offer = _mapper.Map<Offer>(dto);
            offer.ProviderId = providerId;

            var created = await _unitOfWork.Offers.AddAsync(offer);

            // Actualizar estado de la solicitud
            productRequest.Status = ProductRequestStatus.InNegotiation;
            productRequest.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.ProductRequests.UpdateAsync(productRequest);

            return offer;
        }
    }
