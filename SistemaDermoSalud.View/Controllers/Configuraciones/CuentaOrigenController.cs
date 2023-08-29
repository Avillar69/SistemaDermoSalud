using SistemaDermoSalud.Business;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers.Configuraciones
{
    public class CuentaOrigenController : Controller
    {
        // GET: CuentaOrigen
        public ActionResult Index()
        {
            return PartialView();
        }
        public string ObtenerDatos()
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;

            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;

            Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();
            AD_CuentaOrigenBL oAD_CuentaOrigenBL = new AD_CuentaOrigenBL();

            ResultDTO<Ma_MonedaDTO> oListaMoneda = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<AD_CuentaOrigenDTO> oListaCuentaBancaria = oAD_CuentaOrigenBL.ListarTodo();

            string listaMoneda = Serializador.rSerializado(oListaMoneda.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            string ListaCuentaBancaria = Serializador.rSerializado(oListaCuentaBancaria.ListaResultado, new string[]
            { "idCuentaOrigen", "NombreCuenta","DescripcionBanco","DescMoneda","NumeroCuenta","Estado"});
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", "OK", listaMoneda, ListaCuentaBancaria, fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"));
        }
        public string ObtenerPorFecha(string fechaInicio, string fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            AD_CuentaOrigenBL oAD_CuentaOrigenBL = new AD_CuentaOrigenBL();
            ResultDTO<AD_CuentaOrigenDTO> olistaCuentaOrigen = oAD_CuentaOrigenBL.ListarTodo();
            string listaOrdenCompra = Serializador.rSerializado(olistaCuentaOrigen.ListaResultado, new string[]
            { "idCuentaOrigen", "NombreCuenta","DescripcionBanco","DescMoneda","NumeroCuenta","Estado"});
            return String.Format("{0}↔{1}↔{2}", olistaCuentaOrigen.Resultado, olistaCuentaOrigen.MensajeError, listaOrdenCompra);
        }

        public string Grabar(AD_CuentaOrigenDTO olistaCuentaOrigen)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<AD_CuentaOrigenDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            AD_CuentaOrigenBL oAD_CuentaOrigenBL = new AD_CuentaOrigenBL();
            oResultDTO = oAD_CuentaOrigenBL.UpdateInsert(olistaCuentaOrigen, fechaInicio, fechaFin);


            List<AD_CuentaOrigenDTO> lstCOM_OrdenCompraDTO = oResultDTO.ListaResultado;
            string listaOrdenCompra = Serializador.rSerializado(lstCOM_OrdenCompraDTO, new string[]
            {"idCuentaOrigen", "NombreCuenta","DescripcionBanco","DescMoneda","NumeroCuenta","Estado" });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaOrdenCompra);
        }
        public string Eliminar(AD_CuentaOrigenDTO olistaCuentaOrigen)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<AD_CuentaOrigenDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            AD_CuentaOrigenBL oAD_CuentaOrigenBL = new AD_CuentaOrigenBL();
            oResultDTO = oAD_CuentaOrigenBL.Delete(olistaCuentaOrigen, fechaInicio, fechaFin);
            string lista_CuentaOrigen = Serializador.rSerializado(oResultDTO.ListaResultado, new string[]
            {"idCuentaOrigen", "NombreCuenta","DescripcionBanco","DescMoneda","NumeroCuenta","Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, lista_CuentaOrigen);
        }
        public string ObtenerDatosxID(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            AD_CuentaOrigenBL oAD_CuentaOrigenBL = new AD_CuentaOrigenBL();
            ResultDTO<AD_CuentaOrigenDTO> oListaCuentaOrigen = oAD_CuentaOrigenBL.ListarxID(id);
            string listaCuentaOrigen = Serializador.rSerializado(oListaCuentaOrigen.ListaResultado, new string[] { });

            return String.Format("{0}↔{1}", "OK", listaCuentaOrigen);
        }
    }
}