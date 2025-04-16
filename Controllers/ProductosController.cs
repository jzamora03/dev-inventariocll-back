using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventarioAPI.Data;
using InventarioAPI.Models;

namespace InventarioAPI.Controllers;

[Authorize]
[ApiController]
[Route("productos")]
public class ProductosController : ControllerBase
{
	private readonly AppDbContext _context;

	public ProductosController(AppDbContext context)
	{
		_context = context;
	}

	//// POST /productos/movimiento
	//[HttpPost("movimiento")]
	//public IActionResult RegistrarMovimiento([FromBody] MovimientoDTO data)
	//{
	//	var producto = _context.Productos.FirstOrDefault(p => p.Id == data.Id);

	//	if (producto == null)
	//		return NotFound(new { mensaje = "El producto no existe" });

	//	producto.Cantidad += data.Cantidad;
	//	_context.SaveChanges();

	//	return Ok(new
	//	{
	//		mensaje = "Movimiento registrado correctamente",
	//		producto
	//	});
	//}

	// GET /productos/inventario
	[HttpGet("obtener-inventario")]
	public IActionResult ObtenerInventario()
	{
		var lista = _context.Productos.ToList();
		return Ok(lista);
	}
		
	// POST /productos/agrear-nuevo-producto
	[HttpPost("agrear-nuevo-producto")]
	public IActionResult AgregarProducto([FromBody] Producto nuevo)
	{
		if (string.IsNullOrWhiteSpace(nuevo.Nombre))
			return BadRequest(new { mensaje = "El nombre no puede estar vacío." });

		if (nuevo.Cantidad < 0)
			return BadRequest(new { mensaje = "La cantidad no puede ser negativa." });

		_context.Productos.Add(nuevo);
		_context.SaveChanges();

		return CreatedAtAction(nameof(ObtenerInventario), new { id = nuevo.Id }, nuevo);
	}

	// PUT /productos/actualizar-producto
	[HttpPut("actualizar-producto")]
	public IActionResult ActualizarProducto([FromBody] Producto actualizado)
	{
		var producto = _context.Productos.FirstOrDefault(p => p.Id == actualizado.Id);

		if (producto == null)
			return NotFound(new { mensaje = "No se encontró el producto con el ID proporcionado." });

		if (string.IsNullOrWhiteSpace(actualizado.Nombre))
			return BadRequest(new { mensaje = "El nombre del producto no puede estar vacío." });

		if (actualizado.Cantidad < 0)
			return BadRequest(new { mensaje = "La cantidad no puede ser negativa." });

		producto.Nombre = actualizado.Nombre;
		producto.Cantidad = actualizado.Cantidad;

		_context.SaveChanges();

		return Ok(new
		{
			mensaje = "Producto actualizado correctamente.",
			producto
		});
	}

	// DELETE /producots/eliminar-producto/{id}
	[HttpDelete("eliminar-prodcuto/{id}")]
	public IActionResult EliminarProducto(int id)
	{
		var producto = _context.Productos.FirstOrDefault(p => p.Id == id);

		if (producto == null)
			return NotFound(new { mensaje = "No se encontró ningún producto con ese ID asociado" });

		_context.Productos.Remove(producto);
		_context.SaveChanges();

		return Ok(new
		{
			mensaje = "Producto eliminado exitosamente.",
			producto
		});
	}
}

// DTO para el movimiento
public class MovimientoDTO
{
	public int Id { get; set; }
	public int Cantidad { get; set; }
}
