using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDermoSalud.DataAccess.Mantenimiento;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Entities;

namespace SistemaDermoSalud.Business.Mantenimiento
{
    public class Ma_TipoAfectacionBL
    {
        public ResultDTO<Ma_TipoAfectacionDTO> ListarTodo()
        {
            return new Ma_TipoAfectacionDAO().ListarTodo();
        }
        public ResultDTO<Ma_TipoAfectacionDTO> ListarxID(int idTipoAfectacion)
        {
            return new Ma_TipoAfectacionDAO().ListarxID(idTipoAfectacion);
        }
    }
}
