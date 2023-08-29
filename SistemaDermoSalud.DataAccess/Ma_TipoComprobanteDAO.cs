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
    public class Ma_TipoComprobanteDAO
    {
        public ResultDTO<Ma_TipoComprobanteDTO> ListarTodo()
        {
            ResultDTO<Ma_TipoComprobanteDTO> oResultDTO = new ResultDTO<Ma_TipoComprobanteDTO>();
            oResultDTO.ListaResultado = new List<Ma_TipoComprobanteDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_TipoComprobante_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Ma_TipoComprobanteDTO oMa_TipoComprobanteDTO = new Ma_TipoComprobanteDTO();
                        oMa_TipoComprobanteDTO.idTipoComprobante = Convert.ToInt32(dr["idTipoComprobante"] == null ? 0 : Convert.ToInt32(dr["idTipoComprobante"].ToString()));
                        oMa_TipoComprobanteDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oMa_TipoComprobanteDTO.CodigoSunat = dr["CodigoSunat"] == null ? "" : dr["CodigoSunat"].ToString();
                        oMa_TipoComprobanteDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oMa_TipoComprobanteDTO.Abreviatura = dr["Abreviatura"] == null ? "" : dr["Abreviatura"].ToString();
                        oMa_TipoComprobanteDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oMa_TipoComprobanteDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oMa_TipoComprobanteDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oMa_TipoComprobanteDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oMa_TipoComprobanteDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oMa_TipoComprobanteDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<Ma_TipoComprobanteDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
