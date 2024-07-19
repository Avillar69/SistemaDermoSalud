using System;
using System.Collections.Generic;
using System.Transactions;
using SistemaDermoSalud.Entities;
using System.Data;
using System.Data.SqlClient;
using SistemaDermoSalud.Entities.Mantenimiento;

namespace SistemaDermoSalud.DataAccess.Mantenimiento
{
    public class Ma_MarcaDAO
    {
        public ResultDTO<Ma_MarcaDTO> ListarTodo(int p, SqlConnection cn = null)
        {
            ResultDTO<Ma_MarcaDTO> oResultDTO = new ResultDTO<Ma_MarcaDTO>();
            oResultDTO.ListaResultado = new List<Ma_MarcaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Marca_Listar", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@param", p);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_MarcaDTO oMarcaDTO = new Ma_MarcaDTO();
                        oMarcaDTO.idMarca = Convert.ToInt32(dr["idMarca"] == null ? 0 : Convert.ToInt32(dr["idMarca"].ToString()));
                        oMarcaDTO.Marca = dr["Marca"] == null ? "" : dr["Marca"].ToString();
                        oMarcaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMarcaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMarcaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMarcaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMarcaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMarcaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_MarcaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_MarcaDTO> ListarxID(int idMarca)
        {
            ResultDTO<Ma_MarcaDTO> oResultDTO = new ResultDTO<Ma_MarcaDTO>();
            oResultDTO.ListaResultado = new List<Ma_MarcaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_MarcaXID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idMarca", idMarca);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_MarcaDTO oMarcaDTO = new Ma_MarcaDTO();
                        oMarcaDTO.idMarca = Convert.ToInt32(dr["idMarca"] == null ? 0 : Convert.ToInt32(dr["idMarca"].ToString()));
                        oMarcaDTO.Marca = dr["Marca"] == null ? "" : dr["Marca"].ToString();
                        oMarcaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMarcaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMarcaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMarcaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMarcaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMarcaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_MarcaDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Ma_MarcaDTO> UpdateInsert(Ma_MarcaDTO oMarcaDTO)
        {
            ResultDTO<Ma_MarcaDTO> oResultDTO = new ResultDTO<Ma_MarcaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Marca_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMarca", oMarcaDTO.idMarca);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMarcaDTO.Marca);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMarcaDTO.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMarcaDTO.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("Estado", oMarcaDTO.Estado);
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
                            oResultDTO.ListaResultado = new List<Ma_MarcaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_MarcaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_MarcaDTO> Delete(Ma_MarcaDTO oMarca)
        {
            ResultDTO<Ma_MarcaDTO> oResultDTO = new ResultDTO<Ma_MarcaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Marca_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMarca", oMarca.idMarca);
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
                            oResultDTO.ListaResultado = new List<Ma_MarcaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_MarcaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
