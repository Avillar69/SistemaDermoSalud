using SistemaDermoSalud.Business;
using SistemaDermoSalud.Business.Mantenimiento;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers.Inventario
{
    public class OperacionesStockController : Controller
    {
        // GET: OperacionesStock
        public ActionResult Entrada()
        {
            return PartialView();
        }
        public ActionResult Salida()
        {
            return PartialView();
        }
        public ActionResult TransferenciaSalida()
        {
            return PartialView();
        }
        public ActionResult TransferenciaEntrada()
        {
            return PartialView();
        }
        public string ObtenerDatos(string Tipo)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            ALM_MovimientoBL oALM_MovimientoBL = new ALM_MovimientoBL();
            Ma_AlmacenBL oMa_AlmacenBL = new Ma_AlmacenBL();
            Ma_TipoMovimientoBL oMa_TipoMovimientoBL = new Ma_TipoMovimientoBL();
            Ma_EstadoBL oMa_EstadoBL = new Ma_EstadoBL();
            AD_GuiaRemisionBL oAD_GuiaRemisionBL = new AD_GuiaRemisionBL();
            Ma_LocalBL oMa_LocalBL = new Ma_LocalBL();

            string Fecha = DateTime.Now.ToString().Substring(0, 11);
            ResultDTO<ALM_MovimientoDTO> oResultDTO = oALM_MovimientoBL.ListarxTipo(Tipo, eSEGUsuario.idEmpresa);
            ResultDTO<Ma_AlmacenDTO> oResultDTO_Almacen = oMa_AlmacenBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<Ma_TipoMovimientoDTO> oResultDTO_TipoMovimiento = oMa_TipoMovimientoBL.ListarxTipo(Tipo);
            ResultDTO<Ma_EstadoDTO> oResultDTO_Estado = oMa_EstadoBL.ListarxModulo("INVENTARIO");
            ResultDTO<Ma_LocalDTO> oResultDTO_Local = oMa_LocalBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<AD_GuiaRemisionDTO> oListaGuiaRemision = oAD_GuiaRemisionBL.ListarRangoFecha(eSEGUsuario.idEmpresa, fechaInicio, fechaFin);
            string listaALM_Movimiento = "";
            string listaLocal = "";
            string listaAlmacen = "";
            string listaTipoMovimiento = "";
            string listaEstado = "";
            if (!Tipo.Contains("TRANSFERENCIA"))
            {
                if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
                {
                    listaALM_Movimiento = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                    "idMovimiento", "DesLocal","Observaciones", "FechaModificacion", "DesEstado"}, false);
                }
            }
            else
            {
                if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
                {
                    listaALM_Movimiento = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                    "idMovimiento", "DesLocal", "DesAlmacenDestino","Observaciones","FechaModificacion","DesEstado"}, false);
                }
                if (oResultDTO_Almacen.ListaResultado != null && oResultDTO_Almacen.ListaResultado.Count > 0)
                {
                    listaAlmacen = Serializador.Serializar(oResultDTO_Almacen.ListaResultado, '▲', '▼', new string[] { "idAlmacen", "Descripcion" }, false);
                }
            }
            if (oResultDTO_Local.ListaResultado != null && oResultDTO_Local.ListaResultado.Count > 0)
            {
                listaLocal = Serializador.Serializar(oResultDTO_Local.ListaResultado, '▲', '▼', new string[] { "idLocal", "Descripcion" }, false);
            }
            if (oResultDTO_TipoMovimiento.ListaResultado != null && oResultDTO_TipoMovimiento.ListaResultado.Count > 0)
            {
                listaTipoMovimiento = Serializador.Serializar(oResultDTO_TipoMovimiento.ListaResultado, '▲', '▼', new string[] { "idTipoMovimiento", "Descripcion" }, false);
            }
            if (oResultDTO_Estado.ListaResultado != null && oResultDTO_Estado.ListaResultado.Count > 0)
            {
                listaEstado = Serializador.Serializar(oResultDTO_Estado.ListaResultado, '▲', '▼', new string[] { }, false);
            }
            string listaGuiaRemision = Serializador.rSerializado(oListaGuiaRemision.ListaResultado, new string[] { "idGuiaRemision", "SerieGuia", "NumeroGuia", "RazonSocial", "FechaInicioTraslado" });
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}↔{7}↔{8}↔{9}", oResultDTO.Resultado, oResultDTO.MensajeError, listaALM_Movimiento, listaLocal, listaAlmacen, listaTipoMovimiento, listaEstado, Fecha, listaGuiaRemision, fechaInicio);
        }
        public string ObtenerDatosxID(int id)
        {
            ALM_MovimientoBL oALM_MovimientoBL = new ALM_MovimientoBL();
            ResultDTO<ALM_MovimientoDTO> oResultDTO = oALM_MovimientoBL.ListarxID(id);
            string listaALM_Movimiento = "";
            string listaAlmacen = "";
            string listaALM_MovDetalle = "";
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaALM_Movimiento = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { }, false);
                listaAlmacen = cargarAlmacenes(oResultDTO.ListaResultado[0].idLocal);
                listaALM_MovDetalle = Serializador.Serializar(oResultDTO.ListaResultado[0].oListaDetalle, '▲', '▼', new string[]
                { "idMovimientoDetalle", "idArticulo", "DesArticulo","Laboratorio", "Cantidad", "Precio" }, false);

            }

            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", oResultDTO.Resultado, oResultDTO.MensajeError, listaALM_Movimiento, listaAlmacen, listaALM_MovDetalle);
        }
        public string ObtenerPorFecha(string Tipo, string fechaInicio, string fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            ALM_MovimientoBL oALM_MovimientoBL = new ALM_MovimientoBL();
            ResultDTO<ALM_MovimientoDTO> oALM_MovimientoDTO = oALM_MovimientoBL.ListarRangoFecha(Tipo, eSEGUsuario.idEmpresa, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin));
            string listaAlmacen = Serializador.rSerializado(oALM_MovimientoDTO.ListaResultado, new string[]
            { "idMovimiento", "DesLocal","Observaciones", "FechaModificacion", "DesEstado" });
            return String.Format("{0}↔{1}↔{2}", oALM_MovimientoDTO.Resultado, oALM_MovimientoDTO.MensajeError, listaAlmacen);
        }
        public string Grabar(ALM_MovimientoDTO oALM_MovimientoDTO)
        {
            ResultDTO<ALM_MovimientoDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            ALM_MovimientoBL oALM_MovimientoBL = new ALM_MovimientoBL();
            string listaALM_Movimiento = "";
            if (oALM_MovimientoDTO.idMovimiento == 0)
            {
                oALM_MovimientoDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oALM_MovimientoDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oALM_MovimientoBL.UpdateInsert(oALM_MovimientoDTO);
            List<ALM_MovimientoDTO> lstALM_MovimientoDTO = oResultDTO.ListaResultado;
            if (!oALM_MovimientoDTO.TipoMovimiento.Contains("TRANSFERENCIA"))
            {
                if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
                {
                    listaALM_Movimiento = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                     "idMovimiento", "DesLocal","Observaciones", "FechaModificacion", "DesEstado"}, false);
                }
            }
            else
            {
                if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
                {
                    listaALM_Movimiento = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                    "idMovimiento", "DesLocal", "DesAlmacenOrigen", "DesAlmacenDestino","Observaciones", "FechaMovimiento", "FechaMovimientoDestino", "DesEstado"}, false);
                }
            }
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaALM_Movimiento);
        }
        public string Eliminar(ALM_MovimientoDTO oALM_MovimientoDTO)
        {
            ResultDTO<ALM_MovimientoDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            ALM_MovimientoBL oALM_MovimientoBL = new ALM_MovimientoBL();
            string listaALM_Movimiento = "";
            oResultDTO = oALM_MovimientoBL.Delete(oALM_MovimientoDTO);
            List<ALM_MovimientoDTO> lstALM_MovimientoDTO = oResultDTO.ListaResultado;
            if (!oALM_MovimientoDTO.TipoMovimiento.Contains("TRANSFERENCIA"))
            {
                if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
                {
                    listaALM_Movimiento = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                    "idMovimiento", "DesLocal","Observaciones", "FechaModificacion", "DesEstado"}, false);
                }
            }
            else
            {
                if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
                {
                    listaALM_Movimiento = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                    "idMovimiento", "DesLocal", "DesAlmacenOrigen", "DesAlmacenDestino","Observaciones", "FechaMovimiento", "FechaMovimientoDestino", "DesEstado"}, false);
                }
            }
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaALM_Movimiento);
        }
        public string cargarAlmacenes(int idLocal)
        {
            string rpta = "";
            Ma_LocalBL oMa_LocalBL = new Ma_LocalBL();
            ResultDTO<Ma_LocalDTO> oResultDTO = new ResultDTO<Ma_LocalDTO>();
            oResultDTO = oMa_LocalBL.ListarxID(idLocal);
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                if (oResultDTO.ListaResultado[0].oListaAlmacen != null && oResultDTO.ListaResultado[0].oListaAlmacen.Count > 0)
                {
                    rpta = Serializador.Serializar(oResultDTO.ListaResultado[0].oListaAlmacen, '▲', '▼', new string[] {
                    "idAlmacen", "Descripcion"}, false);
                }
            }
            return rpta;
        }
        public string cargarMedicamento()//cargar medicamentos
        {
            string rpta = "";
            string listaMedicamento = "";
            MedicamentoBL oMedicamentoBL = new MedicamentoBL();
            ResultDTO<MedicamentoDTO> oResultDTO = new ResultDTO<MedicamentoDTO>();
            oResultDTO = oMedicamentoBL.ListarTodo(1);
            listaMedicamento = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idMedicamentos", "CodigoMedicamento", "Descripcion", "Laboratorio", "PagoMedicamento" });
            rpta = String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMedicamento);
            return rpta;
        }
        public string cargarMedicamentoxLaboratorio(int idLab)//cargar medicamentos
        {
            string rpta = "";
            string listaMedicamento = "";
            MedicamentoBL oMedicamentoBL = new MedicamentoBL();
            ResultDTO<MedicamentoDTO> oResultDTO = new ResultDTO<MedicamentoDTO>();
            oResultDTO = oMedicamentoBL.ListarporLaboratorio(idLab);
            listaMedicamento = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idMedicamentos", "Descripcion" });
            rpta = String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMedicamento);
            return rpta;
        }
        public string cargarTransferenciaSalida(int idLocal, int idAlmacen)
        {
            string rpta = "";
            string listaTransferencia = "";
            string listaDetalle = "";
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            ALM_MovimientoBL oALM_MovimientoBL = new ALM_MovimientoBL();
            List<Object> oLista = oALM_MovimientoBL.ListarTransferenciaSalida(eSEGUsuario.idEmpresa, idLocal, idAlmacen);
            //ResultDTO<ALM_MovimientoDTO> oResultDTO = oALM_MovimientoBL.ListarTransferenciaSalida(eSEGUsuario.idEmpresa, idLocal, idAlmacen);
            if (oLista != null && oLista.Count == 2)
            {
                ResultDTO<ALM_MovimientoDTO> oResultDTO = (ResultDTO<ALM_MovimientoDTO>)oLista[0];
                ResultDTO<ALM_MovimientoDetalleDTO> oResultDTO_Detalle = (ResultDTO<ALM_MovimientoDetalleDTO>)oLista[1];
                if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
                {
                    listaTransferencia = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                    "idMovimiento", "idAlmacenOrigen", "DesAlmacenOrigen","Observaciones", "FechaMovimiento", "idEstado", "DesEstado"}, false);
                }
                if (oResultDTO_Detalle.ListaResultado != null && oResultDTO_Detalle.ListaResultado.Count > 0)
                {
                    listaDetalle = Serializador.Serializar(oResultDTO_Detalle.ListaResultado, '▲', '▼', new string[] {
                    "idMovimiento", "idMovimientoDetalle", "idArticulo", "DesArticulo","DesMarca", "DesUnidadMedida", "Cantidad"}, false);
                }
            }
            rpta = string.Format("{0}↔{1}", listaTransferencia, listaDetalle);
            return rpta;
        }
        public string cargarStock(int idMedicamento, int idAlmacenO = 0, int idAlmacenD = 0)
        {
            string rpta = "";
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            INV_StockBL oINV_StockBL = new INV_StockBL();
            rpta = oINV_StockBL.ItemStock(eSEGUsuario.idEmpresa, idMedicamento, idAlmacenO, idAlmacenD);
            return rpta;
        }
        public string cargarCompras()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            COM_DocumentoCompraBL oCOM_DocumentoCompraBL = new COM_DocumentoCompraBL();
            ResultDTO<COM_DocumentoCompraDTO> oCOM_DocumentoCompraDTO = oCOM_DocumentoCompraBL.cargarCompras(eSEGUsuario.idEmpresa);
            string listaCompras = Serializador.rSerializado(oCOM_DocumentoCompraDTO.ListaResultado, new string[]
            { "idDocumentoCompra", "FechaDocumento", "NumDocumento", "ProveedorRazon", "TotalNacional" });
            return String.Format("{0}↔{1}↔{2}", oCOM_DocumentoCompraDTO.Resultado, oCOM_DocumentoCompraDTO.MensajeError, listaCompras);
        }
        public string cargarDetalleCompras(int idCompras)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            COM_DocumentoCompraBL oCOM_DocumentoCompraBL = new COM_DocumentoCompraBL();
            ResultDTO<COM_DocumentoCompraDetalleDTO> oCOM_DocumentoCompraDetalleDTO = oCOM_DocumentoCompraBL.cargarDetalleCompras(idCompras);
            string listaCompras = Serializador.rSerializado(oCOM_DocumentoCompraDetalleDTO.ListaResultado, new string[] { "idArticulo", "descripcionArticulo", "Marca", "UnidadMedida", "Cantidad", "PrecioNacional", "idDocumentoCompraDetalle" });
            return String.Format("{0}↔{1}↔{2}", oCOM_DocumentoCompraDetalleDTO.Resultado, oCOM_DocumentoCompraDetalleDTO.MensajeError, listaCompras);
        }
        public string cargarVentas()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
            ResultDTO<VEN_DocumentoVentaDTO> oVEN_DocumentoVentaDTO = oVN_DocumentoVentaBL.cargarVentas(eSEGUsuario.idEmpresa);
            string listaVentas = Serializador.rSerializado(oVEN_DocumentoVentaDTO.ListaResultado, new string[] { "idDocumentoVenta", "FechaDocumento", "NumDocumento", "ClienteRazon", "TotalNacional" });
            return String.Format("{0}↔{1}↔{2}", oVEN_DocumentoVentaDTO.Resultado, oVEN_DocumentoVentaDTO.MensajeError, listaVentas);
        }
        public string cargarDetalleVentas(int idVentas)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
            ResultDTO<VEN_DocumentoVentaDetalleDTO> oVEN_DocumentoVentaDetalleDTO = oVN_DocumentoVentaBL.cargarDetalleVentas(idVentas);
            string listaCompras = Serializador.rSerializado(oVEN_DocumentoVentaDetalleDTO.ListaResultado, new string[] { "idArticulo", "DescripcionArticulo", "Marca", "UnidadMedida", "Cantidad", "PrecioNacional", "idDocumentoVentaDetalle" });
            return String.Format("{0}↔{1}↔{2}", oVEN_DocumentoVentaDetalleDTO.Resultado, oVEN_DocumentoVentaDetalleDTO.MensajeError, listaCompras);
        }
        public string cargarGuias()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            AD_GuiaRemisionBL oAD_GuiaRemisionBL = new AD_GuiaRemisionBL();
            ResultDTO<AD_GuiaRemisionDTO> oAD_GuiaRemisionDTO = oAD_GuiaRemisionBL.cargarGuias(eSEGUsuario.idEmpresa);
            string listaGuias = Serializador.rSerializado(oAD_GuiaRemisionDTO.ListaResultado, new string[] { "idGuiaRemision", "FechaCreacion", "NumeroGuia", "RazonSocial" });
            return String.Format("{0}↔{1}↔{2}", oAD_GuiaRemisionDTO.Resultado, oAD_GuiaRemisionDTO.MensajeError, listaGuias);
        }
        public string cargarDetalleGuias(int idGuias)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            AD_GuiaRemisionBL oAD_GuiaRemisionBL = new AD_GuiaRemisionBL();
            ResultDTO<AD_GuiaRemisionDetalleDTO> oAD_GuiaRemisionDetalleDTO = oAD_GuiaRemisionBL.cargarDetalleGuias(idGuias);
            string listaGuias = Serializador.rSerializado(oAD_GuiaRemisionDetalleDTO.ListaResultado, new string[] { "idArticulo", "DescripcionArticulo", "Marca", "descUnidadMedida", "Cantidad", "idGuiaRemisionDetalle" });
            return String.Format("{0}↔{1}↔{2}", oAD_GuiaRemisionDetalleDTO.Resultado, oAD_GuiaRemisionDetalleDTO.MensajeError, listaGuias);
        }
    }
}