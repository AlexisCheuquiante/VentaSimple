using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backline.Entidades
{
    public class Imagen
    {
        public int ID { get; set; }
        public int EMP_ID { get; set; }
        public int SOL_ID { get; set; }
        public byte[] IMAGEN { get; set; }
        public string RUTA_IMAGEN { get; set; }
        public string NOMBRE_IMAGEN { get; set; }
        public string RUTA_IMAGEN_THUMB { get; set; }
        public bool ES_OBSERVACION { get; set; }
    }
}
