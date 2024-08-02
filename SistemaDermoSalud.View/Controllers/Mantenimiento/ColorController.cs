using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Business.Mantenimiento;
using SistemaDermoSalud.Helpers;

namespace SistemaDermoSalud.View.Controllers.Mantenimiento
{
    public class ColorController : Controller
    {
        // GET: Color
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }

        public string ObtenerDatos(string tabla)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_MultitablaBL oMultitablaBL = new Ma_MultitablaBL();
            ResultDTO<Ma_MultitablaDTO> oResultLabDTO = oMultitablaBL.ListarTodo(tabla);
            string ListaMultitabla = Serializador.rSerializado(oResultLabDTO.ListaResultado, new string[] { "id", "Campo1", "Campo2", "Campo3", "FechaCreacion", "Estado" });
            return String.Format("{0}↔{1}↔{2}", oResultLabDTO.Resultado, oResultLabDTO.MensajeError, ListaMultitabla);
        }
        public string ObtenerDatosxID(int id)
        {
            Ma_MultitablaBL oMultitablaBL = new Ma_MultitablaBL();
            ResultDTO<Ma_MultitablaDTO> oResultDTO = oMultitablaBL.ListarxID(id);
            string ListaMultitabla = "";
            ListaMultitabla = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, ListaMultitabla);
        }
        public string Grabar(Ma_MultitablaDTO oMultitablaDTO)
        {
            ResultDTO<Ma_MultitablaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_MultitablaBL oMultitablaBL = new Ma_MultitablaBL();
            string ListaMultitabla = "";
            if (oMultitablaDTO.id == 0)
            {
                oMultitablaDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oMultitablaDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oMultitablaBL.UpdateInsert(oMultitablaDTO);

            List<Ma_MultitablaDTO> lstMarcaDTO = oResultDTO.ListaResultado;
            ListaMultitabla = Serializador.rSerializado(lstMarcaDTO, new string[] { "id", "Campo1", "Campo2", "Campo3", "FechaCreacion", "Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, ListaMultitabla);
        }
        public string Eliminar(Ma_MultitablaDTO oMultitablaDTO)
        {
            ResultDTO<Ma_MultitablaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_MultitablaBL oMultitablaBL = new Ma_MultitablaBL();
            string ListaMultitabla = "";
            oResultDTO = oMultitablaBL.Delete(oMultitablaDTO);
            List<Ma_MultitablaDTO> lstMarcaDTO = oResultDTO.ListaResultado;
            ListaMultitabla = Serializador.rSerializado(lstMarcaDTO, new string[] { "id", "Campo1", "Campo2", "Campo3", "FechaCreacion", "Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, ListaMultitabla);
        }
    }
}