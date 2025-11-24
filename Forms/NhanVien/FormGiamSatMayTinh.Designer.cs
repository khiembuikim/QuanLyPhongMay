namespace BTL_LTTQ_QLPM.Forms.NhanVien
{
    partial class FormGiamSatMayTinh
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboLocTrangThai = new System.Windows.Forms.ComboBox();
            this.cboLocPhong = new System.Windows.Forms.ComboBox();
            this.dgvMayTinh = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnKhoiPhuc = new System.Windows.Forms.Button();
            this.btnBaoTri = new System.Windows.Forms.Button();
            this.btnLoiKhac = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMayTinh)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.panel1.Controls.Add(this.cboLocTrangThai);
            this.panel1.Controls.Add(this.cboLocPhong);
            this.panel1.Controls.Add(this.dgvMayTinh);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(4, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(797, 382);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cboLocTrangThai
            // 
            this.cboLocTrangThai.FormattingEnabled = true;
            this.cboLocTrangThai.Location = new System.Drawing.Point(348, 40);
            this.cboLocTrangThai.Name = "cboLocTrangThai";
            this.cboLocTrangThai.Size = new System.Drawing.Size(121, 24);
            this.cboLocTrangThai.TabIndex = 12;
            // 
            // cboLocPhong
            // 
            this.cboLocPhong.FormattingEnabled = true;
            this.cboLocPhong.Location = new System.Drawing.Point(76, 40);
            this.cboLocPhong.Name = "cboLocPhong";
            this.cboLocPhong.Size = new System.Drawing.Size(121, 24);
            this.cboLocPhong.TabIndex = 11;
            this.cboLocPhong.SelectedIndexChanged += new System.EventHandler(this.cboLocPhong_SelectedIndexChanged);
            // 
            // dgvMayTinh
            // 
            this.dgvMayTinh.AllowUserToAddRows = false;
            this.dgvMayTinh.AllowUserToDeleteRows = false;
            this.dgvMayTinh.AllowUserToOrderColumns = true;
            this.dgvMayTinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMayTinh.Location = new System.Drawing.Point(2, 65);
            this.dgvMayTinh.Name = "dgvMayTinh";
            this.dgvMayTinh.ReadOnly = true;
            this.dgvMayTinh.RowHeadersWidth = 51;
            this.dgvMayTinh.RowTemplate.Height = 24;
            this.dgvMayTinh.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMayTinh.Size = new System.Drawing.Size(794, 316);
            this.dgvMayTinh.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(242, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 23);
            this.label1.TabIndex = 9;
            this.label1.Text = "Trạng thái: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "Phòng: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(240, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(270, 32);
            this.label4.TabIndex = 7;
            this.label4.Text = "Danh Sách Máy Tính";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btnKhoiPhuc
            // 
            this.btnKhoiPhuc.BackColor = System.Drawing.Color.Blue;
            this.btnKhoiPhuc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhoiPhuc.Location = new System.Drawing.Point(51, 406);
            this.btnKhoiPhuc.Name = "btnKhoiPhuc";
            this.btnKhoiPhuc.Size = new System.Drawing.Size(169, 32);
            this.btnKhoiPhuc.TabIndex = 12;
            this.btnKhoiPhuc.Text = "Khôi Phục";
            this.btnKhoiPhuc.UseVisualStyleBackColor = false;
            this.btnKhoiPhuc.Click += new System.EventHandler(this.btnKhoiPhuc_Click);
            // 
            // btnBaoTri
            // 
            this.btnBaoTri.BackColor = System.Drawing.Color.Blue;
            this.btnBaoTri.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaoTri.Location = new System.Drawing.Point(322, 406);
            this.btnBaoTri.Name = "btnBaoTri";
            this.btnBaoTri.Size = new System.Drawing.Size(169, 32);
            this.btnBaoTri.TabIndex = 13;
            this.btnBaoTri.Text = "Bảo Trì";
            this.btnBaoTri.UseVisualStyleBackColor = false;
            this.btnBaoTri.Click += new System.EventHandler(this.btnBaoTri_Click);
            // 
            // btnLoiKhac
            // 
            this.btnLoiKhac.BackColor = System.Drawing.Color.Blue;
            this.btnLoiKhac.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoiKhac.Location = new System.Drawing.Point(580, 406);
            this.btnLoiKhac.Name = "btnLoiKhac";
            this.btnLoiKhac.Size = new System.Drawing.Size(169, 32);
            this.btnLoiKhac.TabIndex = 14;
            this.btnLoiKhac.Text = "Lỗi";
            this.btnLoiKhac.UseVisualStyleBackColor = false;
            this.btnLoiKhac.Click += new System.EventHandler(this.btnLoiKhac_Click);
            // 
            // FormGiamSatMayTinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLoiKhac);
            this.Controls.Add(this.btnBaoTri);
            this.Controls.Add(this.btnKhoiPhuc);
            this.Controls.Add(this.panel1);
            this.Name = "FormGiamSatMayTinh";
            this.Text = "FormGiamSatMayTinh";
            this.Load += new System.EventHandler(this.FormGiamSatMayTinh_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMayTinh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvMayTinh;
        private System.Windows.Forms.ComboBox cboLocTrangThai;
        private System.Windows.Forms.ComboBox cboLocPhong;
        private System.Windows.Forms.Button btnKhoiPhuc;
        private System.Windows.Forms.Button btnBaoTri;
        private System.Windows.Forms.Button btnLoiKhac;
    }
}