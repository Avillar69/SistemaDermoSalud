using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class ResultDTO<T>
    {
        public List<T> ListaResultado { get; set; }
        public string Resultado { get; set; }
        public string MensajeError { get; set; }
        public string Campo1 { get; set; }
    }
}
