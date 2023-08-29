using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public class Ma_UbigeoBL
    {
        public ResultDTO<Ma_UbigeoDTO> ListarDepartamentos()
        {
            return oMa_UbigeoDAO.ListarDepartamentos();
        }
        Ma_UbigeoDAO oMa_UbigeoDAO = new Ma_UbigeoDAO();

        public ResultDTO<Ma_UbigeoDTO> ListarProvincias()
        {
            return oMa_UbigeoDAO.ListarProvincias();
        }
        public ResultDTO<Ma_UbigeoDTO> ListarDistritos()
        {
            return oMa_UbigeoDAO.ListarDistritos();
        }
    }
}
