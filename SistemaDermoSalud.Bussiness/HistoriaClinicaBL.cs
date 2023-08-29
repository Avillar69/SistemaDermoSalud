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
    public class HistoriaClinicaBL
    {
        HistoriaClinicaDAO oHistoriaClinicaDAO = new HistoriaClinicaDAO();
        public ResultDTO<HistoriaClinicaDTO>ListarTodo()
        {
            return oHistoriaClinicaDAO.ListarTodo();
        }

        public ResultDTO<HistoriaClinicaDTO> ListarxID(int idHistoria)
        {
        return oHistoriaClinicaDAO.ListarxID(idHistoria);
        }

        public ResultDTO<HistoriaClinicaDTO> UpdateInsert(HistoriaClinicaDTO oHistoriaClinicaDTO)
        {
            return oHistoriaClinicaDAO.UpdateInsert( oHistoriaClinicaDTO);
        }

        public ResultDTO<HistoriaClinicaDTO> Delete(HistoriaClinicaDTO oHistoriaClinicaDTO)
        {
            return oHistoriaClinicaDAO.Delete( oHistoriaClinicaDTO);
        }
        public string NroHistoriaUltimo()
        {
            return oHistoriaClinicaDAO.NroHistoriaUltimo();
        }
        public ResultDTO<HistoriaClinica_ArchivosDTO> GetFileArchivo(int idHistoriaArchivo)
        {
            return oHistoriaClinicaDAO.GetFileArchivo(idHistoriaArchivo);
        }
    }
}
