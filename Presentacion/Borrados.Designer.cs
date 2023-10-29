namespace Presentacion
{
    partial class Borrados
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
            this.dgvArticulos = new System.Windows.Forms.DataGridView();
            this.btnrestablecer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEliminarF = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvArticulos
            // 
            this.dgvArticulos.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.dgvArticulos.AllowUserToAddRows = false;
            this.dgvArticulos.AllowUserToDeleteRows = false;
            this.dgvArticulos.AllowUserToResizeRows = false;
            this.dgvArticulos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvArticulos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvArticulos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvArticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvArticulos.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvArticulos.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dgvArticulos.Location = new System.Drawing.Point(12, 44);
            this.dgvArticulos.MultiSelect = false;
            this.dgvArticulos.Name = "dgvArticulos";
            this.dgvArticulos.ReadOnly = true;
            this.dgvArticulos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvArticulos.RowTemplate.ReadOnly = true;
            this.dgvArticulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvArticulos.ShowEditingIcon = false;
            this.dgvArticulos.Size = new System.Drawing.Size(655, 210);
            this.dgvArticulos.StandardTab = true;
            this.dgvArticulos.TabIndex = 0;
            // 
            // btnrestablecer
            // 
            this.btnrestablecer.Location = new System.Drawing.Point(12, 269);
            this.btnrestablecer.Name = "btnrestablecer";
            this.btnrestablecer.Size = new System.Drawing.Size(106, 29);
            this.btnrestablecer.TabIndex = 1;
            this.btnrestablecer.Text = "Restablecer";
            this.btnrestablecer.UseVisualStyleBackColor = true;
            this.btnrestablecer.Click += new System.EventHandler(this.btnrestablecer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "Restablecer Articulos Borrados";
            // 
            // btnEliminarF
            // 
            this.btnEliminarF.Location = new System.Drawing.Point(124, 269);
            this.btnEliminarF.Name = "btnEliminarF";
            this.btnEliminarF.Size = new System.Drawing.Size(118, 28);
            this.btnEliminarF.TabIndex = 2;
            this.btnEliminarF.Text = "Eliminar Fisicamente";
            this.btnEliminarF.UseVisualStyleBackColor = true;
            this.btnEliminarF.Click += new System.EventHandler(this.btnEliminarF_Click_1);
            // 
            // Borrados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 310);
            this.Controls.Add(this.btnEliminarF);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnrestablecer);
            this.Controls.Add(this.dgvArticulos);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Borrados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Restablecer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Borrados_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvArticulos;
        private System.Windows.Forms.Button btnrestablecer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEliminarF;
    }
}