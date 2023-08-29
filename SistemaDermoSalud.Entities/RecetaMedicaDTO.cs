using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class RecetaMedicaDTO
    {
        public int idRecetaMedica { get; set; }
        public int NroReceta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string lista_Cabecera_Recetas { get; set; }
        public string lista_Recetas { get; set; }
    }
}
