using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public class VEN_DocumentoVentaBL
    {
        VEN_DocumentoVentaDAO oVEN_DocumentoVentaDAO = new VEN_DocumentoVentaDAO();
        public ResultDTO<VEN_DocumentoVentaDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_DocumentoVentaDAO.ListarRangoFecha(idEmpresa, fechaInicio, fechaFin);
        }
        public ResultDTO<VEN_DocumentoVentaDTO> ListarxID(int idDocumentoCompra)
        {
            return oVEN_DocumentoVentaDAO.ListarxID(idDocumentoCompra);
        }
        public ResultDTO<VEN_DocumentoVentaDTO> UpdateInsert(VEN_DocumentoVentaDTO oCOM_DocumentoCompraDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_DocumentoVentaDAO.UpdateInsert(oCOM_DocumentoCompraDTO, fechaInicio, fechaFin);
        }
        public ResultDTO<VEN_DocumentoVentaDTO> Delete(VEN_DocumentoVentaDTO oCOM_DocumentoCompraDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_DocumentoVentaDAO.Delete(oCOM_DocumentoCompraDTO, fechaInicio, fechaFin);
        }
        public ResultDTO<VEN_DocumentoVentaDTO> cargarVentas(int idEmpresa)
        {
            return oVEN_DocumentoVentaDAO.cargarVentas(idEmpresa);
        }
        public ResultDTO<VEN_DocumentoVentaDetalleDTO> cargarDetalleVentas(int idVenta)
        {
            return oVEN_DocumentoVentaDAO.cargarDetalleVentas(idVenta);
        }
        public ResultDTO<VEN_DocumentoVentaDTO> ListarVentasxCobrar(int idEmpresa)
        {
            return oVEN_DocumentoVentaDAO.ListarVentasxCobrar(idEmpresa);
        }
        public ResultDTO<VEN_SerieDTO> ListarSeriexTipoDocumento(int idTipoDocumento)
        {
            return oVEN_DocumentoVentaDAO.ListarSeriexTipoDocumento(idTipoDocumento);
        }
        public string ObtenerNumeroDocumentoxSerie(int id,string Serie)
        {
            return oVEN_DocumentoVentaDAO.ObtenerNumeroDocumentoxSerie(id,Serie);
        }
        public ResultDTO<VEN_DocumentoVentaDTO> ListarVentasDashboard()
        {
            return oVEN_DocumentoVentaDAO.ListarVentasDashboard();
        }


        //REPORTE VENTA MEDICAMENTO
        public ResultDTO<VEN_DocumentoVentaDetalleDTO> ReporteVentaFecha(DateTime FechaInicio, DateTime FechaFin)
        {
            return oVEN_DocumentoVentaDAO.ReporteVentaFecha(FechaInicio, FechaFin);
        }


        public ResultDTO<VEN_DocumentoVentaDetalleDTO> ReporteVentaFechaMedicamento(DateTime FechaInicio, DateTime FechaFin, int idMedicamento)
        {
            return oVEN_DocumentoVentaDAO.ReporteVentaFechaMedicamento(FechaInicio, FechaFin, idMedicamento);
        }
    }
}
