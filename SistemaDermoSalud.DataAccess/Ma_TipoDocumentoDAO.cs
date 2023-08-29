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
    public class Ma_TipoDocumentoDAO
    {
        public ResultDTO<Ma_TipoDocumentoDTO> ListarTodo()
        {
            ResultDTO<Ma_TipoDocumentoDTO> oResultDTO = new ResultDTO<Ma_TipoDocumentoDTO>();
            oResultDTO.ListaResultado = new List<Ma_TipoDocumentoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoDocumento_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_TipoDocumentoDTO oMa_TipoDocumentoDTO = new Ma_TipoDocumentoDTO();
                        oMa_TipoDocumentoDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"] == null ? 0 : Convert.ToInt32(dr["idTipoDocumento"].ToString()));
                        oMa_TipoDocumentoDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oMa_TipoDocumentoDTO.CodigoSunat = dr["CodigoSunat"] == null ? "" : dr["CodigoSunat"].ToString();
                        oMa_TipoDocumentoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_TipoDocumentoDTO.Abreviatura = dr["Abreviatura"] == null ? "" : dr["Abreviatura"].ToString();
                        oMa_TipoDocumentoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_TipoDocumentoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_TipoDocumentoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_TipoDocumentoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_TipoDocumentoDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMa_TipoDocumentoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_TipoDocumentoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_TipoDocumentoDTO> ListarxID(int idTipoDocumento)
        {
            ResultDTO<Ma_TipoDocumentoDTO> oResultDTO = new ResultDTO<Ma_TipoDocumentoDTO>();
            oResultDTO.ListaResultado = new List<Ma_TipoDocumentoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoDocumento_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idTipoDocumento", idTipoDocumento);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_TipoDocumentoDTO oMa_TipoDocumentoDTO = new Ma_TipoDocumentoDTO();
                        oMa_TipoDocumentoDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"].ToString());
                        oMa_TipoDocumentoDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_TipoDocumentoDTO.CodigoSunat = dr["CodigoSunat"].ToString();
                        oMa_TipoDocumentoDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_TipoDocumentoDTO.Abreviatura = dr["Abreviatura"].ToString();
                        oMa_TipoDocumentoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_TipoDocumentoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_TipoDocumentoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_TipoDocumentoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_TipoDocumentoDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_TipoDocumentoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_TipoDocumentoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_TipoDocumentoDTO> UpdateInsert(Ma_TipoDocumentoDTO oMa_TipoDocumento)
        {
            ResultDTO<Ma_TipoDocumentoDTO> oResultDTO = new ResultDTO<Ma_TipoDocumentoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoDocumento_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idTipoDocumento", oMa_TipoDocumento.idTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoGenerado", oMa_TipoDocumento.CodigoGenerado);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoSunat", oMa_TipoDocumento.CodigoSunat);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_TipoDocumento.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Abreviatura", oMa_TipoDocumento.Abreviatura);
                        da.SelectCommand.Parameters.AddWithValue("@FechaCreacion", oMa_TipoDocumento.FechaCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaModificacion", oMa_TipoDocumento.FechaModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_TipoDocumento.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_TipoDocumento.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_TipoDocumento.Estado);
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
                            oResultDTO.ListaResultado = new List<Ma_TipoDocumentoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_TipoDocumentoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_TipoDocumentoDTO> Delete(Ma_TipoDocumentoDTO oMa_TipoDocumento)
        {
            ResultDTO<Ma_TipoDocumentoDTO> oResultDTO = new ResultDTO<Ma_TipoDocumentoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoDocumento_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idTipoDocumento", oMa_TipoDocumento.idTipoDocumento);
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
                            oResultDTO.ListaResultado = new List<Ma_TipoDocumentoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_TipoDocumentoDTO>();
                    }
                }
            }
            return oResultDTO;
        }

    }
}
