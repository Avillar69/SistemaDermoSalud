using SistemaDermoSalud.Business;
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
    public class ArticuloController : Controller
    {
        // GET: Articulo
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
            Ma_ArticuloBL oMa_ArticuloBL = new Ma_ArticuloBL();
            Ma_AlmacenBL oMa_AlmacenBL = new Ma_AlmacenBL();
            Ma_MarcaBL oMa_MarcaBL = new Ma_MarcaBL();
            Ma_CategoriaBL oMa_CategoriaBL = new Ma_CategoriaBL();
            Ma_Clase_ArticuloBL oMa_Clase_ArticuloBL = new Ma_Clase_ArticuloBL();
            Ma_UnidadMedidaBL oMa_UnidadMedidaBL = new Ma_UnidadMedidaBL();
            ResultDTO<Ma_ArticuloDTO> oResultDTO = oMa_ArticuloBL.ListarTodo(eSEGUsuario.idEmpresa, Activo);
            ResultDTO<Ma_AlmacenDTO> oResultDTO_Almacen = oMa_AlmacenBL.ListarTodo(eSEGUsuario.idEmpresa, "A");

            ResultDTO<Ma_MarcaDTO> oResultDTO_Marca = oMa_MarcaBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<Ma_CategoriaDTO> oResultDTO_Categoria = oMa_CategoriaBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<Ma_UnidadMedidaDTO> oResultDTO_UnidadMedida = oMa_UnidadMedidaBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<Ma_Clase_ArticuloDTO> oResultDTO_ClaseArticulo = oMa_Clase_ArticuloBL.ListarTodo(eSEGUsuario.idEmpresa, "A");

            string listaMa_Articulo = "";
            string listaMa_Almacen = "";
            string idUsuario = eSEGUsuario.idUsuario.ToString();
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                if (Activo == "")
                {
                    listaMa_Articulo = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                    "idArticulo","CodigoProducto","Descripcion","FechaModificacion","DesUsuarioModificacion","Estado"}, false);
                }
                else
                {
                    listaMa_Articulo = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                    "idArticulo","CodigoProducto","Descripcion","FechaModificacion","DesUsuarioModificacion","Estado"}, false);
                }

            }
            if (oResultDTO_Almacen.ListaResultado != null && oResultDTO_Almacen.ListaResultado.Count > 0)
            {
                listaMa_Almacen = Serializador.Serializar(oResultDTO_Almacen.ListaResultado, '▲', '▼', new string[] {
                "idAlmacen", "CodigoGenerado", "Descripcion", "Direccion"}, false);
            }
            string listaMa_Marca = Serializador.rSerializado(oResultDTO_Marca.ListaResultado, new string[] { "idMarca", "CodigoGenerado", "Descripcion" });
            string listaMa_Categoria = Serializador.rSerializado(oResultDTO_Categoria.ListaResultado, new string[] { "idCategoria", "CodigoGenerado", "Descripcion" });
            string listaMa_UnidadMedida = Serializador.rSerializado(oResultDTO_UnidadMedida.ListaResultado, new string[] { "idUnidadMedida", "Codigo", "Descripcion" });
            string listaMa_ClaseArticulo = Serializador.rSerializado(oResultDTO_ClaseArticulo.ListaResultado, new string[] { "idClaseArticulo", "CodigoGenerado", "Descripcion" });
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}↔{7}↔{8}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_Articulo, oResultDTO.Campo1, listaMa_Almacen, listaMa_ClaseArticulo, listaMa_Categoria, listaMa_Marca, listaMa_UnidadMedida);
        }
        public string ObtenerDatosxID(int id)
        {
            Ma_ArticuloBL oMa_ArticuloBL = new Ma_ArticuloBL();

            ResultDTO<Ma_ArticuloDTO> oResultDTO = oMa_ArticuloBL.ListarxID(id);
            string listaMa_Articulo = "";
            string lista_Diseño = "";
            string listaDiseño = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaMa_Articulo = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { }, false);
                lista_Diseño = Serializador.Serializar(oResultDTO.ListaResultado[0].oListaDiseño, '▲', '▼', new string[] { }, false);
                listaDiseño = Serializador.rSerializado(oResultDTO.ListaResultado[0].oListaDiseño, new string[] { });
            }
            return String.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_Articulo, listaDiseño);
        }
        public string Grabar(Ma_ArticuloDTO oMa_ArticuloDTO)
        {
            ResultDTO<Ma_ArticuloDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_ArticuloBL oMa_ArticuloBL = new Ma_ArticuloBL();
            string listaMa_Articulo = "";
            if (oMa_ArticuloDTO.idArticulo == 0)
            {
                oMa_ArticuloDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oMa_ArticuloDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oMa_ArticuloDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oMa_ArticuloBL.UpdateInsert(oMa_ArticuloDTO);

            List<Ma_ArticuloDTO> lstMa_ArticuloDTO = oResultDTO.ListaResultado;
            if (lstMa_ArticuloDTO != null && lstMa_ArticuloDTO.Count > 0)
            {
                listaMa_Articulo = Serializador.Serializar(lstMa_ArticuloDTO, '▲', '▼', new string[] {
                   "idArticulo","CodigoProducto","Descripcion","FechaModificacion","DesUsuarioModificacion","Estado"}, false);
            }
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_Articulo);
        }
        public string Eliminar(Ma_ArticuloDTO oMa_ArticuloDTO)
        {
            ResultDTO<Ma_ArticuloDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_ArticuloBL oMa_ArticuloBL = new Ma_ArticuloBL();
            string listaMa_Articulo = "";
            oResultDTO = oMa_ArticuloBL.Delete(oMa_ArticuloDTO);
            List<Ma_ArticuloDTO> lstMa_ArticuloDTO = oResultDTO.ListaResultado;
            if (lstMa_ArticuloDTO != null && lstMa_ArticuloDTO.Count > 0)
            {
                listaMa_Articulo = Serializador.Serializar(lstMa_ArticuloDTO, '▲', '▼', new string[] {
                    "idArticulo","CodigoAutogenerado","CodigoProducto","DesCategoria","Descripcion","FechaModificacion","DesUsuarioModificacion","Estado"
                }, false);
            }
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_Articulo);
        }
        public string ObtenerDatosxCategoria(int Cat)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_ArticuloBL oMa_ArticuloBL = new Ma_ArticuloBL();
            ResultDTO<Ma_ArticuloDTO> oResultDTO = oMa_ArticuloBL.ListarTodoxCategoria(Cat, eSEGUsuario.idUsuario, "");
            string listaMa_Articulo = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaMa_Articulo = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                    "idArticulo","CodigoProducto","Descripcion"}, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_Articulo);
        }
        public ActionResult Download(int iAD)
        {
            //byte[] fileBytes = System.IO.File.ReadAllBytes(@"c:\folder\myfile.ext");
            Ma_ArticuloBL oMa_ArticuloBL = new Ma_ArticuloBL();
            ResultDTO<Ma_ArticuloDisenoDTO> oResult = oMa_ArticuloBL.GetFileDiseno(iAD);
            Byte[] bytes = Convert.FromBase64String(oResult.ListaResultado[0].Archivo.Split(',')[1]);
            string fileName = oResult.ListaResultado[0].NombreArchivo;
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}