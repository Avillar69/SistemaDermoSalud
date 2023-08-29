using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class Seg_MenuDTO
    {
        public int idMenu { get; set; }
        public int idEmpresa { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public int idMenuPadre { get; set; }
        public int Nivel { get; set; }
        public int Posicion { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
    }
}
