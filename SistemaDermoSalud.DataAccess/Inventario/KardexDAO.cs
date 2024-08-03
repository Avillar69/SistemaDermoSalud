using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Inventario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.DataAccess.Inventario
{
    public class KardexDAO
    {
        public ResultDTO<KardexDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, int idMarca, int idProducto, SqlConnection cn = null)
        {
            ResultDTO<KardexDTO> oResultDTO = new ResultDTO<KardexDTO>();
            oResultDTO.ListaResultado = new List<KardexDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_INV_RepKardex", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idMarca", idMarca);
                    da.SelectCommand.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@fechaFin", fechaFin);
                    da.SelectCommand.Parameters.AddWithValue("@idProducto", idProducto);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        KardexDTO oKardexDTO = new KardexDTO();
                        oKardexDTO.FechaMovimiento = Convert.ToDateTime(dr["FechaMovimiento"].ToString());
                        oKardexDTO.DocReferencia = dr["DocReferencia"] == null ? "" : dr["DocReferencia"].ToString();
                        oKardexDTO.idArticulo = dr["idArticulo"] == null ? "" : dr["idArticulo"].ToString();
                        oKardexDTO.Articulo = dr["Articulo"] == null ? "" : dr["Articulo"].ToString();
                        oKardexDTO.UnidadMedida = dr["UnidadMedida"] == null ? "" : dr["UnidadMedida"].ToString();
                        oKardexDTO.StockInicial = string.IsNullOrWhiteSpace(dr["StockInicial"].ToString()) ? 0 : Convert.ToDecimal(dr["StockInicial"].ToString());
                        oKardexDTO.PrecioPromedio = Convert.ToDecimal(dr["PrecioPromedio"] == null ? 0 : Convert.ToDecimal(dr["PrecioPromedio"].ToString()));
                        oKardexDTO.CantidadEntrada = Convert.ToDecimal(dr["CantidadEntrada"] == null ? 0 : Convert.ToDecimal(dr["CantidadEntrada"].ToString()));
                        oKardexDTO.PrecioEntrada = Convert.ToDecimal(dr["PrecioEntrada"] == null ? 0 : Convert.ToDecimal(dr["PrecioEntrada"].ToString()));
                        oKardexDTO.TotalEntrada = Convert.ToDecimal(dr["TotalEntrada"] == null ? 0 : Convert.ToDecimal(dr["TotalEntrada"].ToString()));
                        oKardexDTO.CantidadSalida = Convert.ToDecimal(dr["CantidadSalida"] == null ? 0 : Convert.ToDecimal(dr["CantidadSalida"].ToString()));
                        oKardexDTO.PrecioSalida = Convert.ToDecimal(dr["PrecioSalida"] == null ? 0 : Convert.ToDecimal(dr["PrecioSalida"].ToString()));
                        oKardexDTO.TotalSalida = Convert.ToDecimal(dr["TotalSalida"] == null ? 0 : Convert.ToDecimal(dr["TotalSalida"].ToString()));
                        oKardexDTO.Observaciones = dr["Observaciones"] == null ? "" : dr["Observaciones"].ToString();
                        oKardexDTO.Movimiento = dr["Movimiento"] == null ? "" : dr["Movimiento"].ToString();
                        oResultDTO.ListaResultado.Add(oKardexDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<KardexDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
