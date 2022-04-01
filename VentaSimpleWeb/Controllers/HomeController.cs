using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentaSimpleWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            VentaSimpleWeb.Models.HomeModel model = new Models.HomeModel();
            model.NombreUsuario = SessionH.Usuario.Nombre;
            model.NombreEmpresa = SessionH.Usuario.AliasEmpresa;
            return View(model);
        }
        
    }
}