using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaSimpleWeb.SimpleFacturaUtils
{
    public class ModelDTE_Credenciales
    {
        public credenciales credenciales { get; set; }
        public dteReferenciadoExterno dteReferenciadoExterno { get; set; }
    }

    public class credenciales
    {
        public string rutEmisor { get; set; }
        public string rutContribuyente { get; set; }
        public string nombreSucursal { get; set; }
    }

    public class dteReferenciadoExterno
    {
        public int folio { get; set; }
        public int codigoTipoDte { get; set; }
        public int ambiente { get; set; }
    }
}
