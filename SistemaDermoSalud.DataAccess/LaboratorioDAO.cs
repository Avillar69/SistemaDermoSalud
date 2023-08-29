using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SistemaDermoSalud.Entities;
using System.Data;
using System.Data.SqlClient;

namespace SistemaDermoSalud.DataAccess
{
    public class LaboratorioDAO
    {
        public ResultDTO<LaboratorioDTO> ListarTodo(int p, SqlConnection cn = null)
        {
            ResultDTO<LaboratorioDTO> oResultDTO = new ResultDTO<LaboratorioDTO>();
            oResultDTO.ListaResultado = new List<LaboratorioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Laboratorio_Listar", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@param", p);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        LaboratorioDTO oLaboratorioDTO = new LaboratorioDTO();
                        oLaboratorioDTO.idLaboratorio = Convert.ToInt32(dr["idLaboratorio"] == null ? 0 : Convert.ToInt32(dr["idLaboratorio"].ToString()));
                        oLaboratorioDTO.Laboratorio = dr["Laboratorio"] == null ? "" : dr["Laboratorio"].ToString();
                        oLaboratorioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oLaboratorioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oLaboratorioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oLaboratorioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oLaboratorioDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oLaboratorioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<LaboratorioDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<LaboratorioDTO> ListarxID(int idLaboratorio)
        {
            ResultDTO<LaboratorioDTO> oResultDTO = new ResultDTO<LaboratorioDTO>();
            oResultDTO.ListaResultado = new List<LaboratorioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_LaboratorioXID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idLaboratorio", idLaboratorio);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        LaboratorioDTO oLaboratorioDTO = new LaboratorioDTO();
                        oLaboratorioDTO.idLaboratorio = Convert.ToInt32(dr["idLaboratorio"] == null ? 0 : Convert.ToInt32(dr["idLaboratorio"].ToString()));
                        oLaboratorioDTO.Laboratorio = dr["Laboratorio"] == null ? "" : dr["Laboratorio"].ToString();
                        oLaboratorioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oLaboratorioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oLaboratorioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oLaboratorioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oLaboratorioDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oLaboratorioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<LaboratorioDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<LaboratorioDTO> UpdateInsert(LaboratorioDTO oLaboratorioDTO)
        {
            ResultDTO<LaboratorioDTO> oResultDTO = new ResultDTO<LaboratorioDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Laboratorio_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idLaboratorio", oLaboratorioDTO.idLaboratorio);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oLaboratorioDTO.Laboratorio);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oLaboratorioDTO.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oLaboratorioDTO.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("Estado", oLaboratorioDTO.Estado);
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
                            oResultDTO.ListaResultado = new List<LaboratorioDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<LaboratorioDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<LaboratorioDTO> Delete(LaboratorioDTO oLaboratorio)
        {
            ResultDTO<LaboratorioDTO> oResultDTO = new ResultDTO<LaboratorioDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Laboratorio_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idLaboratorio", oLaboratorio.idLaboratorio);
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
                            oResultDTO.ListaResultado = new List<LaboratorioDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<LaboratorioDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
