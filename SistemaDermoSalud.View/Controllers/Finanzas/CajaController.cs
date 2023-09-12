using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Entities;
using System.Web.Mvc;
using SistemaDermoSalud.Helpers;

namespace MubaplastERP.Controllers.Finanzas
{
    public class CajaController : Controller
    {
        // GET: Caja
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }

        public string ObtenerDatos()
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            FN_CajaBL oFN_CajaBL = new FN_CajaBL();
            Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();
            //listas
            ResultDTO<Ma_MonedaDTO> lbeMa_MonedaDTO = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<FN_CajaDTO> lbeFN_CajaDTO = oFN_CajaBL.ListarxFecha(fechaInicio,fechaFin);
            //cadenas
            string idUsuario = eSEGUsuario.idUsuario.ToString();
            string nombreUsuario = eSEGUsuario.Usuario.ToString();
            string Periodo = DateTime.Now.Year.ToString();
            string Fecha = DateTime.Now.ToString().Substring(0, 10);
            string Nrocaja = oFN_CajaBL.NroCajaUltimo(1);
            string listaFN_Caja = Serializador.rSerializado(lbeFN_CajaDTO.ListaResultado, new string[] { "idCaja", "Descripcion", "FechaApertura", "FechaCierre", "MontoInicio", "MontoSaldo", "EstadoCaja" });
            string listaMa_Moneda = Serializador.rSerializado(lbeMa_MonedaDTO.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}↔{7}↔{8}↔{9}↔{10}",
                lbeFN_CajaDTO.Resultado, lbeFN_CajaDTO.MensajeError, listaFN_Caja, listaMa_Moneda, idUsuario, nombreUsuario, Periodo, Fecha, Nrocaja, fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"));
        }
        public string ObtenerPorFecha(string fechaInicio, string fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            FN_CajaBL oFN_CajaBL = new FN_CajaBL();
            Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();
            //listas
            ResultDTO<Ma_MonedaDTO> lbeMa_MonedaDTO = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<FN_CajaDTO> lbeFN_CajaDTO = oFN_CajaBL.ListarxFecha(Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin));
            //cadenas
            string listaFN_Caja = Serializador.rSerializado(lbeFN_CajaDTO.ListaResultado, new string[] { "idCaja", "Descripcion", "FechaApertura", "FechaCierre", "MontoInicio", "MontoSaldo", "EstadoCaja" });
            return String.Format("{0}↔{1}↔{2}",lbeFN_CajaDTO.Resultado, lbeFN_CajaDTO.MensajeError, listaFN_Caja);
        }
        public string ObtenerDatosxID(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_CajaBL oFN_CajaBL = new FN_CajaBL();
            FN_ConceptosCajaBL oFN_ConceptosCajaBL = new FN_ConceptosCajaBL();
            Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();
            FN_CajaDetalleBL oFN_CajaDetalleBL = new FN_CajaDetalleBL();
            //listas
            ResultDTO<FN_ConceptosCajaDTO> lbeFN_ConceptosCajaDTO = oFN_ConceptosCajaBL.ListarTodo();
            ResultDTO<Ma_MonedaDTO> lbeMa_MonedaDTO = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<FN_CajaDTO> oResultDTO = oFN_CajaBL.ListarxID(id);
            ResultDTO<FN_CajaDetalleDTO> lbeFN_CajaDetalleDTO = oFN_CajaDetalleBL.ListarxIDCaja(id);
            string listaCaja = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idCaja","CodigoGenerado","PeriodoAno","NroCaja","FechaApertura","FechaCierre","idMoneda","MontoInicio","Usuario",
                "MontoIngreso","MontoSalida","MontoSaldo","Moneda","EstadoCaja"});
            string listaCaja_Detalle = Serializador.rSerializado(lbeFN_CajaDetalleDTO.ListaResultado, new string[] { "idCajaDetalle","idCaja","DescripcionConcepto","SubTotalNacional",
                "IGVNacional","TotalNacional","TipoOperacion","idTipoPago","TipoPago","idTarjeta","Tarjeta"});
            string listaFN_ConceptosCaja = Serializador.rSerializado(lbeFN_ConceptosCajaDTO.ListaResultado, new string[] { "idConceptoCaja", "Descripcion", "AfectoIgv", "IngresoSalida" });
            string listaCajaDetallePDF = Serializador.rSerializado(lbeFN_CajaDetalleDTO.ListaResultado, new string[] { });

            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCaja, listaCaja_Detalle, listaFN_ConceptosCaja, listaCajaDetallePDF);
        }
        public string Grabar(FN_CajaDTO oFN_CajaDTO)
        {
            ResultDTO<FN_CajaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_CajaBL oFN_CajaBL = new FN_CajaBL();
            oFN_CajaDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            oFN_CajaDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oFN_CajaDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oFN_CajaDTO.Descripcion = "CAJA " + oFN_CajaDTO.PeriodoAno + "-" + oFN_CajaDTO.NroCaja;
            string Estado = oFN_CajaBL.EstadoCaja(oFN_CajaDTO.idCaja);
            oFN_CajaDTO.EstadoCaja = "A";
            //if (oFN_CajaDTO.Opcion != "1")
            //{
            //    if (Estado == "C")
            //    { oFN_CajaDTO.EstadoCaja = "A"; }
            //    else { oFN_CajaDTO.EstadoCaja = "C"; }
            //}
            //else { oFN_CajaDTO.EstadoCaja = "A"; }
            oResultDTO = oFN_CajaBL.UpdateInsert(oFN_CajaDTO);
            string listaFN_CajaCabecera = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idCaja", "Descripcion", "FechaApertura", "FechaCierre", "MontoInicio", "MontoSaldo", "EstadoCaja" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaFN_CajaCabecera);
        }
        public string CerrarCaja(FN_CajaDTO oFN_CajaDTO)
        {
            ResultDTO<FN_CajaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_CajaBL oFN_CajaBL = new FN_CajaBL();
            oResultDTO = oFN_CajaBL.CerrarCaja(oFN_CajaDTO);
            string listaFN_CajaCabecera = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idCaja", "Descripcion", "FechaApertura", "FechaCierre", "MontoInicio", "MontoSaldo", "EstadoCaja" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaFN_CajaCabecera);
        }
        public string Eliminar(FN_CajaDTO oFN_CajaDTO)
        {
            ResultDTO<FN_CajaDTO> oResultDTO = new ResultDTO<FN_CajaDTO>();
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_CajaBL oFN_CajaBL = new FN_CajaBL();
            string Estado = oFN_CajaBL.EstadoCaja(oFN_CajaDTO.idCaja);
            if (Estado == "A")
            {
                oResultDTO = oFN_CajaBL.Delete(oFN_CajaDTO);
            }
            else
            {
                oResultDTO.Resultado = "ERROR";
                oResultDTO.MensajeError = "No se puede eliminar Caja que ya se encuentra Cerrada";
            }
            string listaFN_CajaCabecera = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idCaja", "Descripcion", "FechaApertura", "FechaCierre", "MontoInicio", "MontoSaldo", "EstadoCaja" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaFN_CajaCabecera);
        }
        //DETALLE
        public string GrabarDetalle(FN_CajaDetalleDTO oFN_CajaDetalleDTO)
        {
            ResultDTO<FN_CajaDetalleDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_CajaBL oFN_CajaBL = new FN_CajaBL();
            FN_CajaDetalleBL oFN_CajaDetalleBL = new FN_CajaDetalleBL();
            oFN_CajaDetalleDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            oFN_CajaDetalleDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oFN_CajaDetalleDTO.idEmpresa = eSEGUsuario.idEmpresa;

            string Estado = oFN_CajaBL.EstadoCaja(oFN_CajaDetalleDTO.idCaja);
            if (Estado == "A")
            {
                oResultDTO = oFN_CajaDetalleBL.UpdateInsert(oFN_CajaDetalleDTO);
                ResultDTO<FN_CajaDetalleDTO> lstFN_CajaDetalleDTO = oFN_CajaDetalleBL.ListarxIDCaja(oFN_CajaDetalleDTO.idCaja);
                string listaFN_CajaDetalle = Serializador.rSerializado(lstFN_CajaDetalleDTO.ListaResultado, new string[] {  "idCajaDetalle","idCaja","DescripcionConcepto","SubTotalNacional",
                "IGVNacional","TotalNacional","TipoOperacion","idTipoPago","TipoPago"});
                return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaFN_CajaDetalle);
            }
            else
            {
                ResultDTO<FN_CajaDetalleDTO> lstFN_CajaDetalleDTO = oFN_CajaDetalleBL.ListarxIDCaja(oFN_CajaDetalleDTO.idCaja);
                string listaFN_CajaDetalle = Serializador.rSerializado(lstFN_CajaDetalleDTO.ListaResultado, new string[] {"idCajaDetalle","idCaja","DescripcionConcepto","SubTotalNacional",
                "IGVNacional","TotalNacional","TipoOperacion","idTipoPago","TipoPago"});
                return string.Format("{0}↔{1}↔{2}", "ERROR", "ERROR CAJA ESTA CERRADA", listaFN_CajaDetalle);
            }

        }
        public string EliminarDetalle(int idCaja, int idCajaDetalle)
        {
            ResultDTO<FN_CajaDetalleDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_CajaDetalleBL oFN_CajaDetalleBL = new FN_CajaDetalleBL();
            FN_CajaBL oFN_CajaBL = new FN_CajaBL();
            string Estado = oFN_CajaBL.EstadoCaja(idCaja);
            ResultDTO<FN_CajaDetalleDTO> lstFN_CajaDetalleDTO = new ResultDTO<FN_CajaDetalleDTO>();
            if (Estado == "A")
            {
                oResultDTO = oFN_CajaDetalleBL.Delete(idCajaDetalle, idCaja);
                lstFN_CajaDetalleDTO = oFN_CajaDetalleBL.ListarxIDCaja(idCaja);
                string listaFN_CajaDetalle = Serializador.rSerializado(lstFN_CajaDetalleDTO.ListaResultado, new string[] {  "idCajaDetalle","idCaja","DescripcionConcepto","SubTotalNacional",
                "IGVNacional","TotalNacional","TipoOperacion","idTipoPago","TipoPago"});
                return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaFN_CajaDetalle);
            }
            else
            {
                lstFN_CajaDetalleDTO = oFN_CajaDetalleBL.ListarxIDCaja(idCaja);
                string listaFN_CajaDetalle = Serializador.rSerializado(lstFN_CajaDetalleDTO.ListaResultado, new string[] {  "idCajaDetalle","idCaja","DescripcionConcepto","SubTotalNacional",
                "IGVNacional","TotalNacional","TipoOperacion","idTipoPago","TipoPago"});
                return string.Format("{0}↔{1}↔{2}", "ERROR", "ERROR CAJA ESTA CERRADA", listaFN_CajaDetalle);
            }



        }
        //
        public string ListaConceptosCaja()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_ConceptosCajaBL oFN_ConceptosCajaBL = new FN_ConceptosCajaBL();
            ResultDTO<FN_ConceptosCajaDTO> lstFN_ConceptosCajaDTO = oFN_ConceptosCajaBL.ListarTodo();
            string listaConceptos = Serializador.rSerializado(lstFN_ConceptosCajaDTO.ListaResultado, new string[] { "idConceptoCaja", "Descripcion", "AfectoIgv", "IngresoSalida" });
            return String.Format("{0}↔{1}↔{2}", lstFN_ConceptosCajaDTO.Resultado, lstFN_ConceptosCajaDTO.MensajeError, listaConceptos);
        }
        public string ListaCitas()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            ResultDTO<CitasDTO> lstCitasDTO = oCitasBL.ListarCitasXPagar();
            string listaCitas = Serializador.rSerializado(lstCitasDTO.ListaResultado, new string[] { "idCita", "FechaCita", "Codigo", "Paciente", "Pago", "Observaciones" });
            return String.Format("{0}↔{1}↔{2}", lstCitasDTO.Resultado, lstCitasDTO.MensajeError, listaCitas);
        }
        public string ListaEmpleados()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            PersonalBL oRH_PersonalBL = new PersonalBL();
            ResultDTO<PersonalDTO> lstPersonalDTO = oRH_PersonalBL.ListarTodo();
            string listaEmpleados = Serializador.rSerializado(lstPersonalDTO.ListaResultado, new string[] { "idPersonal", "NombreCompleto" });
            return String.Format("{0}↔{1}↔{2}", lstPersonalDTO.Resultado, lstPersonalDTO.MensajeError, listaEmpleados);
        }
        public string ListaBancos()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_BancoBL oMa_BancoBL = new Ma_BancoBL();
            ResultDTO<Ma_BancoDTO> lstMa_BancoDTO = oMa_BancoBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            string listaBanco = Serializador.rSerializado(lstMa_BancoDTO.ListaResultado, new string[] { "idBanco", "Descripcion" });
            return String.Format("{0}↔{1}↔{2}", lstMa_BancoDTO.Resultado, lstMa_BancoDTO.MensajeError, listaBanco);
        }
        public string ListaCompras()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            COM_DocumentoCompraBL oCOM_DocumentoCompraBL = new COM_DocumentoCompraBL();
            ResultDTO<COM_DocumentoCompraDTO> lstCOM_DocumentoCompraDTO = oCOM_DocumentoCompraBL.ListarComprasXPagar(eSEGUsuario.idEmpresa);
            string listaCompras = Serializador.rSerializado(lstCOM_DocumentoCompraDTO.ListaResultado, new string[] { "idDocumentoCompra", "ProveedorDocumento", "ProveedorRazon", "NumDocumento", "MontoxPagar" });
            return String.Format("{0}↔{1}↔{2}", lstCOM_DocumentoCompraDTO.Resultado, lstCOM_DocumentoCompraDTO.MensajeError, listaCompras);
        }
        public string ListaVentas()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VEN_DocumentoVentaBL oVN_DocumentoVentaBL = new VEN_DocumentoVentaBL();
            ResultDTO<VEN_DocumentoVentaDTO> lstVEN_DocumentoVentaDTO = oVN_DocumentoVentaBL.ListarVentasxCobrar(eSEGUsuario.idEmpresa);
            string listaVentas = Serializador.rSerializado(lstVEN_DocumentoVentaDTO.ListaResultado, new string[] { "idDocumentoVenta", "ClienteDocumento", "ClienteRazon", "NumDocumento", "MontoxCobrar" });
            return String.Format("{0}↔{1}↔{2}", lstVEN_DocumentoVentaDTO.Resultado, lstVEN_DocumentoVentaDTO.MensajeError, listaVentas);
        }
        public string ObtenerDatosCajaDetallexID(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_CajaDetalleBL oFN_CajaDetalleBL = new FN_CajaDetalleBL();
            FN_ConceptosCajaBL oFN_ConceptosCajaBL = new FN_ConceptosCajaBL();
            RH_EmpleadoBL oRH_EmpleadoBL = new RH_EmpleadoBL();
            Ma_BancoBL oMa_BancoBL = new Ma_BancoBL();
            //listas
            ResultDTO<FN_CajaDetalleDTO> lbeFN_CajaDetalleDTO = oFN_CajaDetalleBL.ListarxIDCajaDetalle(id);
            ResultDTO<FN_ConceptosCajaDTO> lbeFN_ConceptosCajaDTO = oFN_ConceptosCajaBL.ListarTodo();
            ResultDTO<RH_EmpleadoDTO> lbeRH_EmpleadoDTO = oRH_EmpleadoBL.ListarTodo();
            ResultDTO<Ma_BancoDTO> lbeMa_BancoDTO = oMa_BancoBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            string listaCaja_Detalle = Serializador.rSerializado(lbeFN_CajaDetalleDTO.ListaResultado, new string[] { "idCajaDetalle","idConcepto","TipoOperacion","idProvCliEmpl","SerieDcto","NroDcto",
                "SerieRecibo","NroOperacion","NroRecibo","idTarjeta","idTipoDocumento","idCompraVenta","NombreProvCliEmpl","Ruc","MontoPendiente","Observaciones","SubTotalNacional","IGVNacional","TotalNacional","idTipoPago","TipoPago","Documento","idTarjeta","Tarjeta" });
            string listaConceptos = Serializador.rSerializado(lbeFN_ConceptosCajaDTO.ListaResultado, new string[] { "idConceptoCaja", "Descripcion", "AfectoIgv", "IngresoSalida" });
            string listaEmpleados = Serializador.rSerializado(lbeRH_EmpleadoDTO.ListaResultado, new string[] { "idEmpleado", "NombreCompleto" });
            string listaBancos = Serializador.rSerializado(lbeMa_BancoDTO.ListaResultado, new string[] { "idBanco", "Descripcion" });
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}", lbeFN_CajaDetalleDTO.Resultado, lbeFN_CajaDetalleDTO.MensajeError, listaCaja_Detalle, listaConceptos, listaEmpleados, listaBancos);
        }
        public int ValidarCajaAperturada()
        {
            FN_CajaBL oFN_CajaBL=new FN_CajaBL();
           return oFN_CajaBL.ValidarCajaAperturada();
        }
    }
}