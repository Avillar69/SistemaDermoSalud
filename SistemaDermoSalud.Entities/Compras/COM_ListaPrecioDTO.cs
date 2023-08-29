using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Compras
{
   public class COM_ListaPrecioDTO
    {
        public int idDetalleListaPrecio { get; set; }
        public int idEmpresa { get; set; }
        public int idProveedor { get; set; }
        public string RazonSocial { get; set; }
        public int idArticulo { get; set; }
        public string descripcionArticulo { get; set; }
        public int idClaseArticulo { get; set; }
        public string descripcionClaseArticulo { get; set; }
        public int idCategoria { get; set; }
        public string descripcionCategoria { get; set; }
        public int idMoneda { get; set; }
        public string descripcionMoneda { get; set; }
        public decimal Valor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public bool Estado { get; set; }
    }
}
