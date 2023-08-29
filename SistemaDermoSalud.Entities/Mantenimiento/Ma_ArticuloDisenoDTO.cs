using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Mantenimiento
{
   public class Ma_ArticuloDisenoDTO
    {
        public int idArticuloDiseno { get; set; }
        public int idArticulo { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime Fecha { get; set; }
        public string Extension { get; set; }
        public string Archivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }



    }
}
