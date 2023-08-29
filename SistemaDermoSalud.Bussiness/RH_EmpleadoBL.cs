using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public class RH_EmpleadoBL
    {
        RH_EmpleadoDAO oRH_EmpleadoDAO = new RH_EmpleadoDAO();
        public ResultDTO<RH_EmpleadoDTO> ListarTodo()
        {
            return oRH_EmpleadoDAO.ListarTodo();
        }

        public ResultDTO<RH_EmpleadoDTO> ListarxID(int idEmpleado)
        {
            return oRH_EmpleadoDAO.ListarxID(idEmpleado);
        }

        public ResultDTO<RH_EmpleadoDTO> UpdateInsert(RH_EmpleadoDTO oRH_EmpleadoDTO, int idUsuario)
        {
            return oRH_EmpleadoDAO.UpdateInsert(oRH_EmpleadoDTO, idUsuario);
        }

        public ResultDTO<RH_EmpleadoDTO> Delete(RH_EmpleadoDTO oRH_EmpleadoDTO, int idUsuario)
        {
            return oRH_EmpleadoDAO.Delete(oRH_EmpleadoDTO, idUsuario);
        }
    }
}
