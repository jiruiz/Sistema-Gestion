﻿namespace Presentacion
{
    partial class frmInicio
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnIrAgencias = new System.Windows.Forms.Button();
            this.btnIrDepositos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(255, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "PANTALLA DE INICIO";
            // 
            // btnIrAgencias
            // 
            this.btnIrAgencias.Location = new System.Drawing.Point(124, 98);
            this.btnIrAgencias.Name = "btnIrAgencias";
            this.btnIrAgencias.Size = new System.Drawing.Size(169, 41);
            this.btnIrAgencias.TabIndex = 1;
            this.btnIrAgencias.Text = "GESTINAR AGENCIAS";
            this.btnIrAgencias.UseVisualStyleBackColor = true;
            this.btnIrAgencias.Click += new System.EventHandler(this.btnIrAgencias_Click);
            // 
            // btnIrDepositos
            // 
            this.btnIrDepositos.Location = new System.Drawing.Point(453, 98);
            this.btnIrDepositos.Name = "btnIrDepositos";
            this.btnIrDepositos.Size = new System.Drawing.Size(194, 41);
            this.btnIrDepositos.TabIndex = 2;
            this.btnIrDepositos.Text = "GESTIONAR DEPOSITOS ";
            this.btnIrDepositos.UseVisualStyleBackColor = true;
            this.btnIrDepositos.Click += new System.EventHandler(this.btnIrDepositos_Click);
            // 
            // frmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnIrDepositos);
            this.Controls.Add(this.btnIrAgencias);
            this.Controls.Add(this.label1);
            this.Name = "frmInicio";
            this.Text = "frmInicio";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnIrAgencias;
        private System.Windows.Forms.Button btnIrDepositos;
    }
}