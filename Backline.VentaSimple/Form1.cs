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

using System.IO;

namespace Backline.VentaSimple
{
    public partial class Form1 : Form
    {
        string _ambiente;
        string _proveedor;        
        public Form1()
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
            if (memoDescripcion.Text == "")
            {
                dxErrorProvider1.SetError(memoDescripcion, "Debe indicar la glosa");
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

            Backline.DTE.APIResult apiResult = new Backline.DTE.APIResult();
            int folioSII = 0;
            string rutaPDF = string.Empty;
            bool validadaSII = false;
            List<Entidades.DetalleFactura> _detalleArticulo = new List<Entidades.DetalleFactura>();

            Backline.Entidades.Factura factura = new Backline.Entidades.Factura();
            factura.Rut = txtRut.Text;
            factura.RazonSocial = txtNombre.Text;
            factura.Total = int.Parse(txtCantidad.Value.ToString()) * int.Parse(txtValor.Value.ToString());
            factura.Neto = Math.Round(factura.Total / 1.19,0);
            factura.Iva = Math.Round(factura.Total - factura.Neto);

            factura.Fecha  = DateTime.Now;

            Entidades.DetalleFactura detalle = new Entidades.DetalleFactura();
            detalle.Cantidad = txtCantidad.Value;
            detalle.DescripcionProducto = memoDescripcion.Text;
            detalle.Valor = txtValor.Value;

            _detalleArticulo.Add(detalle);

            if (_proveedor == "superfactura")
            {
                validadaSII = Utiles.GenerarBoletaElectronica(_detalleArticulo, factura, Backline.DTE.Enums.TipoDocumento.BoletaElectronicaExenta, out folioSII, out rutaPDF, out apiResult);
            }
            //if (_proveedor == "facele")
            //{
            //    generaDTEFormato formato = generaDTEFormato.XML;
            //    string xml = obtieneXMLBoleta(factura);
               
            //    string descripcionOperacion;
            //    long folio;
            //    string rut = "71378000-6";
            //    int tipoDTE = 39;
            //    var salida = client.generaDTE(ref rut, ref tipoDTE, formato, "", xml, null, out descripcionOperacion, out folio);
            //    rutaPDF = ObtenerBoleta(folio);
            //}

            this.Cursor = Cursors.Default;
            Visualizador frmVisualiza = new Visualizador();
            frmVisualiza.CargarPDF(rutaPDF);
            frmVisualiza.ShowDialog();

            AccionLimpiar();
        }


        string ObtenerBoleta(long numero)
        {
            var filename = Guid.NewGuid().ToString() + ".pdf";
            var fileLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

            string descripcionOperacion;
            string rut = "71378000-6";
            int tipoDTE;
            int.TryParse("39", out tipoDTE);
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
        //    sb.Append("<RUTEmisor>71378000-6</RUTEmisor>");
        //    sb.Append("<RznSocEmisor>CORPORACIÓN DE DESARROLLO DE LA REINA </RznSocEmisor>");
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
        //    sb.Append("<NmbItem>" + memoDescripcion.Text + "</NmbItem>");
        //    sb.Append("<QtyItem>" + txtCantidad.EditValue.ToString() + "</QtyItem>");
        //    sb.Append("<UnmdItem></UnmdItem>");
        //    sb.Append("<PrcItem>" + "840" + "</PrcItem>");
        //    sb.Append("<MontoItem>" + "840" + "</MontoItem>");
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
            memoDescripcion.Text = "";
            txtValor.Value = 0;
        }
    }
}
