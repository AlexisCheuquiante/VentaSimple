using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backline.Entidades
{
    public class Factura 
    {
        public bool Pagada { get; set; }

        public bool EsFacturacionElectronica { get; set; }
        public bool TieneBoletaAdjunta { get; set; }

        public Int32 Id { get; set; }

        public Int32 EstId { get; set; }
        public Int32 TipoAjusteId { get; set; }
        public Int32 IdSolicitud { get; set; }

        public Int32 UsrIdCaja { get; set; }

        public Int32 NumeroBoletaFisica { get; set; }
        
        public Int32 DocumentoOrigenINT { get; set; }

        public Int32 RutContribuyente { get; set; }
        public Int32 EmpId { get; set; }
        public string descripcionTipoFactura;
        

        public string Establecimiento;
        public string Cajero { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int Fecha_INT { get; set; }
        public Int16 Mes { get; set; }
        public Int16 Año { get; set; }
        public Int32 ContId { get; set; }
        public Int32 Cliente_cto_id { get; set; }
        public Int32 TipoCompra_Id { get; set; }
        public Int32 Numero { get; set; }
        public Int32 CondId { get; set; }

        public Int32 PacId { get; set; }
        public Int32 MedId { get; set; }
        public Int32 PacId_Retirador { get; set; }

        public Int32 FdesId { get; set; }
        public Int32 PlzoId { get; set; }
        public Int32 StDocId { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime Fecha_Ingreso_Prdocutos { get; set; }
        public Int32 Subtotal { get; set; }
        public Int32 Descuento { get; set; }
        public Int32 Exento { get; set; }
        public Double Neto { get; set; }
        public Double Iva { get; set; }
        
        public Int32 TotalRedondeado { get; set; }

        public Int32 Saldo { get; set; }


        public Int32 Total_guias { get; set; }
        public Int32 Total_ventas { get; set; }
        public Int32 Total_ventas_despachado { get; set; }
        public Int32 Total_ventas_pendiente { get; set; }
        public Int32 Abonos { get; set; }
        public Int32 Deuda { get; set; }
        public Boolean Seleccionada { get; set; }

        public Boolean PreVenta { get; set; }
        public Boolean Nula { get; set; }
        public Boolean Eliminada { get; set; }

        public Boolean Modificada { get; set; }
        public Boolean Autorizada { get; set; }
        public Boolean ZyncSoftland { get; set; }
        public Int32 VouIdCentralizacion { get; set; }
        
        public String RazonSocial { get; set; }
        public String CondicionPago { get; set; }
        public String PlazoEntrega { get; set; }
        public String FormaDespacho { get; set; }
        public String DespacharEn { get; set; }
        public String Periodo { get; set; }
        public String Estado { get; set; }
        public String CodigoAutorizacion { get; set; }
        public String Observacion { get; set; }
        public Int32 IdUsuario { get; set; }
        public String DocumentoOrigen { get; set; }
        public Int32 NumeroOC { get; set; }
        public Boolean DocumentoExento { get; set; }
        public Int32 IdArea { get; set; }
        public Int32 IdCentro { get; set; }
        public Int32 IdBodega { get; set; }
        public String BodegaDescripcion { get; set; }
        public Int32 IdBodegaDestino { get; set; }
        public String BodegaDestinoDescripcion { get; set; }
        public String Solicitante { get; set; }
        public String AreaDescripcion { get; set; }
        public String CentroCostoDescripcion { get; set; }
        public Int32 IdSolicitante { get; set; }
        public Int32 NumeroDocumentoCargado { get; set; }
        public Int32 IdCentroCostoDespacho { get; set; }
        public String RutRetira { get; set; }
        public String NombreRetira { get; set; }
        public String RutSolicitante { get; set; }
        public String NombreSolicitante { get; set; }
        public String DireccionDespacho { get; set; }
        public String RegionDespacho { get; set; }
        public String ComunaDespacho { get; set; }
        public Int32 IdRegionDespacho { get; set; }
        public Int32 IdComunaDespacho { get; set; }
        public Int32 IdPedidoRelacionado { get; set; }
        public Int32 IdEstadoPedido { get; set; }
        public Int32 CantidadBultos { get; set; }
        public Int32 NumeroRecepcion { get; set; }
        public Decimal Cantidad { get; set; }
        public bool seguimiento { get; set; }

        public string OrdenCompraCliente { get; set; }
        public string NumeroSolicitudCliente { get; set; }
        public bool EsRecepcionProductos { get; set; }
        public bool EsDirecta { get; set; }
        public bool PedidoVerificado { get; set; }
        public bool EsDespachoPersonalizado { get; set; }
        private string descripcionEstado;
        public List<DetalleFactura> DetalleFactura { get; set; }
        public List<DetalleFactura> DetalleFacturaFacturacion { get; set; }

        public string NombreCajero { get; set; }
        public string NombreDigitador { get; set; }

        //ARAMARK
        public bool Migrada { get; set; }
        public DateTime FechaIngresoProducto { get; set; }
        public DateTime FechaMigracion { get; set; }
        public String CentroCostoCliente { get; set; }
        public String CodigoCtoCliente { get; set; }
        public String DescCtoCliente { get; set; }
        public string NombreCLienteOC { get; set; }
        public string TipoCompraOC { get; set; }
        public Boolean EsMigrada { get; set; }
        public string DescripcionBodega { get; set; }
        public bool hojaRuta { get; set; }
        public Int32 NumeroHojaRuta { get; set; }
        public DateTime? fechaSalida { get; set; }
        public DateTime? fechaLlegada { get; set; }
        public int tipoTransporte { get; set; }
        public string nombreTransporte { get; set; }
        public string tipoFactura { get; set; }
        public byte[] pdf_file { get; set; }
        public string nombre_archivo { get; set; }
        public bool electronica { get; set; }
        public string tipoDocumento { get; set; }
        public bool GuiaFacturada { get; set; }
        public int Num_factura_asociado { get; set; }
        public DateTime fechaPedido { get; set; }
        public string NumeroPedido { get; set; }
        public bool ocAprobada { get; set; }
        public string razonReferencia { get; set; }

        public Boolean EsCenabast { get; set; }

        public decimal PorcentajeCENABAST { get; set; }
        
        public int Usr_Id { get; set; }

        public string TipoDocumentoStr { get; set; }
        public string FechaMostrar
        {
            get
            {
                return Fecha.ToShortDateString();
            }
        }
        public int Tido_Id { get; set; }
        
        public Int64 NumeroSII { get; set; }
        public int DocumentoReferencia { get; set; }
        public String Rut { get; set; }
        public string Contribuyente { get; set; }
        public string Glosa { get; set; }
        public Int32 Total { get; set; }
        public string Usuario { get; set; }
        public string Sucursal { get; set; }
        public int Tipa_Id { get; set; }
        public string TipoPago { get; set; }
        public bool Eliminado { get; set; }
        public bool Es_Afecta { get; set; }
        public string RutEmpresa { get; set; }
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal NetoFacele { get; set; }
        public decimal IvaFacele { get; set; }
    }
}
