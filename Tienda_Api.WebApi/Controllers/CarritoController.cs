using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tienda_Api.Business;
using Tienda_Api.Entities;

namespace Tienda_Api.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class CarritoController : ControllerBase
    {
        private readonly CarritoService _carritoService;

        public CarritoController(CarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        [HttpGet("{clienteId}")]
        public async Task<ActionResult<Carrito>> GetCarrito(int clienteId)
        {
            var carrito = await _carritoService.GetCarritoByClienteIdAsync(clienteId);
            if (carrito == null)
            {
                return NotFound();
            }
            return Ok(new { message = "Carrito obtenido con éxito", carrito });
        }

        [HttpPost("{clienteId}/articulos/{articuloId}")]
        public async Task<ActionResult> AddArticuloToCarrito(int clienteId, int articuloId, [FromBody] int cantidad)
        {
            await _carritoService.AddArticuloToCarritoAsync(clienteId, articuloId, cantidad);
            return Ok(new { message = "Artículo añadido al carrito con éxito" });
        }

        [HttpDelete("{clienteId}/articulos/{articuloId}")]
        public async Task<ActionResult> RemoveArticuloFromCarrito(int clienteId, int articuloId)
        {
            await _carritoService.RemoveArticuloFromCarritoAsync(clienteId, articuloId);
            return Ok(new { message = "Artículo eliminado del carrito con éxito" });
        }
    }
}
