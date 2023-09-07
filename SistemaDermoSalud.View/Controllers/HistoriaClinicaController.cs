using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Helpers;

namespace SistemaDermoSalud.Controllers
{
    public class HistoriaClinicaController : Controller                     
    {
        public ActionResult Listar()
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
            HistoriaClinicaBL oHistoriaClinicaBL = new HistoriaClinicaBL();
            string Fecha = DateTime.Now.ToString().Substring(0, 10);
            string NroHistoria = oHistoriaClinicaBL.NroHistoriaUltimo();
            ResultDTO<HistoriaClinicaDTO> oResultDTO = oHistoriaClinicaBL.ListarTodo();
            string listaHistoriaClinica= Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idHistoria", "Codigo", "NombrePaciente", "Dni", "FechaNacimiento", "Edad"});
            
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", oResultDTO.Resultado,oResultDTO.MensajeError,listaHistoriaClinica,NroHistoria,Fecha);
        }
        public string ObtenerDatosxID(int id)
        {
            HistoriaClinicaBL oHistoriaClinicaBL = new HistoriaClinicaBL();
            AtencionMedicaBL oAtencionMedicaBL = new AtencionMedicaBL();
            ResultDTO<HistoriaClinicaDTO> oResultDTO = oHistoriaClinicaBL.ListarxID(id);
            ResultDTO<AtencionMedicaDTO> oAtencionDTO = oAtencionMedicaBL.ListarxPaciente(oResultDTO.ListaResultado[0].idPaciente);
            string lista_HistoriaClinica = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            string lista_HistoriaClinica_Archivos = Serializador.rSerializado(oResultDTO.ListaResultado[0].oListaArchivos, new string[] { });
            string lista_AtencionMedica = Serializador.rSerializado(oAtencionDTO.ListaResultado, new string[] { "idAtencionMedica", "FechaCita","Personal","PlanTerapeutico"});
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", oResultDTO.Resultado,oResultDTO.MensajeError, lista_HistoriaClinica,lista_HistoriaClinica_Archivos,lista_AtencionMedica);
        }
        public string Grabar(HistoriaClinicaDTO oHistoriaClinicaDTO)
        {
            ResultDTO<HistoriaClinicaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            HistoriaClinicaBL oHistoriaClinicaBL = new HistoriaClinicaBL();
            if(oHistoriaClinicaDTO.idHistoria == 0)
            {
                oHistoriaClinicaDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oHistoriaClinicaDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oHistoriaClinicaBL.UpdateInsert(oHistoriaClinicaDTO);
            string listaHistoriaClinica = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idHistoria", "Codigo", "NombrePaciente", "Dni", "FechaNacimiento", "Edad" });
            return string.Format("{0}↔{1}↔{2}",oResultDTO.Resultado, oResultDTO.MensajeError, listaHistoriaClinica);
        }
        public string Eliminar(HistoriaClinicaDTO oHistoriaClinicaDTO)
        {
            ResultDTO<HistoriaClinicaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            HistoriaClinicaBL oHistoriaClinicaBL = new HistoriaClinicaBL();
            string listaHistoriaClinica = "";
            oResultDTO = oHistoriaClinicaBL.Delete(oHistoriaClinicaDTO);
            List<HistoriaClinicaDTO> lstHistoriaClinicaDTO = oResultDTO.ListaResultado;
            if (lstHistoriaClinicaDTO != null && lstHistoriaClinicaDTO.Count > 0)
            {
                listaHistoriaClinica = Serializador.Serializar(lstHistoriaClinicaDTO,'▲', '▼', new string[] {},false);
            }
            return string.Format("{0}↔{1}↔{2}",oResultDTO.Resultado, oResultDTO.MensajeError, listaHistoriaClinica);
        }
        public string ListaPacientes()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            PacientesBL oPacientesBL = new PacientesBL();
            ResultDTO<PacientesDTO> lstPacientesDTO = oPacientesBL.ListarTodo_HistoriaClinica();
            string listaPacientes = Serializador.rSerializado(lstPacientesDTO.ListaResultado, new string[] { "idPaciente",  "DNI", "NombreCompleto", "FechaNacimiento","Edad","Sexo" });
            return String.Format("{0}↔{1}↔{2}", lstPacientesDTO.Resultado, lstPacientesDTO.MensajeError, listaPacientes);
        }
        public string ListaPacientesSinHistoria()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            PacientesBL oPacientesBL = new PacientesBL();
            ResultDTO<PacientesDTO> lstPacientesDTO = oPacientesBL.ListarPacientes_SinHistoria();
            string listaPacientes = Serializador.rSerializado(lstPacientesDTO.ListaResultado, new string[] { "idPaciente", "DNI", "NombreCompleto", "FechaNacimiento", "Edad", "Sexo" });
            return String.Format("{0}↔{1}↔{2}", lstPacientesDTO.Resultado, lstPacientesDTO.MensajeError, listaPacientes);
        }
        public ActionResult Download(int iHC)
        {
            HistoriaClinicaBL oHistoriaClinicaBL = new HistoriaClinicaBL();
            ResultDTO<HistoriaClinica_ArchivosDTO> oResult = oHistoriaClinicaBL.GetFileArchivo(iHC);
            Byte[] bytes = Convert.FromBase64String(oResult.ListaResultado[0].Archivo.Split(',')[1]);
            string fileName = oResult.ListaResultado[0].NombreArchivo;
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

    }
}
