using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class COM_DocumentoCompraDTO
    {
        public int idDocumentoCompra { get; set; }
        public int idEmpresa { get; set; }
        // public int idLocal { get; set; }
        // public int idAlmacen { get; set; }
        public int idTipoCompra { get; set; }
        public int idTipoDocumento { get; set; }
        public int idProveedor { get; set; }
        public string ProveedorRazon { get; set; }
        public string ProveedorDireccion { get; set; }
        public string ProveedorDocumento { get; set; }
        public int idMoneda { get; set; }
        public int idOrdenCompra { get; set; }
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
        public decimal MontoxPagar { get; set; }
        public string EstadoDoc { get; set; }
        public bool flgIGV { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string DesUsuarioModificacion { get; set; }
        public string DesTipoDocumento { get; set; }
        public string cadDetalle { get; set; }
        public List<COM_DocumentoCompraDetalleDTO> oListaDetalle { get; set; }
        public string ObservacionCompra { get; set; }
        public decimal PorcDescuento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        //lote
        public string Lote { get; set; }
        public decimal CantidadLote { get; set; }

        /*ingresando los datos del Pago Detalle*/
        public int idPagoDetalle { get; set; }
        public int idPago { get; set; }
        public int idCajaDetalle { get; set; }
        public string Observacion { get; set; }
        public int idTipoOperacion { get; set; }
        public string NumeroOperacion { get; set; }
        public string DescripcionOperacion { get; set; }
        public int idConcepto { get; set; }
        public string Concepto { get; set; }
        public string DescripcionFormaPago { get; set; }
        public int idCuentaBancario { get; set; }
        public int NumeroCuenta { get; set; }
        public decimal Monto { get; set; }
        public int idCuentaBancarioDestino { get; set; }
        public int NumeroCuentaDestino { get; set; }
        public DateTime FechaDetalle { get; set; }
        public string MonedaDesc { get; set; }
    }
}
