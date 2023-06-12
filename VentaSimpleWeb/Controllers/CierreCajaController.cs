using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace VentaSimpleWeb.Controllers
{
    public class CierreCajaController : Controller
    {
        // GET: CierreCaja
        public ActionResult Index(string limpiar)
        {
            var text = DateTime.Now.ToString();
            Session["FechaActual"] = text;

            VentaSimpleWeb.Models.CierreCajaModel modelo = new VentaSimpleWeb.Models.CierreCajaModel();
            Backline.Entidades.Filtro filtro = new Backline.Entidades.Filtro();
            if (limpiar != null)
            {
                Session["FiltroInformeDesde"] = Utiles.ReversaFecha(DateTime.Now);
                Session["FiltroInformeHasta"] = Utiles.ReversaFecha(DateTime.Now);
                filtro.EmpId = SessionH.Usuario.Emp_Id;
                if(SessionH.Usuario.Administrador == false)
                {
                    filtro.EstId = SessionH.Usuario.Est_Id;
                }
                filtro.Desde = Utiles.FechaObtenerMinimo(DateTime.Now);
                filtro.Hasta = Utiles.FechaObtenerMaximo(DateTime.Now);
                modelo.lista = Backline.DAL.CajaDAL.ObtenerCierresCajas(filtro);
            }
            if (Session["registrosEncontrados"] != null && limpiar == null)
            {
                modelo.lista = Session["registrosEncontrados"] as List<Backline.Entidades.Caja>;
            }

            return View(modelo);
        }
        public JsonResult ObtenerUsuarios()
        {
            Backline.Entidades.Usuario filtro = new Backline.Entidades.Usuario();
            filtro.Emp_Id = SessionH.Usuario.Emp_Id;
            filtro.Est_Id = SessionH.Usuario.Est_Id;
            var lista = Backline.DAL.UsuariosDAL.ObtenerUsuario(filtro);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ObtenerCuadreCaja(Backline.Entidades.Filtro filtro)
        {
            var lista = Backline.DAL.CajaDAL.ObtenerCuadreCaja(filtro);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult InsertarCierreCaja(Backline.Entidades.Caja entity)
        {
            try
            {
                Backline.Entidades.Usuario usuario1 = new Backline.Entidades.Usuario();
                usuario1.Id = entity.Id_Cajero;
                var usuario = Backline.DAL.UsuariosDAL.ObtenerUsuario(usuario1);

                Backline.Entidades.Filtro filtro = new Backline.Entidades.Filtro();
                filtro.EstId = usuario[0].Est_Id;
                filtro.UsrId_Caja = entity.Id_Cajero;
                filtro.Desde = Utiles.FechaObtenerMinimo(entity.Fecha_Cierre);
                filtro.Hasta = Utiles.FechaObtenerMaximo(entity.Fecha_Cierre);
                var cierreCaja = Backline.DAL.CajaDAL.ObtenerCierresCajas(filtro);
                if (cierreCaja.Count > 0)
                {
                    return new JsonResult() { ContentEncoding = Encoding.Default, Data = "existente", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

                

                entity.Emp_Id = SessionH.Usuario.Emp_Id;
                entity.Est_Id = usuario[0].Est_Id;
                entity.Fecha_Int = Utiles.FechaToInteger(entity.Fecha_Cierre);
                entity.Id_Usr_Cierra_Caja = SessionH.Usuario.Id;
                entity = Backline.DAL.CajaDAL.InsertarCierreCaja(entity);
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


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
        public ActionResult BusquedaFiltro(Backline.Entidades.Filtro entity)
        {
            entity.Desde = Utiles.FechaObtenerMinimo(entity.Desde);
            entity.Hasta = Utiles.FechaObtenerMaximo(entity.Hasta);
            entity.EmpId = SessionH.Usuario.Emp_Id;
            if (SessionH.Usuario.Administrador == false)
            {
                entity.EstId = SessionH.Usuario.Est_Id;
            }
            List<Backline.Entidades.Caja> historicosEncontrados = Backline.DAL.CajaDAL.ObtenerCierresCajas(entity);
            Session["FiltroInformeDesde"] = Utiles.ReversaFecha(entity.Desde);
            Session["FiltroInformeHasta"] = Utiles.ReversaFecha(entity.Hasta);
            Session["registrosEncontrados"] = historicosEncontrados;

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}