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
    public class Atencion_MedicaController : Controller
    {
        // GET: Atencion_Medica
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public string ListaCitas(int idPaciente)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            ResultDTO<CitasDTO> lstCitasDTO = oCitasBL.ListarCitas_Atencion(idPaciente);
            string listaCitas = Serializador.rSerializado(lstCitasDTO.ListaResultado, new string[] { "idCita", "Codigo", "FechaCita", "NombrePersonal", "idPersonal" });
            return String.Format("{0}↔{1}↔{2}", lstCitasDTO.Resultado, lstCitasDTO.MensajeError, listaCitas);
        }
        public string Grabar(AtencionMedicaDTO oAtencionMedicaDTO)
        {
            ResultDTO<AtencionMedicaDTO> oResultDTO;
            ResultDTO<AtencionMedicaDTO> oResult_Receta;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            AtencionMedicaBL oAtencionMedicaBL = new AtencionMedicaBL();
            if (oAtencionMedicaDTO.idAtencionMedica == 0)
            {
                oAtencionMedicaDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oAtencionMedicaDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oAtencionMedicaBL.UpdateInsert(oAtencionMedicaDTO);
            string idAtencionMedica = oAtencionMedicaBL.UltimoIdAtencionMedica();
            oAtencionMedicaDTO.idAtencionMedica = Convert.ToInt32(idAtencionMedica);
            if (!string.IsNullOrWhiteSpace(oAtencionMedicaDTO.lista_Recetas))
                oResult_Receta = oAtencionMedicaBL.UpdateInsertReceta(oAtencionMedicaDTO);
            string listaHistoriaClinica = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idAtencionMedica", "FechaCreacion", "Personal", "PlanTerapeutico" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaHistoriaClinica);
        }
        public string ObtenerDatosxID(int id)
        {
            HistoriaClinicaBL oHistoriaClinicaBL = new HistoriaClinicaBL();
            AtencionMedicaBL oAtencionMedicaBL = new AtencionMedicaBL();
            ResultDTO<AtencionMedicaDTO> oAtencionDTO = oAtencionMedicaBL.ListarxID(id);
            string lista_AtencionMedica = Serializador.rSerializado(oAtencionDTO.ListaResultado, new string[] { "idAtencionMedica", "Codigo", "idPersonal", "Personal", "PlanTerapeutico", "MotivoConsulta", "idCita" });
            string lista_AtencionMedica_Receta = Serializador.rSerializado(oAtencionDTO.ListaResultado[0].oListaRecetas, new string[] { "idAtencionMedica_Receta", "FechaCreacion", "NroReceta", "idAtencionMedica" });
            string lista_AtencionMedica_Evolucion = Serializador.rSerializado(oAtencionDTO.ListaResultado[0].oListaEvolucion, new string[] { "idAtencionMedica_Evolucion", "FechaEvolucion", "Descripcion", "idAtencionMedica" });
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", oAtencionDTO.Resultado, oAtencionDTO.MensajeError, lista_AtencionMedica, lista_AtencionMedica_Receta, lista_AtencionMedica_Evolucion);
        }
        public string ObtenerDatos()
        {
            HistoriaClinicaBL oHistoriaClinicaBL = new HistoriaClinicaBL();
            AtencionMedicaBL oAtencionMedicaBL = new AtencionMedicaBL();
            ResultDTO<HistoriaClinicaDTO> oHistoriaClinicaDTO = oHistoriaClinicaBL.ListarTodo();
            string lista_HistoriaClinica = Serializador.rSerializado(oHistoriaClinicaDTO.ListaResultado, new string[] { "idHistoria", "Codigo", "NombrePaciente", "Dni", "FechaNacimiento", "Edad" });
            string nroHistoria = oHistoriaClinicaBL.NroHistoriaUltimo();
            string Fecha = DateTime.Now.ToString().Substring(0, 11);
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", oHistoriaClinicaDTO.Resultado, oHistoriaClinicaDTO.MensajeError, lista_HistoriaClinica, nroHistoria, Fecha);
        }
        public string ObtenerNumeroReceta()
        {
            AtencionMedicaBL oAtencionMedicaBL = new AtencionMedicaBL();
            string NroReceta = oAtencionMedicaBL.NroRecetaUltimo();
            return NroReceta;
        }
        public string GrabarReceta(AtencionMedicaDTO oAtencionMedicaDTO)
        {
            ResultDTO<AtencionMedicaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            AtencionMedicaBL oAtencionMedicaBL = new AtencionMedicaBL();
            if (oAtencionMedicaDTO.idReceta == 0)
            {
                oAtencionMedicaDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oAtencionMedicaDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oAtencionMedicaBL.UpdateInsertReceta(oAtencionMedicaDTO);
            string listaHistoriaClinica = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idAtencionMedica", "FechaCreacion", "Personal", "PlanTerapeutico" });
            return string.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listaHistoriaClinica, oResultDTO.Campo1);
        }
        public string ObtenerRecetaxID(int id)
        {
            AtencionMedicaBL oAtencionMedicaBL = new AtencionMedicaBL();
            ResultDTO<AtencionMedica_RecetaDTO> oAtencionMedica_RecetaDTO = oAtencionMedicaBL.ObtenerRecetaxID(id);
            string lista_AtencionMedica_Receta = Serializador.rSerializado(oAtencionMedica_RecetaDTO.ListaResultado, new string[] { "idAtencionMedica_Receta", "Medicamento", "Dosis", "Via", "Frecuencia", "Duracion", "idAtencionMedica" });
            return String.Format("{0}↔{1}↔{2}", oAtencionMedica_RecetaDTO.Resultado, oAtencionMedica_RecetaDTO.MensajeError, lista_AtencionMedica_Receta);
        }
    }
}