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
    public class Ma_MarcaBL
    {

        Ma_MarcaDAO oMa_MarcaDAO = new Ma_MarcaDAO();
        public ResultDTO<Ma_MarcaDTO> ListarTodo(int idEmpresa, string Activo = "")
        {
            return oMa_MarcaDAO.ListarTodo(idEmpresa, Activo);
        }
        public ResultDTO<Ma_MarcaDTO> ListarxID(int idMarca)
        {
            return oMa_MarcaDAO.ListarxID(idMarca);
        }
        public ResultDTO<Ma_MarcaDTO> UpdateInsert(Ma_MarcaDTO oMa_MarcaDTO)
        {
            ResultDTO<Ma_MarcaDTO> oResultDTO = oMa_MarcaDAO.UpdateInsert(oMa_MarcaDTO);
            return oResultDTO;
        }
        public ResultDTO<Ma_MarcaDTO> Delete(Ma_MarcaDTO oMa_MarcaDTO)
        {
            ResultDTO<Ma_MarcaDTO> oResultDTO = oMa_MarcaDAO.Delete(oMa_MarcaDTO);
            return oResultDTO;
        }

    }
}
