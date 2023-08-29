using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public class DashboardBL
    {
        public ResultDTO<DashboardDTO> ListarTodo()
        {
            return new DashboardDAO().ListarTodo();
        }
        public ResultDTO<TipoServicioDTO> ListarTodoCirculo(DateTime fechaInicio,DateTime fechaFin )
        {
            return new DashboardDAO().ListarTodoCirculo(fechaInicio, fechaFin);
        }
    }
}
