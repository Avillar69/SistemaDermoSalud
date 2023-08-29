using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
    public class FN_PagosDetalleBL
    {
        FN_PagosDetalleDAO oFN_PagosDetalleDAO = new FN_PagosDetalleDAO();
        public ResultDTO<FN_PagosDetalleDTO> ListarTodo(int idEmpresa)
        {
            return oFN_PagosDetalleDAO.ListarTodo(idEmpresa);
        }

        public ResultDTO<FN_PagosDetalleDTO> ListarxID(int idPagoDetalle)
        {
            return oFN_PagosDetalleDAO.ListarxID(idPagoDetalle);
        }

        public ResultDTO<FN_PagosDetalleDTO> UpdateInsert(FN_PagosDetalleDTO oFN_PagosDetalleDTO)
        {
            return oFN_PagosDetalleDAO.UpdateInsert(oFN_PagosDetalleDTO);
        }

        public ResultDTO<FN_PagosDetalleDTO> Delete(FN_PagosDetalleDTO oFN_PagosDetalleDTO)
        {
            return oFN_PagosDetalleDAO.Delete(oFN_PagosDetalleDTO);
        }


    }
}
