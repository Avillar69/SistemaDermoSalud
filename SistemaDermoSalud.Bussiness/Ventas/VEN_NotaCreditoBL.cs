using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDermoSalud.DataAccess.Ventas;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Ventas;

namespace SistemaDermoSalud.Business.Ventas
{
    public class VEN_NotaCreditoBL
    {
        VEN_NotaCreditoDAO oVEN_NotaCreditoDAO = new VEN_NotaCreditoDAO();
        public ResultDTO<VEN_NotaCreditoDTO> ListarTodo(int idEmpresa, string tipoNota = null)
        {
            return oVEN_NotaCreditoDAO.ListarTodo(idEmpresa, tipoNota);
        }
        public ResultDTO<VEN_NotaCreditoDTO> ListarRangoFechas(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, string tipoNota = null, string tipoDoc = null)
        {
            return oVEN_NotaCreditoDAO.ListarRangoFechas(idEmpresa, fechaInicio, fechaFin, tipoNota, tipoDoc);
        }

        public ResultDTO<VEN_NotaCreditoDTO> ListarxID(int idNotaCredito)
        {
            return oVEN_NotaCreditoDAO.ListarxID(idNotaCredito);
        }

        public ResultDTO<VEN_NotaCreditoDTO> UpdateInsert(VEN_NotaCreditoDTO oVEN_Nota_CreditoDebitoDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_NotaCreditoDAO.UpdateInsert(oVEN_Nota_CreditoDebitoDTO, fechaInicio, fechaFin);
        }

        public ResultDTO<VEN_DocVentaDTO> ListarDocVenta()
        {
            return oVEN_NotaCreditoDAO.ListarDocVenta();
        }
        public ResultDTO<VEN_DocVentaDTO> ListarDocVentaxCli(int idCliente)
        {
            return oVEN_NotaCreditoDAO.ListarDocVentaxCli(idCliente);
        }
        public string ValidarDocumento(string serie, string numero)
        {
            return oVEN_NotaCreditoDAO.ValidarDocumento(serie, numero);
        }
        public ResultDTO<VEN_SerieDTO> ObtenerNumero(string serie, string tipo)//int idSerie
        {
            return oVEN_NotaCreditoDAO.ObtenerNumero(serie, tipo);//idSerie
        }
        public string ValidarNotaCredito(VEN_NotaCreditoDTO oVEN_NotaCreditoDTO)
        {
            return oVEN_NotaCreditoDAO.ValidarNotaCredito(oVEN_NotaCreditoDTO);
        }
        public ResultDTO<VEN_NotaCreditoDTO> Anular(VEN_NotaCreditoDTO oVEN_NotaCreditoDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            return oVEN_NotaCreditoDAO.Anular(oVEN_NotaCreditoDTO, fechaInicio, fechaFin);
        }
        public VEN_NotaCreditoDTO ListarxIDNota(int idNotaCredito)
        {
            return oVEN_NotaCreditoDAO.ListarxIDNota(idNotaCredito);
        }
        public int validar_notacredito_delete(int idDocVenta)
        {
            return oVEN_NotaCreditoDAO.validar_notacredito_delete(idDocVenta);
        }

        public ResultDTO<VEN_NotaCreditoDTO> ListarxID_DatosImpresion(int idNotaCredito)
        {
            return oVEN_NotaCreditoDAO.ListarxID_DatosImpresion(idNotaCredito);
        }
        public int validar_NotaCredito(int idNotaCredito)
        {
            return oVEN_NotaCreditoDAO.validar_NotaCredito(idNotaCredito);
        }
    }
}
