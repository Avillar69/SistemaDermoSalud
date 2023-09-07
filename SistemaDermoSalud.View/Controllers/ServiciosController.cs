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
    public class ServiciosController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public ActionResult PrecioServicios()
        {
            return PartialView();
        }


        //SERVICIOS
        public string ObtenerDatos()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            ServiciosBL oServiciosBL = new ServiciosBL();
            ResultDTO<ServiciosDTO> oResultDTO = oServiciosBL.ListarTodo();

           TipoServicioBL TServiciosBL = new TipoServicioBL();
            ResultDTO<TipoServicioDTO> oListaTipoServicio = TServiciosBL.ListarTipoServicio();
            string listaTipoServicios = Serializador.rSerializado(oListaTipoServicio.ListaResultado, new string[] { "idTipoServicio", "NombreTipoServicio", "Estado" });
            string listaServicios = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idServicio","Codigo","NombreServicio","Estado"});

            return String.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado,oResultDTO.MensajeError,listaServicios,listaTipoServicios);
        }
        public string ObtenerDatosxID(int id)
        {
            ServiciosBL oServiciosBL = new ServiciosBL();
            ResultDTO<ServiciosDTO> oResultDTO = oServiciosBL.ListarxID(id);
            string listaServicios = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            return String.Format("{0}↔{1}↔{2}",oResultDTO.Resultado,oResultDTO.MensajeError,listaServicios);
        }
        public string Grabar(ServiciosDTO oServiciosDTO)
        {
            ResultDTO<ServiciosDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            ServiciosBL oServiciosBL = new ServiciosBL();
            if(oServiciosDTO.idServicio == 0)
            {
                oServiciosDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oServiciosDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oServiciosBL.UpdateInsert(oServiciosDTO);
            List<ServiciosDTO> lstServiciosDTO = oResultDTO.ListaResultado;
            string listaServicios = Serializador.rSerializado(lstServiciosDTO, new string[] { "idServicio", "Codigo", "NombreServicio", "Estado" });
            return string.Format("{0}↔{1}↔{2}",oResultDTO.Resultado, oResultDTO.MensajeError, listaServicios);
        }
        public string Eliminar(ServiciosDTO oServiciosDTO)
        {
            ResultDTO<ServiciosDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            ServiciosBL oServiciosBL = new ServiciosBL();
            oResultDTO = oServiciosBL.Delete(oServiciosDTO);
            List<ServiciosDTO> lstServiciosDTO = oResultDTO.ListaResultado;
            string listaServicios = Serializador.rSerializado(lstServiciosDTO, new string[] { "idServicio", "Codigo", "NombreServicio", "Estado" });
            return string.Format("{0}↔{1}↔{2}",oResultDTO.Resultado, oResultDTO.MensajeError, listaServicios);
        }

        //PRECIO SERVICIOS
        public string ObtenerDatosPrecioServicios()
        {
            ServiciosBL oServiciosBL = new ServiciosBL();
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            ResultDTO<ServiciosDTO> oResultDTO = oServiciosBL.ListarTodo();
            string listaServicio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idServicio", "Codigo", "NombreServicio", "Precio" });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaServicio);
        }
        public string GrabarPrecio(ServiciosDTO oServiciosDTO)
        {
            ResultDTO<ServiciosDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            ServiciosBL oServiciosBL = new ServiciosBL();
            oServiciosDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oServiciosBL.UpdateInsertPrecio(oServiciosDTO);
            List<ServiciosDTO> lstServiciosDTO = oResultDTO.ListaResultado;
            string listaServicios = Serializador.rSerializado(lstServiciosDTO, new string[] { "idServicio", "Codigo", "NombreServicio", "Precio" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaServicios);
        }
    }
}
