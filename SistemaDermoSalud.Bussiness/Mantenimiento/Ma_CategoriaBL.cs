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
   public class Ma_CategoriaBL
    {

        Ma_CategoriaDAO oMa_CategoriaDAO = new Ma_CategoriaDAO();
        public ResultDTO<Ma_CategoriaDTO> ListarTodo(int idEmpresa, string Activo = "")
        {
            return oMa_CategoriaDAO.ListarTodo(idEmpresa, Activo);
        }
        public ResultDTO<Ma_CategoriaDTO> ListarTodoxTipo(int idEmpresa, int tipo, string Activo = "")
        {
            return oMa_CategoriaDAO.ListarTodoxTipo(idEmpresa, tipo, Activo);
        }
        public ResultDTO<Ma_CategoriaDTO> ListarxID(int idCategoria)
        {
            return oMa_CategoriaDAO.ListarxID(idCategoria);
        }

        public ResultDTO<Ma_CategoriaDTO> UpdateInsert(Ma_CategoriaDTO oMa_CategoriaDTO)
        {
            return oMa_CategoriaDAO.UpdateInsert(oMa_CategoriaDTO);
        }

        public ResultDTO<Ma_CategoriaDTO> Delete(Ma_CategoriaDTO oMa_CategoriaDTO)
        {
            return oMa_CategoriaDAO.Delete(oMa_CategoriaDTO);
        }
    }
}
