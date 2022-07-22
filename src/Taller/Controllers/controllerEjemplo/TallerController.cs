using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RCVUcabBackend.Exceptions;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;
using RCVUcabBackend.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RCVUcabBackend.BussinesLogic.DTOs;
using RCVUcabBackend.Persistence.DAOs.Interfaces;

namespace RCVUcabBackend.Controllers.Taller
{
    [ApiController]
    [Microsoft.AspNetCore.Components.Route("taller")]
    public class TallerController:Controller
    {
        private readonly ITallerDAO _tallerDAO;
        private readonly ILogger<TallerController> _logger;
        
        public TallerController(ILogger<TallerController> logger, ITallerDAO tallerDAO)
        {
            _tallerDAO = tallerDAO;
            _logger = logger;
        }
        
        [HttpPost("create/taller")]
        public ApplicationResponse<TallerDTO> createTaller([Required][FromBody]TallerDTO tallerDto)
        {
            var ressponse = new ApplicationResponse<TallerDTO>();
            try
            {
                ressponse.DataInsert = _tallerDAO.CreateTaller(tallerDto);
                ressponse.Message = "se registro exitosamente";
                ressponse.Data = tallerDto;
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }
        
        [HttpDelete("eliminar/taller/{id_taller}")]
        public ApplicationResponse<TallerDTO> eliminarTaller([Required][FromRoute]Guid id_taller)
        {
            var ressponse = new ApplicationResponse<TallerDTO>();
            try
            {
                ressponse.DataInsert = _tallerDAO.EliminarTaller(id_taller);
                ressponse.Message = "se elimino exitosamente el taller de id="+id_taller;
                ressponse.Data = null;
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }
        
        [HttpPut("actualizar/taller/{id_taller}")]
        public ApplicationResponse<TallerDTO> editarTaller([Required][FromBody]TallerDTO tallerCambios,[Required][FromRoute]Guid id_taller)
        {
            var ressponse = new ApplicationResponse<TallerDTO>();
            try
            {
                ressponse.DataInsert = _tallerDAO.ActualizarTaller(tallerCambios,id_taller);
                ressponse.Message = "se edito exitosamente el taller de id="+id_taller;
                ressponse.Data = null;
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }
        
        [HttpGet("consultar/requerimientosAsignados/{id_taller}")]
        public ApplicationResponse<List<AnalisisConsultaDTO>> consultarRequeimientosAsignados([Required][FromRoute]Guid id_taller)
        {
            var ressponse = new ApplicationResponse<List<AnalisisConsultaDTO>>();
            try
            {
                ressponse.Data = _tallerDAO.ConsultarRequerimientosAsignados(id_taller);
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }
        
        [HttpGet("consultarFiltro/requerimientosAsignados/{id_taller}/{filtro_estado}")]
        public ApplicationResponse<List<AnalisisConsultaDTO>> consultarRequeimientosAsignadosFiltro([Required][FromRoute]Guid id_taller,[Required][FromRoute]CheckEstadoAnalisisAccidente filtro_estado)
        {
            var ressponse = new ApplicationResponse<List<AnalisisConsultaDTO>>();
            try
            {
                ressponse.Data = _tallerDAO.ConsultarRequerimientosAsignadosPorFiltro(id_taller,filtro_estado);
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }

        [HttpGet("consultar/piezasReparar/{id_analisis}")]
        public ApplicationResponse<List<PiezasConsultDTO>> consultarPiezasReparar([Required][FromRoute]Guid id_analisis)
        {
            var ressponse = new ApplicationResponse<List<PiezasConsultDTO>>();
            try
            {
                ressponse.Data = _tallerDAO.ConsultarPiezasAReparar(id_analisis);
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }

        [HttpPut("repararPieza/{id_pieza}")]
        public ApplicationResponse<PiezaDTO> repararPieza([Required][FromRoute]Guid id_pieza)
        {
            var ressponse = new ApplicationResponse<PiezaDTO>();
            try
            {
                ressponse.DataInsert = _tallerDAO.RepararPieza(id_pieza);
                ressponse.Message = "se cambio el estado de comprar pieza al estado de reparar pieza";
                ressponse.Data = null;
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }

        [HttpPut("comprarPieza/{id_pieza}")]
        public ApplicationResponse<PiezaDTO> comprarPieza([Required][FromRoute]Guid id_pieza)
        {
            var ressponse = new ApplicationResponse<PiezaDTO>();
            try
            {
                ressponse.DataInsert = _tallerDAO.ComprarPieza(id_pieza);
                ressponse.Message = "se decidio comprar esta pieza";
                ressponse.Data = null;
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }

        [HttpPut("editarDescripcionPieza/{id_pieza}")]
        public ApplicationResponse<PiezaDTO> EditarDescripcionPieza([Required][FromRoute]Guid id_pieza,[Required][FromBody] PiezaEditDescripcionDTO nuevaDescripcion)
        {
            var ressponse = new ApplicationResponse<PiezaDTO>();
            try
            {
                ressponse.DataInsert = _tallerDAO.EditardescripcionPieza(id_pieza,nuevaDescripcion);
                ressponse.Message = "se decidio comprar esta pieza";
                ressponse.Data = null;
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }

        [HttpPost("create/cotizacionReaparacion")]
        public ApplicationResponse<CrearCotizacionDTO> createCotizacionReparacion([Required][FromBody]CrearCotizacionDTO crearCotizacionDto)
        {
            var ressponse = new ApplicationResponse<CrearCotizacionDTO>();
            try
            {
                ressponse.DataInsert = _tallerDAO.CrearCotizacionDeReparacion(crearCotizacionDto);
                ressponse.Message = "se registro exitosamente";
                ressponse.Data = crearCotizacionDto;
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }

        [HttpGet("consultar/requerimientosConOrdenPago/{id_taller}")]
        public ApplicationResponse<List<AnalisisConsultaDTO>> consultarRequeimientosConOrden([Required][FromRoute]Guid id_taller)
        {
            var ressponse = new ApplicationResponse<List<AnalisisConsultaDTO>>();
            try
            {
                ressponse.Data = _tallerDAO.ConsultarRequerimientosConOrdenPago(id_taller);
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }

        [HttpPut("asociarFacturaOrdenPago/{id_orden_pago}")]
        public ApplicationResponse<OrdenDePagoAsociarFacturaDTO> AsociarFacturaOrdenPago([Required][FromRoute]Guid id_orden_pago,[Required][FromBody] OrdenDePagoAsociarFacturaDTO facturaOrdenPago)
        {
            var ressponse = new ApplicationResponse<OrdenDePagoAsociarFacturaDTO>();
            try
            {
                ressponse.DataInsert = _tallerDAO.AsosciarFaacturaConOrdenPago(id_orden_pago,facturaOrdenPago);
                ressponse.Message = "se asocio la factura";
                ressponse.Data = null;
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }

        [HttpPost("crearUsuarioTaller/{id_taller}")]
        public ApplicationResponse<crearUsuarioTallerDTO> crearUsuarioTaller([Required][FromBody]crearUsuarioTallerDTO usuariooTallerDTO,[Required][FromRoute] Guid id_taller)
                {
                    var ressponse = new ApplicationResponse<crearUsuarioTallerDTO>();
                    try
                    {
                        
                        ressponse.DataInsert = _tallerDAO.crearUsuarioTaller(usuariooTallerDTO,id_taller);
                        ressponse.Message = "se creo el usuario del taller";
                        ressponse.Data = usuariooTallerDTO;
                    }
                    catch (ExcepcionTaller ex)
                    {
                        ressponse.Success = false;
                        ressponse.Message = ex.Mensaje;
                    }
                    return ressponse;
                }

        [HttpDelete("eliminar/usuarioTaller/{id_usuario_taller}")]
        public ApplicationResponse<string> eliminarUsuarioTaller([Required][FromRoute]Guid id_usuario_taller)
        {
            var ressponse = new ApplicationResponse<string>();
            try
            {
                ressponse.DataInsert = _tallerDAO.EliminarUsuarioTaller(id_usuario_taller);
                ressponse.Message = "se elimino exitosamente el usuario taller de id="+id_usuario_taller;
                ressponse.Data = "Se elimino";
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }

        [HttpPut("actualizar/usuarioTaller/{id_usuario_taller}")]
        public ApplicationResponse<ActualizarUsuarioTallerDTO> actualizarUsuarioTaller([Required][FromBody]ActualizarUsuarioTallerDTO usuarioTallerCambios,[Required][FromRoute]Guid id_usuario_taller)
        {
            var ressponse = new ApplicationResponse<ActualizarUsuarioTallerDTO>();
            try
            {
                ressponse.DataInsert = _tallerDAO.ActualizarUsuarioTaller(usuarioTallerCambios,id_usuario_taller);
                ressponse.Message = "se edito exitosamente el usario taller de id="+id_usuario_taller;
                ressponse.Data = null;
            }
            catch (ExcepcionTaller ex)
            {
                ressponse.Success = false;
                ressponse.Message = ex.Mensaje;
            }
            return ressponse;
        }

    }
}