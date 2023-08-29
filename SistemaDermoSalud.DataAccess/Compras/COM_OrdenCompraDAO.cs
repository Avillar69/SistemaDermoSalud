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
   public  class COM_OrdenCompraDAO
    {

        public ResultDTO<COM_OrdenCompraDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, SqlConnection cn = null)
        {
            ResultDTO<COM_OrdenCompraDTO> oResultDTO = new ResultDTO<COM_OrdenCompraDTO>();
            oResultDTO.ListaResultado = new List<COM_OrdenCompraDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_OrdenCompra_ListarxFecha", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_OrdenCompraDTO oCOM_OrdenCompraDTO = new COM_OrdenCompraDTO();
                        oCOM_OrdenCompraDTO.idOrdenCompra = Convert.ToInt32(dr["idOrdenCompra"] == null ? 0 : Convert.ToInt32(dr["idOrdenCompra"].ToString()));
                        oCOM_OrdenCompraDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oCOM_OrdenCompraDTO.idTipoCompra = Convert.ToInt32(dr["idTipoCompra"] == null ? 0 : Convert.ToInt32(dr["idTipoCompra"].ToString()));
                        oCOM_OrdenCompraDTO.NumOrdenCompra = dr["NumOrdenCompra"] == null ? "" : dr["NumOrdenCompra"].ToString();
                        oCOM_OrdenCompraDTO.EstadoOrdenCompra = Convert.ToInt32(dr["EstadoOrdenCompra"] == null ? 0 : Convert.ToInt32(dr["EstadoOrdenCompra"].ToString()));
                        oCOM_OrdenCompraDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oCOM_OrdenCompraDTO.idPedido = Convert.ToInt32(dr["idPedido"] == null ? 0 : Convert.ToInt32(dr["idPedido"].ToString()));
                        oCOM_OrdenCompraDTO.NumPedido = dr["NumPedido"].ToString();
                        oCOM_OrdenCompraDTO.FechaOrdenCompra = Convert.ToDateTime(dr["FechaOrdenCompra"].ToString());
                        oCOM_OrdenCompraDTO.FechaEntrega = Convert.ToDateTime(dr["FechaEntrega"].ToString());
                        oCOM_OrdenCompraDTO.idProveedor = Convert.ToInt32(dr["idProveedor"] == null ? 0 : Convert.ToInt32(dr["idProveedor"].ToString()));
                        oCOM_OrdenCompraDTO.ProveedorRazon = dr["ProveedorRazon"] == null ? "" : dr["ProveedorRazon"].ToString();
                        oCOM_OrdenCompraDTO.ProveedorDocumento = dr["ProveedorDocumento"] == null ? "" : dr["ProveedorDocumento"].ToString();
                        oCOM_OrdenCompraDTO.ProveedorDireccion = dr["ProveedorDireccion"] == null ? "" : dr["ProveedorDireccion"].ToString();
                        oCOM_OrdenCompraDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oCOM_OrdenCompraDTO.EstadoOC = dr["EstadoOC"] == null ? "" : dr["EstadoOC"].ToString();

                        oCOM_OrdenCompraDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
                        oCOM_OrdenCompraDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
                        oCOM_OrdenCompraDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
                        oCOM_OrdenCompraDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
                        oCOM_OrdenCompraDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjero"].ToString()));
                        oCOM_OrdenCompraDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oCOM_OrdenCompraDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));



                        oCOM_OrdenCompraDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCOM_OrdenCompraDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCOM_OrdenCompraDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oCOM_OrdenCompraDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oCOM_OrdenCompraDTO.Impreso = dr["Impreso"] == null ? "" : dr["Impreso"].ToString();
                        oCOM_OrdenCompraDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oCOM_OrdenCompraDTO.IGVcheck = Convert.ToBoolean(dr["IGVcheck"] == null ? false : Convert.ToBoolean(dr["IGVcheck"].ToString()));
                        oCOM_OrdenCompraDTO.Observacion = dr["Observacion"] == null ? "" : dr["Observacion"].ToString();
                        oCOM_OrdenCompraDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"] == null ? 0 : Convert.ToDecimal(dr["PorcDescuento"].ToString()));

                        oResultDTO.ListaResultado.Add(oCOM_OrdenCompraDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_OrdenCompraDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_OrdenCompraDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<COM_OrdenCompraDTO> oResultDTO = new ResultDTO<COM_OrdenCompraDTO>();
            oResultDTO.ListaResultado = new List<COM_OrdenCompraDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_OrdenCompra_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_OrdenCompraDTO oCOM_OrdenCompraDTO = new COM_OrdenCompraDTO();
                        oCOM_OrdenCompraDTO.idOrdenCompra = Convert.ToInt32(dr["idOrdenCompra"] == null ? 0 : Convert.ToInt32(dr["idOrdenCompra"].ToString()));
                        oCOM_OrdenCompraDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        // oCOM_OrdenCompraDTO.idLocal = Convert.ToInt32(dr["idLocal"] == null ? 0 : Convert.ToInt32(dr["idLocal"].ToString()));
                        oCOM_OrdenCompraDTO.idTipoCompra = Convert.ToInt32(dr["idTipoCompra"] == null ? 0 : Convert.ToInt32(dr["idTipoCompra"].ToString()));
                        oCOM_OrdenCompraDTO.NumOrdenCompra = dr["NumOrdenCompra"] == null ? "" : dr["NumOrdenCompra"].ToString();
                        oCOM_OrdenCompraDTO.EstadoOrdenCompra = Convert.ToInt32(dr["EstadoOrdenCompra"] == null ? 0 : Convert.ToInt32(dr["EstadoOrdenCompra"].ToString()));
                        oCOM_OrdenCompraDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oCOM_OrdenCompraDTO.idPedido = Convert.ToInt32(dr["idPedido"] == null ? 0 : Convert.ToInt32(dr["idPedido"].ToString()));
                        oCOM_OrdenCompraDTO.NumPedido = dr["NumPedido"].ToString();
                        oCOM_OrdenCompraDTO.FechaOrdenCompra = Convert.ToDateTime(dr["FechaOrdenCompra"].ToString());
                        oCOM_OrdenCompraDTO.FechaEntrega = Convert.ToDateTime(dr["FechaEntrega"].ToString());
                        oCOM_OrdenCompraDTO.idProveedor = Convert.ToInt32(dr["idProveedor"] == null ? 0 : Convert.ToInt32(dr["idProveedor"].ToString()));
                        oCOM_OrdenCompraDTO.ProveedorRazon = dr["ProveedorRazon"] == null ? "" : dr["ProveedorRazon"].ToString();
                        oCOM_OrdenCompraDTO.ProveedorDocumento = dr["ProveedorDocumento"] == null ? "" : dr["ProveedorDocumento"].ToString();
                        oCOM_OrdenCompraDTO.ProveedorDireccion = dr["ProveedorDireccion"] == null ? "" : dr["ProveedorDireccion"].ToString();
                        oCOM_OrdenCompraDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oCOM_OrdenCompraDTO.EstadoOC = dr["EstadoOC"] == null ? "" : dr["EstadoOC"].ToString();

                        oCOM_OrdenCompraDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
                        oCOM_OrdenCompraDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
                        oCOM_OrdenCompraDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
                        oCOM_OrdenCompraDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
                        oCOM_OrdenCompraDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjero"].ToString()));
                        oCOM_OrdenCompraDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oCOM_OrdenCompraDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));



                        oCOM_OrdenCompraDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCOM_OrdenCompraDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCOM_OrdenCompraDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oCOM_OrdenCompraDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oCOM_OrdenCompraDTO.Impreso = dr["Impreso"] == null ? "" : dr["Impreso"].ToString();
                        oCOM_OrdenCompraDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oCOM_OrdenCompraDTO.IGVcheck = Convert.ToBoolean(dr["IGVcheck"] == null ? false : Convert.ToBoolean(dr["IGVcheck"].ToString()));

                        oCOM_OrdenCompraDTO.Observacion = dr["Observacion"] == null ? "" : dr["Observacion"].ToString();
                        oCOM_OrdenCompraDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"] == null ? 0 : Convert.ToDecimal(dr["PorcDescuento"].ToString()));
                        oResultDTO.ListaResultado.Add(oCOM_OrdenCompraDTO);
                    }

                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_OrdenCompraDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_OrdenCompraDTO> ListarxID(int idOrdenCompra)
        {
            ResultDTO<COM_OrdenCompraDTO> oResultDTO = new ResultDTO<COM_OrdenCompraDTO>();
            oResultDTO.ListaResultado = new List<COM_OrdenCompraDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_OrdenCompra_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idOrdenCompra", idOrdenCompra);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_OrdenCompraDTO oCOM_OrdenCompraDTO = new COM_OrdenCompraDTO();
                        oCOM_OrdenCompraDTO.idOrdenCompra = Convert.ToInt32(dr["idOrdenCompra"] == null ? 0 : Convert.ToInt32(dr["idOrdenCompra"].ToString()));
                        oCOM_OrdenCompraDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        // oCOM_OrdenCompraDTO.idLocal = Convert.ToInt32(dr["idLocal"] == null ? 0 : Convert.ToInt32(dr["idLocal"].ToString()));
                        oCOM_OrdenCompraDTO.idTipoCompra = Convert.ToInt32(dr["idTipoCompra"] == null ? 0 : Convert.ToInt32(dr["idTipoCompra"].ToString()));
                        oCOM_OrdenCompraDTO.NumOrdenCompra = dr["NumOrdenCompra"] == null ? "" : dr["NumOrdenCompra"].ToString();
                        oCOM_OrdenCompraDTO.EstadoOrdenCompra = Convert.ToInt32(dr["EstadoOrdenCompra"] == null ? 0 : Convert.ToInt32(dr["EstadoOrdenCompra"].ToString()));
                        oCOM_OrdenCompraDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oCOM_OrdenCompraDTO.idPedido = Convert.ToInt32(dr["idPedido"] == null ? 0 : Convert.ToInt32(dr["idPedido"].ToString()));
                        oCOM_OrdenCompraDTO.NumPedido = dr["NumPedido"].ToString();
                        oCOM_OrdenCompraDTO.FechaOrdenCompra = Convert.ToDateTime(dr["FechaOrdenCompra"].ToString());
                        oCOM_OrdenCompraDTO.FechaEntrega = Convert.ToDateTime(dr["FechaEntrega"].ToString());
                        oCOM_OrdenCompraDTO.idProveedor = Convert.ToInt32(dr["idProveedor"] == null ? 0 : Convert.ToInt32(dr["idProveedor"].ToString()));
                        oCOM_OrdenCompraDTO.ProveedorRazon = dr["ProveedorRazon"] == null ? "" : dr["ProveedorRazon"].ToString();
                        oCOM_OrdenCompraDTO.ProveedorDocumento = dr["ProveedorDocumento"] == null ? "" : dr["ProveedorDocumento"].ToString();
                        oCOM_OrdenCompraDTO.ProveedorDireccion = dr["ProveedorDireccion"] == null ? "" : dr["ProveedorDireccion"].ToString();
                        oCOM_OrdenCompraDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oCOM_OrdenCompraDTO.EstadoOC = dr["EstadoOC"] == null ? "" : dr["EstadoOC"].ToString();

                        oCOM_OrdenCompraDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
                        oCOM_OrdenCompraDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
                        oCOM_OrdenCompraDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
                        oCOM_OrdenCompraDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
                        oCOM_OrdenCompraDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjero"].ToString()));
                        oCOM_OrdenCompraDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oCOM_OrdenCompraDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));

                        oCOM_OrdenCompraDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCOM_OrdenCompraDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCOM_OrdenCompraDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oCOM_OrdenCompraDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oCOM_OrdenCompraDTO.Impreso = dr["Impreso"] == null ? "" : dr["Impreso"].ToString();
                        oCOM_OrdenCompraDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oCOM_OrdenCompraDTO.IGVcheck = Convert.ToBoolean(dr["IGVcheck"] == null ? false : Convert.ToBoolean(dr["IGVcheck"].ToString()));

                        oCOM_OrdenCompraDTO.Observacion = dr["Observacion"] == null ? "" : dr["Observacion"].ToString();
                        oCOM_OrdenCompraDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"] == null ? 0 : Convert.ToDecimal(dr["PorcDescuento"].ToString()));
                        oCOM_OrdenCompraDTO.EstadoAprobacion = Convert.ToInt32(dr["EstadoAprobacion"] == null ? 0 : Convert.ToInt32(dr["EstadoAprobacion"].ToString()));
                        oResultDTO.ListaResultado.Add(oCOM_OrdenCompraDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<COM_OrdenCompra_DetalleDTO> lista = new List<COM_OrdenCompra_DetalleDTO>();
                            while (dr.Read())
                            {
                                COM_OrdenCompra_DetalleDTO oCOM_OrdenCompraDTO = new COM_OrdenCompra_DetalleDTO();
                                oCOM_OrdenCompraDTO.idOrdenCompraDetalle = Convert.ToInt32(dr["idOrdenCompraDetalle"].ToString());
                                oCOM_OrdenCompraDTO.IdOrdenCompra = Convert.ToInt32(dr["IdOrdenCompra"].ToString());
                                oCOM_OrdenCompraDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                                oCOM_OrdenCompraDTO.DescripcionArticulo = dr["DescripcionArticulo"].ToString();
                                oCOM_OrdenCompraDTO.idCategoria = Convert.ToInt32(dr["idCategoria"].ToString());
                                oCOM_OrdenCompraDTO.DescripcionCategoria = dr["DescripcionCategoria"].ToString();
                                oCOM_OrdenCompraDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                                oCOM_OrdenCompraDTO.PrecioNacional = Convert.ToDecimal(dr["PrecioNacional"].ToString());
                                oCOM_OrdenCompraDTO.PrecioExtranjero = Convert.ToDecimal(dr["PrecioExtranjero"].ToString());
                                oCOM_OrdenCompraDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                                oCOM_OrdenCompraDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"].ToString());
                                oCOM_OrdenCompraDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                                oCOM_OrdenCompraDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                                oCOM_OrdenCompraDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                                oCOM_OrdenCompraDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                                oCOM_OrdenCompraDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                                lista.Add(oCOM_OrdenCompraDTO);
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
                    oResultDTO.ListaResultado = new List<COM_OrdenCompraDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_OrdenCompraDTO> UpdateInsert(COM_OrdenCompraDTO oCOM_OrdenCompra, DateTime FechaInicio, DateTime FechaFin)
        {
            ResultDTO<COM_OrdenCompraDTO> oResultDTO = new ResultDTO<COM_OrdenCompraDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_COM_OrdenCompra_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idOrdenCompra", oCOM_OrdenCompra.idOrdenCompra);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oCOM_OrdenCompra.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoCompra", oCOM_OrdenCompra.idTipoCompra);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoOrdenCompra", oCOM_OrdenCompra.EstadoOrdenCompra);
                        da.SelectCommand.Parameters.AddWithValue("@idFormaPago", oCOM_OrdenCompra.idFormaPago);
                        da.SelectCommand.Parameters.AddWithValue("@idpedido", oCOM_OrdenCompra.idPedido);// id requerimiento
                        da.SelectCommand.Parameters.AddWithValue("@numpedido", oCOM_OrdenCompra.NumPedido);
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
                        da.SelectCommand.Parameters.AddWithValue("@EstadoAprobacion", oCOM_OrdenCompra.EstadoAprobacion);
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
                            oResultDTO.ListaResultado = new List<COM_OrdenCompraDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<COM_OrdenCompraDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_OrdenCompraDTO> Delete(COM_OrdenCompraDTO oCOM_OrdenCompra, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<COM_OrdenCompraDTO> oResultDTO = new ResultDTO<COM_OrdenCompraDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_COM_OrdenCompra_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idOrdenCompra", oCOM_OrdenCompra.idOrdenCompra);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarRangoFecha(oCOM_OrdenCompra.idEmpresa, fechaInicio, fechaFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<COM_OrdenCompraDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<COM_OrdenCompraDTO>();
                    }
                }
            }
            return oResultDTO;
        }

        /*--------------------------------------------------------------------*/

        public ResultDTO<COM_OrdenCompraDTO> ListarOrdenCompra(int OrdenCompra, SqlConnection cn = null)
        {
            ResultDTO<COM_OrdenCompraDTO> oResultDTO = new ResultDTO<COM_OrdenCompraDTO>();
            oResultDTO.ListaResultado = new List<COM_OrdenCompraDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_OrdenCompraEstado", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EstadoOrdenCompra", OrdenCompra);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_OrdenCompraDTO oCOM_OrdenCompraDTO = new COM_OrdenCompraDTO();
                        oCOM_OrdenCompraDTO.idOrdenCompra = Convert.ToInt32(dr["idOrdenCompra"] == null ? 0 : Convert.ToInt32(dr["idOrdenCompra"].ToString()));
                        oCOM_OrdenCompraDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oCOM_OrdenCompraDTO.idTipoCompra = Convert.ToInt32(dr["idTipoCompra"] == null ? 0 : Convert.ToInt32(dr["idTipoCompra"].ToString()));
                        oCOM_OrdenCompraDTO.NumOrdenCompra = dr["NumOrdenCompra"] == null ? "" : dr["NumOrdenCompra"].ToString();
                        oCOM_OrdenCompraDTO.EstadoOrdenCompra = Convert.ToInt32(dr["EstadoOrdenCompra"] == null ? 0 : Convert.ToInt32(dr["EstadoOrdenCompra"].ToString()));
                        oCOM_OrdenCompraDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oCOM_OrdenCompraDTO.idPedido = Convert.ToInt32(dr["idPedido"] == null ? 0 : Convert.ToInt32(dr["idPedido"].ToString()));
                        oCOM_OrdenCompraDTO.NumPedido = dr["NumPedido"].ToString();
                        oCOM_OrdenCompraDTO.FechaOrdenCompra = Convert.ToDateTime(dr["FechaOrdenCompra"].ToString());
                        oCOM_OrdenCompraDTO.FechaEntrega = Convert.ToDateTime(dr["FechaEntrega"].ToString());
                        oCOM_OrdenCompraDTO.idProveedor = Convert.ToInt32(dr["idProveedor"] == null ? 0 : Convert.ToInt32(dr["idProveedor"].ToString()));
                        oCOM_OrdenCompraDTO.ProveedorRazon = dr["ProveedorRazon"] == null ? "" : dr["ProveedorRazon"].ToString();
                        oCOM_OrdenCompraDTO.ProveedorDocumento = dr["ProveedorDocumento"] == null ? "" : dr["ProveedorDocumento"].ToString();
                        oCOM_OrdenCompraDTO.ProveedorDireccion = dr["ProveedorDireccion"] == null ? "" : dr["ProveedorDireccion"].ToString();
                        oCOM_OrdenCompraDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oCOM_OrdenCompraDTO.EstadoOC = dr["EstadoOC"] == null ? "" : dr["EstadoOC"].ToString();
                        oCOM_OrdenCompraDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
                        oCOM_OrdenCompraDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
                        oCOM_OrdenCompraDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
                        oCOM_OrdenCompraDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
                        oCOM_OrdenCompraDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjero"].ToString()));
                        oCOM_OrdenCompraDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oCOM_OrdenCompraDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));

                        oCOM_OrdenCompraDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCOM_OrdenCompraDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCOM_OrdenCompraDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oCOM_OrdenCompraDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oCOM_OrdenCompraDTO.Impreso = dr["Impreso"] == null ? "" : dr["Impreso"].ToString();
                        oCOM_OrdenCompraDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oCOM_OrdenCompraDTO.IGVcheck = Convert.ToBoolean(dr["IGVcheck"] == null ? false : Convert.ToBoolean(dr["IGVcheck"].ToString()));

                        oCOM_OrdenCompraDTO.Observacion = dr["Observacion"] == null ? "" : dr["Observacion"].ToString();
                        oCOM_OrdenCompraDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"] == null ? 0 : Convert.ToDecimal(dr["PorcDescuento"].ToString()));

                        oResultDTO.ListaResultado.Add(oCOM_OrdenCompraDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_OrdenCompraDTO>();
                }
            }
            return oResultDTO;
        }


    }
}
