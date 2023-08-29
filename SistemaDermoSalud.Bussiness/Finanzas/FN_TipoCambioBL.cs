using SistemaDermoSalud.DataAccess.Finanzas;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Finanzas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business.Finanzas
{
   public  class FN_TipoCambioBL
    {

        public ResultDTO<FN_TipoCambioDTO> ListarTodo(int idEmpresa, string fechaInicio, string fechaFin)
        {
            return new FN_TipoCambioDAO().ListarTodo(idEmpresa, fechaInicio, fechaFin);
        }
        public ResultDTO<Ma_MonedaDTO> ListarMonedaTipoCambio(int idEmpresa)
        {
            return new FN_TipoCambioDAO().ListarMonedaTipoCambio(idEmpresa);
        }
        public ResultDTO<FN_TipoCambioDTO> UpdateInsert(FN_TipoCambioDTO oFN_TipoCambioDTO)
        {
            return new FN_TipoCambioDAO().UpdateInsert(oFN_TipoCambioDTO);
        }
        public ResultDTO<FN_TipoCambioDTO> Delete(FN_TipoCambioDTO oFN_TipoCambioDTO)
        {
            return new FN_TipoCambioDAO().Delete(oFN_TipoCambioDTO);
        }
        public ResultDTO<FN_TipoCambioDTO> ListarTipoXFechaUnica(string oFN_TipoCambioDTO)
        {
            return new FN_TipoCambioDAO().ListarTipoXFechaUnica(oFN_TipoCambioDTO);
        }
        public ResultDTO<FN_TipoCambioDTO> ListarTipoXFechaActual(string oFN_TipoCambioDTO)
        {
            return new FN_TipoCambioDAO().ListarTipoXFechaActual(oFN_TipoCambioDTO);
        }

    }
}
