namespace Stock_Data_Grabber {
    partial class Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Refresh = new System.Windows.Forms.Button();
            this.numStatments = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.export = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(56, 26);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 24;
            this.dgv.Size = new System.Drawing.Size(1258, 546);
            this.dgv.TabIndex = 0;
            // 
            // Refresh
            // 
            this.Refresh.Location = new System.Drawing.Point(624, 608);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(130, 45);
            this.Refresh.TabIndex = 1;
            this.Refresh.Text = "Refresh";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // numStatments
            // 
            this.numStatments.Location = new System.Drawing.Point(477, 619);
            this.numStatments.Name = "numStatments";
            this.numStatments.Size = new System.Drawing.Size(100, 22);
            this.numStatments.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(400, 621);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Quantity:";
            // 
            // export
            // 
            this.export.Location = new System.Drawing.Point(817, 608);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(130, 45);
            this.export.TabIndex = 6;
            this.export.Text = "Export to Excel";
            this.export.UseVisualStyleBackColor = true;
            this.export.Click += new System.EventHandler(this.Export_Click);
            // 
            // Main
            // 
            this.AcceptButton = this.Refresh;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1376, 687);
            this.Controls.Add(this.export);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numStatments);
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.dgv);
            this.Name = "Main";
            this.Text = "Stock Data Grabber - Christian Loth";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button Refresh;
        private System.Windows.Forms.TextBox numStatments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button export;
    }
}

