using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace VentaSimpleWeb.Controllers
{
    public class MantenedorPrestacionesController : Controller
    {
        // GET: MantenedorProcedencia
        public ActionResult Index(string limpiar)
        {
            VentaSimpleWeb.Models.MantenedorPrestacionesModel modelo = new VentaSimpleWeb.Models.MantenedorPrestacionesModel();
            Backline.Entidades.Filtro filtro = new Backline.Entidades.Filtro();
            filtro.EmpId = SessionH.Usuario.Emp_Id;
            modelo.listaPrestaciones = Backline.DAL.PrestacionesDAL.ObtenerPrestaciones(filtro);
            return View(modelo);
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
        public JsonResult ObtenerPrestacion(int id)
        {
            Backline.Entidades.Filtro filtro = new Backline.Entidades.Filtro();
            filtro.Id = id;
            var lista = Backline.DAL.PrestacionesDAL.ObtenerPrestaciones(filtro);

            if (lista.Count == 1)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista[0], JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult() { ContentEncoding = Encoding.Default, Data = "noEncontrado", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult InsertarPrestacion(Backline.Entidades.Prestaciones entity)
        {
            try
            {
                entity.Emp_Id = SessionH.Usuario.Emp_Id;
                Backline.DAL.PrestacionesDAL.InsertarPrestacion(entity);

                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }
        public JsonResult EliminarPrestacion(int id)
        {
            try
            {
                Backline.DAL.PrestacionesDAL.EliminarPrestacion(id);
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
    }
}