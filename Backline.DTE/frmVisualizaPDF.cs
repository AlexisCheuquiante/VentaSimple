using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backline.DTE
{
    public partial class frmVisualizaPDF : Form
    {
        public frmVisualizaPDF()
        {
            InitializeComponent();
        }
        public void CargarPDF(string ruta)
        {
            pdfViewer1.LoadDocument(ruta);
        }
    }
}
