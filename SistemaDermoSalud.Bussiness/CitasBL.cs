using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.DataAccess;

namespace SistemaDermoSalud.Business
{
    public class CitasBL
    {
        CitasDAO oCitasDAO = new CitasDAO();
        public ResultDTO<CitasDTO> ListarTodo(string fechaInicio, string fechaFin)
        {
            return oCitasDAO.ListarTodo(fechaInicio, fechaFin);
        }
        public ResultDTO<CitasDTO> ListarxCodigo(string Codigo)
        {
            return oCitasDAO.ListarxCodigo(Codigo);
        }
        public ResultDTO<CitasDTO> ListarxID(int idCita)
        {
            return oCitasDAO.ListarxID(idCita);
        }
        public ResultDTO<CitasDTO> ListarTodoMedicos(int id, DateTime fechaIni, DateTime fechaFin)
        {
            return oCitasDAO.ListarTodoMedicos(id, fechaIni, fechaFin);
        }
        public ResultDTO<CalendarEventDTO> UpdateInsert(CitasDTO oCitasDTO)
        {
            return oCitasDAO.UpdateInsert(oCitasDTO);
        }

        public ResultDTO<CitasDTO> Delete(CitasDTO oCitasDTO)
        {
            return oCitasDAO.Delete(oCitasDTO);
        }
        public ResultDTO<CalendarEventDTO> ListarEventos(string fechaInicio, string fechaFin, int idRol, int idUsuario)
        {
            return oCitasDAO.ListarEventos(fechaInicio, fechaFin, idRol, idUsuario);
        }
        public ResultDTO<CitasDTO> ListarCitasXPagar()
        {
            return oCitasDAO.ListarCitasXPagar();
        }
        public ResultDTO<CitasDTO> ListarCitas_Atencion(int idPaciente)
        {
            return oCitasDAO.ListarCitas_Atencion(idPaciente);
        }
        public ResultDTO<CitasDTO> ValidarCita(CitasDTO oCitasDTO)
        {
            return oCitasDAO.ValidarCita(oCitasDTO.idCita);
        }
        public ResultDTO<CitasDTO> ListarCitasEnVenta()
        {
            return oCitasDAO.ListarCitasEnVenta();
        }
        public ResultDTO<CitasDTO> ListarDetalleCitaxId(int idCita)
        {
            return oCitasDAO.ListarDetalleCitaxId(idCita);
        }
        public ResultDTO<CitasDTO> GrabarComisionMedico(CitasDTO oCitasDTO)
        {
            return oCitasDAO.GrabarComisionMedico(oCitasDTO);
        }
        public ResultDTO<CalendarEventDTO> ConfirmarCita(CitasDTO oCitasDTO)
        {
            return oCitasDAO.ConfirmarCita(oCitasDTO);
        }
        public ResultDTO<CitasDTO> Listar_ComisionMedico(int id, DateTime fechaIni, DateTime fechaFin)
        {
            return oCitasDAO.Listar_ComisionMedico(id, fechaIni, fechaFin);
        }
        public ResultDTO<CitasDTO> Listar_ComisionMedico_Detallado(int id, DateTime fechaIni, DateTime fechaFin)
        {
            return oCitasDAO.Listar_ComisionMedico_Detallado(id, fechaIni, fechaFin);
        }
        public ResultDTO<CitasDTO> ObtenerCitasxPaciente(CitasDTO oCitasDTO)
        {
            return oCitasDAO.ObtenerCitasxPaciente(oCitasDTO);
        }
    }
}
