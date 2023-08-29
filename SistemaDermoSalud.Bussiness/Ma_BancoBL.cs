using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
  public  class Ma_BancoBL
    {
        Ma_BancoDAO oMa_BancoDAO = new Ma_BancoDAO();
        public ResultDTO<Ma_BancoDTO> ListarTodo(int idEmpresa, string Activo)
        {
            return oMa_BancoDAO.ListarTodo(idEmpresa, Activo);
        }

        public ResultDTO<Ma_BancoDTO> ListarxID(int idBanco)
        {
            return oMa_BancoDAO.ListarxID(idBanco);
        }

        public ResultDTO<Ma_BancoDTO> UpdateInsert(Ma_BancoDTO oMa_BancoDTO)
        {
            return oMa_BancoDAO.UpdateInsert(oMa_BancoDTO);
        }

        public ResultDTO<Ma_BancoDTO> Delete(Ma_BancoDTO oMa_BancoDTO)
        {
            return oMa_BancoDAO.Delete(oMa_BancoDTO);
        }
        public ResultDTO<Ma_BancoDTO> ListaBancos()
        {
            return oMa_BancoDAO.ListaBancos();
        }
    }
}
