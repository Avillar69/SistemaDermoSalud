using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.DataAccess.Mantenimiento;
using SistemaDermoSalud.Entities;

namespace SistemaDermoSalud.Business.Mantenimiento
{
    public class Ma_SerieBL
    {
        Ma_SerieDAO oMa_SerieDAO = new Ma_SerieDAO();
        public ResultDTO<VEN_SerieDTO> ListarRangoFecha(int idEmpresa, string fechaInicio, string fechaFin)
        {
            return oMa_SerieDAO.ListarRangoFecha(idEmpresa, fechaInicio, fechaFin);
        }
        public ResultDTO<VEN_SerieDTO> ListarxID(int idSerie)
        {
            return oMa_SerieDAO.ListarxID(idSerie);
        }
        public ResultDTO<Ma_SerieDTO> ObtenerNumeroVenta(string idSerie)
        {
            return oMa_SerieDAO.ObtenerNumeroVenta(idSerie);
        }

        public ResultDTO<VEN_SerieDTO> ListarTodo()
        {
            return oMa_SerieDAO.ListarTodo(1);
        }
        public ResultDTO<VEN_SerieDTO> UpdateInsert(VEN_SerieDTO oVEN_SerieDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oMa_SerieDAO.UpdateInsert(oVEN_SerieDTO, fechaInicio, fechaFin);
        }
        public ResultDTO<VEN_SerieDTO> Delete(VEN_SerieDTO oVEN_SerieDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oMa_SerieDAO.Delete(oVEN_SerieDTO, fechaInicio, fechaFin);
        }
    }
}
