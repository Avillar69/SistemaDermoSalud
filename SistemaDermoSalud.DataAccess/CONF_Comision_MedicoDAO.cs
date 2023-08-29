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
  public  class CONF_Comision_MedicoDAO
    {
        public ResultDTO<CONF_Comision_MedicoDTO> ListarTodo(int p, SqlConnection cn = null)
        {
            ResultDTO<CONF_Comision_MedicoDTO> oResultDTO = new ResultDTO<CONF_Comision_MedicoDTO>();
            oResultDTO.ListaResultado = new List<CONF_Comision_MedicoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Cargos_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@param", p);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CONF_Comision_MedicoDTO oCONF_Comision_MedicoDTO = new CONF_Comision_MedicoDTO();
                        oCONF_Comision_MedicoDTO.idComisionMedico = Convert.ToInt32(dr["idComisionMedico"] == null ? 0 : Convert.ToInt32(dr["idComisionMedico"].ToString()));
                        oCONF_Comision_MedicoDTO.idCita = Convert.ToInt32(dr["idCita"] == null ? 0 : Convert.ToInt32(dr["idCita"].ToString()));
                        oCONF_Comision_MedicoDTO.FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString());
                        oCONF_Comision_MedicoDTO.codigo = Convert.ToInt32(dr["codigo"] == null ? 0 : Convert.ToInt32(dr["codigo"].ToString()));
                        oCONF_Comision_MedicoDTO.idServicio = Convert.ToInt32(dr["idServicio"] == null ? 0 : Convert.ToInt32(dr["idServicio"].ToString()));
                        oCONF_Comision_MedicoDTO.DescripcionServicio = dr["DescripcionServicio"] == null ? "" : dr["DescripcionServicio"].ToString();
                        oCONF_Comision_MedicoDTO.Monto = Convert.ToDecimal(dr["Monto"] == null ? 0 : Convert.ToDecimal(dr["Monto"].ToString()));
                        oCONF_Comision_MedicoDTO.Gasto = Convert.ToDecimal(dr["Gasto"] == null ? 0 : Convert.ToDecimal(dr["Gasto"].ToString()));
                        oCONF_Comision_MedicoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                        oCONF_Comision_MedicoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"]);
                        oCONF_Comision_MedicoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oCONF_Comision_MedicoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oCONF_Comision_MedicoDTO.Estado = Convert.ToBoolean(dr["Estado"]);
                       
                        oResultDTO.ListaResultado.Add(oCONF_Comision_MedicoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CONF_Comision_MedicoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CONF_Comision_MedicoDTO> ListarxID(int idCargo)
        {
            ResultDTO<CONF_Comision_MedicoDTO> oResultDTO = new ResultDTO<CONF_Comision_MedicoDTO>();
            oResultDTO.ListaResultado = new List<CONF_Comision_MedicoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_RHCARGOS_ListaxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idCargo", idCargo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        CONF_Comision_MedicoDTO oCONF_Comision_MedicoDTO = new CONF_Comision_MedicoDTO();
      
                        oCONF_Comision_MedicoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCONF_Comision_MedicoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCONF_Comision_MedicoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oCONF_Comision_MedicoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oCONF_Comision_MedicoDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oCONF_Comision_MedicoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CONF_Comision_MedicoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CONF_Comision_MedicoDTO> UpdateInsert(CONF_Comision_MedicoDTO oCONF_Comision_MedicoDTO)
        {
            ResultDTO<CONF_Comision_MedicoDTO> oResultDTO = new ResultDTO<CONF_Comision_MedicoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_RHCARGOS_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                      
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oCONF_Comision_MedicoDTO.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(1, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<CONF_Comision_MedicoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<CONF_Comision_MedicoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CONF_Comision_MedicoDTO> Delete(CONF_Comision_MedicoDTO oCONF_Comision_MedicoDTO)
        {
            ResultDTO<CONF_Comision_MedicoDTO> oResultDTO = new ResultDTO<CONF_Comision_MedicoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_RHCARGOS_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                     
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(1, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<CONF_Comision_MedicoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<CONF_Comision_MedicoDTO>();
                    }
                }
            }
            return oResultDTO;
        }





    }
}
