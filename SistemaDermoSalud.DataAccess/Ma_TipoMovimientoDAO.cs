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
   public class Ma_TipoMovimientoDAO
    {
        public ResultDTO<Ma_TipoMovimientoDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<Ma_TipoMovimientoDTO> oResultDTO = new ResultDTO<Ma_TipoMovimientoDTO>();
            oResultDTO.ListaResultado = new List<Ma_TipoMovimientoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoMovimiento_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_TipoMovimientoDTO oMa_TipoMovimientoDTO = new Ma_TipoMovimientoDTO();
                        oMa_TipoMovimientoDTO.idTipoMovimiento = Convert.ToInt32(dr["idTipoMovimiento"] == null ? 0 : Convert.ToInt32(dr["idTipoMovimiento"].ToString()));
                        oMa_TipoMovimientoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_TipoMovimientoDTO.Tipo = dr["Tipo"] == null ? "" : dr["Tipo"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_TipoMovimientoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_TipoMovimientoDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Ma_TipoMovimientoDTO> ListarxTipo(string tipo, SqlConnection cn = null)
        {
            ResultDTO<Ma_TipoMovimientoDTO> oResultDTO = new ResultDTO<Ma_TipoMovimientoDTO>();
            oResultDTO.ListaResultado = new List<Ma_TipoMovimientoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoMovimiento_ListarxTipo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Tipo", tipo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_TipoMovimientoDTO oMa_TipoMovimientoDTO = new Ma_TipoMovimientoDTO();
                        oMa_TipoMovimientoDTO.idTipoMovimiento = Convert.ToInt32(dr["idTipoMovimiento"] == null ? 0 : Convert.ToInt32(dr["idTipoMovimiento"].ToString()));
                        oMa_TipoMovimientoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_TipoMovimientoDTO.Tipo = dr["Tipo"] == null ? "" : dr["Tipo"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_TipoMovimientoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_TipoMovimientoDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Ma_TipoMovimientoDTO> ListarxID(int idTipoMovimiento)
        {
            ResultDTO<Ma_TipoMovimientoDTO> oResultDTO = new ResultDTO<Ma_TipoMovimientoDTO>();
            oResultDTO.ListaResultado = new List<Ma_TipoMovimientoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoMovimiento_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idTipoMovimiento", idTipoMovimiento);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_TipoMovimientoDTO oMa_TipoMovimientoDTO = new Ma_TipoMovimientoDTO();
                        oMa_TipoMovimientoDTO.idTipoMovimiento = Convert.ToInt32(dr["idTipoMovimiento"].ToString());
                        oMa_TipoMovimientoDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_TipoMovimientoDTO.Tipo = dr["Tipo"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_TipoMovimientoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_TipoMovimientoDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Ma_TipoMovimientoDTO> UpdateInsert(Ma_TipoMovimientoDTO oMa_TipoMovimiento)
        {
            ResultDTO<Ma_TipoMovimientoDTO> oResultDTO = new ResultDTO<Ma_TipoMovimientoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoMovimiento_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idTipoMovimiento", oMa_TipoMovimiento.idTipoMovimiento);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_TipoMovimiento.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Tipo", oMa_TipoMovimiento.Tipo);
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
                            oResultDTO.ListaResultado = new List<Ma_TipoMovimientoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_TipoMovimientoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public Entities.ResultDTO<Ma_TipoMovimientoDTO> Delete(Ma_TipoMovimientoDTO oMa_TipoMovimiento)
        {
            ResultDTO<Ma_TipoMovimientoDTO> oResultDTO = new ResultDTO<Ma_TipoMovimientoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoMovimiento_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idTipoMovimiento", oMa_TipoMovimiento.idTipoMovimiento);
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
                            oResultDTO.ListaResultado = new List<Ma_TipoMovimientoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_TipoMovimientoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
