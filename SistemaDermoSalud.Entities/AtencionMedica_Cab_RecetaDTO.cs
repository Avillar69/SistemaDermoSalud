using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class AtencionMedica_Cab_RecetaDTO
    {
        public int idReceta { get; set; }
        public int idAtencionMedica { get; set; }
        public int NroReceta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string lista_Cab_Recetas { get; set; }
        public string lista_Recetas { get; set; }
        public List<AtencionMedica_Cab_RecetaDTO> oListaCabRecetas = new List<AtencionMedica_Cab_RecetaDTO>();
        public List<AtencionMedica_RecetaDTO> oListaRecetas = new List<AtencionMedica_RecetaDTO>();
    }
}
