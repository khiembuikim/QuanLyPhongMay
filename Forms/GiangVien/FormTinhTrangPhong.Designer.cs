namespace BTL_LTTQ_QLPM.Forms.GiangVien
{
    partial class FormTinhTrangPhong
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvTinhTrangPhong = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTinhTrangPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(243, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(329, 37);
            this.label4.TabIndex = 7;
            this.label4.Text = "Danh Sách Các Phòng";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(678, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refrest";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvTinhTrangPhong
            // 
            this.dgvTinhTrangPhong.AllowUserToAddRows = false;
            this.dgvTinhTrangPhong.AllowUserToDeleteRows = false;
            this.dgvTinhTrangPhong.AllowUserToOrderColumns = true;
            this.dgvTinhTrangPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTinhTrangPhong.Location = new System.Drawing.Point(2, 49);
            this.dgvTinhTrangPhong.Name = "dgvTinhTrangPhong";
            this.dgvTinhTrangPhong.ReadOnly = true;
            this.dgvTinhTrangPhong.RowHeadersWidth = 51;
            this.dgvTinhTrangPhong.RowTemplate.Height = 24;
            this.dgvTinhTrangPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTinhTrangPhong.Size = new System.Drawing.Size(800, 403);
            this.dgvTinhTrangPhong.TabIndex = 9;
            this.dgvTinhTrangPhong.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTinhTrangPhong_CellFormatting);
            // 
            // FormTinhTrangPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvTinhTrangPhong);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label4);
            this.Name = "FormTinhTrangPhong";
            this.Text = "FormTinhTrangPhong";
            this.Load += new System.EventHandler(this.FormTinhTrangPhong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTinhTrangPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvTinhTrangPhong;
    }
}