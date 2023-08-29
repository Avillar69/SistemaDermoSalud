using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
   public class AD_CuentaOrigenDTO
    {
        public int idCuentaOrigen { get; set; }
        public string NombreCuenta { get; set; }
        public int Banco { get; set; }
        public string DescripcionBanco { get; set; }
        public int idMoneda { get; set; }
        public string DescMoneda { get; set; }
        public string NumeroCuenta { get; set; }
        public Boolean Estado { get; set; }
    }
}
