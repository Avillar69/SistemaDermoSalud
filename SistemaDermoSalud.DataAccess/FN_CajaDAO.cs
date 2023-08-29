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
  public  class FN_CajaDAO
    {
        public ResultDTO<FN_CajaDTO> ListarTodo( SqlConnection cn = null)
        {
            ResultDTO<FN_CajaDTO> oResultDTO = new ResultDTO<FN_CajaDTO>();
            oResultDTO.ListaResultado = new List<FN_CajaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_Caja_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_CajaDTO oFN_CajaDTO = new FN_CajaDTO();
                        oFN_CajaDTO.idCaja = Convert.ToInt32(dr["idCaja"] == null ? 0 : Convert.ToInt32(dr["idCaja"].ToString()));
                        oFN_CajaDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oFN_CajaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oFN_CajaDTO.PeriodoAno = dr["PeriodoAno"] == null ? "" : dr["PeriodoAno"].ToString();
                        oFN_CajaDTO.NroCaja = dr["NroCaja"] == null ? "" : dr["NroCaja"].ToString();
                        oFN_CajaDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oFN_CajaDTO.FechaApertura = Convert.ToDateTime(dr["FechaApertura"].ToString());
                        oFN_CajaDTO.FechaCierre = Convert.ToDateTime(dr["FechaCierre"].ToString());
                        oFN_CajaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oFN_CajaDTO.MontoInicio = Convert.ToDecimal(dr["MontoInicio"] == null ? 0 : Convert.ToDecimal(dr["MontoInicio"].ToString()));
                        oFN_CajaDTO.MontoIngreso = Convert.ToDecimal(dr["MontoIngreso"] == null ? 0 : Convert.ToDecimal(dr["MontoIngreso"].ToString()));
                        oFN_CajaDTO.MontoSalida = Convert.ToDecimal(dr["MontoSalida"] == null ? 0 : Convert.ToDecimal(dr["MontoSalida"].ToString()));
                        oFN_CajaDTO.MontoSaldo = Convert.ToDecimal(dr["MontoSaldo"] == null ? 0 : Convert.ToDecimal(dr["MontoSaldo"].ToString()));
                        oFN_CajaDTO.EstadoCaja = dr["EstadoCaja"] == null ? "" : dr["EstadoCaja"].ToString();
                        oFN_CajaDTO.EstadoCaja = oFN_CajaDTO.EstadoCaja == "C" ?  "CERRADA" : "ABIERTA";
                        oFN_CajaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_CajaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_CajaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_CajaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_CajaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        //oFN_CajaDTO.Usuario = dr["Usuario"] == null ? "" : dr["Usuario"].ToString();
                        oResultDTO.ListaResultado.Add(oFN_CajaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_CajaDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_CajaDTO> ListarxFecha(DateTime FechaInicio, DateTime FechaFin, SqlConnection cn = null)
        {
            ResultDTO<FN_CajaDTO> oResultDTO = new ResultDTO<FN_CajaDTO>();
            oResultDTO.ListaResultado = new List<FN_CajaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_Caja_ListarxFecha", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", FechaFin);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_CajaDTO oFN_CajaDTO = new FN_CajaDTO();
                        oFN_CajaDTO.idCaja = Convert.ToInt32(dr["idCaja"] == null ? 0 : Convert.ToInt32(dr["idCaja"].ToString()));
                        oFN_CajaDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oFN_CajaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oFN_CajaDTO.PeriodoAno = dr["PeriodoAno"] == null ? "" : dr["PeriodoAno"].ToString();
                        oFN_CajaDTO.NroCaja = dr["NroCaja"] == null ? "" : dr["NroCaja"].ToString();
                        oFN_CajaDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oFN_CajaDTO.FechaApertura = Convert.ToDateTime(dr["FechaApertura"].ToString());
                        oFN_CajaDTO.FechaCierre = Convert.ToDateTime(dr["FechaCierre"].ToString());
                        oFN_CajaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oFN_CajaDTO.MontoInicio = Convert.ToDecimal(dr["MontoInicio"] == null ? 0 : Convert.ToDecimal(dr["MontoInicio"].ToString()));
                        oFN_CajaDTO.MontoIngreso = Convert.ToDecimal(dr["MontoIngreso"] == null ? 0 : Convert.ToDecimal(dr["MontoIngreso"].ToString()));
                        oFN_CajaDTO.MontoSalida = Convert.ToDecimal(dr["MontoSalida"] == null ? 0 : Convert.ToDecimal(dr["MontoSalida"].ToString()));
                        oFN_CajaDTO.MontoSaldo = Convert.ToDecimal(dr["MontoSaldo"] == null ? 0 : Convert.ToDecimal(dr["MontoSaldo"].ToString()));
                        oFN_CajaDTO.EstadoCaja = dr["EstadoCaja"] == null ? "" : dr["EstadoCaja"].ToString();
                        oFN_CajaDTO.EstadoCaja = oFN_CajaDTO.EstadoCaja == "C" ? "CERRADA" : "ABIERTA";
                        oFN_CajaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_CajaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_CajaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_CajaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_CajaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oFN_CajaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_CajaDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_CajaDTO> ListarxID(int idCaja)
        {
            ResultDTO<FN_CajaDTO> oResultDTO = new ResultDTO<FN_CajaDTO>();
            oResultDTO.ListaResultado = new List<FN_CajaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_Caja_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCaja", idCaja);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_CajaDTO oFN_CajaDTO = new FN_CajaDTO();
                        oFN_CajaDTO.idCaja = Convert.ToInt32(dr["idCaja"].ToString());
                        oFN_CajaDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oFN_CajaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oFN_CajaDTO.PeriodoAno = dr["PeriodoAno"].ToString();
                        oFN_CajaDTO.NroCaja = dr["NroCaja"].ToString();
                        oFN_CajaDTO.Descripcion = dr["Descripcion"].ToString();
                        oFN_CajaDTO.FechaApertura = Convert.ToDateTime(dr["FechaApertura"].ToString());
                        oFN_CajaDTO.FechaCierre = Convert.ToDateTime(dr["FechaCierre"].ToString());
                        oFN_CajaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oFN_CajaDTO.MontoInicio = Convert.ToDecimal(dr["MontoInicio"].ToString());
                        oFN_CajaDTO.MontoIngreso = Convert.ToDecimal(dr["MontoIngreso"].ToString());
                        oFN_CajaDTO.MontoSalida = Convert.ToDecimal(dr["MontoSalida"].ToString());
                        oFN_CajaDTO.MontoSaldo = Convert.ToDecimal(dr["MontoSaldo"].ToString());
                        oFN_CajaDTO.EstadoCaja = dr["EstadoCaja"].ToString();
                        oFN_CajaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_CajaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_CajaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oFN_CajaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oFN_CajaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oFN_CajaDTO.Usuario = dr["Usuario"].ToString();
                        oFN_CajaDTO.Moneda = dr["Moneda"].ToString();
                        oResultDTO.ListaResultado.Add(oFN_CajaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_CajaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_CajaDTO> UpdateInsert(FN_CajaDTO oFN_Caja)
        {
            ResultDTO<FN_CajaDTO> oResultDTO = new ResultDTO<FN_CajaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_Caja_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idCaja", oFN_Caja.idCaja);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oFN_Caja.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@PeriodoAno", oFN_Caja.PeriodoAno);
                        da.SelectCommand.Parameters.AddWithValue("@NroCaja", oFN_Caja.NroCaja);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oFN_Caja.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@idMoneda", oFN_Caja.idMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@MontoInicio", oFN_Caja.MontoInicio);
                        da.SelectCommand.Parameters.AddWithValue("@MontoIngreso", oFN_Caja.MontoIngreso);
                        da.SelectCommand.Parameters.AddWithValue("@MontoSalida", oFN_Caja.MontoSalida);
                        da.SelectCommand.Parameters.AddWithValue("@MontoSaldo", oFN_Caja.MontoSaldo);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oFN_Caja.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oFN_Caja.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoCaja", oFN_Caja.EstadoCaja);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oFN_Caja.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<FN_CajaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_CajaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_CajaDTO> Delete(FN_CajaDTO oFN_Caja)
        {
            ResultDTO<FN_CajaDTO> oResultDTO = new ResultDTO<FN_CajaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_Caja_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idCaja", oFN_Caja.idCaja);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<FN_CajaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_CajaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public string NroCajaUltimo(int idEmpresa)
        {
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_Caja_UltimoNroCaja", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    var nrocaja = da.SelectCommand.ExecuteScalar();
                    return nrocaja.ToString();
                }
                catch (Exception ex)
                {
                    return "0";
                }
            }
        }
        public string EstadoCaja(int idCaja)
        {
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_Caja_EstadoCaja", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCaja", idCaja);
                    var estadocaja = da.SelectCommand.ExecuteScalar();
                    return estadocaja.ToString();
                }
                catch (Exception ex)
                {
                    return "0";
                }
            }
        }
        public ResultDTO<FN_CajaDTO> ReporteCajaxID(int idCaja)
        {
            ResultDTO<FN_CajaDTO> oResultDTO = new ResultDTO<FN_CajaDTO>();
            oResultDTO.ListaResultado = new List<FN_CajaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_ReporteCaja", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCaja", idCaja);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_CajaDTO oFN_CajaDTO = new FN_CajaDTO();
                        oFN_CajaDTO.FechaApertura = Convert.ToDateTime(dr["FechaApertura"].ToString());
                        oFN_CajaDTO.FechaCierre = Convert.ToDateTime(dr["FechaCierre"].ToString());
                        oFN_CajaDTO.RazonSocial = dr["RazonSocial"].ToString();
                        oFN_CajaDTO.Ruc = dr["Ruc"].ToString();
                        oFN_CajaDTO.Direccion = dr["Direccion"].ToString();
                        oFN_CajaDTO.Usuario = dr["Usuario"].ToString();
                        oFN_CajaDTO.NroCaja = dr["NroCaja"].ToString();
                        oFN_CajaDTO.Descripcion = dr["Descripcion"].ToString();
                        oFN_CajaDTO.MontoInicio = Convert.ToDecimal(dr["MontoInicio"].ToString());
                        oFN_CajaDTO.MontoIngreso = Convert.ToDecimal(dr["MontoIngreso"].ToString());
                        oFN_CajaDTO.TotalIngreso = Convert.ToDecimal(dr["Total Ingreso"].ToString());
                        oFN_CajaDTO.MontoSalida = Convert.ToDecimal(dr["MontoSalida"].ToString());
                        oFN_CajaDTO.MontoTotal = Convert.ToDecimal(dr["Monto Total"].ToString());
                        oResultDTO.ListaResultado.Add(oFN_CajaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_CajaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_CajaDTO> CerrarCaja(FN_CajaDTO oFN_Caja)
        {
            ResultDTO<FN_CajaDTO> oResultDTO = new ResultDTO<FN_CajaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_Caja_CerrarCaja", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idCaja", oFN_Caja.idCaja);                       
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<FN_CajaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_CajaDTO>();
                    }
                }
            }
            return oResultDTO;
        }



    }
}
