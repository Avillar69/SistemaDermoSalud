using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Finanzas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business.Finanzas
{
   public  class FN_PagoBL
    {
        FN_PagosDAO oFN_PagosDAO = new FN_PagosDAO();
        public ResultDTO<FN_PagosDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin)
        {
            return oFN_PagosDAO.ListarRangoFecha(idEmpresa, fechaInicio, fechaFin);
        }
        public ResultDTO<FN_PagosDTO> ListarTodo(int idEmpresa)
        {
            return oFN_PagosDAO.ListarTodo(idEmpresa);
        }

        public ResultDTO<FN_PagosDTO> ListarxID(int idOrdenCompra)
        {
            return oFN_PagosDAO.ListarxID(idOrdenCompra);
        }

        public ResultDTO<FN_PagosDTO> UpdateInsert(FN_PagosDTO oCOM_OrdenCompraDTO, DateTime FechaInicio, DateTime FechaFin)
        {
            return oFN_PagosDAO.UpdateInsert(oCOM_OrdenCompraDTO, FechaInicio, FechaFin);
        }

        public ResultDTO<FN_PagosDTO> Delete(FN_PagosDTO oCOM_OrdenCompraDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oFN_PagosDAO.Delete(oCOM_OrdenCompraDTO, fechaInicio, fechaFin);
        }
        //------------------------------------Lista de Orden  de cOmpra---------------------


        public ResultDTO<FN_PagosDTO> ListarOrdenCompra(int OrdenCompra)
        {
            return oFN_PagosDAO.ListarOrdenCompra(OrdenCompra);
        }


        public ResultDTO<FN_PagosDTO> ListarPagos(int id)
        {
            return oFN_PagosDAO.ListarPagos(id);
        }
        public ResultDTO<FN_PagosDTO> ListarxIDInnerCompras(int idOrdenCompra)
        {
            return oFN_PagosDAO.ListarxIDInnerCompras(idOrdenCompra);
        }
        public ResultDTO<FN_PagosDTO> ListarxIDInnerVentas(int idOrdenCompra)
        {
            return oFN_PagosDAO.ListarxIDInnerVentas(idOrdenCompra);
        }

        public ResultDTO<FN_PagosDTO> ListarPagosPendientes(int catDocumento)
        {
            return oFN_PagosDAO.ListarPagosPendientes(catDocumento);
        }
    }
}
