using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backline.DTE
{
    /// <summary>
    /// Clase Modelo DTE
    /// </summary>
    public class ModelDte
    {
        /// <summary>
        /// Encabezado DTE
        /// </summary>
        public Encabezado Encabezado { get; set; }
        /// <summary>
        /// Lista Detalle Transacciones
        /// </summary>
        public List<Detalle> Detalles { get; set; }

        public Referencia Referencia { get; set; }

        public SuperFactura SuperFactura { get; set; }
    }

    public class SuperFactura
    {
        public string Sucursal { get; set; }
        public string HoraEmis { get; set; }
        public string Cajera { get; set; }
        public string Vuelto { get; set; }
        public string Observaciones { get; set; }
    }
    /// <summary>
    /// Sección Encabezado Documento DTE
    /// </summary>
    public class Encabezado
    {
        /// <summary>
        /// Información Documento
        /// </summary>
        public Documento IdDoc { get; set; }
        /// <summary>
        /// Información Emisor
        /// </summary>
        public Emisor Emisor { get; set; }
        /// <summary>
        /// Información Receptor
        /// </summary>
        public Receptor Receptor { get; set; }

        //public Referencia Referencia { get; set; }

    }

    public class Referencia
    {
        public string CodVndor { get; set; }
        public string CodCaja { get; set; }
    }

    /// <summary>
    /// Detalle Transaccion
    /// </summary>
    public class Detalle
    {
        /// <summary>
        /// Nombre Item
        /// </summary>
        public string NmbItem { get; set; }
        /// <summary>
        /// Descripcion Item
        /// </summary>
        public string DscItem { get; set; }
        /// <summary>
        /// Cantidad
        /// </summary>
        public int QtyItem { get; set; }

        public int DescuentoMonto { get; set; }
        /// <summary>
        /// Unidad Medida
        /// </summary>
        public string UnmdItem { get; set; }
        /// <summary>
        /// Precio
        /// </summary>
        public decimal PrcItem { get; set; }

        
    }

    /// <summary>
    /// Sección documento
    /// </summary>
    public class Documento
    {
        /// <summary>
        /// Tipo Documento
        /// </summary>
        public int TipoDTE { get; set; }

        public int IndServicio { get; set; }

        //public int MntBruto { get; set; }

    }

  

    /// <summary>
    /// Sección Emisor
    /// </summary>
    public class Emisor
    {
        /// <summary>
        /// Rut Emisor de Documento
        /// </summary>
        public string RUTEmisor { get; set; }
        public string GiroEmis { get; set; }
    }

    /// <summary>
    /// Sección Receptor 
    /// </summary>
    public class Receptor
    {
        /// <summary>
        /// Rut Receptor documento
        /// </summary>
        public string RUTRecep { get; set; }
        /// <summary>
        /// Razón Social Receptor
        /// </summary>
        public string RznSocRecep { get; set; }
        /// <summary>
        /// Dirección Receptor
        /// </summary>
        public string DirRecep { get; set; }
        /// <summary>
        /// Comuna Receptor
        /// </summary>
        public string CmnaRecep { get; set; }
        /// <summary>
        /// Ciudad Receptor
        /// </summary>
        public string CiudadRecep { get; set; }

        public string DirPostal { get; set; }
        public string CmnaPostal { get; set; }
        public string CiudadPostal { get; set; }

        public string Contacto { get; set; }
        /// <summary>
        /// Giro Receptor
        /// </summary>
        public string GiroRecep { get; set; }
    }
}
