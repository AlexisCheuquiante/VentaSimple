using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacEleUtils
{
    public class Utiles
    {
       
        public static string ObtenerFecha(DateTime fecha)
        {
            return fecha.Year.ToString("0000") + "-" + fecha.Month.ToString("00") + "-" +  fecha.Day.ToString("00");
        }

        public static string ObtieneXMLBoleta_API(Backline.DTE.ModelDte modelo)
        {
            StringBuilder sb = new StringBuilder();

            double SumaNeto = 0;
            foreach (var a in modelo.Detalles)
            {
                SumaNeto = SumaNeto + (double.Parse(a.QtyItem.ToString()) * double.Parse(a.PrcItem.ToString()));
            }
            double total = SumaNeto * 1.19;
            double iva = total - SumaNeto;

            SumaNeto = Math.Round(SumaNeto, 0);
            total = Math.Round(total, 0);
            iva = Math.Round(iva,0);

            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.Append("<DTE version=\"1.0\">");
            sb.Append("<Documento>");
            sb.Append("<Encabezado>");
            sb.Append("<IdDoc>");
            sb.Append("<TipoDTE>39</TipoDTE>");
            sb.Append("<Folio>0</Folio>");
            sb.Append("<FchEmis>" + FacEleUtils.Utiles.ObtenerFecha(DateTime.Now) + "</FchEmis>");
            sb.Append("<IndServicio>1</IndServicio>");
            //sb.Append("<IndMntNeto>2</IndMntNeto>");
            sb.Append("</IdDoc>");
            sb.Append("<Emisor>");
            sb.Append("<RUTEmisor>" + "69255200-8" + "</RUTEmisor>");
            sb.Append("<RznSocEmisor>" + "I. Municipalidad de Lo Barnechea" + "</RznSocEmisor>");
            sb.Append("<GiroEmisor>Giro</GiroEmisor>");
            sb.Append("<DirOrigen>Direccion</DirOrigen>");
            sb.Append("<CmnaOrigen>" + "Lo Barnechea" + "</CmnaOrigen>");
            sb.Append("<CiudadOrigen>" + "Santiago" + "</CiudadOrigen>");
            sb.Append("</Emisor>");
            sb.Append("<Receptor>");
            sb.Append("<RUTRecep>" + modelo.Encabezado.Receptor.RUTRecep + "</RUTRecep>");
            sb.Append("<RznSocRecep>" + modelo.Encabezado.Receptor.RznSocRecep + "</RznSocRecep>");
            sb.Append("<Contacto>contacto</Contacto>");
            sb.Append("<DirRecep>...</DirRecep>");
            sb.Append("<CmnaRecep>" +"Lo Barnechea" + "</CmnaRecep>");
            sb.Append("<CiudadRecep>" + "Santiago" + "</CiudadRecep>");
            sb.Append("</Receptor>");
            sb.Append("<Totales>");
            sb.Append("<MntNeto>0</MntNeto>");
            sb.Append("<MntExe>0</MntExe>");
            sb.Append("<IVA>0</IVA>");
            sb.Append("<MntTotal>" + SumaNeto.ToString() + "</MntTotal>");
            sb.Append("<VlrPagar>" + SumaNeto.ToString() + "</VlrPagar>");
            sb.Append("</Totales>");
            sb.Append("</Encabezado>");


            int contador = 1;
            foreach (var a in modelo.Detalles)
            {
                sb.Append("<Detalle>");
                sb.Append("<NroLinDet>" + contador.ToString() + "</NroLinDet>");
                sb.Append("<NmbItem>" + a.DscItem + "</NmbItem>");
                sb.Append("<QtyItem>" + a.QtyItem.ToString() + "</QtyItem>");
                sb.Append("<UnmdItem></UnmdItem>");

                decimal neto = a.PrcItem / decimal.Parse("1,19");
                int neto1 = int.Parse(Math.Round(neto, 0).ToString());
                int neto2 = Valor_REDONDEO_INT_ARRIBA(neto1);


                decimal subtotal2 = a.PrcItem * neto;
                decimal Subtotal = a.QtyItem * a.PrcItem;
                sb.Append("<PrcItem>" + a.PrcItem.ToString() + "</PrcItem>");
                sb.Append("<MontoItem>" + Subtotal.ToString() + "</MontoItem>");
                sb.Append("</Detalle>");
                contador++;
            }




            sb.Append("</Documento>");
            sb.Append("</DTE>");

            return sb.ToString();
        }

        public static string ObtieneXMLBoleta(Backline.Entidades.Factura factura, List<Backline.Entidades.DetalleFactura> detalleFactura)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.Append("<DTE version=\"1.0\">");
            sb.Append("<Documento>");
            sb.Append("<Encabezado>");
            sb.Append("<IdDoc>");
            sb.Append("<TipoDTE>39</TipoDTE>");
            sb.Append("<Folio>0</Folio>");
            sb.Append("<FchEmis>" + FacEleUtils.Utiles.ObtenerFecha(DateTime.Now) + "</FchEmis>");
            sb.Append("<IndServicio>1</IndServicio>");
            //sb.Append("<IndMntNeto>2</IndMntNeto>");
            sb.Append("</IdDoc>");
            sb.Append("<Emisor>");
            sb.Append("<RUTEmisor>" + factura.RutEmpresa.Trim() + "</RUTEmisor>");
            sb.Append("<RznSocEmisor>" + factura.RazonSocial + "</RznSocEmisor>");
            sb.Append("<GiroEmisor>Giro</GiroEmisor>");
            sb.Append("<DirOrigen>Direccion</DirOrigen>");
            sb.Append("<CmnaOrigen>" + ObtieneComuna(factura) + "</CmnaOrigen>");
            sb.Append("<CiudadOrigen>" + ObtieneCiudad(factura) + "</CiudadOrigen>");
            sb.Append("</Emisor>");
            sb.Append("<Receptor>");
            sb.Append("<RUTRecep>" + factura.RutCliente + "</RUTRecep>");
            sb.Append("<RznSocRecep>" + factura.NombreCliente + "</RznSocRecep>");
            sb.Append("<Contacto>contacto</Contacto>");
            sb.Append("<DirRecep>...</DirRecep>");
            sb.Append("<CmnaRecep>" + ObtieneComuna(factura) + "</CmnaRecep>");
            sb.Append("<CiudadRecep>" + ObtieneCiudad(factura) + "</CiudadRecep>");
            sb.Append("</Receptor>");
            sb.Append("<Totales>");
            sb.Append("<MntNeto>" + factura.Neto + "</MntNeto>");
            sb.Append("<MntExe>0</MntExe>");
            sb.Append("<IVA>" + factura.Iva.ToString() + "</IVA>");
            sb.Append("<MntTotal>" + factura.Total.ToString() + "</MntTotal>");
            sb.Append("<VlrPagar>" + factura.Total.ToString() + "</VlrPagar>");
            sb.Append("</Totales>");
            sb.Append("</Encabezado>");


            int contador = 1;
            foreach (var a  in detalleFactura)
            {
                sb.Append("<Detalle>");
                sb.Append("<NroLinDet>" + contador.ToString() + "</NroLinDet>");
                sb.Append("<NmbItem>" + a.DescripcionProducto + "</NmbItem>");
                sb.Append("<QtyItem>" + a.Cantidad.ToString() + "</QtyItem>");
                sb.Append("<UnmdItem></UnmdItem>");

                decimal neto = a.Valor / decimal.Parse("1,19");
                int neto1 = int.Parse(Math.Round(neto, 0).ToString());
                int neto2 = Valor_REDONDEO_INT_ARRIBA(neto1);


                decimal subtotal2 = a.Cantidad * neto;
                a.Subtotal = a.Cantidad * a.Valor;
                sb.Append("<PrcItem>" + a.Valor.ToString() + "</PrcItem>");
                sb.Append("<MontoItem>" + a.Subtotal.ToString() + "</MontoItem>");
                sb.Append("</Detalle>");
                contador++;
            }





            sb.Append("</Documento>");
            sb.Append("</DTE>");

            return sb.ToString();
        }

        public static string ObtieneXMLBoletaExenta(Backline.Entidades.Factura factura, List<Backline.Entidades.DetalleFactura> detalleFactura)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.Append("<DTE version=\"1.0\">");
            sb.Append("<Documento>");
            sb.Append("<Encabezado>");
            sb.Append("<IdDoc>");
            sb.Append("<TipoDTE>41</TipoDTE>");
            sb.Append("<Folio>0</Folio>");
            sb.Append("<FchEmis>" + FacEleUtils.Utiles.ObtenerFecha(DateTime.Now) + "</FchEmis>");
            sb.Append("<IndServicio>1</IndServicio>");
            //sb.Append("<IndMntNeto>2</IndMntNeto>");
            sb.Append("</IdDoc>");
            sb.Append("<Emisor>");
            sb.Append("<RUTEmisor>" + factura.RutEmpresa.Trim() + "</RUTEmisor>");
            sb.Append("<RznSocEmisor>" + factura.RazonSocial + "</RznSocEmisor>");
            sb.Append("<GiroEmisor>Giro</GiroEmisor>");
            sb.Append("<DirOrigen>Direccion</DirOrigen>");
            sb.Append("<CmnaOrigen>" + ObtieneComuna(factura) + "</CmnaOrigen>");
            sb.Append("<CiudadOrigen>" + ObtieneCiudad(factura) + "</CiudadOrigen>");
            sb.Append("</Emisor>");
            sb.Append("<Receptor>");
            sb.Append("<RUTRecep>" + factura.RutCliente + "</RUTRecep>");
            sb.Append("<RznSocRecep>" + factura.NombreCliente + "</RznSocRecep>");
            sb.Append("<Contacto>contacto</Contacto>");
            sb.Append("<DirRecep>...</DirRecep>");
            sb.Append("<CmnaRecep>" + ObtieneComuna(factura) + "</CmnaRecep>");
            sb.Append("<CiudadRecep>" + ObtieneCiudad(factura) + "</CiudadRecep>");
            sb.Append("</Receptor>");
            sb.Append("<Totales>");
            sb.Append("<MntExe>" + factura.Total.ToString() + "</MntExe>");
            sb.Append("<MntTotal>" + factura.Total.ToString() + "</MntTotal>");
            sb.Append("<VlrPagar>0</VlrPagar>");
            sb.Append("</Totales>");
            sb.Append("</Encabezado>");


            int contador = 1;
            foreach (var a in detalleFactura)
            {
                sb.Append("<Detalle>");
                sb.Append("<NroLinDet>" + contador.ToString() + "</NroLinDet>");
                sb.Append("<IndExe>1</IndExe>");
                sb.Append("<NmbItem>" + a.DescripcionProducto + "</NmbItem>");
                sb.Append("<QtyItem>" + a.Cantidad.ToString() + "</QtyItem>");
                sb.Append("<UnmdItem></UnmdItem>");

                a.Subtotal = a.Cantidad * a.Valor;
                sb.Append("<PrcItem>" + a.Valor.ToString() + "</PrcItem>");
                sb.Append("<MontoItem>" + a.Subtotal.ToString() + "</MontoItem>");
                sb.Append("</Detalle>");
                contador++;
            }




            sb.Append("</Documento>");
            sb.Append("</DTE>");

            return sb.ToString();
        }

        static string ObtieneCiudad(Backline.Entidades.Factura factura)
        {
            //La Reina
            if (factura.EmpId == 31)
            {
                return "Santiago";
            }
            //Constitución
            if (factura.EmpId == 48)
            {
                return "Constitución";
            }
            else
            {
                return "";
            }
        }
        static string ObtieneComuna(Backline.Entidades.Factura factura)
        {
            if (factura.EmpId == 31)
            {
                return "La Reina";
            }
            //Constitución
            if (factura.EmpId == 48)
            {
                return "Constitución";
            }
            else
            {
                return "";
            }
        }

        public static int Valor_REDONDEO_INT_ARRIBA(int valor)
        {
            int ultimo = int.Parse(valor.ToString().Substring(valor.ToString().Length - 1, 1));

            if (ultimo >= 6)
            {
                return valor = valor + (10 - ultimo);
            }
            if (ultimo < 6)
            {
                return valor = valor - (ultimo);
            }

            return 0;
        }
    }
}
