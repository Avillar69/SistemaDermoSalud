using SistemaDermoSalud.Business;
using SistemaDermoSalud.Business.Compras;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Compras;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers.Compras
{
    public class ReportePagarController : Controller
    {
        // GET: ReportePagar
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
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();
            ResultDTO<Ma_MonedaDTO> oListaMoneda = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            DateTime fechaFin = DateTime.Today;
            COM_PagaSocioBL oCOM_PagaSocioBL = new COM_PagaSocioBL();
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            string listaMoneda = Serializador.rSerializado(oListaMoneda.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            ResultDTO<AD_SocioNegocioDTO> oListaSocios = oAD_SocioNegocioBL.ListarProv(eSEGUsuario.idEmpresa, "P");
            ResultDTO<COM_PagaSocioDTO> oListaOrdenPago = oCOM_PagaSocioBL.ListarTodo();
            string listaSocios = Serializador.rSerializado(oListaSocios.ListaResultado, new string[] { "idSocioNegocio", "RazonSocial", "Documento" });
            string listaOrdenCompra = Serializador.rSerializado(oListaOrdenPago.ListaResultado, new string[]
            {  "TipoDoc","idDocumento", "DescripcionSocial", "MontoTotal", "MontoAplicado", "MontoXPagar"});
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}", "OK", listaOrdenCompra, fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"), listaSocios, listaMoneda);
        }






    }
}