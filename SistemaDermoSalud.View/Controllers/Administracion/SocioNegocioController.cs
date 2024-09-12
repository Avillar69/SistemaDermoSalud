using DocumentFormat.OpenXml.Math;
using Newtonsoft.Json;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace SistemaDermoSalud.View.Controllers.Administracion
{
    public class SocioNegocioController : Controller
    {
        // GET: SocioNegocio
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public ActionResult IndexPro()
        {
            return PartialView();
        }
        //clientes
        public ActionResult IndexCli()
        {
            return PartialView();
        }

        public string ObtenerDatos()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            Ma_PaisBL oMa_PaisBL = new Ma_PaisBL();
            Ma_TipoPersonaBL oMa_TipoPersonaBL = new Ma_TipoPersonaBL();
            Ma_TipoDocumentoBL oMa_TipoDocumentoBL = new Ma_TipoDocumentoBL();
            Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();
            Ma_UbigeoBL oMa_UbigeoBL = new Ma_UbigeoBL();
            //listas
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = oAD_SocioNegocioBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<Ma_PaisDTO> lbeMa_PaisDTO = oMa_PaisBL.ListarTodo();
            ResultDTO<Ma_TipoPersonaDTO> lbeMa_TipoPersonaDTO = oMa_TipoPersonaBL.ListarTodo();
            ResultDTO<Ma_TipoDocumentoDTO> lbeMa_TipoDocumentoDTO = oMa_TipoDocumentoBL.ListarTodo();
            ResultDTO<Ma_MonedaDTO> lbeMa_MonedaDTO = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<Ma_UbigeoDTO> lbe_Departamentos = oMa_UbigeoBL.ListarDepartamentos();
            ResultDTO<Ma_UbigeoDTO> lbe_Provincias = oMa_UbigeoBL.ListarProvincias();
            ResultDTO<Ma_UbigeoDTO> lbe_Distritos = oMa_UbigeoBL.ListarDistritos();

            //cadenas
            string listaAD_SocioNegocio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idSocioNegocio", "Documento", "RazonSocial", "FechaModificacion", "DesUsuarioModificacion", "Estado" });
            string listaMa_Pais = Serializador.rSerializado(lbeMa_PaisDTO.ListaResultado, new string[] { "idPais", "Descripcion" });
            string listaMa_TipoPersona = Serializador.rSerializado(lbeMa_TipoPersonaDTO.ListaResultado, new string[] { "idTipoPersona", "Descripcion" });
            string listaMa_TipoDocumento = Serializador.rSerializado(lbeMa_TipoDocumentoDTO.ListaResultado, new string[] { "idTipoDocumento", "Descripcion" });
            string listaMa_Moneda = Serializador.rSerializado(lbeMa_MonedaDTO.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            string idUsuario = eSEGUsuario.idUsuario.ToString();
            string listDepartamentos = Serializador.rSerializado(lbe_Departamentos.ListaResultado, new string[] { "CodigoDpto", "Departamento", "Ubigeo_ID" });
            string listProvincias = Serializador.rSerializado(lbe_Provincias.ListaResultado, new string[] { "CodigoProv", "Provincia", "Ubigeo_ID" });
            string listDistritos = Serializador.rSerializado(lbe_Distritos.ListaResultado, new string[] { "CodigoDist", "Distrito", "Ubigeo_ID" });

            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}↔{7}↔{8}↔{9}↔{10}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaAD_SocioNegocio, listaMa_Pais, listaMa_TipoPersona,
                listaMa_TipoDocumento, listaMa_Moneda, idUsuario, listDepartamentos, listProvincias, listDistritos);
        }
        public string ObtenerDatosxID(int id)
        {
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = oAD_SocioNegocioBL.ListarxID(id);

            string listaSocio = "";
            string listaSocio_Contacto = "";
            string listaSocio_Direccion = "";
            string listaSocio_Telefono = "";
            string listaSocio_Cuenta = "";

            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaSocio = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { }, false);

                if (oResultDTO.ListaResultado[0].oListaContacto != null && oResultDTO.ListaResultado[0].oListaContacto.Count > 0)
                {
                    listaSocio_Contacto = Serializador.Serializar(oResultDTO.ListaResultado[0].oListaContacto, '▲', '▼', new string[] {
                    "idContacto","NombreCompleto","Cargo","Telefono", "Mail"}, false);
                }
                if (oResultDTO.ListaResultado[0].oListaDireccion != null && oResultDTO.ListaResultado[0].oListaDireccion.Count > 0)
                {
                    listaSocio_Direccion = Serializador.Serializar(oResultDTO.ListaResultado[0].oListaDireccion, '▲', '▼', new string[] {
                    "idDireccion","Direccion","FechaModificacion","Principal"}, false);
                }
                if (oResultDTO.ListaResultado[0].oListaTelefono != null && oResultDTO.ListaResultado[0].oListaTelefono.Count > 0)
                {
                    listaSocio_Telefono = Serializador.Serializar(oResultDTO.ListaResultado[0].oListaTelefono, '▲', '▼', new string[] {
                    "idTelefono","Telefono","FechaModificacion"}, false);
                }
                if (oResultDTO.ListaResultado[0].oListaCuentaBancaria != null && oResultDTO.ListaResultado[0].oListaCuentaBancaria.Count > 0)
                {
                    listaSocio_Cuenta = Serializador.Serializar(oResultDTO.ListaResultado[0].oListaCuentaBancaria, '▲', '▼', new string[] {
                    "idCuentaBancaria","idBanco","DesBanco","Cuenta","DescripcionCuenta","idMoneda","DesMoneda"}, false);
                }
            }
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaSocio, listaSocio_Contacto,
                listaSocio_Direccion, listaSocio_Telefono, listaSocio_Cuenta);
        }
        public string Grabar(AD_SocioNegocioDTO oAD_SocioNegocioDTO)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            string listaAD_SocioNegocio = "";
            if (oAD_SocioNegocioDTO.idSocioNegocio == 0)
            {
                oAD_SocioNegocioDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oAD_SocioNegocioDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oAD_SocioNegocioDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oResultDTO = oAD_SocioNegocioBL.UpdateInsert(oAD_SocioNegocioDTO, eSEGUsuario.idUsuario);
            ResultDTO<AD_SocioNegocioDTO> oResultDTO_Proveedores = oAD_SocioNegocioBL.ListarTodoProveedores(eSEGUsuario.idEmpresa);
            List<AD_SocioNegocioDTO> lstAD_SocioNegocioDTO = oResultDTO_Proveedores.ListaResultado;
            if (lstAD_SocioNegocioDTO != null && lstAD_SocioNegocioDTO.Count > 0)
            {
                listaAD_SocioNegocio = Serializador.Serializar(lstAD_SocioNegocioDTO, '▲', '▼', new string[] {
                "idSocioNegocio", "Documento", "RazonSocial", "FechaModificacion", "Estado" }, false);
            }
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaAD_SocioNegocio);
        }
        public string GrabarCliente(AD_SocioNegocioDTO oAD_SocioNegocioDTO)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            if (oAD_SocioNegocioDTO.idSocioNegocio == 0)
            {
                oAD_SocioNegocioDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oAD_SocioNegocioDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oAD_SocioNegocioDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oResultDTO = oAD_SocioNegocioBL.UpdateInsertCli(oAD_SocioNegocioDTO, eSEGUsuario.idUsuario);
            string lista_Cliente = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idSocioNegocio", "Documento", "RazonSocial", "FechaModificacion", "Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, lista_Cliente);
        }
        public string Eliminar(AD_SocioNegocioDTO oAD_SocioNegocioDTO)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            oAD_SocioNegocioDTO.idEmpresa = eSEGUsuario.idEmpresa;
            string listaAD_SocioNegocio = "";
            oResultDTO = oAD_SocioNegocioBL.Delete(oAD_SocioNegocioDTO, eSEGUsuario.idUsuario);
            ResultDTO<AD_SocioNegocioDTO> oResultDTO_Proveedores = oAD_SocioNegocioBL.ListarTodoProveedores(eSEGUsuario.idEmpresa);
            List<AD_SocioNegocioDTO> lstAD_SocioNegocioDTO = oResultDTO_Proveedores.ListaResultado;

            if (lstAD_SocioNegocioDTO != null && lstAD_SocioNegocioDTO.Count > 0)
            {
                listaAD_SocioNegocio = Serializador.Serializar(lstAD_SocioNegocioDTO, '▲', '▼', new string[] {
               "idSocioNegocio", "Documento", "RazonSocial", "FechaModificacion", "Estado"}, false);
            }
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaAD_SocioNegocio);
        }
        public string EliminarCliente(AD_SocioNegocioDTO oAD_SocioNegocioDTO)
        {
            ResultDTO<AD_SocioNegocioDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            oAD_SocioNegocioDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oResultDTO = oAD_SocioNegocioBL.DeleteCli(oAD_SocioNegocioDTO, eSEGUsuario.idUsuario);
            List<AD_SocioNegocioDTO> lstCliente = oResultDTO.ListaResultado;
            string lista_Cliente = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idSocioNegocio", "Documento", "RazonSocial", "FechaModificacion", "DesUsuarioModificacion", "Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, lista_Cliente);
        }

        public string ObtenerLista()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            //listas
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = oAD_SocioNegocioBL.ListarTodo(eSEGUsuario.idEmpresa);
            //cadenas
            string listaAD_SocioNegocio = "";

            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaAD_SocioNegocio = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
                "idSocioNegocio","Documento","RazonSocial"}, false);
            }
            return String.Format("{0}↔{1}↔{2}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaAD_SocioNegocio);
        }

        //public string ObtenerSocioxTipo(string tipo)
        //{
        //    Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
        //    //BL
        //    AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
        //    //listas
        //    ResultDTO<AD_SocioNegocioDTO> oResultDTO = oAD_SocioNegocioBL.ListarProv(eSEGUsuario.idEmpresa, tipo);
        //    //cadenas
        //    string listaAD_SocioNegocio = "";

        //    if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
        //    {
        //        listaAD_SocioNegocio = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
        //       "idSocioNegocio", "Documento", "RazonSocial", "FechaModificacion", "DesUsuarioModificacion", "Estado"}, false);

        //    }
        //    return String.Format("{0}↔{1}↔{2}",
        //        oResultDTO.Resultado, oResultDTO.MensajeError, listaAD_SocioNegocio);
        //}
        public string ObtenerSocioxTipo(string tipo, int idTipoComprobante = 0)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            //listas
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = oAD_SocioNegocioBL.ListarProv(eSEGUsuario.idEmpresa, tipo, idTipoComprobante);
            //cadenas
            string listaAD_SocioNegocio = "";

            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaAD_SocioNegocio = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
               "idSocioNegocio", "Documento", "RazonSocial", "FechaModificacion", "DesUsuarioModificacion", "Estado"}, false);

            }
            return String.Format("{0}↔{1}↔{2}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaAD_SocioNegocio);
        }
        public string ObtenerDireccionxID(int id)
        {
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = oAD_SocioNegocioBL.ListarxID(id);
            string listaSocio_Direccion = "";
            if (oResultDTO.ListaResultado[0].oListaDireccion.Count() > 0)
            {
                listaSocio_Direccion = Serializador.Serializar(oResultDTO.ListaResultado[0].oListaDireccion, '▲', '▼', new string[] { "idDireccion", "Direccion" }, false);
            }
            return String.Format("{0}↔{1}↔{2}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaSocio_Direccion);
        }

        /*-----------------------------------------Metodos de modificacion 3/04/2018   kevin-------------------------------------*/
        public string ObtenerDatosCliente()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //imgs
            string imageB64 = "";
            string imageB64DNI = "";
            //BL
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            Ma_PaisBL oMa_PaisBL = new Ma_PaisBL();
            Ma_TipoPersonaBL oMa_TipoPersonaBL = new Ma_TipoPersonaBL();
            Ma_TipoDocumentoBL oMa_TipoDocumentoBL = new Ma_TipoDocumentoBL();
            Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();
            Ma_UbigeoBL oMa_UbigeoBL = new Ma_UbigeoBL();
            //listas
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = oAD_SocioNegocioBL.ListarTodoCliente(eSEGUsuario.idEmpresa);
            ResultDTO<Ma_PaisDTO> lbeMa_PaisDTO = oMa_PaisBL.ListarTodo();
            ResultDTO<Ma_TipoPersonaDTO> lbeMa_TipoPersonaDTO = oMa_TipoPersonaBL.ListarTodo();
            ResultDTO<Ma_TipoDocumentoDTO> lbeMa_TipoDocumentoDTO = oMa_TipoDocumentoBL.ListarTodo();
            ResultDTO<Ma_MonedaDTO> lbeMa_MonedaDTO = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<Ma_UbigeoDTO> lbe_Departamentos = oMa_UbigeoBL.ListarDepartamentos();
            ResultDTO<Ma_UbigeoDTO> lbe_Provincias = oMa_UbigeoBL.ListarProvincias();
            ResultDTO<Ma_UbigeoDTO> lbe_Distritos = oMa_UbigeoBL.ListarDistritos();
            
            //cadenas
            string listaAD_SocioNegocio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idSocioNegocio", "Documento", "RazonSocial", "FechaModificacion", "Estado" });
            string listaMa_Pais = Serializador.rSerializado(lbeMa_PaisDTO.ListaResultado, new string[] { "idPais", "Descripcion" });
            string listaMa_TipoPersona = Serializador.rSerializado(lbeMa_TipoPersonaDTO.ListaResultado, new string[] { "idTipoPersona", "Descripcion" });
            string listaMa_TipoDocumento = Serializador.rSerializado(lbeMa_TipoDocumentoDTO.ListaResultado, new string[] { "idTipoDocumento", "Descripcion" });
            string listaMa_Moneda = Serializador.rSerializado(lbeMa_MonedaDTO.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            string idUsuario = eSEGUsuario.idUsuario.ToString();
            string listDepartamentos = Serializador.rSerializado(lbe_Departamentos.ListaResultado, new string[] { "CodigoDpto", "Departamento", "Ubigeo_ID" });
            string listProvincias = Serializador.rSerializado(lbe_Provincias.ListaResultado, new string[] { "CodigoProv", "Provincia", "Ubigeo_ID" });
            string listDistritos = Serializador.rSerializado(lbe_Distritos.ListaResultado, new string[] { "CodigoDist", "Distrito", "Ubigeo_ID" });

            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}↔{7}↔{8}↔{9}↔{10}↔{11}↔{12}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaAD_SocioNegocio, listaMa_Pais, listaMa_TipoPersona,
                listaMa_TipoDocumento, listaMa_Moneda, idUsuario, listDepartamentos, listProvincias, listDistritos, "", "");
        }
        public string ObtenerDatosProv()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //imgs
            string imageB64 = "";
            string imageB64DNI = "";
            //BL
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            Ma_PaisBL oMa_PaisBL = new Ma_PaisBL();
            Ma_TipoPersonaBL oMa_TipoPersonaBL = new Ma_TipoPersonaBL();
            Ma_TipoDocumentoBL oMa_TipoDocumentoBL = new Ma_TipoDocumentoBL();
            Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();
            Ma_UbigeoBL oMa_UbigeoBL = new Ma_UbigeoBL();
            //listas
            ResultDTO<AD_SocioNegocioDTO> oResultDTO = oAD_SocioNegocioBL.ListarTodoProveedores(eSEGUsuario.idEmpresa);
            ResultDTO<Ma_PaisDTO> lbeMa_PaisDTO = oMa_PaisBL.ListarTodo();
            ResultDTO<Ma_TipoPersonaDTO> lbeMa_TipoPersonaDTO = oMa_TipoPersonaBL.ListarTodo();
            ResultDTO<Ma_TipoDocumentoDTO> lbeMa_TipoDocumentoDTO = oMa_TipoDocumentoBL.ListarTodo();
            ResultDTO<Ma_MonedaDTO> lbeMa_MonedaDTO = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<Ma_UbigeoDTO> lbe_Departamentos = oMa_UbigeoBL.ListarDepartamentos();
            ResultDTO<Ma_UbigeoDTO> lbe_Provincias = oMa_UbigeoBL.ListarProvincias();
            ResultDTO<Ma_UbigeoDTO> lbe_Distritos = oMa_UbigeoBL.ListarDistritos();
            if (AccesoInternet())
            {
                Persona myInfo = new Persona();
                Image img = myInfo.GetCapcha;
                Image imgDNI = myInfo.GetCapchaDNI;
                try
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        img.Save(m, img.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        string base64String = Convert.ToBase64String(imageBytes);
                        imageB64 = base64String;
                    }

                }
                catch (Exception ex)
                {

                }
                try
                {
                    using (MemoryStream m2 = new MemoryStream())
                    {
                        imgDNI.Save(m2, imgDNI.RawFormat);
                        byte[] imageBytesDNI = m2.ToArray();
                        string base64StringDNI = Convert.ToBase64String(imageBytesDNI);
                        imageB64DNI = base64StringDNI;
                    }
                }
                catch (Exception ex)
                {
                }

                Session["ObjCaptcha"] = myInfo;
            }
            else
            {
                imageB64 = "Error";
                imageB64DNI = "Error";
            }
            //cadenas
            string listaAD_SocioNegocio = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { "idSocioNegocio", "Documento", "RazonSocial", "FechaModificacion", "Estado" });
            string listaMa_Pais = Serializador.rSerializado(lbeMa_PaisDTO.ListaResultado, new string[] { "idPais", "Descripcion" });
            string listaMa_TipoPersona = Serializador.rSerializado(lbeMa_TipoPersonaDTO.ListaResultado, new string[] { "idTipoPersona", "Descripcion" });
            string listaMa_TipoDocumento = Serializador.rSerializado(lbeMa_TipoDocumentoDTO.ListaResultado, new string[] { "idTipoDocumento", "Descripcion" });
            string listaMa_Moneda = Serializador.rSerializado(lbeMa_MonedaDTO.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            string idUsuario = eSEGUsuario.idUsuario.ToString();
            string listDepartamentos = Serializador.rSerializado(lbe_Departamentos.ListaResultado, new string[] { "CodigoDpto", "Departamento", "Ubigeo_ID" });
            string listProvincias = Serializador.rSerializado(lbe_Provincias.ListaResultado, new string[] { "CodigoProv", "Provincia", "Ubigeo_ID" });
            string listDistritos = Serializador.rSerializado(lbe_Distritos.ListaResultado, new string[] { "CodigoDist", "Distrito", "Ubigeo_ID" });

            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}↔{7}↔{8}↔{9}↔{10}↔{11}↔{12}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaAD_SocioNegocio, listaMa_Pais, listaMa_TipoPersona,
                listaMa_TipoDocumento, listaMa_Moneda, idUsuario, listDepartamentos, listProvincias, listDistritos, imageB64, imageB64DNI);
        }
        private bool AccesoInternet()
        {
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<Reniec> ConsultaDni(string dni)
        {
            HttpClient _httpClientReniec = new HttpClient();
            _httpClientReniec.BaseAddress = new Uri("https://catfact.ninja/");
            //_httpClientReniec.DefaultRequestHeaders.Add("Authorization", new List<string>() { $"Bearer {token}" });
            HttpResponseMessage response = await _httpClientReniec.GetAsync($"dni?numero={dni}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Reniec>(json);
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                throw new Exception(json);
            }
        }
        public JsonResult ConsultaRUC(string r)
        {
            Persona myinfo = new Persona();
            //string a = myinfo.GetInfo(r);
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            ResultDTO<ContribuyenteDTO> resultDTO = oAD_SocioNegocioBL.ConsutaRUC(r);
            ContribuyenteJson oJson = new ContribuyenteJson();
            if (resultDTO.ListaResultado.Count > 0)
            {
                ContribuyenteDTO oContribuyenteDTO = resultDTO.ListaResultado.FirstOrDefault();
                oJson.nombre_o_razon_social = oContribuyenteDTO.RazonSocial;
                oJson.ruc = oContribuyenteDTO.RUC;
                oJson.estado_del_contribuyente = oContribuyenteDTO.EstadoContribuyente;
                oJson.condicion_de_domicilio = oContribuyenteDTO.CondicionDomicilio;

                oJson.departamento = oContribuyenteDTO.Depa.ToUpper();
                oJson.provincia = oContribuyenteDTO.Prov.ToUpper();
                oJson.distrito = oContribuyenteDTO.Dist.ToUpper();

                string dir = "";
                if (!string.IsNullOrWhiteSpace(oContribuyenteDTO.TipoVia) && oContribuyenteDTO.TipoVia != "-" && oContribuyenteDTO.TipoVia != "----")
                    dir += $"{oContribuyenteDTO.TipoVia}";

                if (!string.IsNullOrWhiteSpace(oContribuyenteDTO.NombreVia) && oContribuyenteDTO.NombreVia != "-")
                    dir += $" {oContribuyenteDTO.NombreVia}";

                if (!string.IsNullOrWhiteSpace(oContribuyenteDTO.Numero) && oContribuyenteDTO.Numero != "-")
                    dir += $" NRO. {oContribuyenteDTO.Numero}";

                if (!string.IsNullOrWhiteSpace(oContribuyenteDTO.Interior) && oContribuyenteDTO.Interior != "-")
                    dir += $" INT. {oContribuyenteDTO.Interior}";

                if (!string.IsNullOrWhiteSpace(oContribuyenteDTO.Manzana) && oContribuyenteDTO.Manzana != "-")
                    dir += $" MZ. {oContribuyenteDTO.Manzana}";

                if (!string.IsNullOrWhiteSpace(oContribuyenteDTO.Lote) && oContribuyenteDTO.Lote != "-")
                    dir += $" LT. {oContribuyenteDTO.Lote}";

                if (!string.IsNullOrWhiteSpace(oContribuyenteDTO.Departamento) && oContribuyenteDTO.Departamento != "-")
                    dir += $" DPTO. {oContribuyenteDTO.Departamento}";

                if (!string.IsNullOrWhiteSpace(oContribuyenteDTO.Kilometro) && oContribuyenteDTO.Kilometro != "-")
                    dir += $" KM. {oContribuyenteDTO.Kilometro}";

                if (!string.IsNullOrWhiteSpace(oContribuyenteDTO.CodigoZona) && oContribuyenteDTO.CodigoZona != "-")
                    dir += $" {oContribuyenteDTO.CodigoZona}";

                if (!string.IsNullOrWhiteSpace(oContribuyenteDTO.TipoZona) && oContribuyenteDTO.TipoZona != "-")
                    dir += $" {oContribuyenteDTO.TipoZona}";

                oJson.direccion = dir.Trim();
                oJson.direccion_completa = $"{dir.Trim()} {oJson.departamento} - {oJson.provincia} - {oJson.distrito}";
            }
            return Json(oJson);
        }

        public class ContribuyenteJson
        {
            public string nombre_o_razon_social { get; set; }
            public string ruc { get; set; }
            public string estado_del_contribuyente { get; set; }
            public string condicion_de_domicilio { get; set; }
            public string departamento { get; set; }
            public string provincia { get; set; }
            public string distrito { get; set; }
            public string direccion { get; set; }
            public string direccion_completa { get; set; }
        }
    }
}
public class Reniec
{
    public string Nombres { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public string TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; }
    public string DigitoVerificador { get; set; }
}