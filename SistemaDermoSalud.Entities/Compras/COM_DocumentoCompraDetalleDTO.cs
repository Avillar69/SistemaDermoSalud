using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
 public   class COM_DocumentoCompraDetalleDTO
    {
        public int idDocumentoCompraDetalle { get; set; }
        public int idDocumentoCompra { get; set; }
        public int idArticulo { get; set; }
        public string descripcionArticulo { get; set; }
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public int idCategoria { get; set; }
        public string descripcionCategoria { get; set; }
        public decimal PrecioNacional { get; set; }
        public decimal PrecioExtranjero { get; set; }
        public decimal TotalNacional { get; set; }
        public decimal TotalExtranjero { get; set; }
        public int idDocumentoRef { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string Marca { get; set; }
    }
}
