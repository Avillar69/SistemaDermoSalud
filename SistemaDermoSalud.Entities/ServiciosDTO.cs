using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class ServiciosDTO
    {
        public int idServicio { get; set; }
        public string NombreServicio { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string Codigo { get; set; }
        public int idTipoServicio { get; set; }
        public string descripcionTipoServicio { get; set; }
        public decimal Precio { get; set; }
    }
}
