using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers.Ventas
{
    public class NotaCreditoController : Controller
    {
        // GET: NotaCredito
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}