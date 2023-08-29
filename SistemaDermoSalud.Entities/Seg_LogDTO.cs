using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
   public class Seg_LogDTO
    {
        public int idLog { get; set; }
        public int idEmpresa { get; set; }
        public int idUsuario { get; set; }
        public string Modulo { get; set; }
        public string Tabla { get; set; }
        public int idTabla { get; set; }
        public DateTime Fecha { get; set; }
        public string Transaccion { get; set; }
        public string Origen { get; set; }
        public string Descripcion { get; set; }


    }
}
