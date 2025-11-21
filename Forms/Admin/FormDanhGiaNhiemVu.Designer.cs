namespace BTL_LTTQ_QLPM.Forms.Admin
{
    partial class FormDanhGiaNhiemVu
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
            this.dgvTaskList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTaskName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboMucDanhGia = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHeSoNhanDuoc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rtxtGhiChu = new System.Windows.Forms.RichTextBox();
            this.btnDanhGia = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskList)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Blue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dgvTaskList);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(797, 337);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // dgvTaskList
            // 
            this.dgvTaskList.AllowUserToAddRows = false;
            this.dgvTaskList.AllowUserToDeleteRows = false;
            this.dgvTaskList.AllowUserToOrderColumns = true;
            this.dgvTaskList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaskList.Location = new System.Drawing.Point(0, 28);
            this.dgvTaskList.Name = "dgvTaskList";
            this.dgvTaskList.ReadOnly = true;
            this.dgvTaskList.RowHeadersWidth = 51;
            this.dgvTaskList.RowTemplate.Height = 24;
            this.dgvTaskList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaskList.Size = new System.Drawing.Size(796, 308);
            this.dgvTaskList.TabIndex = 0;
            this.dgvTaskList.SelectionChanged += new System.EventHandler(this.dgvTaskList_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(286, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Danh Sách Nhiệm Vụ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Yellow;
            this.panel2.Controls.Add(this.btnDanhGia);
            this.panel2.Controls.Add(this.rtxtGhiChu);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtHeSoNhanDuoc);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cboMucDanhGia);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblTaskName);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblNhanVien);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(1, 340);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(796, 149);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nhân Viên:";
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhanVien.Location = new System.Drawing.Point(107, 15);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(90, 19);
            this.lblNhanVien.TabIndex = 3;
            this.lblNhanVien.Text = "Nhân Viên:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(265, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nhiệm Vụ:";
            // 
            // lblTaskName
            // 
            this.lblTaskName.AutoSize = true;
            this.lblTaskName.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskName.Location = new System.Drawing.Point(359, 15);
            this.lblTaskName.Name = "lblTaskName";
            this.lblTaskName.Size = new System.Drawing.Size(88, 19);
            this.lblTaskName.TabIndex = 5;
            this.lblTaskName.Text = "Nhiệm Vụ:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "Mức Đánh Giá";
            // 
            // cboMucDanhGia
            // 
            this.cboMucDanhGia.FormattingEnabled = true;
            this.cboMucDanhGia.Location = new System.Drawing.Point(149, 46);
            this.cboMucDanhGia.Name = "cboMucDanhGia";
            this.cboMucDanhGia.Size = new System.Drawing.Size(100, 24);
            this.cboMucDanhGia.TabIndex = 7;
            this.cboMucDanhGia.SelectedIndexChanged += new System.EventHandler(this.cboMucDanhGia_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Điểm Nhận Được:";
            // 
            // txtHeSoNhanDuoc
            // 
            this.txtHeSoNhanDuoc.Location = new System.Drawing.Point(149, 81);
            this.txtHeSoNhanDuoc.Name = "txtHeSoNhanDuoc";
            this.txtHeSoNhanDuoc.Size = new System.Drawing.Size(85, 22);
            this.txtHeSoNhanDuoc.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(265, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 19);
            this.label6.TabIndex = 10;
            this.label6.Text = "Ghi Chú:";
            // 
            // rtxtGhiChu
            // 
            this.rtxtGhiChu.Location = new System.Drawing.Point(349, 51);
            this.rtxtGhiChu.Name = "rtxtGhiChu";
            this.rtxtGhiChu.Size = new System.Drawing.Size(438, 52);
            this.rtxtGhiChu.TabIndex = 11;
            this.rtxtGhiChu.Text = "";
            // 
            // btnDanhGia
            // 
            this.btnDanhGia.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDanhGia.Location = new System.Drawing.Point(302, 109);
            this.btnDanhGia.Name = "btnDanhGia";
            this.btnDanhGia.Size = new System.Drawing.Size(130, 24);
            this.btnDanhGia.TabIndex = 12;
            this.btnDanhGia.Text = "Xác Nhận";
            this.btnDanhGia.UseVisualStyleBackColor = true;
            this.btnDanhGia.Click += new System.EventHandler(this.btnDanhGia_Click);
            // 
            // FormDanhGiaNhiemVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormDanhGiaNhiemVu";
            this.Text = "FormDanhGiaNhiemVu";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskList)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvTaskList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTaskName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHeSoNhanDuoc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboMucDanhGia;
        private System.Windows.Forms.Button btnDanhGia;
        private System.Windows.Forms.RichTextBox rtxtGhiChu;
        private System.Windows.Forms.Label label6;
    }
}