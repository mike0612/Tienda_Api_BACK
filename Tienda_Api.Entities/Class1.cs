namespace Tienda_Api.Entities
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public required string Nombre { get; set; }
        public required string Apellidos { get; set; }
        public required string Direccion { get; set; }
        public required ICollection<ClienteArticulo> ClienteArticulos { get; set; }
    }

    public class Tienda
    {
        public int TiendaId { get; set; }
        public required string Sucursal { get; set; }
        public required string Direccion { get; set; }
        public required ICollection<ArticuloTienda> ArticuloTiendas { get; set; }
    }

    public class Articulo
    {
        public int ArticuloId { get; set; }
        public required string Codigo { get; set; }
        public required string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int Stock { get; set; }
        public required ICollection<ArticuloTienda> ArticuloTiendas { get; set; }
        public required ICollection<ClienteArticulo> ClienteArticulos { get; set; }
    }

    public class ArticuloTienda
    {
        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }
        public int TiendaId { get; set; }
        public  Tienda Tienda { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class ClienteArticulo
    {
        public int ClienteId { get; set; }
        public required Cliente Cliente { get; set; }
        public int ArticuloId { get; set; }
        public required Articulo Articulo { get; set; }
        public DateTime Fecha { get; set; }
    }


    public class Carrito
    {
        public int CarritoId { get; set; }
        public int ClienteId { get; set; }
        public required Cliente Cliente { get; set; }
        public ICollection<CarritoArticulo> CarritoArticulos { get; set; } = new List<CarritoArticulo>();

        public Carrito()
        {
            // Inicializar colecciones
            CarritoArticulos = new List<CarritoArticulo>();
        }
    }

    public class CarritoArticulo
    {
        public int CarritoId { get; set; }
        public required Carrito Carrito { get; set; }
        public int ArticuloId { get; set; }
        public required Articulo Articulo { get; set; }
        public int Cantidad { get; set; }
    }

    public class RegisterModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class LoginModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
