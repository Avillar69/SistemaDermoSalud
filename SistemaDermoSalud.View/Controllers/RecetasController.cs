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
    public class RecetasController : Controller
    {
        // GET: Recetas
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
            PacientesBL oPacientesBL = new PacientesBL();
            ResultDTO<PacientesDTO> oResultDTO = oPacientesBL.ListarTodo();
            string listaPacientes = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idPaciente", "DNI", "NombreCompleto", "UltimaConsulta", "Estado" });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaPacientes);
        }
    }
}