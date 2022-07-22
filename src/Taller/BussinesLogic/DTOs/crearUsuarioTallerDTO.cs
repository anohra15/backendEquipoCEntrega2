using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;
using RCVUcabBackend.BussinesLogic.DTOs;

namespace RCVUcabBackend.BussinesLogic.DTOs
{
    public class crearUsuarioTallerDTO{
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string direccion { get; set; }//Crear una entidad direccion en el futuro
        public string cargo { get; set; }
        public CheckEstadoUsuarioTaller estado { get; set; }
        public string email { get; set; }
        public string contraseña { get; set; }
        public List<crearTelefonoDTO> telefonos { get; set; }
    }
}