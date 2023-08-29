using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class AD_GuiaRemisionDTO
    {
        public int idGuiaRemision { get; set; }
        public int idEmpresa { get; set; }
        //public int idLocal { get; set; }
        //public int idAlmacen { get; set; }
        public int idSocioNegocio { get; set; }
        public string RazonSocial { get; set; }
        public string ProveedorDocumento { get; set; }//4
        public string ProveedorDireccion { get; set; }//5

        public string SerieGuia { get; set; }
        public string NumeroGuia { get; set; }
        public string NOrdenCompra { get; set; }
        public string NDocRef { get; set; }
        public string Ruc { get; set; }
        public DateTime FechaInicioTraslado { get; set; }
        public string PuntoPartida { get; set; }
        public string PuntoLlegada { get; set; }
        public DateTime FechaFinTraslado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string cadDetalle { get; set; }
        public List<AD_GuiaRemisionDetalleDTO> oListaDetalle { get; set; }
    }
}
