using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
   public class RH_EmpleadoDTO
    {
        public int idEmpleado { get; set; }
        public string CodigoGenerado { get; set; }
        public int idEmpresa { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int idTipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Sexo { get; set; }
        public int idPais { get; set; }
        public int idDepartamento { get; set; }
        public int idProvincia { get; set; }
        public int idDistrito { get; set; }
        public string Direccion { get; set; }
        public int NroEssalud { get; set; }
        public int NroAFP { get; set; }
        public string NroRUC { get; set; }
        public int Telefono { get; set; }
        public string Email { get; set; }
        public string EstadoCivil { get; set; }
        public int NroHijos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
    }
}
