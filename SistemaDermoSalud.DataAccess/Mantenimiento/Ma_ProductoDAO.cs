using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;

namespace SistemaDermoSalud.DataAccess.Mantenimiento
{
    public class Ma_ProductoDAO
    {
        public ResultDTO<Ma_ProductoDTO> ListarporMarca(int p, SqlConnection cn = null)
        {
            ResultDTO<Ma_ProductoDTO> oResultDTO = new ResultDTO<Ma_ProductoDTO>();
            oResultDTO.ListaResultado = new List<Ma_ProductoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Producto_Marca", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@param", p);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_ProductoDTO oProductoDTO = new Ma_ProductoDTO();
                        oProductoDTO.idProducto = Convert.ToInt32(dr["idProducto"] == null ? 0 : Convert.ToInt32(dr["idProducto"].ToString()));
                        oProductoDTO.idMarca = Convert.ToInt32(dr["idMarca"] == null ? 0 : Convert.ToInt32(dr["idMarca"].ToString()));
                        oProductoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oResultDTO.ListaResultado.Add(oProductoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_ProductoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_ProductoDTO> ListarTodo(int p, SqlConnection cn = null)
        {
            ResultDTO<Ma_ProductoDTO> oResultDTO = new ResultDTO<Ma_ProductoDTO>();
            oResultDTO.ListaResultado = new List<Ma_ProductoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Producto_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@param", p);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_ProductoDTO oProductoDTO = new Ma_ProductoDTO();
                        oProductoDTO.idProducto = Convert.ToInt32(dr["idProducto"] == null ? 0 : Convert.ToInt32(dr["idProducto"].ToString()));
                        oProductoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oProductoDTO.idMarca = Convert.ToInt32(dr["idMarca"] == null ? 0 : Convert.ToInt32(dr["idMarca"].ToString()));
                        oProductoDTO.Marca = dr["Marca"] == null ? "" : dr["Marca"].ToString();
                        oProductoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oProductoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oProductoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oProductoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oProductoDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oProductoDTO.Precio = Convert.ToDecimal(dr["Precio"].ToString());
                        oProductoDTO.CodigoProducto = dr["CodigoProducto"] == null ? "" : dr["CodigoProducto"].ToString();
                        oResultDTO.ListaResultado.Add(oProductoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_ProductoDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Ma_ProductoDTO> ListarxID(int idProducto)
        {
            ResultDTO<Ma_ProductoDTO> oResultDTO = new ResultDTO<Ma_ProductoDTO>();
            oResultDTO.ListaResultado = new List<Ma_ProductoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Producto_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idProducto", idProducto);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_ProductoDTO oProductoDTO = new Ma_ProductoDTO();
                        oProductoDTO.idProducto = Convert.ToInt32(dr["idProducto"].ToString());
                        oProductoDTO.Descripcion = dr["Descripcion"].ToString();
                        oProductoDTO.idMarca = Convert.ToInt32(dr["idMarca"].ToString());
                        oProductoDTO.Marca = dr["Marca"].ToString();
                        oProductoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oProductoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oProductoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oProductoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oProductoDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oProductoDTO.Precio = Convert.ToDecimal(dr["Precio"].ToString());
                        oProductoDTO.CodigoProducto = dr["CodigoProducto"].ToString();
                        oProductoDTO.CodigoAutogenerado = dr["CodigoAutogenerado"].ToString();
                        oProductoDTO.idTalla = Convert.ToInt32(dr["idTalla"].ToString());
                        oProductoDTO.idColor = Convert.ToInt32(dr["idColor"].ToString());
                        oResultDTO.ListaResultado.Add(oProductoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_ProductoDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Ma_ProductoDTO> UpdateInsert(Ma_ProductoDTO oProducto)
        {
            ResultDTO<Ma_ProductoDTO> oResultDTO = new ResultDTO<Ma_ProductoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Producto_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idProducto", oProducto.idProducto);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oProducto.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@idMarca", oProducto.idMarca);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oProducto.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oProducto.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oProducto.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Precio", oProducto.Precio);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoProducto", oProducto.CodigoProducto);
                        da.SelectCommand.Parameters.AddWithValue("@idTalla", oProducto.idTalla);
                        da.SelectCommand.Parameters.AddWithValue("@idColor", oProducto.idColor);

                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(1, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_ProductoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_ProductoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_ProductoDTO> Delete(Ma_ProductoDTO oProducto)
        {
            ResultDTO<Ma_ProductoDTO> oResultDTO = new ResultDTO<Ma_ProductoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Producto_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idProducto", oProducto.idProducto);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(1, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_ProductoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_ProductoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
