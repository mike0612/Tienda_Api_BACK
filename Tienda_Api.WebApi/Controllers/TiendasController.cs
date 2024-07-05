using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tienda_Api.Business;
using Tienda_Api.Entities;

namespace Tienda_Api.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TiendasController : ControllerBase
    {
        private readonly TiendaService _tiendaService;

        public TiendasController(TiendaService tiendaService)
        {
            _tiendaService = tiendaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tienda>>> GetTiendas()
        {
            var tiendas = await _tiendaService.GetTiendasAsync();
            return Ok(new { message = "Tiendas obtenidas con éxito", tiendas });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tienda>> GetTienda(int id)
        {
            var tienda = await _tiendaService.GetTiendaByIdAsync(id);
            if (tienda == null)
            {
                return NotFound();
            }
            return Ok(new { message = "Tienda obtenida con éxito", tienda });
        }

        [HttpPost]
        public async Task<ActionResult<Tienda>> PostTienda(Tienda tienda)
        {
            await _tiendaService.AddTiendaAsync(tienda);
            return CreatedAtAction(nameof(GetTienda), new { id = tienda.TiendaId }, new { message = "Tienda creada con éxito", tienda });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTienda(int id, Tienda tienda)
        {
            if (id != tienda.TiendaId)
            {
                return BadRequest();
            }

            await _tiendaService.UpdateTiendaAsync(tienda);
            return Ok(new { message = "Tienda actualizada con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTienda(int id)
        {
            await _tiendaService.DeleteTiendaAsync(id);
            return Ok(new { message = "Tienda eliminada con éxito" });
        }
    }
}
