using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Ventas
{
    public class VEN_GuiaRemisionDTO
    {
        public string idHdrGuiaRemision { get; set; }
        public string SerieGuia { get; set; }
        public string NroGuiaRemision { get; set; }
        public string FechaGuiaRemision { get; set; }
        public string FechaTicket { get; set; }
        public string NroTicket { get; set; }
        public string idCliente { get; set; }
        public string NomCliente { get; set; }
        public string RucCliente { get; set; }
        public bool Estado { get; set; }
        public string idVentas { get; set; }
        public string OrdenCompra { get; set; }

        public int guia_tipo { get; set; }
        public string guia_serie_numero { get; set; }
        public string idLocal { get; set; }
        public string Local { get; set; }

        public string cadDetalle { get; set; }
        public List<VEN_DocVentaDetalleDTO> oListaDetalle { get; set; }
    }
}
