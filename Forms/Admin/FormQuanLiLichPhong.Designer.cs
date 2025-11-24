namespace BTL_LTTQ_QLPM.Forms.Admin
{
    partial class FormQuanLiLichPhong
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
            this.TopPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLoc = new System.Windows.Forms.Button();
            this.cboTrangThaiLoc = new System.Windows.Forms.ComboBox();
            this.dtpNgayLoc = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvLichDat = new System.Windows.Forms.DataGridView();
            this.btnDuyet = new System.Windows.Forms.Button();
            this.btnTuChoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.TopPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichDat)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.panel2);
            this.TopPanel.Controls.Add(this.panel1);
            this.TopPanel.Controls.Add(this.btnLoc);
            this.TopPanel.Controls.Add(this.cboTrangThaiLoc);
            this.TopPanel.Controls.Add(this.dtpNgayLoc);
            this.TopPanel.Location = new System.Drawing.Point(-3, -1);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(846, 46);
            this.TopPanel.TabIndex = 0;
            this.TopPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TopPanel_Paint);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(7, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(794, 381);
            this.panel2.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(8, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 382);
            this.panel1.TabIndex = 3;
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(706, 20);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(75, 23);
            this.btnLoc.TabIndex = 2;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // cboTrangThaiLoc
            // 
            this.cboTrangThaiLoc.FormattingEnabled = true;
            this.cboTrangThaiLoc.Location = new System.Drawing.Point(394, 20);
            this.cboTrangThaiLoc.Name = "cboTrangThaiLoc";
            this.cboTrangThaiLoc.Size = new System.Drawing.Size(121, 24);
            this.cboTrangThaiLoc.TabIndex = 1;
            // 
            // dtpNgayLoc
            // 
            this.dtpNgayLoc.Location = new System.Drawing.Point(33, 24);
            this.dtpNgayLoc.Name = "dtpNgayLoc";
            this.dtpNgayLoc.Size = new System.Drawing.Size(200, 22);
            this.dtpNgayLoc.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvLichDat);
            this.panel3.Location = new System.Drawing.Point(7, 46);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(835, 419);
            this.panel3.TabIndex = 1;
            // 
            // dgvLichDat
            // 
            this.dgvLichDat.AllowUserToAddRows = false;
            this.dgvLichDat.AllowUserToDeleteRows = false;
            this.dgvLichDat.AllowUserToOrderColumns = true;
            this.dgvLichDat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichDat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichDat.Location = new System.Drawing.Point(0, 0);
            this.dgvLichDat.Name = "dgvLichDat";
            this.dgvLichDat.ReadOnly = true;
            this.dgvLichDat.RowHeadersWidth = 51;
            this.dgvLichDat.RowTemplate.Height = 24;
            this.dgvLichDat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichDat.Size = new System.Drawing.Size(835, 419);
            this.dgvLichDat.TabIndex = 0;
            this.dgvLichDat.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnDuyet
            // 
            this.btnDuyet.Location = new System.Drawing.Point(46, 472);
            this.btnDuyet.Name = "btnDuyet";
            this.btnDuyet.Size = new System.Drawing.Size(75, 23);
            this.btnDuyet.TabIndex = 2;
            this.btnDuyet.Text = "Duyệt";
            this.btnDuyet.UseVisualStyleBackColor = true;
            this.btnDuyet.Click += new System.EventHandler(this.btnDuyet_Click);
            // 
            // btnTuChoi
            // 
            this.btnTuChoi.Location = new System.Drawing.Point(391, 472);
            this.btnTuChoi.Name = "btnTuChoi";
            this.btnTuChoi.Size = new System.Drawing.Size(75, 23);
            this.btnTuChoi.TabIndex = 4;
            this.btnTuChoi.Text = "Từ Chối";
            this.btnTuChoi.UseVisualStyleBackColor = true;
            this.btnTuChoi.Click += new System.EventHandler(this.btnTuChoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(703, 471);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 5;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // FormQuanLiLichPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 499);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnTuChoi);
            this.Controls.Add(this.btnDuyet);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.TopPanel);
            this.Name = "FormQuanLiLichPhong";
            this.Text = "FormQuanLiLichPhong";
            this.Load += new System.EventHandler(this.FormQuanLiLichPhong_Load);
            this.TopPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichDat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.ComboBox cboTrangThaiLoc;
        private System.Windows.Forms.DateTimePicker dtpNgayLoc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvLichDat;
        private System.Windows.Forms.Button btnDuyet;
        private System.Windows.Forms.Button btnTuChoi;
        private System.Windows.Forms.Button btnXoa;
    }
}