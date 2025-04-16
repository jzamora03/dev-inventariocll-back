using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventarioAPI.Models;

[Table("productos")]
public class Producto
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    public string? Nombre { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }
}