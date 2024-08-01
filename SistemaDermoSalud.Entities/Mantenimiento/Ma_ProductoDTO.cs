using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Mantenimiento
{
    public class Ma_ProductoDTO
    {
        public int idProducto { get; set; }
        public string Descripcion { get; set; }
        public int idMarca { get; set; }
        public string Marca { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public decimal Precio { get; set; }
        public string CodigoProducto { get; set; }
        public string CodigoAutogenerado { get; set; }
        public int idTalla { get; set; }
        public int idColor{ get; set; }
    }
}
