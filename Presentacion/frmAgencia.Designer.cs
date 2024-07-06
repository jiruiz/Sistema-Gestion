namespace Presentacion
{
    partial class frmAgencia
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.administracionDataSet = new Presentacion.AdministracionDataSet();
            this.agenciaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.agenciaTableAdapter = new Presentacion.AdministracionDataSetTableAdapters.AgenciaTableAdapter();
            this.gridAgencia = new System.Windows.Forms.DataGridView();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnGuardarCambios = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.administracionDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.agenciaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAgencia)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "NOMBRE AGENCIA";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(336, 60);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(537, 138);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(168, 37);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "REGISTRAR";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // administracionDataSet
            // 
            this.administracionDataSet.DataSetName = "AdministracionDataSet";
            this.administracionDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // agenciaBindingSource
            // 
            this.agenciaBindingSource.DataMember = "Agencia";
            this.agenciaBindingSource.DataSource = this.administracionDataSet;
            // 
            // agenciaTableAdapter
            // 
            this.agenciaTableAdapter.ClearBeforeFill = true;
            // 
            // gridAgencia
            // 
            this.gridAgencia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridAgencia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAgencia.Location = new System.Drawing.Point(34, 138);
            this.gridAgencia.Name = "gridAgencia";
            this.gridAgencia.Size = new System.Drawing.Size(468, 150);
            this.gridAgencia.TabIndex = 4;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(537, 251);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(168, 37);
            this.btnEliminar.TabIndex = 5;
            this.btnEliminar.Text = "ELIMINAR";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnGuardarCambios
            // 
            this.btnGuardarCambios.Location = new System.Drawing.Point(537, 190);
            this.btnGuardarCambios.Name = "btnGuardarCambios";
            this.btnGuardarCambios.Size = new System.Drawing.Size(168, 37);
            this.btnGuardarCambios.TabIndex = 6;
            this.btnGuardarCambios.Text = "GUARDAR CAMBIOS";
            this.btnGuardarCambios.UseVisualStyleBackColor = true;
         
            // 
            // frmAgencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnGuardarCambios);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.gridAgencia);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label1);
            this.Name = "frmAgencia";
            this.Text = "frmAgencia";
            this.Load += new System.EventHandler(this.frmAgencia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.administracionDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.agenciaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAgencia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button btnGuardar;
        private AdministracionDataSet administracionDataSet;
        private System.Windows.Forms.BindingSource agenciaBindingSource;
        private AdministracionDataSetTableAdapters.AgenciaTableAdapter agenciaTableAdapter;
        private System.Windows.Forms.DataGridView gridAgencia;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnGuardarCambios;
    }
}