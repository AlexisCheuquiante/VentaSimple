using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backline.Entidades
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int IdEtiqueta { get; set; }
        public bool Asociada { get; set; }
        public bool CargadaAsociada { get; set; }
        public bool ModificarPrecio { get; set; }
        public bool EnFrio { get; set; }
        public bool EsFraccionado { get; set; }
        public int NumLinea { get; set; }
        public int FactId_GuiaDespachoRelacionada { get; set; }
        public int FactId { get; set; }
        public int ProdId { get; set; }
        public int CtaId { get; set; }
        public int AreaId { get; set; }
        public int ViaId { get; set; }
        public int PacId { get; set; }
        public int CtoId { get; set; }
        public int EmpId { get; set; }
        public int IdActividad { get; set; }
        public string PtoItem { get; set; }
        public Producto Producto { get; set; }
        public string DescripcionProducto { get; set; }


        public string RegistroISP { get; set; }
        public string Registro { get; set; }
        public string DescripcionPrincipioActivo { get; set; }
        public string CodigoCuenta { get; set; }
        public string DescripcionCuenta { get; set; }
        public string DescripcionArea { get; set; }

        public string DescripcionUnidad { get; set; }
        public string DescripcionCentro { get; set; }

        public decimal PorcentajeCENABAST { get; set; }
        public decimal Cantidad { get; set; }

        public decimal CantidadDiferencia { get; set; }
        public decimal CantidadVerificada { get; set; }
        public decimal CantidadRestante { get; set; }
        public decimal CantidadGuiasDespacho { get; set; }
        public decimal CantidadFacturada { get; set; }
        public decimal CantidadActual { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorDecimal { get; set; }

        public decimal Factor { get; set; }
        public decimal CantidadEtiquetas { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public int BodegaId { get; set; }
        public string Bodega { get; set; }
        public Boolean Eliminado { get; set; }
        public Boolean? Anexo { get; set; }
        public int NumeroDocumentoOrigen { get; set; }
        public int IdDocumentoOrigen { get; set; }
        public string CodigoProducto { get; set; }
        public string RutFuncionario { get; set; }
        public string RutBeneficiario { get; set; }

        public string NombreFuncionario { get; set; }
        public int? IdFuncionario { get; set; }
        public Int32 IdPedidoRelacionado { get; set; }
        public DateTime Fecha { get; set; }

        public int IdCont { get; set; }
        public int Numero { get; set; }
        public Boolean Directa { get; set; }
        public Boolean Seleccionado { get; set; }
        public Boolean Nbd { get; set; }
        public float Costo { get; set; }

        public Decimal CostoCalculado { get; set; }
        public int NumeroOC { get; set; }
        public DateTime FechaUltimoMov { get; set; }
        public Decimal ValorPrecioAnterior { get; set; }
        public DateTime FechaFraccionamiento { get; set; }
        public bool rutInvalido { get; set; }
        public bool codigoInvalido { get; set; }

        //ACTIVO FIJO
        public Int32 VidaUtil { get; set; }
        public String DescripcionActivo { get; set; }
        public String CtaCodigoActivo { get; set; }
        public String CtaDescripcionActivo { get; set; }
        public int IdCuentaActivo { get; set; }
        public Boolean EsActivo { get; set; }

        public Boolean Modificar { get; set; }
        public Boolean NoModificar { get; set; }
        public decimal MontoDisponible { get; set; }
        public string Ubicacion { get; set; }

        //ETIQUETA
        public DateTime FechaVencimientoLote { get; set; }
        public string Lote { get; set; }
        public int IdLote { get; set; }
        public string Posologia { get; set; }
        public string NombreBeneficiario { get; set; }



        //--FIN

        public int StockActual { get; set; }

     

    }
}
