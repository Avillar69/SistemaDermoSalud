using SistemaDermoSalud.Business;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                ObjSesionDTO objConfig = (ObjSesionDTO)Session["Config"];
                //roles administrativos
                List<int> listaIdRolDashboad = new List<int> { 1, 2, 6 };
                if (!listaIdRolDashboad.Contains(objConfig.SessionUsuario.idRol))
                    return PartialView("~/Views/Home/DashBoardOtros.cshtml");
                return PartialView();
            }
        }
        public ActionResult Login()
        {
            if (Session["Config"] != null) return RedirectToAction("Index", "Home"); ;
            return View();
        }
        public bool KeepActiveSession()
        {
            if (Session["Config"] != null)
                return true;
            else
                return false;

        }

        public string DashboardGeneral()
        {
            DashboardBL oDashboardBL = new DashboardBL();
            VEN_DocumentoVentaBL oVEN_DocumentoVentaBL = new VEN_DocumentoVentaBL();
            ResultDTO<DashboardDTO> lstDashboard = oDashboardBL.ListarTodo();
            ResultDTO<VEN_DocumentoVentaDTO> oListaDocumentoVentas = oVEN_DocumentoVentaBL.ListarVentasDashboard();
            string listaDashboard = Serializador.rSerializado(lstDashboard.ListaResultado, new string[] { });
            string listaCompras = "";// Serializador.rSerializado(lstDashboard.ListaResultado[0].listaCompras, new string[] { });
            string listasVentas = "";
            string listaCitas = "";

            string listaVentasDoc = "";
            string listaComprasDoc = "";

            if (lstDashboard.ListaResultado.Count>0)
            {
                listasVentas = Serializador.rSerializado(lstDashboard.ListaResultado[0].listaVentas, new string[] { });
                listaCitas = Serializador.rSerializado(lstDashboard.ListaResultado[0].listaCitas, new string[] { });
                listaComprasDoc = Serializador.rSerializado(lstDashboard.ListaResultado[0].listaCompraDoc, new string[] {
                "FechaDocumento", "SerieDocumento", "ProveedorRazon", "MonedaDesc", "TotalNacional" });
                listaVentasDoc = Serializador.rSerializado(oListaDocumentoVentas.ListaResultado, new string[] {
                "FechaDocumento", "SerieDocumento", "ClienteRazon", "MonedaDesc", "TotalNacional" });
            }

            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}", listaDashboard, listaCompras, listasVentas, listaCitas, listaVentasDoc, listaComprasDoc);
        }
        public string DashboardCircular()
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            DashboardBL oDashboardBL = new DashboardBL();
            ResultDTO<TipoServicioDTO> lstDashboard = oDashboardBL.ListarTodoCirculo(fechaInicio, fechaFin);
            string listaDashboard = Serializador.rSerializado(lstDashboard.ListaResultado, new string[] { "idTipoServicio", "cantServicio", "NombreTipoServicio" });
            return String.Format("{0}", listaDashboard);
        }
        public string Log(string Usuario, string Password)
        {
            string respuesta = "";
            try
            {
                Seg_UsuarioBL oSeg_UsuarioBL = new Seg_UsuarioBL();
                Seg_MenuBL oSeg_MenuBL = new Seg_MenuBL();
                ResultDTO<Seg_UsuarioDTO> oRespuesta = oSeg_UsuarioBL.ValidarLogin(Usuario, Password);
                //List<Seg_MenuDTO> ListaMenuNivel1 = new List<Seg_MenuDTO>();
                //List<Seg_MenuDTO> ListaMenuNivel2 = new List<Seg_MenuDTO>();
                //List<Seg_MenuDTO> ListaMenuNivel3 = new List<Seg_MenuDTO>();
                if (oRespuesta.ListaResultado.Count > 0)
                {
                    ObjSesionDTO objConfig = new ObjSesionDTO();
                    Seg_UsuarioDTO objUsuario = oRespuesta.ListaResultado[0];
                    objConfig.SessionUsuario = objUsuario;
                    List<Seg_MenuDTO> ListaMenuRol = oSeg_MenuBL.ListarMenuxRol(1, objUsuario.idRol).ListaResultado;
                    objConfig.SessionListaMenu = ListaMenuRol;
                    //ListaMenuNivel1 = (from obj in ListaMenuRol where obj.Nivel == 1 select obj).ToList();
                    Session["Config"] = objConfig;
                    respuesta = "OK";
                }
                else
                {
                    respuesta = "Usuario/contraseña incorrectos";
                }

            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            return respuesta;

        }
        public ActionResult Logout()
        {
            Session["Config"] = null;
            return RedirectToAction("Login", "Home");
        }
    }
}