using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;


namespace SistemaDermoSalud.DataAccess.Mantenimiento
{
    public class Ma_MotivoDAO
    {
        public ResultDTO<Ma_MotivoDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<Ma_MotivoDTO> oResultDTO = new ResultDTO<Ma_MotivoDTO>();
            oResultDTO.ListaResultado = new List<Ma_MotivoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Motivo_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_MotivoDTO oMa_MotivoDTO = new Ma_MotivoDTO();
                        oMa_MotivoDTO.idMotivo = Convert.ToInt32(dr["idMotivo"] == null ? 0 : Convert.ToInt32(dr["idMotivo"].ToString()));
                        oMa_MotivoDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oMa_MotivoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_MotivoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_MotivoDTO>();
                }
            }
            return oResultDTO;
        }


        public ResultDTO<Ma_MotivoDTO> ListarTodoDebito(SqlConnection cn = null)
        {
            ResultDTO<Ma_MotivoDTO> oResultDTO = new ResultDTO<Ma_MotivoDTO>();
            oResultDTO.ListaResultado = new List<Ma_MotivoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Motivo_Debito_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_MotivoDTO oMa_MotivoDTO = new Ma_MotivoDTO();
                        oMa_MotivoDTO.idMotivo = Convert.ToInt32(dr["idMotivo_ND"] == null ? 0 : Convert.ToInt32(dr["idMotivo_ND"].ToString()));
                        oMa_MotivoDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oMa_MotivoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_MotivoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_MotivoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_MotivoDTO> ListarxID_Debito(int idMotivo)
        {
            ResultDTO<Ma_MotivoDTO> oResultDTO = new ResultDTO<Ma_MotivoDTO>();
            oResultDTO.ListaResultado = new List<Ma_MotivoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Motivo_Debito_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idMotivo", idMotivo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_MotivoDTO oMa_MotivoDTO = new Ma_MotivoDTO();
                        oMa_MotivoDTO.idMotivo = Convert.ToInt32(dr["idMotivo"] == null ? 0 : Convert.ToInt32(dr["idMotivo"].ToString()));
                        oMa_MotivoDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oMa_MotivoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_MotivoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_MotivoDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Ma_MotivoDTO> ListarxID(int idMotivo)
        {
            ResultDTO<Ma_MotivoDTO> oResultDTO = new ResultDTO<Ma_MotivoDTO>();
            oResultDTO.ListaResultado = new List<Ma_MotivoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Motivo_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idMotivo", idMotivo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_MotivoDTO oMa_MotivoDTO = new Ma_MotivoDTO();
                        oMa_MotivoDTO.idMotivo = Convert.ToInt32(dr["idMotivo"] == null ? 0 : Convert.ToInt32(dr["idMotivo"].ToString()));
                        oMa_MotivoDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oMa_MotivoDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_MotivoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_MotivoDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
