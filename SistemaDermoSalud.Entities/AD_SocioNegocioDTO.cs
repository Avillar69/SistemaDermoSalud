using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
  public  class AD_SocioNegocioDTO
    {
        public int idSocioNegocio { get; set; }
        public string CodigoGenerado { get; set; }
        public int idEmpresa { get; set; }
        public int idTipoPersona { get; set; }
        public string RazonSocial { get; set; }
        public int idTipoDocumento { get; set; }
        public string Documento { get; set; }
        public int idPais { get; set; }
        public string idDepartamento { get; set; }
        public string idProvincia { get; set; }
        public string idDistrito { get; set; }
        public string Web { get; set; }
        public string Mail { get; set; }
        public bool Cliente { get; set; }
        public bool Proveedor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }

        public string Direccion { get; set; }
        //descripciones de id
        public string DesTipoDocumento { get; set; }
        public string DesUsuarioModificacion { get; set; }
        //Detalles
        public string Lista_Contacto { get; set; }
        public string Lista_Direccion { get; set; }
        public string Lista_Telefono { get; set; }
        public string Lista_CuentaBancaria { get; set; }
        public List<AD_SocioNegocio_ContactoDTO> oListaContacto { get; set; }
        public List<AD_SocioNegocio_DireccionDTO> oListaDireccion { get; set; }
        public List<AD_SocioNegocio_TelefonoDTO> oListaTelefono { get; set; }
        public List<AD_SocioNegocio_CuentaBancariaDTO> oListaCuentaBancaria { get; set; }
    }
    //objecto lista detalle
    public class AD_SocioNegocio_ContactoDTO
    {
        public int idContacto { get; set; }
        public int idSocioNegocio { get; set; }
        public int idEmpresa { get; set; }
        public string NombreCompleto { get; set; }
        public string Cargo { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
    }
    public class AD_SocioNegocio_CuentaBancariaDTO
    {
        public int idCuentaBancaria { get; set; }
        public int idSocioNegocio { get; set; }
        public int idEmpresa { get; set; }
        public int idBanco { get; set; }
        public string DescripcionCuenta { get; set; }
        public string Cuenta { get; set; }
        public int idMoneda { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        //Descripciones
        public string DesBanco { get; set; }
        public string DesMoneda { get; set; }
    }
    public class AD_SocioNegocio_DireccionDTO
    {
        public int idDireccion { get; set; }
        public int idSocioNegocio { get; set; }
        public int idEmpresa { get; set; }
        public string Direccion { get; set; }
        public bool Principal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
    }
    public class AD_SocioNegocio_TelefonoDTO
    {
        public int idTelefono { get; set; }
        public int idSocioNegocio { get; set; }
        public int idEmpresa { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
    }

    public class ContribuyenteDTO
    {
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string EstadoContribuyente { get; set; }
        public string CondicionDomicilio { get; set; }
        public string Ubigeo { get; set; }
        public string TipoVia { get; set; }
        public string NombreVia { get; set; }
        public string CodigoZona { get; set; }
        public string TipoZona { get; set; }
        public string Numero { get; set; }
        public string Interior { get; set; }
        public string Lote { get; set; }
        public string Departamento { get; set; }
        public string Manzana { get; set; }
        public string Kilometro { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public string Depa { get; set; }
        public string Prov { get; set; }
        public string Dist { get; set; }
    }

}
