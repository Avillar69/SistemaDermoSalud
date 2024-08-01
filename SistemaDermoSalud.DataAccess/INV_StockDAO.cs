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
   public class INV_StockDAO
    {
        public ResultDTO<INV_StockDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<INV_StockDTO> oResultDTO = new ResultDTO<INV_StockDTO>();
            oResultDTO.ListaResultado = new List<INV_StockDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_INV_Stock_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        INV_StockDTO oINV_StockDTO = new INV_StockDTO();
                        oINV_StockDTO.idStock = Convert.ToInt32(dr["idStock"] == null ? 0 : Convert.ToInt32(dr["idStock"].ToString()));
                        oINV_StockDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oINV_StockDTO.idArticulo = Convert.ToInt32(dr["idArticulo"] == null ? 0 : Convert.ToInt32(dr["idArticulo"].ToString()));
                        oINV_StockDTO.idAlmacen = Convert.ToInt32(dr["idAlmacen"] == null ? 0 : Convert.ToInt32(dr["idAlmacen"].ToString()));
                        oINV_StockDTO.Stock = Convert.ToDecimal(dr["Stock"] == null ? 0 : Convert.ToDecimal(dr["Stock"].ToString()));
                        oINV_StockDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oINV_StockDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oINV_StockDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oINV_StockDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oINV_StockDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oINV_StockDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<INV_StockDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<INV_StockDTO> ListarStockxAlmacen(int idEmpresa, int idAlmacen, int idUsuario)
        {
            ResultDTO<INV_StockDTO> oResultDTO = new ResultDTO<INV_StockDTO>();
            oResultDTO.ListaResultado = new List<INV_StockDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_INV_Stock_ListarxAlmacen", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //da.SelectCommand.Parameters.AddWithValue("@idAlmacen", idAlmacen);
                    //da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        INV_StockDTO oINV_StockDTO = new INV_StockDTO();
                        oINV_StockDTO.idStock = Convert.ToInt32(dr["idStock"] == null ? 0 : Convert.ToInt32(dr["idStock"].ToString()));
                        oINV_StockDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oINV_StockDTO.idAlmacen = Convert.ToInt32(dr["idAlmacen"] == null ? 0 : Convert.ToInt32(dr["idAlmacen"].ToString()));
                        oINV_StockDTO.idArticulo = Convert.ToInt32(dr["idArticulo"] == null ? 0 : Convert.ToInt32(dr["idArticulo"].ToString()));
                        oINV_StockDTO.descCategoria = dr["descCategoria"] == null ? "" : dr["descCategoria"].ToString();
                        oINV_StockDTO.CodArticulo = dr["CodArticulo"] == null ? "" : dr["CodArticulo"].ToString();
                        oINV_StockDTO.descArticulo = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oINV_StockDTO.Stock = Convert.ToDecimal(dr["Stock"] == null ? 0 : Convert.ToDecimal(dr["Stock"].ToString()));
                        oResultDTO.ListaResultado.Add(oINV_StockDTO);
                    }
                    oResultDTO.Resultado = "OK";
                    new Seg_LogDAO().UpdateInsert(da, cn, idEmpresa, idUsuario, "INVENTARIO-CONSULTA_STOCK", "INV_Stock",
                        0, "SELECT", "SISTEMA", "SP_INV_Stock_ListarxAlmacen " + idAlmacen.ToString() + "," + idEmpresa.ToString());
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<INV_StockDTO>();
                }
            }
            return oResultDTO;
        }
        public string ItemStock(int idEmpresa, int idProducto, int idAlmacenO, int idAlmacenD)
        {
            string rpta = "";
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_INV_Stock_Get_ItemStock", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@idProducto", idProducto);
                    da.SelectCommand.Parameters.AddWithValue("@idAlmacenO", idAlmacenO);
                    da.SelectCommand.Parameters.AddWithValue("@idAlmacenD", idAlmacenD);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        rpta += dr["StockO"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    rpta = "Error";
                }
            }
            return rpta;
        }
        public ResultDTO<INV_StockDTO> ListarxID(int idStock)
        {
            ResultDTO<INV_StockDTO> oResultDTO = new ResultDTO<INV_StockDTO>();
            oResultDTO.ListaResultado = new List<INV_StockDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_INV_Stock_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idStock", idStock);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        INV_StockDTO oINV_StockDTO = new INV_StockDTO();
                        oINV_StockDTO.idStock = Convert.ToInt32(dr["idStock"].ToString());
                        oINV_StockDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oINV_StockDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                        oINV_StockDTO.idAlmacen = Convert.ToInt32(dr["idAlmacen"].ToString());
                        oINV_StockDTO.Stock = Convert.ToDecimal(dr["Stock"].ToString());
                        oINV_StockDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oINV_StockDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oINV_StockDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oINV_StockDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oINV_StockDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oINV_StockDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<INV_StockDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<INV_StockDTO> UpdateInsert(INV_StockDTO oINV_Stock)
        {
            ResultDTO<INV_StockDTO> oResultDTO = new ResultDTO<INV_StockDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_INV_Stock_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idStock", oINV_Stock.idStock);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oINV_Stock.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@idArticulo", oINV_Stock.idArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@idAlmacen", oINV_Stock.idAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@Stock", oINV_Stock.Stock);
                        da.SelectCommand.Parameters.AddWithValue("@FechaCreacion", oINV_Stock.FechaCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaModificacion", oINV_Stock.FechaModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oINV_Stock.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oINV_Stock.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oINV_Stock.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oINV_Stock.idEmpresa, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<INV_StockDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<INV_StockDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<INV_StockDTO> Delete(INV_StockDTO oINV_Stock)
        {
            ResultDTO<INV_StockDTO> oResultDTO = new ResultDTO<INV_StockDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_INV_Stock_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idStock", oINV_Stock.idStock);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oINV_Stock.idEmpresa, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<INV_StockDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<INV_StockDTO>();
                    }
                }
            }
            return oResultDTO;
        }
    }
}
