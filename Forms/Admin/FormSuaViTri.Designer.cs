namespace BTL_LTTQ_QLPM.Forms.Admin
{
    partial class FormSuaViTri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSuaViTri));
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaMay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtViTriMoi = new System.Windows.Forms.TextBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.cboPhongMayMoi = new System.Windows.Forms.Label();
            this.cboPhongMayMoi1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Mã Máy: ";
            // 
            // txtMaMay
            // 
            this.txtMaMay.Location = new System.Drawing.Point(111, 29);
            this.txtMaMay.Name = "txtMaMay";
            this.txtMaMay.Size = new System.Drawing.Size(171, 22);
            this.txtMaMay.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "Vị Trí Mới:";
            // 
            // txtViTriMoi
            // 
            this.txtViTriMoi.Location = new System.Drawing.Point(111, 127);
            this.txtViTriMoi.Name = "txtViTriMoi";
            this.txtViTriMoi.Size = new System.Drawing.Size(171, 22);
            this.txtViTriMoi.TabIndex = 8;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.Location = new System.Drawing.Point(92, 187);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(110, 38);
            this.btnXacNhan.TabIndex = 13;
            this.btnXacNhan.Text = resources.GetString("btnXacNhan.Text");
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // cboPhongMayMoi
            // 
            this.cboPhongMayMoi.AutoSize = true;
            this.cboPhongMayMoi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPhongMayMoi.Location = new System.Drawing.Point(12, 75);
            this.cboPhongMayMoi.Name = "cboPhongMayMoi";
            this.cboPhongMayMoi.Size = new System.Drawing.Size(69, 23);
            this.cboPhongMayMoi.TabIndex = 14;
            this.cboPhongMayMoi.Text = "Phòng:";
            // 
            // cboPhongMayMoi1
            // 
            this.cboPhongMayMoi1.FormattingEnabled = true;
            this.cboPhongMayMoi1.Location = new System.Drawing.Point(111, 77);
            this.cboPhongMayMoi1.Name = "cboPhongMayMoi1";
            this.cboPhongMayMoi1.Size = new System.Drawing.Size(171, 24);
            this.cboPhongMayMoi1.TabIndex = 15;
            // 
            // FormSuaViTri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(304, 276);
            this.Controls.Add(this.cboPhongMayMoi1);
            this.Controls.Add(this.cboPhongMayMoi);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.txtViTriMoi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMaMay);
            this.Controls.Add(this.label3);
            this.Name = "FormSuaViTri";
            this.Text = "FormSuaViTri";
            this.Load += new System.EventHandler(this.FormSuaViTri_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaMay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtViTriMoi;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Label cboPhongMayMoi;
        private System.Windows.Forms.ComboBox cboPhongMayMoi1;
    }
}