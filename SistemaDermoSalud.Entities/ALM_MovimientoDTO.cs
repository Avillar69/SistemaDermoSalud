using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class ALM_MovimientoDTO
    {
        public int idMovimiento { get; set; }
        public int idLocal { get; set; }
        public int idTipoMovimiento { get; set; }
        public string Observaciones { get; set; }
        public int idGuiaRemision { get; set; }
        public int idDocumento { get; set; }
        public int idEstado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        //Descripciones
        public string DesLocal { get; set; }
        public string DesAlmacenOrigen { get; set; }
        public string DesAlmacenDestino { get; set; }
        public string Lista_Articulo { get; set; }
        public string Lista_Articulo2 { get; set; }

        public List<ALM_MovimientoDetalleDTO> oListaDetalle { get; set; }
        public string TipoMovimiento { get; set; }
        public string DesEstado { get; set; }
    }

    public class ALM_MovimientoDetalleDTO
    {
        public int idMovimientoDetalle { get; set; }
        public int idMovimiento { get; set; }
        public int idArticulo { get; set; }
        public int Item { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        //descripciones
        public string DesArticulo { get; set; }
        //precio
        public decimal Precio { get; set; }
        public string Laboratorio { get; set; }
    }
}
