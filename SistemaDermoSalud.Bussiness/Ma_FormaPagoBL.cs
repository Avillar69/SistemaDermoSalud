using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public class Ma_FormaPagoBL
    {
        Ma_FormaPagoDAO oMa_FormaPagoDAO = new Ma_FormaPagoDAO();
        public ResultDTO<Ma_FormaPagoDTO> ListarTodo(int idEmpresa, string Activo)
        {
            return oMa_FormaPagoDAO.ListarTodo(idEmpresa, Activo);
        }

        public ResultDTO<Ma_FormaPagoDTO> ListarxID(int idFormaPago)
        {
            return oMa_FormaPagoDAO.ListarxID(idFormaPago);
        }

        public ResultDTO<Ma_FormaPagoDTO> UpdateInsert(Ma_FormaPagoDTO oMa_FormaPagoDTO)
        {
            return oMa_FormaPagoDAO.UpdateInsert(oMa_FormaPagoDTO);
        }

        public ResultDTO<Ma_FormaPagoDTO> Delete(Ma_FormaPagoDTO oMa_FormaPagoDTO)
        {
            return oMa_FormaPagoDAO.Delete(oMa_FormaPagoDTO);
        }





    }
}
