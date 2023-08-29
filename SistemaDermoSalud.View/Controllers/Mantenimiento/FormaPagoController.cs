using SistemaDermoSalud.Business;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers.Mantenimiento
{
    public class FormaPagoController : Controller
    {
        // GET: FormaPago
        public ActionResult Index()
        {
            return PartialView();
        }

        public string ObtenerDatos(string Activo = "")
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_FormaPagoBL oMa_FormaPagoBL = new Ma_FormaPagoBL();
            ResultDTO<Ma_FormaPagoDTO> oResultDTO = oMa_FormaPagoBL.ListarTodo(eSEGUsuario.idEmpresa, Activo);
            string listaMa_FormaPagoDTO = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaMa_FormaPagoDTO = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { "idFormaPago", "CodigoGenerado", "Descripcion", "FechaModificacion", "UsuarioModificacionDescripcion", "Estado" }, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_FormaPagoDTO);
        }

        public string ObtenerDatosxID(int id)
        {
            Ma_FormaPagoBL oMa_FormaPagoBL = new Ma_FormaPagoBL();
            ResultDTO<Ma_FormaPagoDTO> oResultDTO = oMa_FormaPagoBL.ListarxID(id);
            string listaMa_FormaPagoDTO = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaMa_FormaPagoDTO = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { }, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_FormaPagoDTO);
        }

        public string Grabar(Ma_FormaPagoDTO oMa_FormaPagoDTO)
        {
            ResultDTO<Ma_FormaPagoDTO> oResulDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_FormaPagoBL oMa_FormaPagoBL = new Ma_FormaPagoBL();
            string listaMa_FormaPago = "";
            if (oMa_FormaPagoDTO.idFormaPago == 0)
            {
                oMa_FormaPagoDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }

            oMa_FormaPagoDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oMa_FormaPagoDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oResulDTO = oMa_FormaPagoBL.UpdateInsert(oMa_FormaPagoDTO);

            List<Ma_FormaPagoDTO> lstMa_FormaPagoDTO = oResulDTO.ListaResultado;
            if (lstMa_FormaPagoDTO != null && lstMa_FormaPagoDTO.Count > 0)
            {
                listaMa_FormaPago = Serializador.Serializar(lstMa_FormaPagoDTO, '▲', '▼', new string[] { "idFormaPago", "CodigoGenerado", "Descripcion", "FechaModificacion", "UsuarioModificacionDescripcion", "Estado" }, false);
            }
            return string.Format("{0}↔{1}↔{2}↔{3}", oResulDTO.Resultado, oResulDTO.MensajeError, listaMa_FormaPago, oResulDTO.Campo1);
        }

        public string Eliminar(Ma_FormaPagoDTO oMa_FormaPagoDTO)
        {
            ResultDTO<Ma_FormaPagoDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_FormaPagoBL oMa_FormaPagoBL = new Ma_FormaPagoBL();
            string listaMa_FormaPago = "";
            oMa_FormaPagoDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oResultDTO = oMa_FormaPagoBL.Delete(oMa_FormaPagoDTO);
            List<Ma_FormaPagoDTO> lstMa_FormaPagoDTO = oResultDTO.ListaResultado;
            if (lstMa_FormaPagoDTO != null && lstMa_FormaPagoDTO.Count > 0)
            {
                listaMa_FormaPago = Serializador.Serializar(lstMa_FormaPagoDTO, '▲', '▼', new string[] { "idFormaPago", "CodigoGenerado", "Descripcion", "FechaModificacion", "UsuarioModificacionDescripcion", "Estado" }, false);
            }
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_FormaPago);
        }
    }
}