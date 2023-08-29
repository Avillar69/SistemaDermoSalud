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
    public class Seg_MenuBL
    {
        Seg_MenuDAO oSeg_MenuDAO = new Seg_MenuDAO();
        public ResultDTO<Seg_MenuDTO> ListarTodo(int idEmpresa)
        {
            return oSeg_MenuDAO.ListarTodo(idEmpresa);
        }
        public ResultDTO<Seg_MenuDTO> ListarMenuxRol(int idEmpresa, int idRol)
        {
            return oSeg_MenuDAO.ListarMenuxRol(idEmpresa, idRol);
        }
        public ResultDTO<Seg_MenuDTO> ListarxID(int idMenu)
        {
            return oSeg_MenuDAO.ListarxID(idMenu);
        }

        public ResultDTO<Seg_MenuDTO> UpdateInsert(Seg_MenuDTO oSeg_MenuDTO)
        {
            ResultDTO<Seg_MenuDTO> oResultDTO = oSeg_MenuDAO.UpdateInsert(oSeg_MenuDTO);
            if (oResultDTO.Resultado == "OK")
            {
                oResultDTO.ListaResultado = ListarTodo(oSeg_MenuDTO.idMenu).ListaResultado;
            }
            return oResultDTO;
        }

        public ResultDTO<Seg_MenuDTO> Delete(Seg_MenuDTO oSeg_MenuDTO)
        {
            ResultDTO<Seg_MenuDTO> oResultDTO = oSeg_MenuDAO.Delete(oSeg_MenuDTO);
            if (oResultDTO.Resultado == "OK")
            {
                oResultDTO.ListaResultado = ListarTodo(oSeg_MenuDTO.idMenu).ListaResultado;
            }
            return oResultDTO;
        }
    }
}
