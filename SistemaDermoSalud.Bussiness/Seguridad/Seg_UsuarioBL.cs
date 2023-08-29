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
    public class Seg_UsuarioBL
    {
        Seg_UsuarioDAO oSeg_UsuarioDAO = new Seg_UsuarioDAO();
        public List<Seg_UsuarioDTO> ListarTodo(int idEmpresa)
        {
            return oSeg_UsuarioDAO.ListarTodo(idEmpresa);
        }
        public List<Seg_UsuarioDTO> ListarxID(int idUsuario)
        {
            return oSeg_UsuarioDAO.ListarxID(idUsuario);
        }
        public ResultDTO<Seg_UsuarioDTO> UpdateInsert(Seg_UsuarioDTO oSeg_UsuarioDTO)
        {
            ResultDTO<Seg_UsuarioDTO> objResult = oSeg_UsuarioDAO.UpdateInsert(oSeg_UsuarioDTO);
            objResult.ListaResultado = oSeg_UsuarioDAO.ListarTodo(oSeg_UsuarioDTO.idEmpresa);
            return objResult;
        }
        public ResultDTO<Seg_UsuarioDTO> Delete(Seg_UsuarioDTO oSeg_UsuarioDTO)
        {
            ResultDTO<Seg_UsuarioDTO> objResult = oSeg_UsuarioDAO.Delete(oSeg_UsuarioDTO);
            objResult.ListaResultado = oSeg_UsuarioDAO.ListarTodo(oSeg_UsuarioDTO.idEmpresa);
            return objResult;
        }
        public ResultDTO<Seg_UsuarioDTO> ValidarLogin(string Usuario, string Password)
        {
            ResultDTO<Seg_UsuarioDTO> objResult = oSeg_UsuarioDAO.ValidarLogin(Usuario,Password);
            return objResult;
        }
    }
}
