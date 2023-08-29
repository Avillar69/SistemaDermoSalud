using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SistemaDermoSalud.Entities;

namespace SistemaDermoSalud.DataAccess
{
    public class CitasDAO
    {
        public ResultDTO<CitasDTO> ListarTodo(string fechaInicio, string fechaFin, SqlConnection cn = null)
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
            oResultDTO.ListaResultado = new List<CitasDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_ListarTodo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@fechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CitasDTO oCitasDTO = new CitasDTO();
                        oCitasDTO.idCita = Convert.ToInt32(dr["idCita"] == null ? 0 : Convert.ToInt32(dr["idCita"].ToString()));
                        oCitasDTO.idPaciente = Convert.ToInt32(dr["idPaciente"] == null ? 0 : Convert.ToInt32(dr["idPaciente"].ToString()));
                        oCitasDTO.NombreCompleto = dr["NombreCompleto"] == null ? "" : dr["NombreCompleto"].ToString();
                        oCitasDTO.idPersonal = Convert.ToInt32(dr["idPersonal"] == null ? 0 : Convert.ToInt32(dr["idPersonal"].ToString()));
                        oCitasDTO.NombrePersonal = dr["NombrePersonal"] == null ? "" : dr["NombrePersonal"].ToString();
                        oCitasDTO.idServicio = Convert.ToInt32(dr["idServicio"] == null ? 0 : Convert.ToInt32(dr["idServicio"].ToString()));
                        oCitasDTO.DescripcionServicio = dr["DescripcionServicio"] == null ? "" : dr["DescripcionServicio"].ToString();
                        oCitasDTO.Observaciones = dr["Observaciones"] == null ? "" : dr["Observaciones"].ToString();
                        oCitasDTO.FechaHora = dr["FechaHora"] == null ? "" : dr["FechaHora"].ToString();
                        oCitasDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oResultDTO.ListaResultado.Add(oCitasDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CitasDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<CitasDTO> GrabarComisionMedico(CitasDTO oCitas)
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_ComisionMedico_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idComisionMedico", oCitas.idComisionMedico);
                        da.SelectCommand.Parameters.AddWithValue("@idCita", oCitas.idCita);
                        da.SelectCommand.Parameters.AddWithValue("@idServicio", oCitas.idServicio);
                        da.SelectCommand.Parameters.AddWithValue("@FechaComision", oCitas.FechaComision);
                        da.SelectCommand.Parameters.AddWithValue("@Tratamiento", oCitas.Tratamiento);
                        da.SelectCommand.Parameters.AddWithValue("@NroTratamiento", oCitas.NroTratamiento);
                        da.SelectCommand.Parameters.AddWithValue("@Gasto", oCitas.Gasto);
                        da.SelectCommand.Parameters.AddWithValue("@Costo", oCitas.Costo);
                        da.SelectCommand.Parameters.AddWithValue("@Porcentaje", oCitas.PorcentajeMedico);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oCitas.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaCreacion", oCitas.FechaCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@idPersonal", oCitas.idPersonal);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = Listar_ComisionMedico(oCitas.idPersonal, oCitas.FecIni, oCitas.FecFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<CitasDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<CitasDTO>();
                    }
                }
            }
            return oResultDTO;
        }

        public ResultDTO<CitasDTO> ListarTodoMedicos(int id, DateTime fechaIni, DateTime fechaFin, SqlConnection cn = null)
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
            oResultDTO.ListaResultado = new List<CitasDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_ListarxMedico", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idMedico", id);
                    da.SelectCommand.Parameters.AddWithValue("@FechaIni", fechaIni);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CitasDTO oCitasDTO = new CitasDTO();
                        oCitasDTO.idCita = Convert.ToInt32(dr["idCita"] == null ? 0 : Convert.ToInt32(dr["idCita"].ToString()));
                        oCitasDTO.FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString());
                        oCitasDTO.NombreTipoServicio = dr["NombreTipoServicio"] == null ? "" : dr["NombreTipoServicio"].ToString();
                        oCitasDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oCitasDTO.DescripcionServicio = dr["NombreServicio"] == null ? "" : dr["NombreServicio"].ToString();
                        oCitasDTO.Precio = Convert.ToDecimal(dr["Precio"] == null ? 0 : Convert.ToDecimal(dr["Precio"].ToString()));
                        oCitasDTO.Pago = Convert.ToDecimal(dr["Pago"] == null ? 0 : Convert.ToDecimal(dr["Pago"].ToString()));
                        oCitasDTO.PorcentajeMedico = Convert.ToDecimal(dr["PorcentajeMeico"] == null ? 0 : Convert.ToDecimal(dr["PorcentajeMeico"].ToString()));
                        oCitasDTO.Costo = oCitasDTO.Pago;
                        oCitasDTO.Gasto = Convert.ToDecimal(dr["Gasto"] == null ? 0 : Convert.ToDecimal(dr["Gasto"].ToString()));
                        oCitasDTO.Diferencia = Convert.ToDecimal(dr["Diferencia"] == null ? 0 : Convert.ToDecimal(dr["Diferencia"].ToString()));
                        oCitasDTO.Comision = Math.Round(Convert.ToDecimal(dr["Comision"] == null ? 0 : Convert.ToDecimal(dr["Comision"].ToString())), 3);
                        oCitasDTO.idComisionMedico = Convert.ToInt32(dr["idComisionMedico"] == null ? 0 : Convert.ToInt32(dr["idComisionMedico"].ToString()));
                        //oCitasDTO.idCita = Convert.ToInt32(dr["idCita"] == null ? 0 : Convert.ToInt32(dr["idCita"].ToString()));
                        //oCitasDTO.NombreCompleto = dr["NombreServicio"] == null ? "" : dr["NombreServicio"].ToString();                       
                        //oCitasDTO.FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString());
                        //oCitasDTO.Pago = Convert.ToDecimal(dr["Codigo"] == null ? 0 : Convert.ToDecimal(dr["Codigo"].ToString()));
                        oResultDTO.ListaResultado.Add(oCitasDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CitasDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CitasDTO> ListarxCodigo(string Codigo)
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
            oResultDTO.ListaResultado = new List<CitasDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_ListarxCodigos", cn);//SP_LM_Citas_ListarxCodigo
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Codigo", Codigo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CitasDTO oCitasDTO = new CitasDTO();
                        oCitasDTO.idCita = Convert.ToInt32(dr["idCita"].ToString());
                        oCitasDTO.FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString());
                        oCitasDTO.idPaciente = Convert.ToInt32(dr["idPaciente"].ToString());
                        oCitasDTO.NombreCompleto = dr["NombreCompleto"] == null ? "" : dr["NombreCompleto"].ToString();
                        oCitasDTO.idPersonal = Convert.ToInt32(dr["idPersonal"].ToString());
                        oCitasDTO.NombrePersonal = dr["NombrePersonal"] == null ? "" : dr["NombrePersonal"].ToString();
                        oCitasDTO.idServicio = Convert.ToInt32(dr["idServicio"].ToString());
                        oCitasDTO.DescripcionServicio = dr["DescripcionServicio"] == null ? "" : dr["DescripcionServicio"].ToString();
                        oCitasDTO.EstadoPago = Convert.ToInt32(dr["EstadoPago"].ToString());
                        oCitasDTO.Hora = dr["Hora"].ToString();
                        oCitasDTO.idDocRef = Convert.ToInt32(dr["idDocRef"] == null ? 0 : Convert.ToInt32(dr["idDocRef"].ToString()));
                        oCitasDTO.Observaciones = dr["Observaciones"] == null ? "" : dr["Observaciones"].ToString();
                        oCitasDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCitasDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCitasDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oCitasDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oCitasDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oCitasDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oCitasDTO.Pago = Convert.ToDecimal(dr["Pago"] == null ? 0 : Convert.ToDecimal(dr["Pago"].ToString()));
                        oCitasDTO.HoraF = dr["HoraF"].ToString();
                        oCitasDTO.EstadoCita = Convert.ToInt32(string.IsNullOrWhiteSpace(dr["EstadoCita"].ToString()) ? "0" : dr["EstadoCita"].ToString());
                        oCitasDTO.idDocumentoVenta = Convert.ToInt32(string.IsNullOrWhiteSpace(dr["idDocumentoVenta"].ToString()) ? "0" : dr["idDocumentoVenta"].ToString());
                        oCitasDTO.Tratamiento = dr["Tratamiento"] == null ? "" : dr["Tratamiento"].ToString();
                        oResultDTO.ListaResultado.Add(oCitasDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<CitasDetalleDTO> lista = new List<CitasDetalleDTO>();
                            while (dr.Read())
                            {
                                CitasDetalleDTO oCitasDetalleDTO = new CitasDetalleDTO();
                                oCitasDetalleDTO.idCitaDetalle = Convert.ToInt32(dr["idCitaDetalle"].ToString());
                                oCitasDetalleDTO.idCita = Convert.ToInt32(dr["idCita"].ToString());
                                oCitasDetalleDTO.idTratamiento1 = Convert.ToInt32(dr["idTratamiento1"].ToString());
                                oCitasDetalleDTO.Tratamiento1 = dr["Tratamiento1"].ToString();
                                oCitasDetalleDTO.Monto1 = Convert.ToDecimal(dr["Monto1"].ToString());
                                oCitasDetalleDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                                oCitasDetalleDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                                oCitasDetalleDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                                oCitasDetalleDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                                
                                lista.Add(oCitasDetalleDTO);
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
                    oResultDTO.ListaResultado = new List<CitasDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CitasDTO> ListarxID(int idCita)
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
            oResultDTO.ListaResultado = new List<CitasDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCita", idCita);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CitasDTO oCitasDTO = new CitasDTO();
                        oCitasDTO.idCita = Convert.ToInt32(dr["idCita"].ToString());
                        oCitasDTO.FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString());
                        oCitasDTO.idPaciente = Convert.ToInt32(dr["idPaciente"].ToString());
                        oCitasDTO.NombreCompleto = dr["NombreCompleto"] == null ? "" : dr["NombreCompleto"].ToString();
                        oCitasDTO.idPersonal = Convert.ToInt32(dr["idPersonal"].ToString());
                        oCitasDTO.NombrePersonal = dr["NombrePersonal"] == null ? "" : dr["NombrePersonal"].ToString();
                        oCitasDTO.idServicio = Convert.ToInt32(dr["idServicio"].ToString());
                        oCitasDTO.DescripcionServicio = dr["DescripcionServicio"] == null ? "" : dr["DescripcionServicio"].ToString();
                        oCitasDTO.EstadoPago = Convert.ToInt32(dr["EstadoPago"].ToString());
                        oCitasDTO.Hora = dr["Hora"].ToString();
                        oCitasDTO.idDocRef = Convert.ToInt32(dr["idDocRef"] == null ? 0 : Convert.ToInt32(dr["idDocRef"].ToString()));
                        oCitasDTO.Observaciones = dr["Observaciones"] == null ? "" : dr["Observaciones"].ToString();
                        oCitasDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCitasDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCitasDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oCitasDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oCitasDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oCitasDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oCitasDTO.Pago = Convert.ToDecimal(dr["Pago"] == null ? 0 : Convert.ToDecimal(dr["Pago"].ToString()));
                        oResultDTO.ListaResultado.Add(oCitasDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CitasDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CitasDTO> ValidarCita(int idCita)
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
            oResultDTO.ListaResultado = new List<CitasDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_Validar", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCita", idCita);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CitasDTO oCitasDTO = new CitasDTO();
                        oCitasDTO.idCita = Convert.ToInt32(dr["idCita"].ToString());
                        oCitasDTO.FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString());
                        oCitasDTO.idPaciente = Convert.ToInt32(dr["idPaciente"].ToString());
                        oCitasDTO.NombreCompleto = dr["NombreCompleto"] == null ? "" : dr["NombreCompleto"].ToString();
                        oCitasDTO.idPersonal = Convert.ToInt32(dr["idPersonal"].ToString());
                        oCitasDTO.NombrePersonal = dr["NombrePersonal"] == null ? "" : dr["NombrePersonal"].ToString();
                        oCitasDTO.idServicio = Convert.ToInt32(dr["idServicio"].ToString());
                        oCitasDTO.DescripcionServicio = dr["DescripcionServicio"] == null ? "" : dr["DescripcionServicio"].ToString();
                        oCitasDTO.EstadoPago = Convert.ToInt32(dr["EstadoPago"].ToString());
                        oCitasDTO.Hora = dr["Hora"].ToString();
                        oCitasDTO.idDocRef = Convert.ToInt32(dr["idDocRef"] == null ? 0 : Convert.ToInt32(dr["idDocRef"].ToString()));
                        oCitasDTO.Observaciones = dr["Observaciones"] == null ? "" : dr["Observaciones"].ToString();
                        oCitasDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCitasDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCitasDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oCitasDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oCitasDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oCitasDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oCitasDTO.Pago = Convert.ToDecimal(dr["Pago"] == null ? 0 : Convert.ToDecimal(dr["Pago"].ToString()));
                        oResultDTO.ListaResultado.Add(oCitasDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CitasDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CalendarEventDTO> UpdateInsert(CitasDTO oCitas)
        {
            ResultDTO<CalendarEventDTO> oResultDTO = new ResultDTO<CalendarEventDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_UpdateInserts", cn);//SP_LM_Citas_UpdateInsert
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idCita", oCitas.idCita);
                        da.SelectCommand.Parameters.AddWithValue("@FechaCita", oCitas.FechaCita);
                        da.SelectCommand.Parameters.AddWithValue("@idPaciente", oCitas.idPaciente);
                        da.SelectCommand.Parameters.AddWithValue("@NombreCompleto", oCitas.NombreCompleto);
                        da.SelectCommand.Parameters.AddWithValue("@idPersonal", oCitas.idPersonal);
                        da.SelectCommand.Parameters.AddWithValue("@idServicio", oCitas.idServicio);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoPago", oCitas.EstadoPago);
                        da.SelectCommand.Parameters.AddWithValue("@Hora", oCitas.Hora);
                        da.SelectCommand.Parameters.AddWithValue("@HoraF", oCitas.HoraF);
                        da.SelectCommand.Parameters.AddWithValue("@idDocRef", oCitas.idDocRef);
                        da.SelectCommand.Parameters.AddWithValue("@Observaciones", oCitas.Observaciones);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oCitas.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oCitas.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oCitas.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Pago", oCitas.Pago);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoCita", oCitas.EstadoCita);
                        da.SelectCommand.Parameters.AddWithValue("@Tratamiento", oCitas.Tratamiento);
                        da.SelectCommand.Parameters.AddWithValue("@ListaDetalle", oCitas.cadDetalle);
                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarEventos(oCitas.FechaInicio, oCitas.FechaFin, oCitas.idRol, oCitas.UsuarioCreacion, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<CalendarEventDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<CalendarEventDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CitasDTO> Delete(CitasDTO oCitas)
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idCita", oCitas.idCita);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oCitas.FechaInicio, oCitas.FechaFin, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<CitasDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<CitasDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CalendarEventDTO> ListarEventos(string fechaInicio, string fechaFin, int idRol, int idUsuario, SqlConnection cn = null)
        {
            ResultDTO<CalendarEventDTO> oResultDTO = new ResultDTO<CalendarEventDTO>();
            oResultDTO.ListaResultado = new List<CalendarEventDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_CalendarView", cn);
                    da.SelectCommand.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@fechaFin", fechaFin);
                    da.SelectCommand.Parameters.AddWithValue("@idRol", idRol);
                    da.SelectCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CalendarEventDTO oCalendarEventDTO = new CalendarEventDTO();
                        oCalendarEventDTO.idCita = Convert.ToInt32(dr["idCita"] == null ? 0 : Convert.ToInt32(dr["idCita"].ToString()));
                        oCalendarEventDTO.Color = dr["Color"] == null ? "" : dr["Color"].ToString();
                        oCalendarEventDTO.NombrePaciente = dr["NombrePaciente"] == null ? "" : dr["NombrePaciente"].ToString();
                        oCalendarEventDTO.NombrePersonal = dr["NombrePersonal"] == null ? "" : dr["NombrePersonal"].ToString();
                        oCalendarEventDTO.NombreServicio = dr["NombreServicio"] == null ? "" : dr["NombreServicio"].ToString();
                        oCalendarEventDTO.FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString());
                        oCalendarEventDTO.Hora = dr["Hora"] == null ? "" : dr["Hora"].ToString();
                        oCalendarEventDTO.HoraFin = dr["horaF"] == null ? "" : dr["horaF"].ToString();
                        oCalendarEventDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oCalendarEventDTO.FechaHoraCalendar = oCalendarEventDTO.FechaCita.Year + "-" + oCalendarEventDTO.FechaCita.Month.ToString("00") + "-" + oCalendarEventDTO.FechaCita.Day.ToString("00") + "T" + oCalendarEventDTO.Hora + ":00";
                        oCalendarEventDTO.FechaHoraCalendarF = oCalendarEventDTO.FechaCita.Year + "-" + oCalendarEventDTO.FechaCita.Month.ToString("00") + "-" + oCalendarEventDTO.FechaCita.Day.ToString("00") + "T" + oCalendarEventDTO.HoraFin + ":00";
                        oCalendarEventDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();

                        oResultDTO.ListaResultado.Add(oCalendarEventDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CalendarEventDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CitasDTO> ListarCitasXPagar()
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
            oResultDTO.ListaResultado = new List<CitasDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_ListarxPagar", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CitasDTO oCitasDTO = new CitasDTO();
                        oCitasDTO.idCita = Convert.ToInt32(dr["idCita"].ToString());
                        oCitasDTO.Codigo = dr["Codigo"].ToString();
                        oCitasDTO.Paciente = dr["Paciente"].ToString();
                        oCitasDTO.Pago = Convert.ToDecimal(dr["Pago"].ToString());
                        oCitasDTO.Observaciones = dr["Observaciones"].ToString();
                        oCitasDTO.FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString());
                        oResultDTO.ListaResultado.Add(oCitasDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CitasDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CitasDTO> ListarCitas_Atencion(int idPaciente, SqlConnection cn = null)
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
            oResultDTO.ListaResultado = new List<CitasDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Citas_Listar_Atencion", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idPaciente", idPaciente);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CitasDTO oCitasDTO = new CitasDTO();
                        oCitasDTO.idCita = Convert.ToInt32(dr["idCita"] == null ? 0 : Convert.ToInt32(dr["idCita"].ToString()));
                        oCitasDTO.NombreCompleto = dr["NombreCompleto"] == null ? "" : dr["NombreCompleto"].ToString();
                        oCitasDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oCitasDTO.FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString());
                        oCitasDTO.idPersonal = Convert.ToInt32(dr["idPersonal"] == null ? 0 : Convert.ToInt32(dr["idPersonal"].ToString()));
                        oCitasDTO.NombrePersonal = dr["NombrePersonal"] == null ? "" : dr["NombrePersonal"].ToString();
                        oResultDTO.ListaResultado.Add(oCitasDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CitasDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CitasDTO> ListarCitasEnVenta(SqlConnection cn = null)
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
            oResultDTO.ListaResultado = new List<CitasDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_ListarEnVenta", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CitasDTO oCitasDTO = new CitasDTO();
                        oCitasDTO.idCita = Convert.ToInt32(dr["idCita"] == null ? 0 : Convert.ToInt32(dr["idCita"].ToString()));
                        oCitasDTO.idPaciente = Convert.ToInt32(dr["idPaciente"] == null ? 0 : Convert.ToInt32(dr["idPaciente"].ToString()));
                        oCitasDTO.NombreCompleto = dr["NombreCompleto"] == null ? "" : dr["NombreCompleto"].ToString();
                        oCitasDTO.FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString());
                        oCitasDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oCitasDTO.Pago = Convert.ToDecimal(dr["Pago"] == null ? 0 : Convert.ToDecimal(dr["Pago"].ToString()));
                        oResultDTO.ListaResultado.Add(oCitasDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CitasDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CitasDTO> ListarDetalleCitaxId(int idCita, SqlConnection cn = null)
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
            oResultDTO.ListaResultado = new List<CitasDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_DetalleVenta", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCita", idCita);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CitasDTO oCitasDTO = new CitasDTO();
                        oCitasDTO.Paciente = dr["Paciente"] == null ? "" : dr["Paciente"].ToString();
                        oCitasDTO.Dni = dr["Dni"] == null ? "" : dr["Dni"].ToString();
                        oCitasDTO.Direccion = dr["Direccion"] == null ? "" : dr["Direccion"].ToString();
                        oCitasDTO.Observaciones = dr["Observaciones"] == null ? "" : dr["Observaciones"].ToString();
                        oCitasDTO.Pago = Convert.ToDecimal(dr["Pago"] == null ? 0 : Convert.ToDecimal(dr["Pago"].ToString()));
                        oCitasDTO.idPaciente = Convert.ToInt32(dr["idPaciente"] == null ? 0 : Convert.ToInt32(dr["idPaciente"].ToString()));
                        oResultDTO.ListaResultado.Add(oCitasDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<CitasDetalleDTO> lista = new List<CitasDetalleDTO>();
                            while (dr.Read())
                            {
                                CitasDetalleDTO oCitasDetalleDTO = new CitasDetalleDTO();
                                oCitasDetalleDTO.idCitaDetalle = Convert.ToInt32(dr["idCitaDetalle"].ToString());
                                oCitasDetalleDTO.idTratamiento1 = Convert.ToInt32(dr["idTratamiento1"].ToString());
                                oCitasDetalleDTO.Tratamiento1 = dr["Tratamiento1"].ToString();
                                oCitasDetalleDTO.Monto1 = Convert.ToDecimal(dr["Monto1"].ToString());
                                lista.Add(oCitasDetalleDTO);
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
                    oResultDTO.ListaResultado = new List<CitasDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CalendarEventDTO> ConfirmarCita(CitasDTO oCitas)
        {
            ResultDTO<CalendarEventDTO> oResultDTO = new ResultDTO<CalendarEventDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_ConfirmarCita", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idCita", oCitas.idCita);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoCita", oCitas.EstadoCita);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarEventos(oCitas.FechaInicio, oCitas.FechaFin, oCitas.idRol, oCitas.UsuarioCreacion, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<CalendarEventDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<CalendarEventDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CitasDTO> Listar_ComisionMedico(int id, DateTime fechaIni, DateTime fechaFin, SqlConnection cn = null)
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
            oResultDTO.ListaResultado = new List<CitasDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_Listar_ComisionxMedico", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idMedico", id);
                    da.SelectCommand.Parameters.AddWithValue("@FechaIni", fechaIni);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CitasDTO oCitasDTO = new CitasDTO();
                        oCitasDTO.idServicio = Convert.ToInt32(dr["idServicio"] == null ? 0 : Convert.ToInt32(dr["idServicio"].ToString()));
                        oCitasDTO.Servicio = dr["NombreServicio"] == null ? "" : dr["NombreServicio"].ToString();
                        oCitasDTO.NroTratamiento = Convert.ToInt32(dr["Cantidad"] == null ? 0 : Convert.ToInt32(dr["Cantidad"].ToString()));
                        oCitasDTO.Precio = Convert.ToDecimal(dr["Precio"] == null ? 0 : Convert.ToDecimal(dr["Precio"].ToString()));
                        oCitasDTO.Pago = Convert.ToDecimal(dr["Pago"] == null ? 0 : Convert.ToDecimal(dr["Pago"].ToString()));
                        oCitasDTO.Gasto = Convert.ToDecimal(dr["Gasto"] == null ? 0 : Convert.ToDecimal(dr["Gasto"].ToString()));
                        oCitasDTO.Diferencia = Convert.ToDecimal(dr["Diferencia"] == null ? 0 : Convert.ToDecimal(dr["Diferencia"].ToString()));
                        oCitasDTO.PorcentajeMedico = Convert.ToDecimal(dr["PorcentajeMedico"] == null ? 0 : Convert.ToDecimal(dr["PorcentajeMedico"].ToString()));
                        oCitasDTO.Comision = Math.Round(Convert.ToDecimal(dr["Comision"] == null ? 0 : Convert.ToDecimal(dr["Comision"].ToString())), 3);
                        oCitasDTO.idComisionMedico = Convert.ToInt32(dr["idComisionMedico"] == null ? 0 : Convert.ToInt32(dr["idComisionMedico"].ToString()));
                        oCitasDTO.Costo = oCitasDTO.Pago;
                        oResultDTO.ListaResultado.Add(oCitasDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CitasDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CitasDTO> Listar_ComisionMedico_Detallado(int id, DateTime fechaIni, DateTime fechaFin, SqlConnection cn = null)
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
            oResultDTO.ListaResultado = new List<CitasDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_Listar_ComisionxMedico_Detallado", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idMedico", id);
                    da.SelectCommand.Parameters.AddWithValue("@FechaIni", fechaIni);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CitasDTO oCitasDTO = new CitasDTO();
                        oCitasDTO.FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString());
                        oCitasDTO.idServicio = Convert.ToInt32(dr["idServicio"] == null ? 0 : Convert.ToInt32(dr["idServicio"].ToString()));
                        oCitasDTO.Servicio = dr["NombreServicio"] == null ? "" : dr["NombreServicio"].ToString();
                        oCitasDTO.NombreCompleto = dr["NombreCompleto"] == null ? "" : dr["NombreCompleto"].ToString();
                        oCitasDTO.Precio = Convert.ToDecimal(dr["Precio"] == null ? 0 : Convert.ToDecimal(dr["Precio"].ToString()));                       
                        oResultDTO.ListaResultado.Add(oCitasDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CitasDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CitasDTO> ObtenerCitasxPaciente(CitasDTO oCitas)
        {
            ResultDTO<CitasDTO> oResultDTO = new ResultDTO<CitasDTO>();
            oResultDTO.ListaResultado = new List<CitasDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_Citas_ListarxPaciente", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idPaciente", oCitas.idPaciente);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CitasDTO oCitasDTO = new CitasDTO();
                        oCitasDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oCitasDTO.FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString());                        
                        oCitasDTO.NombrePersonal = dr["NombrePersonal"] == null ? "" : dr["NombrePersonal"].ToString();                        
                        oCitasDTO.Tratamiento = dr["Tratamiento"] == null ? "" : dr["Tratamiento"].ToString();                        
                        oResultDTO.ListaResultado.Add(oCitasDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CitasDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
