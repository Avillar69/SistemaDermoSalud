using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;

namespace SistemaDermoSalud.Business
{
    public class AtencionMedicaBL
    {
        AtencionMedicaDAO oAtencionMedicaDAO = new AtencionMedicaDAO();
        public ResultDTO<AtencionMedicaDTO> ListarTodo()
        {
            return oAtencionMedicaDAO.ListarTodo();
        }
        public ResultDTO<AtencionMedicaDTO> ListarxID(int idHistoria)
        {
            return oAtencionMedicaDAO.ListarxID(idHistoria);
        }
        public ResultDTO<AtencionMedicaDTO> UpdateInsert(AtencionMedicaDTO oAtencionMedicaDTO)
        {
            return oAtencionMedicaDAO.UpdateInsert(oAtencionMedicaDTO);
        }
        public ResultDTO<AtencionMedicaDTO> ListarxPaciente(int idPaciente)
        {
            return oAtencionMedicaDAO.ListarxPaciente(idPaciente);
        }
        public string NroRecetaUltimo()
        {
            return oAtencionMedicaDAO.NroRecetaUltimo();
        }
        public ResultDTO<AtencionMedicaDTO> UpdateInsertReceta(AtencionMedicaDTO oAtencionMedicaDTO)
        {
            return oAtencionMedicaDAO.UpdateInsertReceta(oAtencionMedicaDTO);
        }
        public string UltimoIdAtencionMedica()
        {
            return oAtencionMedicaDAO.UltimoIdReceta();
        }
        public ResultDTO<AtencionMedica_RecetaDTO> ObtenerRecetaxID(int idReceta)
        {
            return oAtencionMedicaDAO.ObtenerRecetaxID(idReceta);
        }
    }
}
