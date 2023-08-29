using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Compras
{
    public class COM_PagaSocioDTO
    {
        public string idDocumento { get; set; }
        public string DescripcionSocial { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal MontoAplicado { get; set; }
        public decimal MontoXPagar { get; set; }

    }
}
