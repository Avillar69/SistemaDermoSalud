using SistemaDermoSalud.DataAccess.Mantenimiento;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
    public class Ma_UnidadMedidaBL
    {
        Ma_UnidadMedidaDAO oMa_UnidadMedidaDAO = new Ma_UnidadMedidaDAO();
        public ResultDTO<Ma_UnidadMedidaDTO> ListarTodo(int idEmpresa, string Activo = "")
        {
            return oMa_UnidadMedidaDAO.ListarTodo(idEmpresa, Activo);
        }

        public ResultDTO<Ma_UnidadMedidaDTO> ListarxID(int idUnidadMedida)
        {
            return oMa_UnidadMedidaDAO.ListarxID(idUnidadMedida);
        }

        public ResultDTO<Ma_UnidadMedidaDTO> UpdateInsert(Ma_UnidadMedidaDTO oMa_UnidadMedidaDTO)
        {
            return oMa_UnidadMedidaDAO.UpdateInsert(oMa_UnidadMedidaDTO);
        }

        public ResultDTO<Ma_UnidadMedidaDTO> Delete(Ma_UnidadMedidaDTO oMa_UnidadMedidaDTO)
        {
            return oMa_UnidadMedidaDAO.Delete(oMa_UnidadMedidaDTO);
        }


    }
}
