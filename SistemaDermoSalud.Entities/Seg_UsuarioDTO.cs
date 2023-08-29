using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class Seg_UsuarioDTO
    {
        public int idUsuario { get; set; }
        public string CodigoGenerado { get; set; }
        public int idEmpresa { get; set; }
        public int idRol { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string RolDescripcion { get; set; }
        public string UsuarioModificacionDescripcion { get; set; }
        public List<Ma_LocalDTO> oListaLocales = new List<Ma_LocalDTO>();
        public List<Seg_NotificacionDTO> oListaNotificacion = new List<Seg_NotificacionDTO>();
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Imagen { get; set; }
    }
}
