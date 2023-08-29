using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Finanzas
{
    public class FN_PagosDetalle
    {
        public int idPagoDetalle { get; set; }
        public int idPago { get; set; }
        public int idCajaDetalle { get; set; }
        public int idTipo { get; set; }
        public int idDocumento { get; set; }
        public int idEmpresa { get; set; }
        public int idConcepto { get; set; }
        public string Concepto { get; set; }
        public int idFormaPago { get; set; }
        public string DescripcionFormaPago { get; set; }
        public string NumeroOperacion { get; set; }
        public int idCuentaBancario { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string Observacion { get; set; }
        public string DescripcionOperacion { get; set; }
        public int idCuentaBancarioDestino { get; set; }
        public string NumeroCuentaDestino { get; set; }
        public DateTime FechaDetalle { get; set; }


        /*datos del PAGO*/
        public int idTipoDocumento { get; set; }
        public int idDocumentoCompraVenta { get; set; }
        public int idSocioNegocio { get; set; }
        public string RazonSocial { get; set; }
        public decimal MontoxCobrar { get; set; }
        public decimal MontoxPagar { get; set; }
        public decimal MontoAplicado { get; set; }
        public decimal SaldoxAplicar { get; set; }
        public DateTime FechaCreacionPago { get; set; }
        public string SerieDcto { get; set; }
        public string NumeroDcto { get; set; }

    }
}
