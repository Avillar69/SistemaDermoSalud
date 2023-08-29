using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Finanzas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SistemaDermoSalud.DataAccess.Finanzas
{
    public class FN_TipoCambioDAO
    {

        public ResultDTO<FN_TipoCambioDTO> ListarTodo(int idEmpresa, string fechaInicio, string fechaFin, SqlConnection cn = null)
        {
            ResultDTO<FN_TipoCambioDTO> oResultDTO = new ResultDTO<FN_TipoCambioDTO>();
            oResultDTO.ListaResultado = new List<FN_TipoCambioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_TipoCambio_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@fechaFin", fechaFin);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_TipoCambioDTO oFN_TipoCambioDTO = new FN_TipoCambioDTO();
                        oFN_TipoCambioDTO.idTipoCambio = Convert.ToInt32(dr["idTipoCambio"] == null ? 0 : Convert.ToInt32(dr["idTipoCambio"].ToString()));
                        oFN_TipoCambioDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oFN_TipoCambioDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oFN_TipoCambioDTO.Fecha = Convert.ToDateTime(dr["Fecha"] == null ? "01-01-2000" : dr["Fecha"].ToString());
                        oFN_TipoCambioDTO.ValorCompra = Convert.ToDecimal(dr["ValorCompra"].ToString());
                        oFN_TipoCambioDTO.ValorVenta = Convert.ToDecimal(dr["ValorVenta"].ToString());
                        oFN_TipoCambioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_TipoCambioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_TipoCambioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_TipoCambioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_TipoCambioDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oFN_TipoCambioDTO.DescripcionMoneda = dr["DescripcionMoneda"] == null ? "" : dr["DescripcionMoneda"].ToString();
                        oResultDTO.ListaResultado.Add(oFN_TipoCambioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_TipoCambioDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_TipoCambioDTO> UpdateInsert(FN_TipoCambioDTO oFN_TipoCambioDTO)
        {
            ResultDTO<FN_TipoCambioDTO> oResultDTO = new ResultDTO<FN_TipoCambioDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_TipoCambio_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idTipoCambio", oFN_TipoCambioDTO.idTipoCambio);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oFN_TipoCambioDTO.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@idMoneda", oFN_TipoCambioDTO.idMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@Fecha", oFN_TipoCambioDTO.Fecha);
                        da.SelectCommand.Parameters.AddWithValue("@ValorCompra", oFN_TipoCambioDTO.ValorCompra);
                        da.SelectCommand.Parameters.AddWithValue("@ValorVenta", oFN_TipoCambioDTO.ValorVenta);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oFN_TipoCambioDTO.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oFN_TipoCambioDTO.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oFN_TipoCambioDTO.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";

                            string fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                            string fechaFin = DateTime.Today.ToShortDateString();
                            oResultDTO.ListaResultado = ListarTodo(oFN_TipoCambioDTO.idEmpresa, fechaInicio, fechaFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<FN_TipoCambioDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_TipoCambioDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_TipoCambioDTO> Delete(FN_TipoCambioDTO oFN_TipoCambioDTO)
        {
            ResultDTO<FN_TipoCambioDTO> oResultDTO = new ResultDTO<FN_TipoCambioDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_TipoCambio_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idTipoCambio", oFN_TipoCambioDTO.idTipoCambio);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            string fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                            string fechaFin = DateTime.Today.ToShortDateString();
                            oResultDTO.ListaResultado = ListarTodo(oFN_TipoCambioDTO.idEmpresa, fechaInicio, fechaFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<FN_TipoCambioDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_TipoCambioDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_MonedaDTO> ListarMonedaTipoCambio(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<Ma_MonedaDTO> oResultDTO = new ResultDTO<Ma_MonedaDTO>();
            oResultDTO.ListaResultado = new List<Ma_MonedaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Moneda_ListarTodoTipoCambio", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_MonedaDTO oMa_MonedaDTO = new Ma_MonedaDTO();
                        oMa_MonedaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oMa_MonedaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oMa_MonedaDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oMa_MonedaDTO.CodigoSunat = dr["CodigoSunat"] == null ? "" : dr["CodigoSunat"].ToString();
                        oMa_MonedaDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_MonedaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_MonedaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_MonedaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_MonedaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_MonedaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMa_MonedaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_MonedaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_TipoCambioDTO> ListarTodoTransformacion(int idEmpresa, string fechaInicio, string fechaFin, SqlConnection cn = null)
        {
            ResultDTO<FN_TipoCambioDTO> oResultDTO = new ResultDTO<FN_TipoCambioDTO>();
            oResultDTO.ListaResultado = new List<FN_TipoCambioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_TipoCambio_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@fechaFin", fechaFin);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_TipoCambioDTO oFN_TipoCambioDTO = new FN_TipoCambioDTO();
                        oFN_TipoCambioDTO.idTipoCambio = Convert.ToInt32(dr["idTipoCambio"] == null ? 0 : Convert.ToInt32(dr["idTipoCambio"].ToString()));
                        oFN_TipoCambioDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oFN_TipoCambioDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oFN_TipoCambioDTO.Fecha = Convert.ToDateTime(dr["Fecha"] == null ? "01-01-2000" : dr["Fecha"].ToString());
                        oFN_TipoCambioDTO.ValorCompra = Convert.ToDecimal(dr["ValorCompra"].ToString());
                        oFN_TipoCambioDTO.ValorVenta = Convert.ToDecimal(dr["ValorVenta"].ToString());
                        oFN_TipoCambioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_TipoCambioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_TipoCambioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_TipoCambioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_TipoCambioDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oFN_TipoCambioDTO.DescripcionMoneda = dr["DescripcionMoneda"] == null ? "" : dr["DescripcionMoneda"].ToString();
                        oResultDTO.ListaResultado.Add(oFN_TipoCambioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_TipoCambioDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_TipoCambioDTO> ListarTipoXFechaUnica(string fechaFin, SqlConnection cn = null)
        {
            ResultDTO<FN_TipoCambioDTO> oResultDTO = new ResultDTO<FN_TipoCambioDTO>();
            oResultDTO.ListaResultado = new List<FN_TipoCambioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_TipoCambio_xFechaUnica", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Fecha", fechaFin);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_TipoCambioDTO oFN_TipoCambioDTO = new FN_TipoCambioDTO();
                        oFN_TipoCambioDTO.idTipoCambio = Convert.ToInt32(dr["idTipoCambio"] == null ? 0 : Convert.ToInt32(dr["idTipoCambio"].ToString()));
                        oFN_TipoCambioDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oFN_TipoCambioDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oFN_TipoCambioDTO.Fecha = Convert.ToDateTime(dr["Fecha"] == null ? "01-01-2000" : dr["Fecha"].ToString());
                        oFN_TipoCambioDTO.ValorCompra = Convert.ToDecimal(dr["ValorCompra"].ToString());
                        oFN_TipoCambioDTO.ValorVenta = Convert.ToDecimal(dr["ValorVenta"].ToString());
                        oFN_TipoCambioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_TipoCambioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_TipoCambioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_TipoCambioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_TipoCambioDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oFN_TipoCambioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_TipoCambioDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_TipoCambioDTO> ListarTipoXFechaActual(string fechaFin, SqlConnection cn = null)
        {
            ResultDTO<FN_TipoCambioDTO> oResultDTO = new ResultDTO<FN_TipoCambioDTO>();
            oResultDTO.ListaResultado = new List<FN_TipoCambioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_TipoCambio_xFechaActual", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Fecha", fechaFin);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_TipoCambioDTO oFN_TipoCambioDTO = new FN_TipoCambioDTO();
                        oFN_TipoCambioDTO.idTipoCambio = Convert.ToInt32(dr["idTipoCambio"] == null ? 0 : Convert.ToInt32(dr["idTipoCambio"].ToString()));
                        oFN_TipoCambioDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oFN_TipoCambioDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oFN_TipoCambioDTO.Fecha = Convert.ToDateTime(dr["Fecha"] == null ? "01-01-2000" : dr["Fecha"].ToString());
                        oFN_TipoCambioDTO.ValorCompra = Convert.ToDecimal(dr["ValorCompra"].ToString());
                        oFN_TipoCambioDTO.ValorVenta = Convert.ToDecimal(dr["ValorVenta"].ToString());
                        oFN_TipoCambioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_TipoCambioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_TipoCambioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_TipoCambioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_TipoCambioDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oFN_TipoCambioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_TipoCambioDTO>();
                }
            }
            return oResultDTO;
        }

    }
}
