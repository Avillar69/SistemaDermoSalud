using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class VEN_PedidosDetalleDTO
    {

        public int idPedidoDetalle { get; set; }
        public int idPedido { get; set; }
        public int idArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public int idCategoria { get; set; }
        public string DescripcionCategoria { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CantidadAprobada { get; set; }
        public decimal CantidadRechazada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public bool EstadoAprobacion { get; set; }
        public string descUnidadMedida { get; set; }
    }
}
