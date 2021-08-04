using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace VentaSimpleWeb.Controllers
{
    public class CambiarContraseñaController : Controller
    {
        // GET: CambiarContraseña
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ObtenerContraseña()
        {
            Backline.Entidades.Usuario usuario = new Backline.Entidades.Usuario();
            usuario.Id = SessionH.Usuario.Id;
            var lista = Backline.DAL.UsuariosDAL.ObtenerUsuario(usuario);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista[0], JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult AccionSalir()
        {
            SessionH.Usuario = null;
            SessionH.Logueado = false;
            return RedirectToAction("Index", "Login");
        }
        public JsonResult CambiarClave(Backline.Entidades.Usuario entity)
        {
            try
            {
                entity.Id = SessionH.Usuario.Id;
                Backline.DAL.UsuariosDAL.CambiarClave(entity);
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
    }
}