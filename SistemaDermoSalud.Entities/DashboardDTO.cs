using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class DashboardDTO
    {
        public decimal ComprasSoles { get; set; }
        public decimal ComprasDolares { get; set; }
        public decimal VentasSoles { get; set; }
        public decimal VentasDolares { get; set; }
        public decimal Pagos { get; set; }
        public decimal Cobros { get; set; }
        public decimal ComprasEnero { get; set; }
        public decimal ComprasFebrero { get; set; }
        public decimal ComprasMarzo { get; set; }
        public decimal ComprasAbril { get; set; }
        public decimal ComprasMayo { get; set; }
        public decimal ComprasJunio { get; set; }
        public decimal ComprasJulio { get; set; }
        public decimal ComprasAgosto { get; set; }
        public decimal ComprasSetiembre { get; set; }
        public decimal ComprasOctubre { get; set; }
        public decimal ComprasNoviembre { get; set; }
        public decimal ComprasDiciembre { get; set; }
        public decimal VentasEnero { get; set; }
        public decimal VentasFebrero { get; set; }
        public decimal VentasMarzo { get; set; }
        public decimal VentasAbril { get; set; }
        public decimal VentasMayo { get; set; }
        public decimal VentasJunio { get; set; }
        public decimal VentasJulio { get; set; }
        public decimal VentasAgosto { get; set; }
        public decimal VentasSetiembre { get; set; }
        public decimal VentasOctubre { get; set; }
        public decimal VentasNoviembre { get; set; }
        public decimal VentasDiciembre { get; set; }
        public List<DashboardTopProductoDTO> listaTopArticulos { get; set; } = new List<DashboardTopProductoDTO>();
        public List<DashboardTopClientesDTO> listaVentas { get; set; } = new List<DashboardTopClientesDTO>();
        ///
        //datos para el grafico circular
        public int idServicio { get; set; }
        public int cantUso { get; set; }
        public decimal T_Dermatologico { get; set; }
        public decimal T_Estatico { get; set; }
        public decimal C_Menor { get; set; }
        public decimal Nutricion { get; set; }
        public decimal T_Piel { get; set; }
    }
}
