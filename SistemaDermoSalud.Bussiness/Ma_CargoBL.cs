using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public class Ma_CargoBL
    {
        Ma_CargoDAO oMa_CargoDAO = new Ma_CargoDAO();
        public ResultDTO<Ma_CargoDTO> ListarTodo(string Activo)
        {
            return oMa_CargoDAO.ListarTodo(Activo);
        }

        public ResultDTO<Ma_CargoDTO> ListarxID(int idCargo)
        {
            return oMa_CargoDAO.ListarxID(idCargo);
        }

        public ResultDTO<Ma_CargoDTO> UpdateInsert(Ma_CargoDTO oMa_CargoDTO)
        {
            return oMa_CargoDAO.UpdateInsert(oMa_CargoDTO);
        }

        public ResultDTO<Ma_CargoDTO> Delete(Ma_CargoDTO oMa_CargoDTO)
        {
            return oMa_CargoDAO.Delete(oMa_CargoDTO);
        }


    }
}
