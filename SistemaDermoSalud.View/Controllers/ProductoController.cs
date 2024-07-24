using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Business.Mantenimiento;
using SistemaDermoSalud.Helpers;

namespace SistemaDermoSalud.Controllers
{
    public class ProductoController : Controller
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
            Ma_ProductoBL oProductoBL = new Ma_ProductoBL();
            Ma_MarcaBL oMarcaBL = new Ma_MarcaBL();

            ResultDTO<Ma_ProductoDTO> oResultDTO = oProductoBL.ListarTodo(1);
            ResultDTO<Ma_MarcaDTO> oResultMarcaDTO = oMarcaBL.ListarTodo(1);
            
            string listaProducto = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idProducto","Descripcion","Marca","Estado"});
            string listaMarca = Serializador.rSerializado(oResultMarcaDTO.ListaResultado, new string[] { "idMarca","Marca"});
            return String.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMarca, listaProducto);
        }
        public string ObtenerDatosxID(int id)
        {
            Ma_ProductoBL oProductoBL = new Ma_ProductoBL();
            ResultDTO<Ma_ProductoDTO> oResultDTO = oProductoBL.ListarxID(id);
            string listaProducto = "";
            listaProducto = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError,listaProducto);
        }
        public string Grabar(Ma_ProductoDTO oProductoDTO)
        {
            ResultDTO<Ma_ProductoDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_ProductoBL oProductoBL = new Ma_ProductoBL();
            string listaProducto = "";
            if(oProductoDTO.idProducto == 0)
            {
                oProductoDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oProductoDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oProductoBL.UpdateInsert(oProductoDTO);
            
            List<Ma_ProductoDTO> lstProductoDTO = oResultDTO.ListaResultado;
            listaProducto = Serializador.rSerializado(lstProductoDTO, new string[] { "idProducto", "Descripcion", "Marca", "Estado" });
            return string.Format("{0}↔{1}↔{2}",oResultDTO.Resultado, oResultDTO.MensajeError, listaProducto);
        }
        public string Eliminar(Ma_ProductoDTO oProductoDTO)
        {
            ResultDTO<Ma_ProductoDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_ProductoBL oProductoBL = new Ma_ProductoBL();
            string listaProducto = "";
            oResultDTO = oProductoBL.Delete(oProductoDTO);
            List<Ma_ProductoDTO> lstProductoDTO = oResultDTO.ListaResultado;
            listaProducto = Serializador.rSerializado(lstProductoDTO, new string[] { "idProducto", "Descripcion", "Marca", "Estado" });
            return string.Format("{0}↔{1}↔{2}",oResultDTO.Resultado, oResultDTO.MensajeError, listaProducto);
        }
    }
}
