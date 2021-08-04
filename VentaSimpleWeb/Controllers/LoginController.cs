using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace VentaSimpleWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ValidarLogin(Backline.Entidades.Usuario entity)
        {

            List<Backline.Entidades.Usuario> usuario = new List<Backline.Entidades.Usuario>();
            usuario = Backline.DAL.UsuariosDAL.ObtenerUsuario(entity);


            if (usuario != null && usuario.Count == 1)
            {
                SessionH.Usuario = usuario[0];
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}