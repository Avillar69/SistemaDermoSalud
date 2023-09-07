using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Helpers;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Entities.Ventas;
using SistemaDermoSalud.Entities.Nubefact;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Business.Mantenimiento;
using SistemaDermoSalud.Business.Ventas;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace SistemaDermoSalud.View.Controllers.Ventas
{
    public class CuentaCobrarController : Controller
    {
        // GET: CuentaCobrar
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }


        //NOTA CREDITO

        ResultDTO<VEN_NotaCreditoDTO> oResultDTO_NotaCredito;
        string rptaGrabar = "";
        int flgIgv = 0;
        double montoIgv = 0;

        public const string ruta = "https://www.pse.pe/api/v1/ec4e92d3ee8042158a14cabd9fb61822dd4637e4113a4c0db4b5efd550341ba8";
        public const string token = "eyJhbGciOiJIUzI1NiJ9.IjljYzlhMDMyNmY0MzQ4ODRiMDhlNDBiMzA0Y2U5YmM4NDkzZTAyOGI5NmQyNDVlNzg5MWZmZmVhYmNhOTMzMjEi.GEdJALYumdH5zn4ib6GOeQspPo9R5JwWkEoIFaobWnQ";

        public ActionResult IndexNotCredito()
        {
            return PartialView();
        }



        public string ObtenerDatos()
        {
            try
            {
                DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DateTime fechaFin = DateTime.Today;
                Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;

                VEN_NotaCreditoBL oVEN_NotaCreditoBL = new VEN_NotaCreditoBL();
                Ma_MotivoBL oMa_MotivoBL = new Ma_MotivoBL();
                Ma_TipoAfectacionBL oMa_TipoAfectacionBL = new Ma_TipoAfectacionBL();
                Ma_Clase_ArticuloBL oMa_Clase_ArticuloBL = new Ma_Clase_ArticuloBL();
                Ma_TipoComprobanteBL oMa_TipoComprobanteBL = new Ma_TipoComprobanteBL();
                Ma_ArticuloBL oMa_ArticuloBL = new Ma_ArticuloBL();
                Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();

                ResultDTO<VEN_NotaCreditoDTO> oResult_Nota_CreditoDebitoDTO = oVEN_NotaCreditoBL.ListarRangoFechas(1, fechaInicio, fechaFin, "CREDITO", "VENTA");
                ResultDTO<Ma_MotivoDTO> oResult_Ma_MotivoDTO = oMa_MotivoBL.ListarTodo();
                ResultDTO<Ma_TipoAfectacionDTO> oListaTipoAfectacion = oMa_TipoAfectacionBL.ListarTodo();
                ResultDTO<Ma_Clase_ArticuloDTO> oListaClaseArticulo = oMa_Clase_ArticuloBL.ListarTodo(1, "A");
                ResultDTO<Ma_TipoComprobanteDTO> oListaComprobantes = oMa_TipoComprobanteBL.ListarTodo();
                ResultDTO<Ma_ArticuloDTO> oListaArticulo = oMa_ArticuloBL.ListarTodo(1, "A");
                ResultDTO<Ma_MonedaDTO> oListaMoneda = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);

                string listaNotaCredito = Serializador.rSerializado(oResult_Nota_CreditoDebitoDTO.ListaResultado, new string[] {
                    "idNotaCredito", "FechaEmision", "Numero", "Motivo","NumDocRef", "Cliente", "Total","Enlace", "EstadoSunat" });
                string listaMotivo = Serializador.rSerializado(oResult_Ma_MotivoDTO.ListaResultado, new string[] { "idMotivo", "Descripcion" });
                string numeroNotaCredito = oVEN_NotaCreditoBL.ObtenerNumero("FC01", "CREDITO").ListaResultado.FirstOrDefault().NroSerie;
                string listaTipoAfectacion = Serializador.rSerializado(oListaTipoAfectacion.ListaResultado, new string[] { "idTipoAfectacion", "Descripcion" });
                string listaTipoCompra = Serializador.rSerializado(oListaClaseArticulo.ListaResultado, new string[] { "idClaseArticulo", "CodigoGenerado", "Descripcion" });
                string listaComprobantes = Serializador.rSerializado(oListaComprobantes.ListaResultado, new string[] { "idTipoComprobante", "Descripcion", "CodigoSunat" });
                string listaArticulos = Serializador.rSerializado(oListaArticulo.ListaResultado, new string[] { "CodigoAutogenerado", "Descripcion" });
                string listaMoneda = Serializador.rSerializado(oListaMoneda.ListaResultado, new string[] { "idMoneda", "Descripcion" });

                return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}↔{7}↔{8}↔{9}↔{10}↔{11}", oResult_Nota_CreditoDebitoDTO.Resultado, oResult_Nota_CreditoDebitoDTO.MensajeError,
                    listaNotaCredito, listaMotivo, numeroNotaCredito, fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"),
                    listaTipoAfectacion, listaTipoCompra, listaComprobantes, listaArticulos, listaMoneda);
            }
            catch (Exception ex)
            {
                return String.Format("{0}↔{1}", "Error", ex.Message);
            }
        }
        public string ObtenerDatosxID(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            VEN_NotaCreditoBL oVEN_NotaCreditoBL = new VEN_NotaCreditoBL();
            ResultDTO<VEN_NotaCreditoDTO> oListaNotaCredito = oVEN_NotaCreditoBL.ListarxID(id);
            string listaNotaCredito = Serializador.rSerializado(oListaNotaCredito.ListaResultado, new string[] { });
            string listaNotaCreditoDetalle = Serializador.rSerializado(oListaNotaCredito.ListaResultado.FirstOrDefault().oListaDetalle, new string[] { });

            //VEN_NotaCreditoDTO oVEN_NotaCreditoDTO = oListaNotaCredito.ListaResultado.FirstOrDefault();
            //if (!string.IsNullOrWhiteSpace(oVEN_NotaCreditoDTO.JsonDocFisico))
            //    listaNotaCredito = listaNotaCredito.Replace(oVEN_NotaCreditoDTO.JsonDocFisico.ToUpper(), oVEN_NotaCreditoDTO.JsonDocFisico);

            return String.Format("{0}↔{1}↔{2}", "OK", listaNotaCredito, listaNotaCreditoDetalle);
        }
        public string ObtenerDocVenta(int ic)
        {
            try
            {
                VEN_NotaCreditoBL oVEN_NotaCreditoBL = new VEN_NotaCreditoBL();
                ResultDTO<VEN_DocVentaDTO> oResult_DocumentoVentaDTO = oVEN_NotaCreditoBL.ListarDocVentaxCli(ic);
                string listaDocVenta = Serializador.rSerializado(oResult_DocumentoVentaDTO.ListaResultado, new string[] {
                    "DocumentoVentaID", "FechaDocumento", "ClienteRazon", "SerieDocumento", "NumDocumento" });

                return String.Format("{0}↔{1}↔{2}", oResult_DocumentoVentaDTO.Resultado, oResult_DocumentoVentaDTO.MensajeError, listaDocVenta);
            }
            catch (Exception ex)
            {
                return String.Format("{0}↔{1}", "Error", ex.Message);
            }
        }
        public string ObtenerNumeroNotaCredito(string serie)
        {
            try
            {
                VEN_NotaCreditoBL oVEN_NotaCreditoBL = new VEN_NotaCreditoBL();
                ResultDTO<VEN_SerieDTO> oResultDTO = oVEN_NotaCreditoBL.ObtenerNumero(serie, "CREDITO");

                return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, oResultDTO.ListaResultado.FirstOrDefault().NroSerie);
            }
            catch (Exception ex)
            {
                return String.Format("{0}↔{1}", "Error", ex.Message);
            }
        }
        public string ObtenerPorFecha(string fechaInicio, string fechaFin)
        {
            try
            {
                DateTime fecIni = new DateTime(Convert.ToInt32(fechaInicio.Split('-')[2]), Convert.ToInt32(fechaInicio.Split('-')[1]), Convert.ToInt32(fechaInicio.Split('-')[0]));
                DateTime fecFin = new DateTime(Convert.ToInt32(fechaFin.Split('-')[2]), Convert.ToInt32(fechaFin.Split('-')[1]), Convert.ToInt32(fechaFin.Split('-')[0]));

                Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
                VEN_NotaCreditoBL oVEN_NotaCreditoBL = new VEN_NotaCreditoBL();
                ResultDTO<VEN_NotaCreditoDTO> oResult_Nota_CreditoDebitoDTO = oVEN_NotaCreditoBL.ListarRangoFechas(1, fecIni, fecFin, "CREDITO", "VENTA");

                string listaNotaCredito = Serializador.rSerializado(oResult_Nota_CreditoDebitoDTO.ListaResultado, new string[] {
                      "idNotaCredito", "FechaEmision", "Numero", "Motivo","NumDocRef", "Cliente", "Total","Enlace", "EstadoSunat"});

                return String.Format("{0}↔{1}↔{2}", oResult_Nota_CreditoDebitoDTO.Resultado, oResult_Nota_CreditoDebitoDTO.MensajeError, listaNotaCredito);
            }
            catch (Exception ex)
            {
                return String.Format("{0}↔{1}", "Error", ex.Message);
            }
        }
        public string ObtenerSeriexTipoDocumento(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            VEN_DocVentaBL oVEN_DocumentoVentaBL = new VEN_DocVentaBL();
            ResultDTO<VEN_SerieDTO> oListaSerieDoc = oVEN_DocumentoVentaBL.ListarSeriexTipoDocumento(id);
            string listaSeries = Serializador.rSerializado(oListaSerieDoc.ListaResultado, new string[] { "idSerie", "NroSerie" });
            return String.Format("{0}↔{1}", "OK", listaSeries);
        }


        public string Grabar(VEN_NotaCreditoDTO oVEN_NotaCreditoDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<VEN_NotaCreditoDTO> oResultDTO;

            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VEN_NotaCreditoBL oVEN_NotaCreditoBL = new VEN_NotaCreditoBL();
            string oResultDTO2 = oVEN_NotaCreditoBL.ValidarNotaCredito(oVEN_NotaCreditoDTO);
            string validandoElDocumento = "";
            validandoElDocumento = oResultDTO2;
            if (validandoElDocumento == "" || oVEN_NotaCreditoDTO.idNotaCredito != 0)
            {
                if (oVEN_NotaCreditoDTO.idTipoVenta == 5)
                {
                    oVEN_NotaCreditoDTO.ClienteTipoDocumento = 0;
                    oVEN_NotaCreditoDTO.idOperacion = 2;
                }
                else
                {
                    oVEN_NotaCreditoDTO.idOperacion = 2;
                    if (oVEN_NotaCreditoDTO.ClienteDocumento.Length == 8) { oVEN_NotaCreditoDTO.ClienteTipoDocumento = 1; }
                    if (oVEN_NotaCreditoDTO.ClienteDocumento.Length == 11) { oVEN_NotaCreditoDTO.ClienteTipoDocumento = 6; }
                }
                if (oVEN_NotaCreditoDTO.flgIGV == true) { montoIgv = 1.18; } else { montoIgv = 0.18; }
                oVEN_NotaCreditoDTO.TipoDoc = "VENTA";
                oVEN_NotaCreditoDTO.idEmpresa = Convert.ToInt32(eSEGUsuario.idEmpresa);
                oVEN_NotaCreditoDTO.idTipoDocumento = 3;
                oVEN_NotaCreditoDTO.UsuarioCreacion = eSEGUsuario.idUsuario.ToString();
                oVEN_NotaCreditoDTO.UsuarioModificacion = eSEGUsuario.idUsuario.ToString();
                //---comentar nota credito electronica
                Invoice objNubefact = CrearObjetoNubefact(oVEN_NotaCreditoDTO);
                string json = JsonConvert.SerializeObject(objNubefact, Formatting.Indented);
                Console.WriteLine(json);
                byte[] bytes = Encoding.Default.GetBytes(json);
                string json_en_utf_8 = Encoding.UTF8.GetString(bytes);
                ////Envío de datos a nubefact

                if (!oVEN_NotaCreditoDTO.Serie.Contains("TK"))
                {
                    string json_de_respuesta = SendJson(ruta, json_en_utf_8, token);
                    dynamic r = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
                    string r2 = JsonConvert.SerializeObject(r, Formatting.Indented);
                    dynamic json_r_in = JsonConvert.DeserializeObject<Respuesta>(r2);
                    dynamic leer_respuesta = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
                    if (leer_respuesta.errors == null)
                    {
                        string rpta = "";
                        rpta += "TIPO: " + leer_respuesta.tipo;
                        rpta += "SERIE: " + leer_respuesta.serie;
                        rpta += "NUMERO: " + leer_respuesta.numero;
                        rpta += "URL: " + leer_respuesta.url;
                        rpta += "ACEPTADA_POR_SUNAT: " + leer_respuesta.aceptada_por_sunat;
                        rpta += "DESCRIPCION SUNAT: " + leer_respuesta.sunat_description;
                        rpta += "NOTA SUNAT: " + leer_respuesta.sunat_note;
                        rpta += "CODIGO RESPUESTA SUNAT: " + leer_respuesta.sunat_responsecode;
                        rpta += "SUNAT ERROR SOAP: " + leer_respuesta.sunat_soap_error;
                        rpta += "PDF EN BASE64: " + leer_respuesta.pdf_zip_base64;
                        rpta += "XML EN BASE64: " + leer_respuesta.xml_zip_base64;
                        rpta += "CDR EN BASE64: " + leer_respuesta.cdr_zip_base64;
                        rpta += "CODIGO QR: " + leer_respuesta.cadena_para_codigo_qr;
                        rpta += "CODIGO HASH: " + leer_respuesta.codigo_hash;
                        rpta += "CODIGO DE BARRAS: " + leer_respuesta.codigo_de_barras;
                        rpta += "ERRORES: " + leer_respuesta.errors;
                        rpta += "ENLACES: " + leer_respuesta.enlace;
                        oVEN_NotaCreditoDTO.Enlace = leer_respuesta.enlace + ".pdf";
                        oVEN_NotaCreditoDTO.Aceptada_Sunat = leer_respuesta.aceptada_por_sunat;
                        if (oVEN_NotaCreditoDTO.Aceptada_Sunat == true)
                        {
                            oVEN_NotaCreditoDTO.EstadoSunat = "A";
                        }
                        else
                        {
                            oVEN_NotaCreditoDTO.EstadoSunat = "E";
                        }
                        oVEN_NotaCreditoDTO.Descripcion_Sunat = leer_respuesta.sunat_description;
                        oVEN_NotaCreditoDTO.PDF_Base64 = leer_respuesta.pdf_zip_base64;
                        oVEN_NotaCreditoDTO.XML_Base64 = leer_respuesta.xml_zip_base64;
                        oVEN_NotaCreditoDTO.CDR_Base64 = leer_respuesta.cdr_zip_base64;
                        oResultDTO_NotaCredito = oVEN_NotaCreditoBL.UpdateInsert(oVEN_NotaCreditoDTO, fechaInicio, fechaFin);
                    }
                    else
                    {
                        rptaGrabar += leer_respuesta.errors;
                    }
                }
                else
                {
                    oResultDTO_NotaCredito = oVEN_NotaCreditoBL.UpdateInsert(oVEN_NotaCreditoDTO, fechaInicio, fechaFin);//no se comenta
                }

                if (oResultDTO_NotaCredito != null)
                {
                    string listaNotaCredito = Serializador.rSerializado(oResultDTO_NotaCredito.ListaResultado, new string[]
                    { "idNotaCredito", "FechaEmision", "Numero", "Motivo","NumDocRef", "Cliente", "Total","Enlace", "EstadoSunat"  });

                    List<VEN_NotaCreditoDTO> listaImpresion = oResultDTO_NotaCredito.ListaResultado.OrderBy(y => y.Serie).OrderBy(x => x.CodigoSunat).ToList();
                    string listaDocImpresion = Serializador.rSerializado(listaImpresion, new string[]
                    {"FechaDocumento", "CodigoSunat", "SerieNumDoc", "ClienteDocumento", "ClienteRazon", "MonedaDesc", "TipoCambio", "SubTotalDolares", "SubTotalSoles", "Inafecto", "IGVNacional", "Total"});

                    //ResultDTO<VEN_DocumentoVenta_ReportePorCliente> oListaReportePorCliente = oVN_DocumentoVentaBL.ReportePorCliente(fechaInicio, fechaFin);
                    //string listaDocImpresionPorCliente = Serializador.rSerializado(oListaReportePorCliente.ListaResultado, new string[] { });

                    return string.Format("{0}↔{1}↔{2}↔{3}↔{4}", oResultDTO_NotaCredito.Resultado, oResultDTO_NotaCredito.MensajeError, listaNotaCredito, listaDocImpresion, oVEN_NotaCreditoDTO.Descripcion_Sunat);//, listaDocImpresionPorCliente);
                }
                else
                {
                    return rptaGrabar;
                }
            }
            else
            {
                return string.Format("{0}↔{1}↔{2}", "REPITE", "Ya existe un proveedor que tiene ese numero y serie", "no se guardo nada");
            }
        }
        public string Anular(VEN_NotaCreditoDTO oVEN_NotaCreditoDTO)
        {

            VEN_NotaCreditoBL oVEN_NotaCreditoBL = new VEN_NotaCreditoBL();
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<VEN_DocVentaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            int validar_NotaCredito = oVEN_NotaCreditoBL.validar_NotaCredito(oVEN_NotaCreditoDTO.idNotaCredito);
            if (validar_NotaCredito == 0)
            {
                VEN_NotaCreditoDTO oNotaCredito = oVEN_NotaCreditoBL.ListarxIDNota(oVEN_NotaCreditoDTO.idNotaCredito);
                Invoice objNubefact = CrearObjetoNubefact_Anular(oNotaCredito);
                string json = JsonConvert.SerializeObject(objNubefact, Formatting.Indented);
                Console.WriteLine(json);
                byte[] bytes = Encoding.Default.GetBytes(json);
                string json_en_utf_8 = Encoding.UTF8.GetString(bytes);

                if (!oNotaCredito.Serie.Contains("TK"))
                {
                    //Envío de datos a nubefact
                    string json_de_respuesta = SendJson(ruta, json_en_utf_8, token);
                    dynamic r = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
                    string r2 = JsonConvert.SerializeObject(r, Formatting.Indented);
                    dynamic json_r_in = JsonConvert.DeserializeObject<Respuesta>(r2);
                    dynamic leer_respuesta = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
                    if (leer_respuesta.errors == null)
                    {
                        string rpta = "";
                        rpta += "TIPO: " + leer_respuesta.tipo;
                        rpta += "SERIE: " + leer_respuesta.serie;
                        rpta += "NUMERO: " + leer_respuesta.numero;
                        rpta += "URL: " + leer_respuesta.url;
                        rpta += "ACEPTADA_POR_SUNAT: " + leer_respuesta.aceptada_por_sunat;
                        rpta += "DESCRIPCION SUNAT: " + leer_respuesta.sunat_description;
                        rpta += "NOTA SUNAT: " + leer_respuesta.sunat_note;
                        rpta += "CODIGO RESPUESTA SUNAT: " + leer_respuesta.sunat_responsecode;
                        rpta += "SUNAT ERROR SOAP: " + leer_respuesta.sunat_soap_error;
                        rpta += "PDF EN BASE64: " + leer_respuesta.pdf_zip_base64;
                        rpta += "XML EN BASE64: " + leer_respuesta.xml_zip_base64;
                        rpta += "CDR EN BASE64: " + leer_respuesta.cdr_zip_base64;
                        rpta += "CODIGO QR: " + leer_respuesta.cadena_para_codigo_qr;
                        rpta += "CODIGO HASH: " + leer_respuesta.codigo_hash;
                        rpta += "CODIGO DE BARRAS: " + leer_respuesta.codigo_de_barras;
                        rpta += "ERRORES: " + leer_respuesta.errors;

                        oResultDTO_NotaCredito = oVEN_NotaCreditoBL.Anular(oVEN_NotaCreditoDTO, fechaInicio, fechaFin);
                    }
                    else
                    {
                        rptaGrabar += leer_respuesta.errors;
                    }
                }
                else
                {
                    oResultDTO_NotaCredito = oVEN_NotaCreditoBL.Anular(oVEN_NotaCreditoDTO, fechaInicio, fechaFin);
                }

                if (oResultDTO_NotaCredito != null)
                {
                    string listaNotaCredito = Serializador.rSerializado(oResultDTO_NotaCredito.ListaResultado, new string[] {
                    "idNotaCredito", "FechaEmision", "Numero", "Motivo","NumDocRef", "Cliente", "Total","Enlace", "EstadoSunat" });
                    return string.Format("{0}↔{1}↔{2}", oResultDTO_NotaCredito.Resultado, oResultDTO_NotaCredito.MensajeError, listaNotaCredito);
                }
                else
                {
                    return rptaGrabar;
                }
            }
            else
            {
                return string.Format("{0}↔{1}↔{2}", "NOTA CREDITO", "La Nota de Credito se encuentra bloqueado", "no se podra anular");
            }
        }
        #region Nubefact
        public Invoice CrearObjetoNubefact(VEN_NotaCreditoDTO objNotaCredito)
        {
            Invoice invoice = new Invoice();
            invoice.operacion = "generar_comprobante";
            invoice.tipo_de_comprobante = objNotaCredito.idTipoDocumento;
            invoice.serie = objNotaCredito.Serie;
            invoice.numero = Convert.ToInt32(objNotaCredito.Numero);
            invoice.sunat_transaction = objNotaCredito.idOperacion;
            invoice.cliente_tipo_de_documento = objNotaCredito.ClienteTipoDocumento;
            invoice.cliente_numero_de_documento = objNotaCredito.ClienteDocumento;
            invoice.cliente_denominacion = objNotaCredito.ClienteRazon;
            invoice.cliente_direccion = objNotaCredito.ClienteDireccion;
            invoice.cliente_email = "provesur.reciclemos@gmail.com";
            invoice.cliente_email_1 = "";
            invoice.cliente_email_2 = "";
            invoice.fecha_de_emision = objNotaCredito.FechaEmision;
            invoice.fecha_de_vencimiento = DateTime.Now.AddDays(10);
            invoice.moneda = objNotaCredito.idMoneda;
            invoice.tipo_de_cambio = objNotaCredito.TipoCambio;
            invoice.porcentaje_de_igv = 18.00;
            invoice.descuento_global = objNotaCredito.TotalDescuentoNacional;
            invoice.total_descuento = objNotaCredito.TotalDescuentoNacional; //"";
            invoice.total_anticipo = "";
            invoice.total_gravada = objNotaCredito.TotalGravada;
            invoice.total_inafecta = objNotaCredito.TotalInafecto;
            invoice.total_exonerada = objNotaCredito.TotalExonerado;
            invoice.total_igv = Convert.ToDouble(objNotaCredito.IGVNacional);
            invoice.total_gratuita = objNotaCredito.TotalGratuito; //"";
            invoice.total_otros_cargos = objNotaCredito.TotalOtrosCargos; //"";
            invoice.total = Convert.ToDouble(objNotaCredito.Total);
            invoice.percepcion_tipo = "";
            invoice.percepcion_base_imponible = "";
            invoice.total_percepcion = "";
            invoice.detraccion = false;
            invoice.observaciones = objNotaCredito.Descripcion;
            invoice.documento_que_se_modifica_tipo = objNotaCredito.idTipoDocumentoRef;
            invoice.documento_que_se_modifica_serie = objNotaCredito.NumDocRef.Split('-')[0];
            invoice.documento_que_se_modifica_numero = objNotaCredito.NumDocRef.Split('-')[1];
            invoice.tipo_de_nota_de_credito = objNotaCredito.idMotivo;
            invoice.tipo_de_nota_de_debito = "";
            invoice.enviar_automaticamente_a_la_sunat = true;
            invoice.enviar_automaticamente_al_cliente = true;
            invoice.codigo_unico = "";
            invoice.condiciones_de_pago = objNotaCredito.FormaPago;
            invoice.medio_de_pago = objNotaCredito.TipoPago;
            invoice.placa_vehiculo = "";
            invoice.orden_compra_servicio = "";
            invoice.tabla_personalizada_codigo = "";
            invoice.formato_de_pdf = "A4";
            List<Items> oListaItems = new List<Items>();
            string[] listaDets = objNotaCredito.cadDetalle.Split('¯');
            for (int i = 0; i < listaDets.Length - 1; i++)
            {
                string[] det = listaDets[i].Split('|');
                Items item = new Items();
                item.unidad_de_medida = objNotaCredito.Unidad_Medida;
                item.codigo = det[2];
                item.codigo_producto_sunat = "";
                item.descripcion = det[5];
                item.cantidad = Convert.ToDouble(det[3]);

                switch (objNotaCredito.idTipoVenta)
                {
                    case 2:
                        item.valor_unitario = Convert.ToDouble(det[6]);
                        item.precio_unitario = Convert.ToDouble(det[6]) * 1.18;
                        item.subtotal = item.valor_unitario;
                        item.igv = item.cantidad * item.valor_unitario * 0.18 * (1 - (Convert.ToDouble(objNotaCredito.PorcDescuento) / 100));
                        break;
                    case 5:
                        item.valor_unitario = Convert.ToDouble(det[6]);
                        item.subtotal = item.cantidad * item.valor_unitario;
                        item.precio_unitario = Convert.ToDouble(det[6]);
                        item.igv = 0.00;
                        break;
                    default:
                        //if (objNotaCredito.flgIGV == true)
                        //{
                        //    item.valor_unitario = Convert.ToDouble(det[6]) / 1.18;
                        //    item.subtotal = item.cantidad * item.valor_unitario;
                        //    item.precio_unitario = Convert.ToDouble(det[6]); 
                        //    item.igv = item.cantidad * item.valor_unitario * 0.18;
                        //}
                        //else
                        //{
                        item.valor_unitario = Convert.ToDouble(det[6]);//150
                        item.precio_unitario = Convert.ToDouble(det[6]) * 1.18;
                        item.subtotal = item.cantidad * item.valor_unitario * (1 - (Convert.ToDouble(objNotaCredito.PorcDescuento) / 100));
                        item.igv = item.cantidad * item.valor_unitario * 0.18 * (1 - (Convert.ToDouble(objNotaCredito.PorcDescuento) / 100));
                        //}
                        break;
                }
                //if (objNotaCredito.flgIGV == true)
                //{
                //    item.valor_unitario = Convert.ToDouble(det[6]) / 1.18;
                //    item.subtotal = item.cantidad * item.valor_unitario;
                //    item.precio_unitario = Convert.ToDouble(det[6]); //* 0.18;
                //    item.igv = item.cantidad * item.valor_unitario * 0.18;
                //}
                //else
                //{
                //    item.valor_unitario = Convert.ToDouble(det[6]);//150
                //    item.precio_unitario = Convert.ToDouble(det[6]) * 1.18;
                //    item.subtotal = item.cantidad * item.valor_unitario * (1 - (Convert.ToDouble(objNotaCredito.PorcDescuento) / 100));
                //    item.igv = item.cantidad * item.valor_unitario * 0.18 * (1 - (Convert.ToDouble(objNotaCredito.PorcDescuento) / 100));
                //}
                item.descuento = (item.subtotal * Convert.ToDouble(objNotaCredito.PorcDescuento) / 100); //"";                             
                item.tipo_de_igv = objNotaCredito.idTipoAfectacion;
                item.total = item.subtotal + item.igv;
                item.anticipo_regularizacion = false;
                item.anticipo_comprobante_serie = "";
                item.anticipo_comprobante_numero = "";
                oListaItems.Add(item);
            }
            invoice.items = oListaItems;
            return invoice;
        }
        public Invoice CrearObjetoNubefact_Anular(VEN_NotaCreditoDTO objNotaCredito)
        {
            Invoice invoice = new Invoice();
            invoice.operacion = "generar_anulacion";
            invoice.tipo_de_comprobante = 3; // objNotaCredito.idTipoDocumento;
            invoice.serie = objNotaCredito.Serie;
            invoice.numero = Convert.ToInt32(objNotaCredito.Numero);
            invoice.motivo = "ERROR DEL SISTEMA";
            invoice.codigo_unico = "";
            return invoice;
        }
        public Invoice CrearObjetoNubefact_Consultar(VEN_NotaCreditoDTO objNotaCredito)
        {
            Invoice invoice = new Invoice();
            invoice.operacion = "consultar_comprobante";
            invoice.tipo_de_comprobante = 3;
            invoice.serie = objNotaCredito.Serie;
            invoice.numero = Convert.ToInt32(objNotaCredito.Numero);
            return invoice;
        }

        static string SendJson(string ruta, string json, string token)
        {
            try
            {
                using (var client = new WebClient())
                {
                    /// ESPECIFICAMOS EL TIPO DE DOCUMENTO EN EL ENCABEZADO
                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    /// ASI COMO EL TOKEN UNICO
                    client.Headers[HttpRequestHeader.Authorization] = "Token token=" + token;
                    /// OBTENEMOS LA RESPUESTA
                    string respuesta = client.UploadString(ruta, "POST", json);
                    /// Y LA 'RETORNAMOS'
                    return respuesta;
                }
            }
            catch (WebException ex)
            {
                /// EN CASO EXISTA ALGUN ERROR, LO TOMAMOS
                var respuesta = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                /// Y LO 'RETORNAMOS'
                return respuesta;
            }
        }
        #endregion

        public string ObtenerDatosImpresion(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            VEN_NotaCreditoBL oVEN_NotaCreditoBL = new VEN_NotaCreditoBL();
            ResultDTO<VEN_NotaCreditoDTO> oListaNotaCredito = oVEN_NotaCreditoBL.ListarxID_DatosImpresion(id);
            string listaNotaCredito = Serializador.rSerializado(oListaNotaCredito.ListaResultado, new string[] { "Serie", "Numero", "Cliente", "ClienteDocumento", "ClienteDireccion",
                "FechaEmision","ReferenciaDocumento","NumeroDocumento","ImporteDocumento","FechaDocumento","Descripcion","Motivo","Total","IGV","TotalGravada","Enlace","idMoneda"});
            string listaNotaCreditoDetalle = Serializador.rSerializado(oListaNotaCredito.ListaResultado.FirstOrDefault().oListaDetalle, new string[] { "Cantidad", "UnidadMedida",
                "Descripcion","ValorUnitario","Total" });

            return String.Format("{0}↔{1}↔{2}", "OK", listaNotaCredito, listaNotaCreditoDetalle);
        }
        //Consulta Documento
        public string ConsultarDocumento(string serie, string numero)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            VEN_NotaCreditoDTO oVEN_NotaCreditoDTO = new VEN_NotaCreditoDTO();
            VEN_NotaCreditoBL oVEN_NotaCreditoBL = new VEN_NotaCreditoBL();
            oVEN_NotaCreditoDTO.Serie = serie;
            oVEN_NotaCreditoDTO.Numero = numero;
            Invoice objNubefact = CrearObjetoNubefact_Consultar(oVEN_NotaCreditoDTO);
            string json = JsonConvert.SerializeObject(objNubefact, Formatting.Indented);
            Console.WriteLine(json);
            byte[] bytes = Encoding.Default.GetBytes(json);
            string json_en_utf_8 = Encoding.UTF8.GetString(bytes);
            ////Envío de datos a nubefact
            string archivoXML = ""; string archivoCDR = "";
            if (!oVEN_NotaCreditoDTO.Serie.Contains("TK"))
            {
                string json_de_respuesta = SendJson(ruta, json_en_utf_8, token);
                dynamic r = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
                string r2 = JsonConvert.SerializeObject(r, Formatting.Indented);
                dynamic json_r_in = JsonConvert.DeserializeObject<Respuesta>(r2);
                dynamic leer_respuesta = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
                if (leer_respuesta.errors == null)
                {
                    string rpta = "";
                    rpta += "TIPO: " + leer_respuesta.tipo;
                    rpta += "SERIE: " + leer_respuesta.serie;
                    rpta += "NUMERO: " + leer_respuesta.numero;
                    rpta += "URL: " + leer_respuesta.url;
                    rpta += "ACEPTADA_POR_SUNAT: " + leer_respuesta.aceptada_por_sunat;
                    rpta += "DESCRIPCION SUNAT: " + leer_respuesta.sunat_description;
                    rpta += "NOTA SUNAT: " + leer_respuesta.sunat_note;
                    rpta += "CODIGO RESPUESTA SUNAT: " + leer_respuesta.sunat_responsecode;
                    rpta += "SUNAT ERROR SOAP: " + leer_respuesta.sunat_soap_error;
                    rpta += "PDF EN BASE64: " + leer_respuesta.pdf_zip_base64;
                    rpta += "XML EN BASE64: " + leer_respuesta.xml_zip_base64;
                    rpta += "CDR EN BASE64: " + leer_respuesta.cdr_zip_base64;
                    rpta += "CODIGO QR: " + leer_respuesta.cadena_para_codigo_qr;
                    rpta += "CODIGO HASH: " + leer_respuesta.codigo_hash;
                    rpta += "CODIGO DE BARRAS: " + leer_respuesta.codigo_de_barras;
                    rpta += "ERRORES: " + leer_respuesta.errors;
                    rpta += "ENLACES: " + leer_respuesta.enlace;
                    oVEN_NotaCreditoDTO.PDF_Base64 = leer_respuesta.pdf_zip_base64;
                    oVEN_NotaCreditoDTO.XML_Base64 = leer_respuesta.xml_zip_base64;
                    oVEN_NotaCreditoDTO.CDR_Base64 = leer_respuesta.cdr_zip_base64;
                    archivoCDR = leer_respuesta.enlace_del_cdr;
                    archivoXML = leer_respuesta.enlace_del_xml;
                }
                else
                {
                    rptaGrabar += leer_respuesta.errors;
                }
            }
            return string.Format("{0}↔{1}↔{2}↔{3}", archivoCDR, archivoXML, serie, numero);
        }



    }
}