using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
   public class VEN_PedidosDTO
    {

        public int idPedido { get; set; }
        public int idEmpresa { get; set; }
        public int idTipoCompra { get; set; }
        public int EstadoPedido { get; set; }
        public string NumPedido { get; set; }
        public DateTime FechaOrdenCompra { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int idProveedor { get; set; }
        public string ProveedorRazon { get; set; }
        public string ProveedorDocumento { get; set; }
        public string ProveedorDireccion { get; set; }
        public decimal SubTotalNacional { get; set; }
        public decimal SubTotalExtranjero { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal IGVNacional { get; set; }
        public decimal IGVExtranjero { get; set; }
        public decimal TotalNacional { get; set; }
        public decimal TotalExtranjero { get; set; }
        public decimal MontoxPagar { get; set; }
        public int idMoneda { get; set; }
        public string EstadoOC { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public string Impreso { get; set; }
        public bool Estado { get; set; }
        public string cadDetalle { get; set; }
        public List<VEN_PedidosDetalleDTO> oListaDetalle { get; set; }
        public bool IGVcheck { get; set; }
        public string Observacion { get; set; }
        public decimal PorcDescuento { get; set; }
    }
}
