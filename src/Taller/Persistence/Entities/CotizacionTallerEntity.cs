using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;

namespace RCVUcabBackend.Persistence.Entities
{
    [Table("Cotizacion_taller")]
    public class CotizacionTallerEntity:BaseEntity
    {
        [Required]
        public int cantidad_piezas_reparar { get; set; }
        [Required]
        public double costo_reparacion { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime? fecha_culminacion { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime? fecha_inicio { get; set; }
        [Required]
        public CheckEstadoCotizacionTaller estado { get; set; }
        [Required] 
        public UsuarioTallerEntity usuario_taller { get; set; }
        [Required]
        public Guid idAnalisis {get;set;}
    }
}