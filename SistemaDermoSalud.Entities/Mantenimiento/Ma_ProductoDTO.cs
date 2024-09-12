using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Mantenimiento
{
    public class Ma_ProductoDTO
    {
        public int idProducto { get; set; }
        public string Descripcion { get; set; }
        public int idMarca { get; set; }
        public string Marca { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public decimal Precio { get; set; }
        public string CodigoProducto { get; set; }
        public string CodigoAutogenerado { get; set; }
        public int idTalla { get; set; }
        public int idColor { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal MargenGananciaDeseado { get; set; }
        public decimal MargenGananciaPermitido { get; set; }
        public decimal PorcDescuentoMaximo { get; set; }
        public string Genero { get; set; }
        public string CodigoBarras { get; set; }
        public bool PermiteDescuento { get; set; }
    }
}
