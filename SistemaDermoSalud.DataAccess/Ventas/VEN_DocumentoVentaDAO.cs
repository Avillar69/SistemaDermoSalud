using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Reportes;
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
    public class VEN_DocumentoVentaDAO
    {
        ResultDTO<VEN_DocumentoVentaDTO> ListaRegistroVenta = new ResultDTO<VEN_DocumentoVentaDTO>();
        public ResultDTO<VEN_DocumentoVentaDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, SqlConnection cn = null)
        {
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_ListarxFecha", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocumentoVentaDTO();
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
                        oVEN_DocumentoVentaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oVEN_DocumentoVentaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oVEN_DocumentoVentaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oVEN_DocumentoVentaDTO.Enlace = (dr["Enlace"] == null ? "" : (dr["Enlace"].ToString()));

                        oVEN_DocumentoVentaDTO.CodigoSunat = dr["CodigoSunat"].ToString();
                        oVEN_DocumentoVentaDTO.SerieNumDoc = dr["SerieNumDoc"].ToString();
                        oVEN_DocumentoVentaDTO.SubTotalDolares = Convert.ToDecimal(dr["SubTotalDolares"] == null ? 0 : Convert.ToDecimal(dr["SubTotalDolares"].ToString()));
                        oVEN_DocumentoVentaDTO.SubTotalSoles = Convert.ToDecimal(dr["SubTotalSoles"] == null ? 0 : Convert.ToDecimal(dr["SubTotalSoles"].ToString()));
                        oVEN_DocumentoVentaDTO.MonedaDesc = dr["MonedaDesc"].ToString();
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
                    oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocumentoVenta_ReportePorCliente> ReportePorCliente(DateTime fecIni, DateTime fecFin, SqlConnection cn = null)
        {
            ResultDTO<VEN_DocumentoVenta_ReportePorCliente> oResultDTO = new ResultDTO<VEN_DocumentoVenta_ReportePorCliente>();
            oResultDTO.ListaResultado = new List<VEN_DocumentoVenta_ReportePorCliente>();
            using ((cn ?? (cn = new Conexion().conectar())))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_ReportePorCliente", cn);
                    da.SelectCommand.Parameters.AddWithValue("@FechaIni", fecIni);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fecFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocumentoVenta_ReportePorCliente obj = new VEN_DocumentoVenta_ReportePorCliente();
                        obj.TipoDocumento = dr["Doc"].ToString();
                        obj.Documento = dr["NumDoc"].ToString();
                        obj.FechaDoc = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        obj.TipoVenta = dr["TipoVenta"].ToString();
                        obj.Auxiliar = dr["ClienteRazon"].ToString();
                        obj.Total = Math.Round(Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString())), 2);

                        obj.Cantidad = Convert.ToInt32(dr["Cantidad"] == null ? 0 : Convert.ToInt32(dr["Cantidad"].ToString()));
                        obj.Descripcion = dr["DescripcionArticulo"].ToString();
                        obj.PUnit = Math.Round(Convert.ToDecimal(dr["PUnit"] == null ? 0 : Convert.ToDecimal(dr["PUnit"].ToString())), 2);
                        obj.Bimp = Math.Round(Convert.ToDecimal(dr["BImp"] == null ? 0 : Convert.ToDecimal(dr["BImp"].ToString())), 2);
                        obj.IGV = Math.Round(Convert.ToDecimal(dr["IGV"] == null ? 0 : Convert.ToDecimal(dr["IGV"].ToString())), 2);
                        obj.TotalDetalle = Math.Round(Convert.ToDecimal(dr["TotalDetalle"] == null ? 0 : Convert.ToDecimal(dr["TotalDetalle"].ToString())), 2);
                        obj.Moneda = dr["Moneda"].ToString();
                        oResultDTO.ListaResultado.Add(obj);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_DocumentoVenta_ReportePorCliente>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocumentoVentaDTO> ListarxID(int idDocumentoVenta)
        {
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
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
                        VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocumentoVentaDTO();
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
                        oVEN_DocumentoVentaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oVEN_DocumentoVentaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oVEN_DocumentoVentaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oVEN_DocumentoVentaDTO.NroCita = dr["NroCita"].ToString();
                        oVEN_DocumentoVentaDTO.ObservacionVenta = dr["ObservacionVenta"] == null ? "" : dr["ObservacionVenta"].ToString();
                        oVEN_DocumentoVentaDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"].ToString());
                        oVEN_DocumentoVentaDTO.TotalDescuentoNacional = Convert.ToDecimal(dr["TotalDescuentoNacional"].ToString());
                        oVEN_DocumentoVentaDTO.TotalDescuentoExtranjero = Convert.ToDecimal(dr["TotalDescuentoExtranjero"].ToString());
                        oVEN_DocumentoVentaDTO.MonedaDesc = dr["MonedaDesc"].ToString();
                        oResultDTO.ListaResultado.Add(oVEN_DocumentoVentaDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<VEN_DocumentoVentaDetalleDTO> lista = new List<VEN_DocumentoVentaDetalleDTO>();
                            while (dr.Read())
                            {
                                VEN_DocumentoVentaDetalleDTO oVEN_DocumentoVentaDetalleDTO = new VEN_DocumentoVentaDetalleDTO();
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
                    oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocumentoVentaDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
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
                        VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocumentoVentaDTO();
                        oVEN_DocumentoVentaDTO.idDocumentoVenta = Convert.ToInt32(dr["idDocumentoVenta"] == null ? 0 : Convert.ToInt32(dr["idDocumentoVenta"].ToString()));
                        oVEN_DocumentoVentaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        //   oVEN_DocumentoVentaDTO.idLocal = Convert.ToInt32(dr["idLocal"] == null ? 0 : Convert.ToInt32(dr["idLocal"].ToString()));
                        //  oVEN_DocumentoVentaDTO.idAlmacen = Convert.ToInt32(dr["idAlmacen"] == null ? 0 : Convert.ToInt32(dr["idAlmacen"].ToString()));
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
                        oVEN_DocumentoVentaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oVEN_DocumentoVentaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
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
                    oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocumentoVentaDTO> UpdateInsert(VEN_DocumentoVentaDTO oVEN_DocumentoVenta, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDTO>();
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
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oVEN_DocumentoVenta.idEmpresa);
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
                        da.SelectCommand.Parameters.AddWithValue("@NroCita", oVEN_DocumentoVenta.NroCita);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoAfectacion", oVEN_DocumentoVenta.idTipoAfectacion);
                        da.SelectCommand.Parameters.AddWithValue("@TotalDescuentoNacional", oVEN_DocumentoVenta.TotalDescuentoNacional);
                        da.SelectCommand.Parameters.AddWithValue("@TotalDescuentoExtranjero", oVEN_DocumentoVenta.TotalDescuentoExtranjero);
                        da.SelectCommand.Parameters.AddWithValue("@Enlace", oVEN_DocumentoVenta.Enlace);
                        da.SelectCommand.Parameters.AddWithValue("@TipoPago", oVEN_DocumentoVenta.TipoPago);
                        da.SelectCommand.Parameters.AddWithValue("@Tarjeta", oVEN_DocumentoVenta.Tarjeta);
                        da.SelectCommand.Parameters.AddWithValue("@NroOperacion", oVEN_DocumentoVenta.NroOperacion);
                        da.SelectCommand.Parameters.AddWithValue("@ObservacionCaja", oVEN_DocumentoVenta.ObservacionCaja);
                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 1)
                        {
                            SqlDataAdapter op = new SqlDataAdapter("SP_FN_ObtenerIdPagoCli", cn);
                            op.SelectCommand.CommandType = CommandType.StoredProcedure;

                            op.SelectCommand.Parameters.AddWithValue("@idDocumentoCompra", id_output.Value);
                            SqlDataReader re = op.SelectCommand.ExecuteReader();
                            int IDPAGO = 0;
                            while (re.Read())
                            {
                                IDPAGO = Convert.ToInt32(re["idPago"].ToString());
                            }

                            int FormaPago = oVEN_DocumentoVenta.idFormaPago;
                            if (FormaPago == 2)
                            {
                                SqlDataAdapter pd = new SqlDataAdapter("SP_FN_CajaDetalle_UpdateInsertDV", cn);
                                pd.SelectCommand.CommandType = CommandType.StoredProcedure;

                                pd.SelectCommand.Parameters.AddWithValue("@idCajaDetalle", 0);
                                pd.SelectCommand.Parameters.AddWithValue("@idEmpresa", oVEN_DocumentoVenta.idEmpresa);
                                pd.SelectCommand.Parameters.AddWithValue("@PeriodoAno", DateTime.Now.Year);
                                pd.SelectCommand.Parameters.AddWithValue("@NroCaja", 0);
                                pd.SelectCommand.Parameters.AddWithValue("@TipoOperacion", "I");
                                pd.SelectCommand.Parameters.AddWithValue("@idConcepto", 9);
                                pd.SelectCommand.Parameters.AddWithValue("@DescripcionConcepto", "COBRO DE COMPROBANTE");
                                pd.SelectCommand.Parameters.AddWithValue("@idMoneda", 1);
                                if (oVEN_DocumentoVenta.TipoPago == "TARJETA")
                                {
                                    pd.SelectCommand.Parameters.AddWithValue("@SubTotalNacional", Decimal.Round(oVEN_DocumentoVenta.SubTotalNacional * Convert.ToDecimal(96.46 / 100), 2));
                                    pd.SelectCommand.Parameters.AddWithValue("@IgvNacional", Decimal.Round(oVEN_DocumentoVenta.IGVNacional * Convert.ToDecimal(96.46 / 100), 2));
                                    pd.SelectCommand.Parameters.AddWithValue("@TotalNacional", Decimal.Round(oVEN_DocumentoVenta.TotalNacional * Convert.ToDecimal(96.46 / 100), 2));
                                }
                                else
                                {
                                    pd.SelectCommand.Parameters.AddWithValue("@SubTotalNacional", oVEN_DocumentoVenta.SubTotalNacional);
                                    pd.SelectCommand.Parameters.AddWithValue("@IgvNacional", oVEN_DocumentoVenta.IGVNacional);
                                    pd.SelectCommand.Parameters.AddWithValue("@TotalNacional", oVEN_DocumentoVenta.TotalNacional);
                                }
                                pd.SelectCommand.Parameters.AddWithValue("@idTipoEmpleado", 1);
                                pd.SelectCommand.Parameters.AddWithValue("@idProvCliEmpl", oVEN_DocumentoVenta.idCliente);
                                pd.SelectCommand.Parameters.AddWithValue("@NombreProvCliEmpl", oVEN_DocumentoVenta.ClienteRazon);
                                pd.SelectCommand.Parameters.AddWithValue("@Ruc", oVEN_DocumentoVenta.ClienteDocumento);
                                pd.SelectCommand.Parameters.AddWithValue("@Observaciones", "Cobro Venta " + oVEN_DocumentoVenta.SerieDocumento + "-" + oVEN_DocumentoVenta.NumDocumento);
                                pd.SelectCommand.Parameters.AddWithValue("@idTipoDocumento", oVEN_DocumentoVenta.idTipoDocumento);
                                pd.SelectCommand.Parameters.AddWithValue("@idCompraVenta", id_output.Value);
                                pd.SelectCommand.Parameters.AddWithValue("@SerieDcto", oVEN_DocumentoVenta.SerieDocumento);
                                pd.SelectCommand.Parameters.AddWithValue("@NroDcto", oVEN_DocumentoVenta.NumDocumento);
                                pd.SelectCommand.Parameters.AddWithValue("@MontoPendiente", 0.00);
                                pd.SelectCommand.Parameters.AddWithValue("@idTipoPago", oVEN_DocumentoVenta.idTipoPago);
                                pd.SelectCommand.Parameters.AddWithValue("@TipoPago", oVEN_DocumentoVenta.TipoPago);
                                pd.SelectCommand.Parameters.AddWithValue("@Tarjeta", oVEN_DocumentoVenta.Tarjeta);
                                pd.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oVEN_DocumentoVenta.UsuarioCreacion);
                                pd.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oVEN_DocumentoVenta.UsuarioModificacion);
                                int rpta1 = pd.SelectCommand.ExecuteNonQuery();
                            }
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarRangoFecha(oVEN_DocumentoVenta.idEmpresa, fechaInicio, fechaFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocumentoVentaDTO> Delete(VEN_DocumentoVentaDTO oVEN_DocumentoVenta, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDTO>();
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
                            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocumentoVentaDTO> Anular(int idDocumentoVenta, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDTO>();
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
                        da.SelectCommand.Parameters.AddWithValue("@idDocumentoVenta", idDocumentoVenta);
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
                            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocumentoVentaDTO> cargarVentas(int idEmpresa)
        {
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
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
                        VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocumentoVentaDTO();
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
                    oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocumentoVentaDetalleDTO> cargarDetalleVentas(int idVenta)
        {
            ResultDTO<VEN_DocumentoVentaDetalleDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDetalleDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDetalleDTO>();
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
                        VEN_DocumentoVentaDetalleDTO oVEN_DocumentoVentaDetalleDTO = new VEN_DocumentoVentaDetalleDTO();
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
                    oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDetalleDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocumentoVentaDTO> ListarVentasxCobrar(int idEmpresa)
        {
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
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
                        VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocumentoVentaDTO();
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
                    oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public string ValidarDocumento(VEN_DocumentoVentaDTO oCOM_Documento, SqlConnection cn = null)
        {
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDTO>();
            string dato = "";
            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
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
                        VEN_DocumentoVentaDTO oCOM_DocumentoNotaCredito = new VEN_DocumentoVentaDTO();
                        dato = dr["idDocumentoVenta"] == null ? "" : dr["idDocumentoVenta"].ToString();
                        ;
                    }

                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
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
        public VEN_DocumentoVentaDTO ListarxIDVenta(int idDocumentoVenta)
        {
            VEN_DocumentoVentaDTO oResultDTO = new VEN_DocumentoVentaDTO();
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
                        VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocumentoVentaDTO();
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
                        oVEN_DocumentoVentaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oVEN_DocumentoVentaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oVEN_DocumentoVentaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oVEN_DocumentoVentaDTO.NroCita = dr["NroCita"].ToString();
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

        public ResultDTO<VEN_DocumentoVentaDTO> ListarVentasDashboard(SqlConnection cn = null)
        {
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_ListarDashboard", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocumentoVentaDTO();
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
                        oVEN_DocumentoVentaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oVEN_DocumentoVentaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oVEN_DocumentoVentaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oVEN_DocumentoVentaDTO.Enlace = (dr["Enlace"] == null ? "" : (dr["Enlace"].ToString()));

                        oVEN_DocumentoVentaDTO.CodigoSunat = dr["CodigoSunat"].ToString();
                        oVEN_DocumentoVentaDTO.SerieNumDoc = dr["SerieNumDoc"].ToString();
                        oVEN_DocumentoVentaDTO.SubTotalDolares = Convert.ToDecimal(dr["SubTotalDolares"] == null ? 0 : Convert.ToDecimal(dr["SubTotalDolares"].ToString()));
                        oVEN_DocumentoVentaDTO.SubTotalSoles = Convert.ToDecimal(dr["SubTotalSoles"] == null ? 0 : Convert.ToDecimal(dr["SubTotalSoles"].ToString()));
                        oVEN_DocumentoVentaDTO.MonedaDesc = dr["MonedaDesc"].ToString();
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
                    oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                }
            }
            return oResultDTO;
        }
        //public ResultDTO<VEN_DocumentoVentaDTO> ListarRegistroVenta(ResultDTO<VEN_DocumentoVentaDTO> listaDocumento,int idEmpresa, DateTime fechaInicio, DateTime fechaFin,string TipoDcto, SqlConnection cn = null)
        //{
        //    ResultDTO<VEN_DocumentoVentaDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDTO>();
        //    if (listaDocumento.ListaResultado == null)
        //    {
        //        listaDocumento.ListaResultado = new List<VEN_DocumentoVentaDTO>();
        //    }      

        //    using ((cn == null ? cn = new Conexion().conectar() : cn))
        //    {
        //        try
        //        {
        //            if (cn.State == ConnectionState.Closed) { cn.Open(); }
        //            SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_ListarRegistroVenta", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
        //            da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
        //            da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
        //            da.SelectCommand.Parameters.AddWithValue("@TipoDcto", TipoDcto);
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader dr = da.SelectCommand.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocumentoVentaDTO();
        //                oVEN_DocumentoVentaDTO.idDocumentoVenta = Convert.ToInt32(dr["idDocumentoVenta"] == null ? 0 : Convert.ToInt32(dr["idDocumentoVenta"].ToString()));
        //                oVEN_DocumentoVentaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
        //                oVEN_DocumentoVentaDTO.idTipoVenta = Convert.ToInt32(dr["idTipoVenta"] == null ? 0 : Convert.ToInt32(dr["idTipoVenta"].ToString()));
        //                oVEN_DocumentoVentaDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"] == null ? 0 : Convert.ToInt32(dr["idTipoDocumento"].ToString()));
        //                oVEN_DocumentoVentaDTO.idCliente = Convert.ToInt32(dr["idCliente"] == null ? 0 : Convert.ToInt32(dr["idCliente"].ToString()));
        //                oVEN_DocumentoVentaDTO.ClienteRazon = dr["ClienteRazon"] == null ? "" : dr["ClienteRazon"].ToString();
        //                oVEN_DocumentoVentaDTO.ClienteDireccion = dr["ClienteDireccion"] == null ? "" : dr["ClienteDireccion"].ToString();
        //                oVEN_DocumentoVentaDTO.ClienteDocumento = dr["ClienteDocumento"] == null ? "" : dr["ClienteDocumento"].ToString();
        //                oVEN_DocumentoVentaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
        //                oVEN_DocumentoVentaDTO.idPedido = Convert.ToInt32(dr["idPedido"] == null ? 0 : Convert.ToInt32(dr["idPedido"].ToString()));
        //                oVEN_DocumentoVentaDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
        //                oVEN_DocumentoVentaDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());
        //                oVEN_DocumentoVentaDTO.SerieDocumento = (dr["SerieDocumento"] == null ? "" : (dr["SerieDocumento"].ToString()));
        //                oVEN_DocumentoVentaDTO.NumDocumento = (dr["NumDocumento"] == null ? "" : (dr["NumDocumento"].ToString()));
        //                oVEN_DocumentoVentaDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
        //                oVEN_DocumentoVentaDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
        //                oVEN_DocumentoVentaDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
        //                oVEN_DocumentoVentaDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
        //                oVEN_DocumentoVentaDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjero"].ToString()));
        //                oVEN_DocumentoVentaDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
        //                oVEN_DocumentoVentaDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));
        //                oVEN_DocumentoVentaDTO.EstadoDoc = Convert.ToString(dr["EstadoDoc"].ToString());
        //                oVEN_DocumentoVentaDTO.flgIGV = Convert.ToBoolean(dr["flgIGV"] == null ? false : Convert.ToBoolean(dr["flgIGV"].ToString()));
        //                oVEN_DocumentoVentaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
        //                oVEN_DocumentoVentaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
        //                oVEN_DocumentoVentaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
        //                oVEN_DocumentoVentaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
        //                oVEN_DocumentoVentaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
        //                oVEN_DocumentoVentaDTO.Enlace = (dr["Enlace"] == null ? "" : (dr["Enlace"].ToString()));
        //                oVEN_DocumentoVentaDTO.CodigoSunat = dr["CodigoSunat"].ToString();
        //                oVEN_DocumentoVentaDTO.SerieNumDoc = dr["SerieNumDoc"].ToString();
        //                oVEN_DocumentoVentaDTO.SubTotalDolares = Convert.ToDecimal(dr["SubTotalDolares"] == null ? 0 : Convert.ToDecimal(dr["SubTotalDolares"].ToString()));
        //                oVEN_DocumentoVentaDTO.SubTotalSoles = Convert.ToDecimal(dr["SubTotalSoles"] == null ? 0 : Convert.ToDecimal(dr["SubTotalSoles"].ToString()));
        //                oVEN_DocumentoVentaDTO.MonedaDesc = dr["MonedaDesc"].ToString();
        //                oVEN_DocumentoVentaDTO.Inafecto = Convert.ToDecimal(dr["Inafecto"] == null ? 0 : Convert.ToDecimal(dr["Inafecto"].ToString()));
        //                oVEN_DocumentoVentaDTO.Total = Convert.ToDecimal(dr["Total"] == null ? 0 : Convert.ToDecimal(dr["Total"].ToString()));
        //                listaDocumento.ListaResultado.Add(oVEN_DocumentoVentaDTO);
        //            }
        //            oResultDTO.Resultado = "OK";
        //        }
        //        catch (Exception ex)
        //        {
        //            listaDocumento.Resultado = "Error";
        //            listaDocumento.MensajeError = ex.Message;
        //            listaDocumento.ListaResultado = new List<VEN_DocumentoVentaDTO>();
        //        }
        //    }
        //    return listaDocumento;
        //}
        public ResultDTO<VEN_DocumentoVentaDTO> ListarRegistroVenta(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, int TipoDcto, SqlConnection cn = null)
        {
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_ListarRegistroVenta", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.Parameters.AddWithValue("@TipoDcto", TipoDcto);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocumentoVentaDTO();
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
                        oVEN_DocumentoVentaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oVEN_DocumentoVentaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oVEN_DocumentoVentaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oVEN_DocumentoVentaDTO.Enlace = (dr["Enlace"] == null ? "" : (dr["Enlace"].ToString()));
                        oVEN_DocumentoVentaDTO.CodigoSunat = dr["CodigoSunat"].ToString();
                        oVEN_DocumentoVentaDTO.SerieNumDoc = dr["SerieNumDoc"].ToString();
                        oVEN_DocumentoVentaDTO.SubTotalDolares = Convert.ToDecimal(dr["SubTotalDolares"] == null ? 0 : Convert.ToDecimal(dr["SubTotalDolares"].ToString()));
                        oVEN_DocumentoVentaDTO.SubTotalSoles = Convert.ToDecimal(dr["SubTotalSoles"] == null ? 0 : Convert.ToDecimal(dr["SubTotalSoles"].ToString()));
                        oVEN_DocumentoVentaDTO.MonedaDesc = dr["MonedaDesc"].ToString();
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
                    oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocumentoVentaDTO> Actualizar(VEN_DocumentoVentaDTO oVEN_DocumentoVenta, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_VEN_DocumentoVenta_Actualizar", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oVEN_DocumentoVenta.FechaDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@SerieDocumento", oVEN_DocumentoVenta.SerieDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@NumDocumento", oVEN_DocumentoVenta.NumDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@Enlace", oVEN_DocumentoVenta.Enlace);
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
                            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDTO>();
                    }
                }
            }
            return oResultDTO;
        }


        public ResultDTO<VEN_DocumentoVentaDetalleDTO> ReporteVentaFecha(DateTime FechaInicio, DateTime FechaFin)
        {
            ResultDTO<VEN_DocumentoVentaDetalleDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDetalleDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_ListaVentas_Medicamento_Fecha", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", FechaFin);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocumentoVentaDetalleDTO oVEN_DocumentoVentaDetalleDTO = new VEN_DocumentoVentaDetalleDTO();
                        oVEN_DocumentoVentaDetalleDTO.idArticulo = Convert.ToInt32(dr["idMedicamentos"].ToString());
                        oVEN_DocumentoVentaDetalleDTO.Laboratorio = dr["Laboratorio"].ToString();
                        oVEN_DocumentoVentaDetalleDTO.DescripcionArticulo = dr["Descripcion"].ToString();
                        oVEN_DocumentoVentaDetalleDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                        oVEN_DocumentoVentaDetalleDTO.PrecioMedicamento = Convert.ToDecimal(dr["PrecioMedicamento"].ToString());
                        oVEN_DocumentoVentaDetalleDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        oResultDTO.ListaResultado.Add(oVEN_DocumentoVentaDetalleDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDetalleDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<VEN_DocumentoVentaDetalleDTO> ReporteVentaFechaMedicamento(DateTime FechaInicio, DateTime FechaFin, int idMedicamento)
        {
            ResultDTO<VEN_DocumentoVentaDetalleDTO> oResultDTO = new ResultDTO<VEN_DocumentoVentaDetalleDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_ListaVenta_Medicamento_Fecha_Producto", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", FechaFin);
                    da.SelectCommand.Parameters.AddWithValue("@idMedicamento", idMedicamento);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocumentoVentaDetalleDTO oVEN_DocumentoVentaDetalleDTO = new VEN_DocumentoVentaDetalleDTO();
                        oVEN_DocumentoVentaDetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        oVEN_DocumentoVentaDetalleDTO.Factura = dr["Factura"].ToString();
                        oVEN_DocumentoVentaDetalleDTO.Cliente = dr["ClienteRazon"].ToString();
                        oVEN_DocumentoVentaDetalleDTO.DescripcionArticulo = dr["Descripcion"].ToString();
                        oVEN_DocumentoVentaDetalleDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                        oVEN_DocumentoVentaDetalleDTO.PrecioMedicamento = Convert.ToDecimal(dr["PagoMedicamento"].ToString());
                        oVEN_DocumentoVentaDetalleDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        oResultDTO.ListaResultado.Add(oVEN_DocumentoVentaDetalleDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_DocumentoVentaDetalleDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Rep_DocumentoVentaDetalleCliente> ListarDetalleVentaCliente(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, int idCliente, SqlConnection cn = null)
        {
            ResultDTO<Rep_DocumentoVentaDetalleCliente> oResultDTO = new ResultDTO<Rep_DocumentoVentaDetalleCliente>();
            oResultDTO.ListaResultado = new List<Rep_DocumentoVentaDetalleCliente>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ven_DocumentoVentaDetalle_ListarxCliente", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.Parameters.AddWithValue("@idCliente", idCliente);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Rep_DocumentoVentaDetalleCliente oVEN_DocumentoVentaDTO = new Rep_DocumentoVentaDetalleCliente(); 
                        oVEN_DocumentoVentaDTO.FechaDocumento = dr["FechaDocumento"] == null ? "" : dr["FechaDocumento"].ToString();
                        oVEN_DocumentoVentaDTO.TipoComprobante = dr["TipoComprobante"] == null ? "" : dr["TipoComprobante"].ToString();
                        oVEN_DocumentoVentaDTO.NroComprobante = dr["NroComprobante"] == null ? "" : dr["NroComprobante"].ToString();
                        oVEN_DocumentoVentaDTO.DescripcionArticulo = dr["DescripcionArticulo"] == null ? "" : dr["DescripcionArticulo"].ToString();
                        oVEN_DocumentoVentaDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"] == null ? 0 : Convert.ToDecimal(dr["Cantidad"].ToString()));
                        oVEN_DocumentoVentaDTO.PrecioNacional = Convert.ToDecimal(dr["PrecioNacional"] == null ? 0 : Convert.ToDecimal(dr["PrecioNacional"].ToString()));
                        oVEN_DocumentoVentaDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));

                        oResultDTO.ListaResultado.Add(oVEN_DocumentoVentaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Rep_DocumentoVentaDetalleCliente>();
                }
            }
            return oResultDTO;
        }

    }
}
