using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Pdf;
using DevExpress.XtraPdfViewer;
using System.Drawing.Printing;


namespace Backline.VentaSimple
{
    public partial class Visualizador : Form
    {
        public Visualizador()
        {
            InitializeComponent();
        }

        public void CargarPDF(string ruta)
        {
            pdfViewer1.LoadDocument(ruta);
        }

        public void CargarPDF(System.IO.Stream stream)
        {
            pdfViewer1.LoadDocument(stream);
        }

        private void pdfFilePrintBarItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AccionImprimir();
        }

        public void AccionImprimir()
        {
            PrinterSettings printerSettings = new PrinterSettings();
            PdfPrinterSettings pdfPrinterSettings = new PdfPrinterSettings(printerSettings);

            // Specify the PDF printer settings.
            pdfPrinterSettings.PageOrientation = PdfPrintPageOrientation.Auto;
            pdfPrinterSettings.PageNumbers = new int[] { 1 };
            pdfPrinterSettings.ScaleMode = PdfPrintScaleMode.CustomScale;
            pdfPrinterSettings.Scale = 100;
            pdfPrinterSettings.EnableLegacyPrinting = false;
            pdfPrinterSettings.PrintingDpi = 800;

            pdfPrinterSettings.Settings.DefaultPageSettings.Margins.Top = 0;
            pdfPrinterSettings.Settings.DefaultPageSettings.Margins.Left = 0;
            pdfPrinterSettings.Settings.DefaultPageSettings.Margins.Right = 0;
            pdfPrinterSettings.Settings.DefaultPageSettings.Margins.Left = 0;


            // Print the document using the specified printer settings.
            pdfViewer1.Print(pdfPrinterSettings);
        }
    }
}
