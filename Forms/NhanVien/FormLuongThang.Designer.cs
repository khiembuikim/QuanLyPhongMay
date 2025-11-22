namespace BTL_LTTQ_QLPM.Forms.NhanVien
{
    partial class FormLuongThang
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
            this.label4 = new System.Windows.Forms.Label();
            this.dgvLichSuLuong = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(238, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(296, 32);
            this.label4.TabIndex = 8;
            this.label4.Text = "Lịch sử Lương Chi Tiết";
            // 
            // dgvLichSuLuong
            // 
            this.dgvLichSuLuong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSuLuong.Location = new System.Drawing.Point(0, 44);
            this.dgvLichSuLuong.Name = "dgvLichSuLuong";
            this.dgvLichSuLuong.RowHeadersWidth = 51;
            this.dgvLichSuLuong.RowTemplate.Height = 24;
            this.dgvLichSuLuong.Size = new System.Drawing.Size(804, 407);
            this.dgvLichSuLuong.TabIndex = 9;
            // 
            // FormLuongThang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvLichSuLuong);
            this.Controls.Add(this.label4);
            this.Name = "FormLuongThang";
            this.Text = "FormLuongThang";
            this.Load += new System.EventHandler(this.FormLuongThang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuLuong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvLichSuLuong;
    }
}