using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;

namespace RCVUcabBackend.Persistence.Entities
{
    public class UsuarioTallerEntity:BaseEntity
    {
        [MaxLength(200)]
        [Required]
        public string primer_nombre { get; set; }
        [MaxLength(250)]
        public string? segundo_nombre { get; set; }
        [MaxLength(200)]
        [Required]
        public string primer_apellido { get; set; }
        [MaxLength(250)]
        public string? segundo_apellido { get; set; }
        [MaxLength(300)]
        [Required]
        public string direccion { get; set; }
        [MaxLength(200)]
        [Required]
        public string cargo { get; set; }
        [MaxLength(200)]
        [Required]//Activo o desactivo
        public CheckEstadoUsuarioTaller estado { get; set; }
        [MaxLength(500)]
        public string? email { get; set; }
        [MaxLength(20)]
        [Required]
        public string contraseña { get; set; }
        [Required]
        public TallererEntity taller{ get; set; }
        [Required]
        public ICollection<TelefonoEntity> Telefonos { get; set; }
        [Required]
        public ICollection<CotizacionTallerEntity> cotizaciones_taller { get; set; }
    }
}