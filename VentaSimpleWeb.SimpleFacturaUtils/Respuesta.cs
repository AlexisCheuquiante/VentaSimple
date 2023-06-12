using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaSimpleWeb.SimpleFacturaUtils
{
    public class Respuesta
    {
        public int Folio { get; set; }
        public int IdDocumento { get; set; }
        public string Ruta { get; set; }
        public bool EsError { get; set; }

        public string Error { get; set; }
    }
}
