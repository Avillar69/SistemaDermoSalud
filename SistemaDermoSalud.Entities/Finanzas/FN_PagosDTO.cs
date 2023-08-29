using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Finanzas
{
  public   class FN_PagosDTO
    {
        public int idPago { get; set; }
        public int idEmpresa { get; set; }
        public int idTipo { get; set; }
        public int idDocumentoCompraVenta { get; set; }
        public int idSocioNegocio { get; set; }
        public string RazonSocial { get; set; }
        public string SerieDcto { get; set; }// id requerimiento 
        public string NumeroDcto { get; set; }// string requerimiento
        public DateTime FechaPago { get; set; }
        public decimal MontoxCobrar { get; set; }
        public decimal MontoxPagar { get; set; }
        public decimal MontoAplicado { get; set; }
        public decimal SaldoxAplicar { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string cadDetalle { get; set; }
        public List<FN_PagosDetalle> oListaDetalle { get; set; }

        /*dato para jalar el tipo de moneda*/
        public int idMoneda { get; set; }
    }
}
