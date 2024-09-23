using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class FN_CajaDTO
    {

        public int idCaja { get; set; }
        public string CodigoGenerado { get; set; }
        public int idEmpresa { get; set; }
        public string PeriodoAno { get; set; }
        public string NroCaja { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaApertura { get; set; }
        public string FechaCierre { get; set; }
        public int idMoneda { get; set; }
        public decimal MontoInicio { get; set; }
        public decimal MontoIngreso { get; set; }
        public decimal MontoSalida { get; set; }
        public decimal MontoSaldo { get; set; }
        public string EstadoCaja { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string Usuario { get; set; }
        public string Moneda { get; set; }
        public string Opcion { get; set; }
        public decimal TotalIngreso { get; set; }
        public decimal MontoTotal { get; set; }
        public string RazonSocial { get; set; }
        public string Ruc { get; set; }
        public string Direccion { get; set; }
        public decimal MontoEfectivo { get; set; }
        public decimal MontoTarjeta { get; set; }
        public int idTipoCaja { get; set; }
        public string TipoCaja { get; set; }
        public string HoraApertura { get; set; }
        public string HoraCierre { get; set; }
    }
}
