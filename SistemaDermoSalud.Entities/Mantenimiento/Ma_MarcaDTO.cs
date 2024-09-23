using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Mantenimiento
{
    public class Ma_MarcaDTO
    {
        public int idMarca { get; set; }
        public string Marca { get; set; }
        public int TotalCompra { get; set; }
        public int TotalVenta { get; set; }
        public int StockActual { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
    }
}
