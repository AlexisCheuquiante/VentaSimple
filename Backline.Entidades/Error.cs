using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backline.Entidades
{
    public class Error
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int EstId { get; set; }
        public int UsrId { get; set; }
        public DateTime Fecha { get; set; }
        public string Modulo { get; set; }
        public string DetalleError { get; set; }
    }
}
