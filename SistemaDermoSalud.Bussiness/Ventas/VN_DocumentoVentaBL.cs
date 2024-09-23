using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Reportes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public  class VN_DocumentoVentaBL
    {

        VEN_DocumentoVentaDAO oVEN_DocumentoVentaDAO = new VEN_DocumentoVentaDAO();
        public ResultDTO<VEN_DocumentoVentaDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_DocumentoVentaDAO.ListarRangoFecha(idEmpresa, fechaInicio, fechaFin);
        }
        public ResultDTO<VEN_DocumentoVentaDTO> ListarxID(int idDocumentoVenta)
        {
            return oVEN_DocumentoVentaDAO.ListarxID(idDocumentoVenta);
        }
        public ResultDTO<VEN_DocumentoVentaDTO> UpdateInsert(VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_DocumentoVentaDAO.UpdateInsert(oVEN_DocumentoVentaDTO, fechaInicio, fechaFin);
        }
        public ResultDTO<VEN_DocumentoVentaDTO> Delete(VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_DocumentoVentaDAO.Delete(oVEN_DocumentoVentaDTO, fechaInicio, fechaFin);
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
        public string ValidarDocumento(VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO)
        {
            return oVEN_DocumentoVentaDAO.ValidarDocumento(oVEN_DocumentoVentaDTO);
        }
        public ResultDTO<VEN_DocumentoVentaDTO> Anular(int idDocumentoVenta, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_DocumentoVentaDAO.Anular(idDocumentoVenta, fechaInicio, fechaFin);
        }
        public VEN_DocumentoVentaDTO ListarxIDVenta(int idDocumentoVenta)
        {
            return oVEN_DocumentoVentaDAO.ListarxIDVenta(idDocumentoVenta);
        }
        public ResultDTO<VEN_DocumentoVenta_ReportePorCliente> ReportePorCliente(DateTime fecIni, DateTime fecFin)
        {
            return oVEN_DocumentoVentaDAO.ReportePorCliente(fecIni, fecFin);
        }
        //public ResultDTO<VEN_DocumentoVentaDTO> ListarRegistroVenta(ResultDTO<VEN_DocumentoVentaDTO>Documentos,int idEmpresa, DateTime fechaInicio, DateTime fechaFin,string TipoDcto)
        //{
        //    return oVEN_DocumentoVentaDAO.ListarRegistroVenta(Documentos,idEmpresa, fechaInicio, fechaFin, TipoDcto);
        //}
        public ResultDTO<VEN_DocumentoVentaDTO> ListarRegistroVenta(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, int TipoDcto)
        {
            return oVEN_DocumentoVentaDAO.ListarRegistroVenta( idEmpresa, fechaInicio, fechaFin, TipoDcto);
        }
        public ResultDTO<VEN_DocumentoVentaDTO> Actualizar(VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_DocumentoVentaDAO.Actualizar(oVEN_DocumentoVentaDTO, fechaInicio, fechaFin);
        }
        public ResultDTO<Rep_DocumentoVentaDetalleCliente> ListarDetalleVentaCliente(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, int idCliente)
        {
            return oVEN_DocumentoVentaDAO.ListarDetalleVentaCliente(idEmpresa, fechaInicio, fechaFin, idCliente);
        }   
    }
}
