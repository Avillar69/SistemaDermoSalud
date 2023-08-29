using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class AtencionMedicaDTO
    {
        public int idAtencionMedica { get; set; }
        public int idCita { get; set; }
        public int idPersonal { get; set; }
        public string Personal { get; set; }
        public string PlanTerapeutico{ get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCita { get; set; }
        public string Codigo { get; set; }
        public string lista_Cab_Recetas { get; set; }
        public string lista_Recetas { get; set; }
        public string lista_Evolucion { get; set; }
        public List<AtencionMedica_Cab_RecetaDTO> oListaCabRecetas = new List<AtencionMedica_Cab_RecetaDTO>();
        public List<AtencionMedica_RecetaDTO> oListaRecetas = new List<AtencionMedica_RecetaDTO>();
        public List<AtencionMedica_EvolucionDTO> oListaEvolucion = new List<AtencionMedica_EvolucionDTO>();
        public string MotivoConsulta { get; set; }

        public int idReceta { get; set; }
        public string NroReceta { get; set; }
        public int idPaciente { get; set; }
    }
}
