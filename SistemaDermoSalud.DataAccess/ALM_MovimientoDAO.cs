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
   public class ALM_MovimientoDAO
    {
        public ResultDTO<ALM_MovimientoDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<ALM_MovimientoDTO> oResultDTO = new ResultDTO<ALM_MovimientoDTO>();
            oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_ALM_Movimiento_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        ALM_MovimientoDTO oALM_MovimientoDTO = new ALM_MovimientoDTO();
                        oALM_MovimientoDTO.idMovimiento = Convert.ToInt32(dr["idMovimiento"] == null ? 0 : Convert.ToInt32(dr["idMovimiento"].ToString()));
                        oALM_MovimientoDTO.idLocal = Convert.ToInt32(dr["idLocal"] == null ? 0 : Convert.ToInt32(dr["idLocal"].ToString()));
                        oALM_MovimientoDTO.idTipoMovimiento = Convert.ToInt32(dr["idTipoMovimiento"] == null ? 0 : Convert.ToInt32(dr["idTipoMovimiento"].ToString()));
                        oALM_MovimientoDTO.Observaciones = dr["Observaciones"] == null ? "" : dr["Observaciones"].ToString();
                        oALM_MovimientoDTO.idGuiaRemision = Convert.ToInt32(dr["idGuiaRemision"] == null ? 0 : Convert.ToInt32(dr["idGuiaRemision"].ToString()));
                        oALM_MovimientoDTO.idDocumento = Convert.ToInt32(dr["idDocumento"] == null ? 0 : Convert.ToInt32(dr["idDocumento"].ToString()));
                        oALM_MovimientoDTO.idEstado = Convert.ToInt32(dr["idEstado"] == null ? 0 : Convert.ToInt32(dr["idEstado"].ToString()));
                        oALM_MovimientoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oALM_MovimientoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oALM_MovimientoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oALM_MovimientoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oALM_MovimientoDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oALM_MovimientoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<ALM_MovimientoDTO> ListarTodoTransformaciones(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<ALM_MovimientoDTO> oResultDTO = new ResultDTO<ALM_MovimientoDTO>();
            oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_ALM_Movimiento_Listarx_Transformacion", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        ALM_MovimientoDTO oALM_MovimientoDTO = new ALM_MovimientoDTO();
                        oALM_MovimientoDTO.idMovimiento = Convert.ToInt32(dr["idMovimiento"] == null ? 0 : Convert.ToInt32(dr["idMovimiento"].ToString()));
                        oALM_MovimientoDTO.DesLocal = dr["DesLocal"].ToString();
                        oALM_MovimientoDTO.DesAlmacenOrigen = dr["DesAlmacenOrigen"].ToString();
                        oALM_MovimientoDTO.DesAlmacenDestino = dr["DesAlmacenDestino"].ToString();
                        oALM_MovimientoDTO.Observaciones = dr["Observaciones"].ToString();
                        oALM_MovimientoDTO.DesEstado = dr["DesEstado"].ToString();
                        oResultDTO.ListaResultado.Add(oALM_MovimientoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<ALM_MovimientoDTO> ListarRangoFecha(string TipoMovimiento, int idEmpresa, DateTime fechaInicio, DateTime fechaFin, SqlConnection cn = null)
        {
            ResultDTO<ALM_MovimientoDTO> oResultDTO = new ResultDTO<ALM_MovimientoDTO>();
            oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_ALM_Movimiento_ListarxFecha", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@TipoMovimiento", TipoMovimiento);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        ALM_MovimientoDTO oALM_MovimientoDTO = new ALM_MovimientoDTO();
                        oALM_MovimientoDTO.idMovimiento = Convert.ToInt32(dr["idMovimiento"] == null ? 0 : Convert.ToInt32(dr["idMovimiento"].ToString()));
                        oALM_MovimientoDTO.DesLocal = dr["DesLocal"].ToString();
                        oALM_MovimientoDTO.Observaciones = dr["Observaciones"].ToString();
                        oALM_MovimientoDTO.DesEstado = dr["DesEstado"].ToString();
                        oALM_MovimientoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());


                        oResultDTO.ListaResultado.Add(oALM_MovimientoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                }
            }
            return oResultDTO;
        }
        public List<Object> ListarTransferenciaSalida(int idEmpresa, int idLocal, int idAlmacen)
        {
            List<Object> oLista = new List<object>();
            ResultDTO<ALM_MovimientoDTO> oResultDTO = new ResultDTO<ALM_MovimientoDTO>();
            ResultDTO<ALM_MovimientoDetalleDTO> oResultDTO_Detalle = new ResultDTO<ALM_MovimientoDetalleDTO>();
            oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
            oResultDTO_Detalle.ListaResultado = new List<ALM_MovimientoDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_ALM_Movimiento_ListarTransferenciaSalida", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@idAlmacen", idAlmacen);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        ALM_MovimientoDTO oALM_MovimientoDTO = new ALM_MovimientoDTO();
                        oALM_MovimientoDTO.idMovimiento = Convert.ToInt32(dr["idMovimiento"].ToString());
                        oALM_MovimientoDTO.DesAlmacenOrigen = dr["DesAlmacenOrigen"].ToString();
                        oALM_MovimientoDTO.Observaciones = dr["Observaciones"].ToString();
                        oALM_MovimientoDTO.idEstado = Convert.ToInt32(dr["idEstado"].ToString());
                        oALM_MovimientoDTO.DesEstado = dr["DesEstado"].ToString();
                        oResultDTO.ListaResultado.Add(oALM_MovimientoDTO);
                    }
                    oLista.Add(oResultDTO);

                    if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                ALM_MovimientoDetalleDTO obj = new ALM_MovimientoDetalleDTO();
                                obj.idMovimientoDetalle = Convert.ToInt32(dr["idMovimientoDetalle"].ToString());
                                obj.idMovimiento = Convert.ToInt32(dr["idMovimiento"].ToString());
                                obj.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                                obj.DesArticulo = dr["DesArticulo"].ToString();
                                obj.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                                oResultDTO_Detalle.ListaResultado.Add(obj);
                            }
                        }
                    }
                    oLista.Add(oResultDTO_Detalle);
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();

                    oResultDTO_Detalle.Resultado = "Error";
                    oResultDTO_Detalle.MensajeError = ex.Message;
                    oResultDTO_Detalle.ListaResultado = new List<ALM_MovimientoDetalleDTO>();
                }
            }
            return oLista;
        }

        public ResultDTO<ALM_MovimientoDTO> ListarxID(int idMovimiento)
        {
            ResultDTO<ALM_MovimientoDTO> oResultDTO = new ResultDTO<ALM_MovimientoDTO>();
            oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_ALM_Movimiento_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idMovimiento", idMovimiento);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        ALM_MovimientoDTO oALM_MovimientoDTO = new ALM_MovimientoDTO();
                        oALM_MovimientoDTO.idMovimiento = Convert.ToInt32(dr["idMovimiento"].ToString());
                        oALM_MovimientoDTO.idLocal = Convert.ToInt32(dr["idLocal"].ToString());
                        oALM_MovimientoDTO.idTipoMovimiento = Convert.ToInt32(dr["idTipoMovimiento"].ToString());
                        oALM_MovimientoDTO.Observaciones = dr["Observaciones"].ToString();
                        oALM_MovimientoDTO.idGuiaRemision = Convert.ToInt32(dr["idGuiaRemision"].ToString());
                        oALM_MovimientoDTO.idDocumento = Convert.ToInt32(dr["idDocumento"].ToString());
                        oALM_MovimientoDTO.idEstado = Convert.ToInt32(dr["idEstado"].ToString());
                        oALM_MovimientoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oALM_MovimientoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oALM_MovimientoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oALM_MovimientoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oALM_MovimientoDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());                        
                        oResultDTO.ListaResultado.Add(oALM_MovimientoDTO);
                    }
                    if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<ALM_MovimientoDetalleDTO> lista = new List<ALM_MovimientoDetalleDTO>();
                            while (dr.Read())
                            {
                                ALM_MovimientoDetalleDTO obj = new ALM_MovimientoDetalleDTO();
                                obj.idMovimientoDetalle = Convert.ToInt32(dr["idMovimientoDetalle"].ToString());
                                obj.idArticulo = Convert.ToInt32(dr["idMedicamentos"].ToString());
                                obj.DesArticulo = dr["DesArticulo"].ToString();
                                obj.Laboratorio = dr["Laboratorio"].ToString();
                                obj.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                                obj.Precio = Convert.ToDecimal(dr["Precio"].ToString());
                                lista.Add(obj);
                            }
                            oResultDTO.ListaResultado[0].oListaDetalle = lista;
                        }
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                ALM_MovimientoDTO oALM_MovimientoDTO = new ALM_MovimientoDTO();
                                oALM_MovimientoDTO.idMovimiento = Convert.ToInt32(dr["idMovimiento"].ToString());
                                oALM_MovimientoDTO.Observaciones = dr["Observaciones"].ToString();
                                oALM_MovimientoDTO.idEstado = Convert.ToInt32(dr["idEstado"].ToString());
                                oALM_MovimientoDTO.DesEstado = dr["DesEstado"].ToString();
                                oResultDTO.ListaResultado.Add(oALM_MovimientoDTO);
                            }
                        }
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<ALM_MovimientoDTO> UpdateInsert(ALM_MovimientoDTO oALM_Movimiento)
        {
            ResultDTO<ALM_MovimientoDTO> oResultDTO = new ResultDTO<ALM_MovimientoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_ALM_Movimiento_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMovimiento ", oALM_Movimiento.idMovimiento);
                        da.SelectCommand.Parameters.AddWithValue("@idLocal", oALM_Movimiento.idLocal);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoMovimiento", oALM_Movimiento.idTipoMovimiento);
                        da.SelectCommand.Parameters.AddWithValue("@Observaciones", oALM_Movimiento.Observaciones);
                        da.SelectCommand.Parameters.AddWithValue("@idGuiaRemision", oALM_Movimiento.idGuiaRemision);
                        da.SelectCommand.Parameters.AddWithValue("@idDocumento", oALM_Movimiento.idDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@idEstado", oALM_Movimiento.idEstado);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oALM_Movimiento.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oALM_Movimiento.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oALM_Movimiento.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Lista_Articulo", oALM_Movimiento.Lista_Articulo);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarxTipo(oALM_Movimiento.TipoMovimiento,1, cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, 1, oALM_Movimiento.UsuarioModificacion,
                               "INVENTARIO-OPERACIONES_STOCK", "ALM_Movimiento", (int)id_output.Value, (oALM_Movimiento.idMovimiento == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<ALM_MovimientoDTO> UpdateInsertT(ALM_MovimientoDTO oALM_Movimiento)
        {
            ResultDTO<ALM_MovimientoDTO> oResultDTO = new ResultDTO<ALM_MovimientoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_ALM_Movimiento_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMovimiento ", oALM_Movimiento.idMovimiento);
                        da.SelectCommand.Parameters.AddWithValue("@idLocal", oALM_Movimiento.idLocal);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoMovimiento", oALM_Movimiento.idTipoMovimiento);
                        da.SelectCommand.Parameters.AddWithValue("@Observaciones", oALM_Movimiento.Observaciones);
                        da.SelectCommand.Parameters.AddWithValue("@idGuiaRemision", oALM_Movimiento.idGuiaRemision);
                        da.SelectCommand.Parameters.AddWithValue("@idDocumento", oALM_Movimiento.idDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@idEstado", oALM_Movimiento.idEstado);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oALM_Movimiento.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oALM_Movimiento.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oALM_Movimiento.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Lista_Articulo", oALM_Movimiento.Lista_Articulo);
                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            int tipMovimiento = oALM_Movimiento.idTipoMovimiento;
                            if (tipMovimiento == 11)
                            {
                                SqlDataAdapter dT = new SqlDataAdapter("SP_ALM_Movimiento_UpdateInsert", cn);
                                dT.SelectCommand.CommandType = CommandType.StoredProcedure;
                                dT.SelectCommand.Parameters.AddWithValue("@idMovimiento ", oALM_Movimiento.idMovimiento);
                                dT.SelectCommand.Parameters.AddWithValue("@idLocal", oALM_Movimiento.idLocal);
                                dT.SelectCommand.Parameters.AddWithValue("@idMovimientoRef", id_output.Value);
                                dT.SelectCommand.Parameters.AddWithValue("@idTipoMovimiento", 12);
                                dT.SelectCommand.Parameters.AddWithValue("@Observaciones", oALM_Movimiento.Observaciones);
                                dT.SelectCommand.Parameters.AddWithValue("@idGuiaRemision", oALM_Movimiento.idGuiaRemision);
                                dT.SelectCommand.Parameters.AddWithValue("@idDocumento", oALM_Movimiento.idDocumento);
                                dT.SelectCommand.Parameters.AddWithValue("@idEstado", oALM_Movimiento.idEstado);
                                dT.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oALM_Movimiento.UsuarioCreacion);
                                dT.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oALM_Movimiento.UsuarioModificacion);
                                dT.SelectCommand.Parameters.AddWithValue("@Estado", oALM_Movimiento.Estado);
                                dT.SelectCommand.Parameters.AddWithValue("@Lista_Articulo", oALM_Movimiento.Lista_Articulo2);
                                SqlParameter id_output2 = dT.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                                id_output2.Direction = ParameterDirection.Output;
                                int rpta2 = dT.SelectCommand.ExecuteNonQuery();
                            }
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodoTransformaciones(1, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                    }
                }
            }
            return oResultDTO;
        }

        public ResultDTO<ALM_MovimientoDTO> ListarxIdTransformacion(int idMovimiento)
        {
            ResultDTO<ALM_MovimientoDTO> oResultDTO = new ResultDTO<ALM_MovimientoDTO>();
            oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_ALM_Movimiento_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idMovimiento", idMovimiento);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    int idMovRef = 0;
                    while (dr.Read())
                    {
                        ALM_MovimientoDTO oALM_MovimientoDTO = new ALM_MovimientoDTO();
                        oALM_MovimientoDTO.idMovimiento = Convert.ToInt32(dr["idMovimiento"].ToString());
                        oALM_MovimientoDTO.idLocal = Convert.ToInt32(dr["idLocal"].ToString());
                        oALM_MovimientoDTO.idTipoMovimiento = Convert.ToInt32(dr["idTipoMovimiento"].ToString());
                        oALM_MovimientoDTO.Observaciones = dr["Observaciones"].ToString();
                        oALM_MovimientoDTO.idGuiaRemision = Convert.ToInt32(dr["idGuiaRemision"].ToString());
                        oALM_MovimientoDTO.idDocumento = Convert.ToInt32(dr["idDocumento"].ToString());
                        oALM_MovimientoDTO.idEstado = Convert.ToInt32(dr["idEstado"].ToString());
                        oALM_MovimientoDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oALM_MovimientoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oALM_MovimientoDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oALM_MovimientoDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oALM_MovimientoDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oALM_MovimientoDTO);
                    }
                    if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<ALM_MovimientoDetalleDTO> lista = new List<ALM_MovimientoDetalleDTO>();
                            while (dr.Read())
                            {
                                ALM_MovimientoDetalleDTO obj = new ALM_MovimientoDetalleDTO();
                                obj.idMovimientoDetalle = Convert.ToInt32(dr["idMovimientoDetalle"].ToString());
                                obj.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                                obj.DesArticulo = dr["DesArticulo"].ToString();
                                obj.Cantidad = Convert.ToDecimal(dr["Cantidad"].ToString());
                                lista.Add(obj);
                            }
                            oResultDTO.ListaResultado[0].oListaDetalle = lista;
                        }
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                ALM_MovimientoDTO oALM_MovimientoDTO = new ALM_MovimientoDTO();
                                oALM_MovimientoDTO.idMovimiento = Convert.ToInt32(dr["idMovimiento"].ToString());
                                oALM_MovimientoDTO.Observaciones = dr["Observaciones"].ToString();
                                oALM_MovimientoDTO.DesAlmacenOrigen = dr["DesAlmacenOrigen"].ToString();
                                oALM_MovimientoDTO.idEstado = Convert.ToInt32(dr["idEstado"].ToString());
                                oALM_MovimientoDTO.DesEstado = dr["DesEstado"].ToString();
                                oResultDTO.ListaResultado.Add(oALM_MovimientoDTO);
                            }
                        }





                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                }
            }
            return oResultDTO;
        }


        public ResultDTO<ALM_MovimientoDTO> Delete(ALM_MovimientoDTO oALM_Movimiento)
        {
            ResultDTO<ALM_MovimientoDTO> oResultDTO = new ResultDTO<ALM_MovimientoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_ALM_Movimiento_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMovimiento", oALM_Movimiento.idMovimiento);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarxTipo(oALM_Movimiento.TipoMovimiento,1, cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<ALM_MovimientoDTO> DeleteTrans(int id, int idMovimientoReferencia)
        {
            ResultDTO<ALM_MovimientoDTO> oResultDTO = new ResultDTO<ALM_MovimientoDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_ALM_Movimiento_DeleteTransformacion", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMovimiento", id);
                        da.SelectCommand.Parameters.AddWithValue("@idMovimientoReferencia", idMovimientoReferencia);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta >= 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodoTransformaciones(1, cn).ListaResultado;

                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                    }
                }
            }
            return oResultDTO;
        }



        public ResultDTO<ALM_MovimientoDTO> ListarxTipo(string TipoMovimiento, int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<ALM_MovimientoDTO> oResultDTO = new ResultDTO<ALM_MovimientoDTO>();
            oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_ALM_Movimiento_Listarx_TipoMovimiento", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@TipoMovimiento", TipoMovimiento);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        ALM_MovimientoDTO oALM_MovimientoDTO = new ALM_MovimientoDTO();
                        oALM_MovimientoDTO.idMovimiento = Convert.ToInt32(dr["idMovimiento"] == null ? 0 : Convert.ToInt32(dr["idMovimiento"].ToString()));
                        oALM_MovimientoDTO.DesLocal = dr["DesLocal"].ToString();
                        oALM_MovimientoDTO.Observaciones = dr["Observaciones"].ToString();
                        oALM_MovimientoDTO.DesEstado = dr["DesEstado"].ToString();
                        oALM_MovimientoDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oResultDTO.ListaResultado.Add(oALM_MovimientoDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<ALM_MovimientoDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
