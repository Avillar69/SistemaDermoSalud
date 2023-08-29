using SistemaDermoSalud.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class Ma_LocalDTO
    {
        public int idLocal { get; set; }
        public int idEmpresa { get; set; }
        public string CodigoGenerado { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        //Detalles
        public string Lista_Almacen { get; set; }
        //descripciones
        public string UsuarioModificacionDes { get; set; }
        public List<Ma_AlmacenDTO> oListaAlmacen = new List<Ma_AlmacenDTO>();
    }
}
