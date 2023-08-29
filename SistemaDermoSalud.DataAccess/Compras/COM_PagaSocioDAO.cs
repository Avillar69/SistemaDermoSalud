using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Compras;
using SistemaDermoSalud.Entities.Finanzas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SistemaDermoSalud.DataAccess.Compras
{
    public class COM_PagaSocioDAO
    {
        public ResultDTO<COM_PagaSocioDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<COM_PagaSocioDTO> oResultDTO = new ResultDTO<COM_PagaSocioDTO>();
            oResultDTO.ListaResultado = new List<COM_PagaSocioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_PagarSocio", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_PagaSocioDTO oCOM_OrdenCompraDTO = new COM_PagaSocioDTO();
                        decimal a = Convert.ToDecimal(dr["MontoXPagar"] == null ? 0 : Convert.ToDecimal(dr["MontoXPagar"].ToString()));
                        if (a > 0)
                        {
                            oCOM_OrdenCompraDTO.idDocumento = dr["Documento"] == null ? "" : dr["Documento"].ToString();
                            oCOM_OrdenCompraDTO.DescripcionSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                            oCOM_OrdenCompraDTO.MontoTotal = Convert.ToDecimal(dr["MontoTotal"] == null ? 0 : Convert.ToDecimal(dr["MontoTotal"].ToString()));
                            oCOM_OrdenCompraDTO.MontoAplicado = Convert.ToDecimal(dr["MontoAplicado"] == null ? 0 : Convert.ToDecimal(dr["MontoAplicado"].ToString()));
                            oCOM_OrdenCompraDTO.MontoXPagar = Convert.ToDecimal(dr["MontoXPagar"] == null ? 0 : Convert.ToDecimal(dr["MontoXPagar"].ToString()));
                            oResultDTO.ListaResultado.Add(oCOM_OrdenCompraDTO);
                        }
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_PagaSocioDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_PagosDetalle> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, SqlConnection cn = null)
        {
            ResultDTO<FN_PagosDetalle> oResultDTO = new ResultDTO<FN_PagosDetalle>();
            oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_PagoDetalle_ListarRangoFecha", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_PagosDetalle oFN_PagosDetalle = new FN_PagosDetalle();
                        oFN_PagosDetalle.idPagoDetalle = Convert.ToInt32(dr["idPagoDetalle"] == null ? 0 : Convert.ToInt32(dr["idPagoDetalle"].ToString()));
                        oFN_PagosDetalle.idPago = Convert.ToInt32(dr["idPago"] == null ? 0 : Convert.ToInt32(dr["idPago"].ToString()));
                        oFN_PagosDetalle.idCajaDetalle = Convert.ToInt32(dr["idCajaDetalle"] == null ? 0 : Convert.ToInt32(dr["idCajaDetalle"].ToString()));
                        oFN_PagosDetalle.idTipo = Convert.ToInt32(dr["idTipoOperacion"] == null ? 0 : Convert.ToInt32(dr["idTipoOperacion"].ToString()));
                        oFN_PagosDetalle.idDocumento = Convert.ToInt32(dr["idDocumento"] == null ? 0 : Convert.ToInt32(dr["idDocumento"].ToString()));
                        oFN_PagosDetalle.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oFN_PagosDetalle.idConcepto = Convert.ToInt32(dr["idConcepto"] == null ? 0 : Convert.ToInt32(dr["idConcepto"].ToString()));
                        oFN_PagosDetalle.Concepto = dr["Concepto"] == null ? "" : dr["Concepto"].ToString();
                        oFN_PagosDetalle.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oFN_PagosDetalle.DescripcionFormaPago = dr["DescripcionFormaPago"] == null ? "" : dr["DescripcionFormaPago"].ToString();
                        oFN_PagosDetalle.NumeroOperacion = dr["NumeroOperacion"] == null ? "" : dr["NumeroOperacion"].ToString();
                        oFN_PagosDetalle.idCuentaBancario = Convert.ToInt32(dr["idCuentaBancario"] == null ? 0 : Convert.ToInt32(dr["idCuentaBancario"].ToString()));
                        oFN_PagosDetalle.NumeroCuenta = dr["NumeroCuenta"].ToString();
                        oFN_PagosDetalle.Monto = Convert.ToDecimal(dr["Monto"] == null ? 0 : Convert.ToDecimal(dr["Monto"].ToString()));
                        oFN_PagosDetalle.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_PagosDetalle.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_PagosDetalle.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_PagosDetalle.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_PagosDetalle.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oFN_PagosDetalle.Observacion = dr["Observacion"] == null ? "" : dr["Observacion"].ToString();
                        oFN_PagosDetalle.DescripcionOperacion = dr["DescripcionOperacion"] == null ? "" : dr["DescripcionOperacion"].ToString();
                        oFN_PagosDetalle.idCuentaBancarioDestino = Convert.ToInt32(dr["idCuentaBancarioDestino"] == null ? 0 : Convert.ToInt32(dr["idCuentaBancarioDestino"].ToString()));
                        oFN_PagosDetalle.NumeroCuentaDestino = dr["NumeroCuentaDestino"].ToString();
                        oFN_PagosDetalle.FechaDetalle = Convert.ToDateTime(dr["FechaDetalle"].ToString());

                        oFN_PagosDetalle.SerieDcto = dr["SerieDcto"] == null ? "" : dr["SerieDcto"].ToString();
                        oFN_PagosDetalle.NumeroDcto = dr["NumeroDcto"] == null ? "" : dr["NumeroDcto"].ToString();

                        oResultDTO.ListaResultado.Add(oFN_PagosDetalle);
                        oResultDTO.Resultado = "OK";
                    }
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_PagosDetalle> BuscarPago(int id, int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<FN_PagosDetalle> oResultDTO = new ResultDTO<FN_PagosDetalle>();
            oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_BuscarPago", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idDocumento", id);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_PagosDetalle oFN_PagosDetalle = new FN_PagosDetalle();
                        oFN_PagosDetalle.idPagoDetalle = Convert.ToInt32(dr["idPagoDetalle"] == null ? 0 : Convert.ToInt32(dr["idPagoDetalle"].ToString()));
                        oFN_PagosDetalle.idDocumentoCompraVenta = Convert.ToInt32(dr["idDocumentoCV"] == null ? 0 : Convert.ToInt32(dr["idDocumentoCV"].ToString()));
                        oResultDTO.ListaResultado.Add(oFN_PagosDetalle);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
                }
            }
            return oResultDTO;
        }



        public ResultDTO<FN_PagosDetalle> UpdateInsert(FN_PagosDetalle oFN_PagosDetalle, DateTime FechaInicio, DateTime FechaFin)
        {
            ResultDTO<FN_PagosDetalle> oResultDTO = new ResultDTO<FN_PagosDetalle>();
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
                        SqlDataAdapter pd = new SqlDataAdapter("SP_FN_PagosDetalle_UpdateInsert", cn);
                        pd.SelectCommand.CommandType = CommandType.StoredProcedure;

                        pd.SelectCommand.Parameters.AddWithValue("@idPagoDetalle", oFN_PagosDetalle.idPagoDetalle);
                        pd.SelectCommand.Parameters.AddWithValue("@idPago", oFN_PagosDetalle.idPago);
                        pd.SelectCommand.Parameters.AddWithValue("@idEmpresa", oFN_PagosDetalle.idEmpresa);
                        pd.SelectCommand.Parameters.AddWithValue("@idCajaDetalle", oFN_PagosDetalle.idCajaDetalle);
                        pd.SelectCommand.Parameters.AddWithValue("@Observacion", oFN_PagosDetalle.Observacion);
                        pd.SelectCommand.Parameters.AddWithValue("@idTipoOperacion", oFN_PagosDetalle.idTipo);
                        pd.SelectCommand.Parameters.AddWithValue("@NumeroOperacion", oFN_PagosDetalle.NumeroOperacion);
                        pd.SelectCommand.Parameters.AddWithValue("@DescripcionOperacion", oFN_PagosDetalle.DescripcionOperacion);
                        pd.SelectCommand.Parameters.AddWithValue("@idDocumento", oFN_PagosDetalle.idDocumento);
                        pd.SelectCommand.Parameters.AddWithValue("@idConcepto", oFN_PagosDetalle.idConcepto);
                        pd.SelectCommand.Parameters.AddWithValue("@Concepto", oFN_PagosDetalle.Concepto);
                        pd.SelectCommand.Parameters.AddWithValue("@idFormaPago", oFN_PagosDetalle.idFormaPago);
                        pd.SelectCommand.Parameters.AddWithValue("@DescripcionFormaPago", oFN_PagosDetalle.DescripcionFormaPago);
                        pd.SelectCommand.Parameters.AddWithValue("@idCuentaBancario", oFN_PagosDetalle.idCuentaBancario);
                        pd.SelectCommand.Parameters.AddWithValue("@NumeroCuenta", oFN_PagosDetalle.NumeroCuenta);
                        pd.SelectCommand.Parameters.AddWithValue("@Monto", oFN_PagosDetalle.Monto);
                        pd.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oFN_PagosDetalle.UsuarioCreacion);
                        pd.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oFN_PagosDetalle.UsuarioModificacion);
                        pd.SelectCommand.Parameters.AddWithValue("@Estado", oFN_PagosDetalle.Estado);
                        pd.SelectCommand.Parameters.AddWithValue("@idCuentaBancarioDestino", oFN_PagosDetalle.idCuentaBancarioDestino);
                        pd.SelectCommand.Parameters.AddWithValue("@NumeroCuentaDestino", oFN_PagosDetalle.NumeroCuentaDestino);
                        pd.SelectCommand.Parameters.AddWithValue("@FechaDetalle", oFN_PagosDetalle.FechaDetalle);
                        //agregar un campo de fechaDetalle
                        int rpta = pd.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            //oResultDTO.ListaResultado = ListarTodoDetallePago(cn).ListaResultado;
                            oResultDTO.ListaResultado = ListarRangoFecha(oFN_PagosDetalle.idEmpresa, FechaInicio, FechaFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
                    }
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_PagosDetalle> Delete(FN_PagosDetalle oCOM_OrdenCompra, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<FN_PagosDetalle> oResultDTO = new ResultDTO<FN_PagosDetalle>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_PagosDetalle_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idPagoDetalle", oCOM_OrdenCompra.idPagoDetalle);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodoDetallePago(cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
                    }
                }
            }
            return oResultDTO;
        }


        public ResultDTO<FN_PagosDetalle> ListarTodoDetallePago(SqlConnection cn = null)
        {
            ResultDTO<FN_PagosDetalle> oResultDTO = new ResultDTO<FN_PagosDetalle>();
            oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_PagoDetalle_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_PagosDetalle oFN_PagosDetalle = new FN_PagosDetalle();
                        oFN_PagosDetalle.idPagoDetalle = Convert.ToInt32(dr["idPagoDetalle"] == null ? 0 : Convert.ToInt32(dr["idPagoDetalle"].ToString()));
                        oFN_PagosDetalle.idPago = Convert.ToInt32(dr["idPago"] == null ? 0 : Convert.ToInt32(dr["idPago"].ToString()));
                        oFN_PagosDetalle.idCajaDetalle = Convert.ToInt32(dr["idCajaDetalle"] == null ? 0 : Convert.ToInt32(dr["idCajaDetalle"].ToString()));
                        oFN_PagosDetalle.idTipo = Convert.ToInt32(dr["idTipoOperacion"] == null ? 0 : Convert.ToInt32(dr["idTipoOperacion"].ToString()));
                        oFN_PagosDetalle.idDocumento = Convert.ToInt32(dr["idDocumento"] == null ? 0 : Convert.ToInt32(dr["idDocumento"].ToString()));
                        oFN_PagosDetalle.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oFN_PagosDetalle.idConcepto = Convert.ToInt32(dr["idConcepto"] == null ? 0 : Convert.ToInt32(dr["idConcepto"].ToString()));
                        oFN_PagosDetalle.Concepto = dr["Concepto"] == null ? "" : dr["Concepto"].ToString();
                        oFN_PagosDetalle.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oFN_PagosDetalle.DescripcionFormaPago = dr["DescripcionFormaPago"] == null ? "" : dr["DescripcionFormaPago"].ToString();
                        oFN_PagosDetalle.NumeroOperacion = dr["NumeroOperacion"] == null ? "" : dr["NumeroOperacion"].ToString();
                        oFN_PagosDetalle.idCuentaBancario = Convert.ToInt32(dr["idCuentaBancario"] == null ? 0 : Convert.ToInt32(dr["idCuentaBancario"].ToString()));
                        oFN_PagosDetalle.NumeroCuenta = dr["NumeroCuenta"].ToString();
                        oFN_PagosDetalle.Monto = Convert.ToDecimal(dr["Monto"] == null ? 0 : Convert.ToDecimal(dr["Monto"].ToString()));
                        oFN_PagosDetalle.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_PagosDetalle.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_PagosDetalle.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_PagosDetalle.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_PagosDetalle.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oFN_PagosDetalle.Observacion = dr["Observacion"] == null ? "" : dr["Observacion"].ToString();
                        oFN_PagosDetalle.DescripcionOperacion = dr["DescripcionOperacion"] == null ? "" : dr["DescripcionOperacion"].ToString();
                        oFN_PagosDetalle.idCuentaBancarioDestino = Convert.ToInt32(dr["idCuentaBancarioDestino"] == null ? 0 : Convert.ToInt32(dr["idCuentaBancarioDestino"].ToString()));
                        oFN_PagosDetalle.NumeroCuentaDestino = dr["NumeroCuentaDestino"].ToString();
                        oFN_PagosDetalle.FechaDetalle = Convert.ToDateTime(dr["FechaDetalle"].ToString());

                        oFN_PagosDetalle.SerieDcto = dr["SerieDcto"] == null ? "" : dr["SerieDcto"].ToString();
                        oFN_PagosDetalle.NumeroDcto = dr["NumeroDcto"] == null ? "" : dr["NumeroDcto"].ToString();

                        oResultDTO.ListaResultado.Add(oFN_PagosDetalle);
                        oResultDTO.Resultado = "OK";
                    }
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_PagosDetalle> ListarTodoDetallePagoCliente(SqlConnection cn = null)
        {
            ResultDTO<FN_PagosDetalle> oResultDTO = new ResultDTO<FN_PagosDetalle>();
            oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_PagoDetalle_ListarTodoCliente", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_PagosDetalle oFN_PagosDetalle = new FN_PagosDetalle();
                        oFN_PagosDetalle.idPagoDetalle = Convert.ToInt32(dr["idPagoDetalle"] == null ? 0 : Convert.ToInt32(dr["idPagoDetalle"].ToString()));
                        oFN_PagosDetalle.idPago = Convert.ToInt32(dr["idPago"] == null ? 0 : Convert.ToInt32(dr["idPago"].ToString()));
                        oFN_PagosDetalle.idCajaDetalle = Convert.ToInt32(dr["idCajaDetalle"] == null ? 0 : Convert.ToInt32(dr["idCajaDetalle"].ToString()));
                        oFN_PagosDetalle.idTipo = Convert.ToInt32(dr["idTipoOperacion"] == null ? 0 : Convert.ToInt32(dr["idTipoOperacion"].ToString()));
                        oFN_PagosDetalle.idDocumento = Convert.ToInt32(dr["idDocumento"] == null ? 0 : Convert.ToInt32(dr["idDocumento"].ToString()));
                        oFN_PagosDetalle.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oFN_PagosDetalle.idConcepto = Convert.ToInt32(dr["idConcepto"] == null ? 0 : Convert.ToInt32(dr["idConcepto"].ToString()));
                        oFN_PagosDetalle.Concepto = dr["Concepto"] == null ? "" : dr["Concepto"].ToString();
                        oFN_PagosDetalle.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oFN_PagosDetalle.DescripcionFormaPago = dr["DescripcionFormaPago"] == null ? "" : dr["DescripcionFormaPago"].ToString();
                        oFN_PagosDetalle.NumeroOperacion = dr["NumeroOperacion"] == null ? "" : dr["NumeroOperacion"].ToString();
                        oFN_PagosDetalle.idCuentaBancario = Convert.ToInt32(dr["idCuentaBancario"] == null ? 0 : Convert.ToInt32(dr["idCuentaBancario"].ToString()));
                        oFN_PagosDetalle.NumeroCuenta = dr["NumeroCuenta"].ToString();
                        oFN_PagosDetalle.Monto = Convert.ToDecimal(dr["Monto"] == null ? 0 : Convert.ToDecimal(dr["Monto"].ToString()));
                        oFN_PagosDetalle.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_PagosDetalle.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_PagosDetalle.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_PagosDetalle.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_PagosDetalle.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oFN_PagosDetalle.Observacion = dr["Observacion"] == null ? "" : dr["Observacion"].ToString();
                        oFN_PagosDetalle.DescripcionOperacion = dr["DescripcionOperacion"] == null ? "" : dr["DescripcionOperacion"].ToString();
                        oFN_PagosDetalle.idCuentaBancarioDestino = Convert.ToInt32(dr["idCuentaBancarioDestino"] == null ? 0 : Convert.ToInt32(dr["idCuentaBancarioDestino"].ToString()));
                        oFN_PagosDetalle.NumeroCuentaDestino = dr["NumeroCuentaDestino"].ToString();
                        oFN_PagosDetalle.FechaDetalle = Convert.ToDateTime(dr["FechaDetalle"].ToString());

                        oFN_PagosDetalle.SerieDcto = dr["SerieDcto"] == null ? "" : dr["SerieDcto"].ToString();
                        oFN_PagosDetalle.NumeroDcto = dr["NumeroDcto"] == null ? "" : dr["NumeroDcto"].ToString();

                        oResultDTO.ListaResultado.Add(oFN_PagosDetalle);
                        oResultDTO.Resultado = "OK";
                    }
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_PagosDetalle> ListarXIDPagoDetalle(int idPagoDetalle, SqlConnection cn = null)
        {
            ResultDTO<FN_PagosDetalle> oResultDTO = new ResultDTO<FN_PagosDetalle>();
            oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_PagosDetalle_ListarXID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idPagoDetalle", idPagoDetalle);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_PagosDetalle oFN_PagosDetalle = new FN_PagosDetalle();
                        oFN_PagosDetalle.idPagoDetalle = Convert.ToInt32(dr["idPagoDetalle"] == null ? 0 : Convert.ToInt32(dr["idPagoDetalle"].ToString()));
                        oFN_PagosDetalle.idPago = Convert.ToInt32(dr["idPago"] == null ? 0 : Convert.ToInt32(dr["idPago"].ToString()));
                        oFN_PagosDetalle.idCajaDetalle = Convert.ToInt32(dr["idCajaDetalle"] == null ? 0 : Convert.ToInt32(dr["idCajaDetalle"].ToString()));
                        oFN_PagosDetalle.idTipo = Convert.ToInt32(dr["idTipoOperacion"] == null ? 0 : Convert.ToInt32(dr["idTipoOperacion"].ToString()));
                        oFN_PagosDetalle.idDocumento = Convert.ToInt32(dr["idDocumento"] == null ? 0 : Convert.ToInt32(dr["idDocumento"].ToString()));
                        oFN_PagosDetalle.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oFN_PagosDetalle.idConcepto = Convert.ToInt32(dr["idConcepto"] == null ? 0 : Convert.ToInt32(dr["idConcepto"].ToString()));
                        oFN_PagosDetalle.Concepto = dr["Concepto"] == null ? "" : dr["Concepto"].ToString();
                        oFN_PagosDetalle.idFormaPago = Convert.ToInt32(dr["idFormaPago"] == null ? 0 : Convert.ToInt32(dr["idFormaPago"].ToString()));
                        oFN_PagosDetalle.DescripcionFormaPago = dr["DescripcionFormaPago"] == null ? "" : dr["DescripcionFormaPago"].ToString();
                        oFN_PagosDetalle.NumeroOperacion = dr["NumeroOperacion"] == null ? "" : dr["NumeroOperacion"].ToString();
                        oFN_PagosDetalle.idCuentaBancario = Convert.ToInt32(dr["idCuentaBancario"] == null ? 0 : Convert.ToInt32(dr["idCuentaBancario"].ToString()));
                        oFN_PagosDetalle.NumeroCuenta = dr["NumeroCuenta"].ToString();
                        oFN_PagosDetalle.Monto = Convert.ToDecimal(dr["Monto"] == null ? 0 : Convert.ToDecimal(dr["Monto"].ToString()));
                        oFN_PagosDetalle.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_PagosDetalle.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_PagosDetalle.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_PagosDetalle.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_PagosDetalle.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oFN_PagosDetalle.Observacion = dr["Observacion"] == null ? "" : dr["Observacion"].ToString();
                        oFN_PagosDetalle.DescripcionOperacion = dr["DescripcionOperacion"] == null ? "" : dr["DescripcionOperacion"].ToString();
                        oFN_PagosDetalle.idCuentaBancarioDestino = Convert.ToInt32(dr["idCuentaBancarioDestino"] == null ? 0 : Convert.ToInt32(dr["idCuentaBancarioDestino"].ToString()));
                        oFN_PagosDetalle.NumeroCuentaDestino = dr["NumeroCuentaDestino"].ToString();
                        oFN_PagosDetalle.FechaDetalle = Convert.ToDateTime(dr["FechaDetalle"].ToString());

                        oFN_PagosDetalle.idTipoDocumento = Convert.ToInt32(dr["idTipo"] == null ? 0 : Convert.ToInt32(dr["idTipo"].ToString()));
                        oFN_PagosDetalle.idDocumentoCompraVenta = Convert.ToInt32(dr["idDocCompraVenta"] == null ? 0 : Convert.ToInt32(dr["idDocCompraVenta"].ToString()));
                        oFN_PagosDetalle.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oFN_PagosDetalle.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oFN_PagosDetalle.MontoxCobrar = Convert.ToDecimal(dr["MontoxCobrar"] == null ? 0 : Convert.ToDecimal(dr["MontoxCobrar"].ToString()));
                        oFN_PagosDetalle.MontoxPagar = Convert.ToDecimal(dr["MontoxPagar"] == null ? 0 : Convert.ToDecimal(dr["MontoxPagar"].ToString()));
                        oFN_PagosDetalle.MontoAplicado = Convert.ToDecimal(dr["MontoAplicado"] == null ? 0 : Convert.ToDecimal(dr["MontoAplicado"].ToString()));
                        oFN_PagosDetalle.SaldoxAplicar = Convert.ToDecimal(dr["SaldoxAplicar"] == null ? 0 : Convert.ToDecimal(dr["SaldoxAplicar"].ToString()));
                        oFN_PagosDetalle.FechaCreacionPago = Convert.ToDateTime(dr["FechaCreacion"].ToString());

                        oFN_PagosDetalle.SerieDcto = dr["SerieDcto"] == null ? "" : dr["SerieDcto"].ToString();
                        oFN_PagosDetalle.NumeroDcto = dr["NumeroDcto"] == null ? "" : dr["NumeroDcto"].ToString();

                        oResultDTO.ListaResultado.Add(oFN_PagosDetalle);
                        oResultDTO.Resultado = "OK";
                    }
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDetalle>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<COM_CuentasUsuariosDTO> ListarTodoProveedores(int id, SqlConnection cn = null)//Cuenta Destino
        {
            ResultDTO<COM_CuentasUsuariosDTO> oResultDTO = new ResultDTO<COM_CuentasUsuariosDTO>();
            oResultDTO.ListaResultado = new List<COM_CuentasUsuariosDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_CuentasSocio_ListarTodo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idSocioNegocio", id);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_CuentasUsuariosDTO oCOM_CuentasUsuariosDTO = new COM_CuentasUsuariosDTO();
                        oCOM_CuentasUsuariosDTO.idCuentaBancaria = Convert.ToInt32(dr["idCuentaBancaria"] == null ? 0 : Convert.ToInt32(dr["idCuentaBancaria"].ToString()));
                        oCOM_CuentasUsuariosDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oCOM_CuentasUsuariosDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oCOM_CuentasUsuariosDTO.idBanco = Convert.ToInt32(dr["idBanco"] == null ? 0 : Convert.ToInt32(dr["idBanco"].ToString()));
                        oCOM_CuentasUsuariosDTO.DescripcionCuenta = dr["DescripcionCuenta"] == null ? "" : dr["DescripcionCuenta"].ToString();
                        oCOM_CuentasUsuariosDTO.Cuenta = dr["Cuenta"] == null ? "" : dr["Cuenta"].ToString();
                        oCOM_CuentasUsuariosDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oCOM_CuentasUsuariosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCOM_CuentasUsuariosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCOM_CuentasUsuariosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oCOM_CuentasUsuariosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));

                        oResultDTO.ListaResultado.Add(oCOM_CuentasUsuariosDTO);
                        oResultDTO.Resultado = "OK";
                    }
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_CuentasUsuariosDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_CuentasUsuariosDTO> ListarTodoCuentaOrigen(int id, SqlConnection cn = null)//Cuenta Destino
        {
            ResultDTO<COM_CuentasUsuariosDTO> oResultDTO = new ResultDTO<COM_CuentasUsuariosDTO>();
            oResultDTO.ListaResultado = new List<COM_CuentasUsuariosDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_CuentasSocio_ListarTodo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idSocioNegocio", id);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_CuentasUsuariosDTO oCOM_CuentasUsuariosDTO = new COM_CuentasUsuariosDTO();
                        oCOM_CuentasUsuariosDTO.idCuentaBancaria = Convert.ToInt32(dr["idCuentaBancaria"] == null ? 0 : Convert.ToInt32(dr["idCuentaBancaria"].ToString()));
                        oCOM_CuentasUsuariosDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oCOM_CuentasUsuariosDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oCOM_CuentasUsuariosDTO.idBanco = Convert.ToInt32(dr["idBanco"] == null ? 0 : Convert.ToInt32(dr["idBanco"].ToString()));
                        oCOM_CuentasUsuariosDTO.DescripcionCuenta = dr["DescripcionCuenta"] == null ? "" : dr["DescripcionCuenta"].ToString();
                        oCOM_CuentasUsuariosDTO.Cuenta = dr["Cuenta"] == null ? "" : dr["Cuenta"].ToString();
                        oCOM_CuentasUsuariosDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oCOM_CuentasUsuariosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCOM_CuentasUsuariosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCOM_CuentasUsuariosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oCOM_CuentasUsuariosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));

                        oResultDTO.ListaResultado.Add(oCOM_CuentasUsuariosDTO);
                        oResultDTO.Resultado = "OK";
                    }
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_CuentasUsuariosDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
