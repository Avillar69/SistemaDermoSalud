using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
    public class FN_ConceptosCajaBL
    {

        FN_ConceptosCajaDAO oFN_ConceptosCajaDAO = new FN_ConceptosCajaDAO();
        public ResultDTO<FN_ConceptosCajaDTO> ListarTodo()
        {
            return oFN_ConceptosCajaDAO.ListarTodo();
        }

        public ResultDTO<FN_ConceptosCajaDTO> ListarxID(int idConceptoCaja)
        {
            return oFN_ConceptosCajaDAO.ListarxID(idConceptoCaja);
        }

        public ResultDTO<FN_ConceptosCajaDTO> UpdateInsert(FN_ConceptosCajaDTO oFN_ConceptosCajaDTO)
        {
            return oFN_ConceptosCajaDAO.UpdateInsert(oFN_ConceptosCajaDTO);
        }

        public ResultDTO<FN_ConceptosCajaDTO> Delete(FN_ConceptosCajaDTO oFN_ConceptosCajaDTO)
        {
            return oFN_ConceptosCajaDAO.Delete(oFN_ConceptosCajaDTO);
        }

    }
}
