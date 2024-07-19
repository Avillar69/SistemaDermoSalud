using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.DataAccess.Mantenimiento;
using SistemaDermoSalud.Entities;

namespace SistemaDermoSalud.Business.Mantenimiento
{
    public class Ma_MarcaBL
    {
        Ma_MarcaDAO oMarcaDAO = new Ma_MarcaDAO();

        public ResultDTO<Ma_MarcaDTO>ListarTodo(int p)
        {
            return oMarcaDAO.ListarTodo(p);
        }

        public ResultDTO<Ma_MarcaDTO> ListarxID(int idMarca)
        {
            return oMarcaDAO.ListarxID(idMarca);
        }
        public ResultDTO<Ma_MarcaDTO> UpdateInsert(Ma_MarcaDTO oMarcaDTO)
        {
            return oMarcaDAO.UpdateInsert(oMarcaDTO);
        }

        public ResultDTO<Ma_MarcaDTO> Delete(Ma_MarcaDTO oMarca)
        {
            return oMarcaDAO.Delete(oMarca);
        }
    }
}
