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


    [HttpGet("obtener-productos")]
    public IActionResult ObtenerProductos()
    {
        var productos = _context.Productos
            .OrderBy(p => p.Id) 
            .Select(p => new
            {
                p.Id,
                p.Nombre,
                p.Cantidad
            })
            .ToList();

        return Ok(productos);
    }

    // POST /productos/agrear-nuevo-producto
    [HttpPost("agrear-nuevo-producto")]
	public IActionResult AgregarProducto([FromBody] Producto nuevo)
	{
		if (string.IsNullOrWhiteSpace(nuevo.Nombre))
			return BadRequest(new { mensaje = "El nombre no debe ir vacio." });

		if (nuevo.Cantidad < 0)
			return BadRequest(new { mensaje = "La cantidad no puede ser negativa." });

		_context.Productos.Add(nuevo);
		_context.SaveChanges();

		return CreatedAtAction(nameof(ObtenerProductos), new { id = nuevo.Id }, nuevo);
	}

	// PUT /productos/actualizar-producto
	[HttpPut("actualizar-producto")]
	public IActionResult ActualizarProducto([FromBody] Producto actualizado)
	{
		var producto = _context.Productos.FirstOrDefault(p => p.Id == actualizado.Id);

		if (producto == null)
			return NotFound(new { mensaje = "No se encontro el producto con el ID asociado." });

		if (string.IsNullOrWhiteSpace(actualizado.Nombre))
			return BadRequest(new { mensaje = "El nombre del producto no puede estar vacio." });

		if (actualizado.Cantidad < 0)
			return BadRequest(new { mensaje = "La cantidad no puede ser negativa." });

		producto.Nombre = actualizado.Nombre;
		producto.Cantidad = actualizado.Cantidad;

		_context.SaveChanges();

		return Ok(new
		{
			mensaje = "Se actualizo el producto correctamente",
			producto
		});
	}

	// DELETE /producots/eliminar-producto/{id}
	[HttpDelete("eliminar-prodcuto/{id}")]
	public IActionResult EliminarProducto(int id)
	{
		var producto = _context.Productos.FirstOrDefault(p => p.Id == id);

		if (producto == null)
			return NotFound(new { mensaje = "No se encontro ningun produto con ese ID asociado" });

		_context.Productos.Remove(producto);
		_context.SaveChanges();

		return Ok(new
		{
			mensaje = "Se elimino el producto correctamente",
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
