using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Configuration;

namespace VentaSimpleWeb.Controllers
{
    public class GeneraVentaController : Controller
    {
        // GET: GeneraVenta
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
                if (entity.ContId == 0 && SessionH.Usuario.Emp_Id != 14)
                {
                    var rutDevuelto = VentaSimpleWeb.Utiles.ObtieneRut_INT(entity.Rut);
                    Session["RutCode"] = rutDevuelto;
                    rutFormateado = VentaSimpleWeb.Utiles.FormateaRut(entity.Rut);
                    contribuyente.Rut = rutFormateado;
                    contribuyente.Razon_Social = entity.Contribuyente;
                    contribuyente.Rut_Code = rutDevuelto;
                    Backline.DAL.ContribuyenteDAL.InsertarContribuyente(contribuyente);
                    idContribuyente = contribuyente.Id;
                }




                List<Backline.Entidades.DetalleFactura> detalleArticulos = new List<Backline.Entidades.DetalleFactura>();
                Backline.Entidades.DetalleFactura detalle = new Backline.Entidades.DetalleFactura();
                detalle.Cantidad = entity.Cantidad;
                detalle.DescripcionProducto = entity.Glosa;
                detalle.Valor = entity.Total;

                detalleArticulos.Add(detalle);

                if (idContribuyente != 0)
                {
                    entity.ContId = idContribuyente;
                }
                entity.EmpId = SessionH.Usuario.Emp_Id;
                entity.Fecha = DateTime.Now;
                entity.Usr_Id = SessionH.Usuario.Id;
                entity.EstId = SessionH.Usuario.Est_Id;
                Backline.DAL.BoletaDAL.InsertarFacturaV2(entity);
                var idBoleta = entity.Id;

                Backline.DTE.APIResult apiResult = new Backline.DTE.APIResult();
                int folioSII = 0;
                string rutaPDF = string.Empty;
                bool validadaSII = false;
                if (SessionH.Usuario.Emp_Id != 14 && rutFormateado != "")
                {

                    entity.Rut = rutFormateado;

                }

                if (SessionH.Usuario.Emp_Id == 14)
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
                string ruta = ConfigurationManager.AppSettings["UrlBoletas"] + rutEmpresa + a + "Boleta_" + folioSII.ToString() + ".pdf";

                if (rutaPDF == null || rutaPDF == "")
                {
                    return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

                entity.Id = idBoleta;
                entity.Numero = folioSII;
                Backline.DAL.BoletaDAL.InsertarNumeroBoleta(entity);

                return new JsonResult() { ContentEncoding = Encoding.Default, Data = ruta, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


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