using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Entities;

namespace SistemaDermoSalud.DataAccess.Mantenimiento
{
    public class Ma_SerieDAO
    {
        public ResultDTO<VEN_SerieDTO> ListarRangoFecha(int idEmpresa, string fechaInicio, string fechaFin, SqlConnection cn = null)
        {
            ResultDTO<VEN_SerieDTO> oResultDTO = new ResultDTO<VEN_SerieDTO>();
            oResultDTO.ListaResultado = new List<VEN_SerieDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Serie_ListarxFecha", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_SerieDTO oMa_SerieDTO = new VEN_SerieDTO();
                        oMa_SerieDTO.idSerie = Convert.ToInt32(dr["idSerie"] == null ? 0 : Convert.ToInt32(dr["idSerie"].ToString()));
                        oMa_SerieDTO.idTipoComprobante = Convert.ToInt32(dr["idTipoComprobante"] == null ? 0 : Convert.ToInt32(dr["idTipoComprobante"].ToString()));
                        oMa_SerieDTO.NroSerie = dr["NumeroSerie"] == null ? "" : dr["NumeroSerie"].ToString();
                        oMa_SerieDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        //oMa_SerieDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oMa_SerieDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_SerieDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_SerieDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_SerieDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_SerieDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMa_SerieDTO);
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
        public ResultDTO<VEN_SerieDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<VEN_SerieDTO> oResultDTO = new ResultDTO<VEN_SerieDTO>();
            oResultDTO.ListaResultado = new List<VEN_SerieDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Serie_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_SerieDTO oVEN_SerieDTO = new VEN_SerieDTO();
                        oVEN_SerieDTO.idSerie = Convert.ToInt32(dr["idSerie"] == null ? 0 : Convert.ToInt32(dr["idSerie"].ToString()));
                        oVEN_SerieDTO.NroSerie = dr["NroSerie"] == null ? "" : dr["NroSerie"].ToString();
                        oVEN_SerieDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oVEN_SerieDTO.NumeroDoc = dr["Correlativo"] == null ? "" : dr["Correlativo"].ToString();
                        oVEN_SerieDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
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
        public ResultDTO<VEN_SerieDTO> ListarxID(int idSerie)
        {
            ResultDTO<VEN_SerieDTO> oResultDTO = new ResultDTO<VEN_SerieDTO>();
            oResultDTO.ListaResultado = new List<VEN_SerieDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Serie_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idSerie", idSerie);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        VEN_SerieDTO oMa_SerieDTO = new VEN_SerieDTO();
                        oMa_SerieDTO.idSerie = Convert.ToInt32(dr["idSerie"] == null ? 0 : Convert.ToInt32(dr["idSerie"].ToString()));
                        oMa_SerieDTO.idTipoComprobante = Convert.ToInt32(dr["idTipoComprobante"] == null ? 0 : Convert.ToInt32(dr["idTipoComprobante"].ToString()));
                        oMa_SerieDTO.NroSerie = dr["NroSerie"] == null ? "" : dr["NroSerie"].ToString();
                        oMa_SerieDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_SerieDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_SerieDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_SerieDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_SerieDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMa_SerieDTO);
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
        public ResultDTO<Ma_SerieDTO> ObtenerNumeroVenta(string idSerie)
        {
            ResultDTO<Ma_SerieDTO> oResultDTO = new ResultDTO<Ma_SerieDTO>();
            oResultDTO.ListaResultado = new List<Ma_SerieDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Serie_ObtenerNumeroVenta", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idSerie", idSerie);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_SerieDTO oMa_SerieDTO = new Ma_SerieDTO();
                        oMa_SerieDTO.NumeroSerie = dr["NumeroSerie"] == null ? "" : dr["NumeroSerie"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_SerieDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_SerieDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_SerieDTO> UpdateInsert(VEN_SerieDTO oVEN_SerieDTO, DateTime FechaInicio, DateTime FechaFin)
        {
            ResultDTO<VEN_SerieDTO> oResultDTO = new ResultDTO<VEN_SerieDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Serie_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idSerie  ", oVEN_SerieDTO.idSerie);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoComprobante ", oVEN_SerieDTO.idTipoComprobante);
                        da.SelectCommand.Parameters.AddWithValue("@NroSerie ", oVEN_SerieDTO.NroSerie);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion ", oVEN_SerieDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion ", oVEN_SerieDTO.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oVEN_SerieDTO.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oVEN_SerieDTO.Estado);
                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            int idempresa = 1;
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(idempresa, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<VEN_SerieDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<VEN_SerieDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<VEN_SerieDTO> Delete(VEN_SerieDTO oVEN_SerieDTO, DateTime fechaInicio, DateTime fechaFin)
        {
            ResultDTO<VEN_SerieDTO> oResultDTO = new ResultDTO<VEN_SerieDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_VEN_Serie_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idSerie", oVEN_SerieDTO.idSerie);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(1, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<VEN_SerieDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<VEN_SerieDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
