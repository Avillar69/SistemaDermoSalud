using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;

namespace SistemaDermoSalud.DataAccess.Mantenimiento
{
    public class Ma_TipoAfectacionDAO
    {
        public ResultDTO<Ma_TipoAfectacionDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<Ma_TipoAfectacionDTO> oResultDTO = new ResultDTO<Ma_TipoAfectacionDTO>();
            oResultDTO.ListaResultado = new List<Ma_TipoAfectacionDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoAfectacion_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_TipoAfectacionDTO oMa_TipoAfectacionDTO = new Ma_TipoAfectacionDTO();
                        oMa_TipoAfectacionDTO.idTipoAfectacion = Convert.ToInt32(dr["idTipoAfectacion"] == null ? 0 : Convert.ToInt32(dr["idTipoAfectacion"].ToString()));
                        oMa_TipoAfectacionDTO.CodigoSunat = dr["CodigoSunat"] == null ? "" : dr["CodigoSunat"].ToString();
                        oMa_TipoAfectacionDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_TipoAfectacionDTO.CodigoTributo = dr["CodigoTributo"] == null ? "" : dr["CodigoTributo"].ToString();
                        oMa_TipoAfectacionDTO.Afectacion = dr["Afectacion"] == null ? "" : dr["Afectacion"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_TipoAfectacionDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_TipoAfectacionDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Ma_TipoAfectacionDTO> ListarxID(int idTipoAfectacion)
        {
            ResultDTO<Ma_TipoAfectacionDTO> oResultDTO = new ResultDTO<Ma_TipoAfectacionDTO>();
            oResultDTO.ListaResultado = new List<Ma_TipoAfectacionDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoAfectacion_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idTipoAfectacion", idTipoAfectacion);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_TipoAfectacionDTO oMa_TipoAfectacionDTO = new Ma_TipoAfectacionDTO();
                        oMa_TipoAfectacionDTO.idTipoAfectacion = Convert.ToInt32(dr["idTipoAfectacion"] == null ? 0 : Convert.ToInt32(dr["idTipoAfectacion"].ToString()));
                        oMa_TipoAfectacionDTO.CodigoSunat = dr["CodigoSunat"] == null ? "" : dr["CodigoSunat"].ToString();
                        oMa_TipoAfectacionDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_TipoAfectacionDTO.CodigoTributo = dr["CodigoTributo"] == null ? "" : dr["CodigoTributo"].ToString();
                        oMa_TipoAfectacionDTO.Afectacion = dr["Afectacion"] == null ? "" : dr["Afectacion"].ToString();
                        oResultDTO.ListaResultado.Add(oMa_TipoAfectacionDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_TipoAfectacionDTO>();
                }
            }
            return oResultDTO;
        }

    }
}
