using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class CalendarEventDTO
    {
        public int idCita { get; set; }
        public string Codigo { get; set; }
        public string Color { get; set; }
        public string NombrePaciente { get; set; }
        public string NombrePersonal { get; set; }
        public string NombreServicio { get; set; }
        public DateTime FechaCita { get; set; }
        public string Hora { get; set; }
        public string HoraFin { get; set; }
        public string FechaHoraCalendar { get; set; }
        public string FechaHoraCalendarF { get; set; }
        public string Descripcion { get; set; }
    }
}
