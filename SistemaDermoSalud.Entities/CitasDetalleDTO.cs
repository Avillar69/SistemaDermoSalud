using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class CitasDetalleDTO
    {
        public int idCita { get; set; }
        public int idCitaDetalle { get; set; }
        public int idTratamiento1 { get; set; }
        public string Tratamiento1 { get; set; }
        public decimal Monto1 { get; set; }
        //public int idTratamiento2 { get; set; }
        //public string Tratamiento2 { get; set; }
        //public decimal Monto2 { get; set; }
        //public int idTratamiento3 { get; set; }
        //public string Tratamiento3 { get; set; }
        //public decimal Monto3 { get; set; }
        //public int idTratamiento4 { get; set; }
        //public string Tratamiento4 { get; set; }
        //public decimal Monto4 { get; set; }
        //public int idTratamiento5 { get; set; }
        //public string Tratamiento5 { get; set; }
        //public decimal Monto5 { get; set; }
        //public int idTratamiento6 { get; set; }
        //public string Tratamiento6 { get; set; }
        //public decimal Monto6 { get; set; }
        //public int idTratamiento7 { get; set; }
        //public string Tratamiento7 { get; set; }
        //public decimal Monto7 { get; set; }
        //public int idTratamiento8 { get; set; }
        //public string Tratamiento8 { get; set; }
        //public decimal Monto8 { get; set; }
        //public int idTratamiento9 { get; set; }
        //public string Tratamiento9 { get; set; }
        //public decimal Monto9 { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
    }
}
