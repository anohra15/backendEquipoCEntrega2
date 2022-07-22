using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCVUcabBackend.Persistence.Entities
{
    [Table("Telefono")]
    public class TelefonoEntity:BaseEntity
    {
        [MaxLength(5)]
        [Required]
        public string codigo_area { get; set; }
        [MaxLength(10)]
        [Required]
        public string numero_telefono { get; set; }
        public UsuarioTallerEntity usuario_taller { get; set; }
    }
}