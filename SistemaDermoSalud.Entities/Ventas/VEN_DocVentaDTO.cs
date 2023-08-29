using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Ventas
{
    public class VEN_DocVentaDTO
    {
        public int idDocumentoVenta { get; set; }
        public int idEmpresa { get; set; }
        public int idTipoVenta { get; set; }
        public int idTipoDocumento { get; set; }
        public int idCliente { get; set; }
        public string ClienteRazon { get; set; }
        public string ClienteDireccion { get; set; }
        public string ClienteDocumento { get; set; }
        public int ClienteTipoDocumento { get; set; }
        public int idMoneda { get; set; }
        public int idPedido { get; set; }
        public int idFormaPago { get; set; }
        public DateTime FechaDocumento { get; set; }
        public string SerieDocumento { get; set; }
        public string NumDocumento { get; set; }
        public decimal SubTotalNacional { get; set; }
        public decimal SubTotalExtranjero { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal IGVNacional { get; set; }
        public decimal IGVExtranjero { get; set; }
        public decimal TotalNacional { get; set; }
        public decimal TotalExtranjero { get; set; }
        public string EstadoDoc { get; set; }
        public bool flgIGV { get; set; }
        public decimal MontoxPagar { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string cadDetalle { get; set; }
        public List<VEN_DocVentaDetalleDTO> oListaDetalle { get; set; }
        public string ObservacionVenta { get; set; }
        public int idTipoAfectacion { get; set; }
        public decimal MontoxCobrar { get; set; }
        public decimal PorcDescuento { get; set; }
        public decimal TotalExonerado { get; set; }//57
        public decimal TotalInafecto { get; set; }//57
        public decimal TotalGravado { get; set; }//57
        public decimal TotalIGV { get; set; }//57
        public decimal TotalGratuito { get; set; }//57
        public decimal TotalOtrosCargos { get; set; }
        public decimal TotalDescuentoNacional { get; set; }//1
        public decimal TotalDescuentoExtranjero { get; set; }//1
        public decimal TotalExportacion { get; set; }//58
        public string Unidad_Medida { get; set; }
        public string Enlace { get; set; }
        public string MonedaDesc { get; set; }
        public string TipoPago { get; set; }
        public string Tarjeta { get; set; }
        public string NroOperacion { get; set; }
        public string FormaPago { get; set; }
        //nubefact
        public bool Aceptada_Sunat { get; set; }
        public string Descripcion_Sunat { get; set; }
        public string PDF_Base64 { get; set; }
        public string XML_Base64 { get; set; }
        public string CDR_Base64 { get; set; }
        public string EstadoSunat { get; set; }

        //campos para imprimir el pdf
        public string CodigoSunat { get; set; }
        public string SerieNumDoc { get; set; }
        public decimal SubTotalDolares { get; set; }
        public decimal SubTotalSoles { get; set; }
        public decimal Inafecto { get; set; }
        public decimal Total { get; set; }
        //
        public string NroGuia { get; set; }
        public string NroTicket { get; set; }
        public string OrdenCompra { get; set; }
        public string EstadoVenta { get; set; }
        //
        public string idGuias { get; set; }
        public decimal Pacas { get; set; }
        public string TipoExportacion { get; set; }
        public string Lugar { get; set; }
        //
        public string cadDetalleGuia { get; set; }
        public List<VEN_GuiaRemisionDTO> oListaDetalleGuia { get; set; }
        public decimal Detraccion { get; set; }
        public bool flgDetraccion { get; set; }
        public decimal PorcentajeDetraccion { get; set; }
        public int idOperacion { get; set; }
        public Int64 DocumentoVentaID { get; set; }
    }
}
