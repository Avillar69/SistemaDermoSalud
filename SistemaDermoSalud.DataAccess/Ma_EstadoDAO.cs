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
   public class Ma_EstadoDAO
    {
        public ResultDTO<Ma_EstadoDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<Ma_EstadoDTO> oResultDTO = new ResultDTO<Ma_EstadoDTO>();
            oResultDTO.ListaResultado = new List<Ma_EstadoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Estado_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_EstadoDTO oMa_EstadoDTO = new Ma_EstadoDTO();
                        oMa_EstadoDTO.idEstado = Convert.ToInt32(dr["idEstado"] == null ? 0 : Convert.ToInt32(dr["idEstado"].ToString()));
                        oMa_EstadoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_EstadoDTO.Tipo = dr["Tipo"] == null ? "" : dr["Tipo"].ToString();
                        oMa_EstadoDTO.Modulo = dr["Modulo"] == null ? "" : dr["Modulo"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_EstadoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_EstadoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_EstadoDTO> ListarxModulo(string Modulo)
        {
            ResultDTO<Ma_EstadoDTO> oResultDTO = new ResultDTO<Ma_EstadoDTO>();
            oResultDTO.ListaResultado = new List<Ma_EstadoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Estado_ListarxModulo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Modulo", Modulo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_EstadoDTO oMa_EstadoDTO = new Ma_EstadoDTO();
                        oMa_EstadoDTO.idEstado = Convert.ToInt32(dr["idEstado"].ToString());
                        oMa_EstadoDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_EstadoDTO.Tipo = dr["Tipo"].ToString();
                        oMa_EstadoDTO.Modulo = dr["Modulo"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_EstadoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_EstadoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_EstadoDTO> ListarxID(int idEstado)
        {
            ResultDTO<Ma_EstadoDTO> oResultDTO = new ResultDTO<Ma_EstadoDTO>();
            oResultDTO.ListaResultado = new List<Ma_EstadoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Estado_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEstado", idEstado);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_EstadoDTO oMa_EstadoDTO = new Ma_EstadoDTO();
                        oMa_EstadoDTO.idEstado = Convert.ToInt32(dr["idEstado"].ToString());
                        oMa_EstadoDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_EstadoDTO.Tipo = dr["Tipo"].ToString();
                        oMa_EstadoDTO.Modulo = dr["Modulo"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_EstadoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_EstadoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_EstadoDTO> UpdateInsert(Ma_EstadoDTO oMa_Estado)
        {
            ResultDTO<Ma_EstadoDTO> oResultDTO = new ResultDTO<Ma_EstadoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Estado_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idEstado", oMa_Estado.idEstado);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_Estado.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Tipo", oMa_Estado.Tipo);
                        da.SelectCommand.Parameters.AddWithValue("@Modulo", oMa_Estado.Modulo);
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
                            oResultDTO.ListaResultado = new List<Ma_EstadoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_EstadoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_EstadoDTO> Delete(Ma_EstadoDTO oMa_Estado)
        {
            ResultDTO<Ma_EstadoDTO> oResultDTO = new ResultDTO<Ma_EstadoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Estado_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idEstado", oMa_Estado.idEstado);
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
                            oResultDTO.ListaResultado = new List<Ma_EstadoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_EstadoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
