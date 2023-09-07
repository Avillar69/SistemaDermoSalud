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
    public class Seg_RolController : Controller
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
            Seg_RolBL oSeg_RolBL = new Seg_RolBL();
            ResultDTO<Seg_RolDTO> oResultDTO = oSeg_RolBL.ListarTodo(eSEGUsuario.idEmpresa);
            string listaSeg_Rol = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaSeg_Rol = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { "idRol", "Descripcion", "FechaModificacion", "Estado" }, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaSeg_Rol);
        }
        public string ObtenerDatosxID(int id)
        {
            Seg_RolBL oSeg_RolBL = new Seg_RolBL();
            ResultDTO<Seg_RolDTO> oResultDTO = oSeg_RolBL.ListarxID(id);
            string listaSeg_Rol = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaSeg_Rol = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { }, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaSeg_Rol);
        }

        public string Grabar(Seg_RolDTO oSeg_RolDTO)
        {
            ResultDTO<Seg_RolDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Seg_RolBL oSeg_RolBL = new Seg_RolBL();
            string listaSeg_Rol = "";
            if (oSeg_RolDTO.idRol == 0)
            {
                oSeg_RolDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oSeg_RolDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oSeg_RolDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oResultDTO = oSeg_RolBL.UpdateInsert(oSeg_RolDTO);

            List<Seg_RolDTO> lstSeg_RolDTO = oResultDTO.ListaResultado;
            if (lstSeg_RolDTO != null && lstSeg_RolDTO.Count > 0)
            {
                listaSeg_Rol = Serializador.Serializar(lstSeg_RolDTO, '▲', '▼', new string[] { "idRol", "Descripcion", "FechaModificacion", "Estado" }, false);
            }
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaSeg_Rol);
        }

        public string Eliminar(Seg_RolDTO oSeg_RolDTO)
        {
            ResultDTO<Seg_RolDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            oSeg_RolDTO.idEmpresa = eSEGUsuario.idEmpresa;
            Seg_RolBL oSeg_RolBL = new Seg_RolBL();
            string listaSeg_Rol = "";
            oResultDTO = oSeg_RolBL.Delete(oSeg_RolDTO);
            List<Seg_RolDTO> lstSeg_RolDTO = oResultDTO.ListaResultado;
            if (lstSeg_RolDTO != null && lstSeg_RolDTO.Count > 0)
            {
                listaSeg_Rol = Serializador.Serializar(lstSeg_RolDTO, '▲', '▼', new string[] { "idRol", "Descripcion", "FechaModificacion", "Estado" }, false);
            }
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaSeg_Rol);
        }
    }
}
