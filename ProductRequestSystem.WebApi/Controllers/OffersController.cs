using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductRequestSystem.Application.DTOs;
using ProductRequestSystem.Application.Interfaces;
using ProductRequestSystem.Domain.Enums;
using System.Security.Claims;

namespace ProductRequestSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OffersController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly ILogger<OffersController> _logger;

        public OffersController(IOfferService offerService, ILogger<OffersController> logger)
        {
            _offerService = offerService;
            _logger = logger;
        }

        /// <summary>
        /// Crear nueva oferta (solo Proveedores)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Provider")]
        public async Task<ActionResult<OfferDto>> Create([FromBody] CreateOfferDto dto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _offerService.CreateAsync(dto, userId);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Invalid offer creation: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Invalid offer operation: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating offer");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtener ofertas del proveedor actual
        /// </summary>
        [HttpGet("my-offers")]
        [Authorize(Roles = "Provider")]
        public async Task<ActionResult<IEnumerable<OfferDto>>> GetMyOffers()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _offerService.GetByProviderIdAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting provider offers");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Actualizar estado de oferta (solo Clientes)
        /// </summary>
        [HttpPut("{id}/status")]
        [Authorize(Roles = "Client")]
        public async Task<ActionResult<OfferDto>> UpdateStatus(int id, [FromBody] OfferStatus status)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var dto = new UpdateOfferStatusDto { OfferId = id, Status = status };
                var result = await _offerService.UpdateStatusAsync(dto, userId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Invalid offer status update: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Unauthorized offer status update: {Message}", ex.Message);
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating offer status");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtener oferta por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<OfferDto>> GetById(int id)
        {
            try
            {
                var result = await _offerService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting offer {Id}", id);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }
}
