using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Backline.DAL;
using DevExpress.XtraBars;
using Backline.Entidades;
using DevExpress.Utils.Extensions;
using System.IO;

namespace Backline.VentaSimple
{
    public partial class frmVenta : Form
    {
        string _ambiente;
        Entidades.Contribuyente _contribuyenteEncontrado;        
        string _proveedor;
        public frmVenta()
        {
            _ambiente = System.Configuration.ConfigurationSettings.AppSettings["Ambiente"];
            _proveedor = System.Configuration.ConfigurationSettings.AppSettings["Proveedor"];
            string titulo = _ambiente == "cer" ? "TEST" : "PRODUCCIÓN";
           
            InitializeComponent();
            this.Text = "Backline - emisión de boletas (Ambiente:" + titulo + ") v23102020 1115";
            //Verifica Directorio Ordenes de Compra
            if (!System.IO.Directory.Exists("C:\\Documentos Backline"))
            {
                System.IO.Directory.CreateDirectory("C:\\Documentos Backline");
            }
            //Verifica Directorio Boletas
            if (!System.IO.Directory.Exists(@"C:\\Documentos Backline\BOLETAS"))
            {
                System.IO.Directory.CreateDirectory(@"C:\\Documentos Backline\BOLETAS");
            }
            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 1)
                pictureBox2.Image = picLareina.Image;
            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 2)
                pictureBox2.Image = picÑuñoa.Image;
            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 3)
                pictureBox2.Image = picProvi.Image;
            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 5)
                pictureBox2.Image = picCmvm.Image;
            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 6)
                pictureBox2.Image = picHualpen.Image;
            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 7)
            {
                pictureBox2.Image = picValpo.Image;
                pictureBox2.Width = picValpo.Width;
                pictureBox2.Height = picValpo.Height;
            }

            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 8)
            {
                pictureBox2.Image = picNavia.Image;
                pictureBox2.Width = picNavia.Width;
                pictureBox2.Height = picNavia.Height;
            }

            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 9)
                pictureBox2.Image = picPudahuel.Image;

            if (VariablesGlobales.UsuarioLogeado.Emp_Id == 10)
                pictureBox2.Image = picBarnechea.Image;

            if (VariablesGlobales.UsuarioLogeado.Id==52)
            {
                btnGenerarRapido.Visible = true;
            }

        }

        bool Valida()
        {
            dxErrorProvider1.ClearErrors();

            if (txtRut.Text == "")
            {
                dxErrorProvider1.SetError(txtRut, "Debe indicar el RUT");
            }
            if (txtRut.Text != "" && Utiles.ValidaRut(txtRut.Text) == false)
            {
                dxErrorProvider1.SetError(txtRut, "El RUT indicado no es válido");
            }
            if (txtNombre.Text == "")
            {
                dxErrorProvider1.SetError(txtNombre, "Debe indicar el nombre");
            }

            if (txtCantidad.Value == 0)
            {
                dxErrorProvider1.SetError(txtCantidad, "Debe indicar la cantidad");
            }
            if (txtGlosa.Text == "")
            {
                dxErrorProvider1.SetError(txtGlosa, "Debe indicar la glosa");
            }
            if (txtValor.Value == 0)
            {
                dxErrorProvider1.SetError(txtValor, "Debe indicar el valor");
            }

            if (dxErrorProvider1.HasErrors)
                return false;

            return true;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (Valida() == false)
                return;

            splashScreenManager3.ShowWaitForm();
            btnGenerar.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            Backline.Entidades.Contribuyente contribuyente = new Backline.Entidades.Contribuyente();
            contribuyente.Razon_Social = txtNombre.Text;
            contribuyente.Rut = txtRut.Text;
            contribuyente.Rut_Code = Utiles.ObtieneRut_INT(txtRut.Text);

            //Guardo Usuario si este no existe
            if (_contribuyenteEncontrado == null)
            {
                _contribuyenteEncontrado = ContribuyenteDAL.InsertarContribuyente(contribuyente);

            }

            Backline.DTE.APIResult apiResult = new Backline.DTE.APIResult();
            int folioSII = 0;
            string rutaPDF = string.Empty;
            bool validadaSII = false;
            List<Entidades.DetalleFactura> _detalleArticulo = new List<Entidades.DetalleFactura>();

            Backline.Entidades.Factura factura = new Backline.Entidades.Factura();
            factura.Rut = txtRut.Text.ToUpper();
            factura.RazonSocial = txtNombre.Text;
            factura.Total = int.Parse(txtCantidad.Value.ToString()) * int.Parse(txtValor.Value.ToString());
            factura.Neto = Math.Round(factura.Total / 1.19, 0);
            factura.Iva = Math.Round(factura.Total - factura.Neto);

            factura.Fecha = DateTime.Now;
            factura.ContId = _contribuyenteEncontrado.Id;
            factura.Glosa = txtGlosa.Text;
            factura.Usr_Id = VariablesGlobales.UsuarioLogeado.Id;
            factura.EmpId = VariablesGlobales.UsuarioLogeado.Emp_Id;
            factura.EstId = VariablesGlobales.UsuarioLogeado.Est_Id;

            Entidades.DetalleFactura detalle = new Entidades.DetalleFactura();
            detalle.Cantidad = txtCantidad.Value;
            detalle.DescripcionProducto = txtGlosa.Text;
            detalle.Valor = txtValor.Value;

            _detalleArticulo.Add(detalle);

            if (VariablesGlobales.UsuarioLogeado.Facturador == "superfactura")
            {
                validadaSII = Utiles.GenerarBoletaElectronica(_detalleArticulo, factura, Backline.DTE.Enums.TipoDocumento.BoletaElectronicaExenta, out folioSII, out rutaPDF, out apiResult);
                factura.NumeroSII = folioSII;
                //MessageBox.Show("Número" + folioSII.ToString());
            }
          

            if (rutaPDF == null || rutaPDF == "")
            {
                MessageBox.Show("La ruta del PDF es nula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            factura = Backline.DAL.BoletaDAL.InsertarFactura(factura);
            btnGenerar.Enabled = true;
            splashScreenManager3.CloseWaitForm();
            this.Cursor = Cursors.Default;
            Visualizador frmVisualiza = new Visualizador();
            frmVisualiza.CargarPDF(rutaPDF);
            frmVisualiza.ShowDialog();

            AccionLimpiar();
           
        }

        string ObtenerBoleta(long numero, int TipoDTE)
        {
            var filename = Guid.NewGuid().ToString() + ".pdf";
            var fileLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

            string descripcionOperacion;
            string rut = VariablesGlobales.UsuarioLogeado.RutEmpresa;
            int tipoDTE;
            int.TryParse(TipoDTE.ToString(), out tipoDTE);
            long folioDTE;
            long.TryParse(numero.ToString(), out folioDTE);
            byte[] PDF;
            string XML;
            string URL;
            //client.obtienePDF(rut, tipoDTE, folioDTE, out descripcionOperacion, out XML, out PDF, out URL);
            //client.grabaPDF(PDF, fileLocation);
            //client.obtieneURLPDF(rut, tipoDTE, folioDTE, out descripcionOperacion, out XML, out PDF, out URL);
            //lblLinkPDF.Enabled = true;
            //lblLinkPDF.Text = URL;
            return fileLocation;
        }

        //private string obtieneXMLBoleta(Entidades.Factura factura)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //    sb.Append("<DTE version=\"1.0\">");
        //    sb.Append("<Documento>");
        //    sb.Append("<Encabezado>");
        //    sb.Append("<IdDoc>");
        //    sb.Append("<TipoDTE>39</TipoDTE>");
        //    sb.Append("<Folio>0</Folio>");
        //    sb.Append("<FchEmis>" + FacEleUtils.Utiles.ObtenerFecha(DateTime.Now) + "</FchEmis>");
        //    sb.Append("<IndServicio>1</IndServicio>");
        //    sb.Append("<IndMntNeto>2</IndMntNeto>");
        //    sb.Append("</IdDoc>");
        //    sb.Append("<Emisor>");
        //    sb.Append("<RUTEmisor>" + VariablesGlobales.UsuarioLogeado.RutEmpresa + "</RUTEmisor>");
        //    sb.Append("<RznSocEmisor>" + VariablesGlobales.UsuarioLogeado.NombreEmpresa + "</RznSocEmisor>");
        //    sb.Append("<GiroEmisor>Giro</GiroEmisor>");
        //    sb.Append("<DirOrigen>Direccion</DirOrigen>");
        //    sb.Append("<CmnaOrigen>La Reina</CmnaOrigen>");
        //    sb.Append("<CiudadOrigen>Santiago</CiudadOrigen>");
        //    sb.Append("</Emisor>");
        //    sb.Append("<Receptor>");
        //    sb.Append("<RUTRecep>" + factura.Rut + "</RUTRecep>");
        //    sb.Append("<RznSocRecep>" + factura.RazonSocial + "</RznSocRecep>");
        //    sb.Append("<Contacto>contacto</Contacto>");
        //    sb.Append("<DirRecep>...</DirRecep>");
        //    sb.Append("<CmnaRecep>La Reina</CmnaRecep>");
        //    sb.Append("<CiudadRecep>Santiago</CiudadRecep>");
        //    sb.Append("</Receptor>");
        //    sb.Append("<Totales>");
        //    sb.Append("<MntNeto>" + factura.Neto.ToString() + "</MntNeto>");
        //    sb.Append("<MntExe>0</MntExe>");
        //    sb.Append("<IVA>" + factura.Iva.ToString() + "</IVA>");
        //    sb.Append("<MntTotal>" + factura.Total.ToString() + "</MntTotal>");
        //    sb.Append("<VlrPagar>" + factura.Total.ToString() + "</VlrPagar>");
        //    sb.Append("</Totales>");
        //    sb.Append("</Encabezado>");
        //    sb.Append("<Detalle>");

        //    sb.Append("<NroLinDet>1</NroLinDet>");
        //    sb.Append("<NmbItem>" + txtGlosa.Text + "</NmbItem>");
        //    sb.Append("<QtyItem>" + txtCantidad.EditValue.ToString() + "</QtyItem>");
        //    sb.Append("<UnmdItem></UnmdItem>");
        //    sb.Append("<PrcItem>" + factura.Neto.ToString() + "</PrcItem>");
        //    sb.Append("<MontoItem>" + factura.Neto.ToString() + "</MontoItem>");
        //    sb.Append("</Detalle>");


        //    sb.Append("</Documento>");
        //    sb.Append("</DTE>");

        //    return sb.ToString();
        //}

        //private string obtieneXMLBoletaExcenta(Entidades.Factura factura)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //    sb.Append("<DTE version=\"1.0\">");
        //    sb.Append("<Documento>");
        //    sb.Append("<Encabezado>");
        //    sb.Append("<IdDoc>");
        //    sb.Append("<TipoDTE>41</TipoDTE>");
        //    sb.Append("<Folio>0</Folio>");
        //    sb.Append("<FchEmis>" + FacEleUtils.Utiles.ObtenerFecha(DateTime.Now) + "</FchEmis>");
        //    sb.Append("<IndServicio>1</IndServicio>");

        //    sb.Append("</IdDoc>");
        //    sb.Append("<Emisor>");
        //    sb.Append("<RUTEmisor>"  + VariablesGlobales.UsuarioLogeado.RutEmpresa  + "</RUTEmisor>");
        //    sb.Append("<RznSocEmisor>" + VariablesGlobales.UsuarioLogeado.NombreEmpresa + "</RznSocEmisor>");
        //    sb.Append("<GiroEmisor>Giro</GiroEmisor>");
        //    sb.Append("<DirOrigen>Direccion</DirOrigen>");
        //    sb.Append("<CmnaOrigen>La Reina</CmnaOrigen>");
        //    sb.Append("<CiudadOrigen>Santiago</CiudadOrigen>");
        //    sb.Append("</Emisor>");
        //    sb.Append("<Receptor>");
        //    sb.Append("<RUTRecep>" + factura.Rut + "</RUTRecep>");
        //    sb.Append("<RznSocRecep>" + factura.RazonSocial + "</RznSocRecep>");
        //    sb.Append("<Contacto>contacto</Contacto>");
        //    sb.Append("<DirRecep>...</DirRecep>");
        //    sb.Append("<CmnaRecep>La Reina</CmnaRecep>");
        //    sb.Append("<CiudadRecep>Santiago</CiudadRecep>");
        //    sb.Append("</Receptor>");
        //    sb.Append("<Totales>");

        //    sb.Append("<MntExe>" + factura.Total.ToString() + "</MntExe>");
        //    //sb.Append("<IVA>0</IVA>");
        //    sb.Append("<MntTotal>" + factura.Total.ToString() + "</MntTotal>");
        //    sb.Append("<VlrPagar>0</VlrPagar>");
        //    sb.Append("</Totales>");
        //    sb.Append("</Encabezado>");
        //    sb.Append("<Detalle>");
        //    sb.Append("<NroLinDet>1</NroLinDet>");
        //    sb.Append("<IndExe>1</IndExe>");
        //    sb.Append("<NmbItem>" + txtGlosa.Text + "</NmbItem>");
        //    sb.Append("<QtyItem>" + txtCantidad.EditValue.ToString() + "</QtyItem>");
        //    sb.Append("<UnmdItem/>");
        //    sb.Append("<PrcItem>" + factura.Total.ToString() + "</PrcItem>");
        //    sb.Append("<MontoItem>" + factura.Total.ToString() + "</MontoItem>");
        //    sb.Append("</Detalle>");
        //    sb.Append("</Documento>");
        //    sb.Append("</DTE>");


        //    return sb.ToString();
        //}


        private void txtRut_Leave(object sender, EventArgs e)
        {
            txtRut.Text = Utiles.FormateaRut(txtRut.Text);
            if (!Utiles.ValidaRut(txtRut.Text))
            {
                txtRut.Focus();
                return;
            }

            if (txtRut.Text.Trim() == string.Empty)
                return;


            Entidades.Filtro filtro = new Entidades.Filtro();
            filtro.RutCode = Utiles.ObtieneRut_INT(txtRut.Text);
            var resultado = DAL.ContribuyenteDAL.ObtenerContribuyente(filtro);
            if (resultado != null && resultado.Count == 1)
            {
                txtNombre.Text = resultado[0].Razon_Social;
                _contribuyenteEncontrado = resultado[0];
            }
            else
            {
                txtNombre.Text = "";
                _contribuyenteEncontrado = null;
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            AccionLimpiar();
        }

        void AccionLimpiar()
        {
            txtRut.Text = "";
            txtNombre.Text = "";

            txtCantidad.Value = 0;
            txtGlosa.Text = "";
            txtValor.Value = 0;
        }

        private void txtRut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNombre.Focus();
            }
        }

        private void btnGenerarRapido_Click(object sender, EventArgs e)
        {
           

            splashScreenManager3.ShowWaitForm();
            btnGenerar.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            Backline.Entidades.Contribuyente contribuyente = new Backline.Entidades.Contribuyente();
            contribuyente.Razon_Social = txtNombre.Text;
            contribuyente.Rut = txtRut.Text;
            contribuyente.Rut_Code = Utiles.ObtieneRut_INT(txtRut.Text);

            //Guardo Usuario si este no existe
            if (_contribuyenteEncontrado == null)
            {
                _contribuyenteEncontrado = ContribuyenteDAL.InsertarContribuyente(contribuyente);

            }

            Backline.DTE.APIResult apiResult = new Backline.DTE.APIResult();
            int folioSII = 0;
            string rutaPDF = string.Empty;
            bool validadaSII = false;
            List<Entidades.DetalleFactura> _detalleArticulo = new List<Entidades.DetalleFactura>();

            Backline.Entidades.Factura factura = new Backline.Entidades.Factura();
            factura.Rut = "";
            factura.RazonSocial = "";
            factura.Total = 700;
            factura.Neto = 588;
            factura.Iva = 112;

            factura.Fecha = DateTime.Now;
            factura.ContId = _contribuyenteEncontrado.Id;
            factura.Glosa = txtGlosa.Text;
            factura.Usr_Id = VariablesGlobales.UsuarioLogeado.Id;
            factura.EmpId = VariablesGlobales.UsuarioLogeado.Emp_Id;
            factura.EstId = VariablesGlobales.UsuarioLogeado.Est_Id;

            Entidades.DetalleFactura detalle = new Entidades.DetalleFactura();
            detalle.Cantidad = 1;
            detalle.DescripcionProducto = "Peaje cementerio";
            detalle.Valor = 700;


            _detalleArticulo.Add(detalle);

            if (VariablesGlobales.UsuarioLogeado.Facturador == "superfactura")
            {
                validadaSII = Utiles.GenerarBoletaElectronicaRapida(_detalleArticulo, factura, Backline.DTE.Enums.TipoDocumento.BoletaElectronicaExenta, out folioSII, out rutaPDF, out apiResult);
                factura.NumeroSII = folioSII;
                //MessageBox.Show("Número" + folioSII.ToString());
            }


            if (rutaPDF == null || rutaPDF == "")
            {
                MessageBox.Show("La ruta del PDF es nula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            factura = Backline.DAL.BoletaDAL.InsertarFactura(factura);
            btnGenerar.Enabled = true;
            splashScreenManager3.CloseWaitForm();
            this.Cursor = Cursors.Default;
            Visualizador frmVisualiza = new Visualizador();
            frmVisualiza.CargarPDF(rutaPDF);
            frmVisualiza.ShowDialog();

            AccionLimpiar();
        }
    }
}
