namespace Backline.VentaSimple
{
    partial class frmCambioClave
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
            this.components = new System.ComponentModel.Container();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtClave2 = new DevExpress.XtraEditors.TextEdit();
            this.btnCambiar = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtClave1 = new DevExpress.XtraEditors.TextEdit();
            this.dxErrorProvider2 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClave2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClave1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.simpleButton3);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.txtClave2);
            this.groupControl2.Controls.Add(this.btnCambiar);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.txtClave1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(286, 160);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Cambiar contraseña";
            // 
            // simpleButton3
            // 
            this.simpleButton3.ImageOptions.SvgImage = global::Backline.VentaSimple.Properties.Resources.delete;
            this.simpleButton3.Location = new System.Drawing.Point(180, 99);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(91, 36);
            this.simpleButton3.TabIndex = 5;
            this.simpleButton3.Text = "Cancelar";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 62);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(103, 14);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Repetir contraseña";
            // 
            // txtClave2
            // 
            this.txtClave2.Location = new System.Drawing.Point(132, 59);
            this.txtClave2.Name = "txtClave2";
            this.txtClave2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtClave2.Properties.Appearance.Options.UseFont = true;
            this.txtClave2.Size = new System.Drawing.Size(139, 20);
            this.txtClave2.TabIndex = 3;
            // 
            // btnCambiar
            // 
            this.btnCambiar.ImageOptions.SvgImage = global::Backline.VentaSimple.Properties.Resources.bo_validation2;
            this.btnCambiar.Location = new System.Drawing.Point(12, 99);
            this.btnCambiar.Name = "btnCambiar";
            this.btnCambiar.Size = new System.Drawing.Size(123, 36);
            this.btnCambiar.TabIndex = 2;
            this.btnCambiar.Text = "Cambiar";
            this.btnCambiar.Click += new System.EventHandler(this.btnCambiar_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Contraseña";
            // 
            // txtClave1
            // 
            this.txtClave1.Location = new System.Drawing.Point(132, 33);
            this.txtClave1.Name = "txtClave1";
            this.txtClave1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtClave1.Properties.Appearance.Options.UseFont = true;
            this.txtClave1.Size = new System.Drawing.Size(139, 20);
            this.txtClave1.TabIndex = 0;
            // 
            // dxErrorProvider2
            // 
            this.dxErrorProvider2.ContainerControl = this;
            // 
            // frmCambioClave
            // 
            this.ClientSize = new System.Drawing.Size(286, 160);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCambioClave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contraseña";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClave2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClave1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl lbPassword;
        private DevExpress.XtraEditors.TextEdit txtUsuario;
        private DevExpress.XtraEditors.LabelControl lbUsuario;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btnIngresar;       
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnCambiar;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtClave1;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtClave2;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider2;
    }
}