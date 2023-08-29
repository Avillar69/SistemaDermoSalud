using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public class Ma_TipoMovimientoBL
    {

        Ma_TipoMovimientoDAO oMa_TipoMovimientoDAO = new Ma_TipoMovimientoDAO();
        public ResultDTO<Entities.Ma_TipoMovimientoDTO> ListarTodo()
        {
            return oMa_TipoMovimientoDAO.ListarTodo();
        }

        public ResultDTO<Ma_TipoMovimientoDTO> ListarxID(int idTipoMovimiento)
        {
            return oMa_TipoMovimientoDAO.ListarxID(idTipoMovimiento);
        }

        public ResultDTO<Ma_TipoMovimientoDTO> UpdateInsert(Ma_TipoMovimientoDTO oMa_TipoMovimientoDTO)
        {
            return oMa_TipoMovimientoDAO.UpdateInsert(oMa_TipoMovimientoDTO);
        }

        public ResultDTO<Ma_TipoMovimientoDTO> Delete(Ma_TipoMovimientoDTO oMa_TipoMovimientoDTO)
        {
            return oMa_TipoMovimientoDAO.Delete(oMa_TipoMovimientoDTO);
        }

        public ResultDTO<Ma_TipoMovimientoDTO> ListarxTipo(string Tipo)
        {
            return oMa_TipoMovimientoDAO.ListarxTipo(Tipo);
        }
    }
}
