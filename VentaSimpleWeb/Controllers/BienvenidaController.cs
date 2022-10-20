using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentaSimpleWeb.Controllers
{
    public class BienvenidaController : Controller
    {
        // GET: Bienvenida
        public ActionResult Index()
        {
            VentaSimpleWeb.Models.BienvenidaModel model = new Models.BienvenidaModel();
            model.NombreUsuario = SessionH.Usuario.Nombre;
            model.NombreSucursal = SessionH.Usuario.NombreEstablecimiento;
            return View(model);
        }
    }
}