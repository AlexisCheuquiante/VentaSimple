using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backline.DTE;
using System.IO;
using System.Web;
using Backline;

namespace VentaSimpleWeb
{
    public class Utiles
    {
        public static bool GenerarBoletaElectronica(List<Backline.Entidades.DetalleFactura> detalle, Backline.Entidades.Factura Factura, Enums.TipoDocumento tipo, out int folio, out string rutaPDF, out Backline.DTE.APIResult apiResult)
        {
            apiResult = null;
            rutaPDF = "";
            var TipoDte = tipo;
            Backline.DTE.ModelDte modelo = new ModelDte();
            Backline.DTE.Encabezado encabezado;
            Backline.DTE.Referencia referencia;
            Backline.DTE.SuperFactura superFactura;

            if (SessionH.Usuario.Emp_Id == 14)
            {
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
                        RUTEmisor = SessionH.Usuario.RutEmpresa
                        //GiroEmis= "ACTIVIDADES DE OTRAS ASOCIACIONES N.C.P"
                    }
                    
                    //,
                    //Receptor = new Receptor()
                    //{

                    //    RUTRecep = " ",
                    //    RznSocRecep = " ",
                    //    Contacto = "..."
                    //}
                };
            }
           else
            {
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
                        RUTEmisor = SessionH.Usuario.RutEmpresa
                        //GiroEmis= "ACTIVIDADES DE OTRAS ASOCIACIONES N.C.P"
                    },
                    Receptor = new Receptor()
                    {

                        RUTRecep = Factura.Rut,
                        RznSocRecep = Factura.Contribuyente,
                        Contacto = "..."
                    }
                };
            }

            //referencia = new Referencia() { CodVndor = "codV", CodCaja = "ooo" };

            string sucursal = SessionH.Usuario.NombreEstablecimiento;
            superFactura = new SuperFactura();
            // superFactura = new SuperFactura() {  HoraEmis = Factura.Fecha.ToShortTimeString() };

            superFactura = new SuperFactura() { Sucursal = SessionH.Usuario.NombreEstablecimiento + "(caja:" + SessionH.Usuario.Nombre + ")", HoraEmis = Factura.Fecha.ToShortTimeString() };

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

            string ambiente = SessionH.Usuario.Ambiente;
            Transaction trx = new Transaction();

            //System.Windows.Forms.MessageBox.Show("Ambiente:" + ambiente);

            var dte = trx.GenerarDTE(modelo, SessionH.Usuario.Usuario_FE, SessionH.Usuario.Clave_FE, System.IO.Directory.GetCurrentDirectory(), ambiente.ToLower());
            apiResult = dte;
            //System.Windows.Forms.MessageBox.Show("Salio del Generar" + dte.Message);
            folio = 0;
            if (dte.ok)
            {
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

                //System.Windows.Forms.MessageBox.Show("Salio ok");
                folio = dte.folio;
                string b = rutEmpresa + a + "Boleta_" + folio.ToString();
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
        public static bool GenerarNotaCredito(List<Backline.Entidades.DetalleFactura> detalle, Backline.Entidades.Factura Factura, Enums.TipoDocumento tipo, out int folio, out string rutaPDF, out Backline.DTE.APIResult apiResult)
        {
            rutaPDF = "";
            var TipoDte = tipo;
            Backline.DTE.ModelDteNotaCredito modelo = new ModelDteNotaCredito();
            Backline.DTE.EncabezadoNotaCredito encabezado;
            Backline.DTE.ReferenciaNotaCredito referencia;
            if (SessionH.Usuario.Emp_Id == 14)
            {
                encabezado = new EncabezadoNotaCredito()
                {
                    IdDoc = new DocumentoNotaCredito()
                    {
                        TipoDTE = (Int32)TipoDte,
                        IndServicio = 3,
                        MntBruto = 1
                    },
                    Emisor = new EmisorNotaCredito()
                    {
                        //RUTEmisor = "70859400-8"
                        RUTEmisor = SessionH.Usuario.RutEmpresa
                    },
                    Receptor = new ReceptorNotaCredito()
                    {
                        RUTRecep = "69210100-6",
                        RznSocRecep = "I.Municipalidad de Osorno",
                        Contacto = "..."
                    }
                };
            }
            else
            {
                encabezado = new EncabezadoNotaCredito()
                {
                    IdDoc = new DocumentoNotaCredito()
                    {
                        TipoDTE = (Int32)TipoDte,
                        IndServicio = 3,
                        MntBruto = 1
                    },
                    Emisor = new EmisorNotaCredito()
                    {
                        //RUTEmisor = "70859400-8"
                        RUTEmisor = SessionH.Usuario.RutEmpresa
                    },
                    Receptor = new ReceptorNotaCredito()
                    {
                        RUTRecep = Factura.Rut,
                        RznSocRecep = Factura.Contribuyente,
                        Contacto = "..."
                    }
                };
            }
                
            //encabezado.FchEmis = Factura.Fecha.Year.ToString() + "-" + Factura.Fecha.Month.ToString("00") + "-" + Factura.Fecha.Day.ToString("00");

            string fechaSII = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00");
            referencia = new Backline.DTE.ReferenciaNotaCredito()
            {
                CodRef = 1,
                TpoDocRef = 39,
                FolioRef = Factura.DocumentoOrigenINT,
                FchRef = fechaSII,
                RazonRef = "Nota de crédito a la boleta electrónica N°" + Factura.DocumentoOrigenINT.ToString()
            };

            modelo.Referencia = referencia;
            modelo.Encabezado = encabezado;

            if (modelo.Detalles == null)
                modelo.Detalles = new List<DetalleNotaCredito>();

            foreach (var a in detalle)
            {
                Backline.DTE.DetalleNotaCredito detalleP = new DetalleNotaCredito();
                detalleP.NmbItem = a.DescripcionProducto;
                //detalleP.DscItem = a.DescripcionProducto;
                detalleP.QtyItem = int.Parse(a.Cantidad.ToString());
                detalleP.UnmdItem = "";// Utility.GetDescription(Enums.UnidadMedida.Unidades);
                detalleP.PrcItem = int.Parse(a.Valor.ToString());
                
                modelo.Detalles.Add(detalleP);
            }

            if (TipoDte == Enums.TipoDocumento.FacturaElectronica)
            {
                modelo.Encabezado.Receptor.DirRecep = "Marchant Pereira 10";
                modelo.Encabezado.Receptor.CmnaRecep = "Providencia";
                modelo.Encabezado.Receptor.CiudadRecep = "Santiago";
                modelo.Encabezado.Receptor.GiroRecep = "Asesorías Informáticas";
            }

            string ambiente = SessionH.Usuario.Ambiente;
            Transaction trx = new Transaction();
            var dte = trx.GenerarDTE(modelo, SessionH.Usuario.Usuario_FE, SessionH.Usuario.Clave_FE, System.IO.Directory.GetCurrentDirectory(), ambiente.ToLower());
            apiResult = dte;
            folio = 0;
            if (dte.ok)
            {
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

                folio = dte.folio;
                string b = rutEmpresa + a + "NotaCrédito_" + folio.ToString();
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
        public static int FechaToInteger(DateTime date)
        {
            string a = date.Year.ToString();
            string b = date.Month.ToString("00");
            string c = date.Day.ToString("00");
            string d = a + b + c;

            if (d == "10101")
                d = "0";

            return int.Parse(d);
        }
        public static string ReversaFecha(DateTime fecha)
        {
            string yy = fecha.Year.ToString("0000");
            string mm = fecha.Month.ToString("00");
            string dd = fecha.Day.ToString("00");

            return yy + "-" + mm + "-" + dd;

        }
        public static DateTime FechaObtenerMinimo(DateTime fecha)
        {
            int year = fecha.Year;
            int mes = fecha.Month;
            int day = fecha.Day;

            return new DateTime(year, mes, day, 0, 0, 0);
        }
        public static DateTime FechaObtenerMaximo(DateTime fecha)
        {
            int year = fecha.Year;
            int mes = fecha.Month;
            int day = fecha.Day;

            return new DateTime(year, mes, day, 23, 59, 59);
        }
        public static int ObtenerRutCode(string code)
        {
            code = code.Replace("K", "");
            if (code.Trim().Length == 12)
            {
                var a = code.Substring(0, 2);
                var b = code.Substring(3, 3);
                var c = code.Substring(7, 3);
                string codeNum = a + b + c;
                return int.Parse(codeNum);
            }
            else
            {
                return int.Parse(code);
            }


        }
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
        public static int ObtieneRut_INT(string rut)
        {
            if (rut.Trim().Length == 0)
                return 0;
            int rutDevuelto = 0;

            if (rut.Trim().Length == 8)
            {
                rutDevuelto = int.Parse(rut.Substring(0, 7));
            }
            if (rut.Trim().Length == 9)
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
    }
}