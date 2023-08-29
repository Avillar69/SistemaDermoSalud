using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SistemaDermoSalud.DataAccess.Mantenimiento;
using SistemaDermoSalud.Entities;

namespace SistemaDermoSalud.DataAccess
{
    public class Seg_UsuarioDAO
    {
        public List<Seg_UsuarioDTO> ListarTodo(int idEmpresa)
        {
            List<Seg_UsuarioDTO> lstSeg_UsuarioDTO = new List<Seg_UsuarioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Usuario_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Seg_UsuarioDTO oSeg_UsuarioDTO = new Seg_UsuarioDTO();
                        oSeg_UsuarioDTO.idUsuario = Convert.ToInt32(dr["idUsuario"] == null ? 0 : Convert.ToInt32(dr["idUsuario"].ToString()));
                        oSeg_UsuarioDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oSeg_UsuarioDTO.idRol = Convert.ToInt32(dr["idRol"] == null ? 0 : Convert.ToInt32(dr["idRol"].ToString()));
                        oSeg_UsuarioDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oSeg_UsuarioDTO.Usuario = dr["Usuario"] == null ? "" : dr["Usuario"].ToString();
                        oSeg_UsuarioDTO.Password = dr["Password"] == null ? "" : dr["Password"].ToString();
                        oSeg_UsuarioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oSeg_UsuarioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oSeg_UsuarioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oSeg_UsuarioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oSeg_UsuarioDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oSeg_UsuarioDTO.RolDescripcion = dr["RolDescripcion"] == null ? "" : dr["RolDescripcion"].ToString();
                        oSeg_UsuarioDTO.UsuarioModificacionDescripcion = dr["UsuarioModificacionDescripcion"] == null ? "" : dr["UsuarioModificacionDescripcion"].ToString();
                        oSeg_UsuarioDTO.oListaLocales = new Ma_LocalDAO().ListarxUsuario(oSeg_UsuarioDTO.idUsuario).ListaResultado;
                        lstSeg_UsuarioDTO.Add(oSeg_UsuarioDTO);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return lstSeg_UsuarioDTO;
        }
        public List<Seg_UsuarioDTO> ListarxID(int idUsuario)
        {
            List<Seg_UsuarioDTO> lstSeg_UsuarioDTO = new List<Seg_UsuarioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Usuario_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Seg_UsuarioDTO oSeg_UsuarioDTO = new Seg_UsuarioDTO();
                        oSeg_UsuarioDTO.idUsuario = Convert.ToInt32(dr["idUsuario"] == null ? 0 : Convert.ToInt32(dr["idUsuario"].ToString()));
                        oSeg_UsuarioDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oSeg_UsuarioDTO.idRol = Convert.ToInt32(dr["idRol"] == null ? 0 : Convert.ToInt32(dr["idRol"].ToString()));
                        oSeg_UsuarioDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oSeg_UsuarioDTO.Usuario = dr["Usuario"] == null ? "" : dr["Usuario"].ToString();
                        oSeg_UsuarioDTO.Password = dr["Password"] == null ? "" : dr["Password"].ToString();
                        oSeg_UsuarioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oSeg_UsuarioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oSeg_UsuarioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oSeg_UsuarioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oSeg_UsuarioDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oSeg_UsuarioDTO.oListaLocales = new Ma_LocalDAO().ListarxUsuario(oSeg_UsuarioDTO.idUsuario).ListaResultado;
                        lstSeg_UsuarioDTO.Add(oSeg_UsuarioDTO);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return lstSeg_UsuarioDTO;
        }
        public ResultDTO<Seg_UsuarioDTO> ValidarLogin(string Usuario, string Password)
        {
            ResultDTO<Seg_UsuarioDTO> Respuesta = new ResultDTO<Seg_UsuarioDTO>();
            List<Seg_UsuarioDTO> lstSeg_UsuarioDTO = new List<Seg_UsuarioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Usuario_ValidarLogin", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Usuario", Usuario);
                    da.SelectCommand.Parameters.AddWithValue("@Password", Password);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Seg_UsuarioDTO oSeg_UsuarioDTO = new Seg_UsuarioDTO();
                        oSeg_UsuarioDTO.idUsuario = Convert.ToInt32(dr["idUsuario"] == null ? 0 : Convert.ToInt32(dr["idUsuario"].ToString()));
                        oSeg_UsuarioDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oSeg_UsuarioDTO.idRol = Convert.ToInt32(dr["idRol"] == null ? 0 : Convert.ToInt32(dr["idRol"].ToString()));
                        oSeg_UsuarioDTO.Usuario = dr["Usuario"] == null ? "" : dr["Usuario"].ToString();
                        oSeg_UsuarioDTO.Password = dr["Password"] == null ? "" : dr["Password"].ToString();
                        oSeg_UsuarioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oSeg_UsuarioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oSeg_UsuarioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oSeg_UsuarioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oSeg_UsuarioDTO.Nombre = dr["Nombres"] == null ? "" : dr["Nombres"].ToString();
                        oSeg_UsuarioDTO.Apellido = dr["ApellidoPaterno"] == null ? "" : dr["ApellidoPaterno"].ToString();
                        oSeg_UsuarioDTO.Imagen = dr["Imagen"] == null ? "" : dr["Imagen"].ToString();
                        oSeg_UsuarioDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        lstSeg_UsuarioDTO.Add(oSeg_UsuarioDTO);
                    }
                    Respuesta.ListaResultado = lstSeg_UsuarioDTO;
                    if (Respuesta.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<Ma_LocalDTO> lista = new List<Ma_LocalDTO>();
                            while (dr.Read())
                            {
                                Ma_LocalDTO obj = new Ma_LocalDTO();
                                obj.idLocal = Convert.ToInt32(dr["idLocal"].ToString());
                                obj.Descripcion = dr["Descripcion"].ToString();
                                lista.Add(obj);
                            }
                            Respuesta.ListaResultado[0].oListaLocales = lista;
                        }
                        if (dr.NextResult())
                        {
                            List<Seg_NotificacionDTO> lista = new List<Seg_NotificacionDTO>();
                            while (dr.Read())
                            {
                                Seg_NotificacionDTO obj = new Seg_NotificacionDTO();
                                obj.idNotificacion = Convert.ToInt32(dr["idNotificacion"].ToString());
                                obj.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                                obj.idRol = Convert.ToInt32(dr["idRol"].ToString());
                                obj.Fecha = Convert.ToDateTime(dr["Fecha"].ToString());
                                obj.Titulo = dr["Titulo"].ToString();
                                obj.Texto = dr["Texto"].ToString();
                                obj.Contenido = dr["Contenido"].ToString();
                                obj.idEstado = dr["idEstado"].ToString() == "" ? 0 : Convert.ToInt32(dr["idEstado"].ToString());
                                lista.Add(obj);
                            }
                            Respuesta.ListaResultado[0].oListaNotificacion = lista;
                        }
                    }
                    Respuesta.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    Respuesta.MensajeError = ex.Message;
                    Respuesta.Resultado = "ERROR";
                }
            }
            return Respuesta;
        }
        public ResultDTO<Seg_UsuarioDTO> Delete(Seg_UsuarioDTO oSeg_UsuarioDTO)
        {
            ResultDTO<Seg_UsuarioDTO> Respuesta = new ResultDTO<Seg_UsuarioDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Usuario_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idUsuario", oSeg_UsuarioDTO.idUsuario);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            Respuesta.Resultado = "OK";
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
        public ResultDTO<Seg_UsuarioDTO> UpdateInsert(Seg_UsuarioDTO oSeg_UsuarioDTO)
        {
            ResultDTO<Seg_UsuarioDTO> Respuesta = new ResultDTO<Seg_UsuarioDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Usuario_UpdateInsert", cn);
                        da.SelectCommand.Parameters.AddWithValue("@idUsuario", oSeg_UsuarioDTO.idUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@idRol", oSeg_UsuarioDTO.idRol);
                        da.SelectCommand.Parameters.AddWithValue("@Usuario", oSeg_UsuarioDTO.Usuario);
                        da.SelectCommand.Parameters.AddWithValue("@Password", oSeg_UsuarioDTO.Password);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oSeg_UsuarioDTO.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oSeg_UsuarioDTO.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oSeg_UsuarioDTO.Estado);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            Respuesta.Resultado = "OK";
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
