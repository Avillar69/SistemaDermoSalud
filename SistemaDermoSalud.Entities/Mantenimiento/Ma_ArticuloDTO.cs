using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities.Mantenimiento
{
   public class Ma_ArticuloDTO
    {

        public int idArticulo { get; set; }
        public int idEmpresa { get; set; }
        public string CodigoAutogenerado { get; set; }
        public string CodigoProducto { get; set; }
        public string CodigoBarras { get; set; }
        public decimal CantidadMin { get; set; }
        public string Descripcion { get; set; }
        public int idMarca { get; set; }
        public int idCategoria { get; set; }
        public int idUnidadMedida { get; set; }
        public int idClaseArticulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string DescripcionAlter { get; set; }
        //descripciones de id
        public string DesEmpresa { get; set; }
        public string DesMarca { get; set; }
        public string DesCategoria { get; set; }
        public string DesUnidadMedida { get; set; }
        public string DesUsuarioModificacion { get; set; }

        //listaDetalle
        public string Lista_Almacen { get; set; }
        public List<Ma_AlmacenDTO> oListaAlmacen = new List<Ma_AlmacenDTO>();

        //Precios x Proveedor 
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }

        //Articulo diseño
        public string artDetalleDiseño { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime Fecha { get; set; }
        public string Extension { get; set; }
        public string Archivo { get; set; }
        public string DescripcionDiseño { get; set; }
        public List<Ma_ArticuloDisenoDTO> oListaDiseño = new List<Ma_ArticuloDisenoDTO>();

    }
}
