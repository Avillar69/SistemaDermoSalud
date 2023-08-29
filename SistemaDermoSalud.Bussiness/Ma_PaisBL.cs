using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
  public  class Ma_PaisBL
    {
        Ma_PaisDAO oMa_PaisDAO = new Ma_PaisDAO();
        public ResultDTO<Ma_PaisDTO> ListarTodo()
        {
            return oMa_PaisDAO.ListarTodo();
        }
        public ResultDTO<Ma_PaisDTO> ListarxID(int idPais)
        {
            return oMa_PaisDAO.ListarxID(idPais);
        }
        public ResultDTO<Ma_PaisDTO> UpdateInsert(Ma_PaisDTO oMa_PaisDTO)
        {
            return oMa_PaisDAO.UpdateInsert(oMa_PaisDTO);
        }
        public ResultDTO<Ma_PaisDTO> Delete(Ma_PaisDTO oMa_PaisDTO)
        {
            return oMa_PaisDAO.Delete(oMa_PaisDTO);
        }
    }
}
