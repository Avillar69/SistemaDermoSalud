using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
    public class INV_StockBL
    {
        INV_StockDAO oINV_StockDAO = new INV_StockDAO();
        public ResultDTO<INV_StockDTO> ListarTodo(int idEmpresa)
        {
            return oINV_StockDAO.ListarTodo(idEmpresa);
        }
        public ResultDTO<INV_StockDTO> ListarxID(int idStock)
        {
            return oINV_StockDAO.ListarxID(idStock);
        }
        public ResultDTO<INV_StockDTO> UpdateInsert(INV_StockDTO oINV_StockDTO)
        {
            return oINV_StockDAO.UpdateInsert(oINV_StockDTO);
        }
        public ResultDTO<INV_StockDTO> Delete(INV_StockDTO oINV_StockDTO)
        {
            return oINV_StockDAO.Delete(oINV_StockDTO);
        }
        public string ItemStock(int idEmpresa, int idMedicamento, int idAlmacenO, int idAlmacenD)
        {
            return oINV_StockDAO.ItemStock(idEmpresa, idMedicamento, idAlmacenO, idAlmacenD);
        }
        public ResultDTO<INV_StockDTO> ListarStockxAlmacen(int idEmpresa, int idAlmacen, int idUsuario)
        {
            return oINV_StockDAO.ListarStockxAlmacen(idEmpresa, idAlmacen, idUsuario);
        }
    }
}
