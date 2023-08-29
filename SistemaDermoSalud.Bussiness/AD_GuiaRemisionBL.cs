using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
    public class AD_GuiaRemisionBL
    {
        AD_GuiaRemisionDAO oAD_GuiaRemisionDAO = new AD_GuiaRemisionDAO();
        public ResultDTO<AD_GuiaRemisionDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin)
        {
            return oAD_GuiaRemisionDAO.ListarRangoFecha(idEmpresa, fechaInicio, fechaFin);
        }
        public ResultDTO<AD_GuiaRemisionDTO> ListarxID(int idGuiaRemision)
        {
            return oAD_GuiaRemisionDAO.ListarxID(idGuiaRemision);
        }
        public ResultDTO<AD_GuiaRemisionDTO> UpdateInsert(AD_GuiaRemisionDTO oAD_GuiaRemisionDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oAD_GuiaRemisionDAO.UpdateInsert(oAD_GuiaRemisionDTO, fechaInicio, fechaFin);
        }
        public ResultDTO<AD_GuiaRemisionDTO> Delete(AD_GuiaRemisionDTO oAD_GuiaRemisionDTO, DateTime fechaInicio, DateTime fechaFin, int idUsuario)
        {
            return oAD_GuiaRemisionDAO.Delete(oAD_GuiaRemisionDTO, fechaInicio, fechaFin, idUsuario);
        }
        public ResultDTO<AD_GuiaRemisionDTO> cargarGuias(int idEmpresa)
        {
            return oAD_GuiaRemisionDAO.cargarGuias(idEmpresa);
        }
        public ResultDTO<AD_GuiaRemisionDetalleDTO> cargarDetalleGuias(int idCompra)
        {
            return oAD_GuiaRemisionDAO.cargarDetalleGuias(idCompra);
        }

    }
}
