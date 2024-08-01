using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Helpers;

namespace SistemaDermoSalud.Controllers
{
    public class MonedaController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Config"] != null) {
                return PartialView();
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }
        public string ObtenerDatos()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();
            ResultDTO<Ma_MonedaDTO> oResultDTO = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            string listaMa_Moneda = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaMa_Moneda = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                "idMoneda", "CodigoGenerado", "Descripcion", "FechaModificacion", "Estado"}, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_Moneda);
        }
        public string ObtenerDatosxID(int id)
        {
            MonedaBL oMonedaBL = new MonedaBL();
            ResultDTO<MonedaDTO> oResultDTO = oMonedaBL.ListarxID(id);
            string listaMA_Moneda = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaMA_Moneda = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { }, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMA_Moneda);
        }
        public string Grabar(Ma_MonedaDTO oMonedaDTO)
        {
            ResultDTO<Ma_MonedaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_MonedaBL oMA_MonedaBL = new Ma_MonedaBL();
            string listaMA_Moneda = "";
            if(oMonedaDTO.idMoneda == 0)
            {
                oMonedaDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oMonedaDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oMA_MonedaBL.UpdateInsert(oMonedaDTO);
            
            List<Ma_MonedaDTO> lstMA_MonedaDTO = oResultDTO.ListaResultado;
            if (lstMA_MonedaDTO != null && lstMA_MonedaDTO.Count > 0)
            {
                listaMA_Moneda = Serializador.Serializar(lstMA_MonedaDTO,'▲', '▼', new string[] { "idMoneda", "CodigoGenerado", "Descripcion", "FechaModificacion",  "Estado" },false);
            }
            return string.Format("{0}↔{1}↔{2}",oResultDTO.Resultado, oResultDTO.MensajeError, listaMA_Moneda);
        }
        public string Eliminar(Ma_MonedaDTO oMonedaDTO)
        {
            ResultDTO<Ma_MonedaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_MonedaBL oMA_MonedaBL = new Ma_MonedaBL();
            string listaMA_Moneda = "";
            oResultDTO = oMA_MonedaBL.Delete(oMonedaDTO);
            List<Ma_MonedaDTO> lstMA_MonedaDTO = oResultDTO.ListaResultado;
            if (lstMA_MonedaDTO != null && lstMA_MonedaDTO.Count > 0)
            {
                listaMA_Moneda = Serializador.Serializar(lstMA_MonedaDTO,'▲', '▼', new string[] { "idMoneda", "CodigoGenerado", "Descripcion",
                    "FechaModificacion","Estado" },false);
            }
            return string.Format("{0}↔{1}↔{2}",oResultDTO.Resultado, oResultDTO.MensajeError, listaMA_Moneda);
        }
    }
}
