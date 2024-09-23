using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Reportes
{
    public class Rep_DocumentoVentaDetalleCliente
    {
        public string FechaDocumento { get; set; }
        public string TipoComprobante { get; set; }
        public string NroComprobante { get; set; }
        public string DescripcionArticulo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioNacional { get; set; }
        public decimal TotalNacional { get; set; }
    }
}
