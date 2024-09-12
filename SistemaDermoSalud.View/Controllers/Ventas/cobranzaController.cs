using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Business.Compras;
using SistemaDermoSalud.Business.Finanzas;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Compras;
using SistemaDermoSalud.Entities.Finanzas;
using SistemaDermoSalud.Helpers;

namespace SistemaDermoSalud.View.Controllers.Ventas
{
    public class cobranzaController : Controller
    {
        // GET: cobranza
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public string ObtenerDatosCobroDetalle(int id)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;

            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_PagoBL oFN_PagoBL = new FN_PagoBL();
            Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();
            ResultDTO<Ma_MonedaDTO> oListaMoneda = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            COM_PagaSocioBL oCOM_PagaSocioBL = new COM_PagaSocioBL();
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            string listaMoneda = Serializador.rSerializado(oListaMoneda.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            ResultDTO<AD_SocioNegocioDTO> oListaSocios = oAD_SocioNegocioBL.ListarProv(eSEGUsuario.idEmpresa, "C");
            ResultDTO<FN_PagosDetalle> oListaOrdenPago = oCOM_PagaSocioBL.ListarRangoFecha(eSEGUsuario.idEmpresa, fechaInicio, fechaFin);
            ResultDTO<FN_PagosDTO> oResultDTO = oFN_PagoBL.ListarPagos(id);
            //cadenas
            string listaFN_PagoBL = "";

            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaFN_PagoBL = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[]
                {"idPago", "FechaCreacion","RazonSocial","SerieDcto" ,"NumeroDcto" ,"SaldoxAplicar"}, false);
            }
            string listaSocios = Serializador.rSerializado(oListaSocios.ListaResultado, new string[] { "idSocioNegocio", "RazonSocial", "Documento" });
            string listaOrdenCompra = Serializador.rSerializado(oListaOrdenPago.ListaResultado, new string[]
            { "idPagoDetalle","FechaDetalle","SerieDcto","NumeroDcto", "DescripcionOperacion","DescripcionFormaPago","NumeroOperacion", "Monto"});
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}", "OK", listaOrdenCompra, fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"), listaSocios, listaMoneda, listaFN_PagoBL);
        }

        public string ObtenerDatosxIdDetalle(int IdDetalle)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();
            ResultDTO<Ma_MonedaDTO> oListaMoneda = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            DateTime fechaFin = DateTime.Today;
            COM_PagaSocioBL oCOM_PagaSocioBL = new COM_PagaSocioBL();
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            string listaMoneda = Serializador.rSerializado(oListaMoneda.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            ResultDTO<AD_SocioNegocioDTO> oListaSocios = oAD_SocioNegocioBL.ListarProv(eSEGUsuario.idEmpresa, "P");
            ResultDTO<FN_PagosDetalle> oListaPagoDetalle = oCOM_PagaSocioBL.ListarXIDPagoDetalle(IdDetalle);
            string listaSocios = Serializador.rSerializado(oListaSocios.ListaResultado, new string[] { "idSocioNegocio", "RazonSocial", "Documento" });
            string listaPagoDetalle = Serializador.rSerializado(oListaPagoDetalle.ListaResultado, new string[] { });
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}", "OK", listaPagoDetalle, fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"), listaSocios, listaMoneda);
        }
        public string ObtenerCuentasSocio(int id)
        {// recordar hacer un dao  y dto para las cuentas de los socios para jalarlo desde el boton model de pagar Socio y agregar el procedimiento
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            COM_PagaSocioBL oAD_SocioNegocioBL = new COM_PagaSocioBL();
            DateTime fechaFin = DateTime.Today;
            ResultDTO<COM_CuentasUsuariosDTO> oResultDTO = oAD_SocioNegocioBL.ListarTodoProveedores(id);
            string listaAD_SocioNegocio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[]
            {"idCuentaBancaria","DescripcionCuenta", "Cuenta"});
            return String.Format("{0}↔{1}↔{2}", "OK", fechaInicio.ToString("dd-MM-yyyy"), listaAD_SocioNegocio);
        }
        public string ObtenerCuentasSocioCombo(int id)
        {// recordar hacer un dao  y dto para las cuentas de los socios para jalarlo desde el boton model de pagar Socio y agregar el procedimiento
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            COM_PagaSocioBL oAD_SocioNegocioBL = new COM_PagaSocioBL();
            DateTime fechaFin = DateTime.Today;
            ResultDTO<COM_CuentasUsuariosDTO> oResultDTO = oAD_SocioNegocioBL.ListarTodoProveedores(id);
            string listaAD_SocioNegocio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[]
            {"idCuentaBancaria","Cuenta"});
            return String.Format("{0}↔{1}↔{2}", "OK", fechaInicio.ToString("dd-MM-yyyy"), listaAD_SocioNegocio);
        }
        public string ObtenerCuentasOrigen()
        {// recordar hacer un dao  y dto para las cuentas de los socios para jalarlo desde el boton model de pagar Socio y agregar el procedimiento
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            AD_CuentaOrigenBL oAD_SocioNegocioBL = new AD_CuentaOrigenBL();
            DateTime fechaFin = DateTime.Today;
            ResultDTO<AD_CuentaOrigenDTO> oResultDTO = oAD_SocioNegocioBL.ListarTodo();
            string listaAD_CuentaOrigen = Serializador.rSerializado(oResultDTO.ListaResultado, new string[]
            {"idCuentaOrigen",  "DescripcionBanco","NumeroCuenta","NombreCuenta"});
            return String.Format("{0}↔{1}↔{2}", "OK", fechaInicio.ToString("dd-MM-yyyy"), listaAD_CuentaOrigen);
        }

        public string ListarCobrosPendientes(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            FN_PagoBL oFN_PagoBL = new FN_PagoBL();
            //listas
            ResultDTO<FN_PagosDTO> oResultDTO = oFN_PagoBL.ListarPagosPendientes(id);
            //cadenas
            string listaFN_PagoBL = "";

            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaFN_PagoBL = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[]
                {"idPago", "FechaCreacion","RazonSocial","NumeroDcto" ,"SerieDcto" ,"SaldoxAplicar"}, false);
            }
            return String.Format("{0}↔{1}↔{2}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaFN_PagoBL);
        }
    }
}