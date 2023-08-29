using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Finanzas
{
  public   class FN_TipoCambioDTO
    {
        public int idTipoCambio { get; set; }
        public int idEmpresa { get; set; }
        public int idMoneda { get; set; }
        public DateTime Fecha { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        //CamposAdicionales
        public string DescripcionMoneda { get; set; }
    }
}
