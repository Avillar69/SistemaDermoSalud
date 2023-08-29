using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Mantenimiento
{
 public  class Ma_AlmacenDTO
    {

        public int idAlmacen { get; set; }
        public int idEmpresa { get; set; }
        public string CodigoGenerado { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        //descripciones
        public string DesUsuarioModificacion { get; set; }
        public List<Ma_ArticuloDTO> oListaArticulo { get; set; }
    }
}
