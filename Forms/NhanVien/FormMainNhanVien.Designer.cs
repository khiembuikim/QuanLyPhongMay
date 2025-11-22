namespace BTL_LTTQ_QLPM.Forms.NhanVien
{
    partial class FormMainNhanVien
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
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.pnlUserInfo = new System.Windows.Forms.Panel();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.btnNhiemVuCuaToi = new System.Windows.Forms.Button();
            this.btnLichLamViecNV = new System.Windows.Forms.Button();
            this.btnMayTinh = new System.Windows.Forms.Button();
            this.btnLuongThang = new System.Windows.Forms.Button();
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
            this.pnlUserInfo.Location = new System.Drawing.Point(2, 3);
            this.pnlUserInfo.Name = "pnlUserInfo";
            this.pnlUserInfo.Size = new System.Drawing.Size(275, 456);
            this.pnlUserInfo.TabIndex = 1;
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
            this.lblRole.Location = new System.Drawing.Point(104, 233);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(60, 22);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "label1";
            this.lblRole.Click += new System.EventHandler(this.lblRole_Click);
            // 
            // picAvatar
            // 
            this.picAvatar.Location = new System.Drawing.Point(71, 105);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(119, 103);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAvatar.TabIndex = 0;
            this.picAvatar.TabStop = false;
            this.picAvatar.Paint += new System.Windows.Forms.PaintEventHandler(this.picAvatar_Paint);
            // 
            // btnNhiemVuCuaToi
            // 
            this.btnNhiemVuCuaToi.BackColor = System.Drawing.Color.Blue;
            this.btnNhiemVuCuaToi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhiemVuCuaToi.ForeColor = System.Drawing.Color.Yellow;
            this.btnNhiemVuCuaToi.Location = new System.Drawing.Point(353, 108);
            this.btnNhiemVuCuaToi.Name = "btnNhiemVuCuaToi";
            this.btnNhiemVuCuaToi.Size = new System.Drawing.Size(103, 80);
            this.btnNhiemVuCuaToi.TabIndex = 2;
            this.btnNhiemVuCuaToi.Text = "Nhiệm vụ của tôi";
            this.btnNhiemVuCuaToi.UseVisualStyleBackColor = false;
            this.btnNhiemVuCuaToi.Click += new System.EventHandler(this.btnNhiemVuCuaToi_Click_1);
            // 
            // btnLichLamViecNV
            // 
            this.btnLichLamViecNV.BackColor = System.Drawing.Color.Blue;
            this.btnLichLamViecNV.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLichLamViecNV.ForeColor = System.Drawing.Color.Yellow;
            this.btnLichLamViecNV.Location = new System.Drawing.Point(504, 108);
            this.btnLichLamViecNV.Name = "btnLichLamViecNV";
            this.btnLichLamViecNV.Size = new System.Drawing.Size(103, 80);
            this.btnLichLamViecNV.TabIndex = 4;
            this.btnLichLamViecNV.Text = "Lịch làm việc";
            this.btnLichLamViecNV.UseVisualStyleBackColor = false;
            this.btnLichLamViecNV.Click += new System.EventHandler(this.btnLichLamViecNV_Click_1);
            // 
            // btnMayTinh
            // 
            this.btnMayTinh.BackColor = System.Drawing.Color.Blue;
            this.btnMayTinh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMayTinh.ForeColor = System.Drawing.Color.Yellow;
            this.btnMayTinh.Location = new System.Drawing.Point(353, 245);
            this.btnMayTinh.Name = "btnMayTinh";
            this.btnMayTinh.Size = new System.Drawing.Size(103, 80);
            this.btnMayTinh.TabIndex = 5;
            this.btnMayTinh.Text = "Máy Tính";
            this.btnMayTinh.UseVisualStyleBackColor = false;
            this.btnMayTinh.Click += new System.EventHandler(this.btnMayTinh_Click);
            // 
            // btnLuongThang
            // 
            this.btnLuongThang.BackColor = System.Drawing.Color.Blue;
            this.btnLuongThang.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuongThang.ForeColor = System.Drawing.Color.Yellow;
            this.btnLuongThang.Location = new System.Drawing.Point(504, 245);
            this.btnLuongThang.Name = "btnLuongThang";
            this.btnLuongThang.Size = new System.Drawing.Size(103, 80);
            this.btnLuongThang.TabIndex = 7;
            this.btnLuongThang.Text = "Lương tháng";
            this.btnLuongThang.UseVisualStyleBackColor = false;
            this.btnLuongThang.Click += new System.EventHandler(this.btnLuongThang_Click_1);
            // 
            // FormMainNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLuongThang);
            this.Controls.Add(this.btnMayTinh);
            this.Controls.Add(this.btnLichLamViecNV);
            this.Controls.Add(this.btnNhiemVuCuaToi);
            this.Controls.Add(this.pnlUserInfo);
            this.Name = "FormMainNhanVien";
            this.Text = "FormMainNhanVien";
            this.Load += new System.EventHandler(this.FormMainNhanVien_Load);
            this.pnlUserInfo.ResumeLayout(false);
            this.pnlUserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.Panel pnlUserInfo;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Button btnNhiemVuCuaToi;
        private System.Windows.Forms.Button btnLichLamViecNV;
        private System.Windows.Forms.Button btnMayTinh;
        private System.Windows.Forms.Button btnLuongThang;
    }
}