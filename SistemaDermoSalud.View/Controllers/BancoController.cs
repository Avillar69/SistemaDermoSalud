using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Helpers;

namespace SistemaDermoSalud.View.Controllers
{
    public class BancoController : Controller
    {
        // GET: Banco
        public ActionResult Index()
        {
            return View();
        }
        public string ListarBancos()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            Ma_BancoBL oMa_BancoBL = new Ma_BancoBL();
            //listas
            ResultDTO<Ma_BancoDTO> oResultDTO = oMa_BancoBL.ListaBancos();
            //cadenas
            string listaMa_BancoBL = "";

            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaMa_BancoBL = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
               "idBanco","CodigoGenerado","Descripcion"}, false);
            }
            return String.Format("{0}↔{1}↔{2}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaMa_BancoBL);
        }
    }
}