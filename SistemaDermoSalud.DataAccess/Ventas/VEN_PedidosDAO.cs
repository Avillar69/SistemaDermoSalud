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
    public class VEN_PedidosDAO
    {
        public ResultDTO<VEN_PedidosDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, SqlConnection cn = null)
        {
            ResultDTO<VEN_PedidosDTO> oResultDTO = new ResultDTO<VEN_PedidosDTO>();
            oResultDTO.ListaResultado = new List<VEN_PedidosDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Pedido_ListarxFecha", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_PedidosDTO oVEN_PedidosDTO = new VEN_PedidosDTO();
                        oVEN_PedidosDTO.idPedido = Convert.ToInt32(dr["idPedido"] == null ? 0 : Convert.ToInt32(dr["idPedido"].ToString()));
                        oVEN_PedidosDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oVEN_PedidosDTO.idTipoCompra = Convert.ToInt32(dr["idTipoCompra"] == null ? 0 : Convert.ToInt32(dr["idTipoCompra"].ToString()));
                        oVEN_PedidosDTO.EstadoPedido = Convert.ToInt32(dr["EstadoPedido"] == null ? 0 : Convert.ToInt32(dr["EstadoPedido"].ToString()));
                        oVEN_PedidosDTO.NumPedido = dr["NumPedido"].ToString();
                        oVEN_PedidosDTO.FechaOrdenCompra = Convert.ToDateTime(dr["FechaOrdenCompra"].ToString());
                        oVEN_PedidosDTO.FechaEntrega = Convert.ToDateTime(dr["FechaEntrega"].ToString());
                        oVEN_PedidosDTO.idProveedor = Convert.ToInt32(dr["idProveedor"] == null ? 0 : Convert.ToInt32(dr["idProveedor"].ToString()));
                        oVEN_PedidosDTO.ProveedorRazon = dr["ProveedorRazon"] == null ? "" : dr["ProveedorRazon"].ToString();
                        oVEN_PedidosDTO.ProveedorDocumento = dr["ProveedorDocumento"] == null ? "" : dr["ProveedorDocumento"].ToString();
                        oVEN_PedidosDTO.ProveedorDireccion = dr["ProveedorDireccion"] == null ? "" : dr["ProveedorDireccion"].ToString();
                        oVEN_PedidosDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oVEN_PedidosDTO.EstadoOC = dr["EstadoOC"] == null ? "" : dr["EstadoOC"].ToString();

                        oVEN_PedidosDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
                        oVEN_PedidosDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
                        oVEN_PedidosDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
                        oVEN_PedidosDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
                        oVEN_PedidosDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjero"].ToString()));
                        oVEN_PedidosDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oVEN_PedidosDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));



                        oVEN_PedidosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_PedidosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_PedidosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oVEN_PedidosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oVEN_PedidosDTO.Impreso = dr["Impreso"] == null ? "" : dr["Impreso"].ToString();
                        oVEN_PedidosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oVEN_PedidosDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_PedidosDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_PedidosDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<VEN_PedidosDTO> oResultDTO = new ResultDTO<VEN_PedidosDTO>();
            oResultDTO.ListaResultado = new List<VEN_PedidosDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Pedido_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_PedidosDTO oVEN_PedidosDTO = new VEN_PedidosDTO();
                        oVEN_PedidosDTO.idPedido = Convert.ToInt32(dr["idPedido"] == null ? 0 : Convert.ToInt32(dr["idPedido"].ToString()));
                        oVEN_PedidosDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        // oCOM_OrdenCompraDTO.idLocal = Convert.ToInt32(dr["idLocal"] == null ? 0 : Convert.ToInt32(dr["idLocal"].ToString()));
                        oVEN_PedidosDTO.idTipoCompra = Convert.ToInt32(dr["idTipoCompra"] == null ? 0 : Convert.ToInt32(dr["idTipoCompra"].ToString()));
                        oVEN_PedidosDTO.EstadoPedido = Convert.ToInt32(dr["EstadoPedido"] == null ? 0 : Convert.ToInt32(dr["EstadoPedido"].ToString()));
                        oVEN_PedidosDTO.NumPedido = dr["NumPedido"].ToString();
                        oVEN_PedidosDTO.FechaOrdenCompra = Convert.ToDateTime(dr["FechaOrdenCompra"].ToString());
                        oVEN_PedidosDTO.FechaEntrega = Convert.ToDateTime(dr["FechaEntrega"].ToString());
                        oVEN_PedidosDTO.idProveedor = Convert.ToInt32(dr["idProveedor"] == null ? 0 : Convert.ToInt32(dr["idProveedor"].ToString()));
                        oVEN_PedidosDTO.ProveedorRazon = dr["ProveedorRazon"] == null ? "" : dr["ProveedorRazon"].ToString();
                        oVEN_PedidosDTO.ProveedorDocumento = dr["ProveedorDocumento"] == null ? "" : dr["ProveedorDocumento"].ToString();
                        oVEN_PedidosDTO.ProveedorDireccion = dr["ProveedorDireccion"] == null ? "" : dr["ProveedorDireccion"].ToString();
                        oVEN_PedidosDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oVEN_PedidosDTO.EstadoOC = dr["EstadoOC"] == null ? "" : dr["EstadoOC"].ToString();

                        oVEN_PedidosDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
                        oVEN_PedidosDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
                        oVEN_PedidosDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
                        oVEN_PedidosDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
                        oVEN_PedidosDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjero"].ToString()));
                        oVEN_PedidosDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oVEN_PedidosDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));



                        oVEN_PedidosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_PedidosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_PedidosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oVEN_PedidosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oVEN_PedidosDTO.Impreso = dr["Impreso"] == null ? "" : dr["Impreso"].ToString();
                        oVEN_PedidosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oVEN_PedidosDTO);
                    }

                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_PedidosDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_PedidosDTO> ListarxID(int idPedido)
        {
            ResultDTO<VEN_PedidosDTO> oResultDTO = new ResultDTO<VEN_PedidosDTO>();
            oResultDTO.ListaResultado = new List<VEN_PedidosDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Pedido_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idPedido", idPedido);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_PedidosDTO oVEN_PedidosDTO = new VEN_PedidosDTO();
                        oVEN_PedidosDTO.idPedido = Convert.ToInt32(dr["idPedido"] == null ? 0 : Convert.ToInt32(dr["idPedido"].ToString()));
                        oVEN_PedidosDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oVEN_PedidosDTO.idTipoCompra = Convert.ToInt32(dr["idTipoCompra"] == null ? 0 : Convert.ToInt32(dr["idTipoCompra"].ToString()));
                        oVEN_PedidosDTO.EstadoPedido = Convert.ToInt32(dr["EstadoPedido"] == null ? 0 : Convert.ToInt32(dr["EstadoPedido"].ToString()));
                        oVEN_PedidosDTO.NumPedido = dr["NumPedido"].ToString();
                        oVEN_PedidosDTO.FechaOrdenCompra = Convert.ToDateTime(dr["FechaOrdenCompra"].ToString());
                        oVEN_PedidosDTO.FechaEntrega = Convert.ToDateTime(dr["FechaEntrega"].ToString());
                        oVEN_PedidosDTO.idProveedor = Convert.ToInt32(dr["idProveedor"] == null ? 0 : Convert.ToInt32(dr["idProveedor"].ToString()));
                        oVEN_PedidosDTO.ProveedorRazon = dr["ProveedorRazon"] == null ? "" : dr["ProveedorRazon"].ToString();
                        oVEN_PedidosDTO.ProveedorDocumento = dr["ProveedorDocumento"] == null ? "" : dr["ProveedorDocumento"].ToString();
                        oVEN_PedidosDTO.ProveedorDireccion = dr["ProveedorDireccion"] == null ? "" : dr["ProveedorDireccion"].ToString();
                        oVEN_PedidosDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oVEN_PedidosDTO.EstadoOC = dr["EstadoOC"] == null ? "" : dr["EstadoOC"].ToString();

                        oVEN_PedidosDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
                        oVEN_PedidosDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
                        oVEN_PedidosDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
                        oVEN_PedidosDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
                        oVEN_PedidosDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjero"].ToString()));
                        oVEN_PedidosDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oVEN_PedidosDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));

                        oVEN_PedidosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_PedidosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_PedidosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oVEN_PedidosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oVEN_PedidosDTO.Impreso = dr["Impreso"] == null ? "" : dr["Impreso"].ToString();
                        oVEN_PedidosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));////////////
                        oVEN_PedidosDTO.IGVcheck = Convert.ToBoolean(dr["IGVcheck"] == null ? false : Convert.ToBoolean(dr["IGVcheck"].ToString()));
                        oVEN_PedidosDTO.Observacion = dr["Observacion"] == null ? "" : dr["Observacion"].ToString();
                        oVEN_PedidosDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"] == null ? 0 : Convert.ToDecimal(dr["PorcDescuento"].ToString()));


                        oResultDTO.ListaResultado.Add(oVEN_PedidosDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<VEN_PedidosDetalleDTO> lista = new List<VEN_PedidosDetalleDTO>();
                            while (dr.Read())
                            {
                                VEN_PedidosDetalleDTO oVEN_PedidoDetalleDTO = new VEN_PedidosDetalleDTO();
                                oVEN_PedidoDetalleDTO.idPedidoDetalle = Convert.ToInt32(dr["idPedidoDetalle"].ToString());
                                oVEN_PedidoDetalleDTO.idPedido = Convert.ToInt32(dr["idPedido"].ToString());
                                oVEN_PedidoDetalleDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                                oVEN_PedidoDetalleDTO.DescripcionArticulo = dr["DescripcionArticulo"].ToString();
                                oVEN_PedidoDetalleDTO.idCategoria = Convert.ToInt32(dr["idCategoria"].ToString());
                                oVEN_PedidoDetalleDTO.DescripcionCategoria = dr["DescripcionCategoria"].ToString();
                                oVEN_PedidoDetalleDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                                oVEN_PedidoDetalleDTO.CantidadAprobada = Convert.ToDecimal(dr["CantidadAprobada"].ToString());
                                oVEN_PedidoDetalleDTO.CantidadRechazada = Convert.ToDecimal(dr["CantidadRechazada"].ToString());
                                oVEN_PedidoDetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                                oVEN_PedidoDetalleDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                                oVEN_PedidoDetalleDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                                oVEN_PedidoDetalleDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                                oVEN_PedidoDetalleDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                                oVEN_PedidoDetalleDTO.EstadoAprobacion = Convert.ToBoolean(dr["EstadoAprobacion"].ToString());
                                oVEN_PedidoDetalleDTO.descUnidadMedida = dr["descUnidadMedida"].ToString();
                                lista.Add(oVEN_PedidoDetalleDTO);
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
                    oResultDTO.ListaResultado = new List<VEN_PedidosDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<VEN_PedidosDTO> UpdateInsert(VEN_PedidosDTO oCOM_OrdenCompra, DateTime FechaInicio, DateTime FechaFin)
        {
            ResultDTO<VEN_PedidosDTO> oResultDTO = new ResultDTO<VEN_PedidosDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Pedido_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idPedido", oCOM_OrdenCompra.idPedido);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oCOM_OrdenCompra.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoCompra", oCOM_OrdenCompra.idTipoCompra);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoPedido", oCOM_OrdenCompra.EstadoPedido);
                        da.SelectCommand.Parameters.AddWithValue("@NumPedido", oCOM_OrdenCompra.NumPedido);
                        da.SelectCommand.Parameters.AddWithValue("@FechaOrdenCompra", oCOM_OrdenCompra.FechaOrdenCompra);
                        da.SelectCommand.Parameters.AddWithValue("@FechaEntrega", oCOM_OrdenCompra.FechaEntrega);
                        da.SelectCommand.Parameters.AddWithValue("@idProveedor", oCOM_OrdenCompra.idProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@ProveedorRazon", oCOM_OrdenCompra.ProveedorRazon);
                        da.SelectCommand.Parameters.AddWithValue("@ProveedorDoc", oCOM_OrdenCompra.ProveedorDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@ProveedorDir", oCOM_OrdenCompra.ProveedorDireccion);
                        da.SelectCommand.Parameters.AddWithValue("@idMoneda", oCOM_OrdenCompra.idMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoOC", oCOM_OrdenCompra.EstadoOC);
                        da.SelectCommand.Parameters.AddWithValue("@SubTotalNacional", oCOM_OrdenCompra.SubTotalNacional);
                        da.SelectCommand.Parameters.AddWithValue("@SubTotalExtranjero", oCOM_OrdenCompra.SubTotalExtranjero);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambio", oCOM_OrdenCompra.TipoCambio);
                        da.SelectCommand.Parameters.AddWithValue("@IGVNacional", oCOM_OrdenCompra.IGVNacional);
                        da.SelectCommand.Parameters.AddWithValue("@IGVExtranjero", oCOM_OrdenCompra.IGVExtranjero);
                        da.SelectCommand.Parameters.AddWithValue("@TotalNacional", oCOM_OrdenCompra.TotalNacional);
                        da.SelectCommand.Parameters.AddWithValue("@TotalExtranjero", oCOM_OrdenCompra.TotalExtranjero);
                        da.SelectCommand.Parameters.AddWithValue("@Usuario_reg", oCOM_OrdenCompra.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@Usuario_mod", oCOM_OrdenCompra.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Impreso", oCOM_OrdenCompra.Impreso);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oCOM_OrdenCompra.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@IGVcheck", oCOM_OrdenCompra.IGVcheck);
                        da.SelectCommand.Parameters.AddWithValue("@Observacion", oCOM_OrdenCompra.Observacion);
                        da.SelectCommand.Parameters.AddWithValue("@PorcDescuento", oCOM_OrdenCompra.PorcDescuento);
                        da.SelectCommand.Parameters.AddWithValue("@ListaDetalle", oCOM_OrdenCompra.cadDetalle);
                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarRangoFecha(oCOM_OrdenCompra.idEmpresa, FechaInicio, FechaFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<VEN_PedidosDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<VEN_PedidosDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_PedidosDTO> Delete(VEN_PedidosDTO oVEN_Pedido, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<VEN_PedidosDTO> oResultDTO = new ResultDTO<VEN_PedidosDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Pedido_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idPedido", oVEN_Pedido.idPedido);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarRangoFecha(oVEN_Pedido.idEmpresa, fechaInicio, fechaFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<VEN_PedidosDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<VEN_PedidosDTO>();
                    }
                }
            }
            return oResultDTO;
        }

        /*--------------------------------------------------------------------*/

        public ResultDTO<VEN_PedidosDTO> ListarOrdenCompra(int Pedido, SqlConnection cn = null)
        {
            ResultDTO<VEN_PedidosDTO> oResultDTO = new ResultDTO<VEN_PedidosDTO>();
            oResultDTO.ListaResultado = new List<VEN_PedidosDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_PedidoEstado", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EstadoPedido", Pedido);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_PedidosDTO oVEN_PedidosDTO = new VEN_PedidosDTO();
                        oVEN_PedidosDTO.idPedido = Convert.ToInt32(dr["idPedido"] == null ? 0 : Convert.ToInt32(dr["idPedido"].ToString()));
                        oVEN_PedidosDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oVEN_PedidosDTO.idTipoCompra = Convert.ToInt32(dr["idTipoCompra"] == null ? 0 : Convert.ToInt32(dr["idTipoCompra"].ToString()));
                        oVEN_PedidosDTO.EstadoPedido = Convert.ToInt32(dr["EstadoPedido"] == null ? 0 : Convert.ToInt32(dr["EstadoPedido"].ToString()));
                        oVEN_PedidosDTO.NumPedido = dr["NumPedido"].ToString();
                        oVEN_PedidosDTO.FechaOrdenCompra = Convert.ToDateTime(dr["FechaOrdenCompra"].ToString());
                        oVEN_PedidosDTO.FechaEntrega = Convert.ToDateTime(dr["FechaEntrega"].ToString());
                        oVEN_PedidosDTO.idProveedor = Convert.ToInt32(dr["idProveedor"] == null ? 0 : Convert.ToInt32(dr["idProveedor"].ToString()));
                        oVEN_PedidosDTO.ProveedorRazon = dr["ProveedorRazon"] == null ? "" : dr["ProveedorRazon"].ToString();
                        oVEN_PedidosDTO.ProveedorDocumento = dr["ProveedorDocumento"] == null ? "" : dr["ProveedorDocumento"].ToString();
                        oVEN_PedidosDTO.ProveedorDireccion = dr["ProveedorDireccion"] == null ? "" : dr["ProveedorDireccion"].ToString();
                        oVEN_PedidosDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oVEN_PedidosDTO.EstadoOC = dr["EstadoOC"] == null ? "" : dr["EstadoOC"].ToString();
                        oVEN_PedidosDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
                        oVEN_PedidosDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
                        oVEN_PedidosDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
                        oVEN_PedidosDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
                        oVEN_PedidosDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjero"].ToString()));
                        oVEN_PedidosDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oVEN_PedidosDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));

                        oVEN_PedidosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_PedidosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_PedidosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oVEN_PedidosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oVEN_PedidosDTO.Impreso = dr["Impreso"] == null ? "" : dr["Impreso"].ToString();
                        oVEN_PedidosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oVEN_PedidosDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_PedidosDTO>();
                }
            }
            return oResultDTO;
        }


    }
}
