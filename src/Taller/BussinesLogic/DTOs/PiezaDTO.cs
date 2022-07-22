using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;

namespace RCVUcabBackend.BussinesLogic.DTOs
{
    public class PiezaDTO
    {
        public string descripcion_pieza{ get; set; }
        public Guid id_marcca_pieza{ get; set; }
        public CheckEstadoPieza estado{ get; set; }
        public double? precio{ get; set; }
    }
}