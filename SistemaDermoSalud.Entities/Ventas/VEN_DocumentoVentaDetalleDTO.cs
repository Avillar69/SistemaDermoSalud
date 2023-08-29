using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class VEN_DocumentoVentaDetalleDTO
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
        //Adicional para repote de ventas por Fecha
        public string Laboratorio { get; set; }
        public decimal PrecioMedicamento { get; set; }
        public string Factura { get; set; }
        public string Cliente { get; set; }

    }
}
