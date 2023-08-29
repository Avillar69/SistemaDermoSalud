using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public class Ma_Clase_ArticuloBL
    {
        Ma_Clase_ArticuloDAO oMa_Clase_ArticuloDAO = new Ma_Clase_ArticuloDAO();
        public ResultDTO<Ma_Clase_ArticuloDTO> ListarTodo(int idEmpresa, string Activo)
        {
            return oMa_Clase_ArticuloDAO.ListarTodo(idEmpresa, Activo);
        }

        public ResultDTO<Ma_Clase_ArticuloDTO> ListarxID(int idClaseArticulo)
        {
            return oMa_Clase_ArticuloDAO.ListarxID(idClaseArticulo);
        }

        public ResultDTO<Ma_Clase_ArticuloDTO> UpdateInsert(Ma_Clase_ArticuloDTO oMa_Clase_ArticuloDTO)
        {
            return oMa_Clase_ArticuloDAO.UpdateInsert(oMa_Clase_ArticuloDTO);
        }

        public ResultDTO<Ma_Clase_ArticuloDTO> Delete(Ma_Clase_ArticuloDTO oMa_Clase_ArticuloDTO)
        {
            return oMa_Clase_ArticuloDAO.Delete(oMa_Clase_ArticuloDTO);
        }



    }
}
