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
  public  class Ma_FormaPagoDAO
    {
        public ResultDTO<Ma_FormaPagoDTO> ListarTodo(int idEmpresa, string Activo = "", SqlConnection cn = null)
        {
            ResultDTO<Ma_FormaPagoDTO> oResultDTO = new ResultDTO<Ma_FormaPagoDTO>();
            oResultDTO.ListaResultado = new List<Ma_FormaPagoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_FormaPago_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@Activo", Activo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_FormaPagoDTO oMa_FormaPagoDTO = new Ma_FormaPagoDTO();
                        oMa_FormaPagoDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oMa_FormaPagoDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oMa_FormaPagoDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oMa_FormaPagoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_FormaPagoDTO.Dias = Convert.ToInt32(dr["Dias"] == null ? 0 : Convert.ToInt32(dr["Dias"].ToString()));
                        oMa_FormaPagoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_FormaPagoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_FormaPagoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_FormaPagoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_FormaPagoDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oMa_FormaPagoDTO.UsuarioModificacionDescripcion = dr["UsuarioModificacionDescripcion"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_FormaPagoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_FormaPagoDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Ma_FormaPagoDTO> ListarxID(int idFormaPago)
        {
            ResultDTO<Ma_FormaPagoDTO> oResultDTO = new ResultDTO<Ma_FormaPagoDTO>();
            oResultDTO.ListaResultado = new List<Ma_FormaPagoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_FormaPago_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idFormaPago", idFormaPago);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_FormaPagoDTO oMa_FormaPagoDTO = new Ma_FormaPagoDTO();
                        oMa_FormaPagoDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"].ToString());
                        oMa_FormaPagoDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oMa_FormaPagoDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_FormaPagoDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_FormaPagoDTO.Dias = Convert.ToInt32(dr["Dias"] == null ? 0 : Convert.ToInt32(dr["Dias"].ToString()));
                        oMa_FormaPagoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_FormaPagoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_FormaPagoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_FormaPagoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_FormaPagoDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_FormaPagoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_FormaPagoDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Ma_FormaPagoDTO> UpdateInsert(Ma_FormaPagoDTO oMa_FormaPago)
        {
            ResultDTO<Ma_FormaPagoDTO> oResultDTO = new ResultDTO<Ma_FormaPagoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_FormaPago_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idFormaPago", oMa_FormaPago.idFormaPago);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oMa_FormaPago.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_FormaPago.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Dias", oMa_FormaPago.Dias);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_FormaPago.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_FormaPago.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_FormaPago.Estado);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.Campo1 = id_output.Value.ToString();
                            oResultDTO.ListaResultado = ListarTodo(oMa_FormaPago.idEmpresa, "", cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_FormaPago.idEmpresa, oMa_FormaPago.UsuarioModificacion,
                                "MANTENIMIENTOS-FORMA DE PAGOS", "Ma_FormaPago", (int)id_output.Value, (oMa_FormaPago.idFormaPago == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_FormaPagoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_FormaPagoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_FormaPagoDTO> Delete(Ma_FormaPagoDTO oMa_FormaPago)
        {
            ResultDTO<Ma_FormaPagoDTO> oResultDTO = new ResultDTO<Ma_FormaPagoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_FormaPago_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idFormaPago", oMa_FormaPago.idFormaPago);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oMa_FormaPago.idEmpresa, "", cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_FormaPago.idEmpresa, oMa_FormaPago.UsuarioModificacion,
                                "MANTENIMIENTOS-FORMA DE PAGO", "Ma_FormaPago", oMa_FormaPago.idFormaPago, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_FormaPagoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_FormaPagoDTO>();
                    }
                }
            }
            return oResultDTO;
        }



    }
}
