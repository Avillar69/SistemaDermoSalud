using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class INV_StockDTO
    {
        public int idStock { get; set; }
        public int idEmpresa { get; set; }
        public int idArticulo { get; set; }
        public string descArticulo { get; set; }
        public int idAlmacen { get; set; }
        public string descAlmacen { get; set; }
        public decimal Stock { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string CodArticulo { get; set; }
        public string descCategoria { get; set; }
    }
}
