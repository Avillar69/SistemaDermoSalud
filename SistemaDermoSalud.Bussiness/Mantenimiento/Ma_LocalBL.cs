using SistemaDermoSalud.DataAccess.Mantenimiento;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business.Mantenimiento
{
    public class Ma_LocalBL
    {
        Ma_LocalDAO oMa_LocalDAO = new Ma_LocalDAO();
        public ResultDTO<Ma_LocalDTO> ListarTodo(int idEmpresa)
        {
            return oMa_LocalDAO.ListarTodo(idEmpresa);
        }

        public ResultDTO<Ma_LocalDTO> ListarxID(int idLocal)
        {
            return oMa_LocalDAO.ListarxID(idLocal);
        }

        public ResultDTO<Ma_LocalDTO> UpdateInsert(Ma_LocalDTO oMa_LocalDTO)
        {
            return oMa_LocalDAO.UpdateInsert(oMa_LocalDTO);
        }

        public ResultDTO<Ma_LocalDTO> Delete(Ma_LocalDTO oMa_LocalDTO)
        {
            return oMa_LocalDAO.Delete(oMa_LocalDTO);
        }
    }
}
