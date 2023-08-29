using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
   public class Ma_TipoDocumentoBL
    {
        Ma_TipoDocumentoDAO oMa_TipoDocumentoDAO = new Ma_TipoDocumentoDAO();
        public ResultDTO<Ma_TipoDocumentoDTO> ListarTodo()
        {
            return oMa_TipoDocumentoDAO.ListarTodo();
        }
        public ResultDTO<Ma_TipoDocumentoDTO> ListarxID(int idTipoDocumento)
        {
            return oMa_TipoDocumentoDAO.ListarxID(idTipoDocumento);
        }
        public ResultDTO<Ma_TipoDocumentoDTO> UpdateInsert(Ma_TipoDocumentoDTO oMa_TipoDocumentoDTO)
        {
            return oMa_TipoDocumentoDAO.UpdateInsert(oMa_TipoDocumentoDTO);
        }
        public ResultDTO<Ma_TipoDocumentoDTO> Delete(Ma_TipoDocumentoDTO oMa_TipoDocumentoDTO)
        {
            return oMa_TipoDocumentoDAO.Delete(oMa_TipoDocumentoDTO);
        }
    }
}
