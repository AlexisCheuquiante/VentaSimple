using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backline.Entidades
{
    public class Prestaciones
    {
        public int Id { get; set; }
        public int Emp_Id { get; set; }
        public int Est_Id { get; set; }
        public string Nombre_Establecimiento { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Valor { get; set; }
        public bool Valor_Libre { get; set; }
        public bool Eliminado { get; set; }
    }
}
