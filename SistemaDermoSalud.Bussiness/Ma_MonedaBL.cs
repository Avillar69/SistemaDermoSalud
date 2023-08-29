using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
  public class Ma_MonedaBL
    {
        Ma_MonedaDAO oMa_MonedaDAO = new Ma_MonedaDAO();
        public ResultDTO<Ma_MonedaDTO> ListarTodo(int idEmpresa)
        {
            return oMa_MonedaDAO.ListarTodo(idEmpresa);
        }

        public ResultDTO<Ma_MonedaDTO> ListarxID(int idMoneda)
        {
            return oMa_MonedaDAO.ListarxID(idMoneda);
        }

        public ResultDTO<Ma_MonedaDTO> UpdateInsert(Ma_MonedaDTO oMa_MonedaDTO)
        {
            return oMa_MonedaDAO.UpdateInsert(oMa_MonedaDTO);
        }

        public ResultDTO<Ma_MonedaDTO> Delete(Ma_MonedaDTO oMa_MonedaDTO)
        {
            return oMa_MonedaDAO.Delete(oMa_MonedaDTO);
        }

    }
}
