using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class Ma_EstadoDTO
    {
        public int idEstado { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Modulo { get; set; }
    }
}
