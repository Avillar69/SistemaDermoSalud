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
   public class Ma_BancoDAO
    {
        public ResultDTO<Ma_BancoDTO> ListarTodo(int idEmpresa, string Activo = "", SqlConnection cn = null)
        {
            ResultDTO<Ma_BancoDTO> oResultDTO = new ResultDTO<Ma_BancoDTO>();
            oResultDTO.ListaResultado = new List<Ma_BancoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Banco_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@Activo", Activo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_BancoDTO oMa_BancoDTO = new Ma_BancoDTO();
                        oMa_BancoDTO.idBanco = Convert.ToInt32(dr["idBanco"] == null ? 0 : Convert.ToInt32(dr["idBanco"].ToString()));
                        oMa_BancoDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oMa_BancoDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oMa_BancoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_BancoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_BancoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_BancoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_BancoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_BancoDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oMa_BancoDTO.UsuarioModificacionDescripcion = dr["UsuarioModificacionDescripcion"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_BancoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_BancoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_BancoDTO> ListarxID(int idBanco)
        {
            ResultDTO<Ma_BancoDTO> oResultDTO = new ResultDTO<Ma_BancoDTO>();
            oResultDTO.ListaResultado = new List<Ma_BancoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Banco_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idBanco", idBanco);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_BancoDTO oMa_BancoDTO = new Ma_BancoDTO();
                        oMa_BancoDTO.idBanco = Convert.ToInt32(dr["idBanco"].ToString());
                        oMa_BancoDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oMa_BancoDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_BancoDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_BancoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_BancoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_BancoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_BancoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_BancoDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_BancoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_BancoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_BancoDTO> UpdateInsert(Ma_BancoDTO oMa_Banco)
        {
            ResultDTO<Ma_BancoDTO> oResultDTO = new ResultDTO<Ma_BancoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Banco_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idBanco", oMa_Banco.idBanco);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oMa_Banco.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_Banco.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_Banco.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_Banco.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_Banco.Estado);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.Campo1 = id_output.Value.ToString();
                            oResultDTO.ListaResultado = ListarTodo(oMa_Banco.idEmpresa, "", cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Banco.idEmpresa, oMa_Banco.UsuarioModificacion,
                                "MANTENIMIENTOS-BANCOS", "Ma_Banco", (int)id_output.Value, (oMa_Banco.idBanco == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.Campo1 = "";
                            oResultDTO.ListaResultado = new List<Ma_BancoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_BancoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_BancoDTO> Delete(Ma_BancoDTO oMa_Banco)
        {
            ResultDTO<Ma_BancoDTO> oResultDTO = new ResultDTO<Ma_BancoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Banco_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idBanco", oMa_Banco.idBanco);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oMa_Banco.idEmpresa, "", cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Banco.idEmpresa, oMa_Banco.UsuarioModificacion,
                                "MANTENIMIENTOS-BANCOS", "Ma_Banco", oMa_Banco.idBanco, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_BancoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_BancoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_BancoDTO> ListaBancos(SqlConnection cn = null)
        {
            ResultDTO<Ma_BancoDTO> oResultDTO = new ResultDTO<Ma_BancoDTO>();
            oResultDTO.ListaResultado = new List<Ma_BancoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_MA_BancoLista", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_BancoDTO oMa_BancoDTO = new Ma_BancoDTO();
                        oMa_BancoDTO.idBanco = Convert.ToInt32(dr["idBanco"].ToString());
                        oMa_BancoDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oMa_BancoDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_BancoDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_BancoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_BancoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_BancoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_BancoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_BancoDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_BancoDTO);

                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_BancoDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
