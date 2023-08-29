using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
   public class DashboardDocDTO
    {
        public DateTime FechaDocumento { get; set; }
        public string Numero { get; set; }
        public string ProveedorRazon { get; set; }
        public string Descripcion { get; set; }
        public decimal TotalNacional { get; set; }
    }
}
