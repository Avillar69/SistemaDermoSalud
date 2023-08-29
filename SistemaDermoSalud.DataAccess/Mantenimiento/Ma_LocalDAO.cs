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
   public class Ma_LocalDAO
    {
        public ResultDTO<Ma_LocalDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<Ma_LocalDTO> oResultDTO = new ResultDTO<Ma_LocalDTO>();
            oResultDTO.ListaResultado = new List<Ma_LocalDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Local_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_LocalDTO oMa_LocalDTO = new Ma_LocalDTO();
                        oMa_LocalDTO.idLocal = Convert.ToInt32(dr["idLocal"] == null ? 0 : Convert.ToInt32(dr["idLocal"].ToString()));
                        oMa_LocalDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oMa_LocalDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oMa_LocalDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_LocalDTO.Direccion = dr["Direccion"] == null ? "" : dr["Direccion"].ToString();
                        oMa_LocalDTO.Telefono = dr["Telefono"] == null ? "" : dr["Telefono"].ToString();
                        oMa_LocalDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_LocalDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_LocalDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_LocalDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_LocalDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oMa_LocalDTO.UsuarioModificacionDes = dr["UsuarioModificacionDes"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_LocalDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_LocalDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_LocalDTO> ListarxID(int idLocal)
        {
            ResultDTO<Ma_LocalDTO> oResultDTO = new ResultDTO<Ma_LocalDTO>();
            oResultDTO.ListaResultado = new List<Ma_LocalDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Local_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idLocal", idLocal);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_LocalDTO oMa_LocalDTO = new Ma_LocalDTO();
                        oMa_LocalDTO.idLocal = Convert.ToInt32(dr["idLocal"].ToString());
                        oMa_LocalDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oMa_LocalDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_LocalDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_LocalDTO.Direccion = dr["Direccion"].ToString();
                        oMa_LocalDTO.Telefono = dr["Telefono"].ToString();
                        oMa_LocalDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_LocalDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_LocalDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_LocalDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_LocalDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_LocalDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<Ma_AlmacenDTO> listaAlmacen = new List<Ma_AlmacenDTO>();
                            while (dr.Read())
                            {
                                Ma_AlmacenDTO obj = new Ma_AlmacenDTO();
                                obj.idAlmacen = Convert.ToInt32(dr["idAlmacen"].ToString());
                                obj.CodigoGenerado = dr["CodigoGenerado"].ToString();
                                obj.Descripcion = dr["Descripcion"].ToString();
                                listaAlmacen.Add(obj);
                            }
                            oResultDTO.ListaResultado[0].oListaAlmacen = listaAlmacen;
                        }
                    }
                    oResultDTO.Resultado = "OK";

                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_LocalDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_LocalDTO> UpdateInsert(Ma_LocalDTO oMa_Local)
        {
            ResultDTO<Ma_LocalDTO> oResultDTO = new ResultDTO<Ma_LocalDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Local_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idLocal", oMa_Local.idLocal);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oMa_Local.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_Local.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Direccion", oMa_Local.Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono", oMa_Local.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_Local.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_Local.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_Local.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Lista_Almacen", oMa_Local.Lista_Almacen);
                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oMa_Local.idEmpresa, cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Local.idEmpresa, oMa_Local.UsuarioModificacion,
                                "MANTENIMIENTOS-LOCALES", "Ma_Local", (int)id_output.Value, (oMa_Local.idLocal == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_LocalDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_LocalDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_LocalDTO> Delete(Ma_LocalDTO oMa_Local)
        {
            ResultDTO<Ma_LocalDTO> oResultDTO = new ResultDTO<Ma_LocalDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Local_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idLocal", oMa_Local.idLocal);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oMa_Local.idEmpresa, cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Local.idEmpresa, oMa_Local.UsuarioModificacion,
                                "MANTENIMIENTOS-LOCALES", "Ma_Local", (int)oMa_Local.idLocal, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_LocalDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_LocalDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_LocalDTO> ListarxUsuario(int idUsuario, SqlConnection cn = null)
        {
            ResultDTO<Ma_LocalDTO> oResultDTO = new ResultDTO<Ma_LocalDTO>();
            oResultDTO.ListaResultado = new List<Ma_LocalDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da2 = new SqlDataAdapter("SP_Ma_Local_ListarxUsuario", cn);
                    da2.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da2.SelectCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
                    SqlDataReader dr2 = da2.SelectCommand.ExecuteReader();
                    while (dr2.Read())
                    {
                        Ma_LocalDTO oMa_LocalDTO = new Ma_LocalDTO();
                        oMa_LocalDTO.idLocal = Convert.ToInt32(dr2["idLocal"] == null ? 0 : Convert.ToInt32(dr2["idLocal"].ToString()));
                        oMa_LocalDTO.idEmpresa = Convert.ToInt32(dr2["idEmpresa"] == null ? 0 : Convert.ToInt32(dr2["idEmpresa"].ToString()));
                        oMa_LocalDTO.CodigoGenerado = dr2["CodigoGenerado"] == null ? "" : dr2["CodigoGenerado"].ToString();
                        oMa_LocalDTO.Descripcion = dr2["Descripcion"] == null ? "" : dr2["Descripcion"].ToString();
                        oMa_LocalDTO.Direccion = dr2["Direccion"] == null ? "" : dr2["Direccion"].ToString();
                        oMa_LocalDTO.Telefono = dr2["Telefono"] == null ? "" : dr2["Telefono"].ToString();
                        oMa_LocalDTO.FechaCreacion = Convert.ToDateTime(dr2["FechaCreacion"].ToString());
                        oMa_LocalDTO.FechaModificacion = Convert.ToDateTime(dr2["FechaModificacion"].ToString());
                        oMa_LocalDTO.UsuarioCreacion = Convert.ToInt32(dr2["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr2["UsuarioCreacion"].ToString()));
                        oMa_LocalDTO.UsuarioModificacion = Convert.ToInt32(dr2["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr2["UsuarioModificacion"].ToString()));
                        oMa_LocalDTO.Estado = Convert.ToBoolean(dr2["Estado"] == null ? false : Convert.ToBoolean(dr2["Estado"].ToString()));
                        oMa_LocalDTO.UsuarioModificacionDes = dr2["UsuarioModificacionDes"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_LocalDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_LocalDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
