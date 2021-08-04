using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backline.Entidades
{
    public class Establecimiento
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string NombreEstablecimiento { get; set; }
        public bool AfectaIva { get; set; }
    }
}
