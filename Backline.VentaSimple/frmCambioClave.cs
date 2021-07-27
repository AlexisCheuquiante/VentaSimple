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
    public partial class frmCambioClave : Form
    {
        public frmCambioClave()
        {
            InitializeComponent();
        }

        bool Valida()
        {

            dxErrorProvider2.ClearErrors();

            if (txtClave1.Text.Trim().Length == 0)
            {
                dxErrorProvider2.SetError(txtClave1, "Debe indicar la clave");
            }
            if (txtClave2.Text.Trim().Length == 0)
            {
                dxErrorProvider2.SetError(txtClave2, "Debe repetir la clave");
            }

            if (txtClave1.Text.Trim().Length != 0 && txtClave2.Text.Trim().Length != 0)
            {
                if (txtClave1.Text != txtClave2.Text)
                {
                    dxErrorProvider2.SetError(txtClave1, "Las contraseñas no coinciden");
                    dxErrorProvider2.SetError(txtClave2, "Las contraseñas no coinciden");
                }
                
            }


            if (dxErrorProvider2.HasErrors)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            if (Valida() == false)
                return;

            if (DialogResult.OK == MessageBox.Show("¿Desea cambiar su contraseñas?", "Contraseña", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                VariablesGlobales.UsuarioLogeado.Password = txtClave1.Text;
                DAL.UsuariosDAL.CambiarClave(VariablesGlobales.UsuarioLogeado);
                MessageBox.Show("Su contraseña ha sido cambiada", "Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

          
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
