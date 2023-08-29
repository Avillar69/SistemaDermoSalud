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
    public class LaboratorioBL
    {
        LaboratorioDAO oLaboratorioDAO = new LaboratorioDAO();

        public ResultDTO<LaboratorioDTO>ListarTodo(int p)
        {
            return oLaboratorioDAO.ListarTodo(p);
        }

        public ResultDTO<LaboratorioDTO> ListarxID(int idLaboratorio)
        {
            return oLaboratorioDAO.ListarxID(idLaboratorio);
        }
        public ResultDTO<LaboratorioDTO> UpdateInsert(LaboratorioDTO oLaboratorioDTO)
        {
            return oLaboratorioDAO.UpdateInsert(oLaboratorioDTO);
        }

        public ResultDTO<LaboratorioDTO> Delete(LaboratorioDTO oLaboratorio)
        {
            return oLaboratorioDAO.Delete(oLaboratorio);
        }
    }
}
