using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Models
{
    [Table("movimientos_inventario")]
    public class MovimientoInventario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("producto_id")]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; } = string.Empty;

        [Column("fecha")]
        public DateTime Fecha { get; set; }
    }
}
