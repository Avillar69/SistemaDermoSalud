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
    public class ServiciosBL
    {
        ServiciosDAO oServiciosDAO = new ServiciosDAO();
        public ResultDTO<ServiciosDTO>ListarTodo()
        {
            return oServiciosDAO.ListarTodo();
        }

        public ResultDTO<ServiciosDTO> ListarxID(int idServicio)
        {
        return oServiciosDAO.ListarxID(idServicio);
        }

        public ResultDTO<ServiciosDTO> UpdateInsert(ServiciosDTO oServiciosDTO)
        {
            return oServiciosDAO.UpdateInsert( oServiciosDTO);
        }

        public ResultDTO<ServiciosDTO> Delete(ServiciosDTO oServiciosDTO)
        {
            return oServiciosDAO.Delete( oServiciosDTO);
        }
        public ResultDTO<ServiciosDTO> UpdateInsertPrecio(ServiciosDTO oServiciosDTO)
        {
            return oServiciosDAO.UpdateInsertPrecio(oServiciosDTO);
        }
    }
}
