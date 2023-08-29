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
    public class PacientesDAO
    {
        public ResultDTO<PacientesDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<PacientesDTO> oResultDTO = new ResultDTO<PacientesDTO>();
            oResultDTO.ListaResultado = new List<PacientesDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Pacientes_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        PacientesDTO oPacientesDTO = new PacientesDTO();
                        oPacientesDTO.idPaciente = Convert.ToInt32(dr["idPaciente"] == null ? 0 : Convert.ToInt32(dr["idPaciente"].ToString()));
                        oPacientesDTO.NombreCompleto = dr["NombreCompleto"] == null ? "" : dr["NombreCompleto"].ToString();
                        oPacientesDTO.DNI = dr["DNI"] == null ? "" : dr["DNI"].ToString();
                        oPacientesDTO.UltimaConsulta  = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oPacientesDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oPacientesDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        
                        oResultDTO.ListaResultado.Add(oPacientesDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<PacientesDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<PacientesDTO> ListarxID(int idPaciente)
        {
            ResultDTO<PacientesDTO> oResultDTO = new ResultDTO<PacientesDTO>();
            oResultDTO.ListaResultado = new List<PacientesDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Pacientes_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idPaciente", idPaciente);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        PacientesDTO oPacientesDTO = new PacientesDTO();
                        oPacientesDTO.idPaciente = Convert.ToInt32(dr["idPaciente"].ToString());
                        oPacientesDTO.Nombres = dr["Nombres"].ToString();
                        oPacientesDTO.ApellidoP = dr["ApellidoP"].ToString();
                        oPacientesDTO.ApellidoM = dr["ApellidoM"].ToString();
                        oPacientesDTO.DNI = dr["DNI"].ToString();
                        oPacientesDTO.Edad = Convert.ToInt32(dr["Edad"].ToString());
                        oPacientesDTO.Sexo = dr["Sexo"].ToString();
                        oPacientesDTO.Direccion = dr["Direccion"].ToString();
                        oPacientesDTO.Telefono = dr["Telefono"].ToString();
                        oPacientesDTO.Movil = dr["Movil"].ToString();
                        oPacientesDTO.Email = dr["Email"].ToString();
                        oPacientesDTO.Observaciones = dr["Observaciones"].ToString();
                        oPacientesDTO.FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"].ToString());
                        oPacientesDTO.UltimaConsulta = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oPacientesDTO.Img = dr["Img"].ToString();
                        oPacientesDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oPacientesDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oPacientesDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oPacientesDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oPacientesDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oPacientesDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<PacientesDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<PacientesDTO> UpdateInsert(PacientesDTO oPacientes)
        {
            ResultDTO<PacientesDTO> oResultDTO = new ResultDTO<PacientesDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Pacientes_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idPaciente", oPacientes.idPaciente);
                        da.SelectCommand.Parameters.AddWithValue("@Nombres", oPacientes.Nombres);
                        da.SelectCommand.Parameters.AddWithValue("@ApellidoP", oPacientes.ApellidoP);
                        da.SelectCommand.Parameters.AddWithValue("@ApellidoM", oPacientes.ApellidoM);
                        da.SelectCommand.Parameters.AddWithValue("@DNI", oPacientes.DNI);
                        da.SelectCommand.Parameters.AddWithValue("@Edad", oPacientes.Edad);
                        da.SelectCommand.Parameters.AddWithValue("@Sexo", oPacientes.Sexo);
                        da.SelectCommand.Parameters.AddWithValue("@Direccion", oPacientes.Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono", oPacientes.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@Movil", oPacientes.Movil);
                        da.SelectCommand.Parameters.AddWithValue("@Email", oPacientes.Email);
                        da.SelectCommand.Parameters.AddWithValue("@Observaciones", oPacientes.Observaciones);
                        da.SelectCommand.Parameters.AddWithValue("@FechaNacimiento", oPacientes.FechaNacimiento);
                        //da.SelectCommand.Parameters.AddWithValue("@UltimaConsulta", oPacientes.UltimaConsulta);
                        da.SelectCommand.Parameters.AddWithValue("@Img", oPacientes.Img);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oPacientes.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oPacientes.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oPacientes.Estado);
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
                            oResultDTO.ListaResultado = new List<PacientesDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<PacientesDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<PacientesDTO> Delete(PacientesDTO oPacientes)
        {
            ResultDTO<PacientesDTO> oResultDTO = new ResultDTO<PacientesDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Pacientes_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idPaciente", oPacientes.idPaciente);
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
                            oResultDTO.ListaResultado = new List<PacientesDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<PacientesDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<PacientesDTO> ListarTodo_HistoriaClinica(SqlConnection cn = null)
        {
            ResultDTO<PacientesDTO> oResultDTO = new ResultDTO<PacientesDTO>();
            oResultDTO.ListaResultado = new List<PacientesDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Pacientes_ListarTodo_HistoriaClinica", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        PacientesDTO oPacientesDTO = new PacientesDTO();
                        oPacientesDTO.idPaciente = Convert.ToInt32(dr["idPaciente"] == null ? 0 : Convert.ToInt32(dr["idPaciente"].ToString()));
                        oPacientesDTO.NombreCompleto = dr["NombreCompleto"] == null ? "" : dr["NombreCompleto"].ToString();
                        oPacientesDTO.DNI = dr["Dni"] == null ? "" : dr["Dni"].ToString();
                        oPacientesDTO.FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"].ToString());
                        oPacientesDTO.Edad = Convert.ToInt32(dr["Edad"] == null ? 0 : Convert.ToInt32(dr["Edad"].ToString()));;
                        oPacientesDTO.Sexo = dr["Sexo"] == null ? "" : dr["Sexo"].ToString();
                        oResultDTO.ListaResultado.Add(oPacientesDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<PacientesDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<PacientesDTO> ListarPacientes_SinHistoria(SqlConnection cn = null)
        {
            ResultDTO<PacientesDTO> oResultDTO = new ResultDTO<PacientesDTO>();
            oResultDTO.ListaResultado = new List<PacientesDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Pacientes_ListarSinHistoria", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        PacientesDTO oPacientesDTO = new PacientesDTO();
                        oPacientesDTO.idPaciente = Convert.ToInt32(dr["idPaciente"] == null ? 0 : Convert.ToInt32(dr["idPaciente"].ToString()));
                        oPacientesDTO.NombreCompleto = dr["NombreCompleto"] == null ? "" : dr["NombreCompleto"].ToString();
                        oPacientesDTO.DNI = dr["Dni"] == null ? "" : dr["Dni"].ToString();
                        oPacientesDTO.FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"].ToString());
                        oPacientesDTO.Edad = Convert.ToInt32(dr["Edad"] == null ? 0 : Convert.ToInt32(dr["Edad"].ToString())); ;
                        oPacientesDTO.Sexo = dr["Sexo"] == null ? "" : dr["Sexo"].ToString();
                        oResultDTO.ListaResultado.Add(oPacientesDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<PacientesDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
