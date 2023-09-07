using ClosedXML.Excel;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaDermoSalud.View.Controllers
{
    public class Comision_MedicoController : Controller
    {
        // GET: Comision_Medico
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
            PersonalBL oPersonalBL = new PersonalBL();
            ResultDTO<PersonalDTO> oResultDTO = oPersonalBL.ListarTodo();
            string listaPersonal = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idPersonal", "NombreCompleto", "PorcentajeUtilidad" });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaPersonal);
        }
        public string ObtenerDatosMedico(int id, DateTime fechaIni, DateTime fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            PersonalBL oPersonalBL = new PersonalBL();

            ResultDTO<CitasDTO> oResultDTO = oCitasBL.ListarTodoMedicos(id, fechaIni, fechaFin);
            ResultDTO<PersonalDTO> oResult_Personal = oPersonalBL.ListarxID(id);

            string listaCargos = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idComisionMedico", "idCita", "FechaCita", "DescripcionServicio", "Codigo", "Costo", "Gasto", "Diferencia", "PorcentajeMedico", "Comision" });

            return String.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCargos, oResult_Personal?.ListaResultado?.FirstOrDefault()?.PorcentajeUtilidad.ToString());
        }
        public string ObteneComisionMedico(int id, DateTime fechaIni, DateTime fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            PersonalBL oPersonalBL = new PersonalBL();

            ResultDTO<CitasDTO> oResultDTO = oCitasBL.Listar_ComisionMedico(id, fechaIni, fechaFin);
            ResultDTO<PersonalDTO> oResult_Personal = oPersonalBL.ListarxID(id);

            string listaCargos = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idComisionMedico", "idServicio", "Servicio", "NroTratamiento", "Costo", "Gasto", "Diferencia", "PorcentajeMedico", "Comision" });

            return String.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCargos, oResult_Personal?.ListaResultado?.FirstOrDefault()?.PorcentajeUtilidad.ToString());
        }
        public string Grabar(CitasDTO oCitasDTO)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;

            oCitasDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            oCitasDTO.UsuarioModificacion = eSEGUsuario.idUsuario;

            CitasBL oCitasBL = new CitasBL();
            PersonalBL oPersonalBL = new PersonalBL();
            ResultDTO<CitasDTO> oResultDTO = oCitasBL.GrabarComisionMedico(oCitasDTO);
            string listaCitas = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idComisionMedico", "idServicio", "Servicio", "NroTratamiento", "Costo", "Gasto", "Diferencia", "PorcentajeMedico", "Comision" });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaCitas);
        }
        public ActionResult GetExcelFile(int id, DateTime fechaIni, DateTime fechaFin)
        {
            List<CitasDTO> oLista = new CitasBL().ListarTodoMedicos(id, fechaIni, fechaFin).ListaResultado;

            var gv = new GridView();
            gv.DataSource = oLista.Select(x => new
            {
                FechaCita = x.FechaCita.ToShortDateString(),
                Servicio = x.DescripcionServicio.ToUpper(),
                NumeroCita = x.Codigo,
                x.Costo,
                x.Gasto,
                x.Diferencia,
                x.PorcentajeMedico,
                x.Comision
            }).ToList();
            gv.DataBind();
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                for (int j = 0; j < gv.Rows[i].Cells.Count; j++)
                {
                    if (j >= 3)
                        gv.Rows[i].Cells[j].Style.Add("mso-number-format", @"0\.00");
                    else
                        gv.Rows[i].Cells[j].Style.Add("mso-number-format", @"\@");
                }
            }

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=ComisionPorMedico.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return View("Index");
        }
        //REPORTES
        public string ReporteGeneral(int id, DateTime fechaIni, DateTime fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            PersonalBL oPersonalBL = new PersonalBL();

            ResultDTO<CitasDTO> oResultDTO = oCitasBL.Listar_ComisionMedico(id, fechaIni, fechaFin);
            ResultDTO<PersonalDTO> oResult_Personal = oPersonalBL.ListarxID(id);

            string listarDetalle = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idComisionMedico", "idServicio", "Servicio", "NroTratamiento", "Costo", "Gasto", "Diferencia", "PorcentajeMedico", "Comision" });
            string listarPersonal = Serializador.rSerializado(oResult_Personal.ListaResultado, new string[] { "Nombres", "ApellidoP", "ApellidoM", "Documento" });

            return String.Format("{0}↔{1}↔{2}↔{3}", oResultDTO.Resultado, oResultDTO.MensajeError, listarDetalle, listarPersonal);
        }
        public string ReporteDetallado(int id, DateTime fechaIni, DateTime fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            PersonalBL oPersonalBL = new PersonalBL();

            ResultDTO<CitasDTO> oResultDTO = oCitasBL.Listar_ComisionMedico(id, fechaIni, fechaFin);
            ResultDTO<CitasDTO> oResultDTO_Detallado = oCitasBL.Listar_ComisionMedico_Detallado(id, fechaIni, fechaFin);
            ResultDTO<PersonalDTO> oResult_Personal = oPersonalBL.ListarxID(id);

            string listarComision_General = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idComisionMedico", "idServicio", "Servicio", "NroTratamiento", "Costo", "Gasto", "Diferencia", "PorcentajeMedico", "Comision" });
            string listarPersonal = Serializador.rSerializado(oResult_Personal.ListaResultado, new string[] { "Nombres", "ApellidoP", "ApellidoM", "Documento" });
            string listarComision_Detallado = Serializador.rSerializado(oResultDTO_Detallado.ListaResultado, new string[] { "FechaCita", "idServicio", "Servicio", "NombreCompleto", "Precio" });
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", oResultDTO.Resultado, oResultDTO.MensajeError, listarComision_General, listarPersonal, listarComision_Detallado);
        }
        public string armarTemp(string cad)
        {
            try
            {
                Session["listaCM"] = cad;
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public ActionResult ExportarExcel(string fechaIni, string fechaFin, string med)
        {
            try
            {
                string temp = Session["listaCM"].ToString();
                var lista = temp.Split('~');
                var workbook = new XLWorkbook();
                //string[] header = cabecera.Split(',');
                //foreach (var item in ld)
                //{
                //    var ldd = DetraccionBE.Where(obj => obj.Articulo == item);
                //}
                var worksheet = workbook.Worksheets.Add("Pagina1");

                //worksheet.Cell("N12").Value = DetraccionBE[0].StockInicial;
                int startx = 4;
                int starty = 0;
                //worksheet.Cell()
                //decimal ac = DetraccionBE[0].StockInicial;
                worksheet.Cell(1, 1).Value = "Doctor(a):";
                worksheet.Cell(1, 2).Value = med;
                worksheet.Cell(1, 2).Style.Font.Bold = true;
                worksheet.Cell("B3").Value = "SERVICIO";
                worksheet.Cell("C3").Value = "N°";
                worksheet.Cell("D3").Value = "COSTO";
                worksheet.Cell("E3").Value = "GASTOS";
                worksheet.Cell("F3").Value = "DIFERENCIA";
                worksheet.Cell("G3").Value = "PORCENTAJE";
                worksheet.Cell("H3").Value = "COMISION";
                //worksheet.Cell("D3:J3").Style.Font.Bold = true;
                worksheet.Range("B3", "H3").Style.Font.Bold = true;


                for (int i = 0; i < lista.Length - 1; i++)
                {
                    var obj = lista[i].Split('®');
                    for (int j = 0; j < obj.Length - 1; j++)
                    {
                        if (j == 0 || j == 1)
                        {
                           
                        }
                        else 
                        {
                            worksheet.Cell(i + startx, j + starty).Value = "'" + obj[j];
                        }                     
                    }
                }


                worksheet.Columns().AdjustToContents();
                worksheet.Columns().CellsUsed().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                worksheet.Columns().CellsUsed().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Comision Medico"+med+" Fecha" + fechaIni +
                                   " al " + fechaFin + ".xlsx");

                using (var memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                }
                Response.End();
                return View(Response);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

    }
}