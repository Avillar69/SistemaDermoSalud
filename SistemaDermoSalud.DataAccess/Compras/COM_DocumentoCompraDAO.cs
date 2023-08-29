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
   public  class COM_DocumentoCompraDAO
    {
        public ResultDTO<COM_DocumentoCompraDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, SqlConnection cn = null)
        {
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO = new ResultDTO<COM_DocumentoCompraDTO>();
            oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_DocumentoCompra_ListarxFecha", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO = new COM_DocumentoCompraDTO();
                        oCOM_DocumentoCompraDTO.idDocumentoCompra = Convert.ToInt32(dr["idDocumentoCompra"] == null ? 0 : Convert.ToInt32(dr["idDocumentoCompra"].ToString()));
                        oCOM_DocumentoCompraDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        //oCOM_DocumentoCompraDTO.idLocal = Convert.ToInt32(dr["idLocal"] == null ? 0 : Convert.ToInt32(dr["idLocal"].ToString()));
                        //oCOM_DocumentoCompraDTO.idAlmacen = Convert.ToInt32(dr["idAlmacen"] == null ? 0 : Convert.ToInt32(dr["idAlmacen"].ToString()));
                        oCOM_DocumentoCompraDTO.idTipoCompra = Convert.ToInt32(dr["idTipoCompra"] == null ? 0 : Convert.ToInt32(dr["idTipoCompra"].ToString()));
                        oCOM_DocumentoCompraDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"] == null ? 0 : Convert.ToInt32(dr["idTipoDocumento"].ToString()));
                        oCOM_DocumentoCompraDTO.idProveedor = Convert.ToInt32(dr["idProveedor"] == null ? 0 : Convert.ToInt32(dr["idProveedor"].ToString()));
                        oCOM_DocumentoCompraDTO.ProveedorRazon = dr["ProveedorRazon"] == null ? "" : dr["ProveedorRazon"].ToString();
                        oCOM_DocumentoCompraDTO.ProveedorDireccion = dr["ProveedorDireccion"] == null ? "" : dr["ProveedorDireccion"].ToString();
                        oCOM_DocumentoCompraDTO.ProveedorDocumento = dr["ProveedorDocumento"] == null ? "" : dr["ProveedorDocumento"].ToString();
                        oCOM_DocumentoCompraDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oCOM_DocumentoCompraDTO.idOrdenCompra = Convert.ToInt32(dr["idOrdenCompra"] == null ? 0 : Convert.ToInt32(dr["idOrdenCompra"].ToString()));
                        oCOM_DocumentoCompraDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oCOM_DocumentoCompraDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        oCOM_DocumentoCompraDTO.SerieDocumento = (dr["SerieDocumento"] == null ? "" : (dr["SerieDocumento"].ToString()));
                        oCOM_DocumentoCompraDTO.NumDocumento = (dr["NumDocumento"] == null ? "" : (dr["NumDocumento"].ToString()));
                        oCOM_DocumentoCompraDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
                        oCOM_DocumentoCompraDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
                        oCOM_DocumentoCompraDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
                        oCOM_DocumentoCompraDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
                        oCOM_DocumentoCompraDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjero"].ToString()));
                        oCOM_DocumentoCompraDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oCOM_DocumentoCompraDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));
                        oCOM_DocumentoCompraDTO.EstadoDoc = Convert.ToString(dr["EstadoDoc"].ToString());
                        oCOM_DocumentoCompraDTO.flgIGV = Convert.ToBoolean(dr["flgIGV"] == null ? false : Convert.ToBoolean(dr["flgIGV"].ToString()));
                        oCOM_DocumentoCompraDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCOM_DocumentoCompraDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCOM_DocumentoCompraDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oCOM_DocumentoCompraDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oCOM_DocumentoCompraDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oCOM_DocumentoCompraDTO.ObservacionCompra = dr["ObservacionCompra"] == null ? "" : dr["ObservacionCompra"].ToString();
                        oCOM_DocumentoCompraDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"] == null ? 0 : Convert.ToDecimal(dr["PorcDescuento"].ToString()));
                        oResultDTO.ListaResultado.Add(oCOM_DocumentoCompraDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<COM_DocumentoCompraDTO> ListarxIDNotas_Compras(int idDocumentoCompra)
        {
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO = new ResultDTO<COM_DocumentoCompraDTO>();
            oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_NotasC_Compras_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idDocumentoCompra", idDocumentoCompra);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO = new COM_DocumentoCompraDTO();
                        oCOM_DocumentoCompraDTO.NumDocumento = (dr["NumeroNotaCredito"] == null ? "" : (dr["NumeroNotaCredito"].ToString()));
                        oCOM_DocumentoCompraDTO.idDocumentoCompra = Convert.ToInt32(dr["IdNotaCredito"] == null ? 0 : Convert.ToInt32(dr["IdNotaCredito"].ToString()));
                        oResultDTO.ListaResultado.Add(oCOM_DocumentoCompraDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {

                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
                }
            }
            return oResultDTO;
        }


        public ResultDTO<COM_DocumentoCompraDTO> ListarxID(int idDocumentoCompra)
        {
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO = new ResultDTO<COM_DocumentoCompraDTO>();
            oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_DocumentoCompra_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idDocumentoCompra", idDocumentoCompra);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO = new COM_DocumentoCompraDTO();
                        oCOM_DocumentoCompraDTO.idDocumentoCompra = Convert.ToInt32(dr["idDocumentoCompra"].ToString());
                        oCOM_DocumentoCompraDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        //oCOM_DocumentoCompraDTO.idLocal = Convert.ToInt32(dr["idLocal"].ToString());
                        // oCOM_DocumentoCompraDTO.idAlmacen = Convert.ToInt32(dr["idAlmacen"].ToString());
                        oCOM_DocumentoCompraDTO.idTipoCompra = Convert.ToInt32(dr["idTipoCompra"] == null ? 0 : Convert.ToInt32(dr["idTipoCompra"].ToString()));
                        oCOM_DocumentoCompraDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"].ToString());
                        oCOM_DocumentoCompraDTO.idProveedor = Convert.ToInt32(dr["idProveedor"].ToString());
                        oCOM_DocumentoCompraDTO.ProveedorRazon = dr["ProveedorRazon"].ToString();
                        oCOM_DocumentoCompraDTO.ProveedorDireccion = dr["ProveedorDireccion"].ToString();
                        oCOM_DocumentoCompraDTO.ProveedorDocumento = dr["ProveedorDocumento"].ToString();
                        oCOM_DocumentoCompraDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oCOM_DocumentoCompraDTO.idOrdenCompra = Convert.ToInt32(dr["idOrdenCompra"].ToString());
                        oCOM_DocumentoCompraDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"].ToString());
                        oCOM_DocumentoCompraDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        oCOM_DocumentoCompraDTO.SerieDocumento = (dr["SerieDocumento"].ToString());
                        oCOM_DocumentoCompraDTO.NumDocumento = (dr["NumDocumento"].ToString());
                        oCOM_DocumentoCompraDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"].ToString());
                        oCOM_DocumentoCompraDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"].ToString());
                        oCOM_DocumentoCompraDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"].ToString());
                        oCOM_DocumentoCompraDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"].ToString());
                        oCOM_DocumentoCompraDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"].ToString());
                        oCOM_DocumentoCompraDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        oCOM_DocumentoCompraDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"].ToString());
                        oCOM_DocumentoCompraDTO.EstadoDoc = (dr["EstadoDoc"].ToString());
                        oCOM_DocumentoCompraDTO.flgIGV = Convert.ToBoolean(dr["flgIGV"].ToString());
                        oCOM_DocumentoCompraDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCOM_DocumentoCompraDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCOM_DocumentoCompraDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oCOM_DocumentoCompraDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oCOM_DocumentoCompraDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oCOM_DocumentoCompraDTO.ObservacionCompra = dr["ObservacionCompra"] == null ? "" : dr["ObservacionCompra"].ToString();
                        oCOM_DocumentoCompraDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"] == null ? 0 : Convert.ToDecimal(dr["PorcDescuento"].ToString()));
                        oCOM_DocumentoCompraDTO.FechaVencimiento = Convert.ToDateTime(dr["FechaVencimiento"].ToString());
                        oResultDTO.ListaResultado.Add(oCOM_DocumentoCompraDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<COM_DocumentoCompraDetalleDTO> lista = new List<COM_DocumentoCompraDetalleDTO>();
                            while (dr.Read())
                            {
                                COM_DocumentoCompraDetalleDTO oCOM_DocumentoCompraDetalleDTO = new COM_DocumentoCompraDetalleDTO();
                                oCOM_DocumentoCompraDetalleDTO.idDocumentoCompraDetalle = Convert.ToInt32(dr["idDocumentoCompraDetalle"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.idDocumentoCompra = Convert.ToInt32(dr["idDocumentoCompra"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.descripcionArticulo = dr["descripcionArticulo"].ToString();
                                oCOM_DocumentoCompraDetalleDTO.idCategoria = Convert.ToInt32(dr["idCategoria"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.descripcionCategoria = dr["descripcionCategoria"].ToString();
                                oCOM_DocumentoCompraDetalleDTO.UnidadMedida = dr["Codigo"].ToString();
                                oCOM_DocumentoCompraDetalleDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.PrecioNacional = Convert.ToDecimal(dr["PrecioNacional"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.PrecioExtranjero = Convert.ToDecimal(dr["PrecioExtranjero"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.idDocumentoRef = Convert.ToInt32(dr["idDocumentoRef"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                                oCOM_DocumentoCompraDetalleDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                                lista.Add(oCOM_DocumentoCompraDetalleDTO);
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
                    oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_DocumentoCompraDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO = new ResultDTO<COM_DocumentoCompraDTO>();
            oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_DocumentoCompra_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO = new COM_DocumentoCompraDTO();
                        oCOM_DocumentoCompraDTO.idDocumentoCompra = Convert.ToInt32(dr["idDocumentoCompra"] == null ? 0 : Convert.ToInt32(dr["idDocumentoCompra"].ToString()));
                        oCOM_DocumentoCompraDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        //   oCOM_DocumentoCompraDTO.idLocal = Convert.ToInt32(dr["idLocal"] == null ? 0 : Convert.ToInt32(dr["idLocal"].ToString()));
                        //  oCOM_DocumentoCompraDTO.idAlmacen = Convert.ToInt32(dr["idAlmacen"] == null ? 0 : Convert.ToInt32(dr["idAlmacen"].ToString()));
                        oCOM_DocumentoCompraDTO.idTipoCompra = Convert.ToInt32(dr["idTipoCompra"] == null ? 0 : Convert.ToInt32(dr["idTipoCompra"].ToString()));
                        oCOM_DocumentoCompraDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"] == null ? 0 : Convert.ToInt32(dr["idTipoDocumento"].ToString()));
                        oCOM_DocumentoCompraDTO.idProveedor = Convert.ToInt32(dr["idProveedor"] == null ? 0 : Convert.ToInt32(dr["idProveedor"].ToString()));
                        oCOM_DocumentoCompraDTO.ProveedorRazon = dr["ProveedorRazon"] == null ? "" : dr["ProveedorRazon"].ToString();
                        oCOM_DocumentoCompraDTO.ProveedorDireccion = dr["ProveedorDireccion"] == null ? "" : dr["ProveedorDireccion"].ToString();
                        oCOM_DocumentoCompraDTO.ProveedorDocumento = dr["ProveedorDocumento"] == null ? "" : dr["ProveedorDocumento"].ToString();
                        oCOM_DocumentoCompraDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oCOM_DocumentoCompraDTO.idOrdenCompra = Convert.ToInt32(dr["idOrdenCompra"] == null ? 0 : Convert.ToInt32(dr["idOrdenCompra"].ToString()));
                        oCOM_DocumentoCompraDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oCOM_DocumentoCompraDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        oCOM_DocumentoCompraDTO.SerieDocumento = (dr["SerieDocumento"] == null ? "" : (dr["SerieDocumento"].ToString()));
                        oCOM_DocumentoCompraDTO.NumDocumento = (dr["NumDocumento"] == null ? "" : (dr["NumDocumento"].ToString()));
                        oCOM_DocumentoCompraDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
                        oCOM_DocumentoCompraDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
                        oCOM_DocumentoCompraDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
                        oCOM_DocumentoCompraDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
                        oCOM_DocumentoCompraDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjero"].ToString()));
                        oCOM_DocumentoCompraDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oCOM_DocumentoCompraDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));
                        oCOM_DocumentoCompraDTO.EstadoDoc = Convert.ToString(dr["EstadoDoc"].ToString());
                        oCOM_DocumentoCompraDTO.flgIGV = Convert.ToBoolean(dr["flgIGV"] == null ? false : Convert.ToBoolean(dr["flgIGV"].ToString()));
                        oCOM_DocumentoCompraDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCOM_DocumentoCompraDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCOM_DocumentoCompraDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oCOM_DocumentoCompraDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oCOM_DocumentoCompraDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oCOM_DocumentoCompraDTO.ObservacionCompra = dr["ObservacionCompra"] == null ? "" : dr["ObservacionCompra"].ToString();
                        oCOM_DocumentoCompraDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"] == null ? 0 : Convert.ToDecimal(dr["PorcDescuento"].ToString()));
                        oResultDTO.ListaResultado.Add(oCOM_DocumentoCompraDTO);
                    }

                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_DocumentoCompraDTO> UpdateInsert(COM_DocumentoCompraDTO oCOM_DocumentoCompra, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO = new ResultDTO<COM_DocumentoCompraDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_COM_DocumentoCompra_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idDocumentoCompra", oCOM_DocumentoCompra.idDocumentoCompra);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oCOM_DocumentoCompra.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoCompra", oCOM_DocumentoCompra.idTipoCompra);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoDocumento", oCOM_DocumentoCompra.idTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@idProveedor", oCOM_DocumentoCompra.idProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@ProveedorRazon", oCOM_DocumentoCompra.ProveedorRazon);
                        da.SelectCommand.Parameters.AddWithValue("@ProveedorDireccion", oCOM_DocumentoCompra.ProveedorDireccion);
                        da.SelectCommand.Parameters.AddWithValue("@ProveedorDocumento", oCOM_DocumentoCompra.ProveedorDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@idMoneda", oCOM_DocumentoCompra.idMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@idOrdenCompra", oCOM_DocumentoCompra.idOrdenCompra);
                        da.SelectCommand.Parameters.AddWithValue("@idFormaPago", oCOM_DocumentoCompra.idFormaPago);
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oCOM_DocumentoCompra.FechaDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@SerieDocumento", oCOM_DocumentoCompra.SerieDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@NumDocumento", oCOM_DocumentoCompra.NumDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@SubTotalNacional", oCOM_DocumentoCompra.SubTotalNacional);
                        da.SelectCommand.Parameters.AddWithValue("@SubTotalExtranjero", oCOM_DocumentoCompra.SubTotalExtranjero);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambio", oCOM_DocumentoCompra.TipoCambio);
                        da.SelectCommand.Parameters.AddWithValue("@IGVNacional", oCOM_DocumentoCompra.IGVNacional);
                        da.SelectCommand.Parameters.AddWithValue("@IGVExtranjero", oCOM_DocumentoCompra.IGVExtranjero);
                        da.SelectCommand.Parameters.AddWithValue("@TotalNacional", oCOM_DocumentoCompra.TotalNacional);
                        da.SelectCommand.Parameters.AddWithValue("@TotalExtranjero", oCOM_DocumentoCompra.TotalExtranjero);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoDoc", oCOM_DocumentoCompra.EstadoDoc);
                        da.SelectCommand.Parameters.AddWithValue("@flgIGV", oCOM_DocumentoCompra.flgIGV);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oCOM_DocumentoCompra.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oCOM_DocumentoCompra.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oCOM_DocumentoCompra.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@ObservacionCompra", oCOM_DocumentoCompra.ObservacionCompra);
                        da.SelectCommand.Parameters.AddWithValue("@PorcDescuento", oCOM_DocumentoCompra.PorcDescuento);
                        da.SelectCommand.Parameters.AddWithValue("@FechaVencimiento", oCOM_DocumentoCompra.FechaVencimiento);
                        da.SelectCommand.Parameters.AddWithValue("@ListaDetalle", oCOM_DocumentoCompra.cadDetalle);
                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            SqlDataAdapter op = new SqlDataAdapter("SP_FN_ObtenerIdPago", cn);
                            op.SelectCommand.CommandType = CommandType.StoredProcedure;
                            op.SelectCommand.Parameters.AddWithValue("@idDocumentoCompra", id_output.Value);
                            //op.SelectCommand.Parameters.AddWithValue("@idDocumentoCompra", oCOM_DocumentoCompra.idDocumentoCompra);
                            SqlDataReader re = op.SelectCommand.ExecuteReader();
                            int IDPAGO = 0;
                            while (re.Read())
                            {
                                IDPAGO = Convert.ToInt32(re["idPago"].ToString());
                            }
                            re.Close();
                            int FormaPago = oCOM_DocumentoCompra.idFormaPago;
                            if (FormaPago == 2)
                            {
                                //SqlDataAdapter pd = new SqlDataAdapter("SP_FN_PagosDetalle_UpdateInsert", cn);
                                //pd.SelectCommand.CommandType = CommandType.StoredProcedure;
                                //pd.SelectCommand.Parameters.AddWithValue("@idPagoDetalle", oCOM_DocumentoCompra.idPagoDetalle);
                                ///**/
                                //pd.SelectCommand.Parameters.AddWithValue("@idPago", IDPAGO);
                                //pd.SelectCommand.Parameters.AddWithValue("@idEmpresa", oCOM_DocumentoCompra.idEmpresa);
                                //pd.SelectCommand.Parameters.AddWithValue("@idCajaDetalle", oCOM_DocumentoCompra.idCajaDetalle);
                                //pd.SelectCommand.Parameters.AddWithValue("@Observacion", oCOM_DocumentoCompra.Observacion);
                                //pd.SelectCommand.Parameters.AddWithValue("@idTipoOperacion", oCOM_DocumentoCompra.idTipoOperacion);
                                //pd.SelectCommand.Parameters.AddWithValue("@NumeroOperacion", oCOM_DocumentoCompra.NumeroOperacion);
                                //pd.SelectCommand.Parameters.AddWithValue("@DescripcionOperacion", oCOM_DocumentoCompra.DescripcionOperacion);
                                ///**/
                                //pd.SelectCommand.Parameters.AddWithValue("@idDocumento", id_output.Value);
                                //pd.SelectCommand.Parameters.AddWithValue("@idConcepto", oCOM_DocumentoCompra.idConcepto);
                                //pd.SelectCommand.Parameters.AddWithValue("@Concepto", oCOM_DocumentoCompra.Concepto);
                                //pd.SelectCommand.Parameters.AddWithValue("@idFormaPago", oCOM_DocumentoCompra.idFormaPago);
                                //pd.SelectCommand.Parameters.AddWithValue("@DescripcionFormaPago", oCOM_DocumentoCompra.DescripcionFormaPago);
                                //pd.SelectCommand.Parameters.AddWithValue("@idCuentaBancario", oCOM_DocumentoCompra.idCuentaBancario);
                                //pd.SelectCommand.Parameters.AddWithValue("@NumeroCuenta", oCOM_DocumentoCompra.NumeroCuenta);
                                //pd.SelectCommand.Parameters.AddWithValue("@Monto", oCOM_DocumentoCompra.Monto);
                                //pd.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oCOM_DocumentoCompra.UsuarioCreacion);
                                //pd.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oCOM_DocumentoCompra.UsuarioModificacion);
                                //pd.SelectCommand.Parameters.AddWithValue("@Estado", oCOM_DocumentoCompra.Estado);
                                //pd.SelectCommand.Parameters.AddWithValue("@idCuentaBancarioDestino", oCOM_DocumentoCompra.idCuentaBancarioDestino);
                                //pd.SelectCommand.Parameters.AddWithValue("@NumeroCuentaDestino", oCOM_DocumentoCompra.NumeroCuentaDestino);
                                //pd.SelectCommand.Parameters.AddWithValue("@FechaDetalle", oCOM_DocumentoCompra.FechaDetalle);
                                //int rpta1 = pd.SelectCommand.ExecuteNonQuery();
                            }
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarRangoFecha(oCOM_DocumentoCompra.idEmpresa, fechaInicio, fechaFin, cn).ListaResultado;
                            transactionScope.Complete();

                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_DocumentoCompraDTO> Delete(COM_DocumentoCompraDTO oCOM_DocumentoCompra, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO = new ResultDTO<COM_DocumentoCompraDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_COM_DocumentoCompra_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idDocumentoCompra", oCOM_DocumentoCompra.idDocumentoCompra);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarRangoFecha(oCOM_DocumentoCompra.idEmpresa, fechaInicio, fechaFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_DocumentoCompraDTO> cargarCompras(int idEmpresa)
        {
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO = new ResultDTO<COM_DocumentoCompraDTO>();
            oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_DocumentoCompra_ListarCompras", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO = new COM_DocumentoCompraDTO();
                        oCOM_DocumentoCompraDTO.idDocumentoCompra = Convert.ToInt32(dr["idDocumentoCompra"].ToString());
                        oCOM_DocumentoCompraDTO.ProveedorRazon = dr["ProveedorRazon"].ToString();
                        oCOM_DocumentoCompraDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        oCOM_DocumentoCompraDTO.NumDocumento = (dr["NumDocumento"].ToString());
                        oCOM_DocumentoCompraDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        oResultDTO.ListaResultado.Add(oCOM_DocumentoCompraDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_DocumentoCompraDetalleDTO> cargarDetalleCompras(int idCompra)
        {
            ResultDTO<COM_DocumentoCompraDetalleDTO> oResultDTO = new ResultDTO<COM_DocumentoCompraDetalleDTO>();
            oResultDTO.ListaResultado = new List<COM_DocumentoCompraDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_DocumentoCompraDetalle_ListarxIdCompras", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCompra", idCompra);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_DocumentoCompraDetalleDTO oCOM_DocumentoCompraDetalleDTO = new COM_DocumentoCompraDetalleDTO();
                        oCOM_DocumentoCompraDetalleDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                        oCOM_DocumentoCompraDetalleDTO.descripcionArticulo = dr["descripcionArticulo"].ToString();
                        oCOM_DocumentoCompraDetalleDTO.Marca = dr["Marca"].ToString();
                        oCOM_DocumentoCompraDetalleDTO.UnidadMedida = (dr["UnidadMedida"].ToString());
                        oCOM_DocumentoCompraDetalleDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                        oCOM_DocumentoCompraDetalleDTO.idDocumentoCompraDetalle = Convert.ToInt32(dr["idDocumentoCompraDetalle"].ToString());
                        oCOM_DocumentoCompraDetalleDTO.PrecioNacional = Convert.ToDecimal(dr["PrecioNacional"].ToString());
                        oResultDTO.ListaResultado.Add(oCOM_DocumentoCompraDetalleDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_DocumentoCompraDetalleDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_DocumentoCompraDTO> ListarComprasXPagar(int idEmpresa)
        {
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO = new ResultDTO<COM_DocumentoCompraDTO>();
            oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_DocumentoCompra_ListarxPagar", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO = new COM_DocumentoCompraDTO();
                        oCOM_DocumentoCompraDTO.idDocumentoCompra = Convert.ToInt32(dr["idDocumentoCompra"].ToString());
                        oCOM_DocumentoCompraDTO.ProveedorRazon = dr["ProveedorRazon"].ToString();
                        oCOM_DocumentoCompraDTO.ProveedorDocumento = dr["ProveedorDocumento"].ToString();
                        oCOM_DocumentoCompraDTO.NumDocumento = (dr["NumDocumento"].ToString());
                        oCOM_DocumentoCompraDTO.MontoxPagar = Convert.ToDecimal(dr["MontoxPagar"].ToString());
                        oResultDTO.ListaResultado.Add(oCOM_DocumentoCompraDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_DocumentoCompraDTO> FechaVencimiento(string fecha, int forma)
        {
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO = new ResultDTO<COM_DocumentoCompraDTO>();
            oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_ObtenerFechaVencimiento", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@fechaDoc", fecha);
                    da.SelectCommand.Parameters.AddWithValue("@idFormaPago", forma);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO = new COM_DocumentoCompraDTO();
                        oCOM_DocumentoCompraDTO.FechaVencimiento = Convert.ToDateTime(dr["FechaVencimiento"].ToString());
                        oResultDTO.ListaResultado.Add(oCOM_DocumentoCompraDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
                }
            }
            return oResultDTO;
        }

        // nuevos  cambios para nota de credito

        public string ValidarDocumento(COM_DocumentoCompraDTO oCOM_DocumentoCompra, SqlConnection cn = null)
        {
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO = new ResultDTO<COM_DocumentoCompraDTO>();
            string dato = "";
            oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_Validar_Documento", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdProveedor", oCOM_DocumentoCompra.idProveedor);
                    da.SelectCommand.Parameters.AddWithValue("@Numero", oCOM_DocumentoCompra.NumDocumento);
                    da.SelectCommand.Parameters.AddWithValue("@Serie", oCOM_DocumentoCompra.SerieDocumento);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();

                    while (dr.Read())
                    {
                        COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO = new COM_DocumentoCompraDTO();
                        dato = dr["idDocumentoCompra"] == null ? "" : dr["idDocumentoCompra"].ToString();
                        ;
                    }

                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_DocumentoCompraDTO>();
                }
            }
            return dato;
        }

    }
}
