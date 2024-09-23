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
    public class AD_SocioNegocioDAO
    {
        public ResultDTO<AD_SocioNegocioDTO> ListarTodo(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = new ResultDTO<AD_SocioNegocioDTO>();
            oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_AD_SocioNegocio_ListarTodo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AD_SocioNegocioDTO oAD_SocioNegocioDTO = new AD_SocioNegocioDTO();
                        oAD_SocioNegocioDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oAD_SocioNegocioDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oAD_SocioNegocioDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oAD_SocioNegocioDTO.idTipoPersona = Convert.ToInt32(dr["idTipoPersona"] == null ? 0 : Convert.ToInt32(dr["idTipoPersona"].ToString()));
                        oAD_SocioNegocioDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oAD_SocioNegocioDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"] == null ? 0 : Convert.ToInt32(dr["idTipoDocumento"].ToString()));
                        oAD_SocioNegocioDTO.Documento = dr["Documento"] == null ? "" : dr["Documento"].ToString();
                        oAD_SocioNegocioDTO.idPais = Convert.ToInt32(dr["idPais"] == null ? 0 : Convert.ToInt32(dr["idPais"].ToString()));
                        oAD_SocioNegocioDTO.idDepartamento = dr["idDepartamento"] == null ? "" : (dr["idDepartamento"].ToString());
                        oAD_SocioNegocioDTO.idProvincia = (dr["idProvincia"] == null ? "" : (dr["idProvincia"].ToString()));
                        oAD_SocioNegocioDTO.idDistrito = dr["idDistrito"] == null ? "" : dr["idDistrito"].ToString();
                        oAD_SocioNegocioDTO.Web = dr["Web"] == null ? "" : dr["Web"].ToString();
                        oAD_SocioNegocioDTO.Mail = dr["Mail"] == null ? "" : dr["Mail"].ToString();
                        oAD_SocioNegocioDTO.Cliente = Convert.ToBoolean(dr["Cliente"] == null ? false : Convert.ToBoolean(dr["Cliente"].ToString()));
                        oAD_SocioNegocioDTO.Proveedor = Convert.ToBoolean(dr["Proveedor"] == null ? false : Convert.ToBoolean(dr["Proveedor"].ToString()));
                        oAD_SocioNegocioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oAD_SocioNegocioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oAD_SocioNegocioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oAD_SocioNegocioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oAD_SocioNegocioDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));

                        oAD_SocioNegocioDTO.DesUsuarioModificacion = dr["DesUsuarioModificacion"].ToString();
                        oAD_SocioNegocioDTO.DesTipoDocumento = dr["DesTipoDocumento"].ToString();
                        //oAD_SocioNegocioDTO.Direccion = dr["Direccion"].ToString();
                        oResultDTO.ListaResultado.Add(oAD_SocioNegocioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_SocioNegocioDTO> ListarxNroDocumento(string nroDocumento)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = new ResultDTO<AD_SocioNegocioDTO>();
            oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_AD_SocioNegocio_ListarxDocumento", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@nroDocumento", nroDocumento);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AD_SocioNegocioDTO oAD_SocioNegocioDTO = new AD_SocioNegocioDTO();
                        oAD_SocioNegocioDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"].ToString());
                        oAD_SocioNegocioDTO.RazonSocial = dr["RazonSocial"].ToString();
                        oResultDTO.ListaResultado.Add(oAD_SocioNegocioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_SocioNegocioDTO> ListarxID(int idSocioNegocio)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = new ResultDTO<AD_SocioNegocioDTO>();
            oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_AD_SocioNegocio_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idSocioNegocio", idSocioNegocio);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AD_SocioNegocioDTO oAD_SocioNegocioDTO = new AD_SocioNegocioDTO();
                        oAD_SocioNegocioDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"].ToString());
                        oAD_SocioNegocioDTO.CodigoGenerado = dr["CodigoGenerado"].ToString();
                        oAD_SocioNegocioDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"].ToString());
                        oAD_SocioNegocioDTO.idTipoPersona = Convert.ToInt32(dr["idTipoPersona"].ToString());
                        oAD_SocioNegocioDTO.RazonSocial = dr["RazonSocial"].ToString();
                        oAD_SocioNegocioDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"].ToString());
                        oAD_SocioNegocioDTO.Documento = dr["Documento"].ToString();
                        oAD_SocioNegocioDTO.idPais = Convert.ToInt32(dr["idPais"].ToString());
                        oAD_SocioNegocioDTO.idDepartamento = (dr["idDepartamento"].ToString());
                        oAD_SocioNegocioDTO.idProvincia = (dr["idProvincia"].ToString());
                        oAD_SocioNegocioDTO.idDistrito = dr["idDistrito"].ToString();
                        oAD_SocioNegocioDTO.Web = dr["Web"].ToString();
                        oAD_SocioNegocioDTO.Mail = dr["Mail"].ToString();
                        oAD_SocioNegocioDTO.Cliente = Convert.ToBoolean(dr["Cliente"].ToString());
                        oAD_SocioNegocioDTO.Proveedor = Convert.ToBoolean(dr["Proveedor"].ToString());
                        oAD_SocioNegocioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oAD_SocioNegocioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oAD_SocioNegocioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oAD_SocioNegocioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oAD_SocioNegocioDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oAD_SocioNegocioDTO.CantCompras = Convert.ToInt32(dr["CantCompras"].ToString());
                        oResultDTO.ListaResultado.Add(oAD_SocioNegocioDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<AD_SocioNegocio_ContactoDTO> lista = new List<AD_SocioNegocio_ContactoDTO>();
                            while (dr.Read())
                            {
                                AD_SocioNegocio_ContactoDTO obj = new AD_SocioNegocio_ContactoDTO();
                                obj.idContacto = Convert.ToInt32(dr["idContacto"].ToString());
                                obj.NombreCompleto = dr["NombreCompleto"].ToString();
                                obj.Cargo = dr["Cargo"].ToString();
                                obj.Telefono = dr["Telefono"].ToString();
                                obj.Mail = dr["Mail"].ToString();
                                lista.Add(obj);
                            }
                            oResultDTO.ListaResultado[0].oListaContacto = lista;
                        }
                        if (dr.NextResult())
                        {
                            List<AD_SocioNegocio_DireccionDTO> lista = new List<AD_SocioNegocio_DireccionDTO>();
                            while (dr.Read())
                            {
                                AD_SocioNegocio_DireccionDTO obj = new AD_SocioNegocio_DireccionDTO();
                                obj.idDireccion = Convert.ToInt32(dr["idDireccion"].ToString());
                                obj.Direccion = dr["Direccion"].ToString();
                                obj.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                                obj.Principal = Convert.ToBoolean(dr["Principal"].ToString());
                                lista.Add(obj);
                            }
                            oResultDTO.ListaResultado[0].oListaDireccion = lista;
                        }
                        if (dr.NextResult())
                        {
                            List<AD_SocioNegocio_TelefonoDTO> lista = new List<AD_SocioNegocio_TelefonoDTO>();
                            while (dr.Read())
                            {
                                AD_SocioNegocio_TelefonoDTO obj = new AD_SocioNegocio_TelefonoDTO();
                                obj.idTelefono = Convert.ToInt32(dr["idTelefono"].ToString());
                                obj.Telefono = dr["Telefono"].ToString();
                                obj.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                                lista.Add(obj);
                            }
                            oResultDTO.ListaResultado[0].oListaTelefono = lista;
                        }
                        if (dr.NextResult())
                        {
                            List<AD_SocioNegocio_CuentaBancariaDTO> lista = new List<AD_SocioNegocio_CuentaBancariaDTO>();
                            while (dr.Read())
                            {
                                AD_SocioNegocio_CuentaBancariaDTO obj = new AD_SocioNegocio_CuentaBancariaDTO();
                                obj.idCuentaBancaria = Convert.ToInt32(dr["idCuentaBancaria"].ToString());
                                obj.idBanco = Convert.ToInt32(dr["idBanco"].ToString());
                                obj.DescripcionCuenta = dr["DescripcionCuenta"].ToString();
                                obj.Cuenta = dr["Cuenta"].ToString();
                                obj.idMoneda = Convert.ToInt32(dr["idMoneda"].ToString());
                                obj.DesBanco = dr["DesBanco"].ToString();
                                obj.DesMoneda = dr["DesMoneda"].ToString();
                                lista.Add(obj);
                            }
                            oResultDTO.ListaResultado[0].oListaCuentaBancaria = lista;
                        }
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_SocioNegocioDTO> UpdateInsert(AD_SocioNegocioDTO oAD_SocioNegocio, int idUsuario)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = new ResultDTO<AD_SocioNegocioDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_AD_SocioNegocio_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idSocioNegocio", oAD_SocioNegocio.idSocioNegocio);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oAD_SocioNegocio.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoPersona", oAD_SocioNegocio.idTipoPersona);
                        da.SelectCommand.Parameters.AddWithValue("@RazonSocial", oAD_SocioNegocio.RazonSocial);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoDocumento", oAD_SocioNegocio.idTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@Documento", oAD_SocioNegocio.Documento);
                        da.SelectCommand.Parameters.AddWithValue("@idPais", oAD_SocioNegocio.idPais);
                        da.SelectCommand.Parameters.AddWithValue("@idDepartamento", Convert.ToInt32(oAD_SocioNegocio.idDepartamento).ToString("00"));
                        da.SelectCommand.Parameters.AddWithValue("@idProvincia", Convert.ToInt32(oAD_SocioNegocio.idProvincia).ToString("0000"));
                        da.SelectCommand.Parameters.AddWithValue("@idDistrito", Convert.ToInt32(oAD_SocioNegocio.idDistrito).ToString("000000"));
                        da.SelectCommand.Parameters.AddWithValue("@Web", oAD_SocioNegocio.Web);
                        da.SelectCommand.Parameters.AddWithValue("@Mail", oAD_SocioNegocio.Mail);
                        da.SelectCommand.Parameters.AddWithValue("@Cliente", oAD_SocioNegocio.Cliente);
                        da.SelectCommand.Parameters.AddWithValue("@Proveedor", oAD_SocioNegocio.Proveedor);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oAD_SocioNegocio.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oAD_SocioNegocio.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oAD_SocioNegocio.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Lista_Contacto", oAD_SocioNegocio.Lista_Contacto);
                        da.SelectCommand.Parameters.AddWithValue("@Lista_Direccion", oAD_SocioNegocio.Lista_Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@Lista_Telefono", oAD_SocioNegocio.Lista_Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@Lista_Cuenta", oAD_SocioNegocio.Lista_CuentaBancaria);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oAD_SocioNegocio.idEmpresa, cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oAD_SocioNegocio.idEmpresa, idUsuario,
                                "ADMINISTRACION-SOCIO_NEGOCIO", "AD_SocioNegocio", (int)id_output.Value, (oAD_SocioNegocio.idSocioNegocio == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_SocioNegocioDTO> Delete(AD_SocioNegocioDTO oAD_SocioNegocio, int idUsuario)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = new ResultDTO<AD_SocioNegocioDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_AD_SocioNegocio_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idSocioNegocio", oAD_SocioNegocio.idSocioNegocio);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(oAD_SocioNegocio.idEmpresa, cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oAD_SocioNegocio.idEmpresa, idUsuario,
                                "ADMINISTRACION-SOCIO_NEGOCIO", "AD_SocioNegocio", oAD_SocioNegocio.idSocioNegocio, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_SocioNegocioDTO> ListarProv(int idEmpresa, string tipo, int idTipoComprobante = 0, SqlConnection cn = null)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = new ResultDTO<AD_SocioNegocioDTO>();
            oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_AD_SocioNegocio_ListarProv", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.Parameters.AddWithValue("@tipo", tipo);
                    da.SelectCommand.Parameters.AddWithValue("@idTipoComprobante", idTipoComprobante);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        AD_SocioNegocioDTO oAD_SocioNegocioDTO = new AD_SocioNegocioDTO();
                        oAD_SocioNegocioDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oAD_SocioNegocioDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oAD_SocioNegocioDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"] == null ? 0 : Convert.ToInt32(dr["idTipoDocumento"].ToString()));
                        oAD_SocioNegocioDTO.Documento = dr["Documento"] == null ? "" : dr["Documento"].ToString();
                        oAD_SocioNegocioDTO.Cliente = Convert.ToBoolean(dr["Cliente"] == null ? false : Convert.ToBoolean(dr["Cliente"].ToString()));

                        oAD_SocioNegocioDTO.DesUsuarioModificacion = dr["DesUsuarioModificacion"].ToString();
                        oAD_SocioNegocioDTO.DesTipoDocumento = dr["DesTipoDocumento"].ToString();
                        oResultDTO.ListaResultado.Add(oAD_SocioNegocioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                }
            }
            return oResultDTO;
        }
        /*---------------------------------------------cambios hechos el  3/04/2018 ---------------------------------------*/

        public ResultDTO<AD_SocioNegocioDTO> ListarPorTipoCliente(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = new ResultDTO<AD_SocioNegocioDTO>();
            oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_AD_SocioNegocio_ListarCliente", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {

                        AD_SocioNegocioDTO oAD_SocioNegocioDTO = new AD_SocioNegocioDTO();
                        oAD_SocioNegocioDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oAD_SocioNegocioDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oAD_SocioNegocioDTO.Documento = dr["Documento"] == null ? "" : dr["Documento"].ToString();
                        oAD_SocioNegocioDTO.Cliente = Convert.ToBoolean(dr["Cliente"] == null ? false : Convert.ToBoolean(dr["Cliente"].ToString()));
                        oAD_SocioNegocioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oAD_SocioNegocioDTO.DesUsuarioModificacion = dr["DesUsuarioModificacion"].ToString();
                        oAD_SocioNegocioDTO.DesTipoDocumento = dr["DesTipoDocumento"].ToString();
                        //oAD_SocioNegocioDTO.Direccion = dr["Direccion"].ToString();
                        oResultDTO.ListaResultado.Add(oAD_SocioNegocioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_SocioNegocioDTO> ListarPorTipoProveedor(int idEmpresa, SqlConnection cn = null)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = new ResultDTO<AD_SocioNegocioDTO>();
            oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_AD_SocioNegocio_ListarProveedor", cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpresa", idEmpresa);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {

                        AD_SocioNegocioDTO oAD_SocioNegocioDTO = new AD_SocioNegocioDTO();
                        oAD_SocioNegocioDTO.idSocioNegocio = Convert.ToInt32(dr["idSocioNegocio"] == null ? 0 : Convert.ToInt32(dr["idSocioNegocio"].ToString()));
                        oAD_SocioNegocioDTO.CodigoGenerado = dr["CodigoGenerado"] == null ? "" : dr["CodigoGenerado"].ToString();
                        oAD_SocioNegocioDTO.idEmpresa = Convert.ToInt32(dr["idEmpresa"] == null ? 0 : Convert.ToInt32(dr["idEmpresa"].ToString()));
                        oAD_SocioNegocioDTO.idTipoPersona = Convert.ToInt32(dr["idTipoPersona"] == null ? 0 : Convert.ToInt32(dr["idTipoPersona"].ToString()));
                        oAD_SocioNegocioDTO.RazonSocial = dr["RazonSocial"] == null ? "" : dr["RazonSocial"].ToString();
                        oAD_SocioNegocioDTO.idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"] == null ? 0 : Convert.ToInt32(dr["idTipoDocumento"].ToString()));
                        oAD_SocioNegocioDTO.Documento = dr["Documento"] == null ? "" : dr["Documento"].ToString();
                        oAD_SocioNegocioDTO.idPais = Convert.ToInt32(dr["idPais"] == null ? 0 : Convert.ToInt32(dr["idPais"].ToString()));
                        oAD_SocioNegocioDTO.idDepartamento = dr["idDepartamento"] == null ? "" : (dr["idDepartamento"].ToString());
                        oAD_SocioNegocioDTO.idProvincia = (dr["idProvincia"] == null ? "" : (dr["idProvincia"].ToString()));
                        oAD_SocioNegocioDTO.idDistrito = dr["idDistrito"] == null ? "" : dr["idDistrito"].ToString();
                        oAD_SocioNegocioDTO.Web = dr["Web"] == null ? "" : dr["Web"].ToString();
                        oAD_SocioNegocioDTO.Mail = dr["Mail"] == null ? "" : dr["Mail"].ToString();
                        oAD_SocioNegocioDTO.Cliente = Convert.ToBoolean(dr["Cliente"] == null ? false : Convert.ToBoolean(dr["Cliente"].ToString()));
                        oAD_SocioNegocioDTO.Proveedor = Convert.ToBoolean(dr["Proveedor"] == null ? false : Convert.ToBoolean(dr["Proveedor"].ToString()));
                        oAD_SocioNegocioDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oAD_SocioNegocioDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oAD_SocioNegocioDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oAD_SocioNegocioDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oAD_SocioNegocioDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));

                        oAD_SocioNegocioDTO.DesUsuarioModificacion = dr["DesUsuarioModificacion"].ToString();
                        oAD_SocioNegocioDTO.DesTipoDocumento = dr["DesTipoDocumento"].ToString();
                        //oAD_SocioNegocioDTO.Direccion = dr["Direccion"].ToString();
                        oResultDTO.ListaResultado.Add(oAD_SocioNegocioDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                }
            }
            return oResultDTO;
        }

        /***************************************/
        public ResultDTO<AD_SocioNegocioDTO> UpdateInsertCli(AD_SocioNegocioDTO oAD_SocioNegocio, int idUsuario)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = new ResultDTO<AD_SocioNegocioDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_AD_SocioNegocio_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idSocioNegocio", oAD_SocioNegocio.idSocioNegocio);
                        da.SelectCommand.Parameters.AddWithValue("@idEmpresa", oAD_SocioNegocio.idEmpresa);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoPersona", oAD_SocioNegocio.idTipoPersona);
                        da.SelectCommand.Parameters.AddWithValue("@RazonSocial", oAD_SocioNegocio.RazonSocial);
                        da.SelectCommand.Parameters.AddWithValue("@idTipoDocumento", oAD_SocioNegocio.idTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@Documento", oAD_SocioNegocio.Documento);
                        da.SelectCommand.Parameters.AddWithValue("@idPais", oAD_SocioNegocio.idPais);
                        da.SelectCommand.Parameters.AddWithValue("@idDepartamento", Convert.ToInt32(oAD_SocioNegocio.idDepartamento).ToString("00"));
                        da.SelectCommand.Parameters.AddWithValue("@idProvincia", Convert.ToInt32(oAD_SocioNegocio.idProvincia).ToString("0000"));
                        da.SelectCommand.Parameters.AddWithValue("@idDistrito", Convert.ToInt32(oAD_SocioNegocio.idDistrito).ToString("000000"));
                        da.SelectCommand.Parameters.AddWithValue("@Web", oAD_SocioNegocio.Web);
                        da.SelectCommand.Parameters.AddWithValue("@Mail", oAD_SocioNegocio.Mail);
                        da.SelectCommand.Parameters.AddWithValue("@Cliente", oAD_SocioNegocio.Cliente);
                        da.SelectCommand.Parameters.AddWithValue("@Proveedor", oAD_SocioNegocio.Proveedor);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oAD_SocioNegocio.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oAD_SocioNegocio.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oAD_SocioNegocio.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Lista_Contacto", oAD_SocioNegocio.Lista_Contacto);
                        da.SelectCommand.Parameters.AddWithValue("@Lista_Direccion", oAD_SocioNegocio.Lista_Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@Lista_Telefono", oAD_SocioNegocio.Lista_Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@Lista_Cuenta", oAD_SocioNegocio.Lista_CuentaBancaria);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarPorTipoCliente(oAD_SocioNegocio.idEmpresa, cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oAD_SocioNegocio.idEmpresa, idUsuario,
                                "ADMINISTRACION-SOCIO_NEGOCIO", "AD_SocioNegocio", (int)id_output.Value, (oAD_SocioNegocio.idSocioNegocio == 0 ? "INSERT" : "UPDATE"));
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<AD_SocioNegocioDTO> DeleteCli(AD_SocioNegocioDTO oAD_SocioNegocio, int idUsuario)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = new ResultDTO<AD_SocioNegocioDTO>();
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
                        SqlDataAdapter da = new SqlDataAdapter("SP_AD_SocioNegocio_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idSocioNegocio", oAD_SocioNegocio.idSocioNegocio);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarPorTipoCliente(oAD_SocioNegocio.idEmpresa, cn).ListaResultado;
                            new Seg_LogDAO().UpdateInsert(da, cn, oAD_SocioNegocio.idEmpresa, idUsuario,
                                "ADMINISTRACION-SOCIO_NEGOCIO", "AD_SocioNegocio", oAD_SocioNegocio.idSocioNegocio, "DELETE");
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<AD_SocioNegocioDTO>();
                    }
                }
            }
            return oResultDTO;
        }

        /************/
        public ResultDTO<ContribuyenteDTO> ConsultaRUC(string ruc)
        {
            ResultDTO<ContribuyenteDTO> oResultDTO = new ResultDTO<ContribuyenteDTO>();
            oResultDTO.ListaResultado = new List<ContribuyenteDTO>();

            SqlConnection cn = new SqlConnection("Server=186.64.122.205;Database=Sunat_PadronReducido;User ID=sa;Password=sqlclick;Trusted_Connection=False;MultipleActiveResultSets=True");
            using (cn)
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_ConsultaRUC", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RUC", ruc);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        ContribuyenteDTO obj = new ContribuyenteDTO();
                        obj.RazonSocial = dr["RazonSocial"].ToString();
                        obj.RUC = dr["RUC"].ToString();
                        obj.EstadoContribuyente = dr["EstadoContribuyente"].ToString();
                        obj.CondicionDomicilio = dr["CondicionDomicilio"].ToString();
                        obj.Ubigeo = dr["Ubigeo"].ToString();
                        obj.TipoVia = dr["TipoVia"].ToString();
                        obj.NombreVia = dr["NombreVia"].ToString();
                        obj.CodigoZona = dr["CodigoZona"].ToString();
                        obj.TipoZona = dr["TipoZona"].ToString();
                        obj.Numero = dr["Numero"].ToString();
                        obj.Interior = dr["Interior"].ToString();
                        obj.Lote = dr["Lote"].ToString();
                        obj.Departamento = dr["Departamento"].ToString();
                        obj.Manzana = dr["Manzana"].ToString();
                        obj.Kilometro = dr["Kilometro"].ToString();
                        obj.FechaActualizacion = Convert.ToDateTime(dr["FechaActualizacion"].ToString());

                        obj.Depa = dr["Depa"].ToString();
                        obj.Prov = dr["Prov"].ToString();
                        obj.Dist = dr["Dist"].ToString();

                        oResultDTO.ListaResultado.Add(obj);
                    }
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<ContribuyenteDTO>();
                }
            }
            return oResultDTO;
        }
    }
}