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
    public class MedicamentoController : Controller
    {
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
            MedicamentoBL oMedicamentoBL = new MedicamentoBL();
            LaboratorioBL oLaboratorioBL = new LaboratorioBL();

            ResultDTO<MedicamentoDTO> oResultDTO = oMedicamentoBL.ListarTodo(1);
            ResultDTO<LaboratorioDTO> oResultLabDTO = oLaboratorioBL.ListarTodo(1);
            //ResultDTO<LaboratorioDTO> oResultLabDTO = oLaboratorio.
            string listaMedicamento = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idMedicamentos","Descripcion","Laboratorio","Estado"});
            string listaMed_Lab = Serializador.rSerializado(oResultLabDTO.ListaResultado, new string[] { "idLaboratorio","Laboratorio"});
            //if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            //{
            //     listaMedicamento= Serializador.Serializar(oResultDTO.ListaResultado,'▲', '▼', new string[] {},false);
            //}
            return String.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listaMed_Lab, listaMedicamento);
        }
        public string ObtenerDatosxID(int id)
        {
            MedicamentoBL oMedicamentoBL = new MedicamentoBL();
            ResultDTO<MedicamentoDTO> oResultDTO = oMedicamentoBL.ListarxID(id);
            string listaMedicamento = "";
            listaMedicamento = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            //if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            //{
                 
            //}
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError,listaMedicamento);
        }

        public string Grabar(MedicamentoDTO oMedicamentoDTO)
        {
            ResultDTO<MedicamentoDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            MedicamentoBL oMedicamentoBL = new MedicamentoBL();
            string listaMedicamento = "";
            if(oMedicamentoDTO.idMedicamentos == 0)
            {
                oMedicamentoDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oMedicamentoDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oMedicamentoBL.UpdateInsert(oMedicamentoDTO);
            
            List<MedicamentoDTO> lstMedicamentoDTO = oResultDTO.ListaResultado;
            listaMedicamento = Serializador.rSerializado(lstMedicamentoDTO, new string[] { "idMedicamentos", "Descripcion", "Laboratorio", "Estado" });
            //if (lstMedicamentoDTO != null && lstMedicamentoDTO.Count > 0)
            //{
                
            //}
            return string.Format("{0}↔{1}↔{2}",oResultDTO.Resultado, oResultDTO.MensajeError, listaMedicamento);
        }

         public string Eliminar(MedicamentoDTO oMedicamentoDTO)
        {
            ResultDTO<MedicamentoDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            MedicamentoBL oMedicamentoBL = new MedicamentoBL();
            string listaMedicamento = "";
            oResultDTO = oMedicamentoBL.Delete(oMedicamentoDTO);
            List<MedicamentoDTO> lstMedicamentoDTO = oResultDTO.ListaResultado;
            listaMedicamento = Serializador.rSerializado(lstMedicamentoDTO, new string[] { "idMedicamentos", "Descripcion", "Laboratorio", "Estado" });
            //if (lstMedicamentoDTO != null && lstMedicamentoDTO.Count > 0)
            //{
                
            //}
            return string.Format("{0}↔{1}↔{2}",oResultDTO.Resultado, oResultDTO.MensajeError, listaMedicamento);
        }
    }
}
