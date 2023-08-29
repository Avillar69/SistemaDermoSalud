using SistemaDermoSalud.DataAccess.Mantenimiento;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business.Mantenimiento
{
    public class Ma_ArticuloBL
    {

        Ma_ArticuloDAO oMa_ArticuloDAO = new Ma_ArticuloDAO();
        public ResultDTO<Ma_ArticuloDTO> ListarTodo(int idEmpresa, string Activo)
        {
            return oMa_ArticuloDAO.ListarTodo(idEmpresa, Activo);
        }
        public ResultDTO<Ma_ArticuloDTO> ListarTodoxCategoria(int idCategoria, int idEmpresa, string Activo)
        {
            return oMa_ArticuloDAO.ListarTodoxCategoria(idEmpresa, idCategoria, Activo);
        }
        public ResultDTO<Ma_ArticuloDTO> ListarxID(int idArticulo)
        {
            return oMa_ArticuloDAO.ListarxID(idArticulo);
        }
        public ResultDTO<Ma_ArticuloDTO> UpdateInsert(Ma_ArticuloDTO oMa_ArticuloDTO)
        {
            return oMa_ArticuloDAO.UpdateInsert(oMa_ArticuloDTO);
        }

        public ResultDTO<Ma_ArticuloDTO> Delete(Ma_ArticuloDTO oMa_ArticuloDTO)
        {
            return oMa_ArticuloDAO.Delete(oMa_ArticuloDTO);
        }
        public ResultDTO<Ma_ArticuloDTO> ObtenerPrecioArtProv(int idEmpresa, int idArticulo, int idProveedor, int idMoneda)
        {
            return oMa_ArticuloDAO.ObtenerPrecioArtProv(idEmpresa, idArticulo, idProveedor, idMoneda);
        }
        public ResultDTO<Ma_ArticuloDisenoDTO> GetFileDiseno(int idArticuloDiseno)
        {
            return oMa_ArticuloDAO.GetFileDiseno(idArticuloDiseno);
        }

    }
}
