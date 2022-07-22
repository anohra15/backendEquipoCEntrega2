using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RCVUcabBackend.Persistence.Entities;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;

namespace RCVUcabBackend.BussinesLogic.DTOs
{
    public class PiezasConsultDTO
    {
        public string descripcion_pieza{ get; set; }
        public string estado{ get; set; }
        public double? precio{ get; set; }
    }    
}