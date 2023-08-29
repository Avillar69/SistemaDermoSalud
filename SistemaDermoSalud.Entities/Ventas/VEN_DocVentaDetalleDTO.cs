using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Ventas
{
    public class VEN_DocVentaDetalleDTO
    {
        public int idDocumentoVentaDetalle { get; set; }
        public int idDocumentoVenta { get; set; }
        public int idArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public int idCategoria { get; set; }
        public string DescripcionCategoria { get; set; }
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

        public bool EstadoAprobacion { get; set; }
        public string descUnidadMedida { get; set; }

        public string Marca { get; set; }
        public string DescripcionArtTicket { get; set; }
        public string CodigoProducto { get; set; }

        public string idGuia { get; set; }
        public string idDetGuia { get; set; }
        //
        public Int64 DocumentoVentaDetalleID { get; set; }
        public Int64 DocumentoVentaID { get; set; }
    }
}
