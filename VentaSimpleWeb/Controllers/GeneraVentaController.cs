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
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
//using System.Text.Json;

namespace VentaSimpleWeb.Controllers
{
    
    public class GeneraVentaController : Controller
    {
        // GET: GeneraVenta
        FacEleClient client;
        public ActionResult Index()
        {
            if (VentaSimpleWeb.SessionH.Usuario == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Session["ListaPrestaciones"] = null;
            return View();
        }

        public JsonResult ObtenerPrestaciones()
        {
            Backline.Entidades.Filtro filtro = new Backline.Entidades.Filtro();
            filtro.EmpId = SessionH.Usuario.Emp_Id;
            filtro.EstId = SessionH.Usuario.Est_Id;
            var lista = Backline.DAL.PrestacionesDAL.ObtenerPrestaciones(filtro);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
                if (SessionH.Usuario.Ocupa_Prestaciones == false)
                {
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
                }
                if (SessionH.Usuario.Ocupa_Prestaciones == true)
                {
                    List<Backline.Entidades.DetalleFactura> listadoPrestaciones = Session["ListaPrestaciones"] as List<Backline.Entidades.DetalleFactura>;
                    if (listadoPrestaciones.Count > 0)
                    {
                        detalleArticulos = listadoPrestaciones;
                        var glosa = "";
                        foreach (var a in listadoPrestaciones)
                        {
                            glosa = glosa + "-" + a.DescripcionProducto;
                        }
                        entity.Glosa = glosa;
                    }
                }

                if (idContribuyente != 0)
                {
                    entity.ContId = idContribuyente;
                }

                Backline.Entidades.Filtro filtro = new Backline.Entidades.Filtro();
                filtro.Id = SessionH.Usuario.Id;
                var datos = Backline.DAL.UsuariosDAL.ObtenerDatos(filtro);

                entity.EmpId = SessionH.Usuario.Emp_Id;
                entity.Fecha = DateTime.Now;
                entity.Usr_Id = SessionH.Usuario.Id;
                entity.EstId = SessionH.Usuario.Est_Id;
                entity.Tido_Id = 1;
                entity.Es_Afecta = datos[0].EsAfecta;
                if (SessionH.Usuario.Selecciona_Tipo_Boleta == true)
                {
                    if (entity.Tipo_Boleta_Seleccionado == false)
                    {
                        entity.Es_Afecta = true;
                    }
                    else
                    {
                        entity.Es_Afecta = false;
                    }
                }
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
                    if (SessionH.Usuario.Selecciona_Tipo_Boleta == true)
                    {
                        if (entity.Tipo_Boleta_Seleccionado == false)
                        {
                            SessionH.Usuario.EsAfecta = true;
                        }
                        else
                        {
                            SessionH.Usuario.EsAfecta = false;
                        }
                    }
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
                if (SessionH.Usuario.Facturador == "SimpleFactura")
                {
                    Session["ultimoFolio"] = null;
                    if (SessionH.Usuario.Selecciona_Tipo_Boleta == true)
                    {
                        if (entity.Tipo_Boleta_Seleccionado == false)
                        {
                            SessionH.Usuario.EsAfecta = true;
                        }
                        else
                        {
                            SessionH.Usuario.EsAfecta = false;
                        }
                    }

                    if (SessionH.Usuario.EsAfecta == false) 
                    {
                        entity.Tipo_Boleta = 41;
                    }
                    else
                    {
                        entity.Tipo_Boleta = 39;
                    }
                     
                    entity.FechaFormateada = Utiles.ReversaFecha(entity.Fecha);

                    Task<bool> Task = GenerarBoletaSimpleFactura(detalleArticulos, entity);
                    validadaSII = Task.Result;
                    if (validadaSII == true)
                    {
                        Task<string> ruta2 = RecuperarBoleta(entity);
                        rutaPDF = ConfigurationManager.AppSettings["UrlBoletasSimpleFactura"] + "Boleta_N°" + Session["ultimoFolio"].ToString() + "_" + "(" + SessionH.Usuario.RutEmpresa + ")" + ".pdf";
                        ruta = ConfigurationManager.AppSettings["UrlBoletasSimpleFactura"] + "Boleta_N°" + Session["ultimoFolio"].ToString() + "_" + "(" + SessionH.Usuario.RutEmpresa + ")" + ".pdf";
                        
                    }
                }

                if (rutaPDF == null || rutaPDF == "")
                {
                    return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

                if (Session["ultimoFolio"] != null)
                {
                    folioSII = int.Parse(Session["ultimoFolio"].ToString());
                }
                entity.Id = idBoleta;
                entity.Numero = folioSII;
                entity.Facturador = datos[0].Facturador;
                Backline.DAL.BoletaDAL.InsertarNumeroDocumento(entity);

                if (folioSII > 0)
                {
                    Backline.Entidades.Bitacora bitacora = new Backline.Entidades.Bitacora();
                    bitacora.Id = 0;
                    bitacora.Emp_Id = SessionH.Usuario.Emp_Id;
                    if (SessionH.Usuario.Administrador == false)
                    {
                        bitacora.Est_Id = SessionH.Usuario.Est_Id;
                    }
                    else
                    {
                        bitacora.Est_Id = 0;
                    }
                    bitacora.Fecha = DateTime.Now;
                    bitacora.Mod_Id = 1;
                    bitacora.Observacion = "Se genera la boleta número " + folioSII.ToString();
                    bitacora.Usr_Id = SessionH.Usuario.Id;
                    var bitacoraId = Backline.DAL.BitacoraDAL.InsertarBitacora(bitacora);
                }

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
            int tipoDTE = 0;

            if (SessionH.Usuario.EsAfecta == true)
            {
                tipoDTE = 39;
            }
            else
            {
                tipoDTE = 41;
            }
            generaDTEFormato formato = generaDTEFormato.XML;
            Factura.RazonSocial = SessionH.Usuario.NombreEmpresa;
            Factura.RutEmpresa = SessionH.Usuario.RutEmpresa.Trim();
            string xml = "";
            if (tipoDTE == 39)
            {
                xml = FacEleUtils.Utiles.ObtieneXMLBoleta(Factura, detalle);
            }
            else
            {
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
        public async Task<bool> GenerarBoletaSimpleFactura(List<Backline.Entidades.DetalleFactura> detalle, Backline.Entidades.Factura Factura)
        {
            var client = new RestClient(new Uri("https://api.simplefactura.cl"));
            var request = new RestRequest("/invoiceV2/" + SessionH.Usuario.NombreEstablecimiento.Trim(), Method.POST);
            request.AddHeader("Authorization", "Basic YWxleGlzLmNoZXVxdWlhbnRlQGJhY2tsaW5lc3BhLmNvbTpCYWNrbGluZTIwMjM=");
            request.AddHeader("Content-Type", "application/json");
            var body = "";
            if (Factura.Tipo_Boleta == 41)
            {
                body = ObtenerDocumento(detalle, Factura);
            }
            else
            {
                body = ObtenerDocumento_Afecto(detalle, Factura);
            }
            
            request.AddJsonBody(body);
            var response =  client.Execute(request);

            if (ObtieneStatus(response.Content.ToString()) == false)
            {
                Session["MensajeError"] = ObtieneError(response.Content.ToString());
                return false;
            }
            else
            {
                Session["ultimoFolio"] = ExtraeFolio(response.Content.ToString());
                return true;
            }
            
            //label1.Text = "Se ha generado la boleta " + _ultimoFolio.ToString();
            
            
        }
        bool ObtieneStatus(string response)
        {
            int pos1 = response.IndexOf("status") + 8;
            int pos2 = response.IndexOf(",", pos1);
            int dife = pos2 - pos1;

            string status = response.Substring(pos1, dife);



            return status == "200" ? true : false;
        }
        string ObtieneError(string response)
        {
            int pos1 = response.IndexOf("errors") + 10;
            int pos2 = response.IndexOf("]", pos1 - 4);
            int dife = pos2 - pos1;

            string error = response.Substring(pos1, dife);



            return error;
        }
        public string ObtenerDocumento(List<Backline.Entidades.DetalleFactura> detalle, Backline.Entidades.Factura Factura)
        {
            SimpleFactura.Dte dte = new SimpleFactura.Dte();

            dte.Documento = new SimpleFactura.Documento();

            dte.Documento.Encabezado = new SimpleFactura.Encabezado();

            dte.Documento.Encabezado.IdDoc = new SimpleFactura.IdDoc();
            dte.Documento.Encabezado.IdDoc.TipoDTE = Factura.Tipo_Boleta;
            dte.Documento.Encabezado.IdDoc.FchEmis = Factura.FechaFormateada;
            dte.Documento.Encabezado.IdDoc.FchVenc = Factura.FechaFormateada;

            dte.Documento.Encabezado.Emisor = new SimpleFactura.Emisor();
            dte.Documento.Encabezado.Emisor.RUTEmisor = SessionH.Usuario.RutEmpresa;
            dte.Documento.Encabezado.Emisor.RznSocEmisor = SessionH.Usuario.NombreEmpresa;
            dte.Documento.Encabezado.Emisor.GiroEmisor = SessionH.Usuario.Giro_Impreso.Trim();
            dte.Documento.Encabezado.Emisor.DirOrigen = SessionH.Usuario.Direccion_Sucursal.Trim();
            dte.Documento.Encabezado.Emisor.CmnaOrigen = SessionH.Usuario.Comuna.Trim();
            dte.Documento.Encabezado.Emisor.CiudadOrigen = SessionH.Usuario.Ciudad.Trim();

            dte.Documento.Encabezado.Receptor = new SimpleFactura.Receptor();
            dte.Documento.Encabezado.Receptor.RUTRecep = Factura.Rut;
            dte.Documento.Encabezado.Receptor.RznSocRecep = Factura.Contribuyente;
            dte.Documento.Encabezado.Receptor.DirRecep = SessionH.Usuario.Comuna.Trim();
            dte.Documento.Encabezado.Receptor.CmnaRecep = SessionH.Usuario.Comuna.Trim();
            dte.Documento.Encabezado.Receptor.CiudadRecep = SessionH.Usuario.Ciudad.Trim();

            

            int contador = 1;
            decimal montoNeto = 0;
            decimal montoIVA = 0;
            decimal montoTotal = 0;
            dte.Documento.Detalle = new List<SimpleFactura.Detalle>();


            foreach (var a in detalle)
            {
                SimpleFactura.Detalle detalleBoleta = new SimpleFactura.Detalle();

                detalleBoleta.NroLinDet = contador.ToString();
                detalleBoleta.NmbItem = a.DescripcionProducto;
                detalleBoleta.QtyItem = a.Cantidad.ToString();
                detalleBoleta.UnmdItem = "un";
                detalleBoleta.PrcItem = a.Valor.ToString();
                detalleBoleta.MontoItem = a.Valor.ToString();
                detalleBoleta.IndExe = 1;

                dte.Documento.Detalle.Add(detalleBoleta);
                contador++;
            }
            dte.Documento.Encabezado.Totales = new SimpleFactura.Totales();
            dte.Documento.Encabezado.Totales.MntNeto = montoNeto.ToString();
            dte.Documento.Encabezado.Totales.IVA = montoIVA.ToString(); 
            dte.Documento.Encabezado.Totales.MntTotal = Factura.Total.ToString();
            dte.Documento.Encabezado.Totales.MntExe = Factura.Total.ToString();


            



            string jsonString = JsonConvert.SerializeObject(dte);
            return jsonString;
        }
        public string ObtenerDocumento_Afecto(List<Backline.Entidades.DetalleFactura> detalle, Backline.Entidades.Factura Factura)
        {
            SimpleFactura.Dte dte = new SimpleFactura.Dte();

            dte.Documento = new SimpleFactura.Documento();

            dte.Documento.Encabezado = new SimpleFactura.Encabezado();

            dte.Documento.Encabezado.IdDoc = new SimpleFactura.IdDoc();
            dte.Documento.Encabezado.IdDoc.TipoDTE = Factura.Tipo_Boleta;
            dte.Documento.Encabezado.IdDoc.FchEmis = Factura.FechaFormateada;
            dte.Documento.Encabezado.IdDoc.FchVenc = Factura.FechaFormateada;

            dte.Documento.Encabezado.Emisor = new SimpleFactura.Emisor();
            dte.Documento.Encabezado.Emisor.RUTEmisor = SessionH.Usuario.RutEmpresa;
            dte.Documento.Encabezado.Emisor.RznSocEmisor = SessionH.Usuario.NombreEmpresa;
            dte.Documento.Encabezado.Emisor.GiroEmisor = SessionH.Usuario.Giro_Impreso.Trim();
            dte.Documento.Encabezado.Emisor.DirOrigen = SessionH.Usuario.Direccion_Sucursal.Trim();
            dte.Documento.Encabezado.Emisor.CmnaOrigen = SessionH.Usuario.Comuna.Trim();
            dte.Documento.Encabezado.Emisor.CiudadOrigen = SessionH.Usuario.Ciudad.Trim();

            dte.Documento.Encabezado.Receptor = new SimpleFactura.Receptor();
            dte.Documento.Encabezado.Receptor.RUTRecep = Factura.Rut;
            dte.Documento.Encabezado.Receptor.RznSocRecep = Factura.Contribuyente;
            dte.Documento.Encabezado.Receptor.DirRecep = SessionH.Usuario.Comuna.Trim();
            dte.Documento.Encabezado.Receptor.CmnaRecep = SessionH.Usuario.Comuna.Trim();
            dte.Documento.Encabezado.Receptor.CiudadRecep = SessionH.Usuario.Ciudad.Trim();

            SimpleFactura.Detalle detalleBoleta = new SimpleFactura.Detalle();
            int contador = 1;
            decimal montoNeto = 0;
            decimal montoIVA = 0;
            decimal montoTotal = 0;
            dte.Documento.Detalle = new List<SimpleFactura.Detalle>();

         
                foreach (var d in detalle)
                {
                    detalleBoleta.NroLinDet = contador.ToString();
                    detalleBoleta.NmbItem = d.DescripcionProducto;
                    detalleBoleta.QtyItem = d.Cantidad.ToString();
                    detalleBoleta.UnmdItem = "un";
                    detalleBoleta.PrcItem = d.Valor.ToString();
                    detalleBoleta.MontoItem = d.Valor.ToString();
                    detalleBoleta.IndExe = 0;
                    dte.Documento.Detalle.Add(detalleBoleta);
                    montoTotal = montoTotal + d.Valor;
                    contador++;
                }

                montoNeto = montoTotal / decimal.Parse("1,19");
                montoIVA = montoTotal - montoNeto;

                dte.Documento.Encabezado.Totales = new SimpleFactura.Totales();
                dte.Documento.Encabezado.Totales.MntNeto = Math.Round(montoNeto, 0).ToString();
                dte.Documento.Encabezado.Totales.IVA = Math.Round(montoIVA, 0).ToString();
                dte.Documento.Encabezado.Totales.MntTotal = Math.Round(montoTotal, 0).ToString();
                dte.Documento.Encabezado.Totales.MntExe = "0";
            

            string jsonString = JsonConvert.SerializeObject(dte);
            return jsonString;
        }
        int ExtraeFolio(string response)
        {
            int pos1 = response.IndexOf("folio") + 7;
            int pos2 = response.IndexOf(",", pos1);
            int dife = pos2 - pos1;

            string folio = response.Substring(pos1, dife);


            return int.Parse(folio);
        }
        public async Task<string> RecuperarBoleta(Backline.Entidades.Factura Factura)
        {
            var client = new RestClient(new Uri("https://api.simplefactura.cl"));
            var request = new RestRequest("/getPdf", Method.POST);
            request.AddHeader("Authorization", "Basic YWxleGlzLmNoZXVxdWlhbnRlQGJhY2tsaW5lc3BhLmNvbTpCYWNrbGluZTIwMjM=");
            request.AddHeader("Content-Type", "application/json");
            var body = CreaObjetoPDF(Factura);

            body = body.Replace("@folio", Session["ultimoFolio"].ToString());

            request.AddJsonBody(body);
            var response = client.Execute(request);
            byte[] pdf = response.RawBytes;

            var rutaGuardado = @"C:\Documentos Backline\simpleFactura_boleta_\" + "Boleta_N°" + Session["ultimoFolio"].ToString() +"_" + "(" + SessionH.Usuario.RutEmpresa + ")" + ".pdf";
            Session["rutaPdfSimpleFactura"] = rutaGuardado;
            
            System.IO.File.WriteAllBytes(rutaGuardado, pdf);

            //System.Diagnostics.Process.Start(rutaGuardado);
            return rutaGuardado;
        }
        public string CreaObjetoPDF(Backline.Entidades.Factura Factura)
        {
            VentaSimpleWeb.ModelDTE_Credenciales credenciales = new ModelDTE_Credenciales();
            credenciales.credenciales = new credenciales();
            credenciales.credenciales.rutEmisor = SessionH.Usuario.RutEmpresa;
            credenciales.credenciales.rutContribuyente = Factura.Rut;
            credenciales.credenciales.nombreSucursal = SessionH.Usuario.NombreEstablecimiento.Trim();

            credenciales.dteReferenciadoExterno = new dteReferenciadoExterno();
            credenciales.dteReferenciadoExterno.folio = int.Parse(Session["ultimoFolio"].ToString());
            if (SessionH.Usuario.EsAfecta == false)
            {
                credenciales.dteReferenciadoExterno.codigoTipoDte = 41;
            }
            else
            {
                credenciales.dteReferenciadoExterno.codigoTipoDte = 39;
            }
            if (SessionH.Usuario.Ambiente == "cer")
            {
                credenciales.dteReferenciadoExterno.ambiente = 0;
            }
            else
            {
                credenciales.dteReferenciadoExterno.ambiente = 1;
            }

            string jsonString = JsonConvert.SerializeObject(credenciales);
            return jsonString;
        }
        public JsonResult AgregarPrestacion(Backline.Entidades.DetalleFactura entity)
        {
            try
            {
                if (Session["ListaPrestaciones"] == null)
                {
                    List<Backline.Entidades.DetalleFactura> listadoPrestaciones = new List<Backline.Entidades.DetalleFactura>();
                    listadoPrestaciones.Add(entity);
                    Session["ListaPrestaciones"] = listadoPrestaciones;
                }
                else
                {
                    List<Backline.Entidades.DetalleFactura> listadoPrestaciones = Session["ListaPrestaciones"] as List<Backline.Entidades.DetalleFactura>;
                    listadoPrestaciones.Add(entity);
                    Session["ListaPrestaciones"] = listadoPrestaciones;
                }
                
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public JsonResult QuitarPrestacion(int id)
        {
            try
            {

                List<Backline.Entidades.DetalleFactura> listadoPrestaciones = Session["ListaPrestaciones"] as List<Backline.Entidades.DetalleFactura>;
                List<Backline.Entidades.DetalleFactura> listadoPrestacionesNueva = new List<Backline.Entidades.DetalleFactura>();
                foreach (var a in listadoPrestaciones)
                {
                    if (a.IdLineaProducto != id)
                    {
                        listadoPrestacionesNueva.Add(a);
                    }

                }
                listadoPrestaciones = listadoPrestacionesNueva;
                Session["ListaPrestaciones"] = listadoPrestaciones;

                return new JsonResult() { ContentEncoding = Encoding.Default, Data = listadoPrestaciones, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
    }
}