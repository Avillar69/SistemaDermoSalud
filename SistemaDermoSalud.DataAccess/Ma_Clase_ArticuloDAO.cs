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
    public class Ma_Clase_ArticuloDAO
    {
        public ResultDTO<Ma_Clase_ArticuloDTO> ListarTodo(int idEmpresa, string Activo = "", SqlConnection cn = null)
        {
            ResultDTO<Ma_Clase_ArticuloDTO> oResultDTO = new ResultDTO<Ma_Clase_ArticuloDTO>();
            oResultDTO.ListaResultado = new List<Ma_Clase_ArticuloDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Clase_Articulo_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", 1);
                    da.SelectCommand.Parameters.AddWithValue("@Activo", Activo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_Clase_ArticuloDTO oMa_Clase_ArticuloDTO = new Ma_Clase_ArticuloDTO();
                        oMa_Clase_ArticuloDTO.idClaseArticulo = Convert.ToInt32(dr["idClaseArticulo"] == null ? 0 : Convert.ToInt32(dr["idClaseArticulo"].ToString()));
                        oMa_Clase_ArticuloDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oMa_Clase_ArticuloDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oMa_Clase_ArticuloDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_Clase_ArticuloDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_Clase_ArticuloDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_Clase_ArticuloDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_Clase_ArticuloDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_Clase_ArticuloDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMa_Clase_ArticuloDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_Clase_ArticuloDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Ma_Clase_ArticuloDTO> ListarxID(int idClaseArticulo)
        {
            ResultDTO<Ma_Clase_ArticuloDTO> oResultDTO = new ResultDTO<Ma_Clase_ArticuloDTO>();
            oResultDTO.ListaResultado = new List<Ma_Clase_ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Clase_Articulo_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idClaseArticulo", idClaseArticulo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_Clase_ArticuloDTO oMa_Clase_ArticuloDTO = new Ma_Clase_ArticuloDTO();
                        oMa_Clase_ArticuloDTO.idClaseArticulo = Convert.ToInt32(dr["idClaseArticulo"].ToString());
                        oMa_Clase_ArticuloDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oMa_Clase_ArticuloDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_Clase_ArticuloDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_Clase_ArticuloDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_Clase_ArticuloDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_Clase_ArticuloDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_Clase_ArticuloDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_Clase_ArticuloDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_Clase_ArticuloDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_Clase_ArticuloDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Ma_Clase_ArticuloDTO> UpdateInsert(Ma_Clase_ArticuloDTO oMa_Clase_Articulo)
        {
            ResultDTO<Ma_Clase_ArticuloDTO> oResultDTO = new ResultDTO<Ma_Clase_ArticuloDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Clase_Articulo_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idClaseArticulo", oMa_Clase_Articulo.idClaseArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oMa_Clase_Articulo.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_Clase_Articulo.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_Clase_Articulo.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_Clase_Articulo.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_Clase_Articulo.Estado);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oMa_Clase_Articulo.idEmpresa, "", cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Clase_Articulo.idEmpresa, oMa_Clase_Articulo.UsuarioModificacion,
                                "MANTENIMIENTOS-CLASES DE ARTICULOS", "Ma_Clase_Articulo", (int)id_output.Value, (oMa_Clase_Articulo.idClaseArticulo == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_Clase_ArticuloDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_Clase_ArticuloDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_Clase_ArticuloDTO> Delete(Ma_Clase_ArticuloDTO oMa_Clase_Articulo)
        {
            ResultDTO<Ma_Clase_ArticuloDTO> oResultDTO = new ResultDTO<Ma_Clase_ArticuloDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Clase_Articulo_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idClaseArticulo", oMa_Clase_Articulo.idClaseArticulo);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oMa_Clase_Articulo.idEmpresa, "", cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Clase_Articulo.idEmpresa, oMa_Clase_Articulo.UsuarioModificacion,
                                "MANTENIMIENTOS-CLASES DE ARTICULOS", "Ma_Clase_Articulo", oMa_Clase_Articulo.idClaseArticulo, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_Clase_ArticuloDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_Clase_ArticuloDTO>();
                    }
                }
            }
            return oResultDTO;
        }

    }
}
