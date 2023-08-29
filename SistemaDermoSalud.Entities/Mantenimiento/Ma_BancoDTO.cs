using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
  public class Ma_BancoDTO
    {
        public int idBanco { get; set; }
        public int idEmpresa { get; set; }
        public string CodigoGenerado { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        //descripciones
        public string UsuarioModificacionDescripcion { get; set; }
    }
}
