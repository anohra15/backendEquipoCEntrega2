using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCVUcabBackend.Persistence.Entities
{
    [Table("Marca_carro")]
    public class MarcaCarroEntity:BaseEntity
    {
        [MaxLength(200)]
        [Required]
        public string nombre_marca { get; set; }
        public ICollection<TallererEntity> talleres { get; set; }
    }
}