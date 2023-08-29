using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Finanzas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SistemaDermoSalud.DataAccess.Finanzas
{
   public class FN_PagosDAO
    {
        public ResultDTO<FN_PagosDTO> ListarRangoFecha(int idEmpresa, DateTime fechaInicio, DateTime fechaFin, SqlConnection cn = null)
        {
            ResultDTO<FN_PagosDTO> oResultDTO = new ResultDTO<FN_PagosDTO>();
            oResultDTO.ListaResultado = new List<FN_PagosDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_Pago_ListarxFecha", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_PagosDTO oFN_PagosDTO = new FN_PagosDTO();
                        oFN_PagosDTO.idPago = Convert.ToInt32(dr["idPago"] == null ? 0 : Convert.ToInt32(dr["idPago"].ToString()));
                        oFN_PagosDTO.idTipo = Convert.ToInt32(dr["idTipo"] == null ? 0 : Convert.ToInt32(dr["idTipo"].ToString()));
                        oFN_PagosDTO.idDocumentoCompraVenta = Convert.ToInt32(dr["idDocCompraVenta"] == null ? 0 : Convert.ToInt32(dr["idDocCompraVenta"].ToString()));
                        oFN_PagosDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oFN_PagosDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oFN_PagosDTO.SerieDcto = dr["SerieDcto"] == null ? "" : dr["SerieDcto"].ToString();
                        oFN_PagosDTO.NumeroDcto = dr["NumeroDcto"] == null ? "" : dr["NumeroDcto"].ToString();
                        oFN_PagosDTO.FechaPago = Convert.ToDateTime(dr["FechaPago"].ToString());
                        oFN_PagosDTO.MontoxCobrar = Convert.ToDecimal(dr["MontoxCobrar"] == null ? 0 : Convert.ToDecimal(dr["MontoxCobrar"].ToString()));
                        oFN_PagosDTO.MontoxPagar = Convert.ToDecimal(dr["MontoxPagar"] == null ? 0 : Convert.ToDecimal(dr["MontoxPagar"].ToString()));
                        oFN_PagosDTO.MontoAplicado = Convert.ToDecimal(dr["MontoAplicado"] == null ? 0 : Convert.ToDecimal(dr["MontoAplicado"].ToString()));
                        oFN_PagosDTO.SaldoxAplicar = Convert.ToDecimal(dr["SaldoxAplicar"] == null ? 0 : Convert.ToDecimal(dr["SaldoxAplicar"].ToString()));
                        oFN_PagosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_PagosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_PagosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_PagosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_PagosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oFN_PagosDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_PagosDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<FN_PagosDTO> oResultDTO = new ResultDTO<FN_PagosDTO>();
            oResultDTO.ListaResultado = new List<FN_PagosDTO>();
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
                        FN_PagosDTO oFN_PagosDTO = new FN_PagosDTO();
                        oFN_PagosDTO.idPago = Convert.ToInt32(dr["idPago"] == null ? 0 : Convert.ToInt32(dr["idPago"].ToString()));
                        oFN_PagosDTO.idTipo = Convert.ToInt32(dr["idTipo"] == null ? 0 : Convert.ToInt32(dr["idTipo"].ToString()));
                        oFN_PagosDTO.idDocumentoCompraVenta = Convert.ToInt32(dr["idDocCompraVenta"] == null ? 0 : Convert.ToInt32(dr["idDocCompraVenta"].ToString()));
                        oFN_PagosDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oFN_PagosDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oFN_PagosDTO.SerieDcto = dr["SerieDcto"] == null ? "" : dr["SerieDcto"].ToString();
                        oFN_PagosDTO.NumeroDcto = dr["NumeroDcto"] == null ? "" : dr["NumeroDcto"].ToString();
                        oFN_PagosDTO.FechaPago = Convert.ToDateTime(dr["FechaPago"].ToString());
                        oFN_PagosDTO.MontoxCobrar = Convert.ToDecimal(dr["MontoxCobrar"] == null ? 0 : Convert.ToDecimal(dr["MontoxCobrar"].ToString()));
                        oFN_PagosDTO.MontoxPagar = Convert.ToDecimal(dr["MontoxPagar"] == null ? 0 : Convert.ToDecimal(dr["MontoxPagar"].ToString()));
                        oFN_PagosDTO.MontoAplicado = Convert.ToDecimal(dr["MontoAplicado"] == null ? 0 : Convert.ToDecimal(dr["MontoAplicado"].ToString()));
                        oFN_PagosDTO.SaldoxAplicar = Convert.ToDecimal(dr["SaldoxAplicar"] == null ? 0 : Convert.ToDecimal(dr["SaldoxAplicar"].ToString()));
                        oFN_PagosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_PagosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_PagosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_PagosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_PagosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oFN_PagosDTO);
                    }

                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_PagosDTO> ListarxID(int idPago)
        {
            ResultDTO<FN_PagosDTO> oResultDTO = new ResultDTO<FN_PagosDTO>();
            oResultDTO.ListaResultado = new List<FN_PagosDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_Pago_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idPago", idPago);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {


                        FN_PagosDTO oFN_PagosDTO = new FN_PagosDTO();
                        oFN_PagosDTO.idPago = Convert.ToInt32(dr["idPago"] == null ? 0 : Convert.ToInt32(dr["idPago"].ToString()));
                        oFN_PagosDTO.idTipo = Convert.ToInt32(dr["idTipo"] == null ? 0 : Convert.ToInt32(dr["idTipo"].ToString()));
                        oFN_PagosDTO.idDocumentoCompraVenta = Convert.ToInt32(dr["idDocCompraVenta"] == null ? 0 : Convert.ToInt32(dr["idDocCompraVenta"].ToString()));
                        oFN_PagosDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oFN_PagosDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oFN_PagosDTO.SerieDcto = dr["SerieDcto"] == null ? "" : dr["SerieDcto"].ToString();
                        oFN_PagosDTO.NumeroDcto = dr["NumeroDcto"] == null ? "" : dr["NumeroDcto"].ToString();
                        oFN_PagosDTO.FechaPago = Convert.ToDateTime(dr["FechaPago"].ToString());
                        oFN_PagosDTO.MontoxCobrar = Convert.ToDecimal(dr["MontoxCobrar"] == null ? 0 : Convert.ToDecimal(dr["MontoxCobrar"].ToString()));
                        oFN_PagosDTO.MontoxPagar = Convert.ToDecimal(dr["MontoxPagar"] == null ? 0 : Convert.ToDecimal(dr["MontoxPagar"].ToString()));
                        oFN_PagosDTO.MontoAplicado = Convert.ToDecimal(dr["MontoAplicado"] == null ? 0 : Convert.ToDecimal(dr["MontoAplicado"].ToString()));
                        oFN_PagosDTO.SaldoxAplicar = Convert.ToDecimal(dr["SaldoxAplicar"] == null ? 0 : Convert.ToDecimal(dr["SaldoxAplicar"].ToString()));
                        oFN_PagosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_PagosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_PagosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_PagosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_PagosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oFN_PagosDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                        /*{
                            if (dr.NextResult())
                            {
                                List<FN_PagosDetalle> lista = new List<FN_PagosDetalle>();
                                while (dr.Read())
                                {
                                    FN_PagosDTO oFN_PagosDTO = new FN_PagosDTO();
                                    oFN_PagosDTO.idPago = Convert.ToInt32(dr["idPago"] == null ? 0 : Convert.ToInt32(dr["idPago"].ToString()));
                                    oFN_PagosDTO.idTipo = Convert.ToInt32(dr["idTipo"] == null ? 0 : Convert.ToInt32(dr["idTipo"].ToString()));
                                    oFN_PagosDTO.idDocumentoCompraVenta = Convert.ToInt32(dr["idDocCompraVenta"] == null ? 0 : Convert.ToInt32(dr["idDocCompraVenta"].ToString()));
                                    oFN_PagosDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                                    oFN_PagosDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                                    oFN_PagosDTO.SerieDcto = dr["SerieDcto"] == null ? "" : dr["SerieDcto"].ToString();
                                    oFN_PagosDTO.NumeroDcto = dr["NumeroDcto"] == null ? "" : dr["NumeroDcto"].ToString();
                                    oFN_PagosDTO.FechaPago = Convert.ToDateTime(dr["FechaPago"].ToString());
                                    oFN_PagosDTO.MontoxCobrar = Convert.ToDecimal(dr["MontoxCobrar"] == null ? 0 : Convert.ToDecimal(dr["MontoxCobrar"].ToString()));
                                    oFN_PagosDTO.MontoxPagar = Convert.ToDecimal(dr["MontoxPagar"] == null ? 0 : Convert.ToDecimal(dr["MontoxPagar"].ToString()));
                                    oFN_PagosDTO.MontoAplicado = Convert.ToDecimal(dr["MontoAplicado"] == null ? 0 : Convert.ToDecimal(dr["MontoAplicado"].ToString()));
                                    oFN_PagosDTO.SaldoxAplicar = Convert.ToDecimal(dr["SaldoxAplicar"] == null ? 0 : Convert.ToDecimal(dr["SaldoxAplicar"].ToString()));
                                    oFN_PagosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                                    oFN_PagosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                                    oFN_PagosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                                    oFN_PagosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                                    oFN_PagosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                                    oResultDTO.ListaResultado.Add(oFN_PagosDTO);
                                }
                                oResultDTO.ListaResultado[0].oListaDetalle = lista;
                            }
                        }*/
                        oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_PagosDTO> UpdateInsert(FN_PagosDTO oCOM_OrdenCompra, DateTime FechaInicio, DateTime FechaFin)
        {
            ResultDTO<FN_PagosDTO> oResultDTO = new ResultDTO<FN_PagosDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_Pagos_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idPago", oCOM_OrdenCompra.idPago);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oCOM_OrdenCompra.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@idTipo ", oCOM_OrdenCompra.idTipo);
                        da.SelectCommand.Parameters.AddWithValue("@idDocumentoCompraVenta ", oCOM_OrdenCompra.idDocumentoCompraVenta);
                        da.SelectCommand.Parameters.AddWithValue("@idSocioNegocio ", oCOM_OrdenCompra.idSocioNegocio);
                        da.SelectCommand.Parameters.AddWithValue("@idSocioNegocio ", oCOM_OrdenCompra.idSocioNegocio);
                        da.SelectCommand.Parameters.AddWithValue("@SerieDcto ", oCOM_OrdenCompra.SerieDcto);// id requerimiento
                        da.SelectCommand.Parameters.AddWithValue("@NumeroDcto ", oCOM_OrdenCompra.NumeroDcto);
                        da.SelectCommand.Parameters.AddWithValue("@FechaPago ", oCOM_OrdenCompra.FechaPago);
                        da.SelectCommand.Parameters.AddWithValue("@MontoxCobrar ", oCOM_OrdenCompra.MontoxCobrar);
                        da.SelectCommand.Parameters.AddWithValue("@MontoxPagar ", oCOM_OrdenCompra.MontoxPagar);
                        da.SelectCommand.Parameters.AddWithValue("@MontoAplicado ", oCOM_OrdenCompra.MontoAplicado);
                        da.SelectCommand.Parameters.AddWithValue("@SaldoxAplicar ", oCOM_OrdenCompra.SaldoxAplicar);
                        da.SelectCommand.Parameters.AddWithValue("@Usuario_reg", oCOM_OrdenCompra.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@Usuario_mod", oCOM_OrdenCompra.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oCOM_OrdenCompra.Estado);
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
                            oResultDTO.ListaResultado = new List<FN_PagosDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_PagosDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_PagosDTO> Delete(FN_PagosDTO oCOM_OrdenCompra, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<FN_PagosDTO> oResultDTO = new ResultDTO<FN_PagosDTO>();
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
                        da.SelectCommand.Parameters.AddWithValue("@idPago", oCOM_OrdenCompra.idPago);
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
                            oResultDTO.ListaResultado = new List<FN_PagosDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_PagosDTO>();
                    }
                }
            }
            return oResultDTO;
        }

        /*--------------------------------------------------------------------*/

        public ResultDTO<FN_PagosDTO> ListarOrdenCompra(int OrdenCompra, SqlConnection cn = null)
        {
            ResultDTO<FN_PagosDTO> oResultDTO = new ResultDTO<FN_PagosDTO>();
            oResultDTO.ListaResultado = new List<FN_PagosDTO>();
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
                        FN_PagosDTO oFN_PagosDTO = new FN_PagosDTO();
                        oFN_PagosDTO.idPago = Convert.ToInt32(dr["idPago"] == null ? 0 : Convert.ToInt32(dr["idPago"].ToString()));
                        oFN_PagosDTO.idTipo = Convert.ToInt32(dr["idTipo"] == null ? 0 : Convert.ToInt32(dr["idTipo"].ToString()));
                        oFN_PagosDTO.idDocumentoCompraVenta = Convert.ToInt32(dr["idDocumentoCompraVenta"] == null ? 0 : Convert.ToInt32(dr["idDocumentoCompraVenta"].ToString()));
                        oFN_PagosDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oFN_PagosDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oFN_PagosDTO.SerieDcto = dr["SerieDcto"] == null ? "" : dr["SerieDcto"].ToString();
                        oFN_PagosDTO.NumeroDcto = dr["NumeroDcto"] == null ? "" : dr["NumeroDcto"].ToString();
                        oFN_PagosDTO.FechaPago = Convert.ToDateTime(dr["FechaPago"].ToString());
                        oFN_PagosDTO.MontoxCobrar = Convert.ToDecimal(dr["MontoxCobrar"] == null ? 0 : Convert.ToDecimal(dr["MontoxCobrar"].ToString()));
                        oFN_PagosDTO.MontoxPagar = Convert.ToDecimal(dr["MontoxPagar"] == null ? 0 : Convert.ToDecimal(dr["MontoxPagar"].ToString()));
                        oFN_PagosDTO.MontoAplicado = Convert.ToDecimal(dr["MontoAplicado"] == null ? 0 : Convert.ToDecimal(dr["MontoAplicado"].ToString()));
                        oFN_PagosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_PagosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_PagosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_PagosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_PagosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oFN_PagosDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_PagosDTO> ListarPagos(int id, SqlConnection cn = null)
        {
            ResultDTO<FN_PagosDTO> oResultDTO = new ResultDTO<FN_PagosDTO>();
            oResultDTO.ListaResultado = new List<FN_PagosDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_Pagos_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", id);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        var temp = Convert.ToDecimal(dr["SaldoxAplicar"] == null ? 0 : Convert.ToDecimal(dr["SaldoxAplicar"].ToString()));
                        if (temp > 0)
                        {
                            FN_PagosDTO oFN_PagosDTO = new FN_PagosDTO();
                            oFN_PagosDTO.idPago = Convert.ToInt32(dr["idPago"] == null ? 0 : Convert.ToInt32(dr["idPago"].ToString()));
                            oFN_PagosDTO.idTipo = Convert.ToInt32(dr["idTipo"] == null ? 0 : Convert.ToInt32(dr["idTipo"].ToString()));
                            oFN_PagosDTO.idDocumentoCompraVenta = Convert.ToInt32(dr["idDocCompraVenta"] == null ? 0 : Convert.ToInt32(dr["idDocCompraVenta"].ToString()));
                            oFN_PagosDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                            oFN_PagosDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                            oFN_PagosDTO.SerieDcto = dr["SerieDcto"] == null ? "" : dr["SerieDcto"].ToString();
                            oFN_PagosDTO.NumeroDcto = dr["NumeroDcto"] == null ? "" : dr["NumeroDcto"].ToString();
                            oFN_PagosDTO.FechaPago = Convert.ToDateTime(dr["FechaPago"].ToString());
                            oFN_PagosDTO.MontoxCobrar = Convert.ToDecimal(dr["MontoxCobrar"] == null ? 0 : Convert.ToDecimal(dr["MontoxCobrar"].ToString()));
                            oFN_PagosDTO.MontoxPagar = Convert.ToDecimal(dr["MontoxPagar"] == null ? 0 : Convert.ToDecimal(dr["MontoxPagar"].ToString()));
                            oFN_PagosDTO.MontoAplicado = Convert.ToDecimal(dr["MontoAplicado"] == null ? 0 : Convert.ToDecimal(dr["MontoAplicado"].ToString()));
                            oFN_PagosDTO.SaldoxAplicar = Convert.ToDecimal(dr["SaldoxAplicar"] == null ? 0 : Convert.ToDecimal(dr["SaldoxAplicar"].ToString()));
                            oFN_PagosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                            oFN_PagosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                            oFN_PagosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                            oFN_PagosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                            oFN_PagosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                            oResultDTO.ListaResultado.Add(oFN_PagosDTO);
                        }
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDTO>();
                }
            }
            return oResultDTO;
        }


        /*metodo inner join para usar  Inner  join  Pagos y Compras*/
        public ResultDTO<FN_PagosDTO> ListarxIDInnerCompras(int idPago)
        {
            ResultDTO<FN_PagosDTO> oResultDTO = new ResultDTO<FN_PagosDTO>();
            oResultDTO.ListaResultado = new List<FN_PagosDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_Pago_InnerCompras", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idPago", idPago);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_PagosDTO oFN_PagosDTO = new FN_PagosDTO();
                        oFN_PagosDTO.idPago = Convert.ToInt32(dr["idPago"] == null ? 0 : Convert.ToInt32(dr["idPago"].ToString()));
                        oFN_PagosDTO.idTipo = Convert.ToInt32(dr["idTipo"] == null ? 0 : Convert.ToInt32(dr["idTipo"].ToString()));
                        oFN_PagosDTO.idDocumentoCompraVenta = Convert.ToInt32(dr["idDocCompraVenta"] == null ? 0 : Convert.ToInt32(dr["idDocCompraVenta"].ToString()));
                        oFN_PagosDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oFN_PagosDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oFN_PagosDTO.SerieDcto = dr["SerieDcto"] == null ? "" : dr["SerieDcto"].ToString();
                        oFN_PagosDTO.NumeroDcto = dr["NumeroDcto"] == null ? "" : dr["NumeroDcto"].ToString();
                        oFN_PagosDTO.FechaPago = Convert.ToDateTime(dr["FechaPago"].ToString());
                        oFN_PagosDTO.MontoxCobrar = Convert.ToDecimal(dr["MontoxCobrar"] == null ? 0 : Convert.ToDecimal(dr["MontoxCobrar"].ToString()));
                        oFN_PagosDTO.MontoxPagar = Convert.ToDecimal(dr["MontoxPagar"] == null ? 0 : Convert.ToDecimal(dr["MontoxPagar"].ToString()));
                        oFN_PagosDTO.MontoAplicado = Convert.ToDecimal(dr["MontoAplicado"] == null ? 0 : Convert.ToDecimal(dr["MontoAplicado"].ToString()));
                        oFN_PagosDTO.SaldoxAplicar = Convert.ToDecimal(dr["SaldoxAplicar"] == null ? 0 : Convert.ToDecimal(dr["SaldoxAplicar"].ToString()));
                        oFN_PagosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_PagosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_PagosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_PagosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_PagosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oFN_PagosDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oResultDTO.ListaResultado.Add(oFN_PagosDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                        oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_PagosDTO> ListarxIDInnerVentas(int idPago)
        {
            ResultDTO<FN_PagosDTO> oResultDTO = new ResultDTO<FN_PagosDTO>();
            oResultDTO.ListaResultado = new List<FN_PagosDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_Pago_InnerVentas", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idPago", idPago);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_PagosDTO oFN_PagosDTO = new FN_PagosDTO();
                        oFN_PagosDTO.idPago = Convert.ToInt32(dr["idPago"] == null ? 0 : Convert.ToInt32(dr["idPago"].ToString()));
                        oFN_PagosDTO.idTipo = Convert.ToInt32(dr["idTipo"] == null ? 0 : Convert.ToInt32(dr["idTipo"].ToString()));
                        oFN_PagosDTO.idDocumentoCompraVenta = Convert.ToInt32(dr["idDocCompraVenta"] == null ? 0 : Convert.ToInt32(dr["idDocCompraVenta"].ToString()));
                        oFN_PagosDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oFN_PagosDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oFN_PagosDTO.SerieDcto = dr["SerieDcto"] == null ? "" : dr["SerieDcto"].ToString();
                        oFN_PagosDTO.NumeroDcto = dr["NumeroDcto"] == null ? "" : dr["NumeroDcto"].ToString();
                        oFN_PagosDTO.FechaPago = Convert.ToDateTime(dr["FechaPago"].ToString());
                        oFN_PagosDTO.MontoxCobrar = Convert.ToDecimal(dr["MontoxCobrar"] == null ? 0 : Convert.ToDecimal(dr["MontoxCobrar"].ToString()));
                        oFN_PagosDTO.MontoxPagar = Convert.ToDecimal(dr["MontoxPagar"] == null ? 0 : Convert.ToDecimal(dr["MontoxPagar"].ToString()));
                        oFN_PagosDTO.MontoAplicado = Convert.ToDecimal(dr["MontoAplicado"] == null ? 0 : Convert.ToDecimal(dr["MontoAplicado"].ToString()));
                        oFN_PagosDTO.SaldoxAplicar = Convert.ToDecimal(dr["SaldoxAplicar"] == null ? 0 : Convert.ToDecimal(dr["SaldoxAplicar"].ToString()));
                        oFN_PagosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_PagosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_PagosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_PagosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_PagosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oFN_PagosDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oResultDTO.ListaResultado.Add(oFN_PagosDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                        oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_PagosDTO>();
                }
            }
            return oResultDTO;
        }

    }
}
