using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
    public class TipoServicioBL
    {
        public ResultDTO<TipoServicioDTO> ListarTodo()
        {
            return new TipoServicioDAO().ListarTodo();
        }
        public ResultDTO<TipoServicioDTO> ListarxID(int idTipoServicio)
        {
            return new TipoServicioDAO().ListarxID(idTipoServicio);
        }
        public ResultDTO<TipoServicioDTO> UpdateInsert(TipoServicioDTO oTipoServicioDTO)
        {
            return new TipoServicioDAO().UpdateInsert(oTipoServicioDTO);
        }
        public ResultDTO<TipoServicioDTO> Delete(TipoServicioDTO oTipoServicioDTO)
        {
            return new TipoServicioDAO().Delete(oTipoServicioDTO);
        }
        public ResultDTO<TipoServicioDTO> ListarTipoServicio()
        {
            return new TipoServicioDAO().ListarPorTipoServicio();
        }



    }
}
