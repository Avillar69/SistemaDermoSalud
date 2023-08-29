using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class RecetaMedica_DetalleDTO
    {
        public int idRecetaMedica_Detalle { get; set; }
        public int idRecetaMedica { get; set; }
        public string Medicamento { get; set; }
        public string Dosis { get; set; }
        public string Via { get; set; }
        public string Frecuencia { get; set; }
        public string Duracion { get; set; }
        public string Presentacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string NroReceta { get; set; }
    }
}
