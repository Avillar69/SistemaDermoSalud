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
    public class MonedaDAO
    {
        public ResultDTO<MonedaDTO>ListarTodo(SqlConnection cn = null)
        {
        ResultDTO<MonedaDTO> oResultDTO = new ResultDTO<MonedaDTO>();
        oResultDTO.ListaResultado = new List<MonedaDTO>();
            using((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_MA_Moneda_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while(dr.Read())
                    {
                        MonedaDTO oMonedaDTO = new MonedaDTO();
                        oMonedaDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null?0:Convert.ToInt32(dr["idMoneda"].ToString()));
                        oMonedaDTO.Descripcion = dr["Descripcion"]==null ? "":dr["Descripcion"].ToString();
                        oMonedaDTO.Codigo = dr["CodigoGenerado"] ==null ? "":dr["CodigoGenerado"].ToString();
                        oMonedaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMonedaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMonedaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null?0:Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMonedaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null?0:Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMonedaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null?false:Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMonedaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch(Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<MonedaDTO>();
                }
            }
        return oResultDTO;
        }

       public ResultDTO<MonedaDTO>ListarxID(int idMoneda)
        {
        ResultDTO<MonedaDTO> oResultDTO = new ResultDTO<MonedaDTO>();
        oResultDTO.ListaResultado = new List<MonedaDTO>();
            using(SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_MA_Moneda_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idMoneda",idMoneda);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while(dr.Read())
                    {
                        MonedaDTO oMonedaDTO = new MonedaDTO();
                        oMonedaDTO.idMoneda =Convert.ToInt32(dr["idMoneda"].ToString());
                        oMonedaDTO.Descripcion =dr["Descripcion"].ToString();
                        oMonedaDTO.Codigo = dr["CodigoGenerado"].ToString();
                        oMonedaDTO.FechaCreacion =Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMonedaDTO.FechaModificacion =Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMonedaDTO.UsuarioCreacion =Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMonedaDTO.UsuarioModificacion =Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMonedaDTO.Estado =Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMonedaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch(Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<MonedaDTO>();
                }
            }
        return oResultDTO;
        }

        public ResultDTO<MonedaDTO> UpdateInsert(MonedaDTO oMoneda)
        {
            ResultDTO<MonedaDTO> oResultDTO = new ResultDTO<MonedaDTO>();
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(60)
            };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                using(SqlConnection cn = new Conexion().conectar())
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SP_MA_Moneda_UpdateInsert",cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMoneda",oMoneda.idMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion",oMoneda.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion",oMoneda.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion",oMoneda.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado",oMoneda.Estado);
                        int rpta =  da.SelectCommand.ExecuteNonQuery();
                        if(rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<MonedaDTO>();
                        }
                    }
                    catch(Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<MonedaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public  ResultDTO<MonedaDTO> Delete(MonedaDTO oMoneda)
        {
            ResultDTO<MonedaDTO> oResultDTO = new ResultDTO<MonedaDTO>();
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(60)
            };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                using(SqlConnection cn = new Conexion().conectar())
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SP_MA_Moneda_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMoneda",oMoneda.idMoneda);
                        int rpta =  da.SelectCommand.ExecuteNonQuery();
                        if(rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<MonedaDTO>();
                        }
                    }
                    catch(Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<MonedaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
