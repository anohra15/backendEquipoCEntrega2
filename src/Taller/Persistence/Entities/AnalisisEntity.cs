using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;

namespace RCVUcabBackend.Persistence.Entities
{
    public class AnalisisEntity:BaseEntity
    {
        
        [Required]
        public Guid id_usuario_taller { get; set; }//Esto no es usuario taller si no taller
        public Guid? cotizacion_taller { get; set; }
        [Required]
        public string titulo_analisis{get; set;}
        [Required]
        public CheckEstadoAnalisisAccidente estado { get; set; }

        public ICollection<PiezasEntity> piezas { get; set; }
    }
}