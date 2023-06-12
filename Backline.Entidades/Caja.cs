using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backline.Entidades
{
    public class Caja
    {
        public int Id { get; set; }
        public int Emp_Id { get; set; }
        public int Est_Id { get; set; }
        public string Establecimiento { get; set; }
        public int Id_Cajero { get; set; }
        public string Cajero { get; set; }
        public DateTime Fecha_Cierre { get; set; }
        public int Fecha_Int { get; set; }
        public int Total { get; set; }
        public int Id_Usr_Cierra_Caja { get; set; }
        public string Observacion { get; set; }
        public string Usr_Cierra_Caja_Str { get; set; }
        public string Tipo_Pago { get; set; }
        public int Tido_Id { get; set; }
        public string Tipo_Documento
        {
            get
            {
                if (Tido_Id == 1)
                {
                    return "Boleta";
                }
                if (Tido_Id == 2)
                {
                    return "Nota de crédito";
                }
                else
                {
                    return "";
                }
            }
        }
        public bool Eliminado { get; set; }

    }
}
