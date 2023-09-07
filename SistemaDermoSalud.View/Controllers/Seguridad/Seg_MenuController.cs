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
    public class Seg_MenuController : Controller
    {
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
            Seg_MenuBL oSeg_MenuBL = new Seg_MenuBL();
            ResultDTO<Seg_MenuDTO> oResultDTO = oSeg_MenuBL.ListarTodo(eSEGUsuario.idEmpresa);
            string listaSeg_Menu = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaSeg_Menu = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { "idMenu", "Descripcion", "Action", "Controller", "FechaModificacion", "Estado" }, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaSeg_Menu);
        }
        public string ObtenerDatosxID(int id)
        {
            Seg_MenuBL oSeg_MenuBL = new Seg_MenuBL();
            ResultDTO<Seg_MenuDTO> oResultDTO = oSeg_MenuBL.ListarxID(id);
            string listaSeg_Menu = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaSeg_Menu = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { }, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaSeg_Menu);
        }

        public string Grabar(Seg_MenuDTO oSeg_MenuDTO)
        {
            ResultDTO<Seg_MenuDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Seg_MenuBL oSeg_MenuBL = new Seg_MenuBL();
            string listaSeg_Menu = "";
            if (oSeg_MenuDTO.idMenu == 0)
            {
                oSeg_MenuDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oSeg_MenuDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oSeg_MenuDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oResultDTO = oSeg_MenuBL.UpdateInsert(oSeg_MenuDTO);

            List<Seg_MenuDTO> lstSeg_MenuDTO = oResultDTO.ListaResultado;
            if (lstSeg_MenuDTO != null && lstSeg_MenuDTO.Count > 0)
            {
                listaSeg_Menu = Serializador.Serializar(lstSeg_MenuDTO, '▲', '▼', new string[] { }, false);
            }
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaSeg_Menu);
        }

        public string Eliminar(Seg_MenuDTO oSeg_MenuDTO)
        {
            ResultDTO<Seg_MenuDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Seg_MenuBL oSeg_MenuBL = new Seg_MenuBL();
            string listaSeg_Menu = "";
            oResultDTO = oSeg_MenuBL.Delete(oSeg_MenuDTO);
            List<Seg_MenuDTO> lstSeg_MenuDTO = oResultDTO.ListaResultado;
            if (lstSeg_MenuDTO != null && lstSeg_MenuDTO.Count > 0)
            {
                listaSeg_Menu = Serializador.Serializar(lstSeg_MenuDTO, '▲', '▼', new string[] { }, false);
            }
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaSeg_Menu);
        }
    }
}
