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
    public class TipoServicioDAO
    {
        public ResultDTO<TipoServicioDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<TipoServicioDTO> oResultDTO = new ResultDTO<TipoServicioDTO>();
            oResultDTO.ListaResultado = new List<TipoServicioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_TipoServicio_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        TipoServicioDTO oTipoServicioDTO = new TipoServicioDTO();
                        oTipoServicioDTO.idTipoServicio = Convert.ToInt32(dr["idTipoServicio"] == null ? 0 : Convert.ToInt32(dr["idTipoServicio"].ToString()));
                        oTipoServicioDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oTipoServicioDTO.NombreTipoServicio = dr["NombreTipoServicio"] == null ? "" : dr["NombreTipoServicio"].ToString();
                        oTipoServicioDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oTipoServicioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oTipoServicioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oTipoServicioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oTipoServicioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oTipoServicioDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oTipoServicioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<TipoServicioDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<TipoServicioDTO> ListarxID(int idTipoServicio)
        {
            ResultDTO<TipoServicioDTO> oResultDTO = new ResultDTO<TipoServicioDTO>();
            oResultDTO.ListaResultado = new List<TipoServicioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_TipoServicio_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idTipoServicio", idTipoServicio);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        TipoServicioDTO oTipoServicioDTO = new TipoServicioDTO();
                        oTipoServicioDTO.idTipoServicio = Convert.ToInt32(dr["idTipoServicio"] == null ? 0 : Convert.ToInt32(dr["idTipoServicio"].ToString()));
                        oTipoServicioDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oTipoServicioDTO.NombreTipoServicio = dr["NombreTipoServicio"] == null ? "" : dr["NombreTipoServicio"].ToString();
                        oTipoServicioDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oTipoServicioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oTipoServicioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oTipoServicioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oTipoServicioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oTipoServicioDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oTipoServicioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<TipoServicioDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<TipoServicioDTO> UpdateInsert(TipoServicioDTO oTipoServicioDTO)
        {
            ResultDTO<TipoServicioDTO> oResultDTO = new ResultDTO<TipoServicioDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_LM_TipoServicio_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idTipoServicio", oTipoServicioDTO.idTipoServicio);
                        da.SelectCommand.Parameters.AddWithValue("@NombreTipoServicio", oTipoServicioDTO.NombreTipoServicio);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oTipoServicioDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oTipoServicioDTO.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oTipoServicioDTO.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oTipoServicioDTO.Estado);
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
                            oResultDTO.ListaResultado = new List<TipoServicioDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<TipoServicioDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<TipoServicioDTO> Delete(TipoServicioDTO oTipoServicioDTO)
        {
            ResultDTO<TipoServicioDTO> oResultDTO = new ResultDTO<TipoServicioDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_LM_TipoServicio_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idTipoServicio", oTipoServicioDTO.idTipoServicio);
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
                            oResultDTO.ListaResultado = new List<TipoServicioDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<TipoServicioDTO>();
                    }
                }
            }
            return oResultDTO;
        }

        public ResultDTO<TipoServicioDTO> ListarPorTipoServicio(SqlConnection cn = null)
        {
            ResultDTO<TipoServicioDTO> oResultDTO = new ResultDTO<TipoServicioDTO>();
            oResultDTO.ListaResultado = new List<TipoServicioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("[SP_LM_TipoServicio_Listar]", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        TipoServicioDTO oTipoServicioDTO = new TipoServicioDTO();
                        oTipoServicioDTO.idTipoServicio = Convert.ToInt32(dr["idTipoServicio"] == null ? 0 : Convert.ToInt32(dr["idTipoServicio"].ToString()));
                        oTipoServicioDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oTipoServicioDTO.NombreTipoServicio = dr["NombreTipoServicio"] == null ? "" : dr["NombreTipoServicio"].ToString();
                        oTipoServicioDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oTipoServicioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oTipoServicioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oTipoServicioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oTipoServicioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oTipoServicioDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oTipoServicioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<TipoServicioDTO>();
                }
            }
            return oResultDTO;
        }



    }
}
