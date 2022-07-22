using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;

namespace RCVUcabBackend.BussinesLogic.DTOs
{
    public class CrearCotizacionDTO
    {
    public double costo_reparacion { get; set; }
    public string fecha_culminacion { get; set; }
    public string fecha_inicio { get; set; }
    public Guid usuario_taller { get; set; }
    public Guid idAnalisis{ get; set; }
    }
}