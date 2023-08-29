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
  public  class Ma_PaisDAO
    {
        public ResultDTO<Ma_PaisDTO> ListarTodo()
        {
            ResultDTO<Ma_PaisDTO> oResultDTO = new ResultDTO<Ma_PaisDTO>();
            oResultDTO.ListaResultado = new List<Ma_PaisDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Pais_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_PaisDTO oMa_PaisDTO = new Ma_PaisDTO();
                        oMa_PaisDTO.idPais = Convert.ToInt32(dr["idPais"] == null ? 0 : Convert.ToInt32(dr["idPais"].ToString()));
                        oMa_PaisDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oMa_PaisDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_PaisDTO.Abreviatura = dr["Abreviatura"] == null ? "" : dr["Abreviatura"].ToString();
                        oMa_PaisDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_PaisDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_PaisDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_PaisDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_PaisDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMa_PaisDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_PaisDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_PaisDTO> ListarxID(int idPais)
        {
            ResultDTO<Ma_PaisDTO> oResultDTO = new ResultDTO<Ma_PaisDTO>();
            oResultDTO.ListaResultado = new List<Ma_PaisDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Pais_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idPais", idPais);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_PaisDTO oMa_PaisDTO = new Ma_PaisDTO();
                        oMa_PaisDTO.idPais = Convert.ToInt32(dr["idPais"].ToString());
                        oMa_PaisDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_PaisDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_PaisDTO.Abreviatura = dr["Abreviatura"].ToString();
                        oMa_PaisDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_PaisDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_PaisDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_PaisDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_PaisDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_PaisDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_PaisDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_PaisDTO> UpdateInsert(Ma_PaisDTO oMa_Pais)
        {
            ResultDTO<Ma_PaisDTO> oResultDTO = new ResultDTO<Ma_PaisDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Pais_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idPais", oMa_Pais.idPais);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoGenerado", oMa_Pais.CodigoGenerado);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_Pais.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Abreviatura", oMa_Pais.Abreviatura);
                        da.SelectCommand.Parameters.AddWithValue("@FechaCreacion", oMa_Pais.FechaCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaModificacion", oMa_Pais.FechaModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_Pais.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_Pais.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_Pais.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            transactionScope.Complete();
                            //oResultDTO.ListaResultado = ListarTodo(oMa_Pais).ListaResultado;
                            oResultDTO.ListaResultado = ListarTodo().ListaResultado;
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_PaisDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_PaisDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_PaisDTO> Delete(Ma_PaisDTO oMa_Pais)
        {
            ResultDTO<Ma_PaisDTO> oResultDTO = new ResultDTO<Ma_PaisDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Pais_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idPais", oMa_Pais.idPais);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            transactionScope.Complete();
                            //oResultDTO.ListaResultado = ListarTodo(oMa_Pais).ListaResultado;
                            oResultDTO.ListaResultado = ListarTodo().ListaResultado;
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_PaisDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_PaisDTO>();
                    }
                }
            }
            return oResultDTO;
        }


    }
}
