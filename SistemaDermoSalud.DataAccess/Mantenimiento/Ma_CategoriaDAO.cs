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
   public  class Ma_CategoriaDAO
    {
        public ResultDTO<Ma_CategoriaDTO> ListarTodo(int idEmpresa, string Activo = "", SqlConnection cn = null)
        {
            ResultDTO<Ma_CategoriaDTO> oResultDTO = new ResultDTO<Ma_CategoriaDTO>();
            oResultDTO.ListaResultado = new List<Ma_CategoriaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Categoria_ListarTodo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@Activo", Activo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_CategoriaDTO oMa_CategoriaDTO = new Ma_CategoriaDTO();
                        oMa_CategoriaDTO.idCategoria = Convert.ToInt32(dr["idCategoria"] == null ? 0 : Convert.ToInt32(dr["idCategoria"].ToString()));
                        oMa_CategoriaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oMa_CategoriaDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oMa_CategoriaDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_CategoriaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_CategoriaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_CategoriaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_CategoriaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_CategoriaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        //descripciones
                        oMa_CategoriaDTO.DesUsuarioModificacion = dr["DesUsuarioModificacion"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_CategoriaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_CategoriaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_CategoriaDTO> ListarTodoxTipo(int idEmpresa, int tipo, string Activo = "", SqlConnection cn = null)
        {
            ResultDTO<Ma_CategoriaDTO> oResultDTO = new ResultDTO<Ma_CategoriaDTO>();
            oResultDTO.ListaResultado = new List<Ma_CategoriaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Categoria_ListarTodoxTipo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@Activo", Activo);
                    da.SelectCommand.Parameters.AddWithValue("@Tipo", tipo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_CategoriaDTO oMa_CategoriaDTO = new Ma_CategoriaDTO();
                        oMa_CategoriaDTO.idCategoria = Convert.ToInt32(dr["idCategoria"] == null ? 0 : Convert.ToInt32(dr["idCategoria"].ToString()));
                        oMa_CategoriaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oMa_CategoriaDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oMa_CategoriaDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_CategoriaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_CategoriaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_CategoriaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_CategoriaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_CategoriaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        //descripciones
                        oMa_CategoriaDTO.DesUsuarioModificacion = dr["DesUsuarioModificacion"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_CategoriaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_CategoriaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_CategoriaDTO> ListarxID(int idCategoria)
        {
            ResultDTO<Ma_CategoriaDTO> oResultDTO = new ResultDTO<Ma_CategoriaDTO>();
            oResultDTO.ListaResultado = new List<Ma_CategoriaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Categoria_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCategoria", idCategoria);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_CategoriaDTO oMa_CategoriaDTO = new Ma_CategoriaDTO();
                        oMa_CategoriaDTO.idCategoria = Convert.ToInt32(dr["idCategoria"].ToString());
                        oMa_CategoriaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oMa_CategoriaDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_CategoriaDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_CategoriaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_CategoriaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_CategoriaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_CategoriaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_CategoriaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_CategoriaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_CategoriaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_CategoriaDTO> UpdateInsert(Ma_CategoriaDTO oMa_Categoria)
        {
            ResultDTO<Ma_CategoriaDTO> oResultDTO = new ResultDTO<Ma_CategoriaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Categoria_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idCategoria", oMa_Categoria.idCategoria);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oMa_Categoria.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_Categoria.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_Categoria.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_Categoria.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_Categoria.Estado);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            cn.Close();
                            oResultDTO.ListaResultado = ListarTodo(oMa_Categoria.idEmpresa, "", cn).ListaResultado;
                            oResultDTO.Campo1 = id_output.Value.ToString();
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Categoria.idEmpresa, oMa_Categoria.UsuarioModificacion,
                                "MANTENIMIENTOS-CATEGORIAS", "Ma_Categoria", (int)id_output.Value, (oMa_Categoria.idCategoria == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.Campo1 = "";
                            oResultDTO.ListaResultado = new List<Ma_CategoriaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_CategoriaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_CategoriaDTO> Delete(Ma_CategoriaDTO oMa_Categoria)
        {
            ResultDTO<Ma_CategoriaDTO> oResultDTO = new ResultDTO<Ma_CategoriaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Categoria_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idCategoria", oMa_Categoria.idCategoria);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            cn.Close();
                            oResultDTO.ListaResultado = ListarTodo(oMa_Categoria.idEmpresa, "", cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Categoria.idEmpresa, oMa_Categoria.UsuarioModificacion,
                                "MANTENIMIENTOS-CATEGORIAS", "Ma_Categoria", oMa_Categoria.idCategoria, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_CategoriaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_CategoriaDTO>();
                    }
                }
            }
            return oResultDTO;
        }


    }
}
