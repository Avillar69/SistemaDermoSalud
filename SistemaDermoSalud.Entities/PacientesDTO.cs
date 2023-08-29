using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class PacientesDTO
    {
        public int idPaciente { get; set; }
        public string Nombres { get; set; } = "";
        public string ApellidoP { get; set; } = "";
        public string ApellidoM { get; set; } = "";
        public int Edad { get; set; } = 0;
        public string DNI { get; set; } = "";
        public string Sexo { get; set; } = "";
        public string Direccion { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Movil { get; set; } = "";
        public string Email { get; set; } = "";
        public string Observaciones { get; set; } = "";
        public DateTime FechaNacimiento { get; set; }
        public DateTime UltimaConsulta { get; set; }
        public string Img { get; set; } = "";
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string NombreCompleto { get; set; }
    }
}
