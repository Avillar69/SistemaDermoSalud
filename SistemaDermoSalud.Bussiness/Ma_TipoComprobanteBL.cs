using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
  public   class Ma_TipoComprobanteBL
    {
        public ResultDTO<Ma_TipoComprobanteDTO> ListarTodo()
        {
            return new Ma_TipoComprobanteDAO().ListarTodo();
        }

    }
}
