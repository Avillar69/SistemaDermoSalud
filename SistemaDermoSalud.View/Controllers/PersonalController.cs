using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Helpers;

namespace SistemaDermoSalud.Controllers
{
    public class PersonalController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public ActionResult PorcUtilidad()
        {
            return PartialView();
        }
        public string ObtenerDatos()
        {
            CargosBL oCargosBL = new CargosBL();
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            PersonalBL oPersonalBL = new PersonalBL();
            ResultDTO<PersonalDTO> oResultDTO = oPersonalBL.ListarTodo();
            ResultDTO<CargosDTO> oResultCargosDTO = oCargosBL.ListarTodo(1);
            string listaPersonal = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idPersonal", "Documento", "NombreCompleto", "FechaIngreso", "Estado" });
            string listaCargos = Serializador.rSerializado(oResultCargosDTO.ListaResultado, new string[] { "idCargo", "Descripcion" });
            return String.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listaPersonal, listaCargos);
        }
        public string ObtenerDatosxID(int id)
        {
            PersonalBL oPersonalBL = new PersonalBL();
            ResultDTO<PersonalDTO> oResultDTO = oPersonalBL.ListarxID(id);
            string listaPersonal = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            string image = oResultDTO.ListaResultado.FirstOrDefault().Img;
            return String.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listaPersonal, image);
        }

        public string Grabar(PersonalDTO oPersonalDTO)
        {
            ResultDTO<PersonalDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            PersonalBL oPersonalBL = new PersonalBL();
            if (oPersonalDTO.idPersonal == 0)
            {
                oPersonalDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oPersonalDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oPersonalBL.UpdateInsert(oPersonalDTO);
            List<PersonalDTO> lstPersonalDTO = oResultDTO.ListaResultado;
            string listaPersonal = Serializador.rSerializado(lstPersonalDTO, new string[] { "idPersonal", "FechaIngreso", "Nombres", "ApellidoP", "Estado" });
            //if (lstPersonalDTO != null && lstPersonalDTO.Count > 0)
            //{
            //    listaPersonal = Serializador.Serializar(lstPersonalDTO,'▲', '▼', new string[] {},false);
            //}
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaPersonal);
        }

        public string Eliminar(PersonalDTO oPersonalDTO)
        {
            ResultDTO<PersonalDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            PersonalBL oPersonalBL = new PersonalBL();
            oResultDTO = oPersonalBL.Delete(oPersonalDTO);
            string listaPersonal = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idPersonal", "FechaIngreso", "Nombres", "ApellidoP", "Estado" });
            //List<PersonalDTO> lstPersonalDTO = oResultDTO.ListaResultado;
            //if (lstPersonalDTO != null && lstPersonalDTO.Count > 0)
            //{
            //    listaPersonal = Serializador.Serializar(lstPersonalDTO,'▲', '▼', new string[] {},false);
            //}
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaPersonal);
        }

        //metodos para el porcentaje personal nueva vista 
        public string ObtenerDatosPorcentaje(PersonalDTO oPersonalDTO)
        {
            CargosBL oCargosBL = new CargosBL();
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            PersonalBL oPersonalBL = new PersonalBL();
            ResultDTO<PersonalDTO> oResultDTO = oPersonalBL.UpdPorcentaje(oPersonalDTO);
            ResultDTO<CargosDTO> oResultCargosDTO = oCargosBL.ListarTodo(1);
            string listaPersonal = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idPersonal", "Documento", "NombreCompleto", "PorcentajeUtilidad" });
            string listaCargos = Serializador.rSerializado(oResultCargosDTO.ListaResultado, new string[] { "idCargo", "Descripcion" });
            return String.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listaPersonal, listaCargos);
        }
        public string ObtenerDatosPorc()
        {
            CargosBL oCargosBL = new CargosBL();
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            PersonalBL oPersonalBL = new PersonalBL();
            ResultDTO<PersonalDTO> oResultDTO = oPersonalBL.ListarTodo();
            ResultDTO<CargosDTO> oResultCargosDTO = oCargosBL.ListarTodo(1);
            string listaPersonal = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idPersonal", "Documento", "NombreCompleto", "PorcentajeUtilidad" });
            string listaCargos = Serializador.rSerializado(oResultCargosDTO.ListaResultado, new string[] { "idCargo", "Descripcion" });
            return String.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listaPersonal, listaCargos);
        }
    }
}
