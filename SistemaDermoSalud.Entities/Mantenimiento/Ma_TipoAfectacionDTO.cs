using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Mantenimiento
{
    public class Ma_TipoAfectacionDTO
    {
        public int idTipoAfectacion { get; set; }
        public string CodigoSunat { get; set; }
        public string Descripcion { get; set; }
        public string CodigoTributo { get; set; }
        public string Afectacion { get; set; }
    }
}
