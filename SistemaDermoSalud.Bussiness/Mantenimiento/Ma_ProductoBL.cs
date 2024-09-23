using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.DataAccess.Mantenimiento;
using SistemaDermoSalud.Entities.Reportes;
using System;

namespace SistemaDermoSalud.Business
{
    public class Ma_ProductoBL
    {
        Ma_ProductoDAO oProductoDAO = new Ma_ProductoDAO();
        public ResultDTO<Ma_ProductoDTO> ListarTodo(int p)
        {
            return oProductoDAO.ListarTodo(p);
        }
        public ResultDTO<Ma_ProductoDTO> ListarporMarca(int p)
        {
            return oProductoDAO.ListarporMarca(p);
        }

        public ResultDTO<RepProductoAgotarseDTO> ReporteProductoxAgotarse()
        {
            return oProductoDAO.ReporteProductoxAgotarse();
        }
        public ResultDTO<RepProductoAgotarseDTO> ReporteProductoSinVenta(DateTime FechaInicio, DateTime FechaFin)
        {
            return oProductoDAO.ReporteProductoSinVenta(FechaInicio, FechaFin);
        }
        public ResultDTO<Ma_ProductoDTO> ListarxID(int idProducto)
        {
            return oProductoDAO.ListarxID(idProducto);
        }

        public ResultDTO<Ma_ProductoDTO> UpdateInsert(Ma_ProductoDTO oProductoDTO)
        {
            return oProductoDAO.UpdateInsert(oProductoDTO);
        }

        public ResultDTO<Ma_ProductoDTO> Delete(Ma_ProductoDTO oProductoDTO)
        {
            return oProductoDAO.Delete(oProductoDTO);
        }
        public ResultDTO<Ma_ProductoDTO> ObtenerIDByCodigoBarras(string CodigoBarras)
        {
            return oProductoDAO.ObtenerIDByCodigoBarras(CodigoBarras);
        }
    }
}
