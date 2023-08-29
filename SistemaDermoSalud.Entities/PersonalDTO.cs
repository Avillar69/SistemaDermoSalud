using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class PersonalDTO
    {
        public int idPersonal { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Nombres { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public int Edad { get; set; }
        public string Documento { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string EstadoCivil { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Movil { get; set; }
        public string Login { get; set; }
        public decimal PorcentajeUtilidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string Color { get; set; }
        public string Img { get; set; }
        public int idCargo { get; set; }
        public string DescripcionCargo { get; set; }
        public string Colegiatura { get; set; }
        public string NombreCompleto { get; set; }
        public bool Planilla { get; set; }
    }
}
