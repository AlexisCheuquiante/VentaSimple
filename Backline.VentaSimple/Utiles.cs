using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backline.DTE;
using System.IO;

namespace Backline.VentaSimple
{
    public class Utiles
    {
        public static string ObtenerTimeStamp()
        {
            string yy = DateTime.Now.Year.ToString("0000");
            string mm = DateTime.Now.Month.ToString("00");
            string dd = DateTime.Now.Day.ToString("00");
            string hh = DateTime.Now.Hour.ToString("00");
            string mn = DateTime.Now.Minute.ToString("00");
            string ss = DateTime.Now.Second.ToString("00");

            return yy + mm + dd + hh + mn + ss;

        }
        public static DateTime ObtenerHoraMinima(DateTime fecha)
        {
            DateTime date = new DateTime(fecha.Year, fecha.Month, fecha.Day, 0, 0, 0);
            return date;
        }

        public static DateTime ObtenerHoraMaxima(DateTime fecha)
        {
            DateTime date = new DateTime(fecha.Year, fecha.Month, fecha.Day, 23, 59, 59);
            return date;
        }

        public static int ObtieneRut_INT(string rut)
        {
            if (rut.Trim().Length == 0)
                return 0;
            int rutDevuelto = 0;

            if (rut.Trim().Length == 9)
            {
                rutDevuelto = int.Parse(rut.Substring(0, 7));
            }
            else
            {
                rutDevuelto = int.Parse(rut.Substring(0, 8));
            }



            return rutDevuelto;
        }
        public static string FormateaRut(string rut)
        {
            rut = rut.Trim();
            string rutDevuelto = string.Empty;
            if (rut.Length == 8)
            {
                rut = "0" + rut;
                rutDevuelto = rut.Substring(0, 8) + "-" + rut.Substring(8, 1);
            }
            if (rut.Length == 9)
            {
                if (rut.IndexOf("-") > 0)
                {
                    rutDevuelto = "0" + rut;

                }
                else
                {
                    rutDevuelto = rut.Substring(0, 8) + "-" + rut.Substring(8, 1);
                }
            }
            if (rut.Length == 10)
            {
                rutDevuelto = rut;
            }
            return rutDevuelto;
        }

        public static bool ValidaRut(string rut)
        {
            if (rut.Trim().Length == 0)
                return true;

            if (rut.Trim().Length < 5)
                return false;

            bool valida = true;
            try
            {
                int a1;
                int a2;
                int a3;
                int a4;
                int a5;
                int a6;
                int a7;
                int a8;
                string raya_s;
                int raya;
                int suma;
                int rr;
                int raya2;


                a1 = Int16.Parse(rut.Substring(0, 1)) * 3;
                a2 = Int16.Parse(rut.Substring(1, 1)) * 2;

                a3 = Int16.Parse(rut.Substring(2, 1)) * 7;
                a4 = Int16.Parse(rut.Substring(3, 1)) * 6;
                a5 = Int16.Parse(rut.Substring(4, 1)) * 5;

                a6 = Int16.Parse(rut.Substring(5, 1)) * 4;
                a7 = Int16.Parse(rut.Substring(6, 1)) * 3;
                a8 = Int16.Parse(rut.Substring(7, 1)) * 2;

                suma = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8;
                rr = suma % 11;

                raya_s = rut.Substring(9, 1);
                if (raya_s.Trim().ToUpper() == "K")
                {
                    raya = 10;
                }
                else
                {

                    raya = int.Parse(raya_s);
                }
                raya2 = 11 - rr;
                if (raya2 == 11)
                {
                    raya2 = 0;
                }

                if (raya2 == 11)
                {
                    raya2 = 11;
                }
                if (raya != raya2)
                {
                    //MostrarMensajeError("El R.U.T. ingresado no es válido");
                    valida = false;
                }
            }
            catch
            {
                valida = false;
            }


            return valida;
        }
        public static bool GenerarBoletaElectronica(List<Entidades.DetalleFactura> detalle, Entidades.Factura Factura, Enums.TipoDocumento tipo, out int folio, out string rutaPDF, out Backline.DTE.APIResult apiResult)
        {
            apiResult = null;
            rutaPDF = "";
            var TipoDte = tipo;
            Backline.DTE.ModelDte modelo = new ModelDte();
            Backline.DTE.Encabezado encabezado;
            Backline.DTE.Referencia referencia;
            Backline.DTE.SuperFactura superFactura;
            encabezado = new Encabezado()
            {
                IdDoc = new Documento()
                {
                    TipoDTE = (Int32)TipoDte,
                    IndServicio = 3
                },
                Emisor = new Emisor()
                {
                    //RUTEmisor = "70859400-8"
                    RUTEmisor = VariablesGlobales.UsuarioLogeado.RutEmpresa
                    //GiroEmis= "ACTIVIDADES DE OTRAS ASOCIACIONES N.C.P"
                },
                Receptor = new Receptor()
                {
                    RUTRecep = Factura.Rut,
                    RznSocRecep = Factura.RazonSocial,
                    Contacto = "..."
                }
            };

            //referencia = new Referencia() { CodVndor = "codV", CodCaja = "ooo" };

            string sucursal = VariablesGlobales.UsuarioLogeado.NombreEstablecimiento;
            superFactura = new SuperFactura();
            // superFactura = new SuperFactura() {  HoraEmis = Factura.Fecha.ToShortTimeString() };

            superFactura = new SuperFactura() { Sucursal = VariablesGlobales.UsuarioLogeado.NombreEstablecimiento + "(caja:" + VariablesGlobales.UsuarioLogeado.Nombre + ")", HoraEmis = Factura.Fecha.ToShortTimeString() };

            modelo.Encabezado = encabezado;
            modelo.SuperFactura = superFactura;

            if (modelo.Detalles == null)
                modelo.Detalles = new List<Detalle>();

            foreach (var a in detalle)
            {
                Backline.DTE.Detalle detalleP = new Detalle();
                detalleP.NmbItem = a.DescripcionProducto;
                //detalleP.DscItem = a.DescripcionProducto;
                detalleP.QtyItem = int.Parse(a.Cantidad.ToString());
                detalleP.UnmdItem = "";// Utility.GetDescription(Enums.UnidadMedida.Unidades);
                detalleP.PrcItem = int.Parse(a.Valor.ToString());
                modelo.Detalles.Add(detalleP);
            }

            if (TipoDte == Enums.TipoDocumento.FacturaElectronica)
            {
                modelo.Encabezado.Receptor.DirRecep = "Eliodoro Yañez 1947";
                modelo.Encabezado.Receptor.CmnaRecep = "Providencia";
                modelo.Encabezado.Receptor.CiudadRecep = "Santiago";
                modelo.Encabezado.Receptor.GiroRecep = "Asesorías Informáticas";
            }

            string ambiente = VariablesGlobales.UsuarioLogeado.Ambiente.ToLower();
            Transaction trx = new Transaction();

            //System.Windows.Forms.MessageBox.Show("Ambiente:" + ambiente);
            
            var dte = trx.GenerarDTE(modelo, VariablesGlobales.UsuarioLogeado.Usuario_FE, VariablesGlobales.UsuarioLogeado.Clave_FE, System.IO.Directory.GetCurrentDirectory(), ambiente.ToLower());
            apiResult = dte;
            //System.Windows.Forms.MessageBox.Show("Salio del Generar" + dte.Message);
            folio = 0;
            if (dte.ok)
            {
                //System.Windows.Forms.MessageBox.Show("Salio ok");
                folio = dte.folio;
                string b = "Boleta_" + folio.ToString() + "111";
                // PortalGestion.DAL.FacturaDAO.SeteaNumero(Factura.Id, dte.folio);
                string pdfPath = Path.Combine(dte.Path, dte.FileGuid + ".pdf");
                string nuevoNombre = Path.Combine(dte.Path, b + ".pdf");
                System.IO.File.Move(pdfPath, nuevoNombre);
                rutaPDF = nuevoNombre;

                //Process.Start(nuevoNombre);
                //SubirArchivo(pdfPath, Factura);
                return true;
            }
            else
            {
                
                return false;
            }


        }

        public static bool GenerarBoletaElectronicaRapida(List<Entidades.DetalleFactura> detalle, Entidades.Factura Factura, Enums.TipoDocumento tipo, out int folio, out string rutaPDF, out Backline.DTE.APIResult apiResult)
        {
            apiResult = null;
            rutaPDF = "";
            var TipoDte = tipo;
            Backline.DTE.ModelDte modelo = new ModelDte();
            Backline.DTE.Encabezado encabezado;
            Backline.DTE.Referencia referencia;
            Backline.DTE.SuperFactura superFactura;
            encabezado = new Encabezado()
            {
                IdDoc = new Documento()
                {
                    TipoDTE = (Int32)TipoDte,
                    IndServicio = 3
                },
                Emisor = new Emisor()
                {
                    //RUTEmisor = "70859400-8"
                    RUTEmisor = VariablesGlobales.UsuarioLogeado.RutEmpresa
                }
                //Receptor = new Receptor()
                //{
                //    RUTRecep = Factura.Rut,
                //    RznSocRecep = Factura.RazonSocial,
                //    Contacto = "..."
                //}
            };

            //referencia = new Referencia() { CodVndor = "codV", CodCaja = "ooo" };

            string sucursal = VariablesGlobales.UsuarioLogeado.NombreEstablecimiento;
            superFactura = new SuperFactura() { Sucursal = sucursal };
            // superFactura = new SuperFactura() {  HoraEmis = Factura.Fecha.ToShortTimeString() };

            modelo.Encabezado = encabezado;
            modelo.SuperFactura = superFactura;

            if (modelo.Detalles == null)
                modelo.Detalles = new List<Detalle>();

            foreach (var a in detalle)
            {
                Backline.DTE.Detalle detalleP = new Detalle();
                detalleP.NmbItem = a.DescripcionProducto;
                //detalleP.DscItem = a.DescripcionProducto;
                detalleP.QtyItem = int.Parse(a.Cantidad.ToString());
                detalleP.UnmdItem = "";// Utility.GetDescription(Enums.UnidadMedida.Unidades);
                detalleP.PrcItem = int.Parse(a.Valor.ToString());
                modelo.Detalles.Add(detalleP);
            }

            if (TipoDte == Enums.TipoDocumento.FacturaElectronica)
            {
                modelo.Encabezado.Receptor.DirRecep = "Eliodoro Yañez 1947";
                modelo.Encabezado.Receptor.CmnaRecep = "Providencia";
                modelo.Encabezado.Receptor.CiudadRecep = "Santiago";
                modelo.Encabezado.Receptor.GiroRecep = "Asesorías Informáticas";
            }

            string ambiente = VariablesGlobales.UsuarioLogeado.Ambiente.ToLower();
            Transaction trx = new Transaction();

            //System.Windows.Forms.MessageBox.Show("Ambiente:" + ambiente);

            var dte = trx.GenerarDTE(modelo, VariablesGlobales.UsuarioLogeado.Usuario_FE, VariablesGlobales.UsuarioLogeado.Clave_FE, System.IO.Directory.GetCurrentDirectory(), ambiente.ToLower());
            apiResult = dte;
            //System.Windows.Forms.MessageBox.Show("Salio del Generar" + dte.Message);
            folio = 0;
            if (dte.ok)
            {
                //System.Windows.Forms.MessageBox.Show("Salio ok");
                folio = dte.folio;
                string b = "Boleta_" + folio.ToString();
                // PortalGestion.DAL.FacturaDAO.SeteaNumero(Factura.Id, dte.folio);
                string pdfPath = Path.Combine(dte.Path, dte.FileGuid + ".pdf");
                string nuevoNombre = Path.Combine(dte.Path, b + ".pdf");
                System.IO.File.Move(pdfPath, nuevoNombre);
                rutaPDF = nuevoNombre;

                //Process.Start(nuevoNombre);
                //SubirArchivo(pdfPath, Factura);
                return true;
            }
            else
            {

                return false;
            }


        }
    }
}
