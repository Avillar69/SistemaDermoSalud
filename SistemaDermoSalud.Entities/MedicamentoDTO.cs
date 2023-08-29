using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class MedicamentoDTO
    {
        public int idMedicamentos { get; set; }
        public string Descripcion { get; set; }
        public int idLaboratorio { get; set; }
        public string Laboratorio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public decimal PagoMedicamento { get; set; }
        public string CodigoMedicamento { get; set; }
        public string CodigoAutogenerado { get; set; }
    }
}
