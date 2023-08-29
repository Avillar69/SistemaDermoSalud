using SistemaDermoSalud.DataAccess.Mantenimiento;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business.Mantenimiento
{
  public  class Ma_EmpresaBL
    {
        Ma_EmpresaDAO oMa_EmpresaDAO = new Ma_EmpresaDAO();
        public ResultDTO<Ma_EmpresaDTO> ListarTodo()
        {
            return oMa_EmpresaDAO.ListarTodo();
        }
        public ResultDTO<Ma_EmpresaDTO> ListarxID(int idEmpresa)
        {
            return oMa_EmpresaDAO.ListarxID(idEmpresa);
        }
        public ResultDTO<Ma_EmpresaDTO> UpdateInsert(Ma_EmpresaDTO oMa_EmpresaDTO)
        {
            return oMa_EmpresaDAO.UpdateInsert(oMa_EmpresaDTO);
        }
        public ResultDTO<Ma_EmpresaDTO> Delete(Ma_EmpresaDTO oMa_EmpresaDTO)
        {
            return oMa_EmpresaDAO.Delete(oMa_EmpresaDTO);
        }


    }
}
