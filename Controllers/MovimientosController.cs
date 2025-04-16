using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventarioAPI.Data;
using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("movimientos")]
    public class MovimientosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovimientosController(AppDbContext context)
        {
            _context = context;
        }

        // POST /movimientos/registrar-movimiento-product
        [HttpPost("registrar-movimiento-producto")]
        public IActionResult RegistrarMovimiento([FromBody] MovimientoInventarioDTO dto)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == dto.ProductoId);
            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            if (dto.Cantidad == 0)
                return BadRequest(new { mensaje = "La cantidad no puede ser cero." });

            var tipo = dto.Cantidad > 0 ? "Entrada" : "Salida";

            if (tipo == "Salida" && producto.Cantidad + dto.Cantidad < 0)
                return BadRequest(new { mensaje = "No hay suficiente stock para realizar la salida." });

            producto.Cantidad += dto.Cantidad;

            var movimiento = new MovimientoInventario
            {
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                Tipo = tipo,
                Fecha = DateTime.UtcNow
            };

            _context.MovimientosInventario.Add(movimiento);
            _context.SaveChanges();

            return Ok(new
            {
                mensaje = "Movimiento registrado exitosamente.",
                movimiento
            });
        }

        [HttpGet("historial-movimientos")]
        public IActionResult ObtenerHistorial()
        {
            var movimientos = _context.MovimientosInventario
                .Include(m => m.Producto)
                .OrderByDescending(m => m.Fecha)
                .ToList();

            var resultado = movimientos.Select(m => new
            {
                m.Id,
                m.ProductoId,
                Producto = m.Producto != null ? m.Producto.Nombre : "Producto no disponible",
                m.Cantidad,
                m.Tipo,
                m.Fecha
            });

            return Ok(resultado);
        }

        [HttpGet("historial-movimientos-producto/{productoId}")]
        public IActionResult ObtenerHistorialPorProducto(int productoId)
        {
            var movimientos = _context.MovimientosInventario
                .Include(m => m.Producto)
                .Where(m => m.ProductoId == productoId)
                .OrderByDescending(m => m.Fecha)
                .ToList();

            if (!movimientos.Any())
                return NotFound(new { mensaje = "No hay movimientos registrados para este producto." });

            var resultado = movimientos.Select(m => new
            {
                m.Id,
                m.ProductoId,
                Producto = m.Producto?.Nombre ?? "Producto no disponible",
                m.Cantidad,
                m.Tipo,
                m.Fecha
            });

            return Ok(resultado);
        }
    }

    public class MovimientoInventarioDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
