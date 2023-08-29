using SistemaDermoSalud.Entities.Ventas;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SistemaDermoSalud.DataAccess;

namespace SistemaDermoSalud.DataAccess.Ventas
{
    public class VEN_DocVentaDAO
    {
        ResultDTO<VEN_GuiaRemisionDTO> oResultDTO_Guia = new ResultDTO<VEN_GuiaRemisionDTO>();
        public ResultDTO<VEN_DocVentaDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, SqlConnection cn = null)
        {
            ResultDTO<VEN_DocVentaDTO> oResultDTO = new ResultDTO<VEN_DocVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DctoVenta_ListarxFecha", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocVentaDTO();
                        oVEN_DocumentoVentaDTO.idDocumentoVenta = Convert.ToInt32(dr["idDocumentoVenta"] == null ? 0 : Convert.ToInt32(dr["idDocumentoVenta"].ToString()));
                        oVEN_DocumentoVentaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        //oVEN_DocumentoVentaDTO.idLocal = Convert.ToInt32(dr["idLocal"] == null ? 0 : Convert.ToInt32(dr["idLocal"].ToString()));
                        //oVEN_DocumentoVentaDTO.idAlmacen = Convert.ToInt32(dr["idAlmacen"] == null ? 0 : Convert.ToInt32(dr["idAlmacen"].ToString()));
                        oVEN_DocumentoVentaDTO.idTipoVenta = Convert.ToInt32(dr["idTipoVenta"] == null ? 0 : Convert.ToInt32(dr["idTipoVenta"].ToString()));
                        oVEN_DocumentoVentaDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"] == null ? 0 : Convert.ToInt32(dr["idTipoDocumento"].ToString()));
                        oVEN_DocumentoVentaDTO.idCliente = Convert.ToInt32(dr["idCliente"] == null ? 0 : Convert.ToInt32(dr["idCliente"].ToString()));
                        oVEN_DocumentoVentaDTO.ClienteRazon = dr["ClienteRazon"] == null ? "" : dr["ClienteRazon"].ToString();
                        oVEN_DocumentoVentaDTO.ClienteDireccion = dr["ClienteDireccion"] == null ? "" : dr["ClienteDireccion"].ToString();
                        oVEN_DocumentoVentaDTO.ClienteDocumento = dr["ClienteDocumento"] == null ? "" : dr["ClienteDocumento"].ToString();
                        oVEN_DocumentoVentaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oVEN_DocumentoVentaDTO.idPedido = Convert.ToInt32(dr["idPedido"] == null ? 0 : Convert.ToInt32(dr["idPedido"].ToString()));
                        oVEN_DocumentoVentaDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oVEN_DocumentoVentaDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.SerieDocumento = (dr["SerieDocumento"] == null ? "" : (dr["SerieDocumento"].ToString()));
                        oVEN_DocumentoVentaDTO.NumDocumento = (dr["NumDocumento"] == null ? "" : (dr["NumDocumento"].ToString()));
                        oVEN_DocumentoVentaDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
                        oVEN_DocumentoVentaDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
                        oVEN_DocumentoVentaDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
                        oVEN_DocumentoVentaDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
                        oVEN_DocumentoVentaDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjero"].ToString()));
                        oVEN_DocumentoVentaDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oVEN_DocumentoVentaDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));
                        oVEN_DocumentoVentaDTO.EstadoDoc = Convert.ToString(dr["EstadoDoc"].ToString());
                        oVEN_DocumentoVentaDTO.flgIGV = Convert.ToBoolean(dr["flgIGV"] == null ? false : Convert.ToBoolean(dr["flgIGV"].ToString()));
                        oVEN_DocumentoVentaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_DocumentoVentaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_DocumentoVentaDTO.UsuarioCreacion = Convert.ToString(dr["UsuarioCreacion"].ToString());
                        oVEN_DocumentoVentaDTO.UsuarioModificacion = Convert.ToString(dr["UsuarioModificacion"].ToString());
                        oVEN_DocumentoVentaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oVEN_DocumentoVentaDTO.Enlace = (dr["Enlace"] == null ? "" : (dr["Enlace"].ToString()));
                        oVEN_DocumentoVentaDTO.EstadoSunat = (dr["EstadoVenta"] == null ? "" : (dr["EstadoVenta"].ToString()));
                        oVEN_DocumentoVentaDTO.CodigoSunat = dr["CodigoSunat"].ToString();
                        oVEN_DocumentoVentaDTO.SerieNumDoc = dr["SerieNumDoc"].ToString();
                        oVEN_DocumentoVentaDTO.SubTotalDolares = Convert.ToDecimal(dr["SubTotalDolares"] == null ? 0 : Convert.ToDecimal(dr["SubTotalDolares"].ToString()));
                        oVEN_DocumentoVentaDTO.SubTotalSoles = Convert.ToDecimal(dr["SubTotalSoles"] == null ? 0 : Convert.ToDecimal(dr["SubTotalSoles"].ToString()));
                        //oVEN_DocumentoVentaDTO.MonedaDesc = dr["MonedaDesc"].ToString();
                        oVEN_DocumentoVentaDTO.Inafecto = Convert.ToDecimal(dr["Inafecto"] == null ? 0 : Convert.ToDecimal(dr["Inafecto"].ToString()));
                        oVEN_DocumentoVentaDTO.Total = Convert.ToDecimal(dr["Total"] == null ? 0 : Convert.ToDecimal(dr["Total"].ToString()));

                        oResultDTO.ListaResultado.Add(oVEN_DocumentoVentaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocVentaDTO> ListarxID(int idDocumentoVenta)
        {
            ResultDTO<VEN_DocVentaDTO> oResultDTO = new ResultDTO<VEN_DocVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idDocumentoVenta", idDocumentoVenta);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocVentaDTO();
                        oVEN_DocumentoVentaDTO.idDocumentoVenta = Convert.ToInt32(dr["idDocumentoVenta"].ToString());
                        oVEN_DocumentoVentaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oVEN_DocumentoVentaDTO.idTipoVenta = Convert.ToInt32(dr["idTipoVenta"] == null ? 0 : Convert.ToInt32(dr["idTipoVenta"].ToString()));
                        oVEN_DocumentoVentaDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.idCliente = Convert.ToInt32(dr["idCliente"].ToString());
                        oVEN_DocumentoVentaDTO.ClienteRazon = dr["ClienteRazon"].ToString();
                        oVEN_DocumentoVentaDTO.ClienteDireccion = dr["ClienteDireccion"].ToString();
                        oVEN_DocumentoVentaDTO.ClienteDocumento = dr["ClienteDocumento"].ToString();
                        oVEN_DocumentoVentaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oVEN_DocumentoVentaDTO.idPedido = Convert.ToInt32(dr["idPedido"].ToString());
                        oVEN_DocumentoVentaDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"].ToString());
                        oVEN_DocumentoVentaDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.SerieDocumento = (dr["SerieDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.NumDocumento = (dr["NumDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"].ToString());
                        oVEN_DocumentoVentaDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"].ToString());
                        oVEN_DocumentoVentaDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"].ToString());
                        oVEN_DocumentoVentaDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"].ToString());
                        oVEN_DocumentoVentaDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"].ToString());
                        oVEN_DocumentoVentaDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        oVEN_DocumentoVentaDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"].ToString());
                        oVEN_DocumentoVentaDTO.EstadoDoc = (dr["EstadoDoc"].ToString());
                        oVEN_DocumentoVentaDTO.flgIGV = Convert.ToBoolean(dr["flgIGV"] == null ? false : Convert.ToBoolean(dr["flgIGV"].ToString()));
                        oVEN_DocumentoVentaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_DocumentoVentaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_DocumentoVentaDTO.UsuarioCreacion = dr["UsuarioCreacion"].ToString();
                        oVEN_DocumentoVentaDTO.UsuarioModificacion = dr["UsuarioModificacion"].ToString();
                        oVEN_DocumentoVentaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oVEN_DocumentoVentaDTO.ObservacionVenta = dr["ObservacionVenta"] == null ? "" : dr["ObservacionVenta"].ToString();
                        oVEN_DocumentoVentaDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"].ToString());
                        oVEN_DocumentoVentaDTO.TotalDescuentoNacional = Convert.ToDecimal(dr["TotalDescuentoNacional"].ToString());
                        oVEN_DocumentoVentaDTO.TotalDescuentoExtranjero = Convert.ToDecimal(dr["TotalDescuentoExtranjero"].ToString());
                        //oVEN_DocumentoVentaDTO.MonedaDesc = dr["MonedaDesc"].ToString();
                        oVEN_DocumentoVentaDTO.NroGuia = dr["NroGuia"].ToString();
                        oVEN_DocumentoVentaDTO.NroTicket = dr["NroTicket"].ToString();
                        oVEN_DocumentoVentaDTO.OrdenCompra = dr["OrdenCompra"].ToString();
                        oVEN_DocumentoVentaDTO.Enlace = dr["Enlace"].ToString();
                        oVEN_DocumentoVentaDTO.EstadoSunat = dr["EstadoVenta"].ToString();
                        oVEN_DocumentoVentaDTO.Detraccion = Convert.ToDecimal(dr["Detraccion"].ToString());
                        oVEN_DocumentoVentaDTO.flgDetraccion = Convert.ToBoolean(dr["flgDetraccion"] == null ? false : Convert.ToBoolean(dr["flgDetraccion"].ToString()));
                        oVEN_DocumentoVentaDTO.PorcentajeDetraccion = Convert.ToDecimal(dr["PorcentajeDetraccion"].ToString());
                        oVEN_DocumentoVentaDTO.idOperacion = Convert.ToInt32(dr["idOperacion"].ToString());
                        oResultDTO.ListaResultado.Add(oVEN_DocumentoVentaDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<VEN_DocVentaDetalleDTO> lista = new List<VEN_DocVentaDetalleDTO>();
                            while (dr.Read())
                            {
                                VEN_DocVentaDetalleDTO oVEN_DocumentoVentaDetalleDTO = new VEN_DocVentaDetalleDTO();
                                oVEN_DocumentoVentaDetalleDTO.idDocumentoVentaDetalle = Convert.ToInt32(dr["idDocumentoVentaDetalle"].ToString());
                                //public int idDocumentoVenta { get; set; }
                                oVEN_DocumentoVentaDetalleDTO.idDocumentoVenta = Convert.ToInt32(dr["idDocumentoVenta"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.DescripcionArticulo = dr["DescripcionArticulo"].ToString();
                                oVEN_DocumentoVentaDetalleDTO.idCategoria = Convert.ToInt32(dr["idCategoria"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.DescripcionCategoria = dr["DescripcionCategoria"].ToString();
                                oVEN_DocumentoVentaDetalleDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.PrecioNacional = Convert.ToDecimal(dr["PrecioNacional"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.PrecioExtranjero = Convert.ToDecimal(dr["PrecioExtranjero"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.idDocumentoRef = Convert.ToInt32(dr["idDocumentoRef"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.DescripcionArtTicket = dr["DescripcionArtTicket"].ToString();
                                oVEN_DocumentoVentaDetalleDTO.UnidadMedida = dr["UnidadMedida"].ToString();
                                lista.Add(oVEN_DocumentoVentaDetalleDTO);
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
                    oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocVentaDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<VEN_DocVentaDTO> oResultDTO = new ResultDTO<VEN_DocVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_Delete", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocVentaDTO();
                        oVEN_DocumentoVentaDTO.idDocumentoVenta = Convert.ToInt32(dr["idDocumentoVenta"] == null ? 0 : Convert.ToInt32(dr["idDocumentoVenta"].ToString()));
                        oVEN_DocumentoVentaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oVEN_DocumentoVentaDTO.idTipoVenta = Convert.ToInt32(dr["idTipoVenta"] == null ? 0 : Convert.ToInt32(dr["idTipoVenta"].ToString()));
                        oVEN_DocumentoVentaDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"] == null ? 0 : Convert.ToInt32(dr["idTipoDocumento"].ToString()));
                        oVEN_DocumentoVentaDTO.idCliente = Convert.ToInt32(dr["idCliente"] == null ? 0 : Convert.ToInt32(dr["idCliente"].ToString()));
                        oVEN_DocumentoVentaDTO.ClienteRazon = dr["ClienteRazon"] == null ? "" : dr["ClienteRazon"].ToString();
                        oVEN_DocumentoVentaDTO.ClienteDireccion = dr["ClienteDireccion"] == null ? "" : dr["ClienteDireccion"].ToString();
                        oVEN_DocumentoVentaDTO.ClienteDocumento = dr["ClienteDocumento"] == null ? "" : dr["ClienteDocumento"].ToString();
                        oVEN_DocumentoVentaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oVEN_DocumentoVentaDTO.idPedido = Convert.ToInt32(dr["idPedido"] == null ? 0 : Convert.ToInt32(dr["idPedido"].ToString()));
                        oVEN_DocumentoVentaDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oVEN_DocumentoVentaDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.SerieDocumento = (dr["SerieDocumento"] == null ? "" : (dr["SerieDocumento"].ToString()));
                        oVEN_DocumentoVentaDTO.NumDocumento = (dr["NumDocumento"] == null ? "" : (dr["NumDocumento"].ToString()));
                        oVEN_DocumentoVentaDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
                        oVEN_DocumentoVentaDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
                        oVEN_DocumentoVentaDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
                        oVEN_DocumentoVentaDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
                        oVEN_DocumentoVentaDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjero"].ToString()));
                        oVEN_DocumentoVentaDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oVEN_DocumentoVentaDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));
                        oVEN_DocumentoVentaDTO.EstadoDoc = Convert.ToString(dr["EstadoDoc"].ToString());
                        oVEN_DocumentoVentaDTO.flgIGV = Convert.ToBoolean(dr["flgIGV"] == null ? false : Convert.ToBoolean(dr["flgIGV"].ToString()));
                        oVEN_DocumentoVentaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_DocumentoVentaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_DocumentoVentaDTO.UsuarioCreacion = Convert.ToString(dr["UsuarioCreacion"].ToString());
                        oVEN_DocumentoVentaDTO.UsuarioModificacion = Convert.ToString(dr["UsuarioModificacion"].ToString());
                        oVEN_DocumentoVentaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oVEN_DocumentoVentaDTO.Enlace = dr["Enlace"] == null ? "" : dr["Enlace"].ToString();
                        oResultDTO.ListaResultado.Add(oVEN_DocumentoVentaDTO);
                    }

                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocVentaDTO> UpdateInsert(VEN_DocVentaDTO oVEN_DocumentoVenta, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<VEN_DocVentaDTO> oResultDTO = new ResultDTO<VEN_DocVentaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idDocumentoVenta", oVEN_DocumentoVenta.idDocumentoVenta);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", 1);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoVenta", oVEN_DocumentoVenta.idTipoVenta);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoDocumento", oVEN_DocumentoVenta.idTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@idCliente", oVEN_DocumentoVenta.idCliente);
                        da.SelectCommand.Parameters.AddWithValue("@ClienteRazon", oVEN_DocumentoVenta.ClienteRazon);
                        da.SelectCommand.Parameters.AddWithValue("@ClienteDireccion", oVEN_DocumentoVenta.ClienteDireccion);
                        da.SelectCommand.Parameters.AddWithValue("@ClienteDocumento", oVEN_DocumentoVenta.ClienteDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@idMoneda", oVEN_DocumentoVenta.idMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@idPedido", oVEN_DocumentoVenta.idPedido);
                        da.SelectCommand.Parameters.AddWithValue("@idFormaPago", oVEN_DocumentoVenta.idFormaPago);
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oVEN_DocumentoVenta.FechaDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@SerieDocumento", oVEN_DocumentoVenta.SerieDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@NumDocumento", oVEN_DocumentoVenta.NumDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@SubTotalNacional", oVEN_DocumentoVenta.SubTotalNacional);
                        da.SelectCommand.Parameters.AddWithValue("@SubTotalExtranjero", oVEN_DocumentoVenta.SubTotalExtranjero);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambio", oVEN_DocumentoVenta.TipoCambio);
                        da.SelectCommand.Parameters.AddWithValue("@IGVNacional", oVEN_DocumentoVenta.IGVNacional);
                        da.SelectCommand.Parameters.AddWithValue("@IGVExtranjero", oVEN_DocumentoVenta.IGVExtranjero);
                        da.SelectCommand.Parameters.AddWithValue("@TotalNacional", oVEN_DocumentoVenta.TotalNacional);
                        da.SelectCommand.Parameters.AddWithValue("@TotalExtranjero", oVEN_DocumentoVenta.TotalExtranjero);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoDoc", oVEN_DocumentoVenta.EstadoDoc);
                        da.SelectCommand.Parameters.AddWithValue("@flgIGV", oVEN_DocumentoVenta.flgIGV);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oVEN_DocumentoVenta.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oVEN_DocumentoVenta.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oVEN_DocumentoVenta.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@ObservacionVenta", oVEN_DocumentoVenta.ObservacionVenta);
                        da.SelectCommand.Parameters.AddWithValue("@PorcDescuento", oVEN_DocumentoVenta.PorcDescuento);
                        da.SelectCommand.Parameters.AddWithValue("@ListaDetalle", oVEN_DocumentoVenta.cadDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoAfectacion", oVEN_DocumentoVenta.idTipoAfectacion);
                        da.SelectCommand.Parameters.AddWithValue("@TotalDescuentoNacional", oVEN_DocumentoVenta.TotalDescuentoNacional);
                        da.SelectCommand.Parameters.AddWithValue("@TotalDescuentoExtranjero", oVEN_DocumentoVenta.TotalDescuentoExtranjero);
                        da.SelectCommand.Parameters.AddWithValue("@Enlace", oVEN_DocumentoVenta.Enlace);
                        da.SelectCommand.Parameters.AddWithValue("@TipoPago", oVEN_DocumentoVenta.TipoPago);
                        da.SelectCommand.Parameters.AddWithValue("@Tarjeta", oVEN_DocumentoVenta.Tarjeta);
                        da.SelectCommand.Parameters.AddWithValue("@NroOperacion", oVEN_DocumentoVenta.NroOperacion);
                        da.SelectCommand.Parameters.AddWithValue("@NroGuia", oVEN_DocumentoVenta.NroGuia);
                        da.SelectCommand.Parameters.AddWithValue("@NroTicket", oVEN_DocumentoVenta.NroTicket);
                        da.SelectCommand.Parameters.AddWithValue("@OrdenCompra", oVEN_DocumentoVenta.OrdenCompra);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoVenta", oVEN_DocumentoVenta.EstadoSunat);
                        da.SelectCommand.Parameters.AddWithValue("@Aceptada_Sunat", oVEN_DocumentoVenta.Aceptada_Sunat);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion_Sunat", oVEN_DocumentoVenta.Descripcion_Sunat);
                        da.SelectCommand.Parameters.AddWithValue("@PDF_Base64", oVEN_DocumentoVenta.PDF_Base64);
                        da.SelectCommand.Parameters.AddWithValue("@XML_Base64", oVEN_DocumentoVenta.XML_Base64);
                        da.SelectCommand.Parameters.AddWithValue("@CDR_Base64", oVEN_DocumentoVenta.CDR_Base64);
                        da.SelectCommand.Parameters.AddWithValue("@idGuias", oVEN_DocumentoVenta.idGuias);
                        da.SelectCommand.Parameters.AddWithValue("@Pacas", oVEN_DocumentoVenta.Pacas);
                        da.SelectCommand.Parameters.AddWithValue("@TipoExportacion", oVEN_DocumentoVenta.TipoExportacion);
                        da.SelectCommand.Parameters.AddWithValue("@Lugar", oVEN_DocumentoVenta.Lugar);
                        da.SelectCommand.Parameters.AddWithValue("@Detraccion", oVEN_DocumentoVenta.Detraccion);
                        da.SelectCommand.Parameters.AddWithValue("@flgDetraccion", oVEN_DocumentoVenta.flgDetraccion);
                        da.SelectCommand.Parameters.AddWithValue("@PorcentajeDetraccion", oVEN_DocumentoVenta.PorcentajeDetraccion);
                        da.SelectCommand.Parameters.AddWithValue("@idOperacion", oVEN_DocumentoVenta.idOperacion);
                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarRangoFecha(1, fechaInicio, fechaFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocVentaDTO> Delete(VEN_DocVentaDTO oVEN_DocumentoVenta, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<VEN_DocVentaDTO> oResultDTO = new ResultDTO<VEN_DocVentaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idDocumentoVenta", oVEN_DocumentoVenta.idDocumentoVenta);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarRangoFecha(oVEN_DocumentoVenta.idEmpresa, fechaInicio, fechaFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocVentaDTO> Anular(VEN_DocVentaDTO oVEN_DocumentoVenta, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<VEN_DocVentaDTO> oResultDTO = new ResultDTO<VEN_DocVentaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_Anular", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idDocumentoVenta", oVEN_DocumentoVenta.idDocumentoVenta);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarRangoFecha(1, fechaInicio, fechaFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocVentaDTO> cargarVentas(int idEmpresa)
        {
            ResultDTO<VEN_DocVentaDTO> oResultDTO = new ResultDTO<VEN_DocVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_ListarVentas", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocVentaDTO();
                        oVEN_DocumentoVentaDTO.idDocumentoVenta = Convert.ToInt32(dr["idDocumentoVenta"].ToString());
                        oVEN_DocumentoVentaDTO.ClienteRazon = dr["ClienteRazon"].ToString();
                        oVEN_DocumentoVentaDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.NumDocumento = (dr["NumDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        oResultDTO.ListaResultado.Add(oVEN_DocumentoVentaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocVentaDetalleDTO> cargarDetalleVentas(int idVenta)
        {
            ResultDTO<VEN_DocVentaDetalleDTO> oResultDTO = new ResultDTO<VEN_DocVentaDetalleDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocVentaDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVentaDetalle_ListarxIdVentas", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idVenta", idVenta);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocVentaDetalleDTO oVEN_DocumentoVentaDetalleDTO = new VEN_DocVentaDetalleDTO();
                        oVEN_DocumentoVentaDetalleDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                        oVEN_DocumentoVentaDetalleDTO.DescripcionArticulo = dr["descripcionArticulo"].ToString();
                        oVEN_DocumentoVentaDetalleDTO.Marca = dr["Marca"].ToString();
                        oVEN_DocumentoVentaDetalleDTO.UnidadMedida = (dr["UnidadMedida"].ToString());
                        oVEN_DocumentoVentaDetalleDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                        oVEN_DocumentoVentaDetalleDTO.idDocumentoVentaDetalle = Convert.ToInt32(dr["idDocumentoVentaDetalle"].ToString());
                        oVEN_DocumentoVentaDetalleDTO.PrecioNacional = Convert.ToDecimal(dr["PrecioNacional"].ToString());
                        oResultDTO.ListaResultado.Add(oVEN_DocumentoVentaDetalleDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_DocVentaDetalleDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocVentaDTO> ListarVentasxCobrar(int idEmpresa)
        {
            ResultDTO<VEN_DocVentaDTO> oResultDTO = new ResultDTO<VEN_DocVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_ListarVentasxCobrar", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocVentaDTO();
                        oVEN_DocumentoVentaDTO.idDocumentoVenta = Convert.ToInt32(dr["idDocumentoVenta"].ToString());
                        oVEN_DocumentoVentaDTO.ClienteRazon = dr["ClienteRazon"].ToString();
                        oVEN_DocumentoVentaDTO.ClienteDocumento = dr["ClienteDocumento"].ToString();
                        oVEN_DocumentoVentaDTO.NumDocumento = (dr["NumDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.MontoxCobrar = Convert.ToDecimal(dr["MontoxCobrar"].ToString());
                        oResultDTO.ListaResultado.Add(oVEN_DocumentoVentaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public string ValidarDocumento(VEN_DocVentaDTO oCOM_Documento, SqlConnection cn = null)
        {
            ResultDTO<VEN_DocVentaDTO> oResultDTO = new ResultDTO<VEN_DocVentaDTO>();
            string dato = "";
            oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Validar_Venta_Documento", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdDocumentoVenta", oCOM_Documento.idDocumentoVenta);
                    da.SelectCommand.Parameters.AddWithValue("@Serie", oCOM_Documento.SerieDocumento);
                    da.SelectCommand.Parameters.AddWithValue("@Numero", oCOM_Documento.NumDocumento);

                    SqlDataReader dr = da.SelectCommand.ExecuteReader();

                    while (dr.Read())
                    {
                        VEN_DocVentaDTO oCOM_DocumentoNotaCredito = new VEN_DocVentaDTO();
                        dato = dr["idDocumentoVenta"] == null ? "" : dr["idDocumentoVenta"].ToString();
                        ;
                    }

                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                }
            }
            return dato;
        }
        public ResultDTO<VEN_SerieDTO> ListarSeriexTipoDocumento(int idTipoDocumento)
        {
            ResultDTO<VEN_SerieDTO> oResultDTO = new ResultDTO<VEN_SerieDTO>();
            oResultDTO.ListaResultado = new List<VEN_SerieDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Serie_ListarxIDTipoDocumento", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idTipoDocumento", idTipoDocumento);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_SerieDTO oVEN_SerieDTO = new VEN_SerieDTO();
                        oVEN_SerieDTO.idSerie = Convert.ToInt32(dr["idSerie"].ToString());
                        oVEN_SerieDTO.idTipoComprobante = Convert.ToInt32(dr["idTipoComprobante"].ToString());
                        oVEN_SerieDTO.NroSerie = dr["NroSerie"].ToString();
                        oVEN_SerieDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_SerieDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_SerieDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oVEN_SerieDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oVEN_SerieDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oVEN_SerieDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_SerieDTO>();
                }
            }
            return oResultDTO;
        }
        public string ObtenerNumeroDocumentoxSerie(int id, string Serie)
        {
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Serie_NumeroDocumento", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idTipoDocumento", id);
                    da.SelectCommand.Parameters.AddWithValue("@SerieDocumento", Serie);
                    var nroDocumento = da.SelectCommand.ExecuteScalar();
                    return nroDocumento.ToString();
                }
                catch (Exception ex)
                {
                    return "0";
                }
            }
        }
        public VEN_DocVentaDTO ListarxIDVenta(int idDocumentoVenta)
        {
            VEN_DocVentaDTO oResultDTO = new VEN_DocVentaDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idDocumentoVenta", idDocumentoVenta);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocVentaDTO();
                        oVEN_DocumentoVentaDTO.idDocumentoVenta = Convert.ToInt32(dr["idDocumentoVenta"].ToString());
                        oVEN_DocumentoVentaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oVEN_DocumentoVentaDTO.idTipoVenta = Convert.ToInt32(dr["idTipoVenta"] == null ? 0 : Convert.ToInt32(dr["idTipoVenta"].ToString()));
                        oVEN_DocumentoVentaDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.idCliente = Convert.ToInt32(dr["idCliente"].ToString());
                        oVEN_DocumentoVentaDTO.ClienteRazon = dr["ClienteRazon"].ToString();
                        oVEN_DocumentoVentaDTO.ClienteDireccion = dr["ClienteDireccion"].ToString();
                        oVEN_DocumentoVentaDTO.ClienteDocumento = dr["ClienteDocumento"].ToString();
                        oVEN_DocumentoVentaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oVEN_DocumentoVentaDTO.idPedido = Convert.ToInt32(dr["idPedido"].ToString());
                        oVEN_DocumentoVentaDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"].ToString());
                        oVEN_DocumentoVentaDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.SerieDocumento = (dr["SerieDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.NumDocumento = (dr["NumDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"].ToString());
                        oVEN_DocumentoVentaDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"].ToString());
                        oVEN_DocumentoVentaDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"].ToString());
                        oVEN_DocumentoVentaDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"].ToString());
                        oVEN_DocumentoVentaDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"].ToString());
                        oVEN_DocumentoVentaDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        oVEN_DocumentoVentaDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"].ToString());
                        oVEN_DocumentoVentaDTO.EstadoDoc = (dr["EstadoDoc"].ToString());
                        oVEN_DocumentoVentaDTO.flgIGV = Convert.ToBoolean(dr["flgIGV"] == null ? false : Convert.ToBoolean(dr["flgIGV"].ToString()));
                        oVEN_DocumentoVentaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_DocumentoVentaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_DocumentoVentaDTO.UsuarioCreacion = dr["UsuarioCreacion"].ToString();
                        oVEN_DocumentoVentaDTO.UsuarioModificacion = dr["UsuarioModificacion"].ToString();
                        oVEN_DocumentoVentaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oVEN_DocumentoVentaDTO.ObservacionVenta = dr["ObservacionVenta"] == null ? "" : dr["ObservacionVenta"].ToString();
                        oVEN_DocumentoVentaDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"].ToString());
                        oResultDTO = oVEN_DocumentoVentaDTO;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_GuiaRemisionDTO> ListarGuiasPendientes(string idCliente, string Local, SqlConnection cn = null)
        {
            ResultDTO<VEN_GuiaRemisionDTO> oResultDTO = new ResultDTO<VEN_GuiaRemisionDTO>();
            oResultDTO.ListaResultado = new List<VEN_GuiaRemisionDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_GuiaRemision_ListaPendientes", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdCliente", idCliente);
                    da.SelectCommand.Parameters.AddWithValue("@IdLocal", "004");
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_GuiaRemisionDTO oVEN_GuiaRemisionDTO = new VEN_GuiaRemisionDTO();
                        oVEN_GuiaRemisionDTO.idHdrGuiaRemision = dr["idHdrGuiaRemision"] == null ? "" : dr["idHdrGuiaRemision"].ToString();
                        oVEN_GuiaRemisionDTO.SerieGuia = dr["SerieGuia"] == null ? "" : dr["SerieGuia"].ToString();
                        oVEN_GuiaRemisionDTO.NroGuiaRemision = dr["NroGuiaRemision"] == null ? "" : dr["NroGuiaRemision"].ToString();
                        oVEN_GuiaRemisionDTO.NroTicket = dr["NroTicket"] == null ? "" : dr["NroTicket"].ToString();
                        oVEN_GuiaRemisionDTO.FechaGuiaRemision = dr["FechaGuiaRemision"] == null ? "" : dr["FechaGuiaRemision"].ToString();
                        oVEN_GuiaRemisionDTO.idLocal = dr["idLocal"] == null ? "" : dr["idLocal"].ToString();
                        oVEN_GuiaRemisionDTO.Local = dr["Local"] == null ? "" : dr["Local"].ToString();
                        oResultDTO.ListaResultado.Add(oVEN_GuiaRemisionDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_GuiaRemisionDTO>();
                }
            }
            return oResultDTO;
        }
        public void TraerDatosGuias(string idGuias, string idLocal, ResultDTO<VEN_GuiaRemisionDTO> oResultDTO_Guia, List<VEN_DocVentaDetalleDTO> listaDetalleGuia)
        {
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Guias_TraerDatos", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idGuia", idGuias);
                    da.SelectCommand.Parameters.AddWithValue("@idLocal", idLocal);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_GuiaRemisionDTO oVEN_GuiaRemisionDTO = new VEN_GuiaRemisionDTO();
                        oVEN_GuiaRemisionDTO.idHdrGuiaRemision = dr["idHdrGuiaRemision"].ToString();
                        oVEN_GuiaRemisionDTO.NroGuiaRemision = dr["NroGuiaRemision"].ToString();
                        oVEN_GuiaRemisionDTO.guia_serie_numero = dr["NroGuiaRemision"].ToString();
                        oVEN_GuiaRemisionDTO.NroTicket = dr["NroTicket"].ToString();
                        oVEN_GuiaRemisionDTO.guia_tipo = 1;
                        oResultDTO_Guia.ListaResultado.Add(oVEN_GuiaRemisionDTO);
                    }
                    if (oResultDTO_Guia.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                VEN_DocVentaDetalleDTO oVEN_DocumentoVentaDetalleDTO = new VEN_DocVentaDetalleDTO();
                                oVEN_DocumentoVentaDetalleDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.DescripcionArticulo = dr["DescripcionArticulo"].ToString();
                                oVEN_DocumentoVentaDetalleDTO.UnidadMedida = dr["idUnidadMedida"].ToString();
                                oVEN_DocumentoVentaDetalleDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.PrecioNacional = Convert.ToDecimal(dr["PrecioNacional"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.idGuia = dr["idGuia"].ToString();
                                oVEN_DocumentoVentaDetalleDTO.idDetGuia = dr["idDetGuia"].ToString();
                                listaDetalleGuia.Add(oVEN_DocumentoVentaDetalleDTO);
                            }
                        }
                    }
                    oResultDTO_Guia.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO_Guia.Resultado = "Error";
                    oResultDTO_Guia.MensajeError = ex.Message;
                    oResultDTO_Guia.ListaResultado = new List<VEN_GuiaRemisionDTO>();
                }
            }
        }
        public ResultDTO<VEN_DocVentaDTO> ListarxID_Venta(Int64 idDocumentoVenta)
        {
            ResultDTO<VEN_DocVentaDTO> oResultDTO = new ResultDTO<VEN_DocVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idDocumentoVenta", idDocumentoVenta);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocVentaDTO();
                        oVEN_DocumentoVentaDTO.DocumentoVentaID = Convert.ToInt64(dr["idDocumentoVenta"].ToString());
                        oVEN_DocumentoVentaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oVEN_DocumentoVentaDTO.idTipoVenta = Convert.ToInt32(dr["idTipoVenta"] == null ? 0 : Convert.ToInt32(dr["idTipoVenta"].ToString()));
                        oVEN_DocumentoVentaDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.idCliente = Convert.ToInt32(dr["idCliente"].ToString());
                        oVEN_DocumentoVentaDTO.ClienteRazon = dr["ClienteRazon"].ToString();
                        oVEN_DocumentoVentaDTO.ClienteDireccion = dr["ClienteDireccion"].ToString();
                        oVEN_DocumentoVentaDTO.ClienteDocumento = dr["ClienteDocumento"].ToString();
                        oVEN_DocumentoVentaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oVEN_DocumentoVentaDTO.idPedido = Convert.ToInt32(dr["idPedido"].ToString());
                        oVEN_DocumentoVentaDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"].ToString());
                        oVEN_DocumentoVentaDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.SerieDocumento = (dr["SerieDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.NumDocumento = (dr["NumDocumento"].ToString());
                        oVEN_DocumentoVentaDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"].ToString());
                        oVEN_DocumentoVentaDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"].ToString());
                        oVEN_DocumentoVentaDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"].ToString());
                        oVEN_DocumentoVentaDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"].ToString());
                        oVEN_DocumentoVentaDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"].ToString());
                        oVEN_DocumentoVentaDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        oVEN_DocumentoVentaDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"].ToString());
                        oVEN_DocumentoVentaDTO.EstadoDoc = (dr["EstadoDoc"].ToString());
                        oVEN_DocumentoVentaDTO.flgIGV = Convert.ToBoolean(dr["flgIGV"] == null ? false : Convert.ToBoolean(dr["flgIGV"].ToString()));
                        oVEN_DocumentoVentaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_DocumentoVentaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_DocumentoVentaDTO.UsuarioCreacion = dr["UsuarioCreacion"].ToString();
                        oVEN_DocumentoVentaDTO.UsuarioModificacion = dr["UsuarioModificacion"].ToString();
                        oVEN_DocumentoVentaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oVEN_DocumentoVentaDTO.ObservacionVenta = dr["ObservacionVenta"] == null ? "" : dr["ObservacionVenta"].ToString();
                        oVEN_DocumentoVentaDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"].ToString());
                        oVEN_DocumentoVentaDTO.TotalDescuentoNacional = Convert.ToDecimal(dr["TotalDescuentoNacional"].ToString());
                        oVEN_DocumentoVentaDTO.TotalDescuentoExtranjero = Convert.ToDecimal(dr["TotalDescuentoExtranjero"].ToString());
                        //oVEN_DocumentoVentaDTO.MonedaDesc = dr["MonedaDesc"].ToString();
                        oVEN_DocumentoVentaDTO.NroGuia = dr["NroGuia"].ToString();
                        oVEN_DocumentoVentaDTO.NroTicket = dr["NroTicket"].ToString();
                        oVEN_DocumentoVentaDTO.OrdenCompra = dr["OrdenCompra"].ToString();
                        oVEN_DocumentoVentaDTO.Enlace = dr["Enlace"].ToString();
                        oVEN_DocumentoVentaDTO.EstadoSunat = dr["EstadoVenta"].ToString();
                        oVEN_DocumentoVentaDTO.Detraccion = Convert.ToDecimal(dr["Detraccion"].ToString());
                        oVEN_DocumentoVentaDTO.flgDetraccion = Convert.ToBoolean(dr["flgDetraccion"] == null ? false : Convert.ToBoolean(dr["flgDetraccion"].ToString()));
                        oVEN_DocumentoVentaDTO.PorcentajeDetraccion = Convert.ToDecimal(dr["PorcentajeDetraccion"].ToString());
                        oVEN_DocumentoVentaDTO.idOperacion = Convert.ToInt32(dr["idOperacion"].ToString());
                        oResultDTO.ListaResultado.Add(oVEN_DocumentoVentaDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<VEN_DocVentaDetalleDTO> lista = new List<VEN_DocVentaDetalleDTO>();
                            while (dr.Read())
                            {
                                VEN_DocVentaDetalleDTO oVEN_DocumentoVentaDetalleDTO = new VEN_DocVentaDetalleDTO();
                                oVEN_DocumentoVentaDetalleDTO.DocumentoVentaDetalleID = Convert.ToInt64(dr["idDocumentoVentaDetalle"].ToString());
                                //public int idDocumentoVenta { get; set; }
                                oVEN_DocumentoVentaDetalleDTO.DocumentoVentaID = Convert.ToInt64(dr["idDocumentoVenta"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.DescripcionArticulo = dr["DescripcionArticulo"].ToString();
                                oVEN_DocumentoVentaDetalleDTO.idCategoria = Convert.ToInt32(dr["idCategoria"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.DescripcionCategoria = dr["DescripcionCategoria"].ToString();
                                oVEN_DocumentoVentaDetalleDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.PrecioNacional = Convert.ToDecimal(dr["PrecioNacional"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.PrecioExtranjero = Convert.ToDecimal(dr["PrecioExtranjero"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.idDocumentoRef = Convert.ToInt32(dr["idDocumentoRef"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                                oVEN_DocumentoVentaDetalleDTO.DescripcionArtTicket = dr["DescripcionArtTicket"].ToString();
                                oVEN_DocumentoVentaDetalleDTO.UnidadMedida = dr["UnidadMedida"].ToString();
                                lista.Add(oVEN_DocumentoVentaDetalleDTO);
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
                    oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public int validar_Factura(int idDocumentoVenta, SqlConnection cn = null)
        {
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Factura_ValidarDocumento", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idDocumentoVenta", idDocumentoVenta);
                    var existe = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                    return existe;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
    }
}
