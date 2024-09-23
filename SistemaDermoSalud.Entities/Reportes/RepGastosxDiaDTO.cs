using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Reportes
{
    public class RepGastosxDiaDTO
    {
        public string FechaCreacion { get; set; }
        public string DescripcionConcepto { get; set; }
        public string Observaciones { get; set; }
        public decimal TotalNacional { get; set; }
        public string TipoPago { get; set; }
    }
}
