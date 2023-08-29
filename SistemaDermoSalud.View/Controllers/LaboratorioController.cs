using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Helpers;

namespace SistemaDermoSalud.View.Controllers
{
    public class LaboratorioController : Controller
    {
        //
        // GET: /Laboratorio/
        public ActionResult Index()
        {
            return PartialView();
        }

        public string ObtenerDatos()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            LaboratorioBL oLaboratorioBL = new LaboratorioBL();
            ResultDTO<LaboratorioDTO> oResultLabDTO = oLaboratorioBL.ListarTodo(1);
            string ListaLaboratorio = Serializador.rSerializado(oResultLabDTO.ListaResultado, new string[]{"idLaboratorio","Laboratorio","FechaCreacion","Estado"});
            return String.Format("{0}↔{1}↔{2}", oResultLabDTO.Resultado,oResultLabDTO.MensajeError,ListaLaboratorio);
        }
        public string ObtenerDatosxID(int id)
        {
            LaboratorioBL oLaboratorioBL = new LaboratorioBL();
            ResultDTO<LaboratorioDTO> oResultDTO = oLaboratorioBL.ListarxID(id);
            string listaLaboratorio = "";
            listaLaboratorio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaLaboratorio);
        }

        public string Grabar(LaboratorioDTO oLaboratorioDTO)
        {
            ResultDTO<LaboratorioDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            LaboratorioBL oLaboratorioBL = new LaboratorioBL();
            string listaLaboratorio = "";
            if (oLaboratorioDTO.idLaboratorio == 0)
            {
                oLaboratorioDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oLaboratorioDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oLaboratorioBL.UpdateInsert(oLaboratorioDTO);

            List<LaboratorioDTO> lstLaboratorioDTO = oResultDTO.ListaResultado;
            listaLaboratorio = Serializador.rSerializado(lstLaboratorioDTO, new string[] { "idLaboratorio", "Laboratorio", "FechaCreacion", "Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaLaboratorio);
        }

        public string Eliminar(LaboratorioDTO oLaboratorioDTO)
        {
            ResultDTO<LaboratorioDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            LaboratorioBL oLaboratorioBL = new LaboratorioBL();
            string listaLaboratorio = "";
            oResultDTO = oLaboratorioBL.Delete(oLaboratorioDTO);
            List<LaboratorioDTO> lstLaboratorioDTO = oResultDTO.ListaResultado;
            listaLaboratorio = Serializador.rSerializado(lstLaboratorioDTO, new string[] { "idLaboratorio", "Laboratorio", "FechaCreacion", "Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaLaboratorio);
        }
	}
}