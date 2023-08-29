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
   public  class Ma_MarcaDAO
    {
        public ResultDTO<Ma_MarcaDTO> ListarTodo(int idEmpresa, string Activo = "", SqlConnection cn = null)
        {
            ResultDTO<Ma_MarcaDTO> oResultDTO = new ResultDTO<Ma_MarcaDTO>();
            oResultDTO.ListaResultado = new List<Ma_MarcaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Marca_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@Activo", Activo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_MarcaDTO oMa_MarcaDTO = new Ma_MarcaDTO();
                        oMa_MarcaDTO.idMarca = Convert.ToInt32(dr["idMarca"] == null ? 0 : Convert.ToInt32(dr["idMarca"].ToString()));
                        oMa_MarcaDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_MarcaDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_MarcaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_MarcaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_MarcaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_MarcaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_MarcaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oMa_MarcaDTO.UsuarioModificacionDescripcion = dr["UsuarioModificacionDescripcion"] == null ? "" : dr["UsuarioModificacionDescripcion"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_MarcaDTO);
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
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Marca_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idMarca", idMarca);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_MarcaDTO oMa_MarcaDTO = new Ma_MarcaDTO();
                        oMa_MarcaDTO.idMarca = Convert.ToInt32(dr["idMarca"].ToString());
                        oMa_MarcaDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_MarcaDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_MarcaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_MarcaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_MarcaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_MarcaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_MarcaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_MarcaDTO);
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
        public ResultDTO<Ma_MarcaDTO> UpdateInsert(Ma_MarcaDTO oMa_Marca)
        {
            ResultDTO<Ma_MarcaDTO> Respuesta = new ResultDTO<Ma_MarcaDTO>();
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
                        da.SelectCommand.Parameters.AddWithValue("@idMarca", oMa_Marca.idMarca);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oMa_Marca.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_Marca.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_Marca.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_Marca.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_Marca.Estado);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            Respuesta.Resultado = "OK";
                            Respuesta.ListaResultado = (ListarTodo(oMa_Marca.idEmpresa, "", cn).ListaResultado);
                            Respuesta.Campo1 = id_output.Value.ToString();
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Marca.idEmpresa, oMa_Marca.UsuarioModificacion,
                                "MANTENIMIENTOS-MARCAS", "Ma_Marca", (int)id_output.Value, (oMa_Marca.idMarca == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            Respuesta.Resultado = "ERROR";
                            Respuesta.ListaResultado = new List<Ma_MarcaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        Respuesta.Resultado = "ERROR";
                        Respuesta.MensajeError = ex.Message;
                        Respuesta.ListaResultado = new List<Ma_MarcaDTO>();
                    }
                }
            }
            return Respuesta;
        }
        public ResultDTO<Ma_MarcaDTO> Delete(Ma_MarcaDTO oMa_Marca)
        {
            ResultDTO<Ma_MarcaDTO> Respuesta = new ResultDTO<Ma_MarcaDTO>();
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
                        da.SelectCommand.Parameters.AddWithValue("@idMarca", oMa_Marca.idMarca);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oMa_Marca.idEmpresa);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            Respuesta.Resultado = "OK";
                            Respuesta.ListaResultado = ListarTodo(oMa_Marca.idEmpresa, "", cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Marca.idEmpresa, oMa_Marca.UsuarioModificacion,
                                "MANTENIMIENTOS-MARCAS", "Ma_Marca", oMa_Marca.idMarca, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            Respuesta.Resultado = "ERROR";
                        }
                    }
                    catch (Exception ex)
                    {
                        Respuesta.Resultado = "ERROR";
                        Respuesta.MensajeError = ex.Message;
                    }
                }
            }
            return Respuesta;
        }

    }
}
