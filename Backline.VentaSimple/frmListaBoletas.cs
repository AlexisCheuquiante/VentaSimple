using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backline.VentaSimple
{
    public partial class frmListaBoletas : Form
    {
        public frmListaBoletas()
        {
            InitializeComponent();
        }

        public void Cargar()
        {
            txtDesde.DateTime = DateTime.Now;
            txtHasta.DateTime = DateTime.Now;

            Buscar();
        }

        void Buscar()
        {
            Backline.Entidades.Filtro filtro = new Entidades.Filtro();
            filtro.Desde = Utiles.ObtenerHoraMinima(txtDesde.DateTime);
            filtro.Hasta = Utiles.ObtenerHoraMaxima(txtHasta.DateTime);
            filtro.EmpId = VariablesGlobales.UsuarioLogeado.Emp_Id;

            var listaFactura = Backline.DAL.BoletaDAL.ObtenerFactura(filtro);

            grdDatos.DataSource = listaFactura;
            grdDatos.MainView.LayoutChanged();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            Buscar();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string nombre = "C:\\Documentos Backline\\Boletas_" + Utiles.ObtenerTimeStamp() + ".xlsx";
                grdDatos.ExportToXlsx(nombre);
                System.Diagnostics.Process.Start(nombre);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Es probable que exista este mismo Documento en ejecución, por favor cierrelo y vuelva a intentarlo.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
