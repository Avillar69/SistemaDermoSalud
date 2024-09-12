
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
    public class DashboardDAO
    {
        public ResultDTO<DashboardDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<DashboardDTO> oResultDTO = new ResultDTO<DashboardDTO>();
            oResultDTO.ListaResultado = new List<DashboardDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Home_DatosGenerales", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        DashboardDTO oDashboardDTO = new DashboardDTO();
                        oDashboardDTO.ComprasSoles = Convert.ToDecimal(dr["ComprasSoles"] == null ? 0 : Convert.ToDecimal(dr["ComprasSoles"].ToString())); ;
                        oDashboardDTO.VentasSoles = Convert.ToDecimal(dr["VentasSoles"] == null ? 0 : Convert.ToDecimal(dr["VentasSoles"].ToString()));
                        oDashboardDTO.Pagos = Convert.ToDecimal(dr["Pagos"] == null ? 0 : Convert.ToDecimal(dr["Pagos"].ToString()));
                        oDashboardDTO.Cobros = Convert.ToDecimal(dr["Cobros"] == null ? 0 : Convert.ToDecimal(dr["Cobros"].ToString()));

                        oResultDTO.ListaResultado.Add(oDashboardDTO);

                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        DashboardTopProductoDTO oDashboardTopProductoDTO = new DashboardTopProductoDTO();
                        oDashboardTopProductoDTO.FechaUltima = Convert.ToDateTime(dr["FechaUltimaVenta"] == null ? Convert.ToDateTime("01-01-2000") : Convert.ToDateTime(dr["FechaUltimaVenta"].ToString())).ToString("dd-MM-yyyy");
                        oDashboardTopProductoDTO.idArticulo = Convert.ToInt32(dr["idArticulo"].ToString());
                        oDashboardTopProductoDTO.DescripcionArticulo = dr["DescripcionArticulo"].ToString();
                        oDashboardTopProductoDTO.Stock = Convert.ToDecimal(dr["Stock"] == null ? 0 : Convert.ToDecimal(dr["Stock"].ToString()));
                        oDashboardTopProductoDTO.TotalVendido = Convert.ToDecimal(dr["Suma"] == null ? 0 : Convert.ToDecimal(dr["Suma"].ToString()));
                        oDashboardTopProductoDTO.Precio = Convert.ToDecimal(dr["Precio"] == null ? 0 : Convert.ToDecimal(dr["Precio"].ToString()));
                        oResultDTO.ListaResultado[0].listaTopArticulos.Add(oDashboardTopProductoDTO);
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        DashboardTopClientesDTO oVEN_DocumentoVentaDTO = new DashboardTopClientesDTO();
                        oVEN_DocumentoVentaDTO.idCliente = Convert.ToInt32(dr["idCliente"].ToString());
                        oVEN_DocumentoVentaDTO.FechaUltimaVenta = Convert.ToDateTime(dr["FechaUltimaVenta"] == null ? Convert.ToDateTime("01-01-2000") : Convert.ToDateTime(dr["FechaUltimaVenta"].ToString())).ToString("dd-MM-yyyy");
                        oVEN_DocumentoVentaDTO.ClienteRazon = dr["ClienteRazon"].ToString();
                        oVEN_DocumentoVentaDTO.MontoUltimaVenta = Convert.ToDecimal(dr["MontoUltimaVenta"].ToString());
                        oVEN_DocumentoVentaDTO.Suma = Convert.ToDecimal(dr["Suma"].ToString());
                        oResultDTO.ListaResultado[0].listaVentas.Add(oVEN_DocumentoVentaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<DashboardDTO>();
                }
            }
            return oResultDTO;
        }

        public ResultDTO<TipoServicioDTO> ListarTodoCirculo(DateTime fechaInicio, DateTime FechaFin, SqlConnection cn = null)
        {
            ResultDTO<TipoServicioDTO> oResultDTO = new ResultDTO<TipoServicioDTO>();
            oResultDTO.ListaResultado = new List<TipoServicioDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_Home_DatosTipoServicio", cn);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", FechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        TipoServicioDTO oDashboardDTO = new TipoServicioDTO();
                        oDashboardDTO.idTipoServicio = Convert.ToInt32(dr["idTipoServicio"] == null ? 0 : Convert.ToInt32(dr["idTipoServicio"].ToString()));
                        oDashboardDTO.cantServicio = Convert.ToInt32(dr["UsoServi"] == null ? 0 : Convert.ToInt32(dr["UsoServi"].ToString()));
                        oDashboardDTO.NombreTipoServicio = dr["NombreTipoServicio"].ToString();
                        oResultDTO.ListaResultado.Add(oDashboardDTO);

                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<TipoServicioDTO>();
                }
            }
            return oResultDTO;
        }


    }
}
