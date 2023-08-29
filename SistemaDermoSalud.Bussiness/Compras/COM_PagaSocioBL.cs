using SistemaDermoSalud.DataAccess.Compras;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Compras;
using SistemaDermoSalud.Entities.Finanzas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business.Compras
{
    public class COM_PagaSocioBL
    {

        COM_PagaSocioDAO oCOM_PagaSocioDAO = new COM_PagaSocioDAO();
        public ResultDTO<COM_PagaSocioDTO> ListarTodo()
        {
            return oCOM_PagaSocioDAO.ListarTodo();
        }

        public ResultDTO<FN_PagosDetalle> UpdateInsert(FN_PagosDetalle oCOM_PagaSocioDTO, DateTime FechaInicio, DateTime FechaFin)
        {
            return oCOM_PagaSocioDAO.UpdateInsert(oCOM_PagaSocioDTO, FechaInicio, FechaFin);
        }

        public ResultDTO<FN_PagosDetalle> ListarTodoDetallePago()
        {
            return oCOM_PagaSocioDAO.ListarTodoDetallePago();
        }

        public ResultDTO<FN_PagosDetalle> ListarXIDPagoDetalle(int idPagoDetalle)
        {
            return oCOM_PagaSocioDAO.ListarXIDPagoDetalle(idPagoDetalle);
        }

        public ResultDTO<FN_PagosDetalle> Delete(FN_PagosDetalle oCOM_OrdenCompraDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oCOM_PagaSocioDAO.Delete(oCOM_OrdenCompraDTO, fechaInicio, fechaFin);
        }
        public ResultDTO<COM_CuentasUsuariosDTO> ListarTodoProveedores(int id)
        {
            return oCOM_PagaSocioDAO.ListarTodoProveedores(id);
        }
        public ResultDTO<FN_PagosDetalle> BuscarPago(int id, int idEmpresa)
        {
            return oCOM_PagaSocioDAO.BuscarPago(id, idEmpresa);
        }

        public ResultDTO<FN_PagosDetalle> ListarRangoFecha(int idEmpresa, DateTime FechaInicio, DateTime FechaFin)
        {
            return oCOM_PagaSocioDAO.ListarRangoFecha(idEmpresa, FechaInicio, FechaFin);
        }
    }
}
