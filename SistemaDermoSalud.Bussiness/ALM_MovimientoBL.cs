using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public  class ALM_MovimientoBL
    {
        ALM_MovimientoDAO oALM_MovimientoDAO = new ALM_MovimientoDAO();
        public ResultDTO<ALM_MovimientoDTO> ListarTodo(int idEmpresa)
        {
            return oALM_MovimientoDAO.ListarTodo(idEmpresa);
        }
        public ResultDTO<ALM_MovimientoDTO> ListarxID(int idMovimiento)
        {
            return oALM_MovimientoDAO.ListarxID(idMovimiento);
        }
        public ResultDTO<ALM_MovimientoDTO> ListarRangoFecha(string TipoMovimiento, int idEmpresa, DateTime fechaInicio, DateTime fechaFin)
        {
            return oALM_MovimientoDAO.ListarRangoFecha(TipoMovimiento, idEmpresa, fechaInicio, fechaFin);
        }
        public ResultDTO<ALM_MovimientoDTO> UpdateInsert(ALM_MovimientoDTO oALM_MovimientoDTO)
        {
            return oALM_MovimientoDAO.UpdateInsert(oALM_MovimientoDTO);
        }
        public ResultDTO<ALM_MovimientoDTO> UpdateInsertT(ALM_MovimientoDTO oALM_MovimientoDTO)
        {
            return oALM_MovimientoDAO.UpdateInsertT(oALM_MovimientoDTO);
        }
        public ResultDTO<ALM_MovimientoDTO> Delete(ALM_MovimientoDTO oALM_MovimientoDTO)
        {
            return oALM_MovimientoDAO.Delete(oALM_MovimientoDTO);
        }

        public ResultDTO<ALM_MovimientoDTO> DeleteTrans(int oALM_MovimientoDTO, int idMovimientoReferencia)
        {
            return oALM_MovimientoDAO.DeleteTrans(oALM_MovimientoDTO, idMovimientoReferencia);
        }

        public ResultDTO<ALM_MovimientoDTO> ListarxTipo(string TipoMovimiento, int idEmpresa)
        {
            return oALM_MovimientoDAO.ListarxTipo(TipoMovimiento, idEmpresa);
        }

        public ResultDTO<ALM_MovimientoDTO> ListarTodoTransformaciones(int idEmpresa)
        {
            return oALM_MovimientoDAO.ListarTodoTransformaciones(idEmpresa);
        }
        public List<Object> ListarTransferenciaSalida(int idEmpresa, int idLocal, int idAlmacen)
        {
            return oALM_MovimientoDAO.ListarTransferenciaSalida(idEmpresa, idLocal, idAlmacen);
        }
    }
}
