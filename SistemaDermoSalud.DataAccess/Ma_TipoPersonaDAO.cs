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
  public class Ma_TipoPersonaDAO
    {
        public ResultDTO<Ma_TipoPersonaDTO> ListarTodo()
        {
            ResultDTO<Ma_TipoPersonaDTO> oResultDTO = new ResultDTO<Ma_TipoPersonaDTO>();
            oResultDTO.ListaResultado = new List<Ma_TipoPersonaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoPersona_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_TipoPersonaDTO oMa_TipoPersonaDTO = new Ma_TipoPersonaDTO();
                        oMa_TipoPersonaDTO.idTipoPersona = Convert.ToInt32(dr["idTipoPersona"] == null ? 0 : Convert.ToInt32(dr["idTipoPersona"].ToString()));
                        oMa_TipoPersonaDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oMa_TipoPersonaDTO.CodigoSunat = dr["CodigoSunat"] == null ? "" : dr["CodigoSunat"].ToString();
                        oMa_TipoPersonaDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_TipoPersonaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_TipoPersonaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_TipoPersonaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_TipoPersonaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_TipoPersonaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMa_TipoPersonaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_TipoPersonaDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Ma_TipoPersonaDTO> ListarxID(int idTipoPersona)
        {
            ResultDTO<Ma_TipoPersonaDTO> oResultDTO = new ResultDTO<Ma_TipoPersonaDTO>();
            oResultDTO.ListaResultado = new List<Ma_TipoPersonaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoPersona_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idTipoPersona", idTipoPersona);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_TipoPersonaDTO oMa_TipoPersonaDTO = new Ma_TipoPersonaDTO();
                        oMa_TipoPersonaDTO.idTipoPersona = Convert.ToInt32(dr["idTipoPersona"].ToString());
                        oMa_TipoPersonaDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_TipoPersonaDTO.CodigoSunat = dr["CodigoSunat"].ToString();
                        oMa_TipoPersonaDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_TipoPersonaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_TipoPersonaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_TipoPersonaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_TipoPersonaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_TipoPersonaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_TipoPersonaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_TipoPersonaDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Ma_TipoPersonaDTO> UpdateInsert(Ma_TipoPersonaDTO oMa_TipoPersona)
        {
            ResultDTO<Ma_TipoPersonaDTO> oResultDTO = new ResultDTO<Ma_TipoPersonaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoPersona_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idTipoPersona", oMa_TipoPersona.idTipoPersona);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoGenerado", oMa_TipoPersona.CodigoGenerado);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoSunat", oMa_TipoPersona.CodigoSunat);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_TipoPersona.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaCreacion", oMa_TipoPersona.FechaCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaModificacion", oMa_TipoPersona.FechaModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_TipoPersona.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_TipoPersona.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_TipoPersona.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            transactionScope.Complete();
                            oResultDTO.ListaResultado = ListarTodo().ListaResultado;
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_TipoPersonaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_TipoPersonaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_TipoPersonaDTO> Delete(Ma_TipoPersonaDTO oMa_TipoPersona)
        {
            ResultDTO<Ma_TipoPersonaDTO> oResultDTO = new ResultDTO<Ma_TipoPersonaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoPersona_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idTipoPersona", oMa_TipoPersona.idTipoPersona);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            transactionScope.Complete();
                            oResultDTO.ListaResultado = ListarTodo().ListaResultado;
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_TipoPersonaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_TipoPersonaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
