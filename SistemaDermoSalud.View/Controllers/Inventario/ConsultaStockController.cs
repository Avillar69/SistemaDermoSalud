using SistemaDermoSalud.Business;
using SistemaDermoSalud.Business.Mantenimiento;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers.Inventario
{
    public class ConsultaStockController : Controller
    {
        // GET: ConsultaStock
        public ActionResult Index()
        {
            return PartialView();
        }
        public string ObtenerDatos()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_LocalBL oMa_LocalBL = new Ma_LocalBL();
            ResultDTO<Ma_LocalDTO> oResultDTO_Local = oMa_LocalBL.ListarTodo(eSEGUsuario.idEmpresa);
            string listaLocal = "";
            if (oResultDTO_Local.ListaResultado.Count != 0 && oResultDTO_Local.ListaResultado != null)
            {
                listaLocal = Serializador.Serializar(oResultDTO_Local.ListaResultado, '▲', '▼', new string[] { "idLocal", "Descripcion" }, false);
            }
            return listaLocal;
        }
        public string ObtenerAlmacen(int pL)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_LocalBL oMa_LocalBL = new Ma_LocalBL();
            ResultDTO<Ma_LocalDTO> oResultDTO_Local = oMa_LocalBL.ListarxID(pL);
            string listaAlmacen = "";
            if (oResultDTO_Local.ListaResultado[0].oListaAlmacen.Count != 0 && oResultDTO_Local.ListaResultado[0].oListaAlmacen != null)
            {
                listaAlmacen = Serializador.Serializar(oResultDTO_Local.ListaResultado[0].oListaAlmacen, '▲', '▼', new string[] { "idAlmacen", "Descripcion" }, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO_Local.Resultado, oResultDTO_Local.MensajeError, listaAlmacen);
        }
        public string ObtenerStockxAlmacen(int pA)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            INV_StockBL oINV_StockBL = new INV_StockBL();
            ResultDTO<INV_StockDTO> oResultDTO_Stock = oINV_StockBL.ListarStockxAlmacen(eSEGUsuario.idEmpresa, pA, eSEGUsuario.idUsuario);
            string listaStock = "";
            if (oResultDTO_Stock.ListaResultado.Count != 0 && oResultDTO_Stock.ListaResultado != null)
            {
                listaStock = Serializador.Serializar(oResultDTO_Stock.ListaResultado, '▲', '▼', new string[] 
                {"idStock","CodArticulo","descCategoria", "descArticulo", "Stock" }, false);
            }
            return String.Format("{0}↔{1}↔{2}", oResultDTO_Stock.Resultado, oResultDTO_Stock.MensajeError, listaStock);
        }

    }
}