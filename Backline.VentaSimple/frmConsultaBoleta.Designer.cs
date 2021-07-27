namespace Backline.VentaSimple
{
    partial class frmConsultaBoleta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsultaBoleta));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.Filtros = new DevExpress.XtraEditors.GroupControl();
            this.lbHasta = new DevExpress.XtraEditors.LabelControl();
            this.lbDesde = new DevExpress.XtraEditors.LabelControl();
            this.txtHasta = new DevExpress.XtraEditors.DateEdit();
            this.txtDesde = new DevExpress.XtraEditors.DateEdit();
            this.grdDatos = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clNumero = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clFecha = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clContribuyente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clGlosa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clUsuario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Filtros)).BeginInit();
            this.Filtros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHasta.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHasta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesde.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesde.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnBuscar);
            this.groupControl1.Controls.Add(this.Filtros);
            this.groupControl1.Controls.Add(this.grdDatos);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(800, 450);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Consulta de boleta";
            // 
            // Filtros
            // 
            this.Filtros.Controls.Add(this.lbHasta);
            this.Filtros.Controls.Add(this.lbDesde);
            this.Filtros.Controls.Add(this.txtHasta);
            this.Filtros.Controls.Add(this.txtDesde);
            this.Filtros.Location = new System.Drawing.Point(5, 33);
            this.Filtros.Name = "Filtros";
            this.Filtros.Size = new System.Drawing.Size(155, 90);
            this.Filtros.TabIndex = 1;
            this.Filtros.Text = "Filtros";
            // 
            // lbHasta
            // 
            this.lbHasta.Location = new System.Drawing.Point(8, 59);
            this.lbHasta.Name = "lbHasta";
            this.lbHasta.Size = new System.Drawing.Size(28, 13);
            this.lbHasta.TabIndex = 3;
            this.lbHasta.Text = "Hasta";
            // 
            // lbDesde
            // 
            this.lbDesde.Location = new System.Drawing.Point(8, 28);
            this.lbDesde.Name = "lbDesde";
            this.lbDesde.Size = new System.Drawing.Size(30, 13);
            this.lbDesde.TabIndex = 2;
            this.lbDesde.Text = "Desde";
            // 
            // txtHasta
            // 
            this.txtHasta.EditValue = null;
            this.txtHasta.Location = new System.Drawing.Point(44, 52);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtHasta.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtHasta.Size = new System.Drawing.Size(100, 20);
            this.txtHasta.TabIndex = 1;
            // 
            // txtDesde
            // 
            this.txtDesde.EditValue = null;
            this.txtDesde.Location = new System.Drawing.Point(44, 25);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDesde.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDesde.Size = new System.Drawing.Size(100, 20);
            this.txtDesde.TabIndex = 0;
            // 
            // grdDatos
            // 
            this.grdDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDatos.Location = new System.Drawing.Point(5, 129);
            this.grdDatos.MainView = this.gridView1;
            this.grdDatos.Name = "grdDatos";
            this.grdDatos.Size = new System.Drawing.Size(785, 316);
            this.grdDatos.TabIndex = 0;
            this.grdDatos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clNumero,
            this.clFecha,
            this.clContribuyente,
            this.clGlosa,
            this.clTotal,
            this.clUsuario});
            this.gridView1.GridControl = this.grdDatos;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            // 
            // clNumero
            // 
            this.clNumero.Caption = "N° Sii";
            this.clNumero.FieldName = "NumeroSII";
            this.clNumero.Name = "clNumero";
            this.clNumero.Visible = true;
            this.clNumero.VisibleIndex = 0;
            this.clNumero.Width = 83;
            // 
            // clFecha
            // 
            this.clFecha.Caption = "Fecha de ingreso";
            this.clFecha.FieldName = "Fecha";
            this.clFecha.Name = "clFecha";
            this.clFecha.Visible = true;
            this.clFecha.VisibleIndex = 1;
            this.clFecha.Width = 102;
            // 
            // clContribuyente
            // 
            this.clContribuyente.Caption = "Contribuyente";
            this.clContribuyente.FieldName = "Contribuyente";
            this.clContribuyente.Name = "clContribuyente";
            this.clContribuyente.Visible = true;
            this.clContribuyente.VisibleIndex = 2;
            this.clContribuyente.Width = 143;
            // 
            // clGlosa
            // 
            this.clGlosa.Caption = "Glosa";
            this.clGlosa.FieldName = "Glosa";
            this.clGlosa.Name = "clGlosa";
            this.clGlosa.Visible = true;
            this.clGlosa.VisibleIndex = 3;
            this.clGlosa.Width = 205;
            // 
            // clTotal
            // 
            this.clTotal.Caption = "Total";
            this.clTotal.FieldName = "Total";
            this.clTotal.Name = "clTotal";
            this.clTotal.Visible = true;
            this.clTotal.VisibleIndex = 4;
            this.clTotal.Width = 80;
            // 
            // clUsuario
            // 
            this.clUsuario.Caption = "Usuario";
            this.clUsuario.FieldName = "Usuario";
            this.clUsuario.Name = "clUsuario";
            this.clUsuario.Visible = true;
            this.clUsuario.VisibleIndex = 5;
            this.clUsuario.Width = 153;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Appearance.Options.UseForeColor = true;
            this.btnBuscar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnBuscar.ImageOptions.SvgImage")));
            this.btnBuscar.Location = new System.Drawing.Point(167, 88);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(132, 34);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // frmConsultaBoleta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupControl1);
            this.Name = "frmConsultaBoleta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmConsultaVentas";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Filtros)).EndInit();
            this.Filtros.ResumeLayout(false);
            this.Filtros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHasta.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHasta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesde.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesde.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grdDatos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn clNumero;
        private DevExpress.XtraGrid.Columns.GridColumn clFecha;
        private DevExpress.XtraGrid.Columns.GridColumn clContribuyente;
        private DevExpress.XtraGrid.Columns.GridColumn clGlosa;
        private DevExpress.XtraGrid.Columns.GridColumn clTotal;
        private DevExpress.XtraGrid.Columns.GridColumn clUsuario;
        private DevExpress.XtraEditors.GroupControl Filtros;
        private DevExpress.XtraEditors.LabelControl lbHasta;
        private DevExpress.XtraEditors.LabelControl lbDesde;
        private DevExpress.XtraEditors.DateEdit txtHasta;
        private DevExpress.XtraEditors.DateEdit txtDesde;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
    }
}