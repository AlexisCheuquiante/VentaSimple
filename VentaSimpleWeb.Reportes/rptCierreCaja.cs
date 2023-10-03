using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

namespace VentaSimpleWeb.Reportes
{
    public partial class rptCierreCaja : DevExpress.XtraReports.UI.XtraReport
    {
        public rptCierreCaja()
        {
            InitializeComponent();
        }
        public void Cargar(Backline.Entidades.Caja caja)
        {
            Backline.Entidades.Filtro filtro = new Backline.Entidades.Filtro();
            filtro.Fecha_Ingreso = caja.Fecha_Cierre;
            filtro.UsrId = caja.Id_Cajero;
            List<Backline.Entidades.Caja> lista = Backline.DAL.CajaDAL.ObtenerCuadreCaja(filtro);

            Backline.Entidades.Usuario usuario = new Backline.Entidades.Usuario();
            usuario.Id = caja.Id_Cajero;
            var cajero = Backline.DAL.UsuariosDAL.ObtenerUsuario(usuario);

            lbFechaCierre.Text = caja.Fecha_Cierre.ToString();
            lbEstablecimiento.Text = cajero[0].NombreEstablecimiento.Trim();
            lbNombreCajero.Text = cajero[0].Nombre.Trim();
            lbObservacion.Text = caja.Observacion.Trim();

            DataSet dataSet1 = new DataSet();
            dataSet1.DataSetName = "dataSet";
            DataTable dataTable1 = new DataTable();

            dataSet1.Tables.Add(dataTable1);
            dataTable1.TableName = "Table";


            dataTable1.Columns.Add("Tipo Documento", typeof(string));
            dataTable1.Columns.Add("Tipo Pago", typeof(string));
            dataTable1.Columns.Add("Total", typeof(string));

            foreach (var d in lista)
            {
                dataTable1.Rows.Add(new object[] { d.Tipo_Documento, d.Tipo_Pago, d.Total });
            }


            this.DataSource = dataSet1;
            this.DataMember = dataTable1.TableName;

            this.clTipoDocumento.DataBindings.Add("Text", null, dataTable1.Columns[0].Caption);
            this.clTipoPago.DataBindings.Add("Text", null, dataTable1.Columns[1].Caption);
            this.clTotal.DataBindings.Add("Text", null, dataTable1.Columns[2].Caption);
            
        }
    }
}
