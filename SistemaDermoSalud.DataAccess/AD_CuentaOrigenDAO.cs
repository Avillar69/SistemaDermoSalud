using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SistemaDermoSalud.DataAccess
{
    public class AD_CuentaOrigenDAO
    {
        public ResultDTO<AD_CuentaOrigenDTO> ListarRangoFecha(DateTime fechaInicio, DateTime fechaFin, SqlConnection cn = null)
        {
            ResultDTO<AD_CuentaOrigenDTO> oResultDTO = new ResultDTO<AD_CuentaOrigenDTO>();
            oResultDTO.ListaResultado = new List<AD_CuentaOrigenDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_AD_CuentaOrigen_ListarxFecha", cn);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AD_CuentaOrigenDTO oAD_CuentaOrigenDTO = new AD_CuentaOrigenDTO();
                        oAD_CuentaOrigenDTO.idCuentaOrigen = Convert.ToInt32(dr["idCuentaOrigen"] == null ? 0 : Convert.ToInt32(dr["idCuentaOrigen"].ToString()));
                        oAD_CuentaOrigenDTO.NombreCuenta = dr["idEmpresa"] == null ? "" : dr["idEmpresa"].ToString();
                        oAD_CuentaOrigenDTO.Banco = Convert.ToInt32(dr["Banco"] == null ? 0 : Convert.ToInt32(dr["Banco"].ToString()));
                        oAD_CuentaOrigenDTO.DescripcionBanco = dr["DescripcionBanco"] == null ? "" : dr["DescripcionBanco"].ToString();
                        oAD_CuentaOrigenDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oAD_CuentaOrigenDTO.DescMoneda = dr["DescMoneda"] == null ? "" : dr["DescMoneda"].ToString();
                        oAD_CuentaOrigenDTO.NumeroCuenta = dr["NumeroCuenta"].ToString();
                        oAD_CuentaOrigenDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oAD_CuentaOrigenDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AD_CuentaOrigenDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_CuentaOrigenDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<AD_CuentaOrigenDTO> oResultDTO = new ResultDTO<AD_CuentaOrigenDTO>();
            oResultDTO.ListaResultado = new List<AD_CuentaOrigenDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_AD_CuentaOrigen_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AD_CuentaOrigenDTO oAD_CuentaOrigenDTO = new AD_CuentaOrigenDTO();
                        oAD_CuentaOrigenDTO.idCuentaOrigen = Convert.ToInt32(dr["idCuentaOrigen"] == null ? 0 : Convert.ToInt32(dr["idCuentaOrigen"].ToString()));
                        oAD_CuentaOrigenDTO.NombreCuenta = dr["NombreCuenta"] == null ? "" : dr["NombreCuenta"].ToString();
                        oAD_CuentaOrigenDTO.Banco = Convert.ToInt32(dr["Banco"] == null ? 0 : Convert.ToInt32(dr["Banco"].ToString()));
                        oAD_CuentaOrigenDTO.DescripcionBanco = dr["DescripcionBanco"] == null ? "" : dr["DescripcionBanco"].ToString();
                        oAD_CuentaOrigenDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oAD_CuentaOrigenDTO.DescMoneda = dr["DescMoneda"] == null ? "" : dr["DescMoneda"].ToString();
                        oAD_CuentaOrigenDTO.NumeroCuenta = dr["NumeroCuenta"].ToString();
                        oAD_CuentaOrigenDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oAD_CuentaOrigenDTO);
                    }

                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AD_CuentaOrigenDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_CuentaOrigenDTO> ListarxID(int id)
        {
            ResultDTO<AD_CuentaOrigenDTO> oResultDTO = new ResultDTO<AD_CuentaOrigenDTO>();
            oResultDTO.ListaResultado = new List<AD_CuentaOrigenDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_AD_CuentaOrigen_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCuentaOrigen", id);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AD_CuentaOrigenDTO oAD_CuentaOrigenDTO = new AD_CuentaOrigenDTO();
                        oAD_CuentaOrigenDTO.idCuentaOrigen = Convert.ToInt32(dr["idCuentaOrigen"] == null ? 0 : Convert.ToInt32(dr["idCuentaOrigen"].ToString()));
                        oAD_CuentaOrigenDTO.NombreCuenta = dr["NombreCuenta"] == null ? "" : dr["NombreCuenta"].ToString();
                        oAD_CuentaOrigenDTO.Banco = Convert.ToInt32(dr["Banco"] == null ? 0 : Convert.ToInt32(dr["Banco"].ToString()));
                        oAD_CuentaOrigenDTO.DescripcionBanco = dr["DescripcionBanco"] == null ? "" : dr["DescripcionBanco"].ToString();
                        oAD_CuentaOrigenDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oAD_CuentaOrigenDTO.DescMoneda = dr["DescMoneda"] == null ? "" : dr["DescMoneda"].ToString();
                        oAD_CuentaOrigenDTO.NumeroCuenta = dr["NumeroCuenta"].ToString();
                        oAD_CuentaOrigenDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oAD_CuentaOrigenDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AD_CuentaOrigenDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_CuentaOrigenDTO> UpdateInsert(AD_CuentaOrigenDTO oAD_CuentaOrigenDTO, DateTime FechaInicio, DateTime FechaFin)
        {
            ResultDTO<AD_CuentaOrigenDTO> oResultDTO = new ResultDTO<AD_CuentaOrigenDTO>();
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(60)
            };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                using (SqlConnection cn = new Conexion().conectar())
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SP_AD_CuentaOrigen_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idCuentaOrigen", oAD_CuentaOrigenDTO.idCuentaOrigen);
                        da.SelectCommand.Parameters.AddWithValue("@NombreCuenta", oAD_CuentaOrigenDTO.NombreCuenta);
                        da.SelectCommand.Parameters.AddWithValue("@Banco", oAD_CuentaOrigenDTO.Banco);
                        da.SelectCommand.Parameters.AddWithValue("@DescripcionBanco", oAD_CuentaOrigenDTO.DescripcionBanco);
                        da.SelectCommand.Parameters.AddWithValue("@idMoneda", oAD_CuentaOrigenDTO.idMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@DescMoneda", oAD_CuentaOrigenDTO.DescMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroCuenta", oAD_CuentaOrigenDTO.NumeroCuenta);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oAD_CuentaOrigenDTO.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<AD_CuentaOrigenDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<AD_CuentaOrigenDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_CuentaOrigenDTO> Delete(AD_CuentaOrigenDTO oAD_CuentaOrigenDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<AD_CuentaOrigenDTO> oResultDTO = new ResultDTO<AD_CuentaOrigenDTO>();
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(60)
            };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                using (SqlConnection cn = new Conexion().conectar())
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SP_AD_CuentaOrigen_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCuentaOrigen", oAD_CuentaOrigenDTO.idCuentaOrigen);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<AD_CuentaOrigenDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<AD_CuentaOrigenDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
