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
   public class Ma_AlmacenDAO
    {
        public ResultDTO<Ma_AlmacenDTO> ListarTodo(int idEmpresa, string Activo = "", SqlConnection cn = null)
        {
            ResultDTO<Ma_AlmacenDTO> oResultDTO = new ResultDTO<Ma_AlmacenDTO>();
            oResultDTO.ListaResultado = new List<Ma_AlmacenDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Almacen_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_AlmacenDTO oMa_AlmacenDTO = new Ma_AlmacenDTO();
                        oMa_AlmacenDTO.idAlmacen = Convert.ToInt32(dr["idAlmacen"] == null ? 0 : Convert.ToInt32(dr["idAlmacen"].ToString()));
                        oMa_AlmacenDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oMa_AlmacenDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oMa_AlmacenDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_AlmacenDTO.Direccion = dr["Direccion"] == null ? "" : dr["Direccion"].ToString();
                        oMa_AlmacenDTO.Telefono = dr["Telefono"] == null ? "" : dr["Telefono"].ToString();
                        oMa_AlmacenDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_AlmacenDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_AlmacenDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_AlmacenDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_AlmacenDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oMa_AlmacenDTO.DesUsuarioModificacion = dr["DesUsuarioModificacion"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_AlmacenDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_AlmacenDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_AlmacenDTO> ListarxID(int idAlmacen)
        {
            ResultDTO<Ma_AlmacenDTO> oResultDTO = new ResultDTO<Ma_AlmacenDTO>();
            oResultDTO.ListaResultado = new List<Ma_AlmacenDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Almacen_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idAlmacen", idAlmacen);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_AlmacenDTO oMa_AlmacenDTO = new Ma_AlmacenDTO();
                        oMa_AlmacenDTO.idAlmacen = Convert.ToInt32(dr["idAlmacen"].ToString());
                        oMa_AlmacenDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oMa_AlmacenDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_AlmacenDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_AlmacenDTO.Direccion = dr["Direccion"].ToString();
                        oMa_AlmacenDTO.Telefono = dr["Telefono"].ToString();
                        oMa_AlmacenDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_AlmacenDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_AlmacenDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_AlmacenDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_AlmacenDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_AlmacenDTO);
                    }
                    if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<Ma_ArticuloDTO> lista = new List<Ma_ArticuloDTO>();
                            while (dr.Read())
                            {
                                Ma_ArticuloDTO obj = new Ma_ArticuloDTO();
                                obj.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                                obj.Descripcion = dr["Descripcion"].ToString();
                                obj.CodigoBarras = dr["CodigoBarras"].ToString();
                                obj.DesCategoria = dr["DesCategoria"].ToString();
                                obj.DesMarca = dr["DesMarca"].ToString();
                                obj.DesUnidadMedida = dr["DesUnidadMedida"].ToString();
                                lista.Add(obj);
                            }
                            oResultDTO.ListaResultado[0].oListaArticulo = lista;
                        }
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_AlmacenDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_AlmacenDTO> ListarxIDTransfromacion(int idAlmacen, int Categoria)
        {
            ResultDTO<Ma_AlmacenDTO> oResultDTO = new ResultDTO<Ma_AlmacenDTO>();
            oResultDTO.ListaResultado = new List<Ma_AlmacenDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Almacen_ListarxID_Categoria", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idAlmacen", idAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@idCategoria", Categoria);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_AlmacenDTO oMa_AlmacenDTO = new Ma_AlmacenDTO();
                        oMa_AlmacenDTO.idAlmacen = Convert.ToInt32(dr["idAlmacen"].ToString());
                        oMa_AlmacenDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oMa_AlmacenDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_AlmacenDTO.Descripcion = dr["Descripcion"].ToString();
                        oMa_AlmacenDTO.Direccion = dr["Direccion"].ToString();
                        oMa_AlmacenDTO.Telefono = dr["Telefono"].ToString();
                        oMa_AlmacenDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_AlmacenDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_AlmacenDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_AlmacenDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_AlmacenDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_AlmacenDTO);
                    }
                    if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<Ma_ArticuloDTO> lista = new List<Ma_ArticuloDTO>();
                            while (dr.Read())
                            {
                                Ma_ArticuloDTO obj = new Ma_ArticuloDTO();
                                obj.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                                obj.Descripcion = dr["Descripcion"].ToString();
                                obj.CodigoBarras = dr["CodigoBarras"].ToString();
                                obj.DesCategoria = dr["DesCategoria"].ToString();
                                obj.DesMarca = dr["DesMarca"].ToString();
                                obj.DesUnidadMedida = dr["DesUnidadMedida"].ToString();
                                lista.Add(obj);
                            }
                            oResultDTO.ListaResultado[0].oListaArticulo = lista;
                        }
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_AlmacenDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_AlmacenDTO> UpdateInsert(Ma_AlmacenDTO oMa_Almacen)
        {
            ResultDTO<Ma_AlmacenDTO> oResultDTO = new ResultDTO<Ma_AlmacenDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Almacen_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idAlmacen", oMa_Almacen.idAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oMa_Almacen.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMa_Almacen.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Direccion", oMa_Almacen.Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono", oMa_Almacen.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_Almacen.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_Almacen.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_Almacen.Estado);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oMa_Almacen.idEmpresa, "", cn).ListaResultado;
                            oResultDTO.Campo1 = id_output.Value.ToString();
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Almacen.idEmpresa, oMa_Almacen.UsuarioModificacion,
                                "MANTENIMIENTOS-ALMACEN", "Ma_Almacen", (int)id_output.Value, (oMa_Almacen.idAlmacen == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.Campo1 = "";
                            oResultDTO.ListaResultado = new List<Ma_AlmacenDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_AlmacenDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_AlmacenDTO> Delete(Ma_AlmacenDTO oMa_Almacen)
        {
            ResultDTO<Ma_AlmacenDTO> oResultDTO = new ResultDTO<Ma_AlmacenDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Almacen_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idAlmacen", oMa_Almacen.idAlmacen);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oMa_Almacen.idEmpresa, "", cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Almacen.idEmpresa, oMa_Almacen.UsuarioModificacion,
                                "MANTENIMIENTOS-ALMACEN", "Ma_Almacen", oMa_Almacen.idAlmacen, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_AlmacenDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_AlmacenDTO>();
                    }
                }
            }
            return oResultDTO;
        }

    }
}
