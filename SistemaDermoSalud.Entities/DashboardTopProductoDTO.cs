using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class DashboardTopProductoDTO
    {
        public int idArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public string FechaUltima { get; set; }
        public decimal TotalVendido { get; set; }
        public decimal Stock { get; set; }
        public decimal Precio { get; set; }
    }
    public class DashboardTopClientesDTO
    {
        public int idCliente { get; set; }
        public string ClienteRazon { get; set; }
        public string FechaUltimaVenta { get; set; }
        public decimal MontoUltimaVenta { get; set; }
        public decimal Suma { get; set; }
    }
}
