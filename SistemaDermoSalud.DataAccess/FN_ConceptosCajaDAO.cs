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
   public class FN_ConceptosCajaDAO
    {
        public ResultDTO<FN_ConceptosCajaDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<FN_ConceptosCajaDTO> oResultDTO = new ResultDTO<FN_ConceptosCajaDTO>();
            oResultDTO.ListaResultado = new List<FN_ConceptosCajaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_ConceptosCaja_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_ConceptosCajaDTO oFN_ConceptosCajaDTO = new FN_ConceptosCajaDTO();
                        oFN_ConceptosCajaDTO.idConceptoCaja = Convert.ToInt32(dr["idConceptoCaja"] == null ? 0 : Convert.ToInt32(dr["idConceptoCaja"].ToString()));
                        oFN_ConceptosCajaDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oFN_ConceptosCajaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oFN_ConceptosCajaDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oFN_ConceptosCajaDTO.AfectoIgv = dr["AfectoIgv"] == null ? "" : dr["AfectoIgv"].ToString();
                        if (oFN_ConceptosCajaDTO.AfectoIgv == "S") { oFN_ConceptosCajaDTO.AfectoIgv = "SI"; } else { oFN_ConceptosCajaDTO.AfectoIgv = "NO"; }
                        oFN_ConceptosCajaDTO.IngresoSalida = dr["IngresoSalida"] == null ? "" : dr["IngresoSalida"].ToString();
                        if (oFN_ConceptosCajaDTO.IngresoSalida == "S") { oFN_ConceptosCajaDTO.IngresoSalida = "SALIDA"; } else { oFN_ConceptosCajaDTO.IngresoSalida = "INGRESO"; }
                        oFN_ConceptosCajaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_ConceptosCajaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_ConceptosCajaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oFN_ConceptosCajaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oFN_ConceptosCajaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oFN_ConceptosCajaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_ConceptosCajaDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_ConceptosCajaDTO> ListarxID(int idConceptoCaja)
        {
            ResultDTO<FN_ConceptosCajaDTO> oResultDTO = new ResultDTO<FN_ConceptosCajaDTO>();
            oResultDTO.ListaResultado = new List<FN_ConceptosCajaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_FN_ConceptosCaja_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idConceptoCaja", idConceptoCaja);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        FN_ConceptosCajaDTO oFN_ConceptosCajaDTO = new FN_ConceptosCajaDTO();
                        oFN_ConceptosCajaDTO.idConceptoCaja = Convert.ToInt32(dr["idConceptoCaja"].ToString());
                        oFN_ConceptosCajaDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oFN_ConceptosCajaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oFN_ConceptosCajaDTO.Descripcion = dr["Descripcion"].ToString();
                        oFN_ConceptosCajaDTO.AfectoIgv = dr["AfectoIgv"].ToString();
                        oFN_ConceptosCajaDTO.IngresoSalida = dr["IngresoSalida"].ToString();
                        oFN_ConceptosCajaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oFN_ConceptosCajaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oFN_ConceptosCajaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oFN_ConceptosCajaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oFN_ConceptosCajaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oFN_ConceptosCajaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<FN_ConceptosCajaDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<FN_ConceptosCajaDTO> UpdateInsert(FN_ConceptosCajaDTO oFN_ConceptosCaja)
        {
            ResultDTO<FN_ConceptosCajaDTO> oResultDTO = new ResultDTO<FN_ConceptosCajaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_ConceptosCaja_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idConceptoCaja", oFN_ConceptosCaja.idConceptoCaja);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oFN_ConceptosCaja.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oFN_ConceptosCaja.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@AfectoIgv", oFN_ConceptosCaja.AfectoIgv);
                        da.SelectCommand.Parameters.AddWithValue("@IngresoSalida", oFN_ConceptosCaja.IngresoSalida);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oFN_ConceptosCaja.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oFN_ConceptosCaja.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oFN_ConceptosCaja.Estado);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(cn).ListaResultado;
                            oResultDTO.Campo1 = id_output.Value.ToString();
                            new Seg_LogDAO().UpdateInsert(da, cn, oFN_ConceptosCaja.idEmpresa, oFN_ConceptosCaja.UsuarioModificacion,
                                "MANTENIMIENTOS-CONCEPTOS CAJA", "FN_ConceptosCaja", (int)id_output.Value, (oFN_ConceptosCaja.idConceptoCaja == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<FN_ConceptosCajaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_ConceptosCajaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<FN_ConceptosCajaDTO> Delete(FN_ConceptosCajaDTO oFN_ConceptosCaja)
        {
            ResultDTO<FN_ConceptosCajaDTO> oResultDTO = new ResultDTO<FN_ConceptosCajaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_ConceptosCaja_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idConceptoCaja", oFN_ConceptosCaja.idConceptoCaja);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo( cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<FN_ConceptosCajaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<FN_ConceptosCajaDTO>();
                    }
                }
            }
            return oResultDTO;
        }

    }
}
