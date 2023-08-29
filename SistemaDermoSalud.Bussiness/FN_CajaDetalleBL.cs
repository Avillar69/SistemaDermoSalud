using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public  class FN_CajaDetalleBL
    {
        FN_CajaDetalleDAO oFN_CajaDetalleDAO = new FN_CajaDetalleDAO();
        public ResultDTO<FN_CajaDetalleDTO> ListarTodo(int idEmpresa)
        {
            return oFN_CajaDetalleDAO.ListarTodo(idEmpresa);
        }

        public ResultDTO<FN_CajaDetalleDTO> ListarxID(int idCajaDetalle)
        {
            return oFN_CajaDetalleDAO.ListarxID(idCajaDetalle);
        }

        public ResultDTO<FN_CajaDetalleDTO> UpdateInsert(FN_CajaDetalleDTO oFN_CajaDetalleDTO)
        {
            return oFN_CajaDetalleDAO.UpdateInsert(oFN_CajaDetalleDTO);
        }

        //public ResultDTO<FN_CajaDetalleDTO> Delete(FN_CajaDetalleDTO oFN_CajaDetalleDTO)
        public ResultDTO<FN_CajaDetalleDTO> Delete(int idCajaDetalle, int idCaja)
        {
            return oFN_CajaDetalleDAO.Delete(idCajaDetalle, idCaja);
        }
        public ResultDTO<FN_CajaDetalleDTO> ListarxIDCaja(int idCaja)
        {
            return oFN_CajaDetalleDAO.ListarxIDCaja(idCaja);
        }
        public ResultDTO<FN_CajaDetalleDTO> ReportexIDCaja(int idCaja)
        {
            return oFN_CajaDetalleDAO.ReportexIDCaja(idCaja);
        }

        public ResultDTO<FN_CajaDetalleDTO> ListarxIDCajaDetalle(int idCajaDetalle)
        {
            return oFN_CajaDetalleDAO.ListarxIDCajaDetalle(idCajaDetalle);
        }

    }
}
