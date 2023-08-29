using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class FN_PagosDetalleDTO
    {
        public int idPagoDetalle { get; set; }
        public int idPago { get; set; }
        public int idEmpresa { get; set; }
        public int idConcepto { get; set; }
        public string Concepto { get; set; }
        public int idFormaPago { get; set; }
        public string DescripcionFormaPago { get; set; }
        public string NumeroOperacion { get; set; }
        public int idCuentaBancario { get; set; }
        public int NumeroCuenta { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public int idCompraVenta { get; set; }
        public int idTipo { get; set; }

    }
}
