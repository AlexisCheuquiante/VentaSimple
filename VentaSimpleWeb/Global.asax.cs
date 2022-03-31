using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace VentaSimpleWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
            {
                //var a = ex;
                //Response.Redirect("/Error");
                Backline.Entidades.Error error = new Backline.Entidades.Error();
                error.UsrId = SessionH.Usuario.Id;
                error.EmpId = SessionH.Usuario.Emp_Id;
                error.EstId = SessionH.Usuario.Est_Id;
                error.Fecha = DateTime.Now;
                error.DetalleError = ex.ToString();
                Backline.DAL.ErrorDAL.InsertarError(error);
            }
        }
    }
}
