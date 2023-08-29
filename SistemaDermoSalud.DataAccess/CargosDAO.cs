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
    public class CargosDAO
    {
        public ResultDTO<CargosDTO> ListarTodo(int p, SqlConnection cn = null)
        {
            ResultDTO<CargosDTO> oResultDTO = new ResultDTO<CargosDTO>();
            oResultDTO.ListaResultado = new List<CargosDTO>();
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
                        CargosDTO oCargosDTO = new CargosDTO();
                        oCargosDTO.idCargo = Convert.ToInt32(dr["idCargo"] == null ? 0 : Convert.ToInt32(dr["idCargo"].ToString()));
                        oCargosDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oCargosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCargosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCargosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oCargosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oCargosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oCargosDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CargosDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CargosDTO> ListarxID(int idCargo)
        {
            ResultDTO<CargosDTO> oResultDTO = new ResultDTO<CargosDTO>();
            oResultDTO.ListaResultado = new List<CargosDTO>();
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
                        CargosDTO oCargosDTO = new CargosDTO();
                        oCargosDTO.idCargo = Convert.ToInt32(dr["idCargo"].ToString());
                        oCargosDTO.Descripcion = dr["Descripcion"].ToString();
                        oCargosDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCargosDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oCargosDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oCargosDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oCargosDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oCargosDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<CargosDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CargosDTO> UpdateInsert(CargosDTO oCargosDTO)
        {
            ResultDTO<CargosDTO> oResultDTO = new ResultDTO<CargosDTO>();
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
                        da.SelectCommand.Parameters.AddWithValue("@idCargo", oCargosDTO.idCargo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oCargosDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oCargosDTO.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oCargosDTO.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oCargosDTO.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(1,cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<CargosDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<CargosDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<CargosDTO> Delete(CargosDTO oCargosDTO)
        {
            ResultDTO<CargosDTO> oResultDTO = new ResultDTO<CargosDTO>();
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
                        da.SelectCommand.Parameters.AddWithValue("@idCargo", oCargosDTO.idCargo);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(1,cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<CargosDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<CargosDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
