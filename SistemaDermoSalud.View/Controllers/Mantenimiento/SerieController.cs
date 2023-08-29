using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Helpers;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Business.Mantenimiento;

namespace SistemaDermoSalud.View.Controllers.Mantenimiento
{
    public class SerieController : Controller
    {
        // GET: Serie
        public ActionResult Index()
        {
            if (Session["Config"] != null)
            {
                return PartialView();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public string ObtenerDatos()
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_TipoComprobanteBL oMa_TipoComprobanteBL = new Ma_TipoComprobanteBL();
            Ma_SerieBL oMa_SerieBL = new Ma_SerieBL();
            ResultDTO<VEN_SerieDTO> oListaSerie = oMa_SerieBL.ListarTodo();
            ResultDTO<Ma_TipoComprobanteDTO> oListaComprobantes = oMa_TipoComprobanteBL.ListarTodo();
            string listaSerie = Serializador.rSerializado(oListaSerie.ListaResultado, new string[]{"idSerie", "Descripcion", "NroSerie", "NumeroDoc" ,"FechaCreacion"});
            string listaComprobantes = Serializador.rSerializado(oListaComprobantes.ListaResultado, new string[] { "idTipoComprobante", "Descripcion" });
            return String.Format("{0}↔{1}↔{2}","OK", listaSerie, listaComprobantes);
        }
        public string SeriesParaDocVentas()
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_SerieBL oMa_SerieBL = new Ma_SerieBL();
            ResultDTO<VEN_SerieDTO> oListaSerie = oMa_SerieBL.ListarRangoFecha(eSEGUsuario.idEmpresa, fechaInicio.ToString("dd-MM-yyyy") + "", fechaFin.ToString("dd-MM-yyyy") + "");
            string listaSerie = Serializador.rSerializado(oListaSerie.ListaResultado, new string[]
             {"idSerie","Descripcion", "NroSerie"});

            Ma_TipoComprobanteBL oMa_TipoComprobanteBL = new Ma_TipoComprobanteBL();
            ResultDTO<Ma_TipoComprobanteDTO> oListaComprobantes = oMa_TipoComprobanteBL.ListarTodo();
            string listaComprobantes = Serializador.rSerializado(oListaComprobantes.ListaResultado, new string[] { "idTipoComprobante", "Descripcion" });

            return String.Format("{0}↔{1}↔{2}", oListaSerie.Resultado, oListaSerie.MensajeError, listaSerie);
        }
        public string ObtenerPorFecha(string fechaInicio, string fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_SerieBL oMa_SerieBL = new Ma_SerieBL();
            ResultDTO<VEN_SerieDTO> oListaSerie = oMa_SerieBL.ListarRangoFecha(eSEGUsuario.idEmpresa, fechaInicio, fechaFin);
            string listaSerie = Serializador.rSerializado(oListaSerie.ListaResultado, new string[]
             {"idSerie", "NroSerie", "Descripcion", "FechaModificacion","Estado"});
            return String.Format("{0}↔{1}↔{2}", oListaSerie.Resultado, oListaSerie.MensajeError, listaSerie);
        }
        public string Grabar(VEN_SerieDTO oVEN_SerieDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<VEN_SerieDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_SerieBL oMa_SerieBL = new Ma_SerieBL();
            if (oVEN_SerieDTO.idSerie == 0)
            {
                oVEN_SerieDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oVEN_SerieDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oMa_SerieBL.UpdateInsert(oVEN_SerieDTO, fechaInicio, fechaFin);

            string listaSerie = Serializador.rSerializado(oResultDTO.ListaResultado, new string[]
            {"idSerie", "Descripcion", "NroSerie", "NumeroDoc" ,"FechaCreacion"}); ;
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaSerie);
        }
        public string Eliminar(VEN_SerieDTO oVEN_SerieDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<VEN_SerieDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_SerieBL oMa_SerieBL = new Ma_SerieBL();

            oResultDTO = oMa_SerieBL.Delete(oVEN_SerieDTO, fechaInicio, fechaFin);
            string listaSerie = Serializador.rSerializado(oResultDTO.ListaResultado, new string[]
                {"idSerie", "Descripcion", "NroSerie", "NumeroDoc" ,"FechaCreacion"});
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaSerie);
        }
        public string ObtenerDatosxID(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            Ma_SerieBL oMa_SerieBL = new Ma_SerieBL();
            ResultDTO<VEN_SerieDTO> oVEN_Serie = oMa_SerieBL.ListarxID(id);
            string listaSerie = Serializador.rSerializado(oVEN_Serie.ListaResultado, new string[] { });
            return String.Format("{0}↔{1}", "OK", listaSerie);
        }
        public string ObtenerNumeroVenta(int idSerie, string serie)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            Ma_SerieBL oMa_SerieBL = new Ma_SerieBL();
            ResultDTO<Ma_SerieDTO> oListaNumero = oMa_SerieBL.ObtenerNumeroVenta(serie);
            ResultDTO<VEN_SerieDTO> oListaSerie = oMa_SerieBL.ListarxID(idSerie);
            string listaNumero = Serializador.rSerializado(oListaNumero.ListaResultado, new string[] { "NumeroSerie" });
            string listaSerie = Serializador.rSerializado(oListaSerie.ListaResultado, new string[] { "idTipoComprobante" });
            return String.Format("{0}↔{1}", listaNumero, listaSerie);
        }

    }
}