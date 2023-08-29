using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class ObjSesionDTO
    {
        public Seg_UsuarioDTO SessionUsuario { get; set; }
        public List<Seg_MenuDTO> SessionListaMenu { get; set; }
    }
}
