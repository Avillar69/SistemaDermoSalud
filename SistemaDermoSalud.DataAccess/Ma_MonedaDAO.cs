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
    public class Ma_MonedaDAO
    {
        public ResultDTO<Ma_MonedaDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<Ma_MonedaDTO> oResultDTO = new ResultDTO<Ma_MonedaDTO>();
            oResultDTO.ListaResultado = new List<Ma_MonedaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Moneda_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
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
                        //oMa_MonedaDTO.UsuarioModificacionDes = dr["UsuarioModificacionDes"].ToString();
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
        public ResultDTO<Ma_MonedaDTO> ListarxID(int idMoneda)
        {
            ResultDTO<Ma_MonedaDTO> oResultDTO = new ResultDTO<Ma_MonedaDTO>();
            oResultDTO.ListaResultado = new List<Ma_MonedaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Moneda_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idMoneda", idMoneda);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_MonedaDTO oMa_MonedaDTO = new Ma_MonedaDTO();
                        oMa_MonedaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oMa_MonedaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oMa_MonedaDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_MonedaDTO.CodigoSunat = dr["CodigoSunat"].ToString();
                        oMa_MonedaDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_MonedaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_MonedaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_MonedaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_MonedaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_MonedaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
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
        public ResultDTO<Ma_MonedaDTO> UpdateInsert(Ma_MonedaDTO oMa_Moneda)
        {
            ResultDTO<Ma_MonedaDTO> oResultDTO = new ResultDTO<Ma_MonedaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_MA_Moneda_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMoneda", oMa_Moneda.idMoneda);
                        //da.SelectCommand.Parameters.AddWithValue("@idEmpresa", 1); //oMa_Moneda.idEmpresa == 0 ? 1 : oMa_Moneda.idEmpresa
                        //da.SelectCommand.Parameters.AddWithValue("@CodigoSunat", oMa_Moneda.CodigoSunat == null ? "" : oMa_Moneda.CodigoSunat);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_Moneda.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_Moneda.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_Moneda.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_Moneda.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(1, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_MonedaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_MonedaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_MonedaDTO> Delete(Ma_MonedaDTO oMa_Moneda)
        {
            ResultDTO<Ma_MonedaDTO> oResultDTO = new ResultDTO<Ma_MonedaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_MA_Moneda_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMoneda", oMa_Moneda.idMoneda);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(1, cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Moneda.idEmpresa, oMa_Moneda.UsuarioModificacion,
                                "MANTENIMIENTOS-MONEDA", "Ma_Moneda", oMa_Moneda.idMoneda, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_MonedaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_MonedaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
