namespace BTL_LTTQ_QLPM.Forms.Admin
{
    partial class FormQuanLyMayTinh
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
            this.dgvDanhSachMay = new System.Windows.Forms.DataGridView();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.cboFilterTrangThai = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboFilterPhong = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnBaoTri = new System.Windows.Forms.Button();
            this.btnKhoiPhuc = new System.Windows.Forms.Button();
            this.btnGhiLoi = new System.Windows.Forms.Button();
            this.btnSuaViTri = new System.Windows.Forms.Button();
            this.btnAddMay = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachMay)).BeginInit();
            this.pnlFilters.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Yellow;
            this.panel1.Controls.Add(this.dgvDanhSachMay);
            this.panel1.Controls.Add(this.pnlFilters);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 403);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // dgvDanhSachMay
            // 
            this.dgvDanhSachMay.AllowUserToAddRows = false;
            this.dgvDanhSachMay.AllowUserToDeleteRows = false;
            this.dgvDanhSachMay.AllowUserToOrderColumns = true;
            this.dgvDanhSachMay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachMay.Location = new System.Drawing.Point(5, 64);
            this.dgvDanhSachMay.Name = "dgvDanhSachMay";
            this.dgvDanhSachMay.ReadOnly = true;
            this.dgvDanhSachMay.RowHeadersWidth = 51;
            this.dgvDanhSachMay.RowTemplate.Height = 24;
            this.dgvDanhSachMay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachMay.Size = new System.Drawing.Size(794, 338);
            this.dgvDanhSachMay.TabIndex = 4;
            // 
            // pnlFilters
            // 
            this.pnlFilters.BackColor = System.Drawing.Color.FloralWhite;
            this.pnlFilters.Controls.Add(this.cboFilterTrangThai);
            this.pnlFilters.Controls.Add(this.label3);
            this.pnlFilters.Controls.Add(this.label2);
            this.pnlFilters.Controls.Add(this.cboFilterPhong);
            this.pnlFilters.Location = new System.Drawing.Point(3, 32);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Size = new System.Drawing.Size(796, 30);
            this.pnlFilters.TabIndex = 3;
            // 
            // cboFilterTrangThai
            // 
            this.cboFilterTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterTrangThai.FormattingEnabled = true;
            this.cboFilterTrangThai.Location = new System.Drawing.Point(342, 1);
            this.cboFilterTrangThai.Name = "cboFilterTrangThai";
            this.cboFilterTrangThai.Size = new System.Drawing.Size(121, 24);
            this.cboFilterTrangThai.TabIndex = 5;
            this.cboFilterTrangThai.SelectedIndexChanged += new System.EventHandler(this.cboFilterTrangThai_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(233, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Trạng Thái";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Phòng";
            // 
            // cboFilterPhong
            // 
            this.cboFilterPhong.DisplayMember = "TEN_PHONG";
            this.cboFilterPhong.FormattingEnabled = true;
            this.cboFilterPhong.Location = new System.Drawing.Point(72, 3);
            this.cboFilterPhong.Name = "cboFilterPhong";
            this.cboFilterPhong.Size = new System.Drawing.Size(121, 24);
            this.cboFilterPhong.TabIndex = 0;
            this.cboFilterPhong.ValueMember = "PHONG_ID";
            this.cboFilterPhong.SelectedIndexChanged += new System.EventHandler(this.cboFilterPhong_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(296, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Danh Sách Máy Tính";
            // 
            // pnlControls
            // 
            this.pnlControls.BackColor = System.Drawing.Color.Blue;
            this.pnlControls.Controls.Add(this.btnBaoTri);
            this.pnlControls.Controls.Add(this.btnKhoiPhuc);
            this.pnlControls.Controls.Add(this.btnGhiLoi);
            this.pnlControls.Controls.Add(this.btnSuaViTri);
            this.pnlControls.Controls.Add(this.btnAddMay);
            this.pnlControls.Location = new System.Drawing.Point(0, 397);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(798, 56);
            this.pnlControls.TabIndex = 1;
            // 
            // btnBaoTri
            // 
            this.btnBaoTri.BackColor = System.Drawing.Color.FloralWhite;
            this.btnBaoTri.Location = new System.Drawing.Point(501, 7);
            this.btnBaoTri.Name = "btnBaoTri";
            this.btnBaoTri.Size = new System.Drawing.Size(113, 34);
            this.btnBaoTri.TabIndex = 9;
            this.btnBaoTri.Text = "Bảo Trì";
            this.btnBaoTri.UseVisualStyleBackColor = false;
            this.btnBaoTri.Click += new System.EventHandler(this.btnBaoTri_Click);
            // 
            // btnKhoiPhuc
            // 
            this.btnKhoiPhuc.BackColor = System.Drawing.Color.FloralWhite;
            this.btnKhoiPhuc.Location = new System.Drawing.Point(343, 11);
            this.btnKhoiPhuc.Name = "btnKhoiPhuc";
            this.btnKhoiPhuc.Size = new System.Drawing.Size(113, 34);
            this.btnKhoiPhuc.TabIndex = 8;
            this.btnKhoiPhuc.Text = "Khôi Phục";
            this.btnKhoiPhuc.UseVisualStyleBackColor = false;
            this.btnKhoiPhuc.Click += new System.EventHandler(this.btnKhoiPhuc_Click);
            // 
            // btnGhiLoi
            // 
            this.btnGhiLoi.BackColor = System.Drawing.Color.FloralWhite;
            this.btnGhiLoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGhiLoi.Location = new System.Drawing.Point(676, 7);
            this.btnGhiLoi.Name = "btnGhiLoi";
            this.btnGhiLoi.Size = new System.Drawing.Size(96, 34);
            this.btnGhiLoi.TabIndex = 7;
            this.btnGhiLoi.Text = "Ghi Lỗi ";
            this.btnGhiLoi.UseVisualStyleBackColor = false;
            this.btnGhiLoi.Click += new System.EventHandler(this.btnGhiLoi_Click);
            // 
            // btnSuaViTri
            // 
            this.btnSuaViTri.BackColor = System.Drawing.Color.FloralWhite;
            this.btnSuaViTri.Location = new System.Drawing.Point(180, 11);
            this.btnSuaViTri.Name = "btnSuaViTri";
            this.btnSuaViTri.Size = new System.Drawing.Size(113, 34);
            this.btnSuaViTri.TabIndex = 4;
            this.btnSuaViTri.Text = "Sửa Vị Trí";
            this.btnSuaViTri.UseVisualStyleBackColor = false;
            this.btnSuaViTri.Click += new System.EventHandler(this.btnSuaViTri_Click);
            // 
            // btnAddMay
            // 
            this.btnAddMay.BackColor = System.Drawing.Color.FloralWhite;
            this.btnAddMay.Location = new System.Drawing.Point(16, 12);
            this.btnAddMay.Name = "btnAddMay";
            this.btnAddMay.Size = new System.Drawing.Size(113, 34);
            this.btnAddMay.TabIndex = 3;
            this.btnAddMay.Text = "Thêm máy";
            this.btnAddMay.UseVisualStyleBackColor = false;
            this.btnAddMay.Click += new System.EventHandler(this.btnAddMay_Click);
            // 
            // FormQuanLyMayTinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.panel1);
            this.Name = "FormQuanLyMayTinh";
            this.Text = "FormQuanLyMayTinh";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachMay)).EndInit();
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.ComboBox cboFilterTrangThai;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboFilterPhong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDanhSachMay;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Button btnAddMay;
        private System.Windows.Forms.Button btnSuaViTri;
        private System.Windows.Forms.Button btnGhiLoi;
        private System.Windows.Forms.Button btnBaoTri;
        private System.Windows.Forms.Button btnKhoiPhuc;
    }
}