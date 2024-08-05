using SistemaDermoSalud.Business;
using SistemaDermoSalud.Business.Mantenimiento;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Entities.Nubefact;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace SistemaDermoSalud.View.Controllers.Ventas
{
    public class DocumentoVentaController : Controller
    {
        ResultDTO<VEN_DocumentoVentaDTO> oResultDTO_Venta;
        string rptaGrabar = "";
        int flgIgv = 0;
        double montoIgv = 0;
        //////Prueba
        
        //DERMOSALUD
        public const string ruta = "https://api.pse.pe/api/v1/3538db4eee244c76be698f746650fd56904ca4c787ec432bad1ac11226621987";
        // # TOKEN para enviar documentos
        public const string token = "eyJhbGciOiJIUzI1NiJ9.ImMxYjI5NTJmN2M1YTRlNTE4ZTVhMjljMThjM2I5YmY4ZTA4ZGQxMzVlNjQ2NGJiNTlkYjEzZDQ2NTNiOTcwOTgi.4y3TUrIMP5GuVpeZNMdSqyu64I-daWr59liEka6Jq4E";

        // GET: DocumentoVenta
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
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();
            Ma_FormaPagoBL oMa_FormaPagoBL = new Ma_FormaPagoBL();
            Ma_TipoComprobanteBL oMa_TipoComprobanteBL = new Ma_TipoComprobanteBL();
            Ma_Clase_ArticuloBL oMa_Clase_ArticuloBL = new Ma_Clase_ArticuloBL();
            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
            Ma_BancoBL oMa_BancoBL = new Ma_BancoBL();
            AD_CuentaOrigenBL CuentaOrigen = new AD_CuentaOrigenBL();
            Ma_TipoAfectacionBL oMa_TipoAfectacionBL = new Ma_TipoAfectacionBL();

            ResultDTO<AD_SocioNegocioDTO> oListaSocios = oAD_SocioNegocioBL.ListarProv(eSEGUsuario.idEmpresa, "C");
            ResultDTO<Ma_MonedaDTO> oListaMoneda = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<Ma_FormaPagoDTO> oListaFormaPago = oMa_FormaPagoBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<Ma_TipoComprobanteDTO> oListaComprobantes = oMa_TipoComprobanteBL.ListarTodo();
            ResultDTO<Ma_Clase_ArticuloDTO> oListaClaseArticulo = oMa_Clase_ArticuloBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<VEN_DocumentoVentaDTO> oListaDocumentoVentas = oVN_DocumentoVentaBL.ListarRangoFecha(eSEGUsuario.idEmpresa, fechaInicio, fechaFin);
            ResultDTO<Ma_BancoDTO> oListaBancos = oMa_BancoBL.ListaBancos();
            ResultDTO<AD_CuentaOrigenDTO> oListaCuentaOrigen = CuentaOrigen.ListarTodo();
            ResultDTO<Ma_TipoAfectacionDTO> oListaAfectacion = oMa_TipoAfectacionBL.ListarTodo();
            ResultDTO<VEN_DocumentoVenta_ReportePorCliente> oListaReportePorCliente = oVN_DocumentoVentaBL.ReportePorCliente(fechaInicio, fechaFin);

            string listaSocios = Serializador.rSerializado(oListaSocios.ListaResultado, new string[] { "idSocioNegocio", "RazonSocial", "Documento" });
            string listaMoneda = Serializador.rSerializado(oListaMoneda.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            string listaFormaPago = Serializador.rSerializado(oListaFormaPago.ListaResultado, new string[] { "idFormaPago", "Descripcion" });
            string listaComprobantes = Serializador.rSerializado(oListaComprobantes.ListaResultado, new string[] { "idTipoComprobante", "Descripcion" });
            string listaTipoCompra = Serializador.rSerializado(oListaClaseArticulo.ListaResultado, new string[] { "idClaseArticulo", "CodigoGenerado", "Descripcion" });
            string listaDocumentoVentas = Serializador.rSerializado(oListaDocumentoVentas.ListaResultado, new string[] { "idDocumentoVenta", "FechaDocumento", "SerieDocumento", "NumDocumento", "ClienteRazon", "SubTotalNacional", "IGVNacional", "TotalNacional", "Estado", "Enlace" });
            string listaBanco = Serializador.rSerializado(oListaBancos.ListaResultado, new string[] { "idBanco", "Descripcion" });
            string listaCuentaOrigen = Serializador.rSerializado(oListaCuentaOrigen.ListaResultado, new string[] { "idCuentaOrigen", "NumeroCuenta" });
            string listaTipoAfectacion = Serializador.rSerializado(oListaAfectacion.ListaResultado, new string[] { "idTipoAfectacion", "Descripcion" });

            List<VEN_DocumentoVentaDTO> listaImpresion = oListaDocumentoVentas.ListaResultado.OrderBy(y => y.SerieDocumento).OrderBy(x => x.CodigoSunat).ToList();
            string listaDocImpresion = Serializador.rSerializado(listaImpresion, new string[]
            {"FechaDocumento", "CodigoSunat", "SerieNumDoc", "ClienteDocumento", "ClienteRazon", "MonedaDesc", "TipoCambio", "SubTotalDolares", "SubTotalSoles", "Inafecto", "IGVNacional", "Total"});

            string listaDocImpresionPorCliente = Serializador.rSerializado(oListaReportePorCliente.ListaResultado, new string[] { });

            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}↔{7}↔{8}↔{9}↔{10}↔{11}↔{12}↔{13}", "OK", listaSocios, listaMoneda, listaFormaPago, listaComprobantes, listaTipoCompra, listaDocumentoVentas, fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"), listaBanco, listaCuentaOrigen, listaTipoAfectacion, listaDocImpresion, listaDocImpresionPorCliente);
        }
        public string ObtenerDatosxID(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
            ResultDTO<VEN_DocumentoVentaDTO> oListaDocumentoCompras = oVN_DocumentoVentaBL.ListarxID(id);
            //Direcciones de Cliente
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            //ResultDTO<AD_SocioNegocioDTO> oListaDirecciones = oAD_SocioNegocioBL.ListarxID(oListaDocumentoCompras.ListaResultado[0].idCliente);
            string listaDocumentoVenta = Serializador.rSerializado(oListaDocumentoCompras.ListaResultado, new string[] { });
            string listaSocio_Direccion = "";// Serializador.rSerializado((oListaDirecciones.ListaResultado[0].oListaDireccion), new string[] { "idDireccion", "Direccion" });
            string listaDocumentoVentaDetalle = Serializador.rSerializado(oListaDocumentoCompras.ListaResultado[0].oListaDetalle, new string[] { });
            string fechaEmi = oListaDocumentoCompras.ListaResultado.FirstOrDefault().FechaDocumento.ToString("dd/MM/yyyy");
            string fechaVen = oListaDocumentoCompras.ListaResultado.FirstOrDefault().FechaDocumento.AddDays(10).ToString("dd/MM/yyyy");

            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}", "OK", listaDocumentoVenta, listaSocio_Direccion, listaDocumentoVentaDetalle, fechaEmi, fechaVen);
        }
        public string ObtenerPorFecha(string fechaInicio, string fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
            ResultDTO<VEN_DocumentoVentaDTO> oListaDocumentoVentas = oVN_DocumentoVentaBL.ListarRangoFecha(eSEGUsuario.idEmpresa, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin));
            string listaDocumentoVenta = Serializador.rSerializado(oListaDocumentoVentas.ListaResultado, new string[]
            { "idDocumentoVenta", "FechaDocumento", "SerieDocumento", "NumDocumento", "ClienteRazon", "SubTotalNacional", "IGVNacional", "TotalNacional", "Estado", "Enlace" });

            List<VEN_DocumentoVentaDTO> listaImpresion = oListaDocumentoVentas.ListaResultado.OrderBy(y => y.SerieDocumento).OrderBy(x => x.CodigoSunat).ToList();
            string listaDocImpresion = Serializador.rSerializado(listaImpresion, new string[]
            {"FechaDocumento", "CodigoSunat", "SerieNumDoc", "ClienteDocumento", "ClienteRazon", "MonedaDesc", "TipoCambio", "SubTotalDolares", "SubTotalSoles", "Inafecto", "IGVNacional", "Total"});

            ResultDTO<VEN_DocumentoVenta_ReportePorCliente> oListaReportePorCliente = oVN_DocumentoVentaBL.ReportePorCliente(Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin));
            string listaDocImpresionPorCliente = Serializador.rSerializado(oListaReportePorCliente.ListaResultado, new string[] { });

            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", oListaDocumentoVentas.Resultado, oListaDocumentoVentas.MensajeError, listaDocumentoVenta, listaDocImpresion, listaDocImpresionPorCliente);
        }
        public string Grabar(VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO;

            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
            string oResultDTO2 = oVN_DocumentoVentaBL.ValidarDocumento(oVEN_DocumentoVentaDTO);
            string validandoElDocumento = "";
            validandoElDocumento = oResultDTO2;
            if (validandoElDocumento == "" || oVEN_DocumentoVentaDTO.idDocumentoVenta != 0)
            {
                if (oVEN_DocumentoVentaDTO.ClienteDocumento.Length >= 8 || oVEN_DocumentoVentaDTO.ClienteDocumento.Length <= 10) { oVEN_DocumentoVentaDTO.ClienteTipoDocumento = 1; }
                if (oVEN_DocumentoVentaDTO.ClienteDocumento.Length == 11) { oVEN_DocumentoVentaDTO.ClienteTipoDocumento = 6; }
                if (oVEN_DocumentoVentaDTO.idTipoVenta == 1) { oVEN_DocumentoVentaDTO.Unidad_Medida = "NIU"; }
                if (oVEN_DocumentoVentaDTO.idTipoVenta == 2 || oVEN_DocumentoVentaDTO.idTipoVenta == 3) { oVEN_DocumentoVentaDTO.Unidad_Medida = "ZZ"; }
                if (oVEN_DocumentoVentaDTO.flgIGV == true) { montoIgv = 1.18; } else { montoIgv = 0.18; }
                oVEN_DocumentoVentaDTO.idEmpresa = eSEGUsuario.idEmpresa;
                oVEN_DocumentoVentaDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
                oVEN_DocumentoVentaDTO.UsuarioModificacion = eSEGUsuario.idUsuario;

                oResultDTO_Venta = oVN_DocumentoVentaBL.UpdateInsert(oVEN_DocumentoVentaDTO, fechaInicio, fechaFin);
                if (oResultDTO_Venta.Resultado == "OK")
                {
                    //---comentar fact electronica
                    Invoice objNubefact = CrearObjetoNubefact(oVEN_DocumentoVentaDTO);
                    string json = JsonConvert.SerializeObject(objNubefact, Formatting.Indented);
                    Console.WriteLine(json);
                    byte[] bytes = Encoding.Default.GetBytes(json);
                    string json_en_utf_8 = Encoding.UTF8.GetString(bytes);
                    ////Envío de datos a nubefact

                    if (!oVEN_DocumentoVentaDTO.SerieDocumento.Contains("TK"))
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
                            oVEN_DocumentoVentaDTO.Enlace = leer_respuesta.enlace + ".pdf";
                            oResultDTO_Venta = oVN_DocumentoVentaBL.Actualizar(oVEN_DocumentoVentaDTO, fechaInicio, fechaFin);
                        }
                        else
                        {
                            rptaGrabar += leer_respuesta.errors;
                        }
                    }
                    else
                    {
                        //oResultDTO_Venta = oVN_DocumentoVentaBL.UpdateInsert(oVEN_DocumentoVentaDTO, fechaInicio, fechaFin);//no se comenta
                    }
                }



                if (oResultDTO_Venta != null)
                {
                    string listaDocumentoVenta = Serializador.rSerializado(oResultDTO_Venta.ListaResultado, new string[]
                    {"idDocumentoVenta","FechaDocumento","SerieDocumento", "NumDocumento","ClienteRazon","SubTotalNacional","IGVNacional","TotalNacional","Estado","Enlace" });

                    List<VEN_DocumentoVentaDTO> listaImpresion = oResultDTO_Venta.ListaResultado.OrderBy(y => y.SerieDocumento).OrderBy(x => x.CodigoSunat).ToList();
                    string listaDocImpresion = Serializador.rSerializado(listaImpresion, new string[]
                    {"FechaDocumento", "CodigoSunat", "SerieNumDoc", "ClienteDocumento", "ClienteRazon", "MonedaDesc", "TipoCambio", "SubTotalDolares", "SubTotalSoles", "Inafecto", "IGVNacional", "Total"});

                    ResultDTO<VEN_DocumentoVenta_ReportePorCliente> oListaReportePorCliente = oVN_DocumentoVentaBL.ReportePorCliente(fechaInicio, fechaFin);
                    string listaDocImpresionPorCliente = Serializador.rSerializado(oListaReportePorCliente.ListaResultado, new string[] { });

                    return string.Format("{0}↔{1}↔{2}↔{3}↔{4}", oResultDTO_Venta.Resultado, oResultDTO_Venta.MensajeError, listaDocumentoVenta, listaDocImpresion, listaDocImpresionPorCliente);
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

        #region Nubefact
        public Invoice CrearObjetoNubefact(VEN_DocumentoVentaDTO objVenta)
        {
            Invoice invoice = new Invoice();
            invoice.operacion = "generar_comprobante";
            invoice.tipo_de_comprobante = objVenta.idTipoDocumento;
            invoice.serie = objVenta.SerieDocumento;
            invoice.numero = Convert.ToInt32(objVenta.NumDocumento);
            invoice.sunat_transaction = 1;
            invoice.cliente_tipo_de_documento = objVenta.ClienteTipoDocumento;
            invoice.cliente_numero_de_documento = objVenta.ClienteDocumento;
            invoice.cliente_denominacion = objVenta.ClienteRazon;
            invoice.cliente_direccion = objVenta.ClienteDireccion;
            invoice.cliente_email = "";//"andres_vl89@hotmail.com";
            invoice.cliente_email_1 = "";
            invoice.cliente_email_2 = "";
            invoice.fecha_de_emision =objVenta.FechaDocumento;
            invoice.fecha_de_vencimiento = DateTime.Now.AddDays(10);
            invoice.moneda = 1;
            invoice.tipo_de_cambio = objVenta.TipoCambio;
            invoice.porcentaje_de_igv = 18.00;
            invoice.descuento_global = objVenta.TotalDescuentoNacional;
            invoice.total_descuento = objVenta.TotalDescuentoNacional; //"";
            invoice.total_anticipo = "";
            invoice.total_gravada = objVenta.SubTotalNacional;
            invoice.total_inafecta = objVenta.TotalInafecto;
            invoice.total_exonerada = objVenta.TotalExonerado;
            invoice.total_igv = Convert.ToDouble(objVenta.IGVNacional);
            invoice.total_gratuita = 0; //"";
            invoice.total_otros_cargos = objVenta.TotalOtrosCargos; //"";
            invoice.total = Convert.ToDouble(objVenta.TotalNacional);
            invoice.percepcion_tipo = "";
            invoice.percepcion_base_imponible = "";
            invoice.total_percepcion = "";
            invoice.detraccion = false;
            invoice.observaciones = objVenta.ObservacionVenta;
            invoice.documento_que_se_modifica_tipo = "";
            invoice.documento_que_se_modifica_serie = "";
            invoice.documento_que_se_modifica_numero = "";
            invoice.tipo_de_nota_de_credito = "";
            invoice.tipo_de_nota_de_debito = "";
            invoice.enviar_automaticamente_a_la_sunat = true;
            invoice.enviar_automaticamente_al_cliente = true;
            invoice.codigo_unico = "";
            invoice.condiciones_de_pago = objVenta.FormaPago;
            if (objVenta.idFormaPago == 2 && objVenta.TipoPago == "Efectivo")
            {
                invoice.medio_de_pago = objVenta.TipoPago;
            }
            else
            {
                invoice.medio_de_pago = objVenta.TipoPago + " " + objVenta.Tarjeta + " OP:" + objVenta.NroOperacion;
            }

            invoice.placa_vehiculo = "";
            invoice.orden_compra_servicio = "";
            invoice.tabla_personalizada_codigo = "";
            invoice.formato_de_pdf = "TICKET";
            List<Items> oListaItems = new List<Items>();
            string[] listaDets = objVenta.cadDetalle.Split('¯');
            for (int i = 0; i < listaDets.Length - 1; i++)
            {
                string[] det = listaDets[i].Split('|');
                Items item = new Items();
                item.unidad_de_medida = objVenta.Unidad_Medida;
                item.codigo = det[2];
                item.codigo_producto_sunat = "";
                item.descripcion = det[3];
                item.cantidad = Convert.ToDouble(det[4]);
                //item.valor_unitario = Convert.ToDouble(det[7]);//150

                if (objVenta.idTipoVenta == 2)
                {
                    if (objVenta.flgIGV == true)
                    {
                        item.valor_unitario = Convert.ToDouble(det[7]) / 1.18;
                        item.subtotal = item.cantidad * item.valor_unitario;//item.valor_unitario * 100 / 118;
                        item.precio_unitario = Convert.ToDouble(det[7]);
                        item.igv = item.cantidad * item.valor_unitario * 0.18;
                    }
                    else
                    {
                        item.valor_unitario = Convert.ToDouble(det[7]);
                        item.precio_unitario = Convert.ToDouble(det[7]) * 1.18;
                        item.subtotal = item.valor_unitario;
                        item.igv = item.cantidad * item.valor_unitario * 0.18 * (1 - (Convert.ToDouble(objVenta.PorcDescuento) / 100));
                    }
                    // item.igv = ((item.subtotal) - (item.subtotal * Convert.ToDouble(objVenta.PorcDescuento) / 100)) * 0.18;//item.igv = (item.subtotal - Convert.ToDouble(dscto)) * 0.18;
                }
                else
                {
                    if (objVenta.flgIGV == true)
                    {
                        item.valor_unitario = Convert.ToDouble(det[7]) / 1.18;
                        item.subtotal = item.cantidad * item.valor_unitario;
                        item.precio_unitario = Convert.ToDouble(det[7]); //* 0.18;
                        item.igv = item.cantidad * item.valor_unitario * 0.18;
                    }
                    else
                    {
                        item.valor_unitario = Convert.ToDouble(det[7]);//150
                        item.precio_unitario = Convert.ToDouble(det[7]) * 1.18;
                        item.subtotal = item.cantidad * item.valor_unitario * (1 - (Convert.ToDouble(objVenta.PorcDescuento) / 100));
                        item.igv = item.cantidad * item.valor_unitario * 0.18 * (1 - (Convert.ToDouble(objVenta.PorcDescuento) / 100));
                    }
                    //item.precio_unitario = Convert.ToDouble(det[7]);// * 1.18;
                    //if (objVenta.flgIGV == true) { item.subtotal = item.cantidad * item.valor_unitario * 100 / 118; } else { item.subtotal = item.cantidad * item.valor_unitario; }
                    //item.igv = (item.subtotal - (item.subtotal * Convert.ToDouble(objVenta.PorcDescuento) / 100)) * 0.18;//item.igv = (item.subtotal - Convert.ToDouble(dscto)) * 0.18;
                }
                item.descuento = (item.subtotal * Convert.ToDouble(objVenta.PorcDescuento) / 100); //"";                             
                item.tipo_de_igv = 1;
                item.total = item.subtotal + item.igv;
                item.anticipo_regularizacion = false;
                item.anticipo_comprobante_serie = "";
                item.anticipo_comprobante_numero = "";
                oListaItems.Add(item);
            }
            invoice.items = oListaItems;
            return invoice;
        }
        public Invoice CrearObjetoNubefact_Anular(VEN_DocumentoVentaDTO objVenta)
        {
            Invoice invoice = new Invoice();
            invoice.operacion = "generar_anulacion";
            invoice.tipo_de_comprobante = objVenta.idTipoDocumento;
            invoice.serie = objVenta.SerieDocumento;
            invoice.numero = Convert.ToInt32(objVenta.NumDocumento);
            invoice.motivo = "ERROR DEL SISTEMA";
            invoice.codigo_unico = "";
            return invoice;
        }
        public Invoice CrearObjetoNubefact_Consultar(VEN_DocumentoVentaDTO objVenta)
        {
            Invoice invoice = new Invoice();
            invoice.operacion = "consultar_comprobante";
            invoice.tipo_de_comprobante = objVenta.idTipoDocumento;
            invoice.serie = objVenta.SerieDocumento;
            invoice.numero = Convert.ToInt32(objVenta.NumDocumento);
            return invoice;
        }

        static string SendJson(string ruta, string json, string token)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
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
        ////
        public string Eliminar(VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VN_DocumentoVentaBL oCOM_DocumentoCompraBL = new VN_DocumentoVentaBL();
            oVEN_DocumentoVentaDTO.idEmpresa = eSEGUsuario.idEmpresa;

            oResultDTO = oCOM_DocumentoCompraBL.Delete(oVEN_DocumentoVentaDTO, fechaInicio, fechaFin);
            string lista_Venta = Serializador.rSerializado(oResultDTO.ListaResultado, new string[]
            { "idDocumentoVenta", "FechaDocumento", "SerieDocumento", "NumDocumento","ClienteRazon", "SubTotalNacional", "IGVNacional", "TotalNacional"});
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, lista_Venta);
        }
        public string Anular(VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO)
        {

            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VEN_DocumentoVentaDTO oVenta = oVN_DocumentoVentaBL.ListarxIDVenta(oVEN_DocumentoVentaDTO.idDocumentoVenta);
            Invoice objNubefact = CrearObjetoNubefact_Anular(oVenta);
            string json = JsonConvert.SerializeObject(objNubefact, Formatting.Indented);
            Console.WriteLine(json);
            byte[] bytes = Encoding.Default.GetBytes(json);
            string json_en_utf_8 = Encoding.UTF8.GetString(bytes);

            //int VCaja = oVN_DocumentoVentaBL.ValidarEnCaja(oVEN_DocumentoVentaDTO.idDocumentoVenta);

            if (oVEN_DocumentoVentaDTO.idDocumentoVenta > 0)
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

                    oResultDTO_Venta = oVN_DocumentoVentaBL.Anular(oVEN_DocumentoVentaDTO, fechaInicio, fechaFin);
                }
                else
                {
                    rptaGrabar += leer_respuesta.errors;
                }
            }
            else
            {
                oResultDTO_Venta = oVN_DocumentoVentaBL.Anular(oVEN_DocumentoVentaDTO, fechaInicio, fechaFin);
            }

            if (oResultDTO_Venta != null)
            {
                string listaDocumentoVenta = Serializador.rSerializado(oResultDTO_Venta.ListaResultado, new string[]
                {"idDocumentoVenta", "FechaDocumento", "SerieDocumento", "NumDocumento","ClienteRazon", "SubTotalNacional", "IGVNacional", "TotalNacional","Estado" });
                return string.Format("{0}↔{1}↔{2}", oResultDTO_Venta.Resultado, oResultDTO_Venta.MensajeError, listaDocumentoVenta);
            }
            else
            {
                return rptaGrabar;
            }
        }
        ///////
        public string ObtenerTipoCambio(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
            ResultDTO<VEN_DocumentoVentaDTO> oListaDocumentoCompras = oVN_DocumentoVentaBL.ListarxID(id);
            //Direcciones de Cliente
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            ResultDTO<AD_SocioNegocioDTO> oListaDirecciones = oAD_SocioNegocioBL.ListarxID(oListaDocumentoCompras.ListaResultado[0].idCliente);
            string listaDocumentoVenta = Serializador.rSerializado(oListaDocumentoCompras.ListaResultado, new string[] { });
            string listaSocio_Direccion = Serializador.rSerializado(oListaDirecciones.ListaResultado[0].oListaDireccion, new string[] { "idDireccion", "Direccion" });
            string listaDocumentoVentaDetalle = Serializador.rSerializado(oListaDocumentoCompras.ListaResultado[0].oListaDetalle, new string[] { });
            return String.Format("{0}↔{1}↔{2}↔{3}", "OK", listaDocumentoVenta, listaSocio_Direccion, listaDocumentoVentaDetalle);
        }
        public string ObtenerDatosCita(int idCita)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            CitasBL oCitasBL = new CitasBL();
            PacientesBL oPacientesBL = new PacientesBL();
            ResultDTO<CitasDTO> oListaDetalleCitas = oCitasBL.ListarDetalleCitaxId(idCita);
            int idPaciente = oListaDetalleCitas.ListaResultado[0].idPaciente;
            ResultDTO<PacientesDTO> oListaPaciente = oPacientesBL.ListarxID(idPaciente);
            string listaCita = Serializador.rSerializado(oListaDetalleCitas.ListaResultado, new string[] { "Paciente", "Dni", "Direccion", "Observaciones", "Pago", "idPaciente" });
            string listaPaciente = Serializador.rSerializado(oListaPaciente.ListaResultado, new string[] { "idPaciente", "Nombres", "ApellidoP", "ApellidoM", "DNI", "Direccion" });
            string listaCitasDetalle = Serializador.rSerializado(oListaDetalleCitas.ListaResultado[0].oListaDetalle, new string[] { });
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}", "OK", oListaDetalleCitas, oListaDetalleCitas, listaCita, listaPaciente, listaCitasDetalle);
        }
        public string ObtenerSeriexTipoDocumento(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            VEN_DocumentoVentaBL oVEN_DocumentoVentaBL = new VEN_DocumentoVentaBL();
            ResultDTO<VEN_SerieDTO> oListaSerieDoc = oVEN_DocumentoVentaBL.ListarSeriexTipoDocumento(id);
            string listaSeries = Serializador.rSerializado(oListaSerieDoc.ListaResultado, new string[] { "idSerie", "NroSerie" });
            return String.Format("{0}↔{1}", "OK", listaSeries);
        }
        public string ObtenerNumeroDoc(int id, string Serie)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VEN_DocumentoVentaBL oVEN_DocumentoVentaBL = new VEN_DocumentoVentaBL();
            string NroDocumento = oVEN_DocumentoVentaBL.ObtenerNumeroDocumentoxSerie(id, Serie);
            return NroDocumento;
        }

        //Impresion documento
        public string DownloadPDF(int idDocumento)
        {
            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
            VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocumentoVentaDTO();
            ResultDTO<VEN_DocumentoVentaDTO> obj_DocumentoVenta = oVN_DocumentoVentaBL.ListarxID(idDocumento);
            oVEN_DocumentoVentaDTO.SerieDocumento = obj_DocumentoVenta.ListaResultado[0].SerieDocumento;
            oVEN_DocumentoVentaDTO.NumDocumento = obj_DocumentoVenta.ListaResultado[0].NumDocumento;
            oVEN_DocumentoVentaDTO.idTipoDocumento = obj_DocumentoVenta.ListaResultado[0].idTipoDocumento;
            Invoice objNubefact = CrearObjetoNubefact_Consultar(oVEN_DocumentoVentaDTO);
            string json = JsonConvert.SerializeObject(objNubefact, Formatting.Indented);
            Console.WriteLine(json);
            byte[] bytes = Encoding.Default.GetBytes(json);
            string json_en_utf_8 = Encoding.UTF8.GetString(bytes);
            //Envío de datos a nubefact
            string json_de_respuesta = SendJson(ruta, json_en_utf_8, token);
            dynamic r = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
            string r2 = JsonConvert.SerializeObject(r, Formatting.Indented);
            dynamic json_r_in = JsonConvert.DeserializeObject<Respuesta>(r2);
            dynamic leer_respuesta = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
            string Enlace = "";
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
                rpta += "ENLACE: " + leer_respuesta.enlace;
                Enlace = leer_respuesta.enlace + ".pdf";
            }
            return Enlace;
        }
        public string ConsultarDocumento(int idDocumento, string serieDcto, string nroDcto)
        {
            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
            VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO = new VEN_DocumentoVentaDTO();
            oVEN_DocumentoVentaDTO.idTipoDocumento = idDocumento;
            oVEN_DocumentoVentaDTO.SerieDocumento = serieDcto;
            oVEN_DocumentoVentaDTO.NumDocumento = nroDcto;
            Invoice objNubefact = CrearObjetoNubefact_Consultar(oVEN_DocumentoVentaDTO);
            string json = JsonConvert.SerializeObject(objNubefact, Formatting.Indented);
            Console.WriteLine(json);
            byte[] bytes = Encoding.Default.GetBytes(json);
            string json_en_utf_8 = Encoding.UTF8.GetString(bytes);
            //Envío de datos a nubefact
            string json_de_respuesta = SendJson(ruta, json_en_utf_8, token);
            dynamic r = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
            string r2 = JsonConvert.SerializeObject(r, Formatting.Indented);
            dynamic json_r_in = JsonConvert.DeserializeObject<Respuesta>(r2);
            dynamic leer_respuesta = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
            string Enlace = "";
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
                rpta += "ENLACE: " + leer_respuesta.enlace;
                Enlace = leer_respuesta.enlace + ".pdf";
            }
            return Enlace;
        }
        public string EnviarSunat(VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<VEN_DocumentoVentaDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();

            if (oVEN_DocumentoVentaDTO.ClienteDocumento.Length >= 8 || oVEN_DocumentoVentaDTO.ClienteDocumento.Length <= 10) { oVEN_DocumentoVentaDTO.ClienteTipoDocumento = 1; }
            if (oVEN_DocumentoVentaDTO.ClienteDocumento.Length == 11) { oVEN_DocumentoVentaDTO.ClienteTipoDocumento = 6; }
            if (oVEN_DocumentoVentaDTO.idTipoVenta == 1) { oVEN_DocumentoVentaDTO.Unidad_Medida = "NIU"; }
            if (oVEN_DocumentoVentaDTO.idTipoVenta == 2 || oVEN_DocumentoVentaDTO.idTipoVenta == 3) { oVEN_DocumentoVentaDTO.Unidad_Medida = "ZZ"; }
            if (oVEN_DocumentoVentaDTO.flgIGV == true) { montoIgv = 1.18; } else { montoIgv = 0.18; }
            oVEN_DocumentoVentaDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oVEN_DocumentoVentaDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            oVEN_DocumentoVentaDTO.UsuarioModificacion = eSEGUsuario.idUsuario;

            Invoice objNubefact = CrearObjetoNubefact(oVEN_DocumentoVentaDTO);
            string json = JsonConvert.SerializeObject(objNubefact, Formatting.Indented);
            Console.WriteLine(json);
            byte[] bytes = Encoding.Default.GetBytes(json);
            string json_en_utf_8 = Encoding.UTF8.GetString(bytes);

            if (!oVEN_DocumentoVentaDTO.SerieDocumento.Contains("TK"))
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
                    oVEN_DocumentoVentaDTO.Enlace = leer_respuesta.enlace + ".pdf";
                    oResultDTO_Venta = oVN_DocumentoVentaBL.Actualizar(oVEN_DocumentoVentaDTO, fechaInicio, fechaFin);
                }
                else
                {
                    rptaGrabar += leer_respuesta.errors;
                }
            }

            if (oResultDTO_Venta != null)
            {
                string listaDocumentoVenta = Serializador.rSerializado(oResultDTO_Venta.ListaResultado, new string[]
                {"idDocumentoVenta","FechaDocumento","SerieDocumento", "NumDocumento","ClienteRazon","SubTotalNacional","IGVNacional","TotalNacional","Estado","Enlace" });

                List<VEN_DocumentoVentaDTO> listaImpresion = oResultDTO_Venta.ListaResultado.OrderBy(y => y.SerieDocumento).OrderBy(x => x.CodigoSunat).ToList();
                string listaDocImpresion = Serializador.rSerializado(listaImpresion, new string[]
                {"FechaDocumento", "CodigoSunat", "SerieNumDoc", "ClienteDocumento", "ClienteRazon", "MonedaDesc", "TipoCambio", "SubTotalDolares", "SubTotalSoles", "Inafecto", "IGVNacional", "Total"});

                ResultDTO<VEN_DocumentoVenta_ReportePorCliente> oListaReportePorCliente = oVN_DocumentoVentaBL.ReportePorCliente(fechaInicio, fechaFin);
                string listaDocImpresionPorCliente = Serializador.rSerializado(oListaReportePorCliente.ListaResultado, new string[] { });

                return string.Format("{0}↔{1}↔{2}↔{3}↔{4}", oResultDTO_Venta.Resultado, oResultDTO_Venta.MensajeError, listaDocumentoVenta, listaDocImpresion, listaDocImpresionPorCliente);
            }
            else
            {
                return rptaGrabar;
            }

        }
    }
}





//public string Grabar(VEN_DocumentoVentaDTO oVEN_DocumentoVentaDTO)
//{
//    DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
//    DateTime fechaFin = DateTime.Today;
//    ResultDTO<VEN_DocumentoVentaDTO> oResultDTO;

//    Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
//    VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
//    string oResultDTO2 = oVN_DocumentoVentaBL.ValidarDocumento(oVEN_DocumentoVentaDTO);
//    string validandoElDocumento = "";
//    validandoElDocumento = oResultDTO2;
//    if (validandoElDocumento == "" || oVEN_DocumentoVentaDTO.idDocumentoVenta != 0)
//    {
//        if (oVEN_DocumentoVentaDTO.ClienteDocumento.Length >= 8 || oVEN_DocumentoVentaDTO.ClienteDocumento.Length <= 10) { oVEN_DocumentoVentaDTO.ClienteTipoDocumento = 1; }
//        if (oVEN_DocumentoVentaDTO.ClienteDocumento.Length == 11) { oVEN_DocumentoVentaDTO.ClienteTipoDocumento = 6; }
//        if (oVEN_DocumentoVentaDTO.idTipoVenta == 1) { oVEN_DocumentoVentaDTO.Unidad_Medida = "NIU"; }
//        if (oVEN_DocumentoVentaDTO.idTipoVenta == 2 || oVEN_DocumentoVentaDTO.idTipoVenta == 3) { oVEN_DocumentoVentaDTO.Unidad_Medida = "ZZ"; }
//        if (oVEN_DocumentoVentaDTO.flgIGV == true) { montoIgv = 1.18; } else { montoIgv = 0.18; }
//        oVEN_DocumentoVentaDTO.idEmpresa = eSEGUsuario.idEmpresa;
//        oVEN_DocumentoVentaDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
//        oVEN_DocumentoVentaDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
//        //---comentar fact electronica
//        Invoice objNubefact = CrearObjetoNubefact(oVEN_DocumentoVentaDTO);
//        string json = JsonConvert.SerializeObject(objNubefact, Formatting.Indented);
//        Console.WriteLine(json);
//        byte[] bytes = Encoding.Default.GetBytes(json);
//        string json_en_utf_8 = Encoding.UTF8.GetString(bytes);
//        ////Envío de datos a nubefact

//        if (!oVEN_DocumentoVentaDTO.SerieDocumento.Contains("TK"))
//        {
//            string json_de_respuesta = SendJson(ruta, json_en_utf_8, token);
//            dynamic r = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
//            string r2 = JsonConvert.SerializeObject(r, Formatting.Indented);
//            dynamic json_r_in = JsonConvert.DeserializeObject<Respuesta>(r2);
//            dynamic leer_respuesta = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
//            if (leer_respuesta.errors == null)
//            {
//                string rpta = "";
//                rpta += "TIPO: " + leer_respuesta.tipo;
//                rpta += "SERIE: " + leer_respuesta.serie;
//                rpta += "NUMERO: " + leer_respuesta.numero;
//                rpta += "URL: " + leer_respuesta.url;
//                rpta += "ACEPTADA_POR_SUNAT: " + leer_respuesta.aceptada_por_sunat;
//                rpta += "DESCRIPCION SUNAT: " + leer_respuesta.sunat_description;
//                rpta += "NOTA SUNAT: " + leer_respuesta.sunat_note;
//                rpta += "CODIGO RESPUESTA SUNAT: " + leer_respuesta.sunat_responsecode;
//                rpta += "SUNAT ERROR SOAP: " + leer_respuesta.sunat_soap_error;
//                rpta += "PDF EN BASE64: " + leer_respuesta.pdf_zip_base64;
//                rpta += "XML EN BASE64: " + leer_respuesta.xml_zip_base64;
//                rpta += "CDR EN BASE64: " + leer_respuesta.cdr_zip_base64;
//                rpta += "CODIGO QR: " + leer_respuesta.cadena_para_codigo_qr;
//                rpta += "CODIGO HASH: " + leer_respuesta.codigo_hash;
//                rpta += "CODIGO DE BARRAS: " + leer_respuesta.codigo_de_barras;
//                rpta += "ERRORES: " + leer_respuesta.errors;
//                rpta += "ENLACES: " + leer_respuesta.enlace;
//                oVEN_DocumentoVentaDTO.Enlace = leer_respuesta.enlace + ".pdf";
//                oResultDTO_Venta = oVN_DocumentoVentaBL.UpdateInsert(oVEN_DocumentoVentaDTO, fechaInicio, fechaFin);
//            }
//            else
//            {
//                rptaGrabar += leer_respuesta.errors;
//            }
//        }
//        else
//        {
//            oResultDTO_Venta = oVN_DocumentoVentaBL.UpdateInsert(oVEN_DocumentoVentaDTO, fechaInicio, fechaFin);//no se comenta
//        }
//        //
//        if (oResultDTO_Venta != null)
//        {
//            string listaDocumentoVenta = Serializador.rSerializado(oResultDTO_Venta.ListaResultado, new string[]
//            {"idDocumentoVenta","FechaDocumento","SerieDocumento", "NumDocumento","ClienteRazon","SubTotalNacional","IGVNacional","TotalNacional","Estado","Enlace" });

//            List<VEN_DocumentoVentaDTO> listaImpresion = oResultDTO_Venta.ListaResultado.OrderBy(y => y.SerieDocumento).OrderBy(x => x.CodigoSunat).ToList();
//            string listaDocImpresion = Serializador.rSerializado(listaImpresion, new string[]
//            {"FechaDocumento", "CodigoSunat", "SerieNumDoc", "ClienteDocumento", "ClienteRazon", "MonedaDesc", "TipoCambio", "SubTotalDolares", "SubTotalSoles", "Inafecto", "IGVNacional", "Total"});

//            ResultDTO<VEN_DocumentoVenta_ReportePorCliente> oListaReportePorCliente = oVN_DocumentoVentaBL.ReportePorCliente(fechaInicio, fechaFin);
//            string listaDocImpresionPorCliente = Serializador.rSerializado(oListaReportePorCliente.ListaResultado, new string[] { });

//            return string.Format("{0}↔{1}↔{2}↔{3}↔{4}", oResultDTO_Venta.Resultado, oResultDTO_Venta.MensajeError, listaDocumentoVenta, listaDocImpresion, listaDocImpresionPorCliente);
//        }
//        else
//        {
//            return rptaGrabar;
//        }
//    }
//    else
//    {
//        return string.Format("{0}↔{1}↔{2}", "REPITE", "Ya existe un proveedor que tiene ese numero y serie", "no se guardo nada");
//    }
//}