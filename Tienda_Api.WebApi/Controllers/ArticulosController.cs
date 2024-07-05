using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tienda_Api.Business;
using Tienda_Api.Entities;

namespace Tienda_Api.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ArticulosController : ControllerBase
    {
        private readonly ArticuloService _articuloService;

        public ArticulosController(ArticuloService articuloService)
        {
            _articuloService = articuloService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Articulo>>> GetArticulos()
        {
            var articulos = await _articuloService.GetArticulosAsync();
            return Ok(new { message = "Datos obtenidos con éxito", articulos });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Articulo>> GetArticulo(int id)
        {
            var articulo = await _articuloService.GetArticuloByIdAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return Ok(new { message = "Artículo obtenido con éxito", articulo });
        }

        [HttpPost]
        public async Task<ActionResult<Articulo>> PostArticulo(Articulo articulo)
        {
            await _articuloService.AddArticuloAsync(articulo);
            return CreatedAtAction(nameof(GetArticulo), new { id = articulo.ArticuloId }, new { message = "Artículo creado con éxito", articulo });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticulo(int id, Articulo articulo)
        {
            if (id != articulo.ArticuloId)
            {
                return BadRequest();
            }

            await _articuloService.UpdateArticuloAsync(articulo);
            return Ok(new { message = "Artículo actualizado con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticulo(int id)
        {
            await _articuloService.DeleteArticuloAsync(id);
            return Ok(new { message = "Artículo eliminado con éxito" });
        }
    }
}
