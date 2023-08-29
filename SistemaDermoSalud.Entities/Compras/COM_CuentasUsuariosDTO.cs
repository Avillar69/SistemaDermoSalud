using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Compras
{
    public class COM_CuentasUsuariosDTO
    {

        public int idCuentaBancaria { get; set; }
        public int idSocioNegocio { get; set; }
        public int idEmpresa { get; set; }
        public int idBanco { get; set; }
        public string DescripcionCuenta { get; set; }
        public string Cuenta { get; set; }
        public int idMoneda { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }

    }
}
