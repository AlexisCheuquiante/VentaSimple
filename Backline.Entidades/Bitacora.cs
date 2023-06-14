using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backline.Entidades
{
    public class Bitacora
    {
        public int Id { get; set; }
        public int Emp_Id { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaMostrar
        {
            get
            {
                return Fecha.ToString();
            }
        }
        public int Mod_Id { get; set; }
        public string Modulo { get; set; }
        public string Observacion { get; set; }
        public int Usr_Id { get; set; }
        public string Usuario { get; set; }
        public int Est_Id { get; set; }
        public string Establecimiento { get; set; }
        public bool Eliminado { get; set; }
    }
}
