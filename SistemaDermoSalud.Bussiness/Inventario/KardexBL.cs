using SistemaDermoSalud.DataAccess.Inventario;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business.Inventario
{
    public class KardexBL
    {
        public ResultDTO<KardexDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, int idCategoria, int idArticulo)
        {
            return new KardexDAO().ListarRangoFecha(idEmpresa, fechaInicio, fechaFin, idCategoria, idArticulo);
        }
    }
}
