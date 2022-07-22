using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;

namespace RCVUcabBackend.BussinesLogic.DTOs
{
    public class ActualizarUsuarioTallerDTO
    {
        public Guid id_taller{get;set;}
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string direccion { get; set; }
        public string cargo { get; set; }
        public CheckEstadoUsuarioTaller estado { get; set; }
        public string email { get; set; }
        public string contraseña { get; set; }
        public TallerDTO taller{ get; set; }
        public ICollection<crearTelefonoDTO> Telefonos { get; set; }
    }
}