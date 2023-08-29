using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SistemaDermoSalud.Entities;

namespace SistemaDermoSalud.DataAccess
{
    public class Seg_RolDAO
    {
        public ResultDTO<Seg_RolDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<Seg_RolDTO> oResultDTO = new ResultDTO<Seg_RolDTO>();
            oResultDTO.ListaResultado = new List<Seg_RolDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Rol_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Seg_RolDTO oSeg_RolDTO = new Seg_RolDTO();
                        oSeg_RolDTO.idRol = Convert.ToInt32(dr["idRol"] == null ? 0 : Convert.ToInt32(dr["idRol"].ToString()));
                        oSeg_RolDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oSeg_RolDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oSeg_RolDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oSeg_RolDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oSeg_RolDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oSeg_RolDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oSeg_RolDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oSeg_RolDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oSeg_RolDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Seg_RolDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Seg_RolDTO> ListarxID(int idRol)
        {
            ResultDTO<Seg_RolDTO> oResultDTO = new ResultDTO<Seg_RolDTO>();
            oResultDTO.ListaResultado = new List<Seg_RolDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Rol_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idRol", idRol);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Seg_RolDTO oSeg_RolDTO = new Seg_RolDTO();
                        oSeg_RolDTO.idRol = Convert.ToInt32(dr["idRol"].ToString());
                        oSeg_RolDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oSeg_RolDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oSeg_RolDTO.Descripcion = dr["Descripcion"].ToString();
                        oSeg_RolDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oSeg_RolDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oSeg_RolDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oSeg_RolDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oSeg_RolDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oSeg_RolDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Seg_RolDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Seg_RolDTO> UpdateInsert(Seg_RolDTO oSeg_Rol)
        {
            ResultDTO<Seg_RolDTO> oResultDTO = new ResultDTO<Seg_RolDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Rol_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idRol", oSeg_Rol.idRol);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oSeg_Rol.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oSeg_Rol.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oSeg_Rol.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oSeg_Rol.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oSeg_Rol.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oSeg_Rol.idEmpresa, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Seg_RolDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Seg_RolDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Seg_RolDTO> Delete(Seg_RolDTO oSeg_Rol)
        {
            ResultDTO<Seg_RolDTO> oResultDTO = new ResultDTO<Seg_RolDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Rol_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idRol", oSeg_Rol.idRol);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oSeg_Rol.idEmpresa, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Seg_RolDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Seg_RolDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
