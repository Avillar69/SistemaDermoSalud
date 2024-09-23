using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Reportes
{
    public class RepProductoAgotarseDTO
    {
        public string Marca { get; set; }
        public string CodigoProducto { get; set; }
        public string Producto { get; set; }
        public decimal Stock { get; set; }
    }
}
