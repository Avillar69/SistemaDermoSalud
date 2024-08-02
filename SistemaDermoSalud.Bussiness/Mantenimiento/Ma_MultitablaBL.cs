using SistemaDermoSalud.DataAccess.Mantenimiento;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business.Mantenimiento
{
    public class Ma_MultitablaBL
    {
        Ma_MultitablaDAO oMultitablaDAO = new Ma_MultitablaDAO();

        public ResultDTO<Ma_MultitablaDTO> ListarTodo(string tabla)
        {
            return oMultitablaDAO.ListarTodo(tabla);
        }

        public ResultDTO<Ma_MultitablaDTO> ListarxID(int id)
        {
            return oMultitablaDAO.ListarxID(id);
        }
        public ResultDTO<Ma_MultitablaDTO> UpdateInsert(Ma_MultitablaDTO oMultitablaDTO)
        {
            return oMultitablaDAO.UpdateInsert(oMultitablaDTO);
        }

        public ResultDTO<Ma_MultitablaDTO> Delete(Ma_MultitablaDTO oMultitablaDTO)
        {
            return oMultitablaDAO.Delete(oMultitablaDTO);
        }
    }
}
