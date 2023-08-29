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
    public class Seg_AccesoDAO
    {
        public ResultDTO<Seg_AccesoDTO> ListarTodo()
        {
            ResultDTO<Seg_AccesoDTO> oResultDTO = new ResultDTO<Seg_AccesoDTO>();
            oResultDTO.ListaResultado = new List<Seg_AccesoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Acceso_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Seg_AccesoDTO oSeg_AccesoDTO = new Seg_AccesoDTO();
                        oSeg_AccesoDTO.idAcceso = Convert.ToInt32(dr["idAcceso"] == null ? 0 : Convert.ToInt32(dr["idAcceso"].ToString()));
                        oSeg_AccesoDTO.idMenu = Convert.ToInt32(dr["idMenu"] == null ? 0 : Convert.ToInt32(dr["idMenu"].ToString()));
                        oSeg_AccesoDTO.idRol = Convert.ToInt32(dr["idRol"] == null ? 0 : Convert.ToInt32(dr["idRol"].ToString()));
                        oResultDTO.ListaResultado.Add(oSeg_AccesoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Seg_AccesoDTO> ListarxRol(int idRol, int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<Seg_AccesoDTO> oResultDTO = new ResultDTO<Seg_AccesoDTO>();
            oResultDTO.ListaResultado = new List<Seg_AccesoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Acceso_ListarAccesoRol", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idRol", idRol);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Seg_AccesoDTO oSeg_AccesoDTO = new Seg_AccesoDTO();
                        oSeg_AccesoDTO.idAcceso = Convert.ToInt32(dr["idAcceso"].ToString());
                        oSeg_AccesoDTO.idMenu = Convert.ToInt32(dr["idMenu"].ToString());
                        oSeg_AccesoDTO.idRol = Convert.ToInt32(dr["idRol"].ToString());
                        oResultDTO.ListaResultado.Add(oSeg_AccesoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                }
            }
            return oResultDTO;
        }

        public ResultDTO<Seg_AccesoDTO> UpdateInsert(string cad, int idEmpresa, int idRol)
        {
            ResultDTO<Seg_AccesoDTO> oResultDTO = new ResultDTO<Seg_AccesoDTO>();
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

                        SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Accesos_Merge", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@idRol", idRol);
                        da.SelectCommand.Parameters.AddWithValue("@Lista", cad);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "ERROR";
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "ERROR";
                        oResultDTO.MensajeError = ex.Message;
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Seg_AccesoDTO> Delete(int idRol, int idEmpresa, SqlConnection cn = null)
        {

            ResultDTO<Seg_AccesoDTO> Respuesta = new ResultDTO<Seg_AccesoDTO>();
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(60)
            };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                using ((cn == null ? cn = new Conexion().conectar() : cn))
                {
                    try
                    {
                        if (cn.State == ConnectionState.Closed) { cn.Open(); }
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_Marca_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idRol", idRol);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta <= 1)
                        {
                            Respuesta.Resultado = "OK";
                            transactionScope.Complete();
                        }
                        else
                        {
                            Respuesta.Resultado = "ERROR";
                            Respuesta.MensajeError = "SQL Error";
                            Respuesta.ListaResultado = new List<Seg_AccesoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        Respuesta.Resultado = "ERROR";
                        Respuesta.MensajeError = ex.Message;
                        Respuesta.ListaResultado = new List<Seg_AccesoDTO>();
                    }
                }
            }
            return Respuesta;
        }
    }
}
