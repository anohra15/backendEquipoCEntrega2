using System;
using System.Collections.Generic;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;
using RCVUcabBackend.BussinesLogic.DTOs;

namespace RCVUcabBackend.Persistence.DAOs.Interfaces
{
    public interface ITallerDAO
    {
        public int CreateTaller(TallerDTO taller);
        public int EliminarTaller(Guid taller);
        
        public int ActualizarTaller(TallerDTO taller,Guid id_taller);
        
        public List<AnalisisConsultaDTO> ConsultarRequerimientosAsignados(Guid id_taller);
        
        public List<AnalisisConsultaDTO> ConsultarRequerimientosAsignadosPorFiltro(Guid id_taller,CheckEstadoAnalisisAccidente filtro);

        public List<PiezasConsultDTO> ConsultarPiezasAReparar(Guid id_analisis);

        public int RepararPieza(Guid id_pieza);
        public int ComprarPieza(Guid id_pieza);
        public int EditardescripcionPieza(Guid id_pieza,PiezaEditDescripcionDTO descripcionNueva);
        public int CrearCotizacionDeReparacion(CrearCotizacionDTO cotizacion);
        public List<AnalisisConsultaDTO> ConsultarRequerimientosConOrdenPago(Guid id_taller);
        public int AsosciarFaacturaConOrdenPago(Guid id_orden_pago,OrdenDePagoAsociarFacturaDTO facturaOrdenPago);
        public int crearUsuarioTaller(crearUsuarioTallerDTO usuarioTaller,Guid idTaller);

        public int EliminarUsuarioTaller(Guid id_usuario_taller);

        public int ActualizarUsuarioTaller(ActualizarUsuarioTallerDTO tallerCambios,Guid id_usuario_taller);

    }
}