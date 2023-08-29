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
    public class FN_ProyectoDAO
    {

        public ResultDTO<FN_ProyectosDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<FN_ProyectosDTO> oResultDTO = new ResultDTO<FN_ProyectosDTO>();
            oResultDTO.ListaResultado = new List<FN_ProyectosDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_Proyecto_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_ProyectosDTO oProyecto = new FN_ProyectosDTO();
                        oProyecto.idProyecto = Convert.ToInt32(dr["idProyecto"] == null ? 0 : Convert.ToInt32(dr["idProyecto"].ToString()));
                        oProyecto.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oProyecto.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oProyecto.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oProyecto.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oProyecto.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oProyecto.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oProyecto.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oProyecto.UsuarioModificacionDescripcion = dr["UsuarioModificacionDescripcion"] == null ? "" : dr["UsuarioModificacionDescripcion"].ToString();
                        oResultDTO.ListaResultado.Add(oProyecto);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_ProyectosDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_ProyectosDTO> ListarxID(int idProyecto)
        {
            ResultDTO<FN_ProyectosDTO> oResultDTO = new ResultDTO<FN_ProyectosDTO>();
            oResultDTO.ListaResultado = new List<FN_ProyectosDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_Proyecto_ListarXID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idProyecto", idProyecto);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_ProyectosDTO oFN_ProyectoDTO = new FN_ProyectosDTO();
                        oFN_ProyectoDTO.idProyecto = Convert.ToInt32(dr["idProyecto"].ToString());
                        oFN_ProyectoDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oFN_ProyectoDTO.Descripcion = dr["Descripcion"].ToString();
                        oFN_ProyectoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_ProyectoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_ProyectoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oFN_ProyectoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oFN_ProyectoDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oFN_ProyectoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_ProyectosDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_ProyectosDTO> UpdateInsert(FN_ProyectosDTO oFN_Proyecto)
        {
            ResultDTO<FN_ProyectosDTO> Respuesta = new ResultDTO<FN_ProyectosDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_Proyecto_InsertUpdate", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idProyecto", oFN_Proyecto.idProyecto);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oFN_Proyecto.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oFN_Proyecto.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oFN_Proyecto.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oFN_Proyecto.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oFN_Proyecto.Estado);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            Respuesta.Resultado = "OK";
                            Respuesta.ListaResultado = (ListarTodo(oFN_Proyecto.idEmpresa, cn).ListaResultado);
                            Respuesta.Campo1 = id_output.Value.ToString();
                            new Seg_LogDAO().UpdateInsert(da, cn, oFN_Proyecto.idEmpresa, oFN_Proyecto.UsuarioModificacion,
                                "FINANZAS-Proyecto", "FN_Proyectos", (int)id_output.Value, (oFN_Proyecto.idProyecto == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            Respuesta.Resultado = "ERROR";
                            Respuesta.ListaResultado = new List<FN_ProyectosDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        Respuesta.Resultado = "ERROR";
                        Respuesta.MensajeError = ex.Message;
                        Respuesta.ListaResultado = new List<FN_ProyectosDTO>();
                    }
                }
            }
            return Respuesta;
        }
        public ResultDTO<FN_ProyectosDTO> Delete(FN_ProyectosDTO oFN_Proyecto)
        {
            ResultDTO<FN_ProyectosDTO> Respuesta = new ResultDTO<FN_ProyectosDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_Proyecto_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idProyecto", oFN_Proyecto.idProyecto);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oFN_Proyecto.idEmpresa);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            Respuesta.Resultado = "OK";
                            Respuesta.ListaResultado = ListarTodo(oFN_Proyecto.idEmpresa, cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oFN_Proyecto.idEmpresa, oFN_Proyecto.UsuarioModificacion,
                                "FINANZAS-Proyecto", "FN_Proyectos", oFN_Proyecto.idProyecto, "DELETE");
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
        public ResultDTO<FN_ProyectosDTO> ListarProyectosCaja( SqlConnection cn = null)
        {
            ResultDTO<FN_ProyectosDTO> oResultDTO = new ResultDTO<FN_ProyectosDTO>();
            oResultDTO.ListaResultado = new List<FN_ProyectosDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_Proyecto_ListarCaja", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_ProyectosDTO oProyecto = new FN_ProyectosDTO();
                        oProyecto.idProyecto = Convert.ToInt32(dr["idProyecto"] == null ? 0 : Convert.ToInt32(dr["idProyecto"].ToString()));
                        oProyecto.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oProyecto.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oProyecto.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oProyecto.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oProyecto.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oProyecto.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oProyecto.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oProyecto.UsuarioModificacionDescripcion = dr["UsuarioModificacionDescripcion"] == null ? "" : dr["UsuarioModificacionDescripcion"].ToString();
                        oResultDTO.ListaResultado.Add(oProyecto);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_ProyectosDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
