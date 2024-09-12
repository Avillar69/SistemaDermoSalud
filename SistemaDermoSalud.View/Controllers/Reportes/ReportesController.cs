using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Business.Mantenimiento;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Entities.Nubefact;
using SistemaDermoSalud.Helpers;

namespace SistemaDermoSalud.View.Controllers.Reportes
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult RegistroVentas()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public ActionResult ReporteVentas()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public ActionResult ReporteGastosxDia()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }


        public string ObtenerDatos_RegistroVenta()
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
           
            return String.Format("{0}↔{1}↔{2}", "OK",  fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"));
        }
        public string ObtenerPorFecha_RegistroVentas(string fechaInicio, string fechaFin, int idTipoDctos)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
            ResultDTO<VEN_DocumentoVentaDTO> oListaDocumentoVentas = new ResultDTO<VEN_DocumentoVentaDTO>();
            //string[] TipoDctos = { "1", "2", "3" };
            //string[] Dctos = { "true", "true", "true" };
            //for (int i = 0; i < Dctos.Length; i++)
            //{
            //    if (Dctos[i] == Documentos.Split('-')[i])
            //    {
            //        oListaDocumentoVentas = oVN_DocumentoVentaBL.ListarRegistroVenta(oListaDocumentoVentas,eSEGUsuario.idEmpresa, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin), TipoDctos[i]);                   
            //    }
            //}
            oListaDocumentoVentas = oVN_DocumentoVentaBL.ListarRegistroVenta(eSEGUsuario.idEmpresa, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin), idTipoDctos);

            string listaDocumentoVenta = Serializador.rSerializado(oListaDocumentoVentas.ListaResultado, new string[]
            { "idDocumentoVenta", "FechaDocumento", "SerieDocumento", "NumDocumento", "ClienteRazon", "SubTotalNacional", "IGVNacional", "TotalNacional", "Estado", "Enlace" });
            
            return String.Format("{0}↔{1}↔{2}", oListaDocumentoVentas.Resultado, oListaDocumentoVentas.MensajeError, listaDocumentoVenta);
        }

        public string ReporteVenta_Fecha(string fechaInicio, string fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VEN_DocumentoVentaBL oVEN_DocumentoVentaBL = new VEN_DocumentoVentaBL();
            ResultDTO<VEN_DocumentoVentaDetalleDTO> oListaReporteVentas = new ResultDTO<VEN_DocumentoVentaDetalleDTO>();
            //string[] TipoDctos = { "1", "2", "3" };
            //string[] Dctos = { "true", "true", "true" };
            //for (int i = 0; i < Dctos.Length; i++)
            //{
            //    if (Dctos[i] == Documentos.Split('-')[i])
            //    {
            //        oListaDocumentoVentas = oVN_DocumentoVentaBL.ListarRegistroVenta(oListaDocumentoVentas,eSEGUsuario.idEmpresa, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin), TipoDctos[i]);                   
            //    }
            //}
            oListaReporteVentas = oVEN_DocumentoVentaBL.ReporteVentaFecha(Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin));

            string ListaReporteVentas = Serializador.rSerializado(oListaReporteVentas.ListaResultado, new string[]
            { "idArticulo", "Laboratorio", "DescripcionArticulo", "Cantidad", "PrecioMedicamento", "TotalNacional"});

            return String.Format("{0}↔{1}↔{2}", oListaReporteVentas.Resultado, oListaReporteVentas.MensajeError, ListaReporteVentas);
        }



        public string ReporteVenta_Fecha_Medicamento(string fechaInicio, string fechaFin, string idMedicamento)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VEN_DocumentoVentaBL oVEN_DocumentoVentaBL = new VEN_DocumentoVentaBL();
            ResultDTO<VEN_DocumentoVentaDetalleDTO> oListaReporteVentas = new ResultDTO<VEN_DocumentoVentaDetalleDTO>();
            oListaReporteVentas = oVEN_DocumentoVentaBL.ReporteVentaFechaMedicamento(Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin), Convert.ToInt32(idMedicamento));

            string ListaReporteVentas = Serializador.rSerializado(oListaReporteVentas.ListaResultado, new string[]
            { "FechaCreacion", "Factura", "Cliente", "DescripcionArticulo", "Cantidad", "PrecioMedicamento","TotalNacional"});

            return String.Format("{0}↔{1}↔{2}", oListaReporteVentas.Resultado, oListaReporteVentas.MensajeError, ListaReporteVentas);
        }

    }
}