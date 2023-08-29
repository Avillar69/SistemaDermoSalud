using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDermoSalud.DataAccess.Ventas;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Ventas;

namespace SistemaDermoSalud.Business.Ventas
{
    public class VEN_DocVentaBL
    {
        VEN_DocVentaDAO oVEN_DocumentoVentaDAO = new VEN_DocVentaDAO();
        public ResultDTO<VEN_DocVentaDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_DocumentoVentaDAO.ListarRangoFecha(idEmpresa, fechaInicio, fechaFin);
        }
        public ResultDTO<VEN_DocVentaDTO> ListarxID(int idDocumentoCompra)
        {
            return oVEN_DocumentoVentaDAO.ListarxID(idDocumentoCompra);
        }
        public ResultDTO<VEN_DocVentaDTO> UpdateInsert(VEN_DocVentaDTO oVEN_DocVentaDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_DocumentoVentaDAO.UpdateInsert(oVEN_DocVentaDTO, fechaInicio, fechaFin);
        }
        public ResultDTO<VEN_DocVentaDTO> Delete(VEN_DocVentaDTO oVEN_DocVentaDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_DocumentoVentaDAO.Delete(oVEN_DocVentaDTO, fechaInicio, fechaFin);
        }
        public ResultDTO<VEN_DocVentaDTO> cargarVentas(int idEmpresa)
        {
            return oVEN_DocumentoVentaDAO.cargarVentas(idEmpresa);
        }
        //public ResultDTO<VEN_DocVentaDTO> cargarDetalleVentas(int idVenta)
        //{
        //    return oVEN_DocumentoVentaDAO.cargarDetalleVentas(idVenta);
        //}
        public ResultDTO<VEN_DocVentaDTO> ListarVentasxCobrar(int idEmpresa)
        {
            return oVEN_DocumentoVentaDAO.ListarVentasxCobrar(idEmpresa);
        }
        public ResultDTO<VEN_SerieDTO> ListarSeriexTipoDocumento(int idTipoDocumento)
        {
            return oVEN_DocumentoVentaDAO.ListarSeriexTipoDocumento(idTipoDocumento);
        }
        public string ObtenerNumeroDocumentoxSerie(int id, string Serie)
        {
            return oVEN_DocumentoVentaDAO.ObtenerNumeroDocumentoxSerie(id, Serie);
        }
        public string ValidarDocumento(VEN_DocVentaDTO oVEN_DocumentoVentaDTO)
        {
            return oVEN_DocumentoVentaDAO.ValidarDocumento(oVEN_DocumentoVentaDTO);
        }
        public ResultDTO<VEN_DocVentaDTO> Anular(VEN_DocVentaDTO oVEN_DocumentoVentaDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_DocumentoVentaDAO.Anular(oVEN_DocumentoVentaDTO, fechaInicio, fechaFin);
        }
        public VEN_DocVentaDTO ListarxIDVenta(int idDocumentoVenta)
        {
            return oVEN_DocumentoVentaDAO.ListarxIDVenta(idDocumentoVenta);
        }
        public ResultDTO<VEN_GuiaRemisionDTO> ListarGuiasPendientes(string idCliente, string Local)
        {
            return oVEN_DocumentoVentaDAO.ListarGuiasPendientes(idCliente, Local);
        }
        public void TraerDatosGuias(string idGuia, string idLocal, ResultDTO<VEN_GuiaRemisionDTO> oResultDTO_Guia, List<VEN_DocVentaDetalleDTO> listaDetalleGuia)
        {
            oVEN_DocumentoVentaDAO.TraerDatosGuias(idGuia, idLocal, oResultDTO_Guia, listaDetalleGuia);
        }
        public ResultDTO<VEN_DocVentaDTO> ListarxID_Venta(Int64 idDocumentoCompra)
        {
            return oVEN_DocumentoVentaDAO.ListarxID_Venta(idDocumentoCompra);
        }
        public int validar_Factura(int idDocVenta)
        {
            return oVEN_DocumentoVentaDAO.validar_Factura(idDocVenta);
        }
    }
}
