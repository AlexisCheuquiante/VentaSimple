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
    public partial class frmLogin : Form
    {
        int _contador = 0;
        public frmLogin()
        {
            InitializeComponent();

            txtUsuario.Text = "usrñuñoa";
            txtPassword.Text = "123456";
        }

        bool ValidaLogin()
        {
            dxErrorProvider1.ClearErrors();

            if (txtUsuario.Text.Trim() == string.Empty)
            {
                dxErrorProvider1.SetError(txtUsuario, "Debe indicar el nombre");
            }

            if (txtPassword.Text.Trim() == string.Empty)
            {
                dxErrorProvider1.SetError(txtPassword, "Debe indicar la password");
            }

            if (dxErrorProvider1.HasErrors)
                return false;

            return true;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidaLogin() == false)
                return;

            Backline.Entidades.Usuario usuarios = new Entidades.Usuario();
            usuarios.NombreUsuario = txtUsuario.Text;
            usuarios.Password = txtPassword.Text;

            var listaUsuarios = Backline.DAL.UsuariosDAL.ObtenerUsuario(usuarios);
            if (listaUsuarios != null && listaUsuarios.Count == 1)
            {
                this.Visible = false;
                VariablesGlobales.UsuarioLogeado = listaUsuarios[0];
                frmMenu frm = new frmMenu();
                frm.Show();
                
            }
            else
            {
                MessageBox.Show("No se ha encontrado el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("¿Desea salir del programa?","Salir", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                Application.Exit();
            }
        }

        private void lbUsuario_Click(object sender, EventArgs e)
        {
            _contador = _contador + 1;
        }
    }
}
