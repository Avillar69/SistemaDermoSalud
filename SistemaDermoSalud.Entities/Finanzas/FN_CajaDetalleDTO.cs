using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class FN_CajaDetalleDTO
    {
        public int idCajaDetalle { get; set; }
        public string CodigoGenerado { get; set; }
        public int idDocumento { get; set; }
        public int idEmpresa { get; set; }
        public int idCaja { get; set; }
        public string PeriodoAno { get; set; }
        public string NroCaja { get; set; }
        public int idConcepto { get; set; }
        public string DescripcionConcepto { get; set; }
        public int idMoneda { get; set; }
        public decimal SubTotalNacional { get; set; }
        public decimal SubTotalExtranjero { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal IGVNacional { get; set; }
        public decimal IGVExtranjera { get; set; }
        public decimal TotalNacional { get; set; }
        public decimal TotalExtranjero { get; set; }
        public string EstadoCaja { get; set; }
        public int idTipoEmpleado { get; set; }
        public int idProvCliEmpl { get; set; }
        public string NombreProvCliEmpl { get; set; } //20
        public string Observaciones { get; set; }
        //public decimal idBanco { get; set; }
        //public string NombreBanco { get; set; }
        public string NroCheque { get; set; }
        public int idTipoDocumento { get; set; }
        public string SerieDcto { get; set; }
        public string NroDcto { get; set; }
        public string SerieVale { get; set; }
        //public string NroVale { get; set; }
        public string NroOperacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }//30
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string TipoOperacion { get; set; }//33
        public string Ruc { get; set; }
        public decimal MontoPendiente { get; set; }
        public int idCompraVenta { get; set; }
        public int idTipoPago { get; set; }
        public string TipoPago { get; set; }
        public decimal Ingreso { get; set; }
        public decimal Salida { get; set; }//40
        public decimal Personal { get; set; }

        public int idCita { get; set; }
        public string NroCita { get; set; }
        public string Paciente { get; set; }
        public decimal CostoCita { get; set; }
        public string Documento { get; set; }
        //
        public decimal idTarjeta { get; set; }
        public string Tarjeta { get; set; }
        public string SerieRecibo { get; set; }
        public string NroRecibo { get; set; }
    }
}
