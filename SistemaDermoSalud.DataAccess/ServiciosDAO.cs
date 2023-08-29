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
    public class ServiciosDAO
    {
        public ResultDTO<ServiciosDTO>ListarTodo(SqlConnection cn = null)
        {
        ResultDTO<ServiciosDTO> oResultDTO = new ResultDTO<ServiciosDTO>();
        oResultDTO.ListaResultado = new List<ServiciosDTO>();
            using((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Servicios_ListarTodo",cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while(dr.Read())
                    {
                        ServiciosDTO oServiciosDTO = new ServiciosDTO();
                        oServiciosDTO.idServicio = Convert.ToInt32(dr["idServicio"] == null?0:Convert.ToInt32(dr["idServicio"].ToString()));
                        oServiciosDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oServiciosDTO.NombreServicio = dr["NombreServicio"]==null ? "":dr["NombreServicio"].ToString();
                        oServiciosDTO.Estado = Convert.ToBoolean(dr["Estado"] == null?false:Convert.ToBoolean(dr["Estado"].ToString()));
                        oServiciosDTO.descripcionTipoServicio = dr["descripcionTipoServicio"] == null ? "" : dr["descripcionTipoServicio"].ToString();
                        oServiciosDTO.Precio= Convert.ToDecimal(dr["Precio"] == null ? 0 : Convert.ToDecimal(dr["Precio"].ToString()));
                        oResultDTO.ListaResultado.Add(oServiciosDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch(Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<ServiciosDTO>();
                }
            }
        return oResultDTO;
        }

       public ResultDTO<ServiciosDTO>ListarxID(int idServicio)
        {
        ResultDTO<ServiciosDTO> oResultDTO = new ResultDTO<ServiciosDTO>();
        oResultDTO.ListaResultado = new List<ServiciosDTO>();
            using(SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Servicios_ListarxID",cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idServicio",idServicio);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while(dr.Read())
                    {
                        ServiciosDTO oServiciosDTO = new ServiciosDTO();
                        oServiciosDTO.idServicio =Convert.ToInt32(dr["idServicio"].ToString());
                        oServiciosDTO.NombreServicio =dr["NombreServicio"].ToString();
                        oServiciosDTO.Descripcion =dr["Descripcion"].ToString();
                        oServiciosDTO.FechaCreacion =Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oServiciosDTO.FechaModificacion =Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oServiciosDTO.UsuarioCreacion =Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oServiciosDTO.UsuarioModificacion =Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oServiciosDTO.Estado =Convert.ToBoolean(dr["Estado"].ToString());
                        oServiciosDTO.idTipoServicio = Convert.ToInt32(dr["idTipoServicio"] == null ? 0 : Convert.ToInt32(dr["idTipoServicio"].ToString()));
                        oServiciosDTO.descripcionTipoServicio = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oServiciosDTO.Precio = Convert.ToDecimal(dr["Precio"] == null ? 0 : Convert.ToDecimal(dr["Precio"].ToString()));
                        oResultDTO.ListaResultado.Add(oServiciosDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch(Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<ServiciosDTO>();
                }
            }
        return oResultDTO;
        }

        public ResultDTO<ServiciosDTO> UpdateInsert(ServiciosDTO oServicios)
        {
            ResultDTO<ServiciosDTO> oResultDTO = new ResultDTO<ServiciosDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Servicios_UpdateInsert",cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idServicio",oServicios.idServicio);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoServicio", oServicios.idTipoServicio);
                        da.SelectCommand.Parameters.AddWithValue("@NombreServicio",oServicios.NombreServicio);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion",oServicios.Descripcion);
                        //da.SelectCommand.Parameters.AddWithValue("@Precio", oServicios.Precio);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion",oServicios.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion",oServicios.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado",oServicios.Estado);
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
                            oResultDTO.ListaResultado = new List<ServiciosDTO>();
                        }
                    }
                    catch(Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<ServiciosDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public  ResultDTO<ServiciosDTO> Delete(ServiciosDTO oServicios)
        {
            ResultDTO<ServiciosDTO> oResultDTO = new ResultDTO<ServiciosDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Servicios_Delete",cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idServicio",oServicios.idServicio);
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
                            oResultDTO.ListaResultado = new List<ServiciosDTO>();
                        }
                    }
                    catch(Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<ServiciosDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<ServiciosDTO> UpdateInsertPrecio(ServiciosDTO oServicios)
        {
            ResultDTO<ServiciosDTO> oResultDTO = new ResultDTO<ServiciosDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Servicios_UpdateInsert_Precio", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idServicio", oServicios.idServicio);
                        da.SelectCommand.Parameters.AddWithValue("@Precio", oServicios.Precio);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oServicios.UsuarioModificacion);
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
                            oResultDTO.ListaResultado = new List<ServiciosDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<ServiciosDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
