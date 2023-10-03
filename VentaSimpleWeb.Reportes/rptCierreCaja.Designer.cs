
namespace VentaSimpleWeb.Reportes
{
    partial class rptCierreCaja
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.TipoDocumento = new DevExpress.XtraReports.UI.XRTableCell();
            this.TipoPago = new DevExpress.XtraReports.UI.XRTableCell();
            this.Total = new DevExpress.XtraReports.UI.XRTableCell();
            this.lbObservacion = new DevExpress.XtraReports.UI.XRLabel();
            this.lbNombreCajero = new DevExpress.XtraReports.UI.XRLabel();
            this.lbFechaCierre = new DevExpress.XtraReports.UI.XRLabel();
            this.lbEstablecimiento = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.clTipoDocumento = new DevExpress.XtraReports.UI.XRTableCell();
            this.clTipoPago = new DevExpress.XtraReports.UI.XRTableCell();
            this.clTotal = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.lbObservacion,
            this.lbNombreCajero,
            this.lbFechaCierre,
            this.lbEstablecimiento});
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 432.1044F;
            this.TopMargin.Name = "TopMargin";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Dpi = 254F;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(25.00002F, 368.6044F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(762F, 63.5F);
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.TipoDocumento,
            this.TipoPago,
            this.Total});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // TipoDocumento
            // 
            this.TipoDocumento.BackColor = System.Drawing.Color.Gray;
            this.TipoDocumento.Dpi = 254F;
            this.TipoDocumento.Multiline = true;
            this.TipoDocumento.Name = "TipoDocumento";
            this.TipoDocumento.StylePriority.UseBackColor = false;
            this.TipoDocumento.StylePriority.UseTextAlignment = false;
            this.TipoDocumento.Text = "Tipo Documento";
            this.TipoDocumento.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.TipoDocumento.Weight = 1.1284245768869956D;
            // 
            // TipoPago
            // 
            this.TipoPago.BackColor = System.Drawing.Color.Gray;
            this.TipoPago.Dpi = 254F;
            this.TipoPago.Multiline = true;
            this.TipoPago.Name = "TipoPago";
            this.TipoPago.StylePriority.UseBackColor = false;
            this.TipoPago.StylePriority.UseTextAlignment = false;
            this.TipoPago.Text = "Tipo Pago";
            this.TipoPago.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.TipoPago.Weight = 0.8715754231130044D;
            // 
            // Total
            // 
            this.Total.BackColor = System.Drawing.Color.Gray;
            this.Total.Dpi = 254F;
            this.Total.Multiline = true;
            this.Total.Name = "Total";
            this.Total.StylePriority.UseBackColor = false;
            this.Total.StylePriority.UseTextAlignment = false;
            this.Total.Text = "Total";
            this.Total.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.Total.Weight = 1D;
            // 
            // lbObservacion
            // 
            this.lbObservacion.Dpi = 254F;
            this.lbObservacion.LocationFloat = new DevExpress.Utils.PointFloat(25.00002F, 262.5899F);
            this.lbObservacion.Multiline = true;
            this.lbObservacion.Name = "lbObservacion";
            this.lbObservacion.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbObservacion.SizeF = new System.Drawing.SizeF(677.3334F, 89.40884F);
            this.lbObservacion.Text = "lbObservacion";
            // 
            // lbNombreCajero
            // 
            this.lbNombreCajero.Dpi = 254F;
            this.lbNombreCajero.LocationFloat = new DevExpress.Utils.PointFloat(25.00002F, 181.0402F);
            this.lbNombreCajero.Multiline = true;
            this.lbNombreCajero.Name = "lbNombreCajero";
            this.lbNombreCajero.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbNombreCajero.SizeF = new System.Drawing.SizeF(677.3334F, 58.42F);
            this.lbNombreCajero.Text = "lbNombreCajero";
            // 
            // lbFechaCierre
            // 
            this.lbFechaCierre.Dpi = 254F;
            this.lbFechaCierre.LocationFloat = new DevExpress.Utils.PointFloat(25.00002F, 25.00002F);
            this.lbFechaCierre.Multiline = true;
            this.lbFechaCierre.Name = "lbFechaCierre";
            this.lbFechaCierre.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbFechaCierre.SizeF = new System.Drawing.SizeF(677.3334F, 58.42F);
            this.lbFechaCierre.Text = "lbFechaCierre";
            // 
            // lbEstablecimiento
            // 
            this.lbEstablecimiento.Dpi = 254F;
            this.lbEstablecimiento.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 103.3992F);
            this.lbEstablecimiento.Multiline = true;
            this.lbEstablecimiento.Name = "lbEstablecimiento";
            this.lbEstablecimiento.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbEstablecimiento.SizeF = new System.Drawing.SizeF(677.3334F, 58.42F);
            this.lbEstablecimiento.Text = "lbEstablecimiento";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrLabel2,
            this.xrLine2,
            this.xrLine1});
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 452F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(448.3334F, 160.343F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(254F, 58.42F);
            this.xrLabel3.Text = "Firma Receptor";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(100.5163F, 160.343F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(254F, 58.42F);
            this.xrLabel2.Text = "Firma Cajero";
            // 
            // xrLine2
            // 
            this.xrLine2.Dpi = 254F;
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(424.3881F, 155.343F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(298.4717F, 5F);
            // 
            // xrLine1
            // 
            this.xrLine1.Dpi = 254F;
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(76.05138F, 155.343F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(298.4717F, 5F);
            // 
            // Detail
            // 
            this.Detail.BackColor = System.Drawing.Color.Transparent;
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 94.30767F;
            this.Detail.HierarchyPrintOptions.Indent = 50.8F;
            this.Detail.Name = "Detail";
            this.Detail.StylePriority.UseBackColor = false;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Dpi = 254F;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(25.00002F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(762F, 63.5F);
            this.xrTable2.StylePriority.UseBorders = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.clTipoDocumento,
            this.clTipoPago,
            this.clTotal});
            this.xrTableRow2.Dpi = 254F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // clTipoDocumento
            // 
            this.clTipoDocumento.Dpi = 254F;
            this.clTipoDocumento.Multiline = true;
            this.clTipoDocumento.Name = "clTipoDocumento";
            this.clTipoDocumento.StylePriority.UseTextAlignment = false;
            this.clTipoDocumento.Text = "clTipoDocumento";
            this.clTipoDocumento.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.clTipoDocumento.Weight = 1.1284245768869956D;
            // 
            // clTipoPago
            // 
            this.clTipoPago.Dpi = 254F;
            this.clTipoPago.Multiline = true;
            this.clTipoPago.Name = "clTipoPago";
            this.clTipoPago.StylePriority.UseTextAlignment = false;
            this.clTipoPago.Text = "clTipoPago";
            this.clTipoPago.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.clTipoPago.Weight = 0.8715754231130044D;
            // 
            // clTotal
            // 
            this.clTotal.Dpi = 254F;
            this.clTotal.Multiline = true;
            this.clTotal.Name = "clTotal";
            this.clTotal.StylePriority.UseTextAlignment = false;
            this.clTotal.Text = "clTotal";
            this.clTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.clTotal.Weight = 1D;
            // 
            // rptCierreCaja
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(15, 18, 432, 452);
            this.PageHeight = 2254;
            this.PageWidth = 984;
            this.PaperKind = System.Drawing.Printing.PaperKind.Number9Envelope;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Version = "21.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel lbObservacion;
        private DevExpress.XtraReports.UI.XRLabel lbNombreCajero;
        private DevExpress.XtraReports.UI.XRLabel lbFechaCierre;
        private DevExpress.XtraReports.UI.XRLabel lbEstablecimiento;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell TipoDocumento;
        private DevExpress.XtraReports.UI.XRTableCell TipoPago;
        private DevExpress.XtraReports.UI.XRTableCell Total;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell clTipoDocumento;
        private DevExpress.XtraReports.UI.XRTableCell clTipoPago;
        private DevExpress.XtraReports.UI.XRTableCell clTotal;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
    }
}
