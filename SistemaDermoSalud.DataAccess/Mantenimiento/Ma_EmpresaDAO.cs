using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SistemaDermoSalud.DataAccess.Mantenimiento
{
    public class Ma_EmpresaDAO
    {
        public ResultDTO<Ma_EmpresaDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<Ma_EmpresaDTO> oResultDTO = new ResultDTO<Ma_EmpresaDTO>();
            oResultDTO.ListaResultado = new List<Ma_EmpresaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Empresa_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_EmpresaDTO oMa_EmpresaDTO = new Ma_EmpresaDTO();
                        oMa_EmpresaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oMa_EmpresaDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oMa_EmpresaDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oMa_EmpresaDTO.Ruc = dr["Ruc"] == null ? "" : dr["Ruc"].ToString();
                        oMa_EmpresaDTO.Direccion = dr["Direccion"] == null ? "" : dr["Direccion"].ToString();
                        oMa_EmpresaDTO.Telefono = dr["Telefono"] == null ? "" : dr["Telefono"].ToString();
                        oMa_EmpresaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_EmpresaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_EmpresaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_EmpresaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_EmpresaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMa_EmpresaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_EmpresaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_EmpresaDTO> ListarxID(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<Ma_EmpresaDTO> oResultDTO = new ResultDTO<Ma_EmpresaDTO>();
            oResultDTO.ListaResultado = new List<Ma_EmpresaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Empresa_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_EmpresaDTO oMa_EmpresaDTO = new Ma_EmpresaDTO();
                        oMa_EmpresaDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oMa_EmpresaDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oMa_EmpresaDTO.RazonSocial = dr["RazonSocial"].ToString();
                        oMa_EmpresaDTO.Ruc = dr["Ruc"].ToString();
                        oMa_EmpresaDTO.Direccion = dr["Direccion"].ToString();
                        oMa_EmpresaDTO.Telefono = dr["Telefono"].ToString();
                        oMa_EmpresaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_EmpresaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_EmpresaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oMa_EmpresaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oMa_EmpresaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oMa_EmpresaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_EmpresaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_EmpresaDTO> UpdateInsert(Ma_EmpresaDTO oMa_Empresa)
        {
            ResultDTO<Ma_EmpresaDTO> oResultDTO = new ResultDTO<Ma_EmpresaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Empresa_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oMa_Empresa.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoGenerado", oMa_Empresa.CodigoGenerado);
                        da.SelectCommand.Parameters.AddWithValue("@RazonSocial", oMa_Empresa.RazonSocial);
                        da.SelectCommand.Parameters.AddWithValue("@Ruc", oMa_Empresa.Ruc);
                        da.SelectCommand.Parameters.AddWithValue("@Direccion", oMa_Empresa.Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono", oMa_Empresa.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oMa_Empresa.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oMa_Empresa.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMa_Empresa.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarxID(oMa_Empresa.idEmpresa, cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Empresa.idEmpresa, oMa_Empresa.UsuarioModificacion,
                                "MANTENIMIENTOS-EMPRESA", "Ma_Empresa", oMa_Empresa.idEmpresa, (oMa_Empresa.idEmpresa == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_EmpresaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_EmpresaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_EmpresaDTO> Delete(Ma_EmpresaDTO oMa_Empresa)
        {
            ResultDTO<Ma_EmpresaDTO> oResultDTO = new ResultDTO<Ma_EmpresaDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Empresa_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oMa_Empresa.idEmpresa);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oMa_Empresa.idEmpresa, oMa_Empresa.UsuarioModificacion,
                                "MANTENIMIENTOS-EMPRESA", "Ma_Empresa", oMa_Empresa.idEmpresa, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<Ma_EmpresaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<Ma_EmpresaDTO>();
                    }
                }
            }
            return oResultDTO;
        }

    }
}
