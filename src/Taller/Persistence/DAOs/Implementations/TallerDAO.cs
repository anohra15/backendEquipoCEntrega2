using System;
using System.Collections.Generic;
using System.Linq;
using RCVUcabBackend.Exceptions;
using RCVUcabBackend.Persistence.Database;
using RCVUcabBackend.Persistence.Entities;
using RCVUcabBackend.Persistence.Entities.ChecksEntitys;
using Microsoft.EntityFrameworkCore;
using RCVUcabBackend.BussinesLogic.DTOs;
using RCVUcabBackend.Persistence.DAOs.Interfaces;
using System.Regex;
namespace RCVUcabBackend.Persistence.DAOs.Implementations
{
    public class TallerDAO: ITallerDAO
    {
        public readonly IRCVDbContext _context;
        public List<PiezasEntity> listaPiezas;
        public AnalisisEntity analisis;
        public TallererEntity Taller=new TallererEntity();
        public UsuarioTallerEntity usuarioTaller=new UsuarioTallerEntity();
        public CotizacionTallerEntity cotizacionEntity=new CotizacionTallerEntity();
        public int error = 0;
        public string mensajeError = "Ocurrio un error inesperado ";
        public ICollection<MarcaCarroEntity> listaMarcas= new List<MarcaCarroEntity>();
        public ICollection<TelefonoEntity> listaTelefonos= new List<TelefonoEntity>();
        public ICollection<TelefonoEntity> listaTelefonos2= new List<TelefonoEntity>();
        public ICollection<MarcaCarroEntity> listaMarcas2= new List<MarcaCarroEntity>();
        
        public TallerDAO(IRCVDbContext context)
        {
            _context = context;
        }

        public List<PiezasConsultDTO> crearListaPiezas(AnalisisEntity analisisDePiezas){

            var listPiezsConsultar= new List<PiezasConsultDTO>();

            foreach (var pieza in analisisDePiezas.piezas)
            {
                var piezaConsulta=new PiezasConsultDTO();
                piezaConsulta.descripcion_pieza=pieza.descripcion_pieza;
                piezaConsulta.estado=pieza.estado.ToString();
                piezaConsulta.precio=pieza.precio;
                
                listPiezsConsultar.Add(piezaConsulta);
            }

            return listPiezsConsultar;
        }

        public List<PiezasConsultDTO> crearListaPiezasSleccionadasParaReparar(AnalisisEntity analisisDePiezas){

            var listPiezsConsultar= new List<PiezasConsultDTO>();

            foreach (var pieza in analisisDePiezas.piezas)
            {
                if(pieza.estado.ToString().Equals(CheckEstadoPieza.reparar.ToString())){
                var piezaConsulta=new PiezasConsultDTO();
                piezaConsulta.descripcion_pieza=pieza.descripcion_pieza;
                piezaConsulta.estado=pieza.estado.ToString();
                piezaConsulta.precio=pieza.precio;
                listPiezsConsultar.Add(piezaConsulta);
                }
            }

            return listPiezsConsultar;
        }

        public List<PiezasConsultDTO> crearListaPiezasSeleccionadasAReparar(AnalisisEntity analisisDePiezas){

            var listPiezsConsultar= new List<PiezasConsultDTO>();

            foreach (var pieza in analisisDePiezas.piezas)
            {
                if(pieza.estado.Equals(CheckEstadoPieza.reparar)){
                var piezaConsulta=new PiezasConsultDTO();
                piezaConsulta.descripcion_pieza=pieza.descripcion_pieza;
                piezaConsulta.estado=pieza.estado.ToString();
                piezaConsulta.precio=pieza.precio;
                
                listPiezsConsultar.Add(piezaConsulta);
                }
            }

            return listPiezsConsultar;
        }

        public bool verificarMarca(IRCVDbContext context,MarcaDTO marcaValidar)
        {
            if (context.Marcas.Any(x => x.nombre_marca == marcaValidar.nombre_marca))
            {
                return true;   
            }
            return false;
        }

        public bool verificarTelefono(IRCVDbContext context,crearTelefonoDTO telefonoValidar)
        {
            if (context.Telefonos.Any(x => x.numero_telefono.Equals(telefonoValidar.numero_telefono)))
            {
                return true;   
            }
            return false;
        }

        public bool AsignarMarcaExistente(IRCVDbContext context,MarcaDTO marcaValidar)
        {
            if (verificarMarca( context, marcaValidar))
            {
                var marca = context.Marcas.
                    Where(b => b.nombre_marca.Equals(marcaValidar.nombre_marca)).
                    First();
                this.listaMarcas.Add(marca);
                return true;   
            }
            return false;
        }

        public bool AsignarTelefonoExistente(IRCVDbContext context,crearTelefonoDTO telefonoValidar)
        {
            if (verificarTelefono( context, telefonoValidar))
            {
                var telefono = context.Telefonos.
                    Where(b => b.numero_telefono.Equals(telefonoValidar.numero_telefono)).
                    First();
                this.listaTelefonos.Add(telefono);
                return true;   
            }
            return false;
        }

        public bool validarEspaciosBlancos(string texto)
        {
            var cantidad_espacios = 0;
            foreach (var caracter in texto)
            {
                if (caracter.Equals(' '))
                {
                    cantidad_espacios++;
                    Console.WriteLine(caracter); 
                }
            }

            if (cantidad_espacios == texto.Length)
            {
                return true;
            }

            return false;
        }

        public bool validarExistenciaTaller(IRCVDbContext context,TallerDTO tallerValidar)
        {
            if (context.Talleres.Any(x => x.nombre_taller == tallerValidar.nombre_taller && 
                                                    x.direccion.Equals(tallerValidar.direccion) &&
                                                    x.RIF.Equals(tallerValidar.RIF)
                )
                )
            {
                return true;   
            }

            return false;
        }

        public bool validarExistenciaUsuarioTaller(IRCVDbContext context,crearUsuarioTallerDTO usuariotallerValidar)
        {
            if (context.UsuariosTaller.Any(x => x.primer_nombre.Equals(usuariotallerValidar.primer_nombre) &&
                                                x.segundo_nombre.Equals(usuariotallerValidar.segundo_nombre) &&
                                                x.primer_apellido.Equals(usuariotallerValidar.primer_apellido) &&
                                                x.segundo_apellido.Equals(usuariotallerValidar.segundo_apellido) &&
                                                x.cargo.Equals(usuariotallerValidar.cargo) &&
                                                x.email.Equals(usuariotallerValidar.email)))
            {
                return true;   
            }

            return false;
        }

        public TallererEntity traerTallerA(IRCVDbContext context,Guid id_taller)
        {
            if (context.Talleres.Any(x => x.Id == id_taller ))
            {
                var taller = context.Talleres.
                    Where(b => b.Id==id_taller).
                    Single();
                return taller;
            }
            return null;
        }

        public UsuarioTallerEntity traerUsuarioTaller(IRCVDbContext context,Guid id_usuario_taller)
        {
            if (context.UsuariosTaller.Any(x => x.Id == id_usuario_taller ))
            {
                var usuarioTaller = context.UsuariosTaller.
                    Include(b=>b.Telefonos).
                    Where(b => b.Id==id_usuario_taller).
                    Single();
                return usuarioTaller;
            }
            return null;
        }

        public AnalisisEntity traerAnalisis(IRCVDbContext context,Guid id_analisis)
        {
            if (context.Analisis.Any(x => x.Id == id_analisis ))
            {
                var analisis = _context.Analisis.Include(b => b.piezas).Where(c => c.Id.Equals(id_analisis)).First();
                return analisis;
            }
            return null;
        }

        /*public bool validarExistenciaCotizacionDelAnalisis(IRCVDbContext context,Guid idAnalisis)
        {
            if (context.Cotizaciones.Any(x => x. == tallerValidar.nombre_taller && 
                                                    x.direccion.Equals(tallerValidar.direccion) &&
                                                    x.RIF.Equals(tallerValidar.RIF)
                )
                )
            {
                return true;   
            }

            return false;
        }*/

        public PiezasEntity traerPieza(IRCVDbContext context,Guid id_pieza)
        {
            if (context.Piezas.Any(x => x.Id == id_pieza ))
            {
                var pieza = _context.Piezas.Where(c => c.Id.Equals(id_pieza)).First();
                return pieza;
            }
            return null;
        }

        public OrdenCompraEntity traerOrden(IRCVDbContext context,Guid id_orden)
        {
            if (context.OrdenesCompras.Any(x => x.Id == id_orden ))
            {
                var orden = _context.OrdenesCompras.Where(c => c.Id.Equals(id_orden)).First();
                return orden;
            }
            return null;
        }

        public int cantidadPiezasRepararaElTaller(Guid idAnalisis){
            if(_context.Analisis.Any(x=> x.Id.Equals(idAnalisis))){
                this.analisis=traerAnalisis(_context,idAnalisis);
            }
            return this.crearListaPiezasSleccionadasParaReparar(this.analisis).Count;
        }

        public void crearTallerEntity(TallerDTO T)
        {
            var i2=0;
            var MC=new MarcaCarroEntity();
            foreach (var marcaCarro in T.Marcac_Carros)
            {
                if (!AsignarMarcaExistente(_context, marcaCarro))
                {
                    MC=new MarcaCarroEntity();
                    MC.nombre_marca = marcaCarro.nombre_marca;
                    MC.CreatedAt=DateTime.Now;
                    MC.CreatedBy = null;
                    MC.UpdatedAt = null;
                    MC.UpdatedBy = null;
                    listaMarcas.Add(MC);
                    _context.Marcas.Add(MC);
                    i2=_context.DbContext.SaveChanges();   
                }
            }
            this.Taller.marcas=listaMarcas;
            this.Taller.direccion = T.direccion;
            this.Taller.estado = T.estado;
            this.Taller.nombre_taller = T.nombre_taller;
            this.Taller.RIF = T.RIF;
            this.Taller.CreatedAt=DateTime.Now;
            this.Taller.UpdatedAt = null;
            this.Taller.CreatedBy = null;
            this.Taller.UpdatedBy = null;
        }

        public void eliminarTelefonosDelUsuario(UsuarioTallerEntity usurio){
            _context.Telefonos.RemoveRange(usurio.Telefonos);
            _context.DbContext.SaveChanges();
        }

        public void crearUsuaarioTallerEntity(crearUsuarioTallerDTO usuarioTaller,Guid idTaller)
        {
            var i2=0;
            var telelfonoNuevo=new TelefonoEntity();
            foreach (var telefonoUsuario in usuarioTaller.telefonos)
            {
                if (!AsignarTelefonoExistente(_context, telefonoUsuario))
                {
                    telelfonoNuevo=new TelefonoEntity();
                    telelfonoNuevo.codigo_area=telefonoUsuario.codigo_area;
                    telelfonoNuevo.numero_telefono=telefonoUsuario.numero_telefono;
                    telelfonoNuevo.CreatedAt=DateTime.Now;
                    telelfonoNuevo.CreatedBy = null;
                    telelfonoNuevo.UpdatedAt = null;
                    telelfonoNuevo.UpdatedBy = null;
                    listaTelefonos.Add(telelfonoNuevo);
                    _context.Telefonos.Add(telelfonoNuevo);
                    i2=_context.DbContext.SaveChanges();   
                }
            }
            this.usuarioTaller.Telefonos=listaTelefonos;
            this.usuarioTaller.primer_nombre=usuarioTaller.primer_nombre;
            this.usuarioTaller.segundo_nombre=usuarioTaller.segundo_nombre;
            this.usuarioTaller.primer_apellido=usuarioTaller.primer_apellido;
            this.usuarioTaller.segundo_apellido=usuarioTaller.segundo_apellido;
            this.usuarioTaller.direccion=usuarioTaller.direccion;
            this.usuarioTaller.cargo=usuarioTaller.direccion;
            this.usuarioTaller.estado=CheckEstadoUsuarioTaller.Activo;
            this.usuarioTaller.email=usuarioTaller.email;
            this.usuarioTaller.contraseña=usuarioTaller.contraseña;
            this.usuarioTaller.taller=this.traerTallerA(_context,idTaller);
            this.Taller.CreatedAt=DateTime.Now;
            this.Taller.UpdatedAt = null;
            this.Taller.CreatedBy = null;
            this.Taller.UpdatedBy = null;
        }

        public void crearCotizacionEntity(CrearCotizacionDTO T)
        {
            var i2=0;
            this.cotizacionEntity.cantidad_piezas_reparar=cantidadPiezasRepararaElTaller(T.idAnalisis);
            this.cotizacionEntity.costo_reparacion=T.costo_reparacion;
            this.cotizacionEntity.estado=CheckEstadoCotizacionTaller.Activo;
            this.cotizacionEntity.fecha_inicio=Convert.ToDateTime(T.fecha_inicio);
            this.cotizacionEntity.fecha_culminacion=Convert.ToDateTime(T.fecha_culminacion);
            this.cotizacionEntity.usuario_taller=traerUsuarioTaller(_context,T.usuario_taller);
            this.cotizacionEntity.idAnalisis=T.idAnalisis;
            this.cotizacionEntity.CreatedAt=DateTime.Now;
            this.cotizacionEntity.UpdatedAt = null;
            this.cotizacionEntity.CreatedBy = null;
            this.cotizacionEntity.UpdatedBy = null;
        }

        public int CreateTaller(TallerDTO taller)
        {
            var i=0;
            try
            {
                if (validarExistenciaTaller(_context,taller) == true)
                {
                    error++;
                    mensajeError = "No se puede crear este taller porque ya existe";
                    throw new ExcepcionTaller(mensajeError);
                }
                if ((String.IsNullOrEmpty(taller.direccion)||validarEspaciosBlancos(taller.direccion)) ||
                    (String.IsNullOrEmpty(taller.nombre_taller)||validarEspaciosBlancos(taller.nombre_taller)) ||
                    (String.IsNullOrEmpty(taller.RIF)||validarEspaciosBlancos(taller.RIF)) ||
                    taller.Marcac_Carros.Count == 0)
                {
                    error++;
                    mensajeError = "No se puede crar un taller si alguno de estos datos esta vacio:nombre del taller, direccrioon, RIF y marcas de carros";
                    throw new ExcepcionTaller(mensajeError);
                }
                else
                {
                    crearTallerEntity(taller);
                    var data = _context.Talleres.Add(this.Taller);
                    i=_context.DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionTaller(mensajeError);
            }
            return i;
        }
        
        public int EliminarTaller(Guid id_taller)
        {
            var i=0;
            try
            {
                var data =traerTallerA(_context,id_taller);
                if (data==null)
                {
                    error++;
                    mensajeError = "No existe el talled";
                    throw new ExcepcionTaller(mensajeError);
                }
                else
                {
                    var a=_context.Talleres.Remove(data);
                    i=_context.DbContext.SaveChanges();   
                }
   
            }catch (Exception ex)
            {
                throw new ExcepcionTaller(mensajeError);
            }
            return i;
        }
        
        public int ActualizarTaller(TallerDTO tallerCambios,Guid id_taller)
        {
            var i=0;
            try
            {
                var data =traerTallerA(_context,id_taller);
                if (data==null)
                {
                    error++;
                    mensajeError = "No existe el talled";
                    throw new ExcepcionTaller(mensajeError);
                }
                else
                {
                    if(!(String.IsNullOrEmpty(tallerCambios.direccion)))
                    {
                        if (!(validarEspaciosBlancos(tallerCambios.direccion)))
                        {
                            data.direccion = tallerCambios.direccion;   
                        }
                    }
                    if(!(String.IsNullOrEmpty(tallerCambios.nombre_taller)))
                    {
                        if (!(validarEspaciosBlancos(tallerCambios.nombre_taller)))
                        {
                            data.nombre_taller = tallerCambios.nombre_taller;
                        }
                    }
                    if(!(String.IsNullOrEmpty(tallerCambios.RIF)))
                    {
                        if (!(validarEspaciosBlancos(tallerCambios.RIF)))
                        {
                            data.RIF = tallerCambios.RIF;   
                        }
                    }
                    if(!(String.IsNullOrEmpty(tallerCambios.RIF)))
                    {
                        if (!(validarEspaciosBlancos(tallerCambios.RIF)))
                        {
                            data.estado = tallerCambios.estado;   
                        }
                    }

                    if (tallerCambios.Marcac_Carros != null)
                    {
                        if (tallerCambios.Marcac_Carros.Count != 0)
                        {
                            foreach (var marca in tallerCambios.Marcac_Carros)
                            {
                                if (!AsignarMarcaExistente(_context, marca))
                                {
                                    var MC = new MarcaCarroEntity();
                                    MC.nombre_marca = marca.nombre_marca;
                                    MC.CreatedAt = DateTime.Now;
                                    MC.CreatedBy = null;
                                    MC.UpdatedAt = null;
                                    MC.UpdatedBy = null;
                                    listaMarcas.Add(MC);
                                    _context.Marcas.Add(MC);
                                    i = _context.DbContext.SaveChanges();
                                    listaMarcas2.Add(MC);
                                }
                                else
                                {
                                    
                                    var marcaExistente = _context.Marcas
                                        .Where(b => b.nombre_marca.Equals(marca.nombre_marca)).First();
                                    this.listaMarcas2.Add(marcaExistente);
                                }
                            }
                            data.marcas=this.listaMarcas2;
                        }
                    }

                    _context.Talleres.Update(data);
                    i=_context.DbContext.SaveChanges();   
                }
   
            }catch (Exception ex)
            {
                throw new ExcepcionTaller(mensajeError);
            }
            return i;
        }

        public List<AnalisisConsultaDTO> ConsultarRequerimientosAsignados(Guid id_taller)
        {
            try
            {
                var data = _context.Analisis.Include(b => b.piezas).Where(c => c.id_usuario_taller.Equals(id_taller))
                    .Select(b => new AnalisisConsultaDTO
                    {
                        titulo_analisis = b.titulo_analisis,
                        estado = b.estado.ToString()
                    }).ToList();
                return data;
            }
            catch (Exception e)
            {
                throw new ExcepcionTaller(mensajeError);
            }
        }
        
        public List<AnalisisConsultaDTO> ConsultarRequerimientosAsignadosPorFiltro(Guid id_taller,CheckEstadoAnalisisAccidente filtro)
        {
            try
            {
                var data = _context.Analisis.
                    Include(b => b.piezas).
                    Where(c => c.id_usuario_taller.Equals(id_taller)&&
                               c.estado==filtro)
                    .Select(b => new AnalisisConsultaDTO
                    {
                        titulo_analisis = b.titulo_analisis,
                        estado = b.estado.ToString()
                    }).ToList();
                return data;
            }
            catch (Exception e)
            {
                throw new ExcepcionTaller(mensajeError);
            }
        }

        public List<PiezasConsultDTO> ConsultarPiezasAReparar(Guid id_analisis)
        {
            try
            {
                var data =traerAnalisis(_context,id_analisis);
                if (data==null)
                {
                    error++;
                    mensajeError = "No existe el analisis";
                    throw new ExcepcionTaller(mensajeError);
                }
                else{
                    return crearListaPiezas(data);
                }
            }
            catch (Exception e)
            {
                throw new ExcepcionTaller(mensajeError);
            }
        }

        public int RepararPieza(Guid id_pieza)
        {
            var i=0;
            var data =traerPieza(_context,id_pieza);
            try
            {
                if (data==null)
                {
                    error++;
                    mensajeError = "No existe la pieza";
                    throw new ExcepcionTaller(mensajeError);
                }
                else
                {
                    data.estado=CheckEstadoPieza.reparar;
                    _context.Piezas.Update(data);
                    i=_context.DbContext.SaveChanges();   
                }
   
            }catch (Exception ex)
            {
                throw new ExcepcionTaller(mensajeError);
            }
            return i;
        }

        public int ComprarPieza(Guid id_pieza)
        {
            var i=0;
            var data =traerPieza(_context,id_pieza);
            try
            {
                if (data==null)
                {
                    error++;
                    mensajeError = "No existe la pieza";
                    throw new ExcepcionTaller(mensajeError);
                }
                else
                {
                    data.estado=CheckEstadoPieza.comprar;
                    _context.Piezas.Update(data);
                    i=_context.DbContext.SaveChanges();   
                }
   
            }catch (Exception ex)
            {
                throw new ExcepcionTaller(mensajeError);
            }
            return i;
        }

        public int EditardescripcionPieza(Guid id_pieza,PiezaEditDescripcionDTO descripcionNueva)
        {
            var i=0;
            var data =traerPieza(_context,id_pieza);
            try
            {
                if (data==null)
                {
                    error++;
                    mensajeError = "No existe la pieza";
                    throw new ExcepcionTaller(mensajeError);
                }
                if ((String.IsNullOrEmpty(descripcionNueva.descripcion_pieza)||validarEspaciosBlancos(descripcionNueva.descripcion_pieza))){
                    error++;
                    mensajeError = "No se puede dejar en blanco la nueva descripcion del taller";
                    throw new ExcepcionTaller(mensajeError);
                }
                else
                {
                    data.descripcion_pieza=descripcionNueva.descripcion_pieza;
                    _context.Piezas.Update(data);
                    i=_context.DbContext.SaveChanges();   
                }
   
            }catch (Exception ex)
            {
                throw new ExcepcionTaller(mensajeError);
            }
            return i;
        }

        public List<PiezasConsultDTO> ListaDePiezasSeleccionadasAReparar(Guid id_analisis)
        {
            try
            {
                var data =traerAnalisis(_context,id_analisis);
                if (data==null)
                {
                    error++;
                    mensajeError = "No existe el analisis";
                    throw new ExcepcionTaller(mensajeError);
                }
                else{
                    return crearListaPiezasSeleccionadasAReparar(data);
                }
            }
            catch (Exception e)
            {
                throw new ExcepcionTaller(mensajeError);
            }
        }

        public int CrearCotizacionDeReparacion(CrearCotizacionDTO cotizacion)
        {
            var i=0;
            var dateNow=DateTime.Now;
            var fechaInicio=new DateTime();
            var fechaCulminacion=new DateTime();
            var formatoFecha=new Regex(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|[2-9][0-9])\d\d$");
            var validarFormatoFechaCulminacion=true;
            var validarFormatoFechaInicio=true;
            try
            {
                var dataTaller =traerTallerA(_context,cotizacion.usuario_taller);

                if(cotizacion.fecha_culminacion==null || cotizacion.fecha_inicio==null){
                    error++;
                    mensajeError = "La fecha de inicio o de culminacion no pueden estar vacio";
                    throw new ExcepcionTaller(mensajeError);
                }else{
                    validarFormatoFechaCulminacion=formatoFecha.IsMatch(cotizacion.fecha_culminacion);
                    validarFormatoFechaInicio=formatoFecha.IsMatch(cotizacion.fecha_inicio);
                }

                if(validarFormatoFechaInicio && validarFormatoFechaCulminacion){
                    fechaInicio=Convert.ToDateTime(cotizacion.fecha_inicio);
                    fechaCulminacion=Convert.ToDateTime(cotizacion.fecha_culminacion);
                }else
                {
                    error++;
                    mensajeError = "La fecha de inicio o de culminacion no cumplen con el formato dd/mm/yyyy o alguno de estos esta vacio";
                    throw new ExcepcionTaller(mensajeError);
                }

                if(fechaInicio<dateNow || fechaCulminacion<dateNow){
                    error++;
                    mensajeError = "La fecha de inicio o de culminacion no pueden ser menores a la actual";
                    throw new ExcepcionTaller(mensajeError);
                }else if(fechaInicio==fechaCulminacion){
                    error++;
                    mensajeError = "La fecha de inicio o de culminacion no pueden ser iguales";
                    throw new ExcepcionTaller(mensajeError);
                }else if(fechaInicio>fechaCulminacion){
                    error++;
                    mensajeError = "La fecha de inicio no puede ser una fecha despues de la de culminacion";
                    throw new ExcepcionTaller(mensajeError);
                }else if(cotizacion.costo_reparacion<=0){
                    error++;
                    mensajeError = "El costo del precio de reparacion debe ser mayor a 0";
                    throw new ExcepcionTaller(mensajeError);
                }else if(traerAnalisis(_context,cotizacion.idAnalisis)==null){
                    error++;
                    mensajeError = "El analisis no existe";
                    throw new ExcepcionTaller(mensajeError);
                }else if(this.traerUsuarioTaller(_context,cotizacion.usuario_taller)==null){
                    error++;
                    mensajeError = "El usuario del taller no existe";
                    throw new ExcepcionTaller(mensajeError);
                }
                else
                {
                    crearCotizacionEntity(cotizacion);
                    var dataCotizacion = _context.Cotizaciones.Add(this.cotizacionEntity);
                    i=_context.DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionTaller(mensajeError);
            }
            return i;
        }

        public List<AnalisisConsultaDTO> ConsultarRequerimientosConOrdenPago(Guid id_taller)
        {
            try
            {
                var data = _context.Analisis.
                    Include(b => b.piezas).
                    Where(c => c.id_usuario_taller.Equals(id_taller)&&
                               c.estado==CheckEstadoAnalisisAccidente.Con_Orden)
                    .Select(b => new AnalisisConsultaDTO
                    {
                        titulo_analisis = b.titulo_analisis,
                        estado = b.estado.ToString()
                    }).ToList();
                return data;
            }
            catch (Exception e)
            {
                throw new ExcepcionTaller(mensajeError);
            }
        }

        public int AsosciarFaacturaConOrdenPago(Guid id_orden_pago,OrdenDePagoAsociarFacturaDTO facturaOrdenPago)
        {
            var i=0;
            var data =traerOrden(_context,id_orden_pago);
            try
            {
                if (data==null)
                {
                    error++;
                    mensajeError = "No existe la orden";
                    throw new ExcepcionTaller(mensajeError);
                }
                else
                {
                    data.factura=facturaOrdenPago.factura;
                    _context.OrdenesCompras.Update(data);
                    i=_context.DbContext.SaveChanges();   
                }
   
            }catch (Exception ex)
            {
                throw new ExcepcionTaller(mensajeError);
            }
            return i;
        }

        public int crearUsuarioTaller(crearUsuarioTallerDTO usuarioTaller,Guid idTaller){
            
            var i=0;
            try
            {
                if (validarExistenciaUsuarioTaller(_context,usuarioTaller) == true)
                {
                    error++;
                    mensajeError = "No se puede crear este usuario del taller porque ya existe";
                    throw new ExcepcionTaller(mensajeError);
                }
                if ((String.IsNullOrEmpty(usuarioTaller.primer_nombre)||validarEspaciosBlancos(usuarioTaller.primer_nombre)) ||
                    (String.IsNullOrEmpty(usuarioTaller.segundo_nombre)||validarEspaciosBlancos(usuarioTaller.segundo_nombre)) ||
                    (String.IsNullOrEmpty(usuarioTaller.primer_apellido)||validarEspaciosBlancos(usuarioTaller.primer_apellido)) ||
                    (String.IsNullOrEmpty(usuarioTaller.segundo_apellido)||validarEspaciosBlancos(usuarioTaller.segundo_apellido)) ||
                    (String.IsNullOrEmpty(usuarioTaller.direccion)||validarEspaciosBlancos(usuarioTaller.direccion)) || 
                    (String.IsNullOrEmpty(usuarioTaller.cargo)||validarEspaciosBlancos(usuarioTaller.cargo)) || 
                    (String.IsNullOrEmpty(usuarioTaller.contraseña)||validarEspaciosBlancos(usuarioTaller.contraseña)))
                {
                    error++;
                    mensajeError = "No se puede crar el usuaario taller si alguno de estos datos esta vacio: primer nombre,segundo nombre,primer apellido,segundo apellido,direccion,cargo,contraseña";
                    throw new ExcepcionTaller(mensajeError);
                }
                if(traerTallerA(_context,idTaller)==null){
                    error++;
                    mensajeError = "No se puede crar el usuaario taller si el taller no existe";
                    throw new ExcepcionTaller(mensajeError);
                }
                else
                {
                    crearUsuaarioTallerEntity(usuarioTaller,idTaller);
                    var data = _context.UsuariosTaller.Add(this.usuarioTaller);
                    i=_context.DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionTaller(mensajeError);
            }
            return i;
        }

        public int EliminarUsuarioTaller(Guid id_usuario_taller)
        {
            var i=0;
            try
            {
                var data =traerUsuarioTaller(_context,id_usuario_taller);
                if (data==null)
                {
                    error++;
                    mensajeError = "No existe el usuario del taller";
                    throw new ExcepcionTaller(mensajeError);
                }
                else
                {
                    data.estado=CheckEstadoUsuarioTaller.Bloqueado;
                    var a=_context.UsuariosTaller.Update(data);
                    i=_context.DbContext.SaveChanges();  
                }
   
            }catch (Exception ex)
            {
                throw new ExcepcionTaller(mensajeError);
            }
            return i;
        }

        public int ActualizarUsuarioTaller(ActualizarUsuarioTallerDTO tallerCambios,Guid id_usuario_taller)
        {
            var i=0;
            try
            {
                var data =traerUsuarioTaller(_context,id_usuario_taller);
                if (data==null)
                {
                    error++;
                    mensajeError = "No existe el usuario del taller";
                    throw new ExcepcionTaller(mensajeError);
                }
                else
                {
                    if(!(String.IsNullOrEmpty(tallerCambios.primer_nombre)))
                    {
                        if (!(validarEspaciosBlancos(tallerCambios.primer_nombre)))
                        {
                            data.primer_nombre = tallerCambios.primer_nombre;   
                        }
                    }
                    if(!(String.IsNullOrEmpty(tallerCambios.segundo_nombre)))
                    {
                        if (!(validarEspaciosBlancos(tallerCambios.segundo_nombre)))
                        {
                            data.segundo_nombre = tallerCambios.segundo_nombre;
                        }
                    }
                    if(!(String.IsNullOrEmpty(tallerCambios.primer_apellido)))
                    {
                        if (!(validarEspaciosBlancos(tallerCambios.primer_apellido)))
                        {
                            data.primer_apellido = tallerCambios.primer_apellido;   
                        }
                    }
                    if(!(String.IsNullOrEmpty(tallerCambios.segundo_apellido)))
                    {
                        if (!(validarEspaciosBlancos(tallerCambios.segundo_apellido)))
                        {
                            data.segundo_apellido = tallerCambios.segundo_apellido;   
                        }
                    }
                    if(!(String.IsNullOrEmpty(tallerCambios.direccion)))
                    {
                        if (!(validarEspaciosBlancos(tallerCambios.direccion)))
                        {
                            data.direccion = tallerCambios.direccion;   
                        }
                    }
                    if(!(String.IsNullOrEmpty(tallerCambios.cargo)))
                    {
                        if (!(validarEspaciosBlancos(tallerCambios.cargo)))
                        {
                            data.cargo = tallerCambios.cargo;   
                        }
                    }
                    if(!(String.IsNullOrEmpty(tallerCambios.email)))
                    {
                        if (!(validarEspaciosBlancos(tallerCambios.email)))
                        {
                            data.email = tallerCambios.email;   
                        }
                    }
                    if(!(String.IsNullOrEmpty(tallerCambios.contraseña)))
                    {
                        if (!(validarEspaciosBlancos(tallerCambios.contraseña)))
                        {
                            data.contraseña = tallerCambios.contraseña;   
                        }
                    }

                    if (tallerCambios.Telefonos != null)
                    {
                        if (tallerCambios.Telefonos.Count != 0)
                        {
                            foreach (var telefono in tallerCambios.Telefonos)
                            {
                                if (!AsignarTelefonoExistente(_context, telefono))
                                {
                                    var MC = new TelefonoEntity();
                                    MC.codigo_area = telefono.codigo_area;
                                    MC.numero_telefono = telefono.numero_telefono;
                                    MC.CreatedAt = DateTime.Now;
                                    MC.CreatedBy = null;
                                    MC.UpdatedAt = null;
                                    MC.UpdatedBy = null;
                                    listaTelefonos.Add(MC);
                                    _context.Telefonos.Add(MC);
                                    i = _context.DbContext.SaveChanges();
                                    listaTelefonos2.Add(MC);
                                }
                                else
                                {
                                    
                                    var telefonoExistente = _context.Telefonos
                                        .Where(b => b.numero_telefono.Equals(telefono.numero_telefono)).First();
                                    this.listaTelefonos2.Add(telefonoExistente);
                                }
                            }
                            data.Telefonos=this.listaTelefonos2;
                        }
                    }


                    if(String.IsNullOrEmpty(tallerCambios.id_taller.ToString())){
                        if(traerTallerA(_context,tallerCambios.id_taller)!=null){
                            data.taller=traerTallerA(_context,tallerCambios.id_taller);
                        }else
                        {
                            error++;
                            mensajeError = "No existe el talled";
                            throw new ExcepcionTaller(mensajeError);
                        }
                    }

                    _context.UsuariosTaller.Update(data);
                    i=_context.DbContext.SaveChanges();   
                }
   
            }catch (Exception ex)
            {
                throw new ExcepcionTaller(mensajeError);
            }
            return i;
        }
    }
}