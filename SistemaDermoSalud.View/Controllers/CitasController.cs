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
    public class CitasController : Controller
    {
        //Pantalla Principal Citas
        public ActionResult Index()
        {
            return PartialView();
        }
        public string ObtenerDatos()
        {

            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            PacientesBL oPacientesBL = new PacientesBL();
            ResultDTO<CitasDTO> oResultDTO = oCitasBL.ListarTodo(fechaInicio+"", fechaFin+"");
            ResultDTO<PacientesDTO> oResultDTO_Paciente = oPacientesBL.ListarTodo();
            string listaCitas = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idCita", "Codigo", "DescripcionServicio", "NombreCompleto", "NombrePersonal", "FechaHora", });
            string listaPacientes = Serializador.rSerializado(oResultDTO_Paciente.ListaResultado, new string[] { "NombreCompleto" });
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCitas,eSEGUsuario.idRol,listaPacientes);
        }
        public string ListarCitasEnVenta()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            ResultDTO<CitasDTO> oResultDTO = oCitasBL.ListarCitasEnVenta();
            string listaCitas = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idCita", "FechaCita", "Codigo", "NombreCompleto", "Pago", "idPaciente" });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCitas);
        }
        public string BuscarxFecha(string fI, string fF)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            ResultDTO<CitasDTO> oResultDTO = oCitasBL.ListarTodo(fI, fF);
            string listaCitas = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idCita", "Codigo", "DescripcionServicio", "NombreCompleto", "NombrePersonal", "FechaHora", });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCitas);
        }
        public string ObtenerDatosxID(int id)
        {
            CitasBL oCitasBL = new CitasBL();
            ResultDTO<CitasDTO> oResultDTO = oCitasBL.ListarxID(id);
            string listaCitas = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCitas);
        }
        public string Grabar(CitasDTO oCitasDTO,string fi,string ff)
        {
            ResultDTO<CalendarEventDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            if (oCitasDTO.idCita == 0)
            {
                oCitasDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oCitasDTO.idRol = eSEGUsuario.idRol;
            oCitasDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            DateTime fechaInicio = Convert.ToDateTime(fi);
            DateTime fechaFin = Convert.ToDateTime(ff);
            oCitasDTO.FechaInicio = fechaInicio.ToShortDateString();
            oCitasDTO.FechaFin = fechaFin.ToShortDateString();

            //oCitasDTO.FechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToShortDateString();
            //oCitasDTO.FechaFin= new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).ToShortDateString();
            oResultDTO = oCitasBL.UpdateInsert(oCitasDTO);
            string listaCitas = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] {  });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCitas);
        }
        public string Eliminar(CitasDTO oCitasDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            ResultDTO<CitasDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            
            oResultDTO = oCitasBL.Delete(oCitasDTO);
            oResultDTO = oCitasBL.ListarTodo(fechaInicio.ToShortDateString(), fechaFin.ToShortDateString());
            string listaCitas = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idCita", "Codigo", "DescripcionServicio", "NombreCompleto", "NombrePersonal", "FechaHora", });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCitas);
        }
        //Programación
        public ActionResult Programacion()
        {
            return PartialView();
        }
        public string ObtenerEventos(string fi , string ff)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            CitasBL oCitasBL = new CitasBL();
            PacientesBL oPacientesBL = new PacientesBL();
            ResultDTO<CalendarEventDTO> oResultDTO = oCitasBL.ListarEventos(fechaInicio.ToShortDateString(), fechaFin.ToShortDateString(),eSEGUsuario.idRol ,eSEGUsuario.idUsuario);
            ResultDTO<PacientesDTO> oResultDTO_Paciente = oPacientesBL.ListarTodo();
            string listaEventos = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            string listaPacientes = Serializador.rSerializado(oResultDTO_Paciente.ListaResultado, new string[] { "NombreCompleto" });
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", oResultDTO.Resultado, oResultDTO.MensajeError, listaEventos,eSEGUsuario.idRol,listaPacientes);
        }
        public string detEvento(string c)
        {
            CitasBL oCitasBL = new CitasBL();
            ResultDTO<CitasDTO> oResultDTO = oCitasBL.ListarxCodigo(c);
            string listaCitas = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            string listaCitasDetalle = Serializador.rSerializado(oResultDTO.ListaResultado[0].oListaDetalle, new string[] { });
            return String.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCitas, listaCitasDetalle);
        }
        public string Confirmar(CitasDTO oCitasDTO)
        {
            ResultDTO<CalendarEventDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            oCitasDTO.idRol = eSEGUsuario.idRol;
            oCitasDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oCitasDTO.EstadoCita = 2;//Estado Confirmado
            oCitasDTO.FechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToShortDateString();
            oCitasDTO.FechaFin = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).ToShortDateString();
            oResultDTO = oCitasBL.ConfirmarCita(oCitasDTO);
            string listaCitas = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCitas);
        }
        public string ObtenerEventosxFecha(string fi, string ff)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;

            DateTime fechaInicio = Convert.ToDateTime(fi);
            DateTime fechaFin = Convert.ToDateTime(ff);

            CitasBL oCitasBL = new CitasBL();
            ResultDTO<CalendarEventDTO> oResultDTO = oCitasBL.ListarEventos(fechaInicio.ToShortDateString(), fechaFin.ToShortDateString(), eSEGUsuario.idRol, eSEGUsuario.idUsuario);
            string listaEventos = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            return String.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listaEventos, eSEGUsuario.idRol);
        }
        public string ObtenerCitasxPaciente(CitasDTO oCitasDTO)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            ResultDTO<CitasDTO> oResultDTO = oCitasBL.ObtenerCitasxPaciente(oCitasDTO);
            string listaCitas = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "Codigo","FechaCita","NombrePersonal", "Tratamiento", });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCitas);
        }
    }
}
