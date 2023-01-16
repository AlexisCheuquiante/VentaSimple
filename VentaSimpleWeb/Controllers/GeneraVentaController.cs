using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Configuration;
using System.Threading;
using FacEleUtils;
using FacEleUtils.DoceleOLService;

namespace VentaSimpleWeb.Controllers
{
    public class GeneraVentaController : Controller
    {
        // GET: GeneraVenta
        FacEleClient client;
        public ActionResult Index()
        {          
            return View();
        }

        public JsonResult ObtenerContribuyente(string rut, Backline.Entidades.Filtro filtro)
        {
            var rutDevuelto = VentaSimpleWeb.Utiles.ObtieneRut_INT(rut);
            Session["RutCode"] = rutDevuelto;

            List<Backline.Entidades.Contribuyente> contribuyente = new List<Backline.Entidades.Contribuyente>();
            filtro.RutCode = rutDevuelto;
            contribuyente = Backline.DAL.ContribuyenteDAL.ObtenerContribuyente(filtro);

            if (contribuyente.Count == 1)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = contribuyente[0], JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult() { ContentEncoding = Encoding.Default, Data = "noEncontrado", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult InsertarFactura(Backline.Entidades.Factura entity, Backline.Entidades.Contribuyente contribuyente)
        {
            try
            {

                var idContribuyente = 0;
                var rutFormateado = "";
                var rutListo = "";
                if (entity.ContId == 0 && SessionH.Usuario.OcupaRut == true)
                {
                    var rutDevuelto = VentaSimpleWeb.Utiles.ObtieneRut_INT(entity.Rut);
                    Session["RutCode"] = rutDevuelto;
                    rutFormateado = VentaSimpleWeb.Utiles.FormateaRut(entity.Rut);
                    contribuyente.Rut = rutFormateado;
                    contribuyente.Razon_Social = entity.Contribuyente;
                    contribuyente.Rut_Code = rutDevuelto;
                    Backline.DAL.ContribuyenteDAL.InsertarContribuyente(contribuyente);
                    idContribuyente = contribuyente.Id;
                    rutListo = contribuyente.Rut.ToUpper();
                }

                List<Backline.Entidades.DetalleFactura> detalleArticulos = new List<Backline.Entidades.DetalleFactura>();
                Backline.Entidades.DetalleFactura detalle = new Backline.Entidades.DetalleFactura();
                if (SessionH.Usuario.Emp_Id == 7)
                {
                    detalle.Cantidad = entity.Cantidad;
                    detalle.DescripcionProducto = "Peaje cementerio";
                    detalle.Valor = 800;
                    entity.Tipa_Id = 0;
                    entity.Glosa = "Peaje cementerio";
                    entity.Total = 800;
                }
                else
                {
                    detalle.Cantidad = entity.Cantidad;
                    detalle.DescripcionProducto = entity.Glosa;
                    detalle.Valor = entity.Total;
                }
                

                detalleArticulos.Add(detalle);

                if (idContribuyente != 0)
                {
                    entity.ContId = idContribuyente;
                }
                entity.EmpId = SessionH.Usuario.Emp_Id;
                entity.Fecha = DateTime.Now;
                entity.Usr_Id = SessionH.Usuario.Id;
                entity.EstId = SessionH.Usuario.Est_Id;
                entity.Tido_Id = 1;
                entity.Es_Afecta = SessionH.Usuario.EsAfecta;
                Backline.DAL.BoletaDAL.InsertarFacturaV2(entity);
                var idBoleta = entity.Id;

                Backline.DTE.APIResult apiResult = new Backline.DTE.APIResult();
                int folioSII = 0;
                string rutaPDF = string.Empty;
                bool validadaSII = false;
                if (SessionH.Usuario.OcupaRut == true && rutListo != "")
                {

                    entity.Rut = rutListo;

                }
                var ruta = "";
                if (SessionH.Usuario.Facturador == "facele")
                {
                    entity.RutCliente = entity.Rut;
                    entity.NombreCliente = entity.Contribuyente;
                    validadaSII = GenerarBoletaElectronicaFacele(detalleArticulos, entity, out folioSII, out rutaPDF, out apiResult);
                    entity.NumeroSII = folioSII;
                    var rutEmpresa = SessionH.Usuario.RutEmpresa.Trim();
                    var a = "";
                    if (SessionH.Usuario.EsAfecta == true)
                    {
                        a = "(A)";
                    }
                    if (SessionH.Usuario.EsAfecta == false)
                    {
                        a = "(E)";
                    }
                    ruta = ConfigurationManager.AppSettings["UrlBoletasFacele"] + rutEmpresa + a + "Boleta_" + folioSII + ".pdf";
                }
                if (SessionH.Usuario.Facturador == "superfactura")
                {
                    if (SessionH.Usuario.EsAfecta == true)
                    {
                        validadaSII = Utiles.GenerarBoletaElectronica(detalleArticulos, entity, Backline.DTE.Enums.TipoDocumento.BoletaElectronica, out folioSII, out rutaPDF, out apiResult);
                        entity.NumeroSII = folioSII;
                    }
                    else
                    {
                        validadaSII = Utiles.GenerarBoletaElectronica(detalleArticulos, entity, Backline.DTE.Enums.TipoDocumento.BoletaElectronicaExenta, out folioSII, out rutaPDF, out apiResult);
                        entity.NumeroSII = folioSII;
                    }
                    var rutEmpresa = SessionH.Usuario.RutEmpresa;
                    var a = "";
                    //MessageBox.Show("Número" + folioSII.ToString());
                    if (SessionH.Usuario.EsAfecta == true)
                    {
                        a = "(A)";
                    }
                    else
                    {
                        a = "(E)";
                    }
                    ruta = ConfigurationManager.AppSettings["UrlBoletas"] + rutEmpresa + a + "Boleta_" + folioSII.ToString() + ".pdf";
                }

                if (rutaPDF == null || rutaPDF == "")
                {
                    return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

                entity.Id = idBoleta;
                entity.Numero = folioSII;
                Backline.DAL.BoletaDAL.InsertarNumeroDocumento(entity);

                return new JsonResult() { ContentEncoding = Encoding.Default, Data = ruta, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        
        
        public bool GenerarBoletaElectronicaFacele(List<Backline.Entidades.DetalleFactura> detalle, Backline.Entidades.Factura Factura, out int folio, out string rutaPDF, out Backline.DTE.APIResult apiResult)
        {
            
            folio = 0;
            rutaPDF = "";
            apiResult = new Backline.DTE.APIResult();

            var _detalleArticulo = detalle;

            //RUTINA FACTURACION ELECTRÓNICA (INTENTO CONECTARME AL SII ANTES DE GUARDAR LA FACTURA)
            bool validadaSII = false;
            int folioSII = 0;
            string rutaPDF_Facele = string.Empty;
            Backline.DTE.APIResult apiResult_Facele = new Backline.DTE.APIResult();

            string descripcionOperacion;
            long folio_facele;
            string rut = SessionH.Usuario.RutEmpresa.Trim();
            int tipoDTE = 39;
            generaDTEFormato formato = generaDTEFormato.XML;
            Factura.RazonSocial = SessionH.Usuario.NombreEmpresa;
            Factura.RutEmpresa = SessionH.Usuario.RutEmpresa.Trim();
            string xml = FacEleUtils.Utiles.ObtieneXMLBoleta (Factura, detalle);
            //Hualpen (EXENTAS)
            if (SessionH.Usuario.Emp_Id == 62 || SessionH.Usuario.Emp_Id == 63 || SessionH.Usuario.Emp_Id == 21)
            {
                tipoDTE = 41;
                xml = "";
                xml = FacEleUtils.Utiles.ObtieneXMLBoletaExenta(Factura, _detalleArticulo);
            }


            client = new FacEleClient();
            var salida = client.generaDTE(ref rut, ref tipoDTE, formato, "", xml, null, out descripcionOperacion, out folio_facele);
            
            if (salida == 0)
            {
                return false;
            }
            if (salida == 1)
            {
                folioSII = int.Parse(folio_facele.ToString());
                var a = "";
                if (tipoDTE == 39)
                {
                    a = "(A)";
                }
                if (tipoDTE == 41)
                {
                    a = "(E)";
                }
                var rutEmpresa = SessionH.Usuario.RutEmpresa.Trim();
                rutaPDF = ObtenerBoleta(folio_facele, tipoDTE, rutEmpresa, a);
                validadaSII = true;
                folio = folioSII;
                apiResult.ok = true;
            }

            if (descripcionOperacion != "Proceso OK")
            {
                return false;
            }

           

            return false;

        }
        string ObtenerBoleta(long numero, int TipoDTE, string rutEmpresa, string a)
        {
            var filename = rutEmpresa + a + "Boleta_" + numero + ".pdf";
            var fileLocation = Path.Combine(@"C:\Documentos Backline\BoletasVentaSimpleFacele\", filename);

            string descripcionOperacion;
            string rut = SessionH.Usuario.RutEmpresa.Trim();
            int tipoDTE;
            int.TryParse(TipoDTE.ToString(), out tipoDTE);
            long folioDTE;
            long.TryParse(numero.ToString(), out folioDTE);
            byte[] PDF;
            string XML;
            string URL;
            client.obtienePDF(rut, tipoDTE, folioDTE, out descripcionOperacion, out XML, out PDF, out URL);
            client.grabaPDF(PDF, fileLocation);
            client.obtieneURLPDF(rut, tipoDTE, folioDTE, out descripcionOperacion, out XML, out PDF, out URL);
            //lblLinkPDF.Enabled = true;
            //lblLinkPDF.Text = URL;
            return fileLocation;
        }
        public JsonResult ObtenerTiposPago()
        {

            var lista = Backline.DAL.TipoPagoDAL.ObtenerTiposPago();

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}