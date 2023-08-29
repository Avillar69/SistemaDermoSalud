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
    public class Seg_RolBL
    {
        Seg_RolDAO oSeg_RolDAO = new Seg_RolDAO();
        public ResultDTO<Seg_RolDTO> ListarTodo(int idEmpresa)
        {
            return oSeg_RolDAO.ListarTodo(idEmpresa);
        }

        public ResultDTO<Seg_RolDTO> ListarxID(int idRol)
        {
            return oSeg_RolDAO.ListarxID(idRol);
        }

        public ResultDTO<Seg_RolDTO> UpdateInsert(Seg_RolDTO oSeg_RolDTO)
        {
            return oSeg_RolDAO.UpdateInsert(oSeg_RolDTO);
        }

        public ResultDTO<Seg_RolDTO> Delete(Seg_RolDTO oSeg_RolDTO)
        {
            return oSeg_RolDAO.Delete(oSeg_RolDTO);
        }
    }
}
