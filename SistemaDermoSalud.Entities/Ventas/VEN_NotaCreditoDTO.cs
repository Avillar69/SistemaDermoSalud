using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Ventas
{
    public class VEN_NotaCreditoDTO
    {
        public int idNotaCredito { get; set; }
        public string TipoNota { get; set; }
        public int idEmpresa { get; set; }
        public Int64 idDocVenta { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }//5
        public string Descripcion { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public int idMotivo { get; set; }
        public decimal Total { get; set; }//10
        public decimal IGV { get; set; }
        public decimal TotalGravada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        //campos para mostrar en la tabla
        public string Motivo { get; set; }
        public string Cliente { get; set; }
        public string cadDetalle { get; set; }
        public int idMoneda { get; set; }
        public int idCliente { get; set; }
        public string MontoLetras { get; set; }
        public string cadDetalleWS { get; set; }
        public string NumDocRef { get; set; }
        public string FechaDocRef { get; set; }
        public int idTipoDocumentoRef { get; set; }
        public List<VEN_NotaCredito_DetalleDTO> oListaDetalle { get; set; }
        //descripciones al cargar el registro
        public string Moneda { get; set; }
        public string ClienteDocumento { get; set; }
        public int idTipoVenta { get; set; }
        public int idTipoAfectacion { get; set; }
        public decimal TotalExportacion { get; set; }
        public decimal TotalGratuito { get; set; }
        public string TipoDoc { get; set; }
        //public decimal PorcDescuento { get; set; }
        public string RespuestaSunat { get; set; }
        public string EstadoSunat { get; set; }
        public bool AceptaSunat { get; set; }

        public string ClienteRazon { get; set; }
        public string ClienteDireccion { get; set; }
        public int ClienteTipoDocumento { get; set; }
        public bool flgIGV { get; set; }
        public string Unidad_Medida { get; set; }
        public string Enlace { get; set; }
        public bool Aceptada_Sunat { get; set; }
        public string Descripcion_Sunat { get; set; }
        public string PDF_Base64 { get; set; }
        public string XML_Base64 { get; set; }
        public string CDR_Base64 { get; set; }
        public string CodigoSunat { get; set; }
        public int idTipoDocumento { get; set; }

        public decimal TipoCambio { get; set; }
        public decimal PorcDescuento { get; set; }
        public decimal TotalDescuentoNacional { get; set; }//1
        public decimal TotalDescuentoExtranjero { get; set; }//1
        public decimal SubTotalNacional { get; set; }
        public decimal TotalExonerado { get; set; }//57
        public decimal TotalInafecto { get; set; }//57
        public decimal TotalOtrosCargos { get; set; }
        public decimal IGVNacional { get; set; }
        public decimal TotalNacional { get; set; }
        public string TipoPago { get; set; }
        public string FormaPago { get; set; }
        public int idFormaPago { get; set; }
        public DateTime FechaDocumento { get; set; }

        //IMPRESION
        public string ReferenciaDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal ImporteDocumento { get; set; }
        public int idOperacion { get; set; }
    }
}
