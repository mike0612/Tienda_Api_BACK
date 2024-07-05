using Microsoft.EntityFrameworkCore;
using Tienda_Api.Data;
using Tienda_Api.Entities;


namespace Tienda_Api.Business
{
    public class ClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id) => await _context.Clientes.FindAsync(id);

        public async Task<Cliente> AddClienteAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task DeleteClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }

    public class TiendaService
    {
        private readonly AppDbContext _context;

        public TiendaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tienda>> GetTiendasAsync()
        {
            return await _context.Tiendas.ToListAsync();
        }

        public async Task<Tienda> GetTiendaByIdAsync(int id)
        {
            return await _context.Tiendas.FindAsync(id);
        }

        public async Task<Tienda> AddTiendaAsync(Tienda tienda)
        {
            _context.Tiendas.Add(tienda);
            await _context.SaveChangesAsync();
            return tienda;
        }

        public async Task<Tienda> UpdateTiendaAsync(Tienda tienda)
        {
            _context.Entry(tienda).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return tienda;
        }

        public async Task DeleteTiendaAsync(int id)
        {
            var tienda = await _context.Tiendas.FindAsync(id);
            if (tienda != null)
            {
                _context.Tiendas.Remove(tienda);
                await _context.SaveChangesAsync();
            }
        }
    }

    public class ArticuloService
    {
        private readonly AppDbContext _context;

        public ArticuloService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Articulo>> GetArticulosAsync()
        {
            return await _context.Articulos.ToListAsync();
        }

        public async Task<Articulo> GetArticuloByIdAsync(int id)
        {
            return await _context.Articulos.FindAsync(id);
        }

        public async Task<Articulo> AddArticuloAsync(Articulo articulo)
        {
            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();
            return articulo;
        }

        public async Task<Articulo> UpdateArticuloAsync(Articulo articulo)
        {
            _context.Entry(articulo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return articulo;
        }

        public async Task DeleteArticuloAsync(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo != null)
            {
                _context.Articulos.Remove(articulo);
                await _context.SaveChangesAsync();
            }
        }
    }

    public class CarritoService
    {
        private readonly AppDbContext _context;

        public CarritoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Carrito> GetCarritoByClienteIdAsync(int clienteId)
        {
            return await _context.Carritos
                .Include(c => c.CarritoArticulos)
                .ThenInclude(ca => ca.Articulo)
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
        }

        public async Task<Carrito> AddArticuloToCarritoAsync(int clienteId, int articuloId, int cantidad)
        {
            var carrito = await _context.Carritos
        .Include(c => c.CarritoArticulos)
        .FirstOrDefaultAsync(c => c.ClienteId == clienteId);

            if (carrito == null)
            {
                var cliente = await _context.Clientes.FindAsync(clienteId);
                if (cliente == null)
                {
                    throw new Exception("Cliente no encontrado");
                }

                carrito = new Carrito { ClienteId = clienteId, Cliente = cliente };
                _context.Carritos.Add(carrito);
            }

            var carritoArticulo = carrito.CarritoArticulos
                .FirstOrDefault(ca => ca.ArticuloId == articuloId);

            if (carritoArticulo == null)
            {
                var articulo = await _context.Articulos.FindAsync(articuloId);
                if (articulo == null)
                {
                    throw new Exception("Articulo no encontrado");
                }

                carritoArticulo = new CarritoArticulo
                {
                    CarritoId = carrito.CarritoId,
                    Carrito = carrito,
                    ArticuloId = articuloId,
                    Articulo = articulo,
                    Cantidad = cantidad
                };
                carrito.CarritoArticulos.Add(carritoArticulo);
            }
            else
            {
                carritoArticulo.Cantidad += cantidad;
            }

            await _context.SaveChangesAsync();
            return carrito;
        }

        public async Task RemoveArticuloFromCarritoAsync(int clienteId, int articuloId)
        {
            var carrito = await _context.Carritos
                .Include(c => c.CarritoArticulos)
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);

            if (carrito == null) return;

            var carritoArticulo = carrito.CarritoArticulos
                .FirstOrDefault(ca => ca.ArticuloId == articuloId);

            if (carritoArticulo != null)
            {
                carrito.CarritoArticulos.Remove(carritoArticulo);
                await _context.SaveChangesAsync();
            }
        }
    }

}
