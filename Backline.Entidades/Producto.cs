using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backline.Entidades
{
    public class Producto
    {
        public Int32 Id { get; set; }
        public Int32 IdRlEmpBode { get; set; }
        public Int32 EmpId { get; set; }
        public Int32 ClasId { get; set; }
        public Int32 CtaId { get; set; }
        public Int32 ViaId { get; set; }
        public Int32 BodeId { get; set; }
        public Int32 ListaPrecioId { get; set; }
        public Int32 Precio { get; set; }
        public String ListaPrecioDescripcion { get; set; }

        public String Familia { get; set; }

        public String Marca { get; set; }
        public String NombreFantasia { get; set; }
        public String NombreProveedor { get; set; }
        public String BodeDescripcion { get; set; }
        public String Clase { get; set; }
        public String Codigo { get; set; }

        public String RegistroISP { get; set; }
        public String CodigoCompra { get; set; }
        public String Descripcion { get; set; }
        public Int32 UnidId { get; set; }
        public String Unidad { get; set; }
        public Decimal Stock { get; set; }
        public Decimal Valor { get; set; }
        public int PrecioMedioPonderado { get; set; }
        public int PrecioPromedioPonderado { get; set; }

        public Decimal Margen { get; set; }

        public Decimal Descuento { get; set; }


        public Byte[] Imagen_producto { get; set; }
        public String RutaImagen { get; set; }
        public bool bloqueado { get; set; }
        public bool EsFraccionado { get; set; }
        public bool RequiereReceta { get; set; }
        public bool LoteBloqueado { get; set; }
        public string DescripcionBloqueado { get; set; }
        public Int32 Cantidad { get; set; }
        public Decimal NivelCritico { get; set; }
        public Decimal ValorVenta { get; set; }
        public Decimal ValorReferencia { get; set; }
        public Decimal ValorCompra { get; set; }
        public Boolean Eliminado { get; set; }
        public Boolean Seleccionado { get; set; }
        public Boolean PorReparar { get; set; }
        public String Observacion { get; set; }
        public Boolean EsModificado { get; set; }

        public Boolean Alerta { get; set; }
        public int DiasAlerta { get; set; }

        public String Rut { get; set; }
        public String Funcionario { get; set; }

        public Decimal ValorCostoAnterior { get; set; }
        public Decimal ValorCosto { get; set; }
        public Decimal ValorCostoCalculado { get; set; }
        public string UbicacionDescripcion { get; set; }
        //LOTE
        public string Lote { get; set; }
        public int IdLote { get; set; }
        public DateTime FechaVencimientoLote { get; set; }
        public double ValorLote { get; set; }
        public Decimal StockLote { get; set; }

        //NUEVOS CAMPOS
        public int id_familia { get; set; }
        public int id_color { get; set; }
        public int id_primera_cat { get; set; }
        public int id_segunda_cat { get; set; }
        public int id_talla { get; set; }
        public int id_tipo { get; set; }
        public int id_temporada { get; set; }
        public int id_caracteristica { get; set; } //ROY
        public int cont_id { get; set; }
        public int usuario_id { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_modificacion { get; set; }
        public DateTime fecha_ultima_compra { get; set; }
        public bool Nbd { get; set; }
        //FIN NUEVOS CAMPOS

        public Boolean EsBioEquivalente { get; set; }
        public int UnidadesPorCaja { get; set; }
        public decimal ValorUnidad { get; set; }
        public string EsBioEquivalenteStr
        {
            get
            {
                if (EsBioEquivalente)
                {
                    return "BE";
                }
                else
                {
                    return "NO BE";
                }

            }

        }
        public double DiasVencimiento
        {
            get
            {
                DateTime fechaInicio = FechaVencimientoLote;
                DateTime fechaTermino = DateTime.Now;

                TimeSpan difference = fechaInicio - fechaTermino;

                if (StockLote <= 0)
                {
                    return 0;
                }

                return difference.Days;
            }
        }
    }
}
