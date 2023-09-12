using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
    public class FN_CajaBL
    {
        FN_CajaDAO oFN_CajaDAO = new FN_CajaDAO();
        public ResultDTO<FN_CajaDTO> ListarTodo()
        {
            return oFN_CajaDAO.ListarTodo();
        }
        public ResultDTO<FN_CajaDTO> ListarxFecha(DateTime FechaInicio, DateTime FechaFin)
        {
            return oFN_CajaDAO.ListarxFecha(FechaInicio, FechaFin);
        }
        public ResultDTO<FN_CajaDTO> ListarxID(int idCaja)
        {
            return oFN_CajaDAO.ListarxID(idCaja);
        }

        public ResultDTO<FN_CajaDTO> UpdateInsert(FN_CajaDTO oFN_CajaDTO)
        {
            return oFN_CajaDAO.UpdateInsert(oFN_CajaDTO);
        }

        public ResultDTO<FN_CajaDTO> Delete(FN_CajaDTO oFN_CajaDTO)
        {
            return oFN_CajaDAO.Delete(oFN_CajaDTO);
        }

        public string NroCajaUltimo(int idEmpresa)
        {
            return oFN_CajaDAO.NroCajaUltimo(idEmpresa);
        }
        public string EstadoCaja(int idCaja)
        {
            return oFN_CajaDAO.EstadoCaja(idCaja);
        }
        public ResultDTO<FN_CajaDTO> ReporteCajaxID(int idCaja)
        {
            return oFN_CajaDAO.ReporteCajaxID(idCaja);
        }
        public ResultDTO<FN_CajaDTO> CerrarCaja(FN_CajaDTO oFN_CajaDTO)
        {
            return oFN_CajaDAO.CerrarCaja(oFN_CajaDTO);
        }
        public int ValidarCajaAperturada()
        {
            return oFN_CajaDAO.ValidarCajaAperturada();
        }
    }
}
