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
    public class PersonalDAO
    {
        public ResultDTO<PersonalDTO>ListarTodo(SqlConnection cn = null)
        {
        ResultDTO<PersonalDTO> oResultDTO = new ResultDTO<PersonalDTO>();
        oResultDTO.ListaResultado = new List<PersonalDTO>();
            using((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Personal_ListarTodo",cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while(dr.Read())
                    {
                        PersonalDTO oPersonalDTO = new PersonalDTO();
                        oPersonalDTO.idPersonal = Convert.ToInt32(dr["idPersonal"] == null?0:Convert.ToInt32(dr["idPersonal"].ToString()));
                        oPersonalDTO.FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"].ToString());
                        oPersonalDTO.Nombres = dr["Nombres"]==null ? "":dr["Nombres"].ToString();
                        oPersonalDTO.ApellidoP = dr["ApellidoP"]==null ? "":dr["ApellidoP"].ToString();
                        oPersonalDTO.ApellidoM = dr["ApellidoM"]==null ? "":dr["ApellidoM"].ToString();
                        oPersonalDTO.Edad = Convert.ToInt32(dr["Edad"].ToString());
                        oPersonalDTO.Documento = dr["Documento"]==null ? "":dr["Documento"].ToString();
                        oPersonalDTO.Sexo = dr["Sexo"]==null ? "":dr["Sexo"].ToString();
                        oPersonalDTO.FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"].ToString());
                        oPersonalDTO.DescripcionCargo = dr["descripcionCargo"].ToString();
                        oPersonalDTO.NombreCompleto = dr["Nombres"].ToString() + " " + dr["ApellidoP"].ToString() + " " + dr["ApellidoM"].ToString();
                        oPersonalDTO.EstadoCivil = dr["EstadoCivil"]==null ? "":dr["EstadoCivil"].ToString();
                        oPersonalDTO.Direccion = dr["Direccion"]==null ? "":dr["Direccion"].ToString();
                        oPersonalDTO.Telefono = dr["Telefono"]==null ? "":dr["Telefono"].ToString();
                        oPersonalDTO.Movil = dr["Movil"]==null ? "":dr["Movil"].ToString();
                        oPersonalDTO.Login = dr["Login"]==null ? "":dr["Login"].ToString();
                        oPersonalDTO.PorcentajeUtilidad = Convert.ToDecimal(dr["PorcentajeUtilidad"] == null? 0 : Convert.ToDecimal(dr["PorcentajeUtilidad"].ToString()));
                        oPersonalDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oPersonalDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oPersonalDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null?0:Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oPersonalDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null?0:Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oPersonalDTO.Estado = Convert.ToBoolean(dr["Estado"] == null?false:Convert.ToBoolean(dr["Estado"].ToString()));
                        //oPersonalDTO.Planilla = Convert.ToBoolean(dr["Planilla"] == null ? false : Convert.ToBoolean(dr["Planilla"].ToString()));
                        oResultDTO.ListaResultado.Add(oPersonalDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch(Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<PersonalDTO>();
                }
            }
        return oResultDTO;
        }

       public ResultDTO<PersonalDTO>ListarxID(int idPersonal)
        {
        ResultDTO<PersonalDTO> oResultDTO = new ResultDTO<PersonalDTO>();
        oResultDTO.ListaResultado = new List<PersonalDTO>();
            using(SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Personal_ListarxID",cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idPersonal",idPersonal);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while(dr.Read())
                    {
                        PersonalDTO oPersonalDTO = new PersonalDTO();
                        oPersonalDTO.idPersonal =Convert.ToInt32(dr["idPersonal"].ToString());
                        oPersonalDTO.FechaIngreso =Convert.ToDateTime(dr["FechaIngreso"].ToString());
                        oPersonalDTO.Nombres =dr["Nombres"].ToString();
                        oPersonalDTO.ApellidoP =dr["ApellidoP"].ToString();
                        oPersonalDTO.ApellidoM =dr["ApellidoM"].ToString();
                        oPersonalDTO.Edad = Convert.ToInt32(dr["Edad"].ToString());
                        oPersonalDTO.Documento =dr["Documento"].ToString();
                        oPersonalDTO.Sexo =dr["Sexo"].ToString();
                        oPersonalDTO.FechaNacimiento =Convert.ToDateTime(dr["FechaNacimiento"].ToString());
                        oPersonalDTO.EstadoCivil =dr["EstadoCivil"].ToString();
                        oPersonalDTO.idCargo = Convert.ToInt32(dr["idCargo"] == null ? 0 : Convert.ToInt32(dr["idCargo"].ToString()));
                        oPersonalDTO.Colegiatura= dr["Colegiatura"].ToString();
                        oPersonalDTO.Direccion =dr["Direccion"].ToString();
                        oPersonalDTO.Telefono =dr["Telefono"].ToString();
                        oPersonalDTO.Movil =dr["Movil"].ToString();
                        oPersonalDTO.Login =dr["Login"].ToString();
                        oPersonalDTO.PorcentajeUtilidad =Convert.ToDecimal(dr["PorcentajeUtilidad"].ToString());
                        oPersonalDTO.FechaCreacion =Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oPersonalDTO.FechaModificacion =Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oPersonalDTO.UsuarioCreacion =Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oPersonalDTO.UsuarioModificacion =Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oPersonalDTO.Estado =Convert.ToBoolean(dr["Estado"].ToString());
                        oPersonalDTO.Img = dr["Img"].ToString();
                        oPersonalDTO.Color = dr["Color"].ToString();
                        oPersonalDTO.Planilla = Convert.ToBoolean(dr["Planilla"].ToString());
                        oResultDTO.ListaResultado.Add(oPersonalDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch(Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<PersonalDTO>();
                }
            }
        return oResultDTO;
        }

        public ResultDTO<PersonalDTO> UpdateInsert(PersonalDTO oPersonal)
        {
            ResultDTO<PersonalDTO> oResultDTO = new ResultDTO<PersonalDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Personal_UpdateInsert",cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idPersonal",oPersonal.idPersonal);
                        da.SelectCommand.Parameters.AddWithValue("@FechaIngreso", oPersonal.FechaIngreso);
                        da.SelectCommand.Parameters.AddWithValue("@Nombres",oPersonal.Nombres);
                        da.SelectCommand.Parameters.AddWithValue("@ApellidoP",oPersonal.ApellidoP);
                        da.SelectCommand.Parameters.AddWithValue("@ApellidoM",oPersonal.ApellidoM);
                        da.SelectCommand.Parameters.AddWithValue("@Edad", oPersonal.Edad);
                        da.SelectCommand.Parameters.AddWithValue("@Documento",oPersonal.Documento);
                        da.SelectCommand.Parameters.AddWithValue("@Sexo",oPersonal.Sexo);
                        da.SelectCommand.Parameters.AddWithValue("@FechaNacimiento",oPersonal.FechaNacimiento);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoCivil",oPersonal.EstadoCivil);
                        da.SelectCommand.Parameters.AddWithValue("@Direccion",oPersonal.Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@idCargo", oPersonal.idCargo);
                        da.SelectCommand.Parameters.AddWithValue("@Colegiatura", oPersonal.Colegiatura);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono",oPersonal.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@Movil",oPersonal.Movil);
                        da.SelectCommand.Parameters.AddWithValue("@Login", oPersonal.Login);
                        da.SelectCommand.Parameters.AddWithValue("@PorcentajeUtilidad",oPersonal.PorcentajeUtilidad);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion",oPersonal.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion",oPersonal.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado",oPersonal.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Img", oPersonal.Img);
                        da.SelectCommand.Parameters.AddWithValue("@Color", oPersonal.Color);
                        da.SelectCommand.Parameters.AddWithValue("@Planilla", oPersonal.Planilla);
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
                            oResultDTO.ListaResultado = new List<PersonalDTO>();
                        }
                    }
                    catch(Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<PersonalDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public  ResultDTO<PersonalDTO> Delete(PersonalDTO oPersonal)
        {
            ResultDTO<PersonalDTO> oResultDTO = new ResultDTO<PersonalDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Personal_Delete",cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idPersonal",oPersonal.idPersonal);
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
                            oResultDTO.ListaResultado = new List<PersonalDTO>();
                        }
                    }
                    catch(Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<PersonalDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        /////////////////////
        public ResultDTO<PersonalDTO> UpdatePorcentaje(PersonalDTO oPersonal)
        {
            ResultDTO<PersonalDTO> oResultDTO = new ResultDTO<PersonalDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_FN_PorcentajeMedico_Update", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idPersonal", oPersonal.idPersonal);
                        da.SelectCommand.Parameters.AddWithValue("@Porcentaje", oPersonal.PorcentajeUtilidad);
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
                            oResultDTO.ListaResultado = new List<PersonalDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<PersonalDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
