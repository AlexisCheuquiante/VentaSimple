using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VentaSimpleWeb.Controllers
{
    public class MantenedoresController : Controller
    {
        // GET: Mantenedores
        public ActionResult Index()
        {
            if (VentaSimpleWeb.SessionH.Usuario == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}