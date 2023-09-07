using ClosedXML.Excel;
using SistemaDermoSalud.Business.Inventario;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Inventario;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers.Inventario
{
    public class KardexController : Controller
    {
        // GET: Kardex
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public string BuscarKardex(string fI, string fF, int iC, int iA)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            KardexBL oKardexBL = new KardexBL();
            ResultDTO<KardexDTO> oResult = oKardexBL.ListarRangoFecha(eSEGUsuario.idEmpresa, Convert.ToDateTime(fI), Convert.ToDateTime(fF), iC, iA);
            Session["listaTemporal"] = oResult.ListaResultado;
            string lista = Serializador.rSerializado(oResult.ListaResultado, new string[] { });
            return String.Format("{0}↔{1}↔{2}", oResult.Resultado, oResult.MensajeError, lista);
        }


        public ActionResult exportarExcel(string local, string fechaInicio, string fechaFin, string cabecera)
        {
            try
            {
                string sistema = (string)Session["Sistema"];
                List<KardexDTO> DetraccionBE = (List<KardexDTO>)Session["listaTemporal"];

                var ld = DetraccionBE.Select(o => o.Articulo).Distinct().ToList();
                var workbook = new XLWorkbook();
                string[] header = cabecera.Split(',');
                int conti = 0;
                foreach (var item in ld)
                {
                    conti++;
                    var ldd = DetraccionBE.Where(obj => obj.Articulo == item).ToList();
                    //string title = ldd[0].Articulo.Length <= 30 ? ldd[0].Articulo : ldd[0].Articulo.Substring(0, 30);
                    var worksheet = workbook.Worksheets.Add("Pagina " + conti);
                    worksheet.Range("A14", "D15").Merge();
                    worksheet.Range("A1", "J1").Merge();
                    worksheet.Range("A3", "F3").Merge();
                    worksheet.Range("A4", "F4").Merge();
                    worksheet.Range("A5", "F5").Merge();
                    worksheet.Range("A6", "F6").Merge();
                    worksheet.Range("A7", "F7").Merge();
                    worksheet.Range("A8", "F8").Merge();
                    worksheet.Range("A9", "F9").Merge();
                    worksheet.Range("A10", "F10").Merge();
                    worksheet.Range("A11", "F11").Merge();
                    worksheet.Cell(1, 1).Value = "FORMATO 13.1: \"REGISTRO DE INVENTARIO PERMANENTE VALORIZADO - DETALLE DEL INVENTARIO VALORIZADO\"";
                    worksheet.Cell(3, 1).Value = "PERÍODO: 2021";
                    worksheet.Cell(4, 1).Value = "RUC: 20565643143";
                    worksheet.Cell(5, 1).Value = "APELLIDOS Y NOMBRES, DENOMINACIÓN O RAZÓN SOCIAL: DERMOSALUD SAC";
                    worksheet.Cell(6, 1).Value = "ESTABLECIMIENTO (1): 1";
                    worksheet.Cell(7, 1).Value = "CÓDIGO DE LA EXISTENCIA:";
                    worksheet.Cell(8, 1).Value = "TIPO (TABLA 5): 01";
                    worksheet.Cell(9, 1).Value = "DESCRIPCIÓN: " + ldd[0].Articulo;
                    worksheet.Cell(10, 1).Value = "CÓDIGO DE LA UNIDAD DE MEDIDA (TABLA 6): 07";
                    worksheet.Cell(11, 1).Value = "MÉTODO DE VALUACIÓN: PROMEDIO";
                    worksheet.Range("A14", "D14").Merge();
                    worksheet.Range("A15", "D15").Merge();
                    worksheet.Cell("A14").Value = "DOCUMENTO DE TRASLADO, COMPROBANTE DE PAGO";
                    worksheet.Cell("A15").Value = "DOCUMENTO INTERNO O SIMILAR";
                    worksheet.Cell("A16").Value = "FECHA";
                    worksheet.Cell("B16").Value = "TIPO (TABLA 10)";
                    worksheet.Cell("C16").Value = "SERIE";
                    worksheet.Cell("D16").Value = "NÚMERO";
                    worksheet.Cell("E14").Value = "TIPO DE";
                    worksheet.Cell("E15").Value = "OPERACIÓN";
                    worksheet.Cell("E16").Value = "(TABLA 12)";
                    worksheet.Range("F14", "H14").Merge();
                    worksheet.Range("I14", "K14").Merge();
                    worksheet.Range("L14", "N14").Merge();
                    worksheet.Range("L12", "M12").Merge();
                    worksheet.Cell("F14").Value = "ENTRADAS";
                    worksheet.Cell("I14").Value = "SALIDAS";
                    worksheet.Cell("L14").Value = "SALDO FINAL";
                    worksheet.Cell("F15").Value = "CANTIDAD";
                    worksheet.Cell("G15").Value = "COSTO UNITARIO";
                    worksheet.Cell("H15").Value = "COSTO TOTAL";
                    worksheet.Cell("I15").Value = "CANTIDAD";
                    worksheet.Cell("J15").Value = "COSTO UNITARIO";
                    worksheet.Cell("K15").Value = "COSTO TOTAL";
                    worksheet.Cell("L15").Value = "CANTIDAD";
                    worksheet.Cell("M15").Value = "COSTO UNITARIO";
                    worksheet.Cell("N15").Value = "COSTO TOTAL";
                    worksheet.Cell("L12").Value = "STOCK INICIAL";

                    worksheet.Cell("N12").Value = ldd[0].StockInicial;
                    int startx = 17;
                    //for (int j = 0; j < header.Length; j++)
                    //{
                    //    worksheet.Cell(1, j + 1).Value = header[j];
                    //    worksheet.Cell(1, j + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 201, 201, 201);
                    //    worksheet.Cell(1, j + 1).Style.Font.Bold = true;
                    //}
                    decimal ac = ldd[0].StockInicial;
                    for (int i = 0; i < ldd.Count; i++)
                    {
                        string valor = "";
                        worksheet.Cell(i + startx, 1).Value = "'" + ldd[i].FechaMovimiento.Date;
                        worksheet.Cell(i + startx, 2).Value = "'" + (ldd[i].DocReferencia.Contains("-") ? ldd[i].DocReferencia.Split('-')[0] : " ");
                        valor = Convert.ToString(worksheet.Cell(i + startx, 2).Value);
                        if (valor.Length > 0)
                        {
                            worksheet.Cell(i + startx, 3).Value = "'" + (ldd[i].DocReferencia.Contains("-") ? ldd[i].DocReferencia.Split('-')[1] : " ");
                            worksheet.Cell(i + startx, 4).Value = "'" + (ldd[i].DocReferencia.Contains("-") ? ldd[i].DocReferencia.Split('-')[2] : " ");
                        }
                        else
                        {


                            worksheet.Cell(i + startx, 3).Value = "'" + "-";
                            worksheet.Cell(i + startx, 4).Value = "'" + "-";
                        }

                        worksheet.Cell(i + startx, 5).Value = "";
                        worksheet.Cell(i + startx, 6).Value = "'" + ldd[i].CantidadEntrada;
                        worksheet.Cell(i + startx, 7).Value = "'" + ldd[i].PrecioEntrada;
                        worksheet.Cell(i + startx, 8).Value = ldd[i].TotalEntrada;
                        worksheet.Cell(i + startx, 9).Value = ldd[i].CantidadSalida;
                        worksheet.Cell(i + startx, 10).Value = ldd[i].PrecioSalida;
                        worksheet.Cell(i + startx, 11).Value = ldd[i].TotalSalida;
                        ac = ac + ldd[i].CantidadEntrada - ldd[i].CantidadSalida;
                        worksheet.Cell(i + startx, 12).Value = ac;
                        worksheet.Cell(i + startx, 13).Value = ldd[i].PrecioSalida + ldd[i].PrecioEntrada;
                        worksheet.Cell(i + startx, 14).Value = ac * (ldd[i].PrecioSalida + ldd[i].PrecioEntrada);
                    }
                    worksheet.Columns().AdjustToContents();
                    worksheet.Columns().CellsUsed().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Columns().CellsUsed().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("A3").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("A4").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("A5").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("A6").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("A7").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("A8").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("A9").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("A10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("A11").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                }

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Kardex Valorizado del " + fechaInicio +
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