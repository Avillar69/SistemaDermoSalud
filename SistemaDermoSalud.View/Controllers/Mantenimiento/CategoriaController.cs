using SistemaDermoSalud.Business.Mantenimiento;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers.Mantenimiento
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public string ObtenerDatos(string Activo = "")
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_CategoriaBL oMa_CategoriaBL = new Ma_CategoriaBL();
            ResultDTO<Ma_CategoriaDTO> oResultDTO = oMa_CategoriaBL.ListarTodo(eSEGUsuario.idEmpresa, Activo);
            string listaMa_Categoria = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaMa_Categoria = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                "idCategoria", "CodigoGenerado", "Descripcion", "FechaModificacion", "DesUsuarioModificacion", "Estado"}, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_Categoria);
        }

        public string ObtenerDatosxTipo(string Tipo, string Activo = "")
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_CategoriaBL oMa_CategoriaBL = new Ma_CategoriaBL();
            ResultDTO<Ma_CategoriaDTO> oResultDTO = oMa_CategoriaBL.ListarTodoxTipo(eSEGUsuario.idEmpresa, Convert.ToInt32(Tipo), Activo);
            string listaMa_Categoria = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaMa_Categoria = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                "idCategoria", "CodigoGenerado", "Descripcion", "FechaModificacion", "DesUsuarioModificacion", "Estado"}, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_Categoria);
        }
        public string ObtenerDatosxID(int id)
        {
            Ma_CategoriaBL oMa_CategoriaBL = new Ma_CategoriaBL();
            ResultDTO<Ma_CategoriaDTO> oResultDTO = oMa_CategoriaBL.ListarxID(id);
            string listaMa_Categoria = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaMa_Categoria = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { }, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_Categoria);
        }

        public string Grabar(Ma_CategoriaDTO oMa_CategoriaDTO)
        {
            ResultDTO<Ma_CategoriaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_CategoriaBL oMa_CategoriaBL = new Ma_CategoriaBL();
            string listaMa_Categoria = "";
            if (oMa_CategoriaDTO.idCategoria == 0)
            {
                oMa_CategoriaDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
                oMa_CategoriaDTO.FechaCreacion = DateTime.Now;
            }
            oMa_CategoriaDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oMa_CategoriaDTO.FechaModificacion = DateTime.Now;
            oMa_CategoriaDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oResultDTO = oMa_CategoriaBL.UpdateInsert(oMa_CategoriaDTO);

            List<Ma_CategoriaDTO> lstMa_CategoriaDTO = oResultDTO.ListaResultado;
            if (lstMa_CategoriaDTO != null && lstMa_CategoriaDTO.Count > 0)
            {
                listaMa_Categoria = Serializador.Serializar(lstMa_CategoriaDTO, '▲', '▼', new string[] {
                "idCategoria", "CodigoGenerado", "Descripcion", "FechaModificacion", "DesUsuarioModificacion", "Estado"}, false);
            }
            return string.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_Categoria, oResultDTO.Campo1);
        }

        public string Eliminar(Ma_CategoriaDTO oMa_CategoriaDTO)
        {
            ResultDTO<Ma_CategoriaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_CategoriaBL oMa_CategoriaBL = new Ma_CategoriaBL();
            oMa_CategoriaDTO.idEmpresa = eSEGUsuario.idEmpresa;
            string listaMa_Categoria = "";
            oResultDTO = oMa_CategoriaBL.Delete(oMa_CategoriaDTO);
            List<Ma_CategoriaDTO> lstMa_CategoriaDTO = oResultDTO.ListaResultado;
            if (lstMa_CategoriaDTO != null && lstMa_CategoriaDTO.Count > 0)
            {
                listaMa_Categoria = Serializador.Serializar(lstMa_CategoriaDTO, '▲', '▼', new string[] {
                "idCategoria", "CodigoGenerado", "Descripcion", "FechaModificacion", "DesUsuarioModificacion", "Estado"}, false);
            }
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_Categoria);
        }



    }
}