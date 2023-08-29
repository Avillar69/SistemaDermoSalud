using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.DataAccess;

namespace SistemaDermoSalud.Business
{
    public class MonedaBL
    {
        MonedaDAO oMonedaDAO = new MonedaDAO();
        public ResultDTO<MonedaDTO>ListarTodo()
        {
            return oMonedaDAO.ListarTodo();
        }

        public ResultDTO<MonedaDTO> ListarxID(int idMoneda)
        {
        return oMonedaDAO.ListarxID(idMoneda);
        }

        public ResultDTO<MonedaDTO> UpdateInsert(MonedaDTO oMonedaDTO)
        {
            return oMonedaDAO.UpdateInsert( oMonedaDTO);
        }

        public ResultDTO<MonedaDTO> Delete(MonedaDTO oMonedaDTO)
        {
            return oMonedaDAO.Delete( oMonedaDTO);
        }
    }
}
