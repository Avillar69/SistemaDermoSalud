using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.DataAccess;

namespace SistemaDermoSalud.Business
{
    public class Seg_AccesoBL
    {
        Seg_AccesoDAO oSeg_AccesoDAO = new Seg_AccesoDAO();
        //public List<Seg_AccesoDTO>ListarTodo()
        //{
        //    return oSeg_AccesoDAO.ListarTodo();
        //}
        public ResultDTO<Seg_AccesoDTO> UpdateInsert(string cad, int idEmpresa, int idRol)
        {
            return oSeg_AccesoDAO.UpdateInsert(cad, idEmpresa, idRol);
        }

        //public Resul Delete(int idRol,int idEmpresa)
        //{
        //    return oSeg_AccesoDAO.Delete(idRol,idEmpresa);
        //}
        public ResultDTO<Seg_AccesoDTO> ListarxRol(int idRol, int idEmpresa)
        {
            return oSeg_AccesoDAO.ListarxRol(idRol, idEmpresa);
        }
    }
}
