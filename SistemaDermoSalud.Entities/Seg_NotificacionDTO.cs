using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
  public  class Seg_NotificacionDTO
    {
        public int idNotificacion { get; set; }
        public int idEmpresa { get; set; }
        public int idRol { get; set; }
        public DateTime Fecha { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Contenido { get; set; }
        public int idEstado { get; set; }
    }
}
