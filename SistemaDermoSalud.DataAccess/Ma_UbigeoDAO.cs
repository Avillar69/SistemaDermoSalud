using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.DataAccess
{
   public class Ma_UbigeoDAO
    {
        public ResultDTO<Ma_UbigeoDTO> ListarDepartamentos()
        {
            ResultDTO<Ma_UbigeoDTO> objResultDTO = new ResultDTO<Ma_UbigeoDTO>();
            objResultDTO.ListaResultado = new List<Ma_UbigeoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("usp_Mae_Ubigeo_Departamento", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_UbigeoDTO oMa_UbigeoDTO = new Ma_UbigeoDTO();
                        oMa_UbigeoDTO.Ubigeo_ID = dr["Ubigeo_ID"] == null ? "" : dr["Ubigeo_ID"].ToString();
                        oMa_UbigeoDTO.CodigoDpto = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oMa_UbigeoDTO.Departamento = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        objResultDTO.ListaResultado.Add(oMa_UbigeoDTO);
                    }
                    objResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    objResultDTO.ListaResultado = new List<Ma_UbigeoDTO>();
                    objResultDTO.Resultado = "Error";
                    objResultDTO.MensajeError = ex.Message;
                }
            }
            return objResultDTO;
        }
        public ResultDTO<Ma_UbigeoDTO> ListarProvincias()
        {
            ResultDTO<Ma_UbigeoDTO> objResultDTO = new ResultDTO<Ma_UbigeoDTO>();
            objResultDTO.ListaResultado = new List<Ma_UbigeoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("usp_Mae_Ubigeo_Provincia", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_UbigeoDTO oMa_UbigeoDTO = new Ma_UbigeoDTO();
                        oMa_UbigeoDTO.Ubigeo_ID = dr["Ubigeo_ID"] == null ? "" : dr["Ubigeo_ID"].ToString();
                        oMa_UbigeoDTO.CodigoProv = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oMa_UbigeoDTO.Provincia = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        objResultDTO.ListaResultado.Add(oMa_UbigeoDTO);
                    }
                    objResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    objResultDTO.ListaResultado = new List<Ma_UbigeoDTO>();
                    objResultDTO.Resultado = "Error";
                    objResultDTO.MensajeError = ex.Message;
                }
            }
            return objResultDTO;
        }
        public ResultDTO<Ma_UbigeoDTO> ListarDistritos()
        {
            ResultDTO<Ma_UbigeoDTO> objResultDTO = new ResultDTO<Ma_UbigeoDTO>();
            objResultDTO.ListaResultado = new List<Ma_UbigeoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("usp_Mae_Ubigeo_Distrito", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_UbigeoDTO oMa_UbigeoDTO = new Ma_UbigeoDTO();
                        oMa_UbigeoDTO.Ubigeo_ID = dr["Ubigeo_ID"] == null ? "" : dr["Ubigeo_ID"].ToString();
                        oMa_UbigeoDTO.CodigoDist = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oMa_UbigeoDTO.Distrito = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        objResultDTO.ListaResultado.Add(oMa_UbigeoDTO);
                    }
                    objResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    objResultDTO.ListaResultado = new List<Ma_UbigeoDTO>();
                    objResultDTO.Resultado = "Error";
                    objResultDTO.MensajeError = ex.Message;
                }
            }
            return objResultDTO;
        }
    }
}
