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
    public class FN_CajaDetalleDAO
    {
        public ResultDTO<Entities.FN_CajaDetalleDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<FN_CajaDetalleDTO> oResultDTO = new ResultDTO<FN_CajaDetalleDTO>();
            oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_CajaDetalle_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_CajaDetalleDTO oFN_CajaDetalleDTO = new FN_CajaDetalleDTO();
                        oFN_CajaDetalleDTO.idCajaDetalle = Convert.ToInt32(dr["idCajaDetalle"] == null ? 0 : Convert.ToInt32(dr["idCajaDetalle"].ToString()));
                        oFN_CajaDetalleDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oFN_CajaDetalleDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oFN_CajaDetalleDTO.idCaja = Convert.ToInt32(dr["idCaja"] == null ? 0 : Convert.ToInt32(dr["idCaja"].ToString()));
                        oFN_CajaDetalleDTO.PeriodoAno = dr["PeriodoAno"] == null ? "" : dr["PeriodoAno"].ToString();
                        oFN_CajaDetalleDTO.NroCaja = dr["NroCaja"] == null ? "" : dr["NroCaja"].ToString();
                        oFN_CajaDetalleDTO.idConcepto = Convert.ToInt32(dr["idConcepto"] == null ? 0 : Convert.ToInt32(dr["idConcepto"].ToString()));
                        oFN_CajaDetalleDTO.DescripcionConcepto = dr["DescripcionConcepto"] == null ? "" : dr["DescripcionConcepto"].ToString();
                        oFN_CajaDetalleDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oFN_CajaDetalleDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"] == null ? 0 : Convert.ToDecimal(dr["SubTotalNacional"].ToString()));
                        oFN_CajaDetalleDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["SubTotalExtranjero"].ToString()));
                        oFN_CajaDetalleDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"] == null ? 0 : Convert.ToDecimal(dr["TipoCambio"].ToString()));
                        oFN_CajaDetalleDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"] == null ? 0 : Convert.ToDecimal(dr["IGVNacional"].ToString()));
                        oFN_CajaDetalleDTO.IGVExtranjera = Convert.ToDecimal(dr["IGVExtranjera"] == null ? 0 : Convert.ToDecimal(dr["IGVExtranjera"].ToString()));
                        oFN_CajaDetalleDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oFN_CajaDetalleDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"] == null ? 0 : Convert.ToDecimal(dr["TotalExtranjero"].ToString()));
                        oFN_CajaDetalleDTO.EstadoCaja = dr["EstadoCaja"] == null ? "" : dr["EstadoCaja"].ToString();
                        oFN_CajaDetalleDTO.idProvCliEmpl = Convert.ToInt32(dr["idProvCliEmpl"] == null ? 0 : Convert.ToInt32(dr["idProvCliEmpl"].ToString()));
                        oFN_CajaDetalleDTO.NombreProvCliEmpl = dr["NombreProvCliEmpl"] == null ? "" : dr["NombreProvCliEmpl"].ToString();
                        oFN_CajaDetalleDTO.Observaciones = dr["Observaciones"] == null ? "" : dr["Observaciones"].ToString();
                        //oFN_CajaDetalleDTO.idBanco = Convert.ToDecimal(dr["idBanco"] == null ? 0 : Convert.ToDecimal(dr["idBanco"].ToString()));
                        //oFN_CajaDetalleDTO.NombreBanco = dr["NombreBanco"] == null ? "" : dr["NombreBanco"].ToString();
                        oFN_CajaDetalleDTO.NroCheque = dr["NroCheque"] == null ? "" : dr["NroCheque"].ToString();
                        oFN_CajaDetalleDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"] == null ? 0 : Convert.ToInt32(dr["idTipoDocumento"].ToString()));
                        oFN_CajaDetalleDTO.SerieDcto = dr["SerieDcto"] == null ? "" : dr["SerieDcto"].ToString();
                        oFN_CajaDetalleDTO.NroDcto = dr["NroDcto"] == null ? "" : dr["NroDcto"].ToString();
                        oFN_CajaDetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_CajaDetalleDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_CajaDetalleDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_CajaDetalleDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_CajaDetalleDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oFN_CajaDetalleDTO.TipoOperacion = dr["TipoOperacion"] == null ? "" : dr["TipoOperacion"].ToString();
                        oFN_CajaDetalleDTO.idTarjeta = Convert.ToDecimal(dr["idTarjeta"] == null ? 0 : Convert.ToDecimal(dr["idTarjeta"].ToString()));
                        oFN_CajaDetalleDTO.Tarjeta = dr["Tarjeta"] == null ? "" : dr["Tarjeta"].ToString();
                        oResultDTO.ListaResultado.Add(oFN_CajaDetalleDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_CajaDetalleDTO> ListarxID(int idCajaDetalle)
        {
            ResultDTO<FN_CajaDetalleDTO> oResultDTO = new ResultDTO<FN_CajaDetalleDTO>();
            oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_CajaDetalle_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCajaDetalle", idCajaDetalle);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_CajaDetalleDTO oFN_CajaDetalleDTO = new FN_CajaDetalleDTO();
                        oFN_CajaDetalleDTO.idCajaDetalle = Convert.ToInt32(dr["idCajaDetalle"].ToString());
                        oFN_CajaDetalleDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oFN_CajaDetalleDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oFN_CajaDetalleDTO.idCaja = Convert.ToInt32(dr["idCaja"].ToString());
                        oFN_CajaDetalleDTO.PeriodoAno = dr["PeriodoAno"].ToString();
                        oFN_CajaDetalleDTO.NroCaja = dr["NroCaja"].ToString();
                        oFN_CajaDetalleDTO.idConcepto = Convert.ToInt32(dr["idConcepto"].ToString());
                        oFN_CajaDetalleDTO.DescripcionConcepto = dr["DescripcionConcepto"].ToString();
                        oFN_CajaDetalleDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oFN_CajaDetalleDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"].ToString());
                        oFN_CajaDetalleDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"].ToString());
                        oFN_CajaDetalleDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"].ToString());
                        oFN_CajaDetalleDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"].ToString());
                        oFN_CajaDetalleDTO.IGVExtranjera = Convert.ToDecimal(dr["IGVExtranjera"].ToString());
                        oFN_CajaDetalleDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        oFN_CajaDetalleDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"].ToString());
                        oFN_CajaDetalleDTO.EstadoCaja = dr["EstadoCaja"].ToString();
                        oFN_CajaDetalleDTO.idTipoEmpleado = Convert.ToInt32(dr["idTipoEmpleado"].ToString());
                        oFN_CajaDetalleDTO.idProvCliEmpl = Convert.ToInt32(dr["idProvCliEmpl"].ToString());
                        oFN_CajaDetalleDTO.NombreProvCliEmpl = dr["NombreProvCliEmpl"].ToString();
                        oFN_CajaDetalleDTO.Observaciones = dr["Observaciones"].ToString();
                        //oFN_CajaDetalleDTO.idBanco = Convert.ToDecimal(dr["idBanco"].ToString());
                        //oFN_CajaDetalleDTO.NombreBanco = dr["NombreBanco"].ToString();
                        oFN_CajaDetalleDTO.NroCheque = dr["NroCheque"].ToString();
                        oFN_CajaDetalleDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"].ToString());
                        oFN_CajaDetalleDTO.SerieDcto = dr["SerieDcto"].ToString();
                        oFN_CajaDetalleDTO.NroDcto = dr["NroDcto"].ToString();
                        oFN_CajaDetalleDTO.SerieVale = dr["SerieVale"].ToString();
                        oFN_CajaDetalleDTO.NroOperacion = dr["NroOperacion"].ToString();
                        oFN_CajaDetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_CajaDetalleDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_CajaDetalleDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oFN_CajaDetalleDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oFN_CajaDetalleDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oFN_CajaDetalleDTO.idTarjeta = Convert.ToDecimal(dr["idTarjeta"].ToString());
                        oFN_CajaDetalleDTO.Tarjeta = dr["Tarjeta"].ToString();
                        oResultDTO.ListaResultado.Add(oFN_CajaDetalleDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_CajaDetalleDTO> UpdateInsert(FN_CajaDetalleDTO oFN_CajaDetalle)
        {
            ResultDTO<FN_CajaDetalleDTO> oResultDTO = new ResultDTO<FN_CajaDetalleDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_CajaDetalle_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idCajaDetalle", oFN_CajaDetalle.idCajaDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oFN_CajaDetalle.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@idCaja", oFN_CajaDetalle.idCaja);
                        da.SelectCommand.Parameters.AddWithValue("@PeriodoAno", oFN_CajaDetalle.PeriodoAno);
                        da.SelectCommand.Parameters.AddWithValue("@NroCaja", oFN_CajaDetalle.NroCaja);
                        da.SelectCommand.Parameters.AddWithValue("@idConcepto", oFN_CajaDetalle.idConcepto);
                        da.SelectCommand.Parameters.AddWithValue("@DescripcionConcepto", oFN_CajaDetalle.DescripcionConcepto);
                        da.SelectCommand.Parameters.AddWithValue("@idMoneda", oFN_CajaDetalle.idMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@SubTotalNacional", oFN_CajaDetalle.SubTotalNacional);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambio", oFN_CajaDetalle.TipoCambio);
                        da.SelectCommand.Parameters.AddWithValue("@IGVNacional", oFN_CajaDetalle.IGVNacional);
                        da.SelectCommand.Parameters.AddWithValue("@TotalNacional", oFN_CajaDetalle.TotalNacional);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoEmpleado", oFN_CajaDetalle.idTipoEmpleado);
                        da.SelectCommand.Parameters.AddWithValue("@idProvCliEmpl", oFN_CajaDetalle.idProvCliEmpl);
                        da.SelectCommand.Parameters.AddWithValue("@NombreProvCliEmpl", oFN_CajaDetalle.NombreProvCliEmpl);
                        da.SelectCommand.Parameters.AddWithValue("@Ruc", oFN_CajaDetalle.Ruc == null ? "-" : oFN_CajaDetalle.Ruc);
                        da.SelectCommand.Parameters.AddWithValue("@Observaciones", oFN_CajaDetalle.Observaciones);
                        //da.SelectCommand.Parameters.AddWithValue("@idBanco", oFN_CajaDetalle.idBanco);
                        //da.SelectCommand.Parameters.AddWithValue("@NombreBanco", oFN_CajaDetalle.NombreBanco);
                        //da.SelectCommand.Parameters.AddWithValue("@NroCheque", oFN_CajaDetalle.NroCheque == null ? "-" : oFN_CajaDetalle.NroCheque);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoDocumento", oFN_CajaDetalle.idTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@SerieDcto", oFN_CajaDetalle.SerieDcto == null ? "-" : oFN_CajaDetalle.SerieDcto);
                        da.SelectCommand.Parameters.AddWithValue("@NroDcto", oFN_CajaDetalle.NroDcto == null ? "-" : oFN_CajaDetalle.NroDcto);
                        da.SelectCommand.Parameters.AddWithValue("@MontoPendiente", oFN_CajaDetalle.MontoPendiente);
                        //da.SelectCommand.Parameters.AddWithValue("@SerieVale", oFN_CajaDetalle.SerieVale == null ? "-" : oFN_CajaDetalle.SerieVale);
                        da.SelectCommand.Parameters.AddWithValue("@NroOperacion", oFN_CajaDetalle.NroOperacion == null ? "-" : oFN_CajaDetalle.NroOperacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oFN_CajaDetalle.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oFN_CajaDetalle.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@TipoOperacion", oFN_CajaDetalle.TipoOperacion);
                        da.SelectCommand.Parameters.AddWithValue("@idCompraVenta", oFN_CajaDetalle.idCompraVenta);
                        da.SelectCommand.Parameters.AddWithValue("@idCita", oFN_CajaDetalle.idCita);
                        da.SelectCommand.Parameters.AddWithValue("@NroCita", oFN_CajaDetalle.NroCita);
                        da.SelectCommand.Parameters.AddWithValue("@Paciente", oFN_CajaDetalle.Paciente);
                        da.SelectCommand.Parameters.AddWithValue("@CostoCita", oFN_CajaDetalle.CostoCita);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoPago", oFN_CajaDetalle.idTipoPago);
                        da.SelectCommand.Parameters.AddWithValue("@TipoPago", oFN_CajaDetalle.TipoPago);
                        da.SelectCommand.Parameters.AddWithValue("@idTarjeta", oFN_CajaDetalle.idTarjeta);
                        da.SelectCommand.Parameters.AddWithValue("@Tarjeta", oFN_CajaDetalle.Tarjeta);
                        da.SelectCommand.Parameters.AddWithValue("@SerieRecibo", oFN_CajaDetalle.SerieRecibo == null ? "-" : oFN_CajaDetalle.SerieRecibo);
                        da.SelectCommand.Parameters.AddWithValue("@NroRecibo", oFN_CajaDetalle.NroRecibo == null ? "-" : oFN_CajaDetalle.NroRecibo);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            oResultDTO.Resultado = "OK";
                            //oResultDTO.ListaResultado = ListarxIDCaja(oFN_CajaDetalle.idCaja).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_CajaDetalleDTO> Delete(int idCajaDetalle, int idCaja)
        {
            ResultDTO<FN_CajaDetalleDTO> oResultDTO = new ResultDTO<FN_CajaDetalleDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_CajaDetalle_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idCajaDetalle", idCajaDetalle);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            //oResultDTO.ListaResultado = ListarxIDCaja(idCaja).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
                    }
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_CajaDetalleDTO> ListarxIDCaja(int idCaja)
        {
            ResultDTO<FN_CajaDetalleDTO> oResultDTO = new ResultDTO<FN_CajaDetalleDTO>();
            oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_CajaDetalle_ListarxIDCaja", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCaja", idCaja);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_CajaDetalleDTO oFN_CajaDetalleDTO = new FN_CajaDetalleDTO();
                        oFN_CajaDetalleDTO.idCajaDetalle = Convert.ToInt32(dr["idCajaDetalle"].ToString());
                        oFN_CajaDetalleDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oFN_CajaDetalleDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oFN_CajaDetalleDTO.idCaja = Convert.ToInt32(dr["idCaja"].ToString());
                        oFN_CajaDetalleDTO.PeriodoAno = dr["PeriodoAno"].ToString();
                        oFN_CajaDetalleDTO.NroCaja = dr["NroCaja"].ToString();
                        oFN_CajaDetalleDTO.idConcepto = Convert.ToInt32(dr["idConcepto"].ToString());
                        oFN_CajaDetalleDTO.DescripcionConcepto = dr["DescripcionConcepto"].ToString();
                        oFN_CajaDetalleDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oFN_CajaDetalleDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"].ToString());
                        //oFN_CajaDetalleDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"].ToString());
                        oFN_CajaDetalleDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"].ToString());
                        oFN_CajaDetalleDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"].ToString());
                        //oFN_CajaDetalleDTO.IGVExtranjera = Convert.ToDecimal(dr["IGVExtranjera"].ToString());
                        oFN_CajaDetalleDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        //oFN_CajaDetalleDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"].ToString());
                        oFN_CajaDetalleDTO.EstadoCaja = dr["EstadoCaja"].ToString();
                        oFN_CajaDetalleDTO.idProvCliEmpl = Convert.ToInt32(dr["idProvCliEmpl"].ToString());
                        oFN_CajaDetalleDTO.NombreProvCliEmpl = dr["NombreProvCliEmpl"].ToString();
                        oFN_CajaDetalleDTO.Ruc = dr["Ruc"].ToString();
                        oFN_CajaDetalleDTO.Observaciones = dr["Observaciones"].ToString();
                        //oFN_CajaDetalleDTO.idBanco = Convert.ToDecimal(dr["idBanco"].ToString());
                        //oFN_CajaDetalleDTO.NombreBanco = dr["NombreBanco"].ToString();
                        //oFN_CajaDetalleDTO.NroCheque = dr["NroCheque"].ToString();
                        oFN_CajaDetalleDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"].ToString());
                        oFN_CajaDetalleDTO.SerieDcto = dr["SerieDcto"].ToString();
                        oFN_CajaDetalleDTO.NroDcto = dr["NroDcto"].ToString();
                        oFN_CajaDetalleDTO.MontoPendiente = Convert.ToDecimal(dr["MontoPendiente"] == null ? 0 : Convert.ToDecimal(dr["MontoPendiente"].ToString()));
                        oFN_CajaDetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_CajaDetalleDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_CajaDetalleDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oFN_CajaDetalleDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oFN_CajaDetalleDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oFN_CajaDetalleDTO.TipoOperacion = dr["TipoOperacion"].ToString();
                        oFN_CajaDetalleDTO.idTipoEmpleado = Convert.ToInt32(dr["idTipoEmpleado"].ToString());
                        //oFN_CajaDetalleDTO.SerieVale = dr["SerieVale"].ToString();
                        oFN_CajaDetalleDTO.NroOperacion = dr["NroOperacion"].ToString();
                        oFN_CajaDetalleDTO.idTipoPago = Convert.ToInt32(dr["idTipoPago"].ToString());
                        oFN_CajaDetalleDTO.TipoPago = dr["TipoPago"].ToString();
                        oFN_CajaDetalleDTO.idTarjeta = Convert.ToDecimal(dr["idTarjeta"].ToString());
                        oFN_CajaDetalleDTO.Tarjeta = dr["Tarjeta"].ToString();
                        oFN_CajaDetalleDTO.SerieRecibo = dr["SerieRecibo"].ToString();
                        oFN_CajaDetalleDTO.NroRecibo = dr["NroRecibo"].ToString();
                        oResultDTO.ListaResultado.Add(oFN_CajaDetalleDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_CajaDetalleDTO> ReportexIDCaja(int idCaja)
        {
            ResultDTO<FN_CajaDetalleDTO> oResultDTO = new ResultDTO<FN_CajaDetalleDTO>();
            oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_RPT_DetalleCaja", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCaja", idCaja);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_CajaDetalleDTO oFN_CajaDetalleDTO = new FN_CajaDetalleDTO();
                        oFN_CajaDetalleDTO.NroCaja = dr["NroCaja"].ToString();
                        oFN_CajaDetalleDTO.DescripcionConcepto = dr["DescripcionConcepto"].ToString();
                        oFN_CajaDetalleDTO.Observaciones = dr["Observaciones"].ToString();
                        oFN_CajaDetalleDTO.TipoPago = dr["TipoPago"].ToString();
                        oFN_CajaDetalleDTO.NombreProvCliEmpl = dr["NombreProvCliEmpl"].ToString();
                        oFN_CajaDetalleDTO.Ruc = dr["Ruc"].ToString();
                        oFN_CajaDetalleDTO.Salida = Convert.ToDecimal(dr["Salida"].ToString());
                        oFN_CajaDetalleDTO.Ingreso = Convert.ToDecimal(dr["Ingreso"].ToString());
                        oFN_CajaDetalleDTO.Personal = Convert.ToDecimal(dr["Personal"].ToString());
                        oResultDTO.ListaResultado.Add(oFN_CajaDetalleDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_CajaDetalleDTO> ListarxIDCajaDetalle(int idCajaDetalle)
        {
            ResultDTO<FN_CajaDetalleDTO> oResultDTO = new ResultDTO<FN_CajaDetalleDTO>();
            oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_CajaDetalle_ListarxIDCajaDetalle", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCajaDetalle", idCajaDetalle);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_CajaDetalleDTO oFN_CajaDetalleDTO = new FN_CajaDetalleDTO();
                        oFN_CajaDetalleDTO.idCajaDetalle = Convert.ToInt32(dr["idCajaDetalle"].ToString());
                        oFN_CajaDetalleDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oFN_CajaDetalleDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oFN_CajaDetalleDTO.idCaja = Convert.ToInt32(dr["idCaja"].ToString());
                        oFN_CajaDetalleDTO.PeriodoAno = dr["PeriodoAno"].ToString();
                        oFN_CajaDetalleDTO.NroCaja = dr["NroCaja"].ToString();
                        oFN_CajaDetalleDTO.idConcepto = Convert.ToInt32(dr["idConcepto"].ToString());
                        oFN_CajaDetalleDTO.DescripcionConcepto = dr["DescripcionConcepto"].ToString();
                        oFN_CajaDetalleDTO.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                        oFN_CajaDetalleDTO.SubTotalNacional = Convert.ToDecimal(dr["SubTotalNacional"].ToString());
                        //oFN_CajaDetalleDTO.SubTotalExtranjero = Convert.ToDecimal(dr["SubTotalExtranjero"].ToString());
                        oFN_CajaDetalleDTO.TipoCambio = Convert.ToDecimal(dr["TipoCambio"].ToString());
                        oFN_CajaDetalleDTO.IGVNacional = Convert.ToDecimal(dr["IGVNacional"].ToString());
                        //oFN_CajaDetalleDTO.IGVExtranjera = Convert.ToDecimal(dr["IGVExtranjera"].ToString());
                        oFN_CajaDetalleDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        //oFN_CajaDetalleDTO.TotalExtranjero = Convert.ToDecimal(dr["TotalExtranjero"].ToString());
                        oFN_CajaDetalleDTO.EstadoCaja = dr["EstadoCaja"].ToString();
                        oFN_CajaDetalleDTO.idProvCliEmpl = Convert.ToInt32(dr["idProvCliEmpl"].ToString());
                        oFN_CajaDetalleDTO.NombreProvCliEmpl = dr["NombreProvCliEmpl"].ToString();
                        oFN_CajaDetalleDTO.Observaciones = dr["Observaciones"].ToString();
                        //oFN_CajaDetalleDTO.idBanco = Convert.ToDecimal(dr["idBanco"].ToString());
                        //oFN_CajaDetalleDTO.NombreBanco = dr["NombreBanco"].ToString();
                        //oFN_CajaDetalleDTO.NroCheque = dr["NroCheque"].ToString();
                        oFN_CajaDetalleDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"].ToString());
                        oFN_CajaDetalleDTO.SerieDcto = dr["SerieDcto"].ToString();
                        oFN_CajaDetalleDTO.NroDcto = dr["NroDcto"].ToString();
                        oFN_CajaDetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_CajaDetalleDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_CajaDetalleDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oFN_CajaDetalleDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oFN_CajaDetalleDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oFN_CajaDetalleDTO.TipoOperacion = dr["TipoOperacion"].ToString();
                        //oFN_CajaDetalleDTO.idTipoEmpleado =Convert.ToInt32(dr["idTipoEmpleado"].ToString());
                        //oFN_CajaDetalleDTO.SerieVale = dr["SerieVale"].ToString();
                        oFN_CajaDetalleDTO.NroOperacion = dr["NroOperacion"].ToString();
                        oFN_CajaDetalleDTO.idCompraVenta = Convert.ToInt32(dr["idCompraVenta"].ToString());
                        oFN_CajaDetalleDTO.Ruc = dr["Ruc"].ToString();
                        oFN_CajaDetalleDTO.MontoPendiente = Convert.ToDecimal(dr["MontoPendiente"].ToString());
                        oFN_CajaDetalleDTO.idTipoPago = Convert.ToInt32(dr["idTipoPago"].ToString());
                        oFN_CajaDetalleDTO.TipoPago = dr["TipoPago"].ToString();
                        oFN_CajaDetalleDTO.Documento = dr["SerieDcto"].ToString() + "-" + dr["NroDcto"].ToString();
                        oFN_CajaDetalleDTO.idTarjeta = Convert.ToDecimal(dr["idTarjeta"].ToString());
                        oFN_CajaDetalleDTO.Tarjeta = dr["Tarjeta"].ToString();
                        oFN_CajaDetalleDTO.SerieRecibo = dr["SerieRecibo"].ToString();
                        oFN_CajaDetalleDTO.NroRecibo = dr["NroRecibo"].ToString();
                        oResultDTO.ListaResultado.Add(oFN_CajaDetalleDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_CajaDetalleDTO>();
                }
            }
            return oResultDTO;
        }

    }
}
