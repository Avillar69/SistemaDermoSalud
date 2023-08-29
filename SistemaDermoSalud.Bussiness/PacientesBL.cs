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
    public class PacientesBL
    {
        PacientesDAO oPacientesDAO = new PacientesDAO();
        public ResultDTO<PacientesDTO>ListarTodo()
        {
            return oPacientesDAO.ListarTodo();
        }

        public ResultDTO<PacientesDTO> ListarxID(int idPaciente)
        {
        return oPacientesDAO.ListarxID(idPaciente);
        }

        public ResultDTO<PacientesDTO> UpdateInsert(PacientesDTO oPacientesDTO)
        {
            return oPacientesDAO.UpdateInsert( oPacientesDTO);
        }

        public ResultDTO<PacientesDTO> Delete(PacientesDTO oPacientesDTO)
        {
            return oPacientesDAO.Delete( oPacientesDTO);
        }
        public ResultDTO<PacientesDTO> ListarTodo_HistoriaClinica()
        {
            return oPacientesDAO.ListarTodo_HistoriaClinica();
        }
        public ResultDTO<PacientesDTO> ListarPacientes_SinHistoria()
        {
            return oPacientesDAO.ListarPacientes_SinHistoria();
        }
    }
}
