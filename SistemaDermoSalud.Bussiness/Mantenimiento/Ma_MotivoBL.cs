using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.DataAccess.Mantenimiento;

namespace SistemaDermoSalud.Business.Mantenimiento
{
    public class Ma_MotivoBL
    {
        Ma_MotivoDAO oMa_MotivoDAO = new Ma_MotivoDAO();
        public ResultDTO<Ma_MotivoDTO> ListarTodo()
        {
            return oMa_MotivoDAO.ListarTodo();
        }

        public ResultDTO<Ma_MotivoDTO> ListarTodoDebito()
        {
            return oMa_MotivoDAO.ListarTodoDebito();
        }
        public ResultDTO<Ma_MotivoDTO> ListarxID(int idMotivo)
        {
            return oMa_MotivoDAO.ListarxID(idMotivo);
        }
        public ResultDTO<Ma_MotivoDTO> ListarxID_Debito(int idMotivo)
        {
            return oMa_MotivoDAO.ListarxID_Debito(idMotivo);
        }
    }
}
