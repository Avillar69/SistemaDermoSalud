using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.DataAccess
{
    public class CONF_Comision_MedicoDTO
    {
        public int idComisionMedico { get; set; }
        public int idCita { get; set; }
        public DateTime FechaCita { get; set; }
        public int codigo { get; set; }
        public int idServicio { get; set; }
        public string DescripcionServicio { get; set; }
        public decimal Monto { get; set; }
        public decimal Gasto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }


    }
}
