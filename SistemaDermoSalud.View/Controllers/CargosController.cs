using SistemaDermoSalud.Business;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers
{
    public class CargosController : Controller
    {
        // GET: Cargos
        public ActionResult Index()
        {
            return PartialView();
        }
        public string ObtenerDatos()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CargosBL oCargosBL = new CargosBL();
            ResultDTO<CargosDTO> oResultDTO = oCargosBL.ListarTodo(1);
            string listaCargos = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idCargo", "Descripcion", "FechaModificacion", "Estado" });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCargos);
        }
        public string ObtenerDatosxID(int id)
        {
            CargosBL oCargosBL = new CargosBL();
            ResultDTO<CargosDTO> oResultDTO = oCargosBL.ListarxID(id);
            string lstCargos = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, lstCargos);
        }

        public string Grabar(CargosDTO oCargosDTO)
        {
            ResultDTO<CargosDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CargosBL oCargosBL = new CargosBL();
            if (oCargosDTO.idCargo == 0)
            {
                oCargosDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oCargosDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oCargosBL.UpdateInsert(oCargosDTO);
            List<CargosDTO> lstServiciosDTO = oResultDTO.ListaResultado;
            string listaCargos = Serializador.rSerializado(lstServiciosDTO, new string[] { "idCargo", "Descripcion", "FechaModificacion", "Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCargos);
        }

        public string Eliminar(CargosDTO oCargosDTO)
        {
            ResultDTO<CargosDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CargosBL oCargosBL = new CargosBL();
            oResultDTO = oCargosBL.Delete(oCargosDTO);
            List<CargosDTO> lstCargosDTO = oResultDTO.ListaResultado;
            string listaServicios = Serializador.rSerializado(lstCargosDTO, new string[] { "idCargo", "Descripcion", "FechaModificacion", "Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaServicios);
        }
    }
}