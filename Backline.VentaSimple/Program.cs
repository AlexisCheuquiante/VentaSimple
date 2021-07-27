using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backline.VentaSimple
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Verifica Directorio Ordenes de Compra
            if (!System.IO.Directory.Exists("C:\\Documentos Backline"))
            {
                System.IO.Directory.CreateDirectory("C:\\Documentos Backline");
                MessageBox.Show("Se ha creado el directorio C:\\Documentos Backline.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
