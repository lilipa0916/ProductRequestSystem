using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductRequestSystem.Application.DTOs;
using ProductRequestSystem.Application.Interfaces;
using System.Security.Claims;

namespace ProductRequestSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductRequestsController : ControllerBase
    {
        private readonly IProductRequestService _productRequestService;
        private readonly ILogger<ProductRequestsController> _logger;

        public ProductRequestsController(
            IProductRequestService productRequestService,
            ILogger<ProductRequestsController> logger)
        {
            _productRequestService = productRequestService;
            _logger = logger;
        }

        /// <summary>
        /// Crear nueva solicitud de producto (solo Clientes)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<ActionResult<ProductRequestDto>> Create([FromBody] CreateProductRequestDto dto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _productRequestService.CreateAsync(dto, userId);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product request");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtener solicitudes del cliente actual
        /// </summary>
        [HttpGet("my-requests")]
        [Authorize(Roles = "Client")]
        public async Task<ActionResult<IEnumerable<ProductRequestDto>>> GetMyRequests()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var result = await _productRequestService.GetByClientIdAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting client requests");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtener solicitudes abiertas (solo Proveedores)
        /// </summary>
        [HttpGet("open")]
        [Authorize(Roles = "Provider")]
        public async Task<ActionResult<IEnumerable<ProductRequestDto>>> GetOpenRequests()
        {
            try
            {
                var result = await _productRequestService.GetOpenRequestsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting open requests");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtener solicitud por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductRequestDto>> GetById(int id)
        {
            try
            {
                var result = await _productRequestService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product request {Id}", id);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }

}
