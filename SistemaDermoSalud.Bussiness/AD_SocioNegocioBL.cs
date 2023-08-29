using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
    public class AD_SocioNegocioBL
    {
        AD_SocioNegocioDAO oAD_SocioNegocioDAO = new AD_SocioNegocioDAO();
        public ResultDTO<AD_SocioNegocioDTO> ListarTodo(int idEmpresa)
        {
            return oAD_SocioNegocioDAO.ListarTodo(idEmpresa);
        }
        public ResultDTO<AD_SocioNegocioDTO> ListarxID(int idSocioNegocio)
        {
            return oAD_SocioNegocioDAO.ListarxID(idSocioNegocio);
        }
        public ResultDTO<AD_SocioNegocioDTO> UpdateInsert(AD_SocioNegocioDTO oAD_SocioNegocioDTO, int idUsuario)
        {
            return oAD_SocioNegocioDAO.UpdateInsert(oAD_SocioNegocioDTO, idUsuario);
        }
        public ResultDTO<AD_SocioNegocioDTO> UpdateInsertCli(AD_SocioNegocioDTO oAD_SocioNegocioDTO, int idUsuario)
        {
            return oAD_SocioNegocioDAO.UpdateInsertCli(oAD_SocioNegocioDTO, idUsuario);
        }
        public ResultDTO<AD_SocioNegocioDTO> Delete(AD_SocioNegocioDTO oAD_SocioNegocioDTO, int idUsuario)
        {
            return oAD_SocioNegocioDAO.Delete(oAD_SocioNegocioDTO, idUsuario);
        }
        public ResultDTO<AD_SocioNegocioDTO> DeleteCli(AD_SocioNegocioDTO oAD_SocioNegocioDTO, int idUsuario)
        {
            return oAD_SocioNegocioDAO.DeleteCli(oAD_SocioNegocioDTO, idUsuario);
        }
        public ResultDTO<AD_SocioNegocioDTO> ListarProv(int idEmpresa, string tipo, int idTipoComprobante = 0)
        {
            return oAD_SocioNegocioDAO.ListarProv(idEmpresa, tipo, idTipoComprobante);
        }

        /*---------------------------------------metodos creados 3/04/2018-----------------------------------*/
        public ResultDTO<AD_SocioNegocioDTO> ListarTodoCliente(int idEmpresa)
        {
            return oAD_SocioNegocioDAO.ListarPorTipoCliente(idEmpresa);
        }
        public ResultDTO<AD_SocioNegocioDTO> ListarTodoProveedores(int idEmpresa)
        {
            return oAD_SocioNegocioDAO.ListarPorTipoProveedor(idEmpresa);
        }
        public ResultDTO<ContribuyenteDTO> ConsutaRUC(string ruc)
        {
            return oAD_SocioNegocioDAO.ConsultaRUC(ruc);
        }
    }
}
