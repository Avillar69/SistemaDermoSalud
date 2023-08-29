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
   public class AD_GuiaRemisionDAO
    {
        public ResultDTO<AD_GuiaRemisionDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, SqlConnection cn = null)
        {
            ResultDTO<AD_GuiaRemisionDTO> oResultDTO = new ResultDTO<AD_GuiaRemisionDTO>();
            oResultDTO.ListaResultado = new List<AD_GuiaRemisionDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_AD_GuiaRemision_ListarxFecha", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {

                        AD_GuiaRemisionDTO oAD_GuiaRemisionDTO = new AD_GuiaRemisionDTO();
                        oAD_GuiaRemisionDTO.idGuiaRemision = Convert.ToInt32(dr["idGuiaRemision"] == null ? 0 : Convert.ToInt32(dr["idGuiaRemision"].ToString()));
                        oAD_GuiaRemisionDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oAD_GuiaRemisionDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oAD_GuiaRemisionDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oAD_GuiaRemisionDTO.ProveedorDocumento = dr["ProveedorDocumento"] == null ? "" : dr["ProveedorDocumento"].ToString();
                        oAD_GuiaRemisionDTO.ProveedorDireccion = dr["ProveedorDireccion"] == null ? "" : dr["ProveedorDireccion"].ToString();
                        oAD_GuiaRemisionDTO.SerieGuia = dr["SerieGuia"] == null ? "" : dr["SerieGuia"].ToString();
                        oAD_GuiaRemisionDTO.NumeroGuia = dr["NumeroGuia"] == null ? "" : dr["NumeroGuia"].ToString();//*
                        oAD_GuiaRemisionDTO.NOrdenCompra = dr["NOrdenCompra"] == null ? "" : dr["NOrdenCompra"].ToString();//-
                        oAD_GuiaRemisionDTO.NDocRef = dr["NDocRef"] == null ? "" : dr["NDocRef"].ToString();//-
                        oAD_GuiaRemisionDTO.Ruc = dr["Ruc"] == null ? "" : dr["Ruc"].ToString();//*
                        oAD_GuiaRemisionDTO.FechaInicioTraslado = Convert.ToDateTime(dr["FechaInicioTraslado"].ToString());
                        oAD_GuiaRemisionDTO.PuntoPartida = dr["PuntoPartida"] == null ? "" : dr["PuntoPartida"].ToString();
                        oAD_GuiaRemisionDTO.PuntoLlegada = dr["PuntoPartida"] == null ? "" : dr["PuntoPartida"].ToString();
                        oAD_GuiaRemisionDTO.FechaFinTraslado = Convert.ToDateTime(dr["FechaFinTraslado"].ToString());
                        oAD_GuiaRemisionDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oAD_GuiaRemisionDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oAD_GuiaRemisionDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oAD_GuiaRemisionDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oAD_GuiaRemisionDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oAD_GuiaRemisionDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AD_GuiaRemisionDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_GuiaRemisionDTO> ListarxID(int idGuiaRemision)
        {
            ResultDTO<AD_GuiaRemisionDTO> oResultDTO = new ResultDTO<AD_GuiaRemisionDTO>();
            oResultDTO.ListaResultado = new List<AD_GuiaRemisionDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_AD_GuiaRemision_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idGuiaRemision", idGuiaRemision);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AD_GuiaRemisionDTO oAD_GuiaRemisionDTO = new AD_GuiaRemisionDTO();
                        oAD_GuiaRemisionDTO.idGuiaRemision = Convert.ToInt32(dr["idGuiaRemision"] == null ? 0 : Convert.ToInt32(dr["idGuiaRemision"].ToString()));
                        oAD_GuiaRemisionDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        // oAD_GuiaRemisionDTO.idLocal = Convert.ToInt32(dr["idLocal"] == null ? 0 : Convert.ToInt32(dr["idLocal"].ToString()));
                        // oAD_GuiaRemisionDTO.idAlmacen = Convert.ToInt32(dr["idAlmacen"] == null ? 0 : Convert.ToInt32(dr["idAlmacen"].ToString()));
                        oAD_GuiaRemisionDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oAD_GuiaRemisionDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oAD_GuiaRemisionDTO.ProveedorDocumento = dr["ProveedorDocumento"] == null ? "" : dr["ProveedorDocumento"].ToString();
                        oAD_GuiaRemisionDTO.ProveedorDireccion = dr["ProveedorDireccion"] == null ? "" : dr["ProveedorDireccion"].ToString();
                        oAD_GuiaRemisionDTO.SerieGuia = dr["SerieGuia"] == null ? "" : dr["SerieGuia"].ToString();
                        oAD_GuiaRemisionDTO.NumeroGuia = dr["NumeroGuia"] == null ? "" : dr["NumeroGuia"].ToString();
                        oAD_GuiaRemisionDTO.NOrdenCompra = dr["NOrdenCompra"] == null ? "" : dr["NOrdenCompra"].ToString();
                        oAD_GuiaRemisionDTO.NDocRef = dr["NDocRef"] == null ? "" : dr["NDocRef"].ToString();
                        oAD_GuiaRemisionDTO.Ruc = dr["Ruc"] == null ? "" : dr["Ruc"].ToString();
                        oAD_GuiaRemisionDTO.FechaInicioTraslado = Convert.ToDateTime(dr["FechaInicioTraslado"].ToString());
                        oAD_GuiaRemisionDTO.PuntoPartida = dr["PuntoPartida"] == null ? "" : dr["PuntoPartida"].ToString();
                        oAD_GuiaRemisionDTO.PuntoLlegada = dr["PuntoLlegada"] == null ? "" : dr["PuntoLlegada"].ToString();
                        oAD_GuiaRemisionDTO.FechaFinTraslado = Convert.ToDateTime(dr["FechaFinTraslado"].ToString());
                        oAD_GuiaRemisionDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oAD_GuiaRemisionDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oAD_GuiaRemisionDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oAD_GuiaRemisionDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oAD_GuiaRemisionDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));

                        oResultDTO.ListaResultado.Add(oAD_GuiaRemisionDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<AD_GuiaRemisionDetalleDTO> lista = new List<AD_GuiaRemisionDetalleDTO>();
                            while (dr.Read())
                            {
                                AD_GuiaRemisionDetalleDTO oAD_GuiaRemisionDetalleDTO = new AD_GuiaRemisionDetalleDTO();
                                oAD_GuiaRemisionDetalleDTO.idGuiaRemisionDetalle = Convert.ToInt32(dr["idGuiaRemisionDetalle"].ToString());
                                oAD_GuiaRemisionDetalleDTO.idGuiaRemision = Convert.ToInt32(dr["idGuiaRemision"].ToString());
                                oAD_GuiaRemisionDetalleDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                                oAD_GuiaRemisionDetalleDTO.DescripcionArticulo = dr["descripcionArticulo"].ToString();
                                oAD_GuiaRemisionDetalleDTO.descUnidadMedida = dr["descUnidadMedida"].ToString();
                                oAD_GuiaRemisionDetalleDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                                oAD_GuiaRemisionDetalleDTO.idCategoria = Convert.ToInt32(dr["idCategoria"].ToString());
                                oAD_GuiaRemisionDetalleDTO.DescripcionCategoria = dr["descripcionCategoria"].ToString();
                                oAD_GuiaRemisionDetalleDTO.idDocumentoRef = Convert.ToInt32(dr["idDocumentoRef"].ToString());
                                oAD_GuiaRemisionDetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                                oAD_GuiaRemisionDetalleDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                                oAD_GuiaRemisionDetalleDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                                oAD_GuiaRemisionDetalleDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                                oAD_GuiaRemisionDetalleDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                                lista.Add(oAD_GuiaRemisionDetalleDTO);
                            }
                            oResultDTO.ListaResultado[0].oListaDetalle = lista;
                        }
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AD_GuiaRemisionDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_GuiaRemisionDTO> UpdateInsert(AD_GuiaRemisionDTO oAD_GuiaRemisionDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<AD_GuiaRemisionDTO> oResultDTO = new ResultDTO<AD_GuiaRemisionDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_AD_GuiaRemision_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idGuiaRemision", oAD_GuiaRemisionDTO.idGuiaRemision);
                        // da.SelectCommand.Parameters.AddWithValue("@idLocal", oAD_GuiaRemisionDTO.idLocal);
                        //da.SelectCommand.Parameters.AddWithValue("@idAlmacen", oAD_GuiaRemisionDTO.idAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oAD_GuiaRemisionDTO.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@idSocioNegocio", oAD_GuiaRemisionDTO.idSocioNegocio);
                        da.SelectCommand.Parameters.AddWithValue("@RazonSocial", oAD_GuiaRemisionDTO.RazonSocial);
                        da.SelectCommand.Parameters.AddWithValue("@ProveedorDocumento", oAD_GuiaRemisionDTO.ProveedorDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@ProveedorDireccion", oAD_GuiaRemisionDTO.ProveedorDireccion);
                        da.SelectCommand.Parameters.AddWithValue("@SerieGuia", oAD_GuiaRemisionDTO.SerieGuia);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroGuia", oAD_GuiaRemisionDTO.NumeroGuia);
                        da.SelectCommand.Parameters.AddWithValue("@NOrdenCompra", oAD_GuiaRemisionDTO.NOrdenCompra);
                        da.SelectCommand.Parameters.AddWithValue("@NDocRef", oAD_GuiaRemisionDTO.NDocRef);
                        da.SelectCommand.Parameters.AddWithValue("@Ruc", oAD_GuiaRemisionDTO.Ruc);
                        da.SelectCommand.Parameters.AddWithValue("@FechaInicioTraslado", oAD_GuiaRemisionDTO.FechaInicioTraslado);
                        da.SelectCommand.Parameters.AddWithValue("@PuntoPartida", oAD_GuiaRemisionDTO.PuntoPartida);
                        da.SelectCommand.Parameters.AddWithValue("@PuntoLlegada", oAD_GuiaRemisionDTO.PuntoLlegada);
                        da.SelectCommand.Parameters.AddWithValue("@FechaFinTraslado", oAD_GuiaRemisionDTO.FechaFinTraslado);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oAD_GuiaRemisionDTO.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oAD_GuiaRemisionDTO.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oAD_GuiaRemisionDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@ListaDetalle", oAD_GuiaRemisionDTO.cadDetalle);
                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarRangoFecha(oAD_GuiaRemisionDTO.idEmpresa, fechaInicio, fechaFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<AD_GuiaRemisionDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<AD_GuiaRemisionDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_GuiaRemisionDTO> Delete(AD_GuiaRemisionDTO oAD_GuiaRemisionDTO, DateTime fechaInicio, DateTime fechaFin, int idUsuario)
        {
            ResultDTO<AD_GuiaRemisionDTO> oResultDTO = new ResultDTO<AD_GuiaRemisionDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("[SP_AD_GuiaRemision_Delete]", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idGuiaRemision", oAD_GuiaRemisionDTO.idGuiaRemision);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarRangoFecha(oAD_GuiaRemisionDTO.idEmpresa, fechaInicio, fechaFin, cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oAD_GuiaRemisionDTO.idEmpresa, idUsuario,
                                "GUIA REMISION", "AD_GuiaRemision", oAD_GuiaRemisionDTO.idGuiaRemision, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<AD_GuiaRemisionDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<AD_GuiaRemisionDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_GuiaRemisionDTO> cargarGuias(int idEmpresa)
        {
            ResultDTO<AD_GuiaRemisionDTO> oResultDTO = new ResultDTO<AD_GuiaRemisionDTO>();
            oResultDTO.ListaResultado = new List<AD_GuiaRemisionDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_AD_GuiaRemision_ListarGuias", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AD_GuiaRemisionDTO oAD_GuiaRemisionDTO = new AD_GuiaRemisionDTO();
                        oAD_GuiaRemisionDTO.idGuiaRemision = Convert.ToInt32(dr["idGuiaRemision"].ToString());
                        oAD_GuiaRemisionDTO.RazonSocial = dr["RazonSocial"].ToString();
                        oAD_GuiaRemisionDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oAD_GuiaRemisionDTO.NumeroGuia = (dr["NumeroGuia"].ToString());
                        oResultDTO.ListaResultado.Add(oAD_GuiaRemisionDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AD_GuiaRemisionDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Entities.AD_GuiaRemisionDetalleDTO> cargarDetalleGuias(int idGuia)
        {
            ResultDTO<AD_GuiaRemisionDetalleDTO> oResultDTO = new ResultDTO<AD_GuiaRemisionDetalleDTO>();
            oResultDTO.ListaResultado = new List<AD_GuiaRemisionDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_AD_GuiaRemisionDetalle_ListarxIdGuias", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idGuia", idGuia);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AD_GuiaRemisionDetalleDTO oAD_GuiaRemisionDetalleDTO = new AD_GuiaRemisionDetalleDTO();
                        oAD_GuiaRemisionDetalleDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                        oAD_GuiaRemisionDetalleDTO.DescripcionArticulo = dr["DescripcionArticulo"].ToString();
                        oAD_GuiaRemisionDetalleDTO.Marca = dr["Marca"].ToString();
                        oAD_GuiaRemisionDetalleDTO.descUnidadMedida = (dr["UnidadMedida"].ToString());
                        oAD_GuiaRemisionDetalleDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                        oAD_GuiaRemisionDetalleDTO.idGuiaRemisionDetalle = Convert.ToInt32(dr["idGuiaRemisionDetalle"].ToString());
                        oResultDTO.ListaResultado.Add(oAD_GuiaRemisionDetalleDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AD_GuiaRemisionDetalleDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
