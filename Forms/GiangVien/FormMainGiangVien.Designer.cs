namespace BTL_LTTQ_QLPM.Forms.GiangVien
{
    partial class FormMainGiangVien
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
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.btnDatPhongMay = new System.Windows.Forms.Button();
            this.btnLichDatCuaToi = new System.Windows.Forms.Button();
            this.btnTinhTrangPhong = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pnlUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlUserInfo
            // 
            this.pnlUserInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlUserInfo.Controls.Add(this.btnDangXuat);
            this.pnlUserInfo.Controls.Add(this.lblFullName);
            this.pnlUserInfo.Controls.Add(this.lblRole);
            this.pnlUserInfo.Controls.Add(this.picAvatar);
            this.pnlUserInfo.Location = new System.Drawing.Point(1, 1);
            this.pnlUserInfo.Name = "pnlUserInfo";
            this.pnlUserInfo.Size = new System.Drawing.Size(286, 456);
            this.pnlUserInfo.TabIndex = 1;
            this.pnlUserInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlUserInfo_Paint);
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
            this.lblRole.Location = new System.Drawing.Point(95, 242);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(60, 22);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "label1";
            // 
            // picAvatar
            // 
            this.picAvatar.Location = new System.Drawing.Point(71, 105);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(119, 103);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAvatar.TabIndex = 0;
            this.picAvatar.TabStop = false;
            // 
            // btnDatPhongMay
            // 
            this.btnDatPhongMay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnDatPhongMay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDatPhongMay.ForeColor = System.Drawing.Color.Black;
            this.btnDatPhongMay.Location = new System.Drawing.Point(336, 185);
            this.btnDatPhongMay.Name = "btnDatPhongMay";
            this.btnDatPhongMay.Size = new System.Drawing.Size(103, 80);
            this.btnDatPhongMay.TabIndex = 2;
            this.btnDatPhongMay.Text = "Đặt Phòng Máy";
            this.btnDatPhongMay.UseVisualStyleBackColor = false;
            this.btnDatPhongMay.Click += new System.EventHandler(this.btnDatPhongMay_Click);
            // 
            // btnLichDatCuaToi
            // 
            this.btnLichDatCuaToi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLichDatCuaToi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLichDatCuaToi.ForeColor = System.Drawing.Color.Black;
            this.btnLichDatCuaToi.Location = new System.Drawing.Point(502, 185);
            this.btnLichDatCuaToi.Name = "btnLichDatCuaToi";
            this.btnLichDatCuaToi.Size = new System.Drawing.Size(103, 80);
            this.btnLichDatCuaToi.TabIndex = 3;
            this.btnLichDatCuaToi.Text = "Lịch Đặt";
            this.btnLichDatCuaToi.UseVisualStyleBackColor = false;
            this.btnLichDatCuaToi.Click += new System.EventHandler(this.btnLichDatCuaToi_Click);
            // 
            // btnTinhTrangPhong
            // 
            this.btnTinhTrangPhong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnTinhTrangPhong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTinhTrangPhong.ForeColor = System.Drawing.Color.Black;
            this.btnTinhTrangPhong.Location = new System.Drawing.Point(659, 185);
            this.btnTinhTrangPhong.Name = "btnTinhTrangPhong";
            this.btnTinhTrangPhong.Size = new System.Drawing.Size(103, 80);
            this.btnTinhTrangPhong.TabIndex = 4;
            this.btnTinhTrangPhong.Text = "Phòng Học";
            this.btnTinhTrangPhong.UseVisualStyleBackColor = false;
            this.btnTinhTrangPhong.Click += new System.EventHandler(this.btnTinhTrangPhong_Click);
            // 
            // FormMainGiangVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnTinhTrangPhong);
            this.Controls.Add(this.btnLichDatCuaToi);
            this.Controls.Add(this.btnDatPhongMay);
            this.Controls.Add(this.pnlUserInfo);
            this.Name = "FormMainGiangVien";
            this.Text = "FormMainGiangVien";
            this.Load += new System.EventHandler(this.FormMainGiangVien_Load);
            this.pnlUserInfo.ResumeLayout(false);
            this.pnlUserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlUserInfo;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Button btnDatPhongMay;
        private System.Windows.Forms.Button btnLichDatCuaToi;
        private System.Windows.Forms.Button btnTinhTrangPhong;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}