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
    public class RH_EmpleadoDAO
    {
        public ResultDTO<RH_EmpleadoDTO> ListarTodo( SqlConnection cn = null)
        {
            ResultDTO<RH_EmpleadoDTO> oResultDTO = new ResultDTO<RH_EmpleadoDTO>();
            oResultDTO.ListaResultado = new List<RH_EmpleadoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_RH_Empleado_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        RH_EmpleadoDTO oRH_EmpleadoDTO = new RH_EmpleadoDTO();
                        oRH_EmpleadoDTO.idEmpleado = Convert.ToInt32(dr["idEmpleado"] == null ? 0 : Convert.ToInt32(dr["idEmpleado"].ToString()));
                        oRH_EmpleadoDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oRH_EmpleadoDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oRH_EmpleadoDTO.ApellidoPaterno = dr["ApellidoPaterno"] == null ? "" : dr["ApellidoPaterno"].ToString();
                        oRH_EmpleadoDTO.ApellidoMaterno = dr["ApellidoMaterno"] == null ? "" : dr["ApellidoMaterno"].ToString();
                        oRH_EmpleadoDTO.Nombres = dr["Nombres"] == null ? "" : dr["Nombres"].ToString();
                        oRH_EmpleadoDTO.NombreCompleto = dr["NombreCompleto"] == null ? "" : dr["NombreCompleto"].ToString();
                        oRH_EmpleadoDTO.FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"].ToString());
                        oRH_EmpleadoDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"] == null ? 0 : Convert.ToInt32(dr["idTipoDocumento"].ToString()));
                        oRH_EmpleadoDTO.Documento = dr["Documento"] == null ? "" : dr["Documento"].ToString();
                        oRH_EmpleadoDTO.Sexo = dr["Sexo"] == null ? "" : dr["Sexo"].ToString();
                        oRH_EmpleadoDTO.idPais = Convert.ToInt32(dr["idPais"] == null ? 0 : Convert.ToInt32(dr["idPais"].ToString()));
                        oRH_EmpleadoDTO.idDepartamento = Convert.ToInt32(dr["idDepartamento"] == null ? 0 : Convert.ToInt32(dr["idDepartamento"].ToString()));
                        oRH_EmpleadoDTO.idProvincia = Convert.ToInt32(dr["idProvincia"] == null ? 0 : Convert.ToInt32(dr["idProvincia"].ToString()));
                        oRH_EmpleadoDTO.idDistrito = Convert.ToInt32(dr["idDistrito"] == null ? 0 : Convert.ToInt32(dr["idDistrito"].ToString()));
                        oRH_EmpleadoDTO.Direccion = dr["Direccion"] == null ? "" : dr["Direccion"].ToString();
                        oRH_EmpleadoDTO.NroEssalud = Convert.ToInt32(dr["NroEssalud"] == null ? 0 : Convert.ToInt32(dr["NroEssalud"].ToString()));
                        oRH_EmpleadoDTO.NroAFP = Convert.ToInt32(dr["NroAFP"] == null ? 0 : Convert.ToInt32(dr["NroAFP"].ToString()));
                        oRH_EmpleadoDTO.NroRUC = dr["NroRUC"] == null ? "" : dr["NroRUC"].ToString();
                        oRH_EmpleadoDTO.Telefono = Convert.ToInt32(dr["Telefono"] == null ? 0 : Convert.ToInt32(dr["Telefono"].ToString()));
                        oRH_EmpleadoDTO.Email = dr["Email"] == null ? "" : dr["Email"].ToString();
                        oRH_EmpleadoDTO.EstadoCivil = dr["EstadoCivil"] == null ? "" : dr["EstadoCivil"].ToString();
                        oRH_EmpleadoDTO.NroHijos = Convert.ToInt32(dr["NroHijos"] == null ? 0 : Convert.ToInt32(dr["NroHijos"].ToString()));
                        oRH_EmpleadoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oRH_EmpleadoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oRH_EmpleadoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oRH_EmpleadoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oRH_EmpleadoDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oRH_EmpleadoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<RH_EmpleadoDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<RH_EmpleadoDTO> ListarxID(int idEmpleado)
        {
            ResultDTO<RH_EmpleadoDTO> oResultDTO = new ResultDTO<RH_EmpleadoDTO>();
            oResultDTO.ListaResultado = new List<RH_EmpleadoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_RH_Empleado_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        RH_EmpleadoDTO oRH_EmpleadoDTO = new RH_EmpleadoDTO();
                        oRH_EmpleadoDTO.idEmpleado = Convert.ToInt32(dr["idEmpleado"].ToString());
                        oRH_EmpleadoDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oRH_EmpleadoDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oRH_EmpleadoDTO.ApellidoPaterno = dr["ApellidoPaterno"].ToString();
                        oRH_EmpleadoDTO.ApellidoMaterno = dr["ApellidoMaterno"].ToString();
                        oRH_EmpleadoDTO.Nombres = dr["Nombres"].ToString();
                        oRH_EmpleadoDTO.NombreCompleto = dr["NombreCompleto"].ToString();
                        oRH_EmpleadoDTO.FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"].ToString());
                        oRH_EmpleadoDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"].ToString());
                        oRH_EmpleadoDTO.Documento = dr["Documento"].ToString();
                        oRH_EmpleadoDTO.Sexo = dr["Sexo"].ToString();
                        oRH_EmpleadoDTO.idPais = Convert.ToInt32(dr["idPais"].ToString());
                        oRH_EmpleadoDTO.idDepartamento = Convert.ToInt32(dr["idDepartamento"].ToString());
                        oRH_EmpleadoDTO.idProvincia = Convert.ToInt32(dr["idProvincia"].ToString());
                        oRH_EmpleadoDTO.idDistrito = Convert.ToInt32(dr["idDistrito"].ToString());
                        oRH_EmpleadoDTO.Direccion = dr["Direccion"].ToString();
                        oRH_EmpleadoDTO.NroEssalud = Convert.ToInt32(dr["NroEssalud"].ToString());
                        oRH_EmpleadoDTO.NroAFP = Convert.ToInt32(dr["NroAFP"].ToString());
                        oRH_EmpleadoDTO.NroRUC = dr["NroRUC"].ToString();
                        oRH_EmpleadoDTO.Telefono = Convert.ToInt32(dr["Telefono"].ToString());
                        oRH_EmpleadoDTO.Email = dr["Email"].ToString();
                        oRH_EmpleadoDTO.EstadoCivil = dr["EstadoCivil"].ToString();
                        oRH_EmpleadoDTO.NroHijos = Convert.ToInt32(dr["NroHijos"].ToString());
                        oRH_EmpleadoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oRH_EmpleadoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oRH_EmpleadoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oRH_EmpleadoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oRH_EmpleadoDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oRH_EmpleadoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<RH_EmpleadoDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<RH_EmpleadoDTO> UpdateInsert(RH_EmpleadoDTO oRH_Empleado, int idUsuario)
        {
            ResultDTO<RH_EmpleadoDTO> oResultDTO = new ResultDTO<RH_EmpleadoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_RH_Empleado_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idEmpleado", oRH_Empleado.idEmpleado);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oRH_Empleado.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@ApellidoPaterno", oRH_Empleado.ApellidoPaterno);
                        da.SelectCommand.Parameters.AddWithValue("@ApellidoMaterno", oRH_Empleado.ApellidoMaterno);
                        da.SelectCommand.Parameters.AddWithValue("@Nombres", oRH_Empleado.Nombres);
                        da.SelectCommand.Parameters.AddWithValue("@NombreCompleto", oRH_Empleado.NombreCompleto);
                        da.SelectCommand.Parameters.AddWithValue("@FechaNacimiento", oRH_Empleado.FechaNacimiento);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoDocumento", oRH_Empleado.idTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@Documento", oRH_Empleado.Documento);
                        da.SelectCommand.Parameters.AddWithValue("@Sexo", oRH_Empleado.Sexo);
                        da.SelectCommand.Parameters.AddWithValue("@idPais", oRH_Empleado.idPais);
                        da.SelectCommand.Parameters.AddWithValue("@idDepartamento", oRH_Empleado.idDepartamento);
                        da.SelectCommand.Parameters.AddWithValue("@idProvincia", oRH_Empleado.idProvincia);
                        da.SelectCommand.Parameters.AddWithValue("@idDistrito", oRH_Empleado.idDistrito);
                        da.SelectCommand.Parameters.AddWithValue("@Direccion", oRH_Empleado.Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@NroEssalud", oRH_Empleado.NroEssalud);
                        da.SelectCommand.Parameters.AddWithValue("@NroAFP", oRH_Empleado.NroAFP);
                        da.SelectCommand.Parameters.AddWithValue("@NroRUC", oRH_Empleado.NroRUC);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono", oRH_Empleado.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@Email", oRH_Empleado.Email);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoCivil", oRH_Empleado.EstadoCivil);
                        da.SelectCommand.Parameters.AddWithValue("@NroHijos", oRH_Empleado.NroHijos);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oRH_Empleado.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oRH_Empleado.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oRH_Empleado.Estado);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo( cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oRH_Empleado.idEmpresa, idUsuario,
                                "RRHH-EMPLEADO", "RH_Empleado", (int)id_output.Value, (oRH_Empleado.idEmpleado == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<RH_EmpleadoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<RH_EmpleadoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<RH_EmpleadoDTO> Delete(RH_EmpleadoDTO oRH_Empleado, int idUsuario)
        {
            ResultDTO<RH_EmpleadoDTO> oResultDTO = new ResultDTO<RH_EmpleadoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_RH_Empleado_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idEmpleado", oRH_Empleado.idEmpleado);
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
                            oResultDTO.ListaResultado = new List<RH_EmpleadoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<RH_EmpleadoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
       


    }
}
