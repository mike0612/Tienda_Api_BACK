using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tienda_Api.Business;
using Tienda_Api.Entities;

namespace Tienda_Api.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteService.GetClientesAsync();
            return Ok(new { message = "Clientes obtenidos con éxito", clientes });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(new { message = "Cliente obtenido con éxito", cliente });
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            await _clienteService.AddClienteAsync(cliente);
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.ClienteId }, new { message = "Cliente creado con éxito", cliente });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            await _clienteService.UpdateClienteAsync(cliente);
            return Ok(new { message = "Cliente actualizado con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            await _clienteService.DeleteClienteAsync(id);
            return Ok(new { message = "Cliente eliminado con éxito" });
        }
    }
}
