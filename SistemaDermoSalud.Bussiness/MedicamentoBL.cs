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
    public class MedicamentoBL
    {
        MedicamentoDAO oMedicamentoDAO = new MedicamentoDAO();
        public ResultDTO<MedicamentoDTO> ListarTodo(int p)
        {
            return oMedicamentoDAO.ListarTodo(p);
        }
        public ResultDTO<MedicamentoDTO> ListarporLaboratorio(int p)
        {
            return oMedicamentoDAO.ListarporLaboratorio(p);
        }

        public ResultDTO<MedicamentoDTO> ListarxID(int idMedicamentos)
        {
            return oMedicamentoDAO.ListarxID(idMedicamentos);
        }

        public ResultDTO<MedicamentoDTO> UpdateInsert(MedicamentoDTO oMedicamentoDTO)
        {
            return oMedicamentoDAO.UpdateInsert(oMedicamentoDTO);
        }

        public ResultDTO<MedicamentoDTO> Delete(MedicamentoDTO oMedicamentoDTO)
        {
            return oMedicamentoDAO.Delete(oMedicamentoDTO);
        }
    }
}
