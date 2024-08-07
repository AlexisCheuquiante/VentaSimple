﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;



namespace VentaSimpleWeb.Controllers
{
    public class LibroVentasController : Controller
    {
        // GET: LibroVentas
        public ActionResult Index(string limpiar)
        {
            if (VentaSimpleWeb.SessionH.Usuario == null)
            {
                return RedirectToAction("Index", "Login");
            }

            VentaSimpleWeb.Models.LibroVentasModel modelo = new VentaSimpleWeb.Models.LibroVentasModel();
            Backline.Entidades.Filtro filtro = new Backline.Entidades.Filtro();
            filtro.EmpId = SessionH.Usuario.Emp_Id;
            filtro.Desde = Utiles.FechaObtenerMinimo(DateTime.Now);
            filtro.Hasta = Utiles.FechaObtenerMaximo(DateTime.Now);

            if (Session["registrosEncontrados"] != null && limpiar != null)
            {
                filtro.EmpId = SessionH.Usuario.Emp_Id;
                Session["FiltroInformeDesde"] = Utiles.ReversaFecha(DateTime.Now);
                Session["FiltroInformeHasta"] = Utiles.ReversaFecha(DateTime.Now);
                modelo.listaBoletas = Backline.DAL.BoletaDAL.ObtenerFactura(filtro);
                Session["registrosEncontrados"] = modelo.listaBoletas;
            }
            if (Session["registrosEncontrados"] == null && SessionH.Usuario.Administrador == true)
            {
                filtro.EmpId = SessionH.Usuario.Emp_Id;
                Session["FiltroInformeDesde"] = Utiles.ReversaFecha(DateTime.Now);
                Session["FiltroInformeHasta"] = Utiles.ReversaFecha(DateTime.Now);
                modelo.listaBoletas = Backline.DAL.BoletaDAL.ObtenerFactura(filtro);
                Session["registrosEncontrados"] = modelo.listaBoletas;
            }
            if (SessionH.Usuario.Administrador == false && limpiar != null)
            {
                filtro.EmpId = SessionH.Usuario.Emp_Id;
                filtro.EstId = SessionH.Usuario.Est_Id;
                Session["FiltroInformeDesde"] = Utiles.ReversaFecha(DateTime.Now);
                Session["FiltroInformeHasta"] = Utiles.ReversaFecha(DateTime.Now);
                modelo.listaBoletas = Backline.DAL.BoletaDAL.ObtenerFactura(filtro);
                Session["registrosEncontrados"] = modelo.listaBoletas;
            }
            if (Session["registrosEncontrados"] != null && limpiar == null)
            {
                modelo.listaBoletas = Session["registrosEncontrados"] as List<Backline.Entidades.Factura>;
            }


            return View(modelo);
        }
        public ActionResult BusquedaFiltro(Backline.Entidades.Filtro entity)
        {
            if (SessionH.Usuario.Administrador == true)
            {
                entity.Desde = Utiles.FechaObtenerMinimo(entity.Desde);
                entity.Hasta = Utiles.FechaObtenerMaximo(entity.Hasta);
                entity.EmpId = SessionH.Usuario.Emp_Id;
                if (entity.EstId == -1)
                {
                    entity.EstId = 0;
                }
                List<Backline.Entidades.Factura> historicosEncontrados = Backline.DAL.BoletaDAL.ObtenerFactura(entity);
                Session["FiltroInformeDesde"] = Utiles.ReversaFecha(entity.Desde);
                Session["FiltroInformeHasta"] = Utiles.ReversaFecha(entity.Hasta);
                Session["registrosEncontrados"] = historicosEncontrados;
            }
            else
            {
                entity.Desde = Utiles.FechaObtenerMinimo(entity.Desde);
                entity.Hasta = Utiles.FechaObtenerMaximo(entity.Hasta);
                entity.EmpId = SessionH.Usuario.Emp_Id;
                entity.EstId = SessionH.Usuario.Est_Id;
                List<Backline.Entidades.Factura> historicosEncontrados = Backline.DAL.BoletaDAL.ObtenerFactura(entity);
                Session["FiltroInformeDesde"] = Utiles.ReversaFecha(entity.Desde);
                Session["FiltroInformeHasta"] = Utiles.ReversaFecha(entity.Hasta);
                Session["registrosEncontrados"] = historicosEncontrados;
            }

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
            List<Backline.Entidades.Factura> lista = Session["registrosEncontrados"] as List<Backline.Entidades.Factura>;
            if (SessionH.Usuario.Emp_Id == 14)
            {
                string[] columns = { "TipoDocumentoStr", "FechaMostrar", "NumeroSII", "DocumentoReferencia", "Glosa", "Total", "Usuario", "Sucursal", "TipoPago", "Nula" };
                byte[] filecontent = Code.ExcelExportHelper.ExportExcel(lista, "Listado de ventas", true, columns);
                return File(filecontent, Code.ExcelExportHelper.ExcelContentType, "listaVentas_" + timestamp + ".xlsx");
            }
            else
            {
                string[] columns = { "FechaMostrar", "NumeroSII", "Rut", "Contribuyente", "Glosa", "Total", "Usuario", "Sucursal", "TipoPago", "Nula" };
                byte[] filecontent = Code.ExcelExportHelper.ExportExcel(lista, "Listado de ventas", true, columns);
                return File(filecontent, Code.ExcelExportHelper.ExcelContentType, "listaVentas_" + timestamp + ".xlsx");
            }


        }
        public ActionResult ObtenerPdf(int folioSII)
        {
            try
            {
                //MessageBox.Show("Número" + folioSII.ToString());
                //if (SessionH.Usuario.Administrador == false)
                //{
                //    if (SessionH.Usuario.EsAfecta == true)
                //    {
                //        a = "(A)";
                //    }
                //    if (SessionH.Usuario.EsAfecta == false)
                //    {
                //        a = "(E)";
                //    }
                //    if (SessionH.Usuario.Facturador == "SimpleFactura")
                //    {
                //        facturador = "SimpleFactura";
                //    }
                //    if (SessionH.Usuario.Facturador == "superfactura")
                //    {
                //        facturador = "superfactura";
                //    }
                //    if (SessionH.Usuario.Facturador == "facele")
                //    {
                //        facturador = "facele";
                //    }
                //}

                var rutEmpresa = SessionH.Usuario.RutEmpresa;
                var a = "";
                var ruta = "";
                var facturador = "";

                //Obtengo la boleta con el número del SII
                Backline.Entidades.Filtro filtro = new Backline.Entidades.Filtro();
                filtro.FolioSii = folioSII;
                filtro.EmpId = SessionH.Usuario.Emp_Id;
                var boleta = Backline.DAL.BoletaDAL.ObtenerBoleta(filtro);
                if (boleta[0].Es_Afecta == true)
                {
                    a = "(A)";
                }
                else
                {
                    a = "(E)";
                }
                //Indico el proveedor de BE que tiene el documento
                if (boleta[0].Facturador == "SimpleFactura")
                {
                    facturador = "SimpleFactura";
                }
                if (boleta[0].Facturador == "superfactura")
                {
                    facturador = "superfactura";
                }
                if (boleta[0].Facturador == "facele")
                {
                    facturador = "facele";
                }

                //Indico la URL correspondiente al proveedor de boleta
                if (facturador == "superfactura")
                {
                    ruta = ConfigurationManager.AppSettings["UrlBoletas"] + rutEmpresa + a + "Boleta_" + folioSII.ToString() + ".pdf";
                }
                if (facturador == "facele")
                {
                    ruta = ConfigurationManager.AppSettings["UrlBoletasFacele"] + rutEmpresa + a + "Boleta_" + folioSII.ToString() + ".pdf";
                }
                if (facturador == "SimpleFactura")
                {
                    ruta = ConfigurationManager.AppSettings["UrlBoletasSimpleFactura"] + "Boleta_N°" + folioSII.ToString() + "_" + "(" + SessionH.Usuario.RutEmpresa + ")" + ".pdf";
                }

                if (ruta == null || ruta == "")
                {
                    return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

                if (ruta != null)
                {
                    Backline.Entidades.Bitacora bitacora = new Backline.Entidades.Bitacora();
                    bitacora.Id = 0;
                    bitacora.Emp_Id = SessionH.Usuario.Emp_Id;
                    if (SessionH.Usuario.Administrador == false)
                    {
                        bitacora.Est_Id = SessionH.Usuario.Est_Id;
                    }
                    else
                    {
                        bitacora.Est_Id = 0;
                    }
                    bitacora.Fecha = DateTime.Now;
                    bitacora.Mod_Id = 2;
                    bitacora.Observacion = "Se recupera la boleta número " + folioSII.ToString();
                    bitacora.Usr_Id = SessionH.Usuario.Id;
                    var bitacoraId = Backline.DAL.BitacoraDAL.InsertarBitacora(bitacora);
                }
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = ruta, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
        public JsonResult ObtenerUsuariosAutorizadores()
        {
            Backline.Entidades.Filtro filtro = new Backline.Entidades.Filtro();
            filtro.EmpId = SessionH.Usuario.Emp_Id;
            filtro.EsAdministrador = true;
            var lista = Backline.DAL.UsuariosDAL.ValidarClaveAutorizacion(filtro);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public async Task<JsonResult> CrearNotaCredito(Backline.Entidades.Factura entity, Backline.Entidades.Filtro filtro)
        {
            try
            {
                if (SessionH.Usuario.Ocupa_Clave_Autorizacion == true)
                {
                    filtro.EsAdministrador = true;
                    var puedeEmitirNota = Backline.DAL.UsuariosDAL.ValidarClaveAutorizacion(filtro);
                    if (puedeEmitirNota == null || puedeEmitirNota.Count == 0)
                    {
                        return new JsonResult() { ContentEncoding = Encoding.Default, Data = "NoAutorizado", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                }

                var numeroSII = 0;

                var lista = Backline.DAL.BoletaDAL.ObtenerBoleta(filtro);
                var idBoleta = lista[0].Id;

                List<Backline.Entidades.DetalleFactura> detalleArticulos = new List<Backline.Entidades.DetalleFactura>();
                Backline.Entidades.DetalleFactura detalle = new Backline.Entidades.DetalleFactura();
                detalle.Cantidad = 1;
                detalle.DescripcionProducto = lista[0].Glosa;
                detalle.Valor = lista[0].Total;

                detalleArticulos.Add(detalle);

                entity.ContId = lista[0].ContId;
                entity.Tido_Id = 2;
                entity.EmpId = SessionH.Usuario.Emp_Id;
                entity.Fecha = DateTime.Now;
                entity.Glosa = lista[0].Glosa;
                entity.Total = lista[0].Total;
                entity.Usr_Id = SessionH.Usuario.Id;
                entity.EstId = SessionH.Usuario.Est_Id;
                entity.Tipa_Id = lista[0].Tipa_Id;
                Backline.DAL.BoletaDAL.InsertarFacturaV2(entity);
                var idNotaCredito = entity.Id;

                string ruta = "";

                if (SessionH.Usuario.Facturador == "superfactura")
                {
                    Backline.DTE.APIResult apiResult = new Backline.DTE.APIResult();
                    int folioSII = 0;
                    string rutaPDF = string.Empty;
                    bool validadaSII = false;

                    entity.DocumentoOrigenINT = lista[0].Numero;
                    entity.Rut = lista[0].Rut;
                    entity.Contribuyente = lista[0].Contribuyente;
                    validadaSII = Utiles.GenerarNotaCredito(detalleArticulos, entity, Backline.DTE.Enums.TipoDocumento.NotaCredito, out folioSII, out rutaPDF, out apiResult);
                    entity.NumeroSII = folioSII;
                    numeroSII = folioSII;
                    var rutEmpresa = SessionH.Usuario.RutEmpresa;
                    var a = "";
                    //MessageBox.Show("Número" + folioSII.ToString());
                    if (SessionH.Usuario.EsAfecta == true)
                    {
                        a = "(A)";
                    }
                    else
                    {
                        a = "(E)";
                    }

                    ruta = ConfigurationManager.AppSettings["UrlBoletas"] + rutEmpresa + a + "NotaCrédito_" + numeroSII.ToString() + ".pdf";

                    if (rutaPDF == null || rutaPDF == "")
                    {
                        return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                }
                if (SessionH.Usuario.Facturador == "SimpleFactura")
                {

                    SimpleFacturaUtils.Utiles utl = new SimpleFacturaUtils.Utiles();
                    int tipoDocumento = 41;
                    var rutEmpresa = SessionH.Usuario.RutEmpresa;
                    entity.DocumentoOrigenINT = lista[0].Numero;
                    entity.RutCliente = lista[0].Rut;
                    entity.RutEmpresa = rutEmpresa;
                    entity.NombreEstablecimiento = SessionH.Usuario.NombreEstablecimiento;
                    entity.Total = lista[0].Total;
                    var respuestaAsyntec = await utl.GenerarNotaCredito(entity, tipoDocumento, rutEmpresa);
                    if (respuestaAsyntec.EsError == false)
                    {
                        numeroSII = respuestaAsyntec.Folio;
                        ruta = ConfigurationManager.AppSettings["UrlBoletasSimpleFactura"] + "Nota_Crédito_N°_" + numeroSII.ToString() + "(" + rutEmpresa.ToString() + ")" + ".pdf";
                    }
                    else
                    {
                        entity.Id = idNotaCredito;
                        Backline.DAL.BoletaDAL.EliminarNotaCredito(entity);
                        return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                }


                //Inserto Numero del SII
                entity.Id = idNotaCredito;
                entity.Numero = numeroSII;
                Backline.DAL.BoletaDAL.InsertarNumeroDocumento(entity);

                //Inserto Documento de referencia a Boleta
                entity.Id = idBoleta;
                entity.DocumentoReferencia = numeroSII;
                Backline.DAL.BoletaDAL.InsertarDocumentoReferencia(entity);

                //Inserto Documento de referencia a nota de crèdito
                entity.Id = idNotaCredito;
                entity.DocumentoReferencia = lista[0].Numero;
                Backline.DAL.BoletaDAL.InsertarDocumentoReferencia(entity);

                return new JsonResult() { ContentEncoding = Encoding.Default, Data = ruta, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
        public ActionResult ObtenerPdfNotaCredito(int folioSII)
        {
            try
            {
                var rutEmpresa = SessionH.Usuario.RutEmpresa;
                var a = "";
                var ruta = "";
                //MessageBox.Show("Número" + folioSII.ToString());
                if (SessionH.Usuario.EsAfecta == true || SessionH.Usuario.Id == 191)
                {
                    a = "(A)";
                }
                if (SessionH.Usuario.EsAfecta == false && SessionH.Usuario.Id != 191)
                {
                    a = "(E)";
                }
                if (SessionH.Usuario.Facturador == "superfactura")
                {
                    ruta = ConfigurationManager.AppSettings["UrlBoletas"] + rutEmpresa + a + "NotaCrédito_" + folioSII.ToString() + ".pdf";
                }
                if (SessionH.Usuario.Facturador == "SimpleFactura")
                {
                    ruta = ConfigurationManager.AppSettings["UrlBoletasSimpleFactura"] + "Nota_Crédito_N°_" + folioSII.ToString() + "(" + rutEmpresa.ToString() + ")" + ".pdf";
                }

                if (ruta == null || ruta == "")
                {
                    return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }


                return new JsonResult() { ContentEncoding = Encoding.Default, Data = ruta, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
        public JsonResult AnularBoleta(Backline.Entidades.Factura entity)
        {
            try
            {
                Backline.DAL.BoletaDAL.AnularBoleta(entity);
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
    }
}