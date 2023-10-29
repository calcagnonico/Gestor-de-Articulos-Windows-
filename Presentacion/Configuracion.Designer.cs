namespace Presentacion
{
    partial class Configuracion
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
            this.txtRutaImagenes = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.txtCarpeta = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtErrorimage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExaminarimg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtRutaImagenes
            // 
            this.txtRutaImagenes.Location = new System.Drawing.Point(132, 45);
            this.txtRutaImagenes.Name = "txtRutaImagenes";
            this.txtRutaImagenes.ReadOnly = true;
            this.txtRutaImagenes.Size = new System.Drawing.Size(213, 20);
            this.txtRutaImagenes.TabIndex = 0;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(16, 143);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(94, 23);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnExaminar
            // 
            this.btnExaminar.Location = new System.Drawing.Point(351, 45);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(75, 20);
            this.btnExaminar.TabIndex = 1;
            this.btnExaminar.Text = "Examinar";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // txtCarpeta
            // 
            this.txtCarpeta.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.txtCarpeta.AutoSize = true;
            this.txtCarpeta.Location = new System.Drawing.Point(16, 48);
            this.txtCarpeta.Name = "txtCarpeta";
            this.txtCarpeta.Size = new System.Drawing.Size(110, 13);
            this.txtCarpeta.TabIndex = 4;
            this.txtCarpeta.Text = "Backup de imagenes:";
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Configuración";
            // 
            // label2
            // 
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Servidor SQL:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(132, 106);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(213, 20);
            this.txtServer.TabIndex = 4;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(116, 143);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(92, 23);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtErrorimage
            // 
            this.txtErrorimage.Location = new System.Drawing.Point(132, 75);
            this.txtErrorimage.Name = "txtErrorimage";
            this.txtErrorimage.Size = new System.Drawing.Size(213, 20);
            this.txtErrorimage.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Imagen por defecto:";
            // 
            // btnExaminarimg
            // 
            this.btnExaminarimg.Location = new System.Drawing.Point(351, 75);
            this.btnExaminarimg.Name = "btnExaminarimg";
            this.btnExaminarimg.Size = new System.Drawing.Size(75, 20);
            this.btnExaminarimg.TabIndex = 3;
            this.btnExaminarimg.Text = "Examinar";
            this.btnExaminarimg.UseVisualStyleBackColor = true;
            this.btnExaminarimg.Click += new System.EventHandler(this.btnExaminarimg_Click);
            // 
            // Configuracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 188);
            this.Controls.Add(this.btnExaminarimg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtErrorimage);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCarpeta);
            this.Controls.Add(this.btnExaminar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtRutaImagenes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Configuracion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracion";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Configuracion_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRutaImagenes;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.Label txtCarpeta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtErrorimage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExaminarimg;
    }
}