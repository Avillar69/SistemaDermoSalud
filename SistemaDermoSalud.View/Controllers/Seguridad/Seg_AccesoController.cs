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
    public class Seg_AccesoController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }
        public string ObtenerDatos()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Seg_MenuBL oSeg_MenuBL = new Seg_MenuBL();
            Seg_RolBL oSeg_RolBL = new Seg_RolBL();
            ResultDTO<Seg_MenuDTO> oResultDTO = oSeg_MenuBL.ListarTodo(eSEGUsuario.idEmpresa);
            string listaSeg_Menu = "";
            ResultDTO<Seg_RolDTO> oResultRolDTO = oSeg_RolBL.ListarTodo(eSEGUsuario.idEmpresa);
            string listaSeg_Rol = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaSeg_Menu = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { "idMenu", "idMenuPadre", "Descripcion", "Nivel", "Posicion" }, false);
            }
            if (oResultRolDTO.ListaResultado != null && oResultRolDTO.ListaResultado.Count > 0)
            {
                listaSeg_Rol = Serializador.Serializar(oResultRolDTO.ListaResultado, '▲', '▼', new string[] { "idRol", "Descripcion" }, false);
            }
            return String.Format("{0}↔{1}", listaSeg_Menu, listaSeg_Rol);
        }
        public string bAc(int bAc)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            string lista_Accesos = "";
            Seg_AccesoBL oSeg_AccesosBL = new Seg_AccesoBL();
            ResultDTO<Seg_AccesoDTO> oResultDTO = oSeg_AccesosBL.ListarxRol(bAc, eSEGUsuario.idEmpresa);

            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                lista_Accesos = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { "idMenu" }, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, lista_Accesos);
        }
        public string gAc(string cad, int iR)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Seg_AccesoBL oSeg_AccesosBL = new Seg_AccesoBL();
            ResultDTO<Seg_AccesoDTO> oResultDTO = oSeg_AccesosBL.UpdateInsert(cad, eSEGUsuario.idEmpresa, iR);
            return String.Format("{0}↔{1}", oResultDTO.Resultado, oResultDTO.MensajeError);
        }
    }
}
