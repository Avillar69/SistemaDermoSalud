using SistemaDermoSalud.DataAccess.Mantenimiento;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business.Mantenimiento
{
    public class Ma_AlmacenBL
    {
        Ma_AlmacenDAO oMa_AlmacenDAO = new Ma_AlmacenDAO();
        public ResultDTO<Ma_AlmacenDTO> ListarTodo(int idEmpresa, string Activo = "")
        {
            return oMa_AlmacenDAO.ListarTodo(idEmpresa, Activo);
        }

        public ResultDTO<Ma_AlmacenDTO> ListarxID(int idAlmacen)
        {
            return oMa_AlmacenDAO.ListarxID(idAlmacen);
        }

        public ResultDTO<Ma_AlmacenDTO> ListarxIDTransfromacion(int idAlmacen, int categoria)
        {
            return oMa_AlmacenDAO.ListarxIDTransfromacion(idAlmacen, categoria);
        }
        public ResultDTO<Ma_AlmacenDTO> UpdateInsert(Ma_AlmacenDTO oMa_AlmacenDTO)
        {
            return oMa_AlmacenDAO.UpdateInsert(oMa_AlmacenDTO);
        }

        public ResultDTO<Ma_AlmacenDTO> Delete(Ma_AlmacenDTO oMa_AlmacenDTO)
        {
            return oMa_AlmacenDAO.Delete(oMa_AlmacenDTO);
        }


    }
}
