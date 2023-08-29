using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
  public  class COM_OrdenCompraBL
    {
        COM_OrdenCompraDAO oCOM_OrdenCompraDAO = new COM_OrdenCompraDAO();
        public ResultDTO<COM_OrdenCompraDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin)
        {
            return oCOM_OrdenCompraDAO.ListarRangoFecha(idEmpresa, fechaInicio, fechaFin);
        }
        public ResultDTO<COM_OrdenCompraDTO> ListarTodo()
        {
            return oCOM_OrdenCompraDAO.ListarTodo();
        }

        public ResultDTO<COM_OrdenCompraDTO> ListarxID(int idOrdenCompra)
        {
            return oCOM_OrdenCompraDAO.ListarxID(idOrdenCompra);
        }

        public ResultDTO<COM_OrdenCompraDTO> UpdateInsert(COM_OrdenCompraDTO oCOM_OrdenCompraDTO, DateTime FechaInicio, DateTime FechaFin)
        {
            return oCOM_OrdenCompraDAO.UpdateInsert(oCOM_OrdenCompraDTO, FechaInicio, FechaFin);
        }

        public ResultDTO<COM_OrdenCompraDTO> Delete(COM_OrdenCompraDTO oCOM_OrdenCompraDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oCOM_OrdenCompraDAO.Delete(oCOM_OrdenCompraDTO, fechaInicio, fechaFin);
        }
        //------------------------------------Lista de Orden  de cOmpra---------------------


        public ResultDTO<COM_OrdenCompraDTO> ListarOrdenCompra(int OrdenCompra)
        {
            return oCOM_OrdenCompraDAO.ListarOrdenCompra(OrdenCompra);
        }

    }
}
