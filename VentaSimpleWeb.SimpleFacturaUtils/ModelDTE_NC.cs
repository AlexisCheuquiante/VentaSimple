using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaSimpleWeb.SimpleFacturaUtils
{
    public class NotaCredito
    {
        public credenciales credenciales { get; set; }
        public dteReferenciadoExterno dteReferenciadoExterno { get; set; }

        public int tipoNota { get; set; }
        public int motivo { get; set; }
        public string razon { get; set; }
        public bool notaDebito { get; set; }
        public string MontoItem { get; set; }
    }
    public class ModelDTE_NC
    {
        public int tipoNota { get; set; }
        public int motivo { get; set; }
        public string razon { get; set; }
        public bool notaDebito { get; set; }
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
