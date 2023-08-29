using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public class VN_PedidosBL
    {


        VEN_PedidosDAO oVEN_PedidosDAO = new VEN_PedidosDAO();
        public ResultDTO<VEN_PedidosDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_PedidosDAO.ListarRangoFecha(idEmpresa, fechaInicio, fechaFin);
        }
        public ResultDTO<VEN_PedidosDTO> ListarTodo(int idEmpresa)
        {
            return oVEN_PedidosDAO.ListarTodo(idEmpresa);
        }

        public ResultDTO<VEN_PedidosDTO> ListarxID(int idPedido)
        {
            return oVEN_PedidosDAO.ListarxID(idPedido);
        }

        public ResultDTO<VEN_PedidosDTO> UpdateInsert(VEN_PedidosDTO oVEN_PedidosDTO, DateTime FechaInicio, DateTime FechaFin)
        {
            return oVEN_PedidosDAO.UpdateInsert(oVEN_PedidosDTO, FechaInicio, FechaFin);
        }

        public ResultDTO<VEN_PedidosDTO> Delete(VEN_PedidosDTO oVEN_PedidosDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_PedidosDAO.Delete(oVEN_PedidosDTO, fechaInicio, fechaFin);
        }
        //------------------------------------Lista de Orden  de cOmpra---------------------


        public ResultDTO<VEN_PedidosDTO> ListarOrdenCompra(int OrdenCompra)
        {
            return oVEN_PedidosDAO.ListarOrdenCompra(OrdenCompra);
        }

    }
}
