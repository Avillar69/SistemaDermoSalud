using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public class AD_CuentaOrigenBL
    {
        AD_CuentaOrigenDAO oCOM_OrdenCompraDAO = new AD_CuentaOrigenDAO();


        public ResultDTO<AD_CuentaOrigenDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin)
        {
            return oCOM_OrdenCompraDAO.ListarRangoFecha(fechaInicio, fechaFin);
        }
        public ResultDTO<AD_CuentaOrigenDTO> ListarTodo()
        {
            return oCOM_OrdenCompraDAO.ListarTodo();
        }

        public ResultDTO<AD_CuentaOrigenDTO> ListarxID(int id)
        {
            return oCOM_OrdenCompraDAO.ListarxID(id);
        }

        public ResultDTO<AD_CuentaOrigenDTO> UpdateInsert(AD_CuentaOrigenDTO oCOM_OrdenCompraDTO, DateTime FechaInicio, DateTime FechaFin)
        {
            return oCOM_OrdenCompraDAO.UpdateInsert(oCOM_OrdenCompraDTO, FechaInicio, FechaFin);
        }

        public ResultDTO<AD_CuentaOrigenDTO> Delete(AD_CuentaOrigenDTO oCOM_OrdenCompraDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oCOM_OrdenCompraDAO.Delete(oCOM_OrdenCompraDTO, fechaInicio, fechaFin);

        }

    }
}
