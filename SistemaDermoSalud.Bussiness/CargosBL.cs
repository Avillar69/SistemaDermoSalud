using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
    public class CargosBL
    {
        public ResultDTO<CargosDTO> ListarTodo(int p)
        {
            return new CargosDAO().ListarTodo(p);
        }
        public ResultDTO<CargosDTO> ListarxID(int idCargo)
        {
            return new CargosDAO().ListarxID(idCargo);
        }
        public ResultDTO<CargosDTO> UpdateInsert(CargosDTO oCargosDTO)
        {
            return new CargosDAO().UpdateInsert(oCargosDTO);
        }
        public ResultDTO<CargosDTO> Delete(CargosDTO oCargosDTO)
        {
            return new CargosDAO().Delete(oCargosDTO);
        }
    }
}
