using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
   public class COM_OrdenCompra_DetalleDTO
    {
        public int idOrdenCompraDetalle { get; set; }
        public int IdOrdenCompra { get; set; }
        public int idArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public int idCategoria { get; set; }
        public string DescripcionCategoria { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioNacional { get; set; }
        public decimal PrecioExtranjero { get; set; }
        public decimal TotalNacional { get; set; }
        public decimal TotalExtranjero { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string descUnidadMedida { get; set; }
    }
}
