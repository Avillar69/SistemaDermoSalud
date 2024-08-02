using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Business.Mantenimiento;
using SistemaDermoSalud.Helpers;

namespace SistemaDermoSalud.View.Controllers
{
    public class MarcaController : Controller
    {
        //
        // GET: /Marca/
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
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_MarcaBL oMarcaBL = new Ma_MarcaBL();
            ResultDTO <Ma_MarcaDTO> oResultLabDTO = oMarcaBL.ListarTodo(1);
            string ListaMarca = Serializador.rSerializado(oResultLabDTO.ListaResultado, new string[]{"idMarca","Marca","FechaCreacion","Estado"});
            return String.Format("{0}↔{1}↔{2}", oResultLabDTO.Resultado,oResultLabDTO.MensajeError,ListaMarca);
        }
        public string ObtenerDatosxID(int id)
        {
            Ma_MarcaBL oMarcaBL = new Ma_MarcaBL();
            ResultDTO<Ma_MarcaDTO> oResultDTO = oMarcaBL.ListarxID(id);
            string listaMarca = "";
            listaMarca = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMarca);
        }
        public string Grabar(Ma_MarcaDTO oMarcaDTO)
        {
            ResultDTO<Ma_MarcaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_MarcaBL oMarcaBL = new Ma_MarcaBL();
            string listaMarca = "";
            if (oMarcaDTO.idMarca == 0)
            {
                oMarcaDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oMarcaDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oMarcaBL.UpdateInsert(oMarcaDTO);

            List<Ma_MarcaDTO> lstMarcaDTO = oResultDTO.ListaResultado;
            listaMarca = Serializador.rSerializado(lstMarcaDTO, new string[] { "idMarca", "Marca", "FechaCreacion", "Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMarca);
        }
        public string Eliminar(Ma_MarcaDTO oMarcaDTO)
        {
            ResultDTO<Ma_MarcaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_MarcaBL oMarcaBL = new Ma_MarcaBL();
            string listaMarca = "";
            oResultDTO = oMarcaBL.Delete(oMarcaDTO);
            List<Ma_MarcaDTO> lstMarcaDTO = oResultDTO.ListaResultado;
            listaMarca = Serializador.rSerializado(lstMarcaDTO, new string[] { "idMarca", "Marca", "FechaCreacion", "Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMarca);
        }
	}
}