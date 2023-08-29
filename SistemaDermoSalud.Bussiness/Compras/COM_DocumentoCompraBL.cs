using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
    public class COM_DocumentoCompraBL
    {
        COM_DocumentoCompraDAO oCOM_DocumentoCompraDAO = new COM_DocumentoCompraDAO();
        public ResultDTO<COM_DocumentoCompraDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin)
        {
            return oCOM_DocumentoCompraDAO.ListarRangoFecha(idEmpresa, fechaInicio, fechaFin);
        }
        public ResultDTO<COM_DocumentoCompraDTO> ListarxID(int idDocumentoCompra)
        {
            return oCOM_DocumentoCompraDAO.ListarxID(idDocumentoCompra);//ListarTodo
        }
        public ResultDTO<COM_DocumentoCompraDTO> ListarxIDNotas_Compras(int idDocumentoCompra)
        {
            return oCOM_DocumentoCompraDAO.ListarxIDNotas_Compras(idDocumentoCompra);//ListarTodo
        }
        public ResultDTO<COM_DocumentoCompraDTO> ListarTodo()
        {
            return oCOM_DocumentoCompraDAO.ListarTodo();
        }
        public ResultDTO<COM_DocumentoCompraDTO> UpdateInsert(COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oCOM_DocumentoCompraDAO.UpdateInsert(oCOM_DocumentoCompraDTO, fechaInicio, fechaFin);
        }
        public ResultDTO<COM_DocumentoCompraDTO> Delete(COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oCOM_DocumentoCompraDAO.Delete(oCOM_DocumentoCompraDTO, fechaInicio, fechaFin);
        }
        public ResultDTO<COM_DocumentoCompraDTO> cargarCompras(int idEmpresa)
        {
            return oCOM_DocumentoCompraDAO.cargarCompras(idEmpresa);
        }
        public ResultDTO<COM_DocumentoCompraDetalleDTO> cargarDetalleCompras(int idCompra)
        {
            return oCOM_DocumentoCompraDAO.cargarDetalleCompras(idCompra);
        }
        public ResultDTO<COM_DocumentoCompraDTO> ListarComprasXPagar(int idEmpresa)
        {
            return oCOM_DocumentoCompraDAO.ListarComprasXPagar(idEmpresa);
        }
        public ResultDTO<COM_DocumentoCompraDTO> FechaVencimiento(string fecha, int forma)
        {
            return oCOM_DocumentoCompraDAO.FechaVencimiento(fecha, forma);
        }

        public string ValidarDocumento(COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO)
        {
            return oCOM_DocumentoCompraDAO.ValidarDocumento(oCOM_DocumentoCompraDTO);
        }
    }
}
