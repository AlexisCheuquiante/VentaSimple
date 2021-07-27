using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backline.Entidades
{
    public class Contribuyente
    {
        public int Id { get; set;}
        public string Razon_Social { get; set;}
        public string Rut { get; set;}
        public int Rut_Code { get; set;}
        public bool Eliminado { get; set;}
        public string Facturador { get; set; }
        public string Ambiente { get; set; }
    }
}
