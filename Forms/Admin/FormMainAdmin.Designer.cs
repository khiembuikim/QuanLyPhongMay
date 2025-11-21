namespace BTL_LTTQ_QLPM.Forms.Admin
{
    partial class FormMainAdmin
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
            this.pnlUserInfo = new System.Windows.Forms.Panel();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.btnQuanLyLichPhong = new System.Windows.Forms.Button();
            this.btnGiaoNhiemVu = new System.Windows.Forms.Button();
            this.btnDanhGiaNhiemVu = new System.Windows.Forms.Button();
            this.btnQuanLyTaiKhoan = new System.Windows.Forms.Button();
            this.btnQuanLyPhongMay = new System.Windows.Forms.Button();
            this.btnQuanLyMayTinh = new System.Windows.Forms.Button();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.pnlUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlUserInfo
            // 
            this.pnlUserInfo.BackColor = System.Drawing.Color.Blue;
            this.pnlUserInfo.Controls.Add(this.btnDangXuat);
            this.pnlUserInfo.Controls.Add(this.lblFullName);
            this.pnlUserInfo.Controls.Add(this.lblRole);
            this.pnlUserInfo.Controls.Add(this.picAvatar);
            this.pnlUserInfo.Location = new System.Drawing.Point(-1, -2);
            this.pnlUserInfo.Name = "pnlUserInfo";
            this.pnlUserInfo.Size = new System.Drawing.Size(275, 456);
            this.pnlUserInfo.TabIndex = 0;
            this.pnlUserInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlUserInfo_Paint);
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.Location = new System.Drawing.Point(95, 308);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(60, 22);
            this.lblFullName.TabIndex = 3;
            this.lblFullName.Text = "label1";
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.Location = new System.Drawing.Point(95, 242);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(60, 22);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "label1";
            this.lblRole.Click += new System.EventHandler(this.label1_Click);
            // 
            // picAvatar
            // 
            this.picAvatar.Location = new System.Drawing.Point(71, 105);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(119, 103);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAvatar.TabIndex = 0;
            this.picAvatar.TabStop = false;
            this.picAvatar.Click += new System.EventHandler(this.pictureBox1_Click);
            this.picAvatar.Paint += new System.Windows.Forms.PaintEventHandler(this.picAvatar_Paint);
            // 
            // btnQuanLyLichPhong
            // 
            this.btnQuanLyLichPhong.BackColor = System.Drawing.Color.Blue;
            this.btnQuanLyLichPhong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanLyLichPhong.ForeColor = System.Drawing.Color.Yellow;
            this.btnQuanLyLichPhong.Location = new System.Drawing.Point(384, 96);
            this.btnQuanLyLichPhong.Name = "btnQuanLyLichPhong";
            this.btnQuanLyLichPhong.Size = new System.Drawing.Size(103, 80);
            this.btnQuanLyLichPhong.TabIndex = 1;
            this.btnQuanLyLichPhong.Text = "Quản Lý Lịch Phòng";
            this.btnQuanLyLichPhong.UseVisualStyleBackColor = false;
            this.btnQuanLyLichPhong.Click += new System.EventHandler(this.btnQuanLyLichPhong_Click);
            // 
            // btnGiaoNhiemVu
            // 
            this.btnGiaoNhiemVu.BackColor = System.Drawing.Color.Blue;
            this.btnGiaoNhiemVu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGiaoNhiemVu.ForeColor = System.Drawing.Color.Yellow;
            this.btnGiaoNhiemVu.Location = new System.Drawing.Point(545, 94);
            this.btnGiaoNhiemVu.Name = "btnGiaoNhiemVu";
            this.btnGiaoNhiemVu.Size = new System.Drawing.Size(103, 80);
            this.btnGiaoNhiemVu.TabIndex = 7;
            this.btnGiaoNhiemVu.Text = "Giao Nhiệm Vụ";
            this.btnGiaoNhiemVu.UseVisualStyleBackColor = false;
            this.btnGiaoNhiemVu.Click += new System.EventHandler(this.btnGiaoNhiemVu_Click);
            // 
            // btnDanhGiaNhiemVu
            // 
            this.btnDanhGiaNhiemVu.BackColor = System.Drawing.Color.Blue;
            this.btnDanhGiaNhiemVu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDanhGiaNhiemVu.ForeColor = System.Drawing.Color.Yellow;
            this.btnDanhGiaNhiemVu.Location = new System.Drawing.Point(703, 94);
            this.btnDanhGiaNhiemVu.Name = "btnDanhGiaNhiemVu";
            this.btnDanhGiaNhiemVu.Size = new System.Drawing.Size(103, 80);
            this.btnDanhGiaNhiemVu.TabIndex = 8;
            this.btnDanhGiaNhiemVu.Text = "Đánh Giá Nhiệm Vụ";
            this.btnDanhGiaNhiemVu.UseVisualStyleBackColor = false;
            this.btnDanhGiaNhiemVu.Click += new System.EventHandler(this.btnDanhGiaNhiemVu_Click);
            // 
            // btnQuanLyTaiKhoan
            // 
            this.btnQuanLyTaiKhoan.BackColor = System.Drawing.Color.Blue;
            this.btnQuanLyTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanLyTaiKhoan.ForeColor = System.Drawing.Color.Yellow;
            this.btnQuanLyTaiKhoan.Location = new System.Drawing.Point(384, 226);
            this.btnQuanLyTaiKhoan.Name = "btnQuanLyTaiKhoan";
            this.btnQuanLyTaiKhoan.Size = new System.Drawing.Size(103, 80);
            this.btnQuanLyTaiKhoan.TabIndex = 9;
            this.btnQuanLyTaiKhoan.Text = "Quản Lý Tài Khoản";
            this.btnQuanLyTaiKhoan.UseVisualStyleBackColor = false;
            this.btnQuanLyTaiKhoan.Click += new System.EventHandler(this.btnQuanLyTaiKhoan_Click);
            // 
            // btnQuanLyPhongMay
            // 
            this.btnQuanLyPhongMay.BackColor = System.Drawing.Color.Blue;
            this.btnQuanLyPhongMay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanLyPhongMay.ForeColor = System.Drawing.Color.Yellow;
            this.btnQuanLyPhongMay.Location = new System.Drawing.Point(545, 226);
            this.btnQuanLyPhongMay.Name = "btnQuanLyPhongMay";
            this.btnQuanLyPhongMay.Size = new System.Drawing.Size(103, 80);
            this.btnQuanLyPhongMay.TabIndex = 10;
            this.btnQuanLyPhongMay.Text = "Quản Lý Phòng Máy";
            this.btnQuanLyPhongMay.UseVisualStyleBackColor = false;
            this.btnQuanLyPhongMay.Click += new System.EventHandler(this.btnQuanLyPhongMay_Click);
            // 
            // btnQuanLyMayTinh
            // 
            this.btnQuanLyMayTinh.BackColor = System.Drawing.Color.Blue;
            this.btnQuanLyMayTinh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanLyMayTinh.ForeColor = System.Drawing.Color.Yellow;
            this.btnQuanLyMayTinh.Location = new System.Drawing.Point(703, 226);
            this.btnQuanLyMayTinh.Name = "btnQuanLyMayTinh";
            this.btnQuanLyMayTinh.Size = new System.Drawing.Size(103, 80);
            this.btnQuanLyMayTinh.TabIndex = 11;
            this.btnQuanLyMayTinh.Text = "Quản Lý Máy Tính";
            this.btnQuanLyMayTinh.UseVisualStyleBackColor = false;
            this.btnQuanLyMayTinh.Click += new System.EventHandler(this.btnQuanLyMayTinh_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangXuat.Location = new System.Drawing.Point(13, 417);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(75, 23);
            this.btnDangXuat.TabIndex = 4;
            this.btnDangXuat.Text = "Đăng Xuất";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // FormMainAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(838, 450);
            this.Controls.Add(this.btnQuanLyMayTinh);
            this.Controls.Add(this.btnQuanLyPhongMay);
            this.Controls.Add(this.btnQuanLyTaiKhoan);
            this.Controls.Add(this.btnDanhGiaNhiemVu);
            this.Controls.Add(this.btnGiaoNhiemVu);
            this.Controls.Add(this.btnQuanLyLichPhong);
            this.Controls.Add(this.pnlUserInfo);
            this.Name = "FormMainAdmin";
            this.Text = "FormMainAdmin";
            this.Load += new System.EventHandler(this.FormMainAdmin_Load);
            this.pnlUserInfo.ResumeLayout(false);
            this.pnlUserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlUserInfo;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Button btnQuanLyLichPhong;
        private System.Windows.Forms.Button btnGiaoNhiemVu;
        private System.Windows.Forms.Button btnDanhGiaNhiemVu;
        private System.Windows.Forms.Button btnQuanLyTaiKhoan;
        private System.Windows.Forms.Button btnQuanLyPhongMay;
        private System.Windows.Forms.Button btnQuanLyMayTinh;
        private System.Windows.Forms.Button btnDangXuat;
    }
}