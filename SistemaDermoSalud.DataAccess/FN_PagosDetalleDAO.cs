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
    public class FN_PagosDetalleDAO
    {
        public ResultDTO<FN_PagosDetalleDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<FN_PagosDetalleDTO> oResultDTO = new ResultDTO<FN_PagosDetalleDTO>();
            oResultDTO.ListaResultado = new List<FN_PagosDetalleDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_PagosDetalle_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_PagosDetalleDTO oFN_PagosDetalleDTO = new FN_PagosDetalleDTO();
                        oFN_PagosDetalleDTO.idPagoDetalle = Convert.ToInt32(dr["idPagoDetalle"] == null ? 0 : Convert.ToInt32(dr["idPagoDetalle"].ToString()));
                        oFN_PagosDetalleDTO.idPago = Convert.ToInt32(dr["idPago"] == null ? 0 : Convert.ToInt32(dr["idPago"].ToString()));
                        oFN_PagosDetalleDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oFN_PagosDetalleDTO.idConcepto = Convert.ToInt32(dr["idConcepto"] == null ? 0 : Convert.ToInt32(dr["idConcepto"].ToString()));
                        oFN_PagosDetalleDTO.Concepto = dr["Concepto"] == null ? "" : dr["Concepto"].ToString();
                        oFN_PagosDetalleDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oFN_PagosDetalleDTO.DescripcionFormaPago = dr["DescripcionFormaPago"] == null ? "" : dr["DescripcionFormaPago"].ToString();
                        oFN_PagosDetalleDTO.NumeroOperacion = dr["NumeroOperacion"] == null ? "" : dr["NumeroOperacion"].ToString();
                        oFN_PagosDetalleDTO.idCuentaBancario = Convert.ToInt32(dr["idCuentaBancario"] == null ? 0 : Convert.ToInt32(dr["idCuentaBancario"].ToString()));
                        oFN_PagosDetalleDTO.NumeroCuenta = Convert.ToInt32(dr["NumeroCuenta"] == null ? 0 : Convert.ToInt32(dr["NumeroCuenta"].ToString()));
                        oFN_PagosDetalleDTO.Monto = Convert.ToInt32(dr["Monto"] == null ? 0 : Convert.ToInt32(dr["Monto"].ToString()));
                        oFN_PagosDetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_PagosDetalleDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_PagosDetalleDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_PagosDetalleDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_PagosDetalleDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oFN_PagosDetalleDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDetalleDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_PagosDetalleDTO> ListarxID(int idPagoDetalle)
        {
            ResultDTO<FN_PagosDetalleDTO> oResultDTO = new ResultDTO<FN_PagosDetalleDTO>();
            oResultDTO.ListaResultado = new List<FN_PagosDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_PagosDetalle_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idPagoDetalle", idPagoDetalle);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_PagosDetalleDTO oFN_PagosDetalleDTO = new FN_PagosDetalleDTO();
                        oFN_PagosDetalleDTO.idPagoDetalle = Convert.ToInt32(dr["idPagoDetalle"].ToString());
                        oFN_PagosDetalleDTO.idPago = Convert.ToInt32(dr["idPago"].ToString());
                        oFN_PagosDetalleDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oFN_PagosDetalleDTO.idConcepto = Convert.ToInt32(dr["idConcepto"].ToString());
                        oFN_PagosDetalleDTO.Concepto = dr["Concepto"].ToString();
                        oFN_PagosDetalleDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"].ToString());
                        oFN_PagosDetalleDTO.DescripcionFormaPago = dr["DescripcionFormaPago"].ToString();
                        oFN_PagosDetalleDTO.NumeroOperacion = dr["NumeroOperacion"].ToString();
                        oFN_PagosDetalleDTO.idCuentaBancario = Convert.ToInt32(dr["idCuentaBancario"].ToString());
                        oFN_PagosDetalleDTO.NumeroCuenta = Convert.ToInt32(dr["NumeroCuenta"].ToString());
                        oFN_PagosDetalleDTO.Monto = Convert.ToInt32(dr["Monto"].ToString());
                        oFN_PagosDetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_PagosDetalleDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_PagosDetalleDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oFN_PagosDetalleDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oFN_PagosDetalleDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oFN_PagosDetalleDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDetalleDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_PagosDetalleDTO> UpdateInsert(FN_PagosDetalleDTO oFN_PagosDetalle)
        {
            ResultDTO<FN_PagosDetalleDTO> oResultDTO = new ResultDTO<FN_PagosDetalleDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_PagosDetalle_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        //da.SelectCommand.Parameters.AddWithValue("@idPagoDetalle",oFN_PagosDetalle.idPagoDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@idDocumento", oFN_PagosDetalle.idCompraVenta);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oFN_PagosDetalle.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@idConcepto", oFN_PagosDetalle.idConcepto);
                        da.SelectCommand.Parameters.AddWithValue("@Concepto", oFN_PagosDetalle.Concepto);
                        da.SelectCommand.Parameters.AddWithValue("@idFormaPago", oFN_PagosDetalle.idFormaPago);
                        da.SelectCommand.Parameters.AddWithValue("@DescripcionFormaPago", oFN_PagosDetalle.DescripcionFormaPago);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroOperacion", oFN_PagosDetalle.NumeroOperacion);
                        da.SelectCommand.Parameters.AddWithValue("@idCuentaBancario", oFN_PagosDetalle.idCuentaBancario);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroCuenta", oFN_PagosDetalle.NumeroCuenta);
                        da.SelectCommand.Parameters.AddWithValue("@Monto", oFN_PagosDetalle.Monto);
                        //da.SelectCommand.Parameters.AddWithValue("@FechaCreacion",oFN_PagosDetalle.FechaCreacion);
                        //da.SelectCommand.Parameters.AddWithValue("@FechaModificacion",oFN_PagosDetalle.FechaModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oFN_PagosDetalle.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oFN_PagosDetalle.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oFN_PagosDetalle.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oFN_PagosDetalle.idEmpresa, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<FN_PagosDetalleDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_PagosDetalleDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_PagosDetalleDTO> Delete(FN_PagosDetalleDTO oFN_PagosDetalle)
        {
            ResultDTO<FN_PagosDetalleDTO> oResultDTO = new ResultDTO<FN_PagosDetalleDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_PagosDetalle_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idPagoDetalle", oFN_PagosDetalle.idPagoDetalle);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oFN_PagosDetalle.idEmpresa, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<FN_PagosDetalleDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_PagosDetalleDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
