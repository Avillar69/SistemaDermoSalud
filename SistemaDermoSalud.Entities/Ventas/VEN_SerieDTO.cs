using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class VEN_SerieDTO
    {
        public int idSerie { get; set; }
        public int idTipoComprobante { get; set; }
        public string Descripcion { get; set; }
        public string NroSerie { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string NumeroDoc { get; set; }
    }
}
