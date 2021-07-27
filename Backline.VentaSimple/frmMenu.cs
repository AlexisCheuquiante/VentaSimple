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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
            txtEstablecimiento.Text = VariablesGlobales.UsuarioLogeado.NombreEstablecimiento;
            txtUsuario.Text = VariablesGlobales.UsuarioLogeado.Nombre;
            txtAfecta.Text = VariablesGlobales.UsuarioLogeado.EsAfecta == true ? "SI" : "NO";

            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 1)
                pictureBox1.Image = picLareina.Image;
            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 2)
                pictureBox1.Image = picÑuñoa.Image;
            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 3)
                pictureBox1.Image = picProvi.Image;
            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 5)
                pictureBox1.Image = picCmvm.Image;
            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 6)
                pictureBox1.Image = picHualpen.Image;

            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 7)
            {
                pictureBox1.Image = picValpo.Image;
                pictureBox1.Width = picValpo.Width;
                pictureBox1.Height = picValpo.Height;
            }

            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 8)
            {
                pictureBox1.Image = picNavia.Image;
                pictureBox1.Width = picNavia.Width;
                pictureBox1.Height = picNavia.Height;
            }

            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 9)
                pictureBox1.Image = picPudahuel.Image;

            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 10)
                pictureBox1.Image = picBarnechea.Image;
        }

        private void btnGenerarBoleta_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            frmVenta frm = new frmVenta();
            frm.Show();
        }

        private void btnConsultarBoleta_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            frmListaBoletas frm = new frmListaBoletas();
            frm.Cargar();
            frm.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("¿Desea salir del programa?", "Salir", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                Application.Exit();
            }
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {
            frmCambioClave frm = new frmCambioClave();
            frm.ShowDialog();
        }
    }
}
