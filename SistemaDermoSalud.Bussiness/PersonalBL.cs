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
    public class PersonalBL
    {
        PersonalDAO oPersonalDAO = new PersonalDAO();
        public ResultDTO<PersonalDTO> ListarTodo() { 
        
            return oPersonalDAO.ListarTodo();
        }

        public ResultDTO<PersonalDTO> ListarxID(int idPersonal)
        {
        return oPersonalDAO.ListarxID(idPersonal);
        }

        public ResultDTO<PersonalDTO> UpdateInsert(PersonalDTO oPersonalDTO)
        {
            return oPersonalDAO.UpdateInsert( oPersonalDTO);
        }

        public ResultDTO<PersonalDTO> Delete(PersonalDTO oPersonalDTO)
        {
            return oPersonalDAO.Delete( oPersonalDTO);
        }
        // 
        public ResultDTO<PersonalDTO> UpdPorcentaje(PersonalDTO oPersonalDTO)
        {
            return oPersonalDAO.UpdatePorcentaje(oPersonalDTO);
        }


}
}
