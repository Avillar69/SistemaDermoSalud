using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Inventario
{
    public class KardexDTO
    {
        public DateTime FechaMovimiento { get; set; }
        public string DocReferencia { get; set; }
        public string idArticulo { get; set; }
        public string Articulo { get; set; }
        public string UnidadMedida { get; set; }
        public decimal StockInicial { get; set; }
        public decimal PrecioPromedio { get; set; }
        public decimal CantidadEntrada { get; set; }
        public decimal PrecioEntrada { get; set; }
        public decimal TotalEntrada { get; set; }
        public decimal CantidadSalida { get; set; }
        public decimal PrecioSalida { get; set; }
        public decimal TotalSalida { get; set; }
        public string Observaciones { get; set; }
        public string Movimiento { get; set; }
    }
}
