using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace VentaSimpleWeb.Controllers
{
    public class BitacoraController : Controller
    {
        // GET: Bitacora
        public ActionResult Index(string limpiar)
        {
            if (VentaSimpleWeb.SessionH.Usuario == null)
            {
                return RedirectToAction("Index", "Login");
            }

            VentaSimpleWeb.Models.BitacoraModel modelo = new VentaSimpleWeb.Models.BitacoraModel();
            Backline.Entidades.Filtro filtro = new Backline.Entidades.Filtro();

            if (Session["registrosEncontrados"] != null && limpiar == null)
            {
                modelo.lista = Session["registrosEncontrados"] as List<Backline.Entidades.Bitacora>;
            }
            if (limpiar != null)
            {
                Session["FiltroInformeDesde"] = Utiles.ReversaFecha(DateTime.Now);
                Session["FiltroInformeHasta"] = Utiles.ReversaFecha(DateTime.Now);
                filtro.EmpId = SessionH.Usuario.Emp_Id;
                if (SessionH.Usuario.Administrador == false)
                {
                    filtro.EstId = SessionH.Usuario.Est_Id;
                }
                filtro.Desde = Utiles.FechaObtenerMinimo(DateTime.Now);
                filtro.Hasta = Utiles.FechaObtenerMaximo(DateTime.Now);
                modelo.lista = Backline.DAL.BitacoraDAL.ObtenerBitacora(filtro);
                Session["registrosEncontrados"] = modelo.lista;
            }

            return View(modelo);
        }
        public JsonResult ObtenerModulos()
        {

            var lista = Backline.DAL.ModuloDAL.ObtenerModulos();

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ObtenerUsuario()
        {
            Backline.Entidades.Usuario usuario = new Backline.Entidades.Usuario();
            usuario.Emp_Id = SessionH.Usuario.Emp_Id;
            var lista = Backline.DAL.UsuariosDAL.ObtenerUsuario(usuario);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
            List<Backline.Entidades.Bitacora> lista = Session["registrosEncontrados"] as List<Backline.Entidades.Bitacora>;

            string[] columns = { "FechaMostrar", "Modulo", "Observacion", "Usuario", "Establecimiento" };
            byte[] filecontent = Code.ExcelExportHelper.ExportExcel(lista, "Informe de bitacora", true, columns);
            return File(filecontent, Code.ExcelExportHelper.ExcelContentType, "InformeBitacora_" + timestamp + ".xlsx");
        }
        public ActionResult BusquedaFiltro(Backline.Entidades.Filtro entity)
        {
            entity.Desde = Utiles.FechaObtenerMinimo(entity.Desde);
            entity.Hasta = Utiles.FechaObtenerMaximo(entity.Hasta);
            entity.EmpId = SessionH.Usuario.Emp_Id;
            if (entity.Mod_Id <= 0)
            {
                entity.Mod_Id = 0;
            }
            if (entity.EstId <= 0)
            {
                entity.EstId = 0;
            }
            if (entity.UsrId <= 0)
            {
                entity.UsrId = 0;
            }
            List<Backline.Entidades.Bitacora> historicosEncontrados = Backline.DAL.BitacoraDAL.ObtenerBitacora(entity);
            Session["FiltroInformeDesde"] = Utiles.ReversaFecha(entity.Desde);
            Session["FiltroInformeHasta"] = Utiles.ReversaFecha(entity.Hasta);
            Session["registrosEncontrados"] = historicosEncontrados;

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}