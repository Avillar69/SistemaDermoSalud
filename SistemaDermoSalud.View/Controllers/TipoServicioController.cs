using SistemaDermoSalud.Business;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers
{
    public class TipoServicioController : Controller
    {
        // GET: TipoServicio
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
            TipoServicioBL oTipoServicioBL = new TipoServicioBL();
            ResultDTO<TipoServicioDTO> oResultDTO = oTipoServicioBL.ListarTodo();
            string listaTipoServicio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idTipoServicio", "Codigo", "NombreTipoServicio", "Estado" });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaTipoServicio);
        }
        public string ObtenerDatosxID(int id)
        {
            TipoServicioBL oTipoServicioBL = new TipoServicioBL();
            ResultDTO<TipoServicioDTO> oResultDTO = oTipoServicioBL.ListarxID(id);
            string listaTipoServicio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaTipoServicio);
        }

        public string Grabar(TipoServicioDTO oTipoServicioDTO)
        {
            ResultDTO<TipoServicioDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            TipoServicioBL oTipoServicioBL = new TipoServicioBL();
            if (oTipoServicioDTO.idTipoServicio == 0)
            {
                oTipoServicioDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oTipoServicioDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oTipoServicioBL.UpdateInsert(oTipoServicioDTO);
            string listaTipoServicio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idTipoServicio", "Codigo", "NombreTipoServicio", "Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaTipoServicio);
        }

        public string Eliminar(TipoServicioDTO oServiciosDTO)
        {
            ResultDTO<TipoServicioDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            TipoServicioBL oTipoServicioBL = new TipoServicioBL();
            oResultDTO = oTipoServicioBL.Delete(oServiciosDTO);
            string listaTipoServicio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idTipoServicio", "Codigo", "NombreTipoServicio", "Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaTipoServicio);
        }
    }
}