using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SistemaDermoSalud.DataAccess.Compras
{
   public class COM_ListaPrecioDAO
    {
        public ResultDTO<COM_ListaPrecioDTO> ListarxProveedor(int idEmpresa, int idProveedor, SqlConnection cn = null)
        {
            ResultDTO<COM_ListaPrecioDTO> oResultDTO = new ResultDTO<COM_ListaPrecioDTO>();
            oResultDTO.ListaResultado = new List<COM_ListaPrecioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_ListaPrecio_ListarxProveedor", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@idProveedor", idProveedor);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_ListaPrecioDTO oCOM_ListaPrecioDTO = new COM_ListaPrecioDTO();
                        oCOM_ListaPrecioDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oCOM_ListaPrecioDTO.idProveedor = Convert.ToInt32(dr["idProveedor"] == null ? 0 : Convert.ToInt32(dr["idProveedor"].ToString()));
                        oCOM_ListaPrecioDTO.RazonSocial = dr["ProveedorRazon"] == null ? "" : dr["ProveedorRazon"].ToString();
                        oCOM_ListaPrecioDTO.idArticulo = Convert.ToInt32(dr["idArticulo"] == null ? 0 : Convert.ToInt32(dr["idArticulo"].ToString()));
                        oCOM_ListaPrecioDTO.descripcionArticulo = dr["descripcionArticulo"] == null ? "" : dr["descripcionArticulo"].ToString();
                        oCOM_ListaPrecioDTO.idClaseArticulo = Convert.ToInt32(dr["idClaseArticulo"] == null ? 0 : Convert.ToInt32(dr["idClaseArticulo"].ToString()));
                        oCOM_ListaPrecioDTO.descripcionClaseArticulo = dr["descripcionClaseArticulo"] == null ? "" : dr["descripcionClaseArticulo"].ToString();
                        oCOM_ListaPrecioDTO.idCategoria = Convert.ToInt32(dr["idCategoria"] == null ? 0 : Convert.ToInt32(dr["idCategoria"].ToString()));
                        oCOM_ListaPrecioDTO.descripcionCategoria = dr["descripcionCategoria"] == null ? "" : dr["descripcionCategoria"].ToString();
                        oCOM_ListaPrecioDTO.idMoneda = Convert.ToInt32(dr["idMoneda"] == null ? 0 : Convert.ToInt32(dr["idMoneda"].ToString()));
                        oCOM_ListaPrecioDTO.descripcionMoneda = dr["descripcionMoneda"] == null ? "" : dr["descripcionMoneda"].ToString();
                        oCOM_ListaPrecioDTO.Valor = Convert.ToDecimal(dr["Valor"] == null ? 0 : Convert.ToDecimal(dr["Valor"].ToString()));
                        oCOM_ListaPrecioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oCOM_ListaPrecioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oCOM_ListaPrecioDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oCOM_ListaPrecioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_ListaPrecioDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_ListaPrecioDTO> UpdateInsert(COM_ListaPrecioDTO oCOM_ListaPrecioDTO)
        {
            ResultDTO<COM_ListaPrecioDTO> oResultDTO = new ResultDTO<COM_ListaPrecioDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_COM_ListaPrecio_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idDetalleListaPrecio", oCOM_ListaPrecioDTO.idDetalleListaPrecio);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oCOM_ListaPrecioDTO.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@idProveedor", oCOM_ListaPrecioDTO.idProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@idArticulo", oCOM_ListaPrecioDTO.idArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@idMoneda", oCOM_ListaPrecioDTO.idMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@Valor", oCOM_ListaPrecioDTO.Valor);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oCOM_ListaPrecioDTO.UsuarioCreacion);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarxProveedor(oCOM_ListaPrecioDTO.idEmpresa, oCOM_ListaPrecioDTO.idProveedor, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<COM_ListaPrecioDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<COM_ListaPrecioDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<COM_ListaPrecioDTO> ListarxIdMaterial(int idEmpresa, int idProveedor, int idArticulo, int idMoneda, SqlConnection cn = null)
        {
            ResultDTO<COM_ListaPrecioDTO> oResultDTO = new ResultDTO<COM_ListaPrecioDTO>();
            oResultDTO.ListaResultado = new List<COM_ListaPrecioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_COM_ListaPrecio_ListarxMaterial", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@idProveedor", idProveedor);
                    da.SelectCommand.Parameters.AddWithValue("@idArticulo", idArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@idMoneda", idMoneda);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        COM_ListaPrecioDTO oCOM_ListaPrecioDTO = new COM_ListaPrecioDTO();
                        oCOM_ListaPrecioDTO.descripcionArticulo = dr["descripcionArticulo"] == null ? "" : dr["descripcionArticulo"].ToString();
                        oCOM_ListaPrecioDTO.descripcionMoneda = dr["descripcionMoneda"] == null ? "" : dr["descripcionMoneda"].ToString();
                        oCOM_ListaPrecioDTO.Valor = Convert.ToDecimal(dr["Valor"] == null ? 0 : Convert.ToDecimal(dr["Valor"].ToString()));
                        oCOM_ListaPrecioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oResultDTO.ListaResultado.Add(oCOM_ListaPrecioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<COM_ListaPrecioDTO>();
                }
            }
            return oResultDTO;
        }


    }
}
