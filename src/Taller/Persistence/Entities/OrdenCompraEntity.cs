using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using System.Reflection.Metadata;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;

namespace RCVUcabBackend.Persistence.Entities
{
    [Table("Orden_compra")]
    public class OrdenCompraEntity:BaseEntity
    {
        [Required]
        public int limite_dias_pago { get; set; }
        [Column(TypeName = "DATE")]
        [Required]
        public DateTime fecha_vencimiento { get; set; }
        [Required]
        public double total_pagar { get; set; }
        [MaxLength(500)]
        [Required]
        public string descripcion { get; set; }
        public string? factura { get; set; }
        [Required]
        public CheckEstadoOrdenDeCompra estado { get; set; }
        [Required]
        public AnalisisEntity analisis { get; set; }

    }
}