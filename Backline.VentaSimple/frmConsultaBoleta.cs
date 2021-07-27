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
    public partial class frmConsultaBoleta : Form
    {
        public frmConsultaBoleta()
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

            var listaFactura = Backline.DAL.BoletaDAL.ObtenerFactura(filtro);

            grdDatos.DataSource = listaFactura;
            grdDatos.MainView.LayoutChanged();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
