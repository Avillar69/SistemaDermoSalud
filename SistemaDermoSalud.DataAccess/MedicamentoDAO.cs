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
    public class MedicamentoDAO
    {
        public ResultDTO<MedicamentoDTO> ListarporLaboratorio(int p, SqlConnection cn = null)
        {
            ResultDTO<MedicamentoDTO> oResultDTO = new ResultDTO<MedicamentoDTO>();
            oResultDTO.ListaResultado = new List<MedicamentoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Medicamento_Laboratorio", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@param", p);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        MedicamentoDTO oMedicamentoDTO = new MedicamentoDTO();
                        oMedicamentoDTO.idMedicamentos = Convert.ToInt32(dr["idMedicamentos"] == null ? 0 : Convert.ToInt32(dr["idMedicamentos"].ToString()));
                        oMedicamentoDTO.idLaboratorio = Convert.ToInt32(dr["idLaboratorio"] == null ? 0 : Convert.ToInt32(dr["idLaboratorio"].ToString()));
                        oMedicamentoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oResultDTO.ListaResultado.Add(oMedicamentoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<MedicamentoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<MedicamentoDTO> ListarTodo(int p, SqlConnection cn = null)
        {
            ResultDTO<MedicamentoDTO> oResultDTO = new ResultDTO<MedicamentoDTO>();
            oResultDTO.ListaResultado = new List<MedicamentoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Medicamento_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@param", p);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        MedicamentoDTO oMedicamentoDTO = new MedicamentoDTO();
                        oMedicamentoDTO.idMedicamentos = Convert.ToInt32(dr["idMedicamentos"] == null ? 0 : Convert.ToInt32(dr["idMedicamentos"].ToString()));
                        oMedicamentoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMedicamentoDTO.idLaboratorio = Convert.ToInt32(dr["idLaboratorio"] == null ? 0 : Convert.ToInt32(dr["idLaboratorio"].ToString()));
                        oMedicamentoDTO.Laboratorio = dr["Laboratorio"] == null ? "" : dr["Laboratorio"].ToString();
                        oMedicamentoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMedicamentoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMedicamentoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMedicamentoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMedicamentoDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oMedicamentoDTO.PagoMedicamento = Convert.ToDecimal(dr["PagoMedicamento"].ToString());
                        oMedicamentoDTO.CodigoMedicamento = dr["CodigoMedicamento"] == null ? "" : dr["CodigoMedicamento"].ToString();
                        oResultDTO.ListaResultado.Add(oMedicamentoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<MedicamentoDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<MedicamentoDTO> ListarxID(int idMedicamentos)
        {
            ResultDTO<MedicamentoDTO> oResultDTO = new ResultDTO<MedicamentoDTO>();
            oResultDTO.ListaResultado = new List<MedicamentoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Medicamento_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idMedicamentos", idMedicamentos);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        MedicamentoDTO oMedicamentoDTO = new MedicamentoDTO();
                        oMedicamentoDTO.idMedicamentos = Convert.ToInt32(dr["idMedicamentos"].ToString());
                        oMedicamentoDTO.Descripcion = dr["Descripcion"].ToString();
                        oMedicamentoDTO.idLaboratorio = Convert.ToInt32(dr["idLaboratorio"].ToString());
                        oMedicamentoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMedicamentoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMedicamentoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMedicamentoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMedicamentoDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oMedicamentoDTO.PagoMedicamento = Convert.ToDecimal(dr["PagoMedicamento"].ToString());
                        oMedicamentoDTO.CodigoMedicamento = dr["CodigoMedicamento"].ToString();
                        oMedicamentoDTO.CodigoAutogenerado = dr["CodigoAutogenerado"].ToString();
                        oResultDTO.ListaResultado.Add(oMedicamentoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<MedicamentoDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<MedicamentoDTO> UpdateInsert(MedicamentoDTO oMedicamento)
        {
            ResultDTO<MedicamentoDTO> oResultDTO = new ResultDTO<MedicamentoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Medicamento_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMedicamentos", oMedicamento.idMedicamentos);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMedicamento.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@idLaboratorio", oMedicamento.idLaboratorio);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMedicamento.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMedicamento.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMedicamento.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@PagoMedicamento", oMedicamento.PagoMedicamento);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoMedicamento", oMedicamento.CodigoMedicamento);

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
                            oResultDTO.ListaResultado = new List<MedicamentoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<MedicamentoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<MedicamentoDTO> Delete(MedicamentoDTO oMedicamento)
        {
            ResultDTO<MedicamentoDTO> oResultDTO = new ResultDTO<MedicamentoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Medicamento_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMedicamentos", oMedicamento.idMedicamentos);
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
                            oResultDTO.ListaResultado = new List<MedicamentoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<MedicamentoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
