namespace BTL_LTTQ_QLPM.Forms.NhanVien
{
    partial class FormNhiemVuCuaToi
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvNhiemVu = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNhiemVuDangChon = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTienDo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGhiChuBaoCao = new System.Windows.Forms.TextBox();
            this.btnCapNhatTienDo = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhiemVu)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Blue;
            this.panel1.Controls.Add(this.dgvNhiemVu);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 282);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(281, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Danh Sách Nhiệm Vụ";
            // 
            // dgvNhiemVu
            // 
            this.dgvNhiemVu.AllowUserToAddRows = false;
            this.dgvNhiemVu.AllowUserToDeleteRows = false;
            this.dgvNhiemVu.AllowUserToOrderColumns = true;
            this.dgvNhiemVu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvNhiemVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhiemVu.Location = new System.Drawing.Point(1, 30);
            this.dgvNhiemVu.Name = "dgvNhiemVu";
            this.dgvNhiemVu.ReadOnly = true;
            this.dgvNhiemVu.RowHeadersWidth = 51;
            this.dgvNhiemVu.RowTemplate.Height = 24;
            this.dgvNhiemVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhiemVu.Size = new System.Drawing.Size(798, 251);
            this.dgvNhiemVu.TabIndex = 4;
            this.dgvNhiemVu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhiemVu_CellClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Lime;
            this.panel2.Controls.Add(this.btnCapNhatTienDo);
            this.panel2.Controls.Add(this.txtGhiChuBaoCao);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtTienDo);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblNhiemVuDangChon);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(4, 289);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(794, 194);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nhiệm Vụ: ";
            // 
            // lblNhiemVuDangChon
            // 
            this.lblNhiemVuDangChon.AutoSize = true;
            this.lblNhiemVuDangChon.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhiemVuDangChon.Location = new System.Drawing.Point(130, 9);
            this.lblNhiemVuDangChon.Name = "lblNhiemVuDangChon";
            this.lblNhiemVuDangChon.Size = new System.Drawing.Size(0, 23);
            this.lblNhiemVuDangChon.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tiến độ: ";
            // 
            // txtTienDo
            // 
            this.txtTienDo.Location = new System.Drawing.Point(94, 48);
            this.txtTienDo.Name = "txtTienDo";
            this.txtTienDo.Size = new System.Drawing.Size(100, 22);
            this.txtTienDo.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Ghi Chú";
            // 
            // txtGhiChuBaoCao
            // 
            this.txtGhiChuBaoCao.Location = new System.Drawing.Point(90, 84);
            this.txtGhiChuBaoCao.Multiline = true;
            this.txtGhiChuBaoCao.Name = "txtGhiChuBaoCao";
            this.txtGhiChuBaoCao.Size = new System.Drawing.Size(694, 76);
            this.txtGhiChuBaoCao.TabIndex = 9;
            // 
            // btnCapNhatTienDo
            // 
            this.btnCapNhatTienDo.Location = new System.Drawing.Point(281, 166);
            this.btnCapNhatTienDo.Name = "btnCapNhatTienDo";
            this.btnCapNhatTienDo.Size = new System.Drawing.Size(157, 23);
            this.btnCapNhatTienDo.TabIndex = 10;
            this.btnCapNhatTienDo.Text = "Cập Nhật";
            this.btnCapNhatTienDo.UseVisualStyleBackColor = true;
            this.btnCapNhatTienDo.Click += new System.EventHandler(this.btnCapNhatTienDo_Click);
            // 
            // FormNhiemVuCuaToi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 484);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormNhiemVuCuaToi";
            this.Text = "FormNhiemVuCuaToi";
            this.Load += new System.EventHandler(this.FormNhiemVuCuaToi_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhiemVu)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvNhiemVu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblNhiemVuDangChon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCapNhatTienDo;
        private System.Windows.Forms.TextBox txtGhiChuBaoCao;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTienDo;
        private System.Windows.Forms.Label label3;
    }
}