using SistemaDermoSalud.Business.Finanzas;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Finanzas;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers.Finanzas
{
    public class TipoCambioController : Controller
    {
        // GET: TipoCambio
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }

        public string ObtenerDatos()
        {

            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;

            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_TipoCambioBL oFN_TipoCambioBL = new FN_TipoCambioBL();
            ResultDTO<Ma_MonedaDTO> oListaMoneda = oFN_TipoCambioBL.ListarMonedaTipoCambio(eSEGUsuario.idEmpresa);
            ResultDTO<FN_TipoCambioDTO> oListaTipoCambio = oFN_TipoCambioBL.ListarTodo(eSEGUsuario.idEmpresa, fechaInicio.ToShortDateString(), fechaFin.ToShortDateString());
            string listaMoneda = Serializador.rSerializado(oListaMoneda.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            string listaTipoCambio = Serializador.rSerializado(oListaTipoCambio.ListaResultado, new string[] {"idTipoCambio","Fecha","idMoneda","DescripcionMoneda", "ValorCompra",
                   "ValorVenta"});

            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", "OK", listaMoneda, listaTipoCambio, fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"));
        }
        public string ObtenerDatosxFecha(string fI, string fF)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_TipoCambioBL oFN_TipoCambioBL = new FN_TipoCambioBL();
            ResultDTO<FN_TipoCambioDTO> oListaTipoCambio = oFN_TipoCambioBL.ListarTodo(eSEGUsuario.idEmpresa, fI, fF);
            string listaTipoCambio = Serializador.rSerializado(oListaTipoCambio.ListaResultado, new string[] {"idTipoCambio","Fecha","idMoneda","DescripcionMoneda", "ValorCompra",
                   "ValorVenta"});

            return String.Format("{0}↔{1}↔{2}", "OK", oListaTipoCambio.MensajeError, listaTipoCambio);
        }
        public string Grabar(FN_TipoCambioDTO oFN_TipoCambioDTO)
        {
            ResultDTO<FN_TipoCambioDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_TipoCambioBL oFN_TipoCambioBL = new FN_TipoCambioBL();
            if (oFN_TipoCambioDTO.idTipoCambio == 0)
            {
                oFN_TipoCambioDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oFN_TipoCambioDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oFN_TipoCambioDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oResultDTO = oFN_TipoCambioBL.UpdateInsert(oFN_TipoCambioDTO);
            string listaTipoCambio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] {"idTipoCambio","Fecha","idMoneda","DescripcionMoneda", "ValorCompra",
                   "ValorVenta"});
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaTipoCambio);
        }
        public string Eliminar(FN_TipoCambioDTO oFN_TipoCambioDTO)
        {
            ResultDTO<FN_TipoCambioDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_TipoCambioBL oFN_TipoCambioBL = new FN_TipoCambioBL();
            oFN_TipoCambioDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oResultDTO = oFN_TipoCambioBL.Delete(oFN_TipoCambioDTO);
            string listaTipoCambio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] {"idTipoCambio","Fecha","idMoneda","DescripcionMoneda", "ValorCompra",
                   "ValorVenta"});
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaTipoCambio);
        }
        public string ObtenerDatosTipoCambio(string fecha)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_TipoCambioBL oFN_TipoCambioBL = new FN_TipoCambioBL();
            ResultDTO<FN_TipoCambioDTO> oListaTipoCambio = oFN_TipoCambioBL.ListarTipoXFechaUnica(fecha);


            string listaTipoCambio = Serializador.rSerializado(oListaTipoCambio.ListaResultado, new string[]
            {"idTipoCambio","Fecha","idMoneda","DescripcionMoneda", "ValorCompra","ValorVenta"});
            return String.Format("{0}↔{1}↔{2}", "OK", oListaTipoCambio.MensajeError, listaTipoCambio);
        }
        public string ListarTipoXFechaActual(string fecha)
        {
            FN_TipoCambioBL oFN_TipoCambioBL = new FN_TipoCambioBL();
            ResultDTO<FN_TipoCambioDTO> oListaTipoCambio = oFN_TipoCambioBL.ListarTipoXFechaActual(fecha);
            string listaTipoCambio = Serializador.rSerializado(oListaTipoCambio.ListaResultado, new string[]
            {"idTipoCambio","Fecha","idMoneda","DescripcionMoneda", "ValorCompra","ValorVenta"});
            return String.Format("{0}↔{1}↔{2}", "OK", oListaTipoCambio.MensajeError, listaTipoCambio);
        }

    }
}