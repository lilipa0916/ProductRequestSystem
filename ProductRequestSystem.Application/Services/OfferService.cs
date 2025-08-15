using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductRequestSystem.Application.DTOs;
using ProductRequestSystem.Application.Interfaces;
using ProductRequestSystem.Domain.Entities;
using ProductRequestSystem.Domain.Enums;
using ProductRequestSystem.Domain.Interfaces;

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

            await _unitOfWork.SaveChangesAsync();

            var result = await _unitOfWork.Offers.GetByIdWithDetailsAsync(created.Id);
            return _mapper.Map<OfferDto>(result!);
        }

        public async Task<IEnumerable<OfferDto>> GetByProviderIdAsync(string providerId)
        {
            var offers = await _unitOfWork.Offers.GetByProviderIdAsync(providerId);
            return _mapper.Map<IEnumerable<OfferDto>>(offers);
        }

        public async Task<OfferDto> UpdateStatusAsync(UpdateOfferStatusDto dto, string clientId)
        {
            var offer = await _unitOfWork.Offers.GetByIdWithDetailsAsync(dto.OfferId);
            if (offer == null)
            {
                throw new ArgumentException("Oferta no encontrada");
            }

            // Verificar que el cliente es dueño de la solicitud
            if (offer.ProductRequest.ClientId != clientId)
            {
                throw new UnauthorizedAccessException("No autorizado para actualizar esta oferta");
            }

            offer.Status = dto.Status;
            offer.UpdatedAt = DateTime.UtcNow;

            // Si se acepta la oferta, cerrar la solicitud y rechazar otras ofertas
            if (dto.Status == OfferStatus.Accepted)
            {
                offer.ProductRequest.Status = ProductRequestStatus.Closed;
                offer.ProductRequest.UpdatedAt = DateTime.UtcNow;

                // Rechazar otras ofertas de la misma solicitud
                var otherOffers = await _unitOfWork.Offers.GetByProductRequestIdAsync(offer.ProductRequestId);
                foreach (var otherOffer in otherOffers.Where(o => o.Id != offer.Id))
                {
                    otherOffer.Status = OfferStatus.Rejected;
                    otherOffer.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.Offers.UpdateAsync(otherOffer);
                }
            }

            await _unitOfWork.Offers.UpdateAsync(offer);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OfferDto>(offer);
        }

        public async Task<OfferDto?> GetByIdAsync(int id)
        {
            var offer = await _unitOfWork.Offers.GetByIdWithDetailsAsync(id);
            return offer != null ? _mapper.Map<OfferDto>(offer) : null;
        }
    }
}
