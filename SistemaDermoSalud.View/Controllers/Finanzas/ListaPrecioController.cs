using SistemaDermoSalud.Business;
using SistemaDermoSalud.Business.Compras;
using SistemaDermoSalud.Business.Mantenimiento;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Compras;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers.Finanzas
{
    public class ListaPrecioController : Controller
    {
        // GET: ListaPrecio
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }

        public string ObtenerDatosCompras()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            Ma_Clase_ArticuloBL oMa_Clase_ArticuloBL = new Ma_Clase_ArticuloBL();
            //Listas
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = oAD_SocioNegocioBL.ListarProv(eSEGUsuario.idEmpresa, "P");
            ResultDTO<Ma_Clase_ArticuloDTO> oListaClaseArticulo = oMa_Clase_ArticuloBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            //Cadenas
            string listaAD_SocioNegocio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idSocioNegocio", "Documento", "RazonSocial" });

            string listaTipoCompra = Serializador.rSerializado(oListaClaseArticulo.ListaResultado, new string[] { "idClaseArticulo", "CodigoGenerado", "Descripcion" });
            return String.Format("{0}↔{1}↔{2}↔{3}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaAD_SocioNegocio, listaTipoCompra);
        }
        public string ObtenerDatosxProveedor(int idProveedor)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            COM_ListaPrecioBL oCOM_ListaPrecioBL = new COM_ListaPrecioBL();
            ResultDTO<COM_ListaPrecioDTO> oResultDTO = oCOM_ListaPrecioBL.ListarxProveedor(eSEGUsuario.idEmpresa, idProveedor);
            string listaPrecioCompra = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idDetalleListaPrecio", "RazonSocial", "descripcionArticulo","descripcionClaseArticulo",
            "descripcionCategoria","descripcionMoneda","Valor","FechaCreacion","idArticulo","idMoneda"});
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaPrecioCompra);
        }
        public string ObtenerDatosxIDMetarial(int idProveedor, int idArticulo, int idMoneda)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            COM_ListaPrecioBL oCOM_ListaPrecioBL = new COM_ListaPrecioBL();
            ResultDTO<COM_ListaPrecioDTO> oResultDTO = oCOM_ListaPrecioBL.ListarxIdMaterial(eSEGUsuario.idEmpresa, idProveedor, idArticulo, idMoneda);
            string listaPrecioCompra = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "FechaCreacion", "descripcionArticulo", "descripcionMoneda", "Valor" });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaPrecioCompra);
        }
        public string ObtenerPrecioArtProv(int iA, int iP, int iM)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_ArticuloBL oMa_ArticuloBL = new Ma_ArticuloBL();

            ResultDTO<Ma_ArticuloDTO> oResultDTO = oMa_ArticuloBL.ObtenerPrecioArtProv(eSEGUsuario.idEmpresa, iA, iP, iM);
            string listaMa_Articulo = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "DesUnidadMedida", "PrecioCompra" });

            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_Articulo);

        }
        public string Grabar(COM_ListaPrecioDTO oCOM_ListaPrecioDTO)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            ResultDTO<COM_ListaPrecioDTO> oResultDTO;
            COM_ListaPrecioBL oCOM_ListaPrecioBL = new COM_ListaPrecioBL();
            if (oCOM_ListaPrecioDTO.idDetalleListaPrecio == 0)
            {
                oCOM_ListaPrecioDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oCOM_ListaPrecioDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oResultDTO = oCOM_ListaPrecioBL.UpdateInsert(oCOM_ListaPrecioDTO);
            List<COM_ListaPrecioDTO> lstAD_SocioNegocioDTO = oResultDTO.ListaResultado;
            string listaPreciosCompra = Serializador.rSerializado(lstAD_SocioNegocioDTO, new string[] { "idDetalleListaPrecio", "RazonSocial", "descripcionArticulo","descripcionClaseArticulo",
            "descripcionCategoria","descripcionMoneda","Valor","FechaCreacion","idArticulo","idMoneda"});
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaPreciosCompra);
        }



    }
}