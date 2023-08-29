using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDermoSalud.Entities;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace SistemaDermoSalud.DataAccess
{
    public class AtencionMedicaDAO
    {
        public ResultDTO<AtencionMedicaDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<AtencionMedicaDTO> oResultDTO = new ResultDTO<AtencionMedicaDTO>();
            oResultDTO.ListaResultado = new List<AtencionMedicaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_AtencionMedica_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AtencionMedicaDTO oAtencionMedicaDTO = new AtencionMedicaDTO();
                        oAtencionMedicaDTO.idAtencionMedica = Convert.ToInt32(dr["idAtencionMedica"] == null ? 0 : Convert.ToInt32(dr["idAtencionMedica"].ToString()));
                        oAtencionMedicaDTO.idCita = Convert.ToInt32(dr["idCita"] == null ? 0 : Convert.ToInt32(dr["idCita"].ToString()));
                        oAtencionMedicaDTO.idPersonal = Convert.ToInt32(dr["idPersonal"] == null ? 0 : Convert.ToInt32(dr["idPersonal"].ToString()));
                        oAtencionMedicaDTO.Personal = dr["Personal"] == null ? "" : dr["Personal"].ToString();
                        oAtencionMedicaDTO.PlanTerapeutico = dr["PlanTerapeutico"] == null ? "" : dr["PlanTerapeutico"].ToString();
                        oAtencionMedicaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oAtencionMedicaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oAtencionMedicaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oAtencionMedicaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oAtencionMedicaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oAtencionMedicaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AtencionMedicaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AtencionMedicaDTO> ListarxID(int idAtencionMedica)
        {
            ResultDTO<AtencionMedicaDTO> oResultDTO = new ResultDTO<AtencionMedicaDTO>();
            oResultDTO.ListaResultado = new List<AtencionMedicaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_AtencionMedica_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idAtencionMedica", idAtencionMedica);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AtencionMedicaDTO oAtencionMedicaDTO = new AtencionMedicaDTO();
                        oAtencionMedicaDTO.idAtencionMedica = Convert.ToInt32(dr["idAtencionMedica"].ToString());
                        oAtencionMedicaDTO.idPersonal = Convert.ToInt32(dr["idPersonal"].ToString());
                        oAtencionMedicaDTO.Codigo = dr["Codigo"].ToString();
                        oAtencionMedicaDTO.Personal = dr["Personal"].ToString();
                        oAtencionMedicaDTO.PlanTerapeutico = dr["PlanTerapeutico"].ToString();
                        oAtencionMedicaDTO.idCita = Convert.ToInt32(dr["idCita"].ToString());
                        oResultDTO.ListaResultado.Add(oAtencionMedicaDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<AtencionMedica_RecetaDTO> listaReceta = new List<AtencionMedica_RecetaDTO>();
                            while (dr.Read())
                            {
                                AtencionMedica_RecetaDTO objReceta = new AtencionMedica_RecetaDTO();
                                objReceta.idAtencionMedica_Receta = Convert.ToInt32(dr["idReceta"].ToString());
                                objReceta.idAtencionMedica = Convert.ToInt32(dr["idAtencionMedica"].ToString());
                                objReceta.NroReceta = dr["NroReceta"].ToString();
                                objReceta.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                                //objReceta.Medicamento = dr["Medicamento"].ToString();
                                //objReceta.Dosis = dr["Dosis"].ToString();
                                //objReceta.Via = dr["Via"].ToString();
                                //objReceta.Frecuencia = dr["Frecuencia"].ToString();
                                //objReceta.Duracion = dr["Duracion"].ToString();
                                listaReceta.Add(objReceta);
                            }
                            oResultDTO.ListaResultado[0].oListaRecetas = listaReceta;


                        }
                        if (dr.NextResult())
                        {
                            List<AtencionMedica_EvolucionDTO> listaEvolucion = new List<AtencionMedica_EvolucionDTO>();
                            while (dr.Read())
                            {
                                AtencionMedica_EvolucionDTO objEvolucion = new AtencionMedica_EvolucionDTO();
                                objEvolucion.idAtencionMedica_Evolucion = Convert.ToInt32(dr["idEvolucion"].ToString());
                                objEvolucion.idAtencionMedica = Convert.ToInt32(dr["idAtencionMedica"].ToString());
                                objEvolucion.FechaEvolucion = Convert.ToDateTime(dr["FechaEvolucion"].ToString());
                                objEvolucion.Descripcion = dr["Descripcion"].ToString();
                                listaEvolucion.Add(objEvolucion);
                            }
                            oResultDTO.ListaResultado[0].oListaEvolucion = listaEvolucion;
                        }
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AtencionMedicaDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<AtencionMedicaDTO> UpdateInsert(AtencionMedicaDTO oAtencionMedicaDTO)
        {
            ResultDTO<AtencionMedicaDTO> oResultDTO = new ResultDTO<AtencionMedicaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_LM_AtencionMedica_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idAtencionMedica", oAtencionMedicaDTO.idAtencionMedica);
                        da.SelectCommand.Parameters.AddWithValue("@idCita", oAtencionMedicaDTO.idCita);
                        da.SelectCommand.Parameters.AddWithValue("@idPersonal", oAtencionMedicaDTO.idPersonal);
                        da.SelectCommand.Parameters.AddWithValue("@Personal", oAtencionMedicaDTO.Personal);
                        da.SelectCommand.Parameters.AddWithValue("@PlanTerapeutico", oAtencionMedicaDTO.PlanTerapeutico);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oAtencionMedicaDTO.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oAtencionMedicaDTO.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oAtencionMedicaDTO.Estado);
                        //da.SelectCommand.Parameters.AddWithValue("@lista_Cab_Recetas", oAtencionMedicaDTO.lista_Cab_Recetas);
                        //da.SelectCommand.Parameters.AddWithValue("@lista_Recetas", oAtencionMedicaDTO.lista_Recetas);
                        da.SelectCommand.Parameters.AddWithValue("@lista_Evolucion", oAtencionMedicaDTO.lista_Evolucion);
                        da.SelectCommand.Parameters.AddWithValue("@MotivoConsulta", oAtencionMedicaDTO.MotivoConsulta);
                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarxPaciente(oAtencionMedicaDTO.idPaciente, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<AtencionMedicaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<AtencionMedicaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AtencionMedicaDTO> Delete(AtencionMedicaDTO oAtencionMedicaDTO)
        {
            ResultDTO<AtencionMedicaDTO> oResultDTO = new ResultDTO<AtencionMedicaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_AtencionMedica_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idAtencionMedica", oAtencionMedicaDTO.idAtencionMedica);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<AtencionMedicaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<AtencionMedicaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AtencionMedicaDTO> ListarxPaciente(int idPaciente, SqlConnection cn = null)
        {
            ResultDTO<AtencionMedicaDTO> oResultDTO = new ResultDTO<AtencionMedicaDTO>();
            oResultDTO.ListaResultado = new List<AtencionMedicaDTO>();
            using (cn = cn ?? new Conexion().conectar())
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_AtencionMedica_ListarxIDPaciente", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idPaciente", idPaciente);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AtencionMedicaDTO oAtencionMedicaDTO = new AtencionMedicaDTO();
                        oAtencionMedicaDTO.idAtencionMedica = Convert.ToInt32(dr["idAtencionMedica"].ToString());
                        oAtencionMedicaDTO.idCita = Convert.ToInt32(dr["idCita"].ToString());
                        oAtencionMedicaDTO.idPersonal = Convert.ToInt32(dr["idPersonal"].ToString());
                        oAtencionMedicaDTO.Personal = dr["Personal"].ToString();
                        oAtencionMedicaDTO.PlanTerapeutico = dr["PlanTerapeutico"].ToString();
                        oAtencionMedicaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oAtencionMedicaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oAtencionMedicaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oAtencionMedicaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oAtencionMedicaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oAtencionMedicaDTO.FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString());
                        oResultDTO.ListaResultado.Add(oAtencionMedicaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AtencionMedicaDTO>();
                }
            }
            return oResultDTO;
        }
        public string NroRecetaUltimo()
        {
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_AtencionMedica_Receta_UltimoNumero", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    var nroreceta = da.SelectCommand.ExecuteScalar();
                    return nroreceta.ToString();
                }
                catch (Exception ex)
                {
                    return "0";
                }
            }
        }
        public string UltimoIdReceta()
        {
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_AtencionMedica_UltimoId", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    var idAtencion = da.SelectCommand.ExecuteScalar();
                    return idAtencion.ToString();
                }
                catch (Exception ex)
                {
                    return "0";
                }
            }
        }
        public ResultDTO<AtencionMedicaDTO> UpdateInsertReceta(AtencionMedicaDTO oAtencionMedicaDTO)
        {
            ResultDTO<AtencionMedicaDTO> oResultDTO = new ResultDTO<AtencionMedicaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_LM_AtencionMedica_Receta_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idReceta", oAtencionMedicaDTO.idReceta);
                        da.SelectCommand.Parameters.AddWithValue("@idAtencionMedica", oAtencionMedicaDTO.idAtencionMedica);
                        da.SelectCommand.Parameters.AddWithValue("@NroReceta", oAtencionMedicaDTO.NroReceta);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oAtencionMedicaDTO.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oAtencionMedicaDTO.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oAtencionMedicaDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@idCita", oAtencionMedicaDTO.idCita);
                        da.SelectCommand.Parameters.AddWithValue("@lista_Recetas", oAtencionMedicaDTO.lista_Recetas);
                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(cn).ListaResultado;
                            oResultDTO.Campo1 = Convert.ToInt32(id_output.Value).ToString();
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<AtencionMedicaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<AtencionMedicaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AtencionMedica_RecetaDTO> ObtenerRecetaxID(int idReceta)
        {
            ResultDTO<AtencionMedica_RecetaDTO> oResultDTO = new ResultDTO<AtencionMedica_RecetaDTO>();
            oResultDTO.ListaResultado = new List<AtencionMedica_RecetaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_AtencionMedica_RecetasxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idReceta", idReceta);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AtencionMedica_RecetaDTO oAtencionMedicaDTO = new AtencionMedica_RecetaDTO();
                        oAtencionMedicaDTO.idAtencionMedica = Convert.ToInt32(dr["idAtencionMedica"].ToString());
                        oAtencionMedicaDTO.idAtencionMedica_Receta = Convert.ToInt32(dr["idReceta"].ToString());
                        oAtencionMedicaDTO.idReceta = Convert.ToInt32(dr["idReceta"].ToString());
                        oAtencionMedicaDTO.idRecetaDetalle = Convert.ToInt32(dr["idRecetaDetalle"].ToString());
                        oAtencionMedicaDTO.Medicamento = dr["Medicamento"].ToString();
                        oAtencionMedicaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oAtencionMedicaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oAtencionMedicaDTO.Via = dr["Via"].ToString();
                        oAtencionMedicaDTO.Dosis = dr["Dosis"].ToString();
                        oAtencionMedicaDTO.Duracion = dr["Duracion"].ToString();
                        oAtencionMedicaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oAtencionMedicaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oAtencionMedicaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oAtencionMedicaDTO.Frecuencia = dr["Frecuencia"].ToString();
                        oResultDTO.ListaResultado.Add(oAtencionMedicaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AtencionMedica_RecetaDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
