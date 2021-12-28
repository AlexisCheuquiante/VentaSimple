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
    public class LibroVentasController : Controller
    {
        // GET: LibroVentas
        public ActionResult Index()
        {
            VentaSimpleWeb.Models.LibroVentasModel modelo = new VentaSimpleWeb.Models.LibroVentasModel();
            Backline.Entidades.Filtro filtro = new Backline.Entidades.Filtro();
            filtro.EmpId = SessionH.Usuario.Emp_Id;
            filtro.Desde = Utiles.FechaObtenerMinimo(DateTime.Now);
            filtro.Hasta = Utiles.FechaObtenerMaximo(DateTime.Now);

            if (Session["registrosEncontrados"] == null && SessionH.Usuario.Administrador == true)
            {
                filtro.EmpId = SessionH.Usuario.Emp_Id;
                Session["FiltroInformeDesde"] = Utiles.ReversaFecha(DateTime.Now);
                Session["FiltroInformeHasta"] = Utiles.ReversaFecha(DateTime.Now);
                modelo.listaBoletas = Backline.DAL.BoletaDAL.ObtenerFactura(filtro);
                Session["registrosEncontrados"] = modelo.listaBoletas;
            }
            if (Session["registrosEncontrados"] == null && SessionH.Usuario.Administrador == false)
            {
                filtro.EmpId = SessionH.Usuario.Emp_Id;
                filtro.EstId = SessionH.Usuario.Est_Id;
                Session["FiltroInformeDesde"] = Utiles.ReversaFecha(DateTime.Now);
                Session["FiltroInformeHasta"] = Utiles.ReversaFecha(DateTime.Now);
                modelo.listaBoletas = Backline.DAL.BoletaDAL.ObtenerFactura(filtro);
                Session["registrosEncontrados"] = modelo.listaBoletas;
            }
            if (Session["registrosEncontrados"] != null)
            {
                modelo.listaBoletas = Session["registrosEncontrados"] as List<Backline.Entidades.Factura>;
            }

            return View(modelo);
        }
        public ActionResult BusquedaFiltro(Backline.Entidades.Filtro entity)
        {
            if (SessionH.Usuario.Administrador == true)
            {
                entity.Desde = Utiles.FechaObtenerMinimo(entity.Desde);
                entity.Hasta = Utiles.FechaObtenerMaximo(entity.Hasta);
                entity.EmpId = SessionH.Usuario.Emp_Id;
                List<Backline.Entidades.Factura> historicosEncontrados = Backline.DAL.BoletaDAL.ObtenerFactura(entity);
                Session["FiltroInformeDesde"] = Utiles.ReversaFecha(entity.Desde);
                Session["FiltroInformeHasta"] = Utiles.ReversaFecha(entity.Hasta);
                Session["registrosEncontrados"] = historicosEncontrados;
            }
            else
            {
                entity.Desde = Utiles.FechaObtenerMinimo(entity.Desde);
                entity.Hasta = Utiles.FechaObtenerMaximo(entity.Hasta);
                entity.EmpId = SessionH.Usuario.Emp_Id;
                entity.EstId = SessionH.Usuario.Est_Id;
                List<Backline.Entidades.Factura> historicosEncontrados = Backline.DAL.BoletaDAL.ObtenerFactura(entity);
                Session["FiltroInformeDesde"] = Utiles.ReversaFecha(entity.Desde);
                Session["FiltroInformeHasta"] = Utiles.ReversaFecha(entity.Hasta);
                Session["registrosEncontrados"] = historicosEncontrados;
            }

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ObtenerEstablecimientos()
        {
            Backline.Entidades.Filtro filtro = new Backline.Entidades.Filtro();
            filtro.EmpId = SessionH.Usuario.Emp_Id;
            var lista = Backline.DAL.EstablecimientoDAL.ObtenerEstablecimientos(filtro);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public FileContentResult ExportToExcel()
        {
            var timestamp = Utiles.ObtenerTimeStamp();
            List<Backline.Entidades.Factura> lista = Session["registrosEncontrados"] as List<Backline.Entidades.Factura>;
            string[] columns = { "FechaMostrar", "NumeroSII",  "Rut", "Contribuyente", "Glosa", "Total", "Usuario", "Sucursal", "TipoPago" };
            byte[] filecontent = Code.ExcelExportHelper.ExportExcel(lista, "Listado de ventas", true, columns);
            return File(filecontent, Code.ExcelExportHelper.ExcelContentType, "listaVentas_" + timestamp + ".xlsx");
        }
        public ActionResult ObtenerPdf(int folioSII)
        {
            try
            {
                //MessageBox.Show("Número" + folioSII.ToString());

                string ruta = ConfigurationManager.AppSettings["UrlBoletas"] + "Boleta_" + folioSII.ToString() + ".pdf";

                if (ruta == null || ruta == "")
                {
                    return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }


                return new JsonResult() { ContentEncoding = Encoding.Default, Data = ruta, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
    }
}