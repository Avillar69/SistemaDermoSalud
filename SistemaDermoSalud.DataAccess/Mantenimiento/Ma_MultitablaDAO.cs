using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SistemaDermoSalud.DataAccess.Mantenimiento
{
    public class Ma_MultitablaDAO
    {
        public ResultDTO<Ma_MultitablaDTO> ListarTodo(string tabla, SqlConnection cn = null)
        {
            ResultDTO<Ma_MultitablaDTO> oResultDTO = new ResultDTO<Ma_MultitablaDTO>();
            oResultDTO.ListaResultado = new List<Ma_MultitablaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Multitabla_Listar", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@tabla", tabla);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_MultitablaDTO oMultitablaDTO = new Ma_MultitablaDTO();
                        oMultitablaDTO.id = Convert.ToInt32(dr["id"] == null ? 0 : Convert.ToInt32(dr["id"].ToString()));
                        oMultitablaDTO.Tabla = dr["Tabla"] == null ? "" : dr["Tabla"].ToString();
                        oMultitablaDTO.Campo1 = dr["Campo1"] == null ? "" : dr["Campo1"].ToString();
                        oMultitablaDTO.Campo2 = dr["Campo2"] == null ? "" : dr["Campo2"].ToString();
                        oMultitablaDTO.Campo3 = dr["Campo3"] == null ? "" : dr["Campo3"].ToString();
                        oMultitablaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMultitablaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaEdicion"].ToString());
                        oMultitablaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMultitablaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioEdicion"] == null ? 0 : Convert.ToInt32(dr["UsuarioEdicion"].ToString()));
                        oMultitablaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMultitablaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_MultitablaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_MultitablaDTO> ListarxID(int id)
        {
            ResultDTO<Ma_MultitablaDTO> oResultDTO = new ResultDTO<Ma_MultitablaDTO>();
            oResultDTO.ListaResultado = new List<Ma_MultitablaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_MultitablaXID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@id", id);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_MultitablaDTO oMultitablaDTO = new Ma_MultitablaDTO();
                        oMultitablaDTO.id = Convert.ToInt32(dr["id"] == null ? 0 : Convert.ToInt32(dr["id"].ToString()));
                        oMultitablaDTO.Tabla = dr["Tabla"] == null ? "" : dr["Tabla"].ToString();
                        oMultitablaDTO.Campo1 = dr["Campo1"] == null ? "" : dr["Campo1"].ToString();
                        oMultitablaDTO.Campo2 = dr["Campo2"] == null ? "" : dr["Campo2"].ToString();
                        oMultitablaDTO.Campo3 = dr["Campo3"] == null ? "" : dr["Campo3"].ToString();
                        oMultitablaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMultitablaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaEdicion"].ToString());
                        oMultitablaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMultitablaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioEdicion"] == null ? 0 : Convert.ToInt32(dr["UsuarioEdicion"].ToString()));
                        oMultitablaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMultitablaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_MultitablaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_MultitablaDTO> UpdateInsert(Ma_MultitablaDTO oMultitablaDTO)
        {
            ResultDTO<Ma_MultitablaDTO> oResultDTO = new ResultDTO<Ma_MultitablaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Multitabla_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@id", oMultitablaDTO.id);
                        da.SelectCommand.Parameters.AddWithValue("@Tabla", oMultitablaDTO.Tabla);
                        da.SelectCommand.Parameters.AddWithValue("@Campo1", oMultitablaDTO.Campo1);
                        da.SelectCommand.Parameters.AddWithValue("@Campo2", oMultitablaDTO.Campo2);
                        da.SelectCommand.Parameters.AddWithValue("@Campo3", oMultitablaDTO.Campo3);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMultitablaDTO.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMultitablaDTO.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("Estado", oMultitablaDTO.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oMultitablaDTO.Tabla, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_MultitablaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_MultitablaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_MultitablaDTO> Delete(Ma_MultitablaDTO oMultitabla)
        {
            ResultDTO<Ma_MultitablaDTO> oResultDTO = new ResultDTO<Ma_MultitablaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Multitabla_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@id", oMultitabla.id);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oMultitabla.Tabla, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_MultitablaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_MultitablaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
