namespace BTL_LTTQ_QLPM.Forms.Admin
{
    partial class FormQuanLyTaiKhoan
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
            this.dgvUserList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnThemUser = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSuaUser = new System.Windows.Forms.Button();
            this.btnXoaUser = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Blue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dgvUserList);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(807, 372);
            this.panel1.TabIndex = 0;
            // 
            // dgvUserList
            // 
            this.dgvUserList.AllowUserToAddRows = false;
            this.dgvUserList.AllowUserToDeleteRows = false;
            this.dgvUserList.AllowUserToOrderColumns = true;
            this.dgvUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserList.Location = new System.Drawing.Point(0, 29);
            this.dgvUserList.Name = "dgvUserList";
            this.dgvUserList.ReadOnly = true;
            this.dgvUserList.RowHeadersWidth = 51;
            this.dgvUserList.RowTemplate.Height = 24;
            this.dgvUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserList.Size = new System.Drawing.Size(806, 343);
            this.dgvUserList.TabIndex = 0;
            this.dgvUserList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvUserList_CellFormatting);
            this.dgvUserList.SelectionChanged += new System.EventHandler(this.dgvUserList_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(266, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Danh Sách Tài Khoản";
            // 
            // btnThemUser
            // 
            this.btnThemUser.BackColor = System.Drawing.Color.FloralWhite;
            this.btnThemUser.Location = new System.Drawing.Point(54, 17);
            this.btnThemUser.Name = "btnThemUser";
            this.btnThemUser.Size = new System.Drawing.Size(113, 34);
            this.btnThemUser.TabIndex = 1;
            this.btnThemUser.Text = "Add";
            this.btnThemUser.UseVisualStyleBackColor = false;
            this.btnThemUser.Click += new System.EventHandler(this.btnThemUser_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Yellow;
            this.panel2.Controls.Add(this.btnXoaUser);
            this.panel2.Controls.Add(this.btnSuaUser);
            this.panel2.Controls.Add(this.btnThemUser);
            this.panel2.Location = new System.Drawing.Point(2, 373);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(803, 75);
            this.panel2.TabIndex = 2;
            // 
            // btnSuaUser
            // 
            this.btnSuaUser.BackColor = System.Drawing.Color.FloralWhite;
            this.btnSuaUser.Location = new System.Drawing.Point(334, 17);
            this.btnSuaUser.Name = "btnSuaUser";
            this.btnSuaUser.Size = new System.Drawing.Size(113, 34);
            this.btnSuaUser.TabIndex = 2;
            this.btnSuaUser.Text = "Sửa";
            this.btnSuaUser.UseVisualStyleBackColor = false;
            this.btnSuaUser.Click += new System.EventHandler(this.btnSuaUser_Click);
            // 
            // btnXoaUser
            // 
            this.btnXoaUser.BackColor = System.Drawing.Color.FloralWhite;
            this.btnXoaUser.Location = new System.Drawing.Point(627, 17);
            this.btnXoaUser.Name = "btnXoaUser";
            this.btnXoaUser.Size = new System.Drawing.Size(113, 34);
            this.btnXoaUser.TabIndex = 3;
            this.btnXoaUser.Text = "Xóa";
            this.btnXoaUser.UseVisualStyleBackColor = false;
            this.btnXoaUser.Click += new System.EventHandler(this.btnXoaUser_Click);
            // 
            // FormQuanLyTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormQuanLyTaiKhoan";
            this.Text = "FormQuanLyTaiKhoan";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvUserList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThemUser;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnXoaUser;
        private System.Windows.Forms.Button btnSuaUser;
    }
}