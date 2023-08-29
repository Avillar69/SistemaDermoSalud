using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SistemaDermoSalud.DataAccess.Mantenimiento
{
   public class Ma_UnidadMedidaDAO
    {

        public ResultDTO<Ma_UnidadMedidaDTO> ListarTodo(int idEmpresa, string Activo = "", SqlConnection cn = null)
        {
            ResultDTO<Ma_UnidadMedidaDTO> oResultDTO = new ResultDTO<Ma_UnidadMedidaDTO>();
            oResultDTO.ListaResultado = new List<Ma_UnidadMedidaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_UnidadMedida_ListarTodo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@Activo", Activo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_UnidadMedidaDTO oMa_UnidadMedidaDTO = new Ma_UnidadMedidaDTO();
                        oMa_UnidadMedidaDTO.idUnidadMedida = Convert.ToInt32(dr["idUnidadMedida"] == null ? 0 : Convert.ToInt32(dr["idUnidadMedida"].ToString()));
                        oMa_UnidadMedidaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oMa_UnidadMedidaDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oMa_UnidadMedidaDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_UnidadMedidaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_UnidadMedidaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_UnidadMedidaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_UnidadMedidaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_UnidadMedidaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oMa_UnidadMedidaDTO.DesUsuarioModificacion = dr["DesUsuarioModificacion"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_UnidadMedidaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_UnidadMedidaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_UnidadMedidaDTO> ListarxID(int idUnidadMedida)
        {
            ResultDTO<Ma_UnidadMedidaDTO> oResultDTO = new ResultDTO<Ma_UnidadMedidaDTO>();
            oResultDTO.ListaResultado = new List<Ma_UnidadMedidaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_UnidadMedida_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idUnidadMedida", idUnidadMedida);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_UnidadMedidaDTO oMa_UnidadMedidaDTO = new Ma_UnidadMedidaDTO();
                        oMa_UnidadMedidaDTO.idUnidadMedida = Convert.ToInt32(dr["idUnidadMedida"].ToString());
                        oMa_UnidadMedidaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oMa_UnidadMedidaDTO.Codigo = dr["Codigo"].ToString();
                        oMa_UnidadMedidaDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_UnidadMedidaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_UnidadMedidaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_UnidadMedidaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_UnidadMedidaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_UnidadMedidaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_UnidadMedidaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_UnidadMedidaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_UnidadMedidaDTO> UpdateInsert(Ma_UnidadMedidaDTO oMa_UnidadMedida)
        {
            ResultDTO<Ma_UnidadMedidaDTO> oResultDTO = new ResultDTO<Ma_UnidadMedidaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_UnidadMedida_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idUnidadMedida", oMa_UnidadMedida.idUnidadMedida);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oMa_UnidadMedida.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oMa_UnidadMedida.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_UnidadMedida.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_UnidadMedida.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_UnidadMedida.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_UnidadMedida.Estado);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oMa_UnidadMedida.idEmpresa, "", cn).ListaResultado;
                            oResultDTO.Campo1 = id_output.Value.ToString();
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_UnidadMedida.idEmpresa, oMa_UnidadMedida.UsuarioModificacion,
                                "MANTENIMIENTOS-UNIDAD_MEDIDA", "Ma_UnidadMedida", (int)id_output.Value, (oMa_UnidadMedida.idUnidadMedida == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_UnidadMedidaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_UnidadMedidaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_UnidadMedidaDTO> Delete(Ma_UnidadMedidaDTO oMa_UnidadMedida)
        {
            ResultDTO<Ma_UnidadMedidaDTO> oResultDTO = new ResultDTO<Ma_UnidadMedidaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_UnidadMedida_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idUnidadMedida", oMa_UnidadMedida.idUnidadMedida);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oMa_UnidadMedida.idEmpresa, "", cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_UnidadMedida.idEmpresa, oMa_UnidadMedida.UsuarioModificacion,
                                "MANTENIMIENTOS-UNIDAD_MEDIDA", "Ma_UnidadMedida", oMa_UnidadMedida.idUnidadMedida, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_UnidadMedidaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_UnidadMedidaDTO>();
                    }
                }
            }
            return oResultDTO;
        }

    }
}
