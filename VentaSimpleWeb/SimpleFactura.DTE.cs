using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace VentaSimpleWeb
{
    public class SimpleFactura
    {
        public class Dte
        {
            public Documento Documento { get; set; }
        }
        public class Documento
        {
            public Encabezado Encabezado { get; set; }

            public List<Detalle> Detalle { get; set; }
        }

        public class Encabezado
        {
            public IdDoc IdDoc { get; set; }
            public Emisor Emisor { get; set; }
            public Receptor Receptor { get; set; }
            public Totales Totales { get; set; }


        }
        public class IdDoc
        {
            public int TipoDTE { get; set; }
            public string FchEmis { get; set; }
            //public int FmaPago { get; set; }
            public string FchVenc { get; set; }
        }

        public class Emisor
        {
            public string RUTEmisor { get; set; }
            public string RznSocEmisor { get; set; }
            public string GiroEmisor { get; set; }
            public string DirOrigen { get; set; }
            public string CmnaOrigen { get; set; }
            public string CiudadOrigen { get; set; }
        }

        public class Receptor
        {
            public string RUTRecep { get; set; }
            public string RznSocRecep { get; set; }
            public string CorreoRecep { get; set; }
            public string DirRecep { get; set; }

            public string CmnaRecep { get; set; }
            public string CiudadRecep { get; set; }
        }

        public class Totales
        {
            public string MntNeto { get; set; }
            public string IVA { get; set; }
            public string MntExe { get; set; }
            public string MntTotal { get; set; }
        }

        public class Detalle
        {
            public string NroLinDet { get; set; }
            public string NmbItem { get; set; }

            public string QtyItem { get; set; }
            public string UnmdItem { get; set; }
            public string PrcItem { get; set; }
            public string MontoItem { get; set; }

            public int IndExe { get; set; }

        }
    }
}