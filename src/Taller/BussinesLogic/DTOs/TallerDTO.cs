using System.Collections.Generic;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;

namespace RCVUcabBackend.BussinesLogic.DTOs
{
    public class TallerDTO
    {
        public string nombre_taller { get; set; }
        public string direccion { get; set; }
        public string RIF { get; set; }
        public CheckEstadoTaller estado { get; set; }

        public List<MarcaDTO> Marcac_Carros { get; set; }
    }
}