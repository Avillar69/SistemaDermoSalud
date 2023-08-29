using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public class Ma_EstadoBL
    {
        Ma_EstadoDAO oMa_EstadoDAO = new Ma_EstadoDAO();
        public ResultDTO<Ma_EstadoDTO> ListarTodo()
        {
            return oMa_EstadoDAO.ListarTodo();
        }
        public ResultDTO<Ma_EstadoDTO> ListarxID(int idEstado)
        {
            return oMa_EstadoDAO.ListarxID(idEstado);
        }
        public ResultDTO<Ma_EstadoDTO> UpdateInsert(Ma_EstadoDTO oMa_EstadoDTO)
        {
            return oMa_EstadoDAO.UpdateInsert(oMa_EstadoDTO);
        }
        public ResultDTO<Ma_EstadoDTO> Delete(Ma_EstadoDTO oMa_EstadoDTO)
        {
            return oMa_EstadoDAO.Delete(oMa_EstadoDTO);
        }
        public ResultDTO<Ma_EstadoDTO> ListarxModulo(string Modulo)
        {
            return oMa_EstadoDAO.ListarxModulo(Modulo);
        }
    }
}
