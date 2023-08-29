using SistemaDermoSalud.DataAccess.Compras;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business.Compras
{
   public class COM_ListaPrecioBL
    {
        public ResultDTO<COM_ListaPrecioDTO> ListarxProveedor(int idEmpresa, int idProveedor)
        {
            return new COM_ListaPrecioDAO().ListarxProveedor(idEmpresa, idProveedor);
        }
        public ResultDTO<COM_ListaPrecioDTO> UpdateInsert(COM_ListaPrecioDTO oCOM_ListaPrecioDTO)
        {
            return new COM_ListaPrecioDAO().UpdateInsert(oCOM_ListaPrecioDTO);
        }
        public ResultDTO<COM_ListaPrecioDTO> ListarxIdMaterial(int idEmpresa, int idProveedor, int idMaterial, int idMoneda)
        {
            return new COM_ListaPrecioDAO().ListarxIdMaterial(idEmpresa, idProveedor, idMaterial, idMoneda);
        }
    }
}
