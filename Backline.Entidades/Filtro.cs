using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backline.Entidades
{
    public class Filtro
    {
        public int RutCode { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public int BoletaId { get; set; }
        public int EmpId { get; set; }
        public int EstId { get; set; }
        public int ContId { get; set; }
        public int UsrId { get; set; }
        public bool EsAdministrador { get; set; }
        public string Clave_Autorizacion { get; set; }
        public int FolioSii { get; set; }
    }
}
