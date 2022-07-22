using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RCVUcabBackend.Persistence.Entities;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;

namespace RCVUcabBackend.BussinesLogic.DTOs
{
    public class AnalisisConsultaDTO
    {
        public string titulo_analisis{get; set;}
        public string estado { get; set; }
    }
}