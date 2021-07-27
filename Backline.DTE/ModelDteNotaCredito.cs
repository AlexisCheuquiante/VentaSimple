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
    public class ModelDteNotaCredito
    {
        /// <summary>
        /// Encabezado DTE
        /// </summary>
        public EncabezadoNotaCredito Encabezado { get; set; }
        /// <summary>
        /// Lista Detalle Transacciones
        /// </summary>
        public List<DetalleNotaCredito> Detalles { get; set; }

        public ReferenciaNotaCredito Referencia { get; set; }
    }

    /// <summary>
    /// Sección Encabezado Documento DTE
    /// </summary>
    public class EncabezadoNotaCredito
    {
        /// <summary>
        /// Información Documento
        /// </summary>
        public DocumentoNotaCredito IdDoc { get; set; }

       
        /// <summary>
        /// Información Emisor
        /// </summary>
        public EmisorNotaCredito Emisor { get; set; }
        /// <summary>
        /// Información Receptor
        /// </summary>
        public ReceptorNotaCredito Receptor { get; set; }

        

    }

    public class ReferenciaNotaCredito
    {
        public string RazonRef { get; set; }

        public string CodVndor { get; set; }

        public string CodCaja { get; set; }

        public int CodRef { get; set; }
        /// <summary>
        /// Información Documento
        /// </summary>
        public int TpoDocRef { get; set; }
        /// <summary>
        /// Información Emisor
        /// </summary>
        public int FolioRef { get; set; }
        /// <summary>
        /// Información Receptor
        /// </summary>
        public string FchRef { get; set; }

   


    }

    /// <summary>
    /// Detalle Transaccion
    /// </summary>
    public class DetalleNotaCredito
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
    public class DocumentoNotaCredito
    {
        /// <summary>
        /// Tipo Documento
        /// </summary>
        public int TipoDTE { get; set; }

        public int IndServicio { get; set; }

        //public int PeriodoDesde { get; set; }

        //public int PeriodoHasta { get; set; }

        public string FchVenc { get; set; }

        public string IndMntNeto { get; set; }

        public int MntBruto { get; set; }

    }

  

    /// <summary>
    /// Sección Emisor
    /// </summary>
    public class EmisorNotaCredito
    {
        /// <summary>
        /// Rut Emisor de Documento
        /// </summary>
        public string RUTEmisor { get; set; }
    }

    /// <summary>
    /// Sección Receptor 
    /// </summary>
    public class ReceptorNotaCredito
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
