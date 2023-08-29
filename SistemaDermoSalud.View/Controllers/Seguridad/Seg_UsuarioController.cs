using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Helpers;
using SistemaDermoSalud.Business.Mantenimiento;

namespace SistemaDermoSalud.Controllers
{
    public class Seg_UsuarioController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }
        public string ObtenerDatos()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Seg_UsuarioBL oSeg_UsuarioBL = new Seg_UsuarioBL();
            Seg_RolBL oSeg_RolBL = new Seg_RolBL();
            Ma_LocalBL oMa_LocalBL = new Ma_LocalBL();
            List<Seg_UsuarioDTO> lst_UsuarioDTO = oSeg_UsuarioBL.ListarTodo(eSEGUsuario.idEmpresa);
            List<Seg_RolDTO> lst_RolDTO = oSeg_RolBL.ListarTodo(eSEGUsuario.idEmpresa).ListaResultado;
            List<Ma_LocalDTO> lstMa_LocalDTO = oMa_LocalBL.ListarTodo(eSEGUsuario.idEmpresa).ListaResultado;
            string listaUsuarios = "";
            string listaRoles = "";
            string listaLocal = "";
            if (lst_UsuarioDTO != null && lst_UsuarioDTO.Count > 0)
            {
                listaUsuarios = Serializador.Serializar(lst_UsuarioDTO, '▲', '▼', new string[] { "idUsuario", "CodigoGenerado", "RolDescripcion", "Usuario", "FechaModificacion", "UsuarioModificacionDescripcion", "Estado" }, false);
            }
            if (lst_RolDTO != null && lst_RolDTO.Count > 0)
            {
                listaRoles = Serializador.Serializar(lst_RolDTO, '▲', '▼', new string[] { "idRol", "Descripcion" }, false);
            }
            if (lstMa_LocalDTO != null && lstMa_LocalDTO.Count > 0)
            {
                listaLocal = Serializador.Serializar(lstMa_LocalDTO, '▲', '▼', new string[] { "idLocal", "CodigoGenerado", "Descripcion", "Direccion" }, false);
            }
            return string.Format("{0}↔{1}↔{2}", listaUsuarios, listaRoles, listaLocal);
        }
        public string ObtenerDatosxID(int id)
        {

            Seg_UsuarioBL oSeg_UsuarioBL = new Seg_UsuarioBL();
            List<Seg_UsuarioDTO> lst_UsuarioDTO = oSeg_UsuarioBL.ListarxID(id);

            string listaUsuario = "";
            string Locales = "";
            if (lst_UsuarioDTO != null && lst_UsuarioDTO.Count > 0)
            {
                listaUsuario = Serializador.Serializar(lst_UsuarioDTO, '▲', '▼', new string[] { }, false);
                Locales = Serializador.Serializar(lst_UsuarioDTO[0].oListaLocales, '▲', '▼', new string[] { }, false);
            }
            return string.Format("{0}↔{1}", listaUsuario, Locales);
        }
        public string Grabar(Seg_UsuarioDTO oSeg_UsuarioDTO)
        {
            ResultDTO<Seg_UsuarioDTO> Respuesta;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Seg_UsuarioBL oSeg_UsuarioBL = new Seg_UsuarioBL();

            string listaUsuarios = "";

            if (oSeg_UsuarioDTO.idUsuario == 0)
            {
                oSeg_UsuarioDTO.UsuarioCreacion = 1;
            }
            oSeg_UsuarioDTO.UsuarioModificacion = 1;
            oSeg_UsuarioDTO.idEmpresa = eSEGUsuario.idEmpresa;
            Respuesta = oSeg_UsuarioBL.UpdateInsert(oSeg_UsuarioDTO);

            List<Seg_UsuarioDTO> lst_UsuarioDTO = Respuesta.ListaResultado;

            if (lst_UsuarioDTO != null && lst_UsuarioDTO.Count > 0)
            {
                listaUsuarios = Serializador.Serializar(lst_UsuarioDTO, '▲', '▼', new string[] { "idUsuario", "CodigoGenerado", "RolDescripcion", "Usuario", "FechaModificacion", "UsuarioModificacionDescripcion", "Estado" }, false);
            }
            return string.Format("{0}↔{1}↔{2}", Respuesta.Resultado, Respuesta.MensajeError, listaUsuarios);

        }
        public string Eliminar(Seg_UsuarioDTO oSeg_UsuarioDTO)
        {

            ResultDTO<Seg_UsuarioDTO> Respuesta;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Seg_UsuarioBL oSeg_UsuarioBL = new Seg_UsuarioBL();

            string listaUsuarios = "";

            if (oSeg_UsuarioDTO.idUsuario == 0)
            {
                oSeg_UsuarioDTO.UsuarioCreacion = 1;
            }
            oSeg_UsuarioDTO.UsuarioModificacion = 1;
            oSeg_UsuarioDTO.idEmpresa = eSEGUsuario.idEmpresa;
            Respuesta = oSeg_UsuarioBL.Delete(oSeg_UsuarioDTO);

            List<Seg_UsuarioDTO> lst_UsuarioDTO = Respuesta.ListaResultado;

            if (lst_UsuarioDTO != null && lst_UsuarioDTO.Count > 0)
            {
                listaUsuarios = Serializador.Serializar(lst_UsuarioDTO, '▲', '▼', new string[] { "idUsuario", "CodigoGenerado", "RolDescripcion", "Usuario", "FechaModificacion", "UsuarioModificacionDescripcion", "Estado" }, false);
            }
            return string.Format("{0}↔{1}↔{2}", Respuesta.Resultado, Respuesta.MensajeError, listaUsuarios);
        }
    }
}
