using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class CitasDTO
    {
        public int idCita { get; set; }
        public DateTime FechaCita { get; set; }
        public int idPaciente { get; set; }
        public string NombreCompleto { get; set; }
        public int idPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public int idServicio { get; set; }
        public string DescripcionServicio { get; set; }
        public int EstadoPago { get; set; }
        public string Hora { get; set; }
        public int idDocRef { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        //Campos Filtrado
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string FechaHora { get; set; }
        public string Codigo { get; set; }
        //nuevo campo de pago cita kevin 15/05/2018
        public decimal Pago { get; set; }
        //Caja
        public string Paciente { get; set; }
        public string HoraF { get; set; }
        //
        public int EstadoCita { get; set; }
        public string EstadoVenta { get; set; }
        //DETALLEVENTA
        //public string Paciente { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        //Por Roles
        public int idRol { get; set; }
        //comision x medico
        public string NombreTipoServicio { get; set; }
        public decimal Precio { get; set; }
        public decimal Costo { get; set; }
        public decimal Gasto { get; set; }
        public decimal Diferencia { get; set; }
        public decimal PorcentajeMedico { get; set; }
        public decimal Comision { get; set; }
        public int idComisionMedico { get; set; }
        public DateTime FecIni { get; set; }
        public DateTime FecFin { get; set; }
        public int idDocumentoVenta { get; set; }
        //Nuevo Campo Cita 04-01-2019
        public string Tratamiento { get; set; }
        public List<CitasDetalleDTO> oListaDetalle { get; set; }
        public string cadDetalle { get; set; }
        public int NroTratamiento { get; set; }
        public string Servicio { get; set; }
        public DateTime FechaComision { get; set; }
    }
}
