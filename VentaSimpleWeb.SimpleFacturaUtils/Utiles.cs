using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace VentaSimpleWeb.SimpleFacturaUtils
{
    public class Utiles
    {
        
        public async Task<Respuesta> GenerarNotaCredito(Backline.Entidades.Factura factura, int tipoDocumento, string rutEmpresa)
         {
            
            var client = new RestClient(new Uri("https://api.simplefactura.cl"));
            var request = new RestRequest("/invoiceCreditDebitNotes", Method.POST);
            request.AddHeader("Authorization", "Basic YWxleGlzLmNoZXVxdWlhbnRlQGJhY2tsaW5lc3BhLmNvbTpCYWNrbGluZTIwMjM=");
            request.AddHeader("Content-Type", "application/json");
            var body = ObtenerNota(factura);
            request.AddJsonBody(body);
            var response = client.Execute(request);
            Console.WriteLine(response.Content);

            bool respuesta = ObtieneStatus(response.Content.ToString());

            

            Respuesta respuestaSimpleFactura = new Respuesta();

            if (respuesta == true)
            {
                respuestaSimpleFactura.Folio = ExtraeFolioNC(response.Content.ToString());
                respuestaSimpleFactura.EsError = !respuesta;
                int tipoNC = 61;
                rutEmpresa = factura.RutEmpresa;
                string ruta = await RecuperarBoleta(respuestaSimpleFactura.Folio, factura.RutCliente, tipoNC, rutEmpresa, factura.NombreEstablecimiento);
                respuestaSimpleFactura.Ruta = ruta;
            }
            else
            {
                respuestaSimpleFactura.Error = ObtieneError(response.Content.ToString());
                respuestaSimpleFactura.EsError = true;
            }

            return respuestaSimpleFactura;
        }
        public string ObtenerNota(Backline.Entidades.Factura factura)
        {
            SimpleFacturaUtils.NotaCredito notaCredito = new NotaCredito();
            notaCredito.credenciales = new credenciales();
            notaCredito.credenciales.rutEmisor = factura.RutEmpresa;
            notaCredito.credenciales.rutContribuyente = factura.RutCliente;
            notaCredito.credenciales.nombreSucursal = factura.NombreEstablecimiento.Trim();

            notaCredito.dteReferenciadoExterno = new dteReferenciadoExterno();
            notaCredito.dteReferenciadoExterno.folio = factura.DocumentoOrigenINT;
            notaCredito.dteReferenciadoExterno.codigoTipoDte = 41;
            notaCredito.dteReferenciadoExterno.ambiente = 1;

            notaCredito.tipoNota = 1;
            notaCredito.motivo = 2;
            notaCredito.razon = "Anulación de boleta";
            notaCredito.notaDebito = false;

            string jsonString = JsonConvert.SerializeObject(notaCredito);
            return jsonString;
        }
        bool ObtieneStatus(string response)
        {
            int pos1 = response.IndexOf("status") + 8;
            int pos2 = response.IndexOf(",", pos1);
            int dife = pos2 - pos1;

            string status = response.Substring(pos1, dife);



            return status == "200" ? true : false;
        }
        int ExtraeFolioNC(string response)
        {
            int pos1 = response.IndexOf("folio") + 6;
            int pos2 = response.IndexOf("que", pos1);
            int dife = pos2 - pos1;

            string folio = response.Substring(pos1, dife);


            return int.Parse(folio);
        }

        public async Task<string> RecuperarBoleta(int folio, string rut, int tipoNC, string rutEmpresa, string Establecimiento)
        {

            var client = new RestClient(new Uri("https://api.simplefactura.cl"));
            var request = new RestRequest("/getPdf", Method.POST);
            request.AddHeader("Authorization", "Basic YWxleGlzLmNoZXVxdWlhbnRlQGJhY2tsaW5lc3BhLmNvbTpCYWNrbGluZTIwMjM=");
            request.AddHeader("Content-Type", "application/json");
            var body = CreaObjetoPDF(folio, rut, tipoNC, rutEmpresa, Establecimiento);

            body = body.Replace("@folio", folio.ToString());

            request.AddJsonBody(body);
            var response = client.Execute(request);
            byte[] pdf = response.RawBytes;

            //var prefijo = tipoNC == 41 ? "boleta_" : "nota_";


            var rutaGuardado = @"C:\Documentos Backline\simpleFactura_boleta_\" + "Nota_Crédito_N°_" + folio.ToString() + "(" + rutEmpresa.ToString() + ")" + ".pdf";

            System.IO.File.WriteAllBytes(rutaGuardado, pdf);

            //System.Diagnostics.Process.Start(rutaGuardado);
            return rutaGuardado;
        }
        public string CreaObjetoPDF(int folio, string rut, int tipoNC, string rutEmpresa, string NombreEstablecimiento)
        {
            SimpleFacturaUtils.ModelDTE_Credenciales credenciales = new ModelDTE_Credenciales();
            credenciales.credenciales = new credenciales();
            credenciales.credenciales.rutEmisor = rutEmpresa;
            credenciales.credenciales.rutContribuyente = rut;
            credenciales.credenciales.nombreSucursal = NombreEstablecimiento.Trim();

            credenciales.dteReferenciadoExterno = new dteReferenciadoExterno();
            credenciales.dteReferenciadoExterno.folio = folio;
            credenciales.dteReferenciadoExterno.codigoTipoDte = 61;
            credenciales.dteReferenciadoExterno.ambiente = 1;

            string jsonString = JsonConvert.SerializeObject(credenciales);
            return jsonString;
        }
        string ObtieneError(string response)
        {
            int pos1 = response.IndexOf("errors") + 10;
            int pos2 = response.IndexOf("]", pos1 - 4);
            int dife = pos2 - pos1;

            string error = response.Substring(pos1, dife);



            return error;
        }
        public static string ObtenerTimeStamp()
        {
            string yy = DateTime.Now.Year.ToString("0000");
            string mm = DateTime.Now.Month.ToString("00");
            string dd = DateTime.Now.Day.ToString("00");
            string hh = DateTime.Now.Hour.ToString("00");
            string mn = DateTime.Now.Minute.ToString("00");
            string ss = DateTime.Now.Second.ToString("00");

            return yy + mm + dd + hh + mn + ss;

        }
    }
}
