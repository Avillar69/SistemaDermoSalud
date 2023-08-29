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
    public class Seg_MenuDAO
    {
        public ResultDTO<Seg_MenuDTO> ListarTodo(int idEmpresa)
        {
            ResultDTO<Seg_MenuDTO> oResultDTO = new ResultDTO<Seg_MenuDTO>();
            oResultDTO.ListaResultado = new List<Seg_MenuDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Menu_ListarTodo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Seg_MenuDTO oSeg_MenuDTO = new Seg_MenuDTO();
                        oSeg_MenuDTO.idMenu = Convert.ToInt32(dr["idMenu"] == null ? 0 : Convert.ToInt32(dr["idMenu"].ToString()));
                        oSeg_MenuDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oSeg_MenuDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oSeg_MenuDTO.Icono = dr["Icono"] == null ? "" : dr["Icono"].ToString();
                        oSeg_MenuDTO.idMenuPadre = Convert.ToInt32(dr["idMenuPadre"] == null ? 0 : Convert.ToInt32(dr["idMenuPadre"].ToString()));
                        oSeg_MenuDTO.Nivel = Convert.ToInt32(dr["Nivel"] == null ? 0 : Convert.ToInt32(dr["Nivel"].ToString()));
                        oSeg_MenuDTO.Posicion = Convert.ToInt32(dr["Posicion"] == null ? 0 : Convert.ToInt32(dr["Posicion"].ToString()));
                        oSeg_MenuDTO.Action = dr["Action"] == null ? "" : dr["Action"].ToString();
                        oSeg_MenuDTO.Controller = dr["Controller"] == null ? "" : dr["Controller"].ToString();
                        oSeg_MenuDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oSeg_MenuDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oSeg_MenuDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oSeg_MenuDTO.FechaModificacion = Convert.ToDateTime(string.IsNullOrWhiteSpace(dr["FechaModificacion"].ToString()) ? DateTime.Now.ToString("dd/MM/yyyy") : dr["FechaModificacion"].ToString());
                        oResultDTO.ListaResultado.Add(oSeg_MenuDTO);
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
        public ResultDTO<Seg_MenuDTO> ListarMenuxRol(int idEmpresa, int idRol)
        {
            ResultDTO<Seg_MenuDTO> oResultDTO = new ResultDTO<Seg_MenuDTO>();
            oResultDTO.ListaResultado = new List<Seg_MenuDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Menu_ListarRol", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@idRol", idRol);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Seg_MenuDTO oSeg_MenuDTO = new Seg_MenuDTO();
                        oSeg_MenuDTO.idMenu = Convert.ToInt32(dr["idMenu"] == null ? 0 : Convert.ToInt32(dr["idMenu"].ToString()));
                        oSeg_MenuDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oSeg_MenuDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oSeg_MenuDTO.Icono = dr["Icono"] == null ? "" : dr["Icono"].ToString();
                        oSeg_MenuDTO.idMenuPadre = Convert.ToInt32(dr["idMenuPadre"] == null ? 0 : Convert.ToInt32(dr["idMenuPadre"].ToString()));
                        oSeg_MenuDTO.Nivel = Convert.ToInt32(dr["Nivel"] == null ? 0 : Convert.ToInt32(dr["Nivel"].ToString()));
                        oSeg_MenuDTO.Posicion = Convert.ToInt32(dr["Posicion"] == null ? 0 : Convert.ToInt32(dr["Posicion"].ToString()));
                        oSeg_MenuDTO.Action = dr["Action"] == null ? "" : dr["Action"].ToString();
                        oSeg_MenuDTO.Controller = dr["Controller"] == null ? "" : dr["Controller"].ToString();
                        oSeg_MenuDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oSeg_MenuDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oSeg_MenuDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oSeg_MenuDTO);
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
        public ResultDTO<Seg_MenuDTO> ListarxID(int idMenu)
        {
            ResultDTO<Seg_MenuDTO> oResultDTO = new ResultDTO<Seg_MenuDTO>();
            oResultDTO.ListaResultado = new List<Seg_MenuDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Menu_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idMenu", idMenu);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        Seg_MenuDTO oSeg_MenuDTO = new Seg_MenuDTO();
                        oSeg_MenuDTO.idMenu = Convert.ToInt32(dr["idMenu"].ToString());
                        oSeg_MenuDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oSeg_MenuDTO.Descripcion = dr["Descripcion"].ToString();
                        oSeg_MenuDTO.Icono = dr["Icono"].ToString();
                        oSeg_MenuDTO.idMenuPadre = Convert.ToInt32(dr["idMenuPadre"].ToString());
                        oSeg_MenuDTO.Nivel = Convert.ToInt32(dr["Nivel"].ToString());
                        oSeg_MenuDTO.Posicion = Convert.ToInt32(dr["Posicion"].ToString());
                        oSeg_MenuDTO.Action = dr["Action"].ToString();
                        oSeg_MenuDTO.Controller = dr["Controller"].ToString();
                        oSeg_MenuDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oSeg_MenuDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oSeg_MenuDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oSeg_MenuDTO.FechaModificacion = Convert.ToDateTime(string.IsNullOrWhiteSpace(dr["FechaModificacion"].ToString()) ? DateTime.Now.ToString("dd/MM/yyyy") : dr["FechaModificacion"].ToString());
                        oResultDTO.ListaResultado.Add(oSeg_MenuDTO);
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
        public ResultDTO<Seg_MenuDTO> UpdateInsert(Seg_MenuDTO oSeg_Menu)
        {
            ResultDTO<Seg_MenuDTO> oResultDTO = new ResultDTO<Seg_MenuDTO>();
            oResultDTO.ListaResultado = new List<Seg_MenuDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Menu_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMenu", oSeg_Menu.idMenu);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oSeg_Menu.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oSeg_Menu.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Icono", oSeg_Menu.Icono);
                        da.SelectCommand.Parameters.AddWithValue("@idMenuPadre", oSeg_Menu.idMenuPadre);
                        da.SelectCommand.Parameters.AddWithValue("@Nivel", oSeg_Menu.Nivel);
                        da.SelectCommand.Parameters.AddWithValue("@Posicion", oSeg_Menu.Posicion);
                        da.SelectCommand.Parameters.AddWithValue("@Action", oSeg_Menu.Action);
                        da.SelectCommand.Parameters.AddWithValue("@Controller", oSeg_Menu.Controller);
                        da.SelectCommand.Parameters.AddWithValue("@FechaCreacion", oSeg_Menu.FechaCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaModificacion", oSeg_Menu.FechaModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oSeg_Menu.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oSeg_Menu.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oSeg_Menu.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<Seg_MenuDTO> Delete(Seg_MenuDTO oSeg_Menu)
        {
            ResultDTO<Seg_MenuDTO> oResultDTO = new ResultDTO<Seg_MenuDTO>();
            oResultDTO.ListaResultado = new List<Seg_MenuDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Menu_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idMenu", oSeg_Menu.idMenu);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                    }
                }
            }
            return oResultDTO;
        }
    }
}
