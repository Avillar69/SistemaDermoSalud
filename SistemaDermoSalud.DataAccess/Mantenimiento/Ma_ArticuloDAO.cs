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
    public class Ma_ArticuloDAO
    {


        public ResultDTO<Ma_ArticuloDTO> ListarTodo(int idEmpresa, string Activo = "", SqlConnection cn = null)
        {
            ResultDTO<Ma_ArticuloDTO> oResultDTO = new ResultDTO<Ma_ArticuloDTO>();
            oResultDTO.ListaResultado = new List<Ma_ArticuloDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Articulo_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_ArticuloDTO oMa_ArticuloDTO = new Ma_ArticuloDTO();
                        oMa_ArticuloDTO.idArticulo = Convert.ToInt32(dr["idArticulo"] == null ? 0 : Convert.ToInt32(dr["idArticulo"].ToString()));
                        oMa_ArticuloDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oMa_ArticuloDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_ArticuloDTO.idMarca = Convert.ToInt32(dr["idMarca"] == null ? 0 : Convert.ToInt32(dr["idMarca"].ToString()));
                        oMa_ArticuloDTO.idCategoria = Convert.ToInt32(dr["idCategoria"] == null ? 0 : Convert.ToInt32(dr["idCategoria"].ToString()));
                        oMa_ArticuloDTO.idUnidadMedida = Convert.ToInt32(dr["idUnidadMedida"] == null ? 0 : Convert.ToInt32(dr["idUnidadMedida"].ToString()));
                        oMa_ArticuloDTO.idClaseArticulo = Convert.ToInt32(dr["idClaseArticulo"] == null ? 0 : Convert.ToInt32(dr["idClaseArticulo"].ToString()));
                        oMa_ArticuloDTO.CodigoBarras = dr["CodigoBarras"] == null ? "" : dr["CodigoBarras"].ToString();
                        oMa_ArticuloDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_ArticuloDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_ArticuloDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_ArticuloDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_ArticuloDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oMa_ArticuloDTO.DescripcionAlter = dr["DescripcionAlter"].ToString();
                        //descripcines
                        oMa_ArticuloDTO.DesEmpresa = dr["DesEmpresa"].ToString();
                        oMa_ArticuloDTO.DesCategoria = dr["DesCategoria"].ToString();
                        oMa_ArticuloDTO.DesMarca = dr["DesMarca"].ToString();
                        oMa_ArticuloDTO.DesUnidadMedida = dr["DesUnidadMedida"].ToString();
                        oMa_ArticuloDTO.DesUsuarioModificacion = dr["DesUsuarioModificacion"].ToString();
                        //Cambio 6/4/17
                        oMa_ArticuloDTO.CodigoAutogenerado = dr["CodigoAutogenerado"].ToString();
                        oMa_ArticuloDTO.CodigoProducto = dr["CodigoProducto"].ToString();
                        oMa_ArticuloDTO.CantidadMin = Convert.ToDecimal(dr["CantidadMin"].ToString() == "" ? 0 : Convert.ToDecimal(dr["CantidadMin"].ToString()));
                        oResultDTO.ListaResultado.Add(oMa_ArticuloDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_ArticuloDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_ArticuloDTO> ListarxID(int idArticulo)
        {
            ResultDTO<Ma_ArticuloDTO> oResultDTO = new ResultDTO<Ma_ArticuloDTO>();
            oResultDTO.ListaResultado = new List<Ma_ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Articulo_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idArticulo", idArticulo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_ArticuloDTO oMa_ArticuloDTO = new Ma_ArticuloDTO();
                        oMa_ArticuloDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                        oMa_ArticuloDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_ArticuloDTO.idMarca = Convert.ToInt32(dr["idMarca"].ToString());
                        oMa_ArticuloDTO.idCategoria = Convert.ToInt32(dr["idCategoria"].ToString());
                        oMa_ArticuloDTO.idUnidadMedida = Convert.ToInt32(dr["idUnidadMedida"].ToString());
                        oMa_ArticuloDTO.idClaseArticulo = Convert.ToInt32(dr["idClaseArticulo"].ToString());
                        oMa_ArticuloDTO.CodigoBarras = dr["CodigoBarras"].ToString();
                        oMa_ArticuloDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_ArticuloDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_ArticuloDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_ArticuloDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_ArticuloDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oMa_ArticuloDTO.DescripcionAlter = dr["DescripcionAlter"].ToString();
                        //descripcines
                        oMa_ArticuloDTO.DesEmpresa = dr["DesEmpresa"].ToString();
                        oMa_ArticuloDTO.DesCategoria = dr["DesCategoria"].ToString();
                        oMa_ArticuloDTO.DesMarca = dr["DesMarca"].ToString();
                        oMa_ArticuloDTO.DesUnidadMedida = dr["DesUnidadMedida"].ToString();
                        oMa_ArticuloDTO.DesUsuarioModificacion = dr["DesUsuarioModificacion"].ToString();
                        //Cambio 6/4/17
                        oMa_ArticuloDTO.CodigoAutogenerado = dr["CodigoAutogenerado"].ToString();
                        oMa_ArticuloDTO.CodigoProducto = dr["CodigoProducto"].ToString();
                        oMa_ArticuloDTO.CantidadMin = Convert.ToDecimal(dr["CantidadMin"].ToString() == "" ? 0 : Convert.ToDecimal(dr["CantidadMin"].ToString()));

                        oResultDTO.ListaResultado.Add(oMa_ArticuloDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<Ma_ArticuloDisenoDTO> listaDiseño = new List<Ma_ArticuloDisenoDTO>();
                            while (dr.Read())
                            {
                                Ma_ArticuloDisenoDTO objDiseño = new Ma_ArticuloDisenoDTO();
                                objDiseño.idArticuloDiseno = Convert.ToInt32(dr["idArticuloDiseno"].ToString());
                                objDiseño.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                                objDiseño.NombreArchivo = dr["NombreArchivo"].ToString();
                                objDiseño.Fecha = Convert.ToDateTime(dr["Fecha"].ToString());
                                objDiseño.Extension = dr["Extension"].ToString();
                                objDiseño.Archivo = dr["Archivo"].ToString();
                                objDiseño.Descripcion = dr["Descripcion"].ToString();
                                listaDiseño.Add(objDiseño);
                            }
                            oResultDTO.ListaResultado[0].oListaDiseño = listaDiseño;
                        }
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_ArticuloDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_ArticuloDTO> UpdateInsert(Ma_ArticuloDTO oMa_Articulo)
        {
            ResultDTO<Ma_ArticuloDTO> oResultDTO = new ResultDTO<Ma_ArticuloDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Articulo_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idArticulo", oMa_Articulo.idArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oMa_Articulo.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoBarras", oMa_Articulo.CodigoBarras);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_Articulo.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@idMarca", oMa_Articulo.idMarca);
                        da.SelectCommand.Parameters.AddWithValue("@idCategoria", oMa_Articulo.idCategoria);
                        da.SelectCommand.Parameters.AddWithValue("@idUnidadMedida", oMa_Articulo.idUnidadMedida);
                        da.SelectCommand.Parameters.AddWithValue("@idClaseArticulo", oMa_Articulo.idClaseArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_Articulo.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_Articulo.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_Articulo.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@DescripcionAlter", oMa_Articulo.DescripcionAlter);
                        da.SelectCommand.Parameters.AddWithValue("@Lista_Almacen", oMa_Articulo.Lista_Almacen);
                        //Cambio 6/4/17
                        da.SelectCommand.Parameters.AddWithValue("@CodigoProducto", oMa_Articulo.CodigoProducto);
                        da.SelectCommand.Parameters.AddWithValue("@CantidadMin", oMa_Articulo.CantidadMin);
                        //cambio 17/02/2018

                        da.SelectCommand.Parameters.AddWithValue("@lista", oMa_Articulo.artDetalleDiseño);
                        /*   da.SelectCommand.Parameters.AddWithValue("@NombreArchivo", oMa_Articulo.NombreArchivo);
                           da.SelectCommand.Parameters.AddWithValue("@FechaDiseño", oMa_Articulo.Fecha);
                           da.SelectCommand.Parameters.AddWithValue("@extension", oMa_Articulo.Extension);
                           da.SelectCommand.Parameters.AddWithValue("@archivo", oMa_Articulo.Archivo);
                           da.SelectCommand.Parameters.AddWithValue("@descripcionDiseño", oMa_Articulo.DescripcionDiseño);*/

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.Campo1 = id_output.Value.ToString();
                            oResultDTO.ListaResultado = ListarTodo(oMa_Articulo.idEmpresa, "", cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Articulo.idEmpresa, oMa_Articulo.UsuarioModificacion,
                                "MANTENIMIENTOS-ARTICULOS", "Ma_Articulo", (int)id_output.Value, (oMa_Articulo.idArticulo == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.Campo1 = "";
                            oResultDTO.ListaResultado = new List<Ma_ArticuloDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_ArticuloDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_ArticuloDTO> Delete(Ma_ArticuloDTO oMa_Articulo)
        {
            ResultDTO<Ma_ArticuloDTO> oResultDTO = new ResultDTO<Ma_ArticuloDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Articulo_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idArticulo", oMa_Articulo.idArticulo);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oMa_Articulo.idEmpresa, "", cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Articulo.idEmpresa, oMa_Articulo.UsuarioModificacion,
                                "MANTENIMIENTOS-ARTICULOS", "Ma_Articulo", oMa_Articulo.idArticulo, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_ArticuloDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_ArticuloDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_ArticuloDTO> ListarTodoxCategoria(int idEmpresa, int idCategoria, string Activo = "", SqlConnection cn = null)
        {
            ResultDTO<Ma_ArticuloDTO> oResultDTO = new ResultDTO<Ma_ArticuloDTO>();
            oResultDTO.ListaResultado = new List<Ma_ArticuloDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Articulo_ListarTodoxCategoria", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCategoria", idCategoria);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_ArticuloDTO oMa_ArticuloDTO = new Ma_ArticuloDTO();
                        oMa_ArticuloDTO.idArticulo = Convert.ToInt32(dr["idArticulo"] == null ? 0 : Convert.ToInt32(dr["idArticulo"].ToString()));
                        oMa_ArticuloDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_ArticuloDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_ArticuloDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oMa_ArticuloDTO.DescripcionAlter = dr["DescripcionAlter"].ToString();
                        //descripcines
                        oMa_ArticuloDTO.DesUsuarioModificacion = dr["DesUsuarioModificacion"].ToString();
                        //Cambio 6/4/17
                        oMa_ArticuloDTO.CodigoProducto = dr["CodigoProducto"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_ArticuloDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_ArticuloDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_ArticuloDTO> ObtenerPrecioArtProv(int idEmpresa, int idArticulo, int idProveedor, int idMoneda)
        {
            ResultDTO<Ma_ArticuloDTO> oResultDTO = new ResultDTO<Ma_ArticuloDTO>();
            oResultDTO.ListaResultado = new List<Ma_ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Articulo_ObtenerPrecioArtProv", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@idArticulo", idArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@idProveedor", idProveedor);
                    da.SelectCommand.Parameters.AddWithValue("@idMoneda", idMoneda);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_ArticuloDTO oMa_ArticuloDTO = new Ma_ArticuloDTO();
                        oMa_ArticuloDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                        oMa_ArticuloDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_ArticuloDTO.idMarca = Convert.ToInt32(dr["idMarca"].ToString());
                        oMa_ArticuloDTO.idCategoria = Convert.ToInt32(dr["idCategoria"].ToString());
                        oMa_ArticuloDTO.idUnidadMedida = Convert.ToInt32(dr["idUnidadMedida"].ToString());
                        oMa_ArticuloDTO.idClaseArticulo = Convert.ToInt32(dr["idClaseArticulo"].ToString());
                        oMa_ArticuloDTO.CodigoBarras = dr["CodigoBarras"].ToString();
                        oMa_ArticuloDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_ArticuloDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_ArticuloDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_ArticuloDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_ArticuloDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oMa_ArticuloDTO.DescripcionAlter = dr["DescripcionAlter"].ToString();
                        //descripcines
                        oMa_ArticuloDTO.DesUnidadMedida = dr["DesUnidadMedida"].ToString();
                        //Cambio 6/4/17
                        oMa_ArticuloDTO.CodigoAutogenerado = dr["CodigoAutogenerado"].ToString();
                        oMa_ArticuloDTO.CodigoProducto = dr["CodigoProducto"].ToString();
                        oMa_ArticuloDTO.CantidadMin = Convert.ToDecimal(dr["CantidadMin"].ToString() == "" ? 0 : Convert.ToDecimal(dr["CantidadMin"].ToString()));
                        oMa_ArticuloDTO.PrecioCompra = Convert.ToDecimal(dr["Valor"].ToString() == "" ? 0 : Convert.ToDecimal(dr["Valor"].ToString()));
                        oResultDTO.ListaResultado.Add(oMa_ArticuloDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_ArticuloDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_ArticuloDisenoDTO> GetFileDiseno(int idArticuloDiseno)
        {
            ResultDTO<Ma_ArticuloDisenoDTO> oResultDTO = new ResultDTO<Ma_ArticuloDisenoDTO>();
            oResultDTO.ListaResultado = new List<Ma_ArticuloDisenoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Articulo_GetFileDiseno", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idArticuloDiseno", idArticuloDiseno);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_ArticuloDisenoDTO oMa_ArticuloDisenoDTO = new Ma_ArticuloDisenoDTO();
                        oMa_ArticuloDisenoDTO.Archivo = dr["Archivo"].ToString();
                        oMa_ArticuloDisenoDTO.NombreArchivo = dr["NombreArchivo"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_ArticuloDisenoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_ArticuloDisenoDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
