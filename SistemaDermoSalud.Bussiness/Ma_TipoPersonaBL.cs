using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
    public class Ma_TipoPersonaBL
    {
        Ma_TipoPersonaDAO oMa_TipoPersonaDAO = new Ma_TipoPersonaDAO();
        public ResultDTO<Ma_TipoPersonaDTO> ListarTodo()
        {
            return oMa_TipoPersonaDAO.ListarTodo();
        }
        public ResultDTO<Ma_TipoPersonaDTO> ListarxID(int idTipoPersona)
        {
            return oMa_TipoPersonaDAO.ListarxID(idTipoPersona);
        }
        public ResultDTO<Ma_TipoPersonaDTO> UpdateInsert(Ma_TipoPersonaDTO oMa_TipoPersonaDTO)
        {
            return oMa_TipoPersonaDAO.UpdateInsert(oMa_TipoPersonaDTO);
        }
        public ResultDTO<Ma_TipoPersonaDTO> Delete(Ma_TipoPersonaDTO oMa_TipoPersonaDTO)
        {
            return oMa_TipoPersonaDAO.Delete(oMa_TipoPersonaDTO);
        }
    }
}
