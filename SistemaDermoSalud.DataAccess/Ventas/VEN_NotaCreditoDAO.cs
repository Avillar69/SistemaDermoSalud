using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Ventas;

namespace SistemaDermoSalud.DataAccess.Ventas
{
    public class VEN_NotaCreditoDAO
    {
        public ResultDTO<VEN_NotaCreditoDTO> ListarTodo(int idEmpresa, string tipoNota = null, SqlConnection cn = null)
        {
            ResultDTO<VEN_NotaCreditoDTO> oResultDTO = new ResultDTO<VEN_NotaCreditoDTO>();
            oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Nota_CreditoDebito_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@TipoNota", tipoNota);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_NotaCreditoDTO oVEN_NotaCreditoDTO = new VEN_NotaCreditoDTO();
                        oVEN_NotaCreditoDTO.idNotaCredito = Convert.ToInt32(dr["idNotaCredito"] == null ? 0 : Convert.ToInt32(dr["idNotaCredito"].ToString()));
                        oVEN_NotaCreditoDTO.TipoNota = dr["TipoNota"] == null ? "" : dr["TipoNota"].ToString();
                        oVEN_NotaCreditoDTO.idDocVenta = Convert.ToInt64(dr["idDocVenta"] == null ? 0 : Convert.ToInt64(dr["idDocVenta"].ToString()));
                        oVEN_NotaCreditoDTO.Serie = dr["Serie"] == null ? "" : dr["Serie"].ToString();
                        oVEN_NotaCreditoDTO.Numero = dr["Numero"] == null ? "" : dr["Numero"].ToString();
                        oVEN_NotaCreditoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oVEN_NotaCreditoDTO.FechaEmision = Convert.ToDateTime(dr["FechaEmision"].ToString());
                        oVEN_NotaCreditoDTO.FechaRecepcion = Convert.ToDateTime(dr["FechaRecepcion"].ToString());
                        oVEN_NotaCreditoDTO.idMotivo = Convert.ToInt32(dr["idMotivo"] == null ? 0 : Convert.ToInt32(dr["idMotivo"].ToString()));
                        oVEN_NotaCreditoDTO.Total = Convert.ToDecimal(dr["Total"] == null ? 0 : Convert.ToDecimal(dr["Total"].ToString()));
                        oVEN_NotaCreditoDTO.IGV = Convert.ToDecimal(dr["IGV"] == null ? 0 : Convert.ToDecimal(dr["IGV"].ToString()));
                        oVEN_NotaCreditoDTO.TotalGravada = Convert.ToDecimal(dr["TotalGravada"] == null ? 0 : Convert.ToDecimal(dr["TotalGravada"].ToString()));
                        //oVEN_NotaCreditoDTO.JsonDocFisico = dr["JsonDocFisico"] == null ? "" : dr["JsonDocFisico"].ToString();
                        oVEN_NotaCreditoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_NotaCreditoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_NotaCreditoDTO.UsuarioCreacion = dr["UsuarioCreacion"] == null ? "" : dr["UsuarioCreacion"].ToString();
                        oVEN_NotaCreditoDTO.UsuarioModificacion = dr["UsuarioModificacion"] == null ? "" : dr["UsuarioModificacion"].ToString();

                        oVEN_NotaCreditoDTO.Motivo = dr["Motivo"] == null ? "" : dr["Motivo"].ToString();
                        oVEN_NotaCreditoDTO.Cliente = dr["Cliente"] == null ? "" : dr["Cliente"].ToString();
                        oResultDTO.ListaResultado.Add(oVEN_NotaCreditoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_NotaCreditoDTO> ListarRangoFechas(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, string tipoNota = null, string tipoDoc = null, SqlConnection cn = null)
        {
            ResultDTO<VEN_NotaCreditoDTO> oResultDTO = new ResultDTO<VEN_NotaCreditoDTO>();
            oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Nota_CreditoDebito_ListarxFecha", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@TipoNota", tipoNota);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.Parameters.AddWithValue("@TipoDoc", tipoDoc);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_NotaCreditoDTO oVEN_NotaCreditoDTO = new VEN_NotaCreditoDTO();
                        oVEN_NotaCreditoDTO.idNotaCredito = Convert.ToInt32(dr["idNotaCredito"] == null ? 0 : Convert.ToInt32(dr["idNotaCredito"].ToString()));
                        oVEN_NotaCreditoDTO.FechaEmision = Convert.ToDateTime(dr["FechaEmision"].ToString());
                        //oVEN_NotaCreditoDTO.Serie = dr["Serie"] == null ? "" : dr["Serie"].ToString();
                        oVEN_NotaCreditoDTO.Numero = dr["Numero"] == null ? "" : dr["Numero"].ToString();
                        oVEN_NotaCreditoDTO.Motivo = dr["Motivo"] == null ? "" : dr["Motivo"].ToString();
                        oVEN_NotaCreditoDTO.NumDocRef = dr["Documento"] == null ? "" : dr["Documento"].ToString();
                        oVEN_NotaCreditoDTO.Cliente = dr["Cliente"] == null ? "" : dr["Cliente"].ToString();
                        oVEN_NotaCreditoDTO.Total = Convert.ToDecimal(dr["Total"] == null ? 0 : Convert.ToDecimal(dr["Total"].ToString()));
                        oVEN_NotaCreditoDTO.Enlace = dr["Enlace"].ToString();
                        oVEN_NotaCreditoDTO.EstadoSunat = dr["EstadoSunat"].ToString();
                        oResultDTO.ListaResultado.Add(oVEN_NotaCreditoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
                }
            }
            return oResultDTO;
        }
        public string ValidarDocumento(string serie, string numero, SqlConnection cn = null)
        {
            ResultDTO<VEN_DocVentaDTO> oResultDTO = new ResultDTO<VEN_DocVentaDTO>();
            string dato = "";
            oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
            using ((cn ?? (cn = new Conexion().conectar())))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Nota_CreditoDebito_Validar_Docmuento", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Serie", serie);
                    da.SelectCommand.Parameters.AddWithValue("@Numero", numero);

                    SqlDataReader dr = da.SelectCommand.ExecuteReader();

                    while (dr.Read())
                    {
                        VEN_DocVentaDTO oCOM_DocumentoNotaCredito = new VEN_DocVentaDTO();
                        dato = dr["idNota"] == null ? "" : dr["idNota"].ToString();
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
        public ResultDTO<VEN_DocVentaDTO> ListarDocVentaxCli(int idCliente)
        {
            ResultDTO<VEN_DocVentaDTO> oResultDTO = new ResultDTO<VEN_DocVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_NotaCreditoDebito_ListarDocVentaxCli", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCliente", idCliente);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocVentaDTO oVEN_DocVentaDTO = new VEN_DocVentaDTO();
                        oVEN_DocVentaDTO.DocumentoVentaID = Convert.ToInt64(dr["idDocumentoVenta"].ToString());
                        oVEN_DocVentaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oVEN_DocVentaDTO.idTipoVenta = Convert.ToInt32(dr["idTipoVenta"] == null ? 0 : Convert.ToInt32(dr["idTipoVenta"].ToString()));
                        oVEN_DocVentaDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"].ToString());
                        oVEN_DocVentaDTO.idCliente = Convert.ToInt32(dr["idCliente"].ToString());
                        oVEN_DocVentaDTO.ClienteRazon = dr["ClienteRazon"].ToString();
                        oVEN_DocVentaDTO.ClienteDireccion = dr["ClienteDireccion"].ToString();
                        oVEN_DocVentaDTO.ClienteDocumento = dr["ClienteDocumento"].ToString();
                        oVEN_DocVentaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oVEN_DocVentaDTO.idPedido = Convert.ToInt32(dr["idPedido"].ToString());
                        oVEN_DocVentaDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"].ToString());
                        oVEN_DocVentaDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        oVEN_DocVentaDTO.SerieDocumento = (dr["SerieDocumento"].ToString());
                        oVEN_DocVentaDTO.NumDocumento = (dr["NumDocumento"].ToString());
                        oVEN_DocVentaDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"].ToString());
                        oVEN_DocVentaDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"].ToString());
                        oVEN_DocVentaDTO.TotalDescuentoNacional = Convert.ToDecimal(dr["TotalDescuentoNacional"].ToString());
                        oVEN_DocVentaDTO.TotalDescuentoExtranjero = Convert.ToDecimal(dr["TotalDescuentoExtranjero"].ToString());
                        oVEN_DocVentaDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"].ToString());
                        oVEN_DocVentaDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"].ToString());
                        oVEN_DocVentaDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"].ToString());
                        oVEN_DocVentaDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        oVEN_DocVentaDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"].ToString());
                        oVEN_DocVentaDTO.EstadoDoc = (dr["EstadoDoc"].ToString());
                        oVEN_DocVentaDTO.flgIGV = Convert.ToBoolean(dr["flgIGV"] == null ? false : Convert.ToBoolean(dr["flgIGV"].ToString()));
                        oVEN_DocVentaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_DocVentaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_DocVentaDTO.UsuarioCreacion = dr["UsuarioCreacion"].ToString();
                        oVEN_DocVentaDTO.UsuarioModificacion = dr["UsuarioModificacion"].ToString();
                        oVEN_DocVentaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oVEN_DocVentaDTO.ObservacionVenta = dr["ObservacionVenta"] == null ? "" : dr["ObservacionVenta"].ToString();
                        oVEN_DocVentaDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"].ToString());
                        //oVEN_DocVentaDTO.TipoOperacion = Convert.ToInt32(dr["TipoOperacion"].ToString());
                        oResultDTO.ListaResultado.Add(oVEN_DocVentaDTO);
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
                    oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_DocVentaDTO> ListarDocVenta()
        {
            ResultDTO<VEN_DocVentaDTO> oResultDTO = new ResultDTO<VEN_DocVentaDTO>();
            oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_NotaCreditoDebito_ListarDocVenta", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_DocVentaDTO oVEN_DocVentaDTO = new VEN_DocVentaDTO();
                        oVEN_DocVentaDTO.DocumentoVentaID = Convert.ToInt64(dr["idDocumentoVenta"].ToString());
                        oVEN_DocVentaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oVEN_DocVentaDTO.idTipoVenta = Convert.ToInt32(dr["idTipoVenta"] == null ? 0 : Convert.ToInt32(dr["idTipoVenta"].ToString()));
                        oVEN_DocVentaDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"].ToString());
                        oVEN_DocVentaDTO.idCliente = Convert.ToInt32(dr["idCliente"].ToString());
                        oVEN_DocVentaDTO.ClienteRazon = dr["ClienteRazon"].ToString();
                        oVEN_DocVentaDTO.ClienteDireccion = dr["ClienteDireccion"].ToString();
                        oVEN_DocVentaDTO.ClienteDocumento = dr["ClienteDocumento"].ToString();
                        oVEN_DocVentaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oVEN_DocVentaDTO.idPedido = Convert.ToInt32(dr["idPedido"].ToString());
                        oVEN_DocVentaDTO.idFormaPago = Convert.ToInt32(dr["idFormaPago"].ToString());
                        oVEN_DocVentaDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());
                        oVEN_DocVentaDTO.SerieDocumento = (dr["SerieDocumento"].ToString());
                        oVEN_DocVentaDTO.NumDocumento = (dr["NumDocumento"].ToString());
                        oVEN_DocVentaDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"].ToString());
                        oVEN_DocVentaDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"].ToString());
                        oVEN_DocVentaDTO.TotalDescuentoNacional = Convert.ToDecimal(dr["TotalDescuentoNacional"].ToString());
                        oVEN_DocVentaDTO.TotalDescuentoExtranjero = Convert.ToDecimal(dr["TotalDescuentoExtranjero"].ToString());
                        oVEN_DocVentaDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"].ToString());
                        oVEN_DocVentaDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"].ToString());
                        oVEN_DocVentaDTO.IGVExtranjero = Convert.ToDecimal(dr["IGVExtranjero"].ToString());
                        oVEN_DocVentaDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        oVEN_DocVentaDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"].ToString());
                        oVEN_DocVentaDTO.EstadoDoc = (dr["EstadoDoc"].ToString());
                        oVEN_DocVentaDTO.flgIGV = Convert.ToBoolean(dr["flgIGV"] == null ? false : Convert.ToBoolean(dr["flgIGV"].ToString()));
                        oVEN_DocVentaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_DocVentaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_DocVentaDTO.UsuarioCreacion = dr["UsuarioCreacion"].ToString();
                        oVEN_DocVentaDTO.UsuarioModificacion = dr["UsuarioModificacion"].ToString();
                        oVEN_DocVentaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oVEN_DocVentaDTO.ObservacionVenta = dr["ObservacionVenta"] == null ? "" : dr["ObservacionVenta"].ToString();
                        oVEN_DocVentaDTO.PorcDescuento = Convert.ToDecimal(dr["PorcDescuento"].ToString());
                        //oVEN_DocVentaDTO.TipoOperacion = Convert.ToInt32(dr["TipoOperacion"].ToString());
                        oResultDTO.ListaResultado.Add(oVEN_DocVentaDTO);
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
                    oResultDTO.ListaResultado = new List<VEN_DocVentaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_NotaCreditoDTO> ListarxID(int idNotaCredito)
        {
            ResultDTO<VEN_NotaCreditoDTO> oResultDTO = new ResultDTO<VEN_NotaCreditoDTO>();
            oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Nota_CreditoDebito_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idNotaCredito", idNotaCredito);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_NotaCreditoDTO oVEN_NotaCreditoDTO = new VEN_NotaCreditoDTO();
                        oVEN_NotaCreditoDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oVEN_NotaCreditoDTO.idNotaCredito = Convert.ToInt32(dr["idNotaCredito"].ToString());
                        oVEN_NotaCreditoDTO.TipoNota = dr["TipoNota"].ToString();
                        oVEN_NotaCreditoDTO.idDocVenta = Convert.ToInt64(dr["idDocVenta"].ToString());
                        oVEN_NotaCreditoDTO.Serie = dr["Serie"].ToString();
                        oVEN_NotaCreditoDTO.Numero = dr["Numero"].ToString();
                        oVEN_NotaCreditoDTO.Descripcion = dr["Descripcion"].ToString();
                        oVEN_NotaCreditoDTO.FechaEmision = Convert.ToDateTime(dr["FechaEmision"].ToString());
                        oVEN_NotaCreditoDTO.FechaRecepcion = Convert.ToDateTime(dr["FechaRecepcion"].ToString());
                        oVEN_NotaCreditoDTO.idMotivo = Convert.ToInt32(dr["idMotivo"].ToString());
                        oVEN_NotaCreditoDTO.Total = Convert.ToDecimal(dr["Total"].ToString());
                        oVEN_NotaCreditoDTO.IGV = Convert.ToDecimal(dr["IGV"].ToString());
                        oVEN_NotaCreditoDTO.TotalGravada = Convert.ToDecimal(dr["TotalGravada"].ToString());
                        oVEN_NotaCreditoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_NotaCreditoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_NotaCreditoDTO.UsuarioCreacion = dr["UsuarioCreacion"].ToString();
                        oVEN_NotaCreditoDTO.UsuarioModificacion = dr["UsuarioModificacion"].ToString();
                        oVEN_NotaCreditoDTO.Cliente = dr["Cliente"].ToString();
                        oVEN_NotaCreditoDTO.ClienteDocumento = dr["ClienteDocumento"].ToString();
                        oVEN_NotaCreditoDTO.NumDocRef = dr["NumDocRef"].ToString();
                        oVEN_NotaCreditoDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocRef"].ToString());
                        oVEN_NotaCreditoDTO.idTipoVenta = Convert.ToInt32(dr["idTipoVenta"].ToString());
                        oVEN_NotaCreditoDTO.ClienteDireccion = dr["ClienteDireccion"].ToString();
                        oVEN_NotaCreditoDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oVEN_NotaCreditoDTO.idTipoDocumentoRef = Convert.ToInt32(dr["idTipoDocumento"].ToString());

                        oResultDTO.ListaResultado.Add(oVEN_NotaCreditoDTO);
                    }

                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<VEN_NotaCredito_DetalleDTO> lista = new List<VEN_NotaCredito_DetalleDTO>();
                            while (dr.Read())
                            {
                                VEN_NotaCredito_DetalleDTO oVEN_NotaCredito_DetalleDTO = new VEN_NotaCredito_DetalleDTO();
                                oVEN_NotaCredito_DetalleDTO.idNotaCreditoDetalle = Convert.ToInt32(dr["idNotaCreditoDetalle"] == null ? 0 : Convert.ToInt32(dr["idNotaCreditoDetalle"].ToString()));
                                oVEN_NotaCredito_DetalleDTO.idNotaCredito = Convert.ToInt32(dr["idNotaCredito"] == null ? 0 : Convert.ToInt32(dr["idNotaCredito"].ToString()));
                                oVEN_NotaCredito_DetalleDTO.idArticulo = Convert.ToInt32(dr["idArticulo"] == null ? 0 : Convert.ToInt32(dr["idArticulo"].ToString()));
                                oVEN_NotaCredito_DetalleDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"] == null ? 0 : Convert.ToDecimal(dr["Cantidad"].ToString()));
                                oVEN_NotaCredito_DetalleDTO.UnidadMedida = dr["UnidadMedida"] == null ? "" : dr["UnidadMedida"].ToString();
                                oVEN_NotaCredito_DetalleDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                                oVEN_NotaCredito_DetalleDTO.ValorUnitario = Convert.ToDecimal(dr["ValorUnitario"] == null ? 0 : Convert.ToDecimal(dr["ValorUnitario"].ToString()));
                                oVEN_NotaCredito_DetalleDTO.Descuento = Convert.ToDecimal(dr["Descuento"] == null ? 0 : Convert.ToDecimal(dr["Descuento"].ToString()));
                                oVEN_NotaCredito_DetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                                oVEN_NotaCredito_DetalleDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                                oVEN_NotaCredito_DetalleDTO.UsuarioCreacion = dr["UsuarioCreacion"] == null ? "" : dr["UsuarioCreacion"].ToString();
                                oVEN_NotaCredito_DetalleDTO.UsuarioModificacion = dr["UsuarioModificacion"] == null ? "" : dr["UsuarioModificacion"].ToString();
                                lista.Add(oVEN_NotaCredito_DetalleDTO);
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
                    oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_NotaCreditoDTO> UpdateInsert(VEN_NotaCreditoDTO oVEN_NotaCredito, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<VEN_NotaCreditoDTO> oResultDTO = new ResultDTO<VEN_NotaCreditoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Nota_CreditoDebito_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idNotaCredito", oVEN_NotaCredito.idNotaCredito);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oVEN_NotaCredito.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@TipoNota", oVEN_NotaCredito.idMotivo);
                        da.SelectCommand.Parameters.AddWithValue("@idDocVenta", oVEN_NotaCredito.idDocVenta);
                        da.SelectCommand.Parameters.AddWithValue("@Serie", oVEN_NotaCredito.Serie);
                        da.SelectCommand.Parameters.AddWithValue("@Numero", oVEN_NotaCredito.Numero);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oVEN_NotaCredito.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaEmision", oVEN_NotaCredito.FechaEmision);
                        da.SelectCommand.Parameters.AddWithValue("@FechaRecepcion", oVEN_NotaCredito.FechaRecepcion);
                        da.SelectCommand.Parameters.AddWithValue("@idMotivo", oVEN_NotaCredito.idMotivo);
                        da.SelectCommand.Parameters.AddWithValue("@Total", oVEN_NotaCredito.Total);
                        da.SelectCommand.Parameters.AddWithValue("@IGV", oVEN_NotaCredito.IGV);
                        da.SelectCommand.Parameters.AddWithValue("@TotalGravada", oVEN_NotaCredito.TotalGravada);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oVEN_NotaCredito.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oVEN_NotaCredito.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@ListaDetalle", oVEN_NotaCredito.cadDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@idCliente", oVEN_NotaCredito.idCliente);
                        da.SelectCommand.Parameters.AddWithValue("@TipoDoc", oVEN_NotaCredito.TipoDoc);
                        da.SelectCommand.Parameters.AddWithValue("@RespuestaSunat", oVEN_NotaCredito.Descripcion_Sunat);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoSunat", oVEN_NotaCredito.EstadoSunat);
                        da.SelectCommand.Parameters.AddWithValue("@AceptaSunat", oVEN_NotaCredito.Aceptada_Sunat);
                        da.SelectCommand.Parameters.AddWithValue("@Enlace", oVEN_NotaCredito.Enlace);
                        da.SelectCommand.Parameters.AddWithValue("@idMoneda", oVEN_NotaCredito.idMoneda);
                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarRangoFechas(1, fechaInicio, fechaFin, "", "", cn).ListaResultado;
                            oResultDTO.Campo1 = id_output.Value.ToString();
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_SerieDTO> ObtenerNumero(string serie, string tipo)//int idSerie 
        {
            ResultDTO<VEN_SerieDTO> oResultDTO = new ResultDTO<VEN_SerieDTO>();
            oResultDTO.ListaResultado = new List<VEN_SerieDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Nota_CreditoDebito_ObtenerNumero", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //da.SelectCommand.Parameters.AddWithValue("@idSerie", idSerie);
                    da.SelectCommand.Parameters.AddWithValue("@Serie", serie);
                    da.SelectCommand.Parameters.AddWithValue("@Tipo", tipo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_SerieDTO oVEN_SerieDTO = new VEN_SerieDTO();
                        oVEN_SerieDTO.NroSerie = dr["NumeroSerie"] == null ? "" : dr["NumeroSerie"].ToString();
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
        public string ValidarNotaCredito(VEN_NotaCreditoDTO oNotaCredito, SqlConnection cn = null)
        {
            ResultDTO<VEN_NotaCreditoDTO> oResultDTO = new ResultDTO<VEN_NotaCreditoDTO>();
            string dato = "";
            oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_NotaCredito_Validar", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdNotaCredito", oNotaCredito.idNotaCredito);
                    da.SelectCommand.Parameters.AddWithValue("@Serie", oNotaCredito.Serie);
                    da.SelectCommand.Parameters.AddWithValue("@Numero", oNotaCredito.Numero);

                    SqlDataReader dr = da.SelectCommand.ExecuteReader();

                    while (dr.Read())
                    {
                        VEN_NotaCreditoDTO oVEN_NotaCreditoDTO = new VEN_NotaCreditoDTO();
                        dato = dr["idNotaCredito"] == null ? "" : dr["idNotaCredito"].ToString();
                        ;
                    }

                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
                }
            }
            return dato;
        }
        public ResultDTO<VEN_NotaCreditoDTO> Anular(VEN_NotaCreditoDTO oVEN_NotaCreditoDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<VEN_NotaCreditoDTO> oResultDTO = new ResultDTO<VEN_NotaCreditoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_VEN_NotaCredito_Anular", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idNotaCredito", oVEN_NotaCreditoDTO.idNotaCredito);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarRangoFechas(1, fechaInicio, fechaFin, "", "", cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public VEN_NotaCreditoDTO ListarxIDNota(int idNotaCredito)
        {
            VEN_NotaCreditoDTO oResultDTO = new VEN_NotaCreditoDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Nota_CreditoDebito_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idNotaCredito", idNotaCredito);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_NotaCreditoDTO oVEN_NotaCreditoDTO = new VEN_NotaCreditoDTO();
                        oVEN_NotaCreditoDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oVEN_NotaCreditoDTO.idNotaCredito = Convert.ToInt32(dr["idNotaCredito"].ToString());
                        oVEN_NotaCreditoDTO.TipoNota = dr["TipoNota"].ToString();
                        oVEN_NotaCreditoDTO.idDocVenta = Convert.ToInt64(dr["idDocVenta"].ToString());
                        oVEN_NotaCreditoDTO.Serie = dr["Serie"].ToString();
                        oVEN_NotaCreditoDTO.Numero = dr["Numero"].ToString();
                        oVEN_NotaCreditoDTO.Descripcion = dr["Descripcion"].ToString();
                        oVEN_NotaCreditoDTO.FechaEmision = Convert.ToDateTime(dr["FechaEmision"].ToString());
                        oVEN_NotaCreditoDTO.FechaRecepcion = Convert.ToDateTime(dr["FechaRecepcion"].ToString());
                        oVEN_NotaCreditoDTO.idMotivo = Convert.ToInt32(dr["idMotivo"].ToString());
                        oVEN_NotaCreditoDTO.Total = Convert.ToDecimal(dr["Total"].ToString());
                        oVEN_NotaCreditoDTO.IGV = Convert.ToDecimal(dr["IGV"].ToString());
                        oVEN_NotaCreditoDTO.TotalGravada = Convert.ToDecimal(dr["TotalGravada"].ToString());
                        oVEN_NotaCreditoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oVEN_NotaCreditoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oVEN_NotaCreditoDTO.UsuarioCreacion = dr["UsuarioCreacion"].ToString();
                        oVEN_NotaCreditoDTO.UsuarioModificacion = dr["UsuarioModificacion"].ToString();
                        oVEN_NotaCreditoDTO.Cliente = dr["Cliente"].ToString();
                        oVEN_NotaCreditoDTO.ClienteDocumento = dr["ClienteDocumento"].ToString();
                        oVEN_NotaCreditoDTO.NumDocRef = dr["NumDocRef"].ToString();
                        oVEN_NotaCreditoDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocRef"].ToString());
                        oVEN_NotaCreditoDTO.idTipoVenta = Convert.ToInt32(dr["idTipoVenta"].ToString());
                        oVEN_NotaCreditoDTO.ClienteDireccion = dr["ClienteDireccion"].ToString();
                        oVEN_NotaCreditoDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oVEN_NotaCreditoDTO.idTipoDocumentoRef = Convert.ToInt32(dr["idTipoDocumento"].ToString());
                        oResultDTO = oVEN_NotaCreditoDTO;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return oResultDTO;
        }
        public int validar_notacredito_delete(int idDocumentoVenta, SqlConnection cn = null)
        {
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Factura_Delete_ValidarNota", cn);
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
        public ResultDTO<VEN_NotaCreditoDTO> ListarxID_DatosImpresion(int idNotaCredito)
        {
            ResultDTO<VEN_NotaCreditoDTO> oResultDTO = new ResultDTO<VEN_NotaCreditoDTO>();
            oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Nota_CreditoDebito_ListarxID_DatosImpresion", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idNotaCredito", idNotaCredito);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_NotaCreditoDTO oVEN_NotaCreditoDTO = new VEN_NotaCreditoDTO();
                        oVEN_NotaCreditoDTO.Serie = dr["Serie"].ToString();
                        oVEN_NotaCreditoDTO.Numero = dr["Numero"].ToString();
                        oVEN_NotaCreditoDTO.Cliente = dr["Cliente"].ToString();
                        oVEN_NotaCreditoDTO.ClienteDocumento = dr["ClienteDocumento"].ToString();
                        oVEN_NotaCreditoDTO.ClienteDireccion = dr["ClienteDireccion"].ToString();
                        oVEN_NotaCreditoDTO.FechaEmision = Convert.ToDateTime(dr["FechaEmision"].ToString());
                        oVEN_NotaCreditoDTO.ReferenciaDocumento = dr["ReferenciaDocumento"].ToString(); //DOC REFERENCIA
                        oVEN_NotaCreditoDTO.NumeroDocumento = dr["NumeroDocumento"].ToString();//NRO DCTO
                        oVEN_NotaCreditoDTO.ImporteDocumento = Convert.ToDecimal(dr["ImporteDocumento"].ToString());//IMPORTE 
                        oVEN_NotaCreditoDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"].ToString());//FECHA
                        oVEN_NotaCreditoDTO.Descripcion = dr["Descripcion"].ToString();
                        oVEN_NotaCreditoDTO.Motivo = dr["Motivo"].ToString();
                        oVEN_NotaCreditoDTO.Total = Convert.ToDecimal(dr["Total"].ToString());
                        oVEN_NotaCreditoDTO.IGV = Convert.ToDecimal(dr["IGV"].ToString());
                        oVEN_NotaCreditoDTO.TotalGravada = Convert.ToDecimal(dr["TotalGravada"].ToString());
                        oVEN_NotaCreditoDTO.Enlace = dr["Enlace"].ToString();
                        oVEN_NotaCreditoDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oResultDTO.ListaResultado.Add(oVEN_NotaCreditoDTO);
                    }

                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<VEN_NotaCredito_DetalleDTO> lista = new List<VEN_NotaCredito_DetalleDTO>();
                            while (dr.Read())
                            {
                                VEN_NotaCredito_DetalleDTO oVEN_NotaCredito_DetalleDTO = new VEN_NotaCredito_DetalleDTO();
                                oVEN_NotaCredito_DetalleDTO.Cantidad = Convert.ToDecimal(dr["Cantidad"] == null ? 0 : Convert.ToDecimal(dr["Cantidad"].ToString()));
                                oVEN_NotaCredito_DetalleDTO.UnidadMedida = dr["UnidadMedida"] == null ? "" : dr["UnidadMedida"].ToString();
                                oVEN_NotaCredito_DetalleDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                                oVEN_NotaCredito_DetalleDTO.ValorUnitario = Convert.ToDecimal(dr["ValorUnitario"] == null ? 0 : Convert.ToDecimal(dr["ValorUnitario"].ToString()));
                                oVEN_NotaCredito_DetalleDTO.Total = Convert.ToDecimal(dr["Total"] == null ? 0 : Convert.ToDecimal(dr["Total"].ToString()));
                                lista.Add(oVEN_NotaCredito_DetalleDTO);
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
                    oResultDTO.ListaResultado = new List<VEN_NotaCreditoDTO>();
                }
            }
            return oResultDTO;
        }

        public int validar_NotaCredito(int idNotaCredito, SqlConnection cn = null)
        {
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_NotaCredito_ValidarDocumento", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idNotaCredito", idNotaCredito);
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
