
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
                        oDashboardDTO.ComprasSoles = Convert.ToDecimal(dr["ComprasSoles"] == null ? 0 : Convert.ToDecimal(dr["ComprasSoles"].ToString()));
                        oDashboardDTO.ComprasDolares = Convert.ToDecimal(dr["ComprasDolares"] == null ? 0 : Convert.ToDecimal(dr["ComprasDolares"].ToString()));
                        oDashboardDTO.VentasSoles = Convert.ToDecimal(dr["VentasSoles"] == null ? 0 : Convert.ToDecimal(dr["VentasSoles"].ToString()));
                        oDashboardDTO.VentasDolares = Convert.ToDecimal(dr["VentasDolares"] == null ? 0 : Convert.ToDecimal(dr["VentasDolares"].ToString()));
                        oDashboardDTO.Pagos = Convert.ToDecimal(dr["Pagos"] == null ? 0 : Convert.ToDecimal(dr["Pagos"].ToString()));
                        oDashboardDTO.Cobros = Convert.ToDecimal(dr["Cobros"] == null ? 0 : Convert.ToDecimal(dr["Cobros"].ToString()));
                        oDashboardDTO.ComprasEnero = Convert.ToDecimal(dr["CitasEnero"] == null ? 0 : Convert.ToDecimal(dr["CitasEnero"].ToString()));
                        oDashboardDTO.ComprasFebrero = Convert.ToDecimal(dr["CitasFebrero"] == null ? 0 : Convert.ToDecimal(dr["CitasFebrero"].ToString()));
                        oDashboardDTO.ComprasMarzo = Convert.ToDecimal(dr["CitasMarzo"] == null ? 0 : Convert.ToDecimal(dr["CitasMarzo"].ToString()));
                        oDashboardDTO.ComprasAbril = Convert.ToDecimal(dr["CitasAbril"] == null ? 0 : Convert.ToDecimal(dr["CitasAbril"].ToString()));
                        oDashboardDTO.ComprasMayo = Convert.ToDecimal(dr["CitasMayo"] == null ? 0 : Convert.ToDecimal(dr["CitasMayo"].ToString()));
                        oDashboardDTO.ComprasJunio = Convert.ToDecimal(dr["CitasJunio"] == null ? 0 : Convert.ToDecimal(dr["CitasJunio"].ToString()));
                        oDashboardDTO.ComprasJulio = Convert.ToDecimal(dr["CitasJulio"] == null ? 0 : Convert.ToDecimal(dr["CitasJulio"].ToString()));
                        oDashboardDTO.ComprasAgosto = Convert.ToDecimal(dr["CitasAgosto"] == null ? 0 : Convert.ToDecimal(dr["CitasAgosto"].ToString()));
                        oDashboardDTO.ComprasSetiembre = Convert.ToDecimal(dr["CitasSetiembre"] == null ? 0 : Convert.ToDecimal(dr["CitasSetiembre"].ToString()));
                        oDashboardDTO.ComprasOctubre = Convert.ToDecimal(dr["CitasOctubre"] == null ? 0 : Convert.ToDecimal(dr["CitasOctubre"].ToString()));
                        oDashboardDTO.ComprasNoviembre = Convert.ToDecimal(dr["CitasNoviembre"] == null ? 0 : Convert.ToDecimal(dr["CitasNoviembre"].ToString()));
                        oDashboardDTO.ComprasDiciembre = Convert.ToDecimal(dr["CitasDiciembre"] == null ? 0 : Convert.ToDecimal(dr["CitasDiciembre"].ToString()));

                        oResultDTO.ListaResultado.Add(oDashboardDTO);

                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        DashboardDocDTO oDashboardDocDTO = new DashboardDocDTO();
                        oDashboardDocDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"] == null ? Convert.ToDateTime("01-01-2000") : Convert.ToDateTime(dr["FechaDocumento"].ToString()));
                        oDashboardDocDTO.Numero = $"{dr["SerieDocumento"].ToString()}-{dr["NumDocumento"].ToString()}";// dr["Numero"] == null ? "" : dr["Numero"].ToString();
                        oDashboardDocDTO.ProveedorRazon = dr["ProveedorRazon"] == null ? "" : dr["ProveedorRazon"].ToString();
                        oDashboardDocDTO.Descripcion = dr["Descripcion"] == null ? "" : dr["Descripcion"].ToString();
                        oDashboardDocDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"] == null ? 0 : Convert.ToDecimal(dr["TotalNacional"].ToString()));
                        oResultDTO.ListaResultado[0].listaCompras.Add(oDashboardDocDTO);
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocumentoVentaDTO();
                        oVEN_DocumentoVentaDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"] == null ? Convert.ToDateTime("01-01-2000") : Convert.ToDateTime(dr["FechaDocumento"].ToString()));
                        oVEN_DocumentoVentaDTO.SerieDocumento = $"{dr["SerieDocumento"].ToString()}-{dr["NumDocumento"].ToString()}";
                        oVEN_DocumentoVentaDTO.ClienteRazon = dr["ClienteRazon"].ToString();
                        oVEN_DocumentoVentaDTO.MonedaDesc = dr["MonedaDesc"].ToString();
                        oVEN_DocumentoVentaDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        oResultDTO.ListaResultado[0].listaVentaDoc.Add(oVEN_DocumentoVentaDTO);
                    }

                    dr.NextResult();

                    while (dr.Read())
                    {
                        COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO = new COM_DocumentoCompraDTO();
                        oCOM_DocumentoCompraDTO.FechaDocumento = Convert.ToDateTime(dr["FechaDocumento"] == null ? Convert.ToDateTime("01-01-2000") : Convert.ToDateTime(dr["FechaDocumento"].ToString()));
                        oCOM_DocumentoCompraDTO.SerieDocumento = $"{dr["SerieDocumento"].ToString()}-{dr["NumDocumento"].ToString()}";
                        oCOM_DocumentoCompraDTO.ProveedorRazon = dr["ProveedorRazon"].ToString();
                        oCOM_DocumentoCompraDTO.MonedaDesc = dr["MonedaDesc"].ToString();
                        oCOM_DocumentoCompraDTO.TotalNacional = Convert.ToDecimal(dr["TotalNacional"].ToString());
                        oResultDTO.ListaResultado[0].listaCompraDoc.Add(oCOM_DocumentoCompraDTO);
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
