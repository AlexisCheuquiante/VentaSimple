﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace VentaSimpleWeb.Controllers
{
    public class MantenedorUsuariosController : Controller
    {
        // GET: MantenedorUsuarios
        public ActionResult Index()
        {
            if (VentaSimpleWeb.SessionH.Usuario == null)
            {
                return RedirectToAction("Index", "Login");
            }

            VentaSimpleWeb.Models.MantenedorUsuariosModel modelo = new VentaSimpleWeb.Models.MantenedorUsuariosModel();
            Backline.Entidades.Usuario usuario = new Backline.Entidades.Usuario();
            usuario.Emp_Id = SessionH.Usuario.Emp_Id;
            modelo.listaUsuarios = Backline.DAL.UsuariosDAL.ObtenerUsuario(usuario);
            return View(modelo);
        }
        public JsonResult ObtenerUsuario(int id)
        {
            Backline.Entidades.Usuario usuario = new Backline.Entidades.Usuario();
            usuario.Id = id;
            var lista = Backline.DAL.UsuariosDAL.ObtenerUsuario(usuario);

            if (lista.Count == 1)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista[0], JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult() { ContentEncoding = Encoding.Default, Data = "noEncontrado", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult InsertarUsuario(Backline.Entidades.Usuario entity)
        {
            try
            {
                entity.Emp_Id = SessionH.Usuario.Emp_Id;
                if (entity.Administrador == true)
                {
                    entity.Est_Id = 0;
                }
                if (SessionH.Usuario.Emp_Id != 14)
                {
                    entity.Clave_Autorizacion = null;
                }
                Backline.DAL.UsuariosDAL.InsertarUsuario(entity);

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
        public JsonResult EliminarUsuario(int id)
        {
            try
            {
                Backline.DAL.UsuariosDAL.EliminarUsuario(id);
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
    }
}