namespace SimpliPay
{
    partial class ChooseEmpObject
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblEmpName = new System.Windows.Forms.Label();
            this.lblEarningType = new System.Windows.Forms.Label();
            this.txtPayRate = new System.Windows.Forms.TextBox();
            this.txtHours = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.checkEmp = new System.Windows.Forms.CheckBox();
            this.pictureRemove = new System.Windows.Forms.PictureBox();
            this.picPayAdd = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPayAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEmpName
            // 
            this.lblEmpName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEmpName.AutoSize = true;
            this.lblEmpName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEmpName.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblEmpName.Location = new System.Drawing.Point(64, 20);
            this.lblEmpName.Name = "lblEmpName";
            this.lblEmpName.Size = new System.Drawing.Size(110, 19);
            this.lblEmpName.TabIndex = 3;
            this.lblEmpName.Text = "Employee Name";
            // 
            // lblEarningType
            // 
            this.lblEarningType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEarningType.AutoSize = true;
            this.lblEarningType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEarningType.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEarningType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblEarningType.Location = new System.Drawing.Point(285, 20);
            this.lblEarningType.Name = "lblEarningType";
            this.lblEarningType.Size = new System.Drawing.Size(89, 19);
            this.lblEarningType.TabIndex = 6;
            this.lblEarningType.Text = "Earning Type";
            // 
            // txtPayRate
            // 
            this.txtPayRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPayRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.txtPayRate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayRate.Location = new System.Drawing.Point(437, 18);
            this.txtPayRate.Name = "txtPayRate";
            this.txtPayRate.Size = new System.Drawing.Size(100, 25);
            this.txtPayRate.TabIndex = 0;
            this.txtPayRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPayRate_KeyPress);
            this.txtPayRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPayRate_KeyUp);
            // 
            // txtHours
            // 
            this.txtHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHours.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.txtHours.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHours.Location = new System.Drawing.Point(588, 18);
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(100, 25);
            this.txtHours.TabIndex = 1;
            this.txtHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHours_KeyPress);
            this.txtHours.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtHours_KeyUp);
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.txtTotal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(741, 18);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 25);
            this.txtTotal.TabIndex = 2;
            this.txtTotal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotal_KeyPress);
            // 
            // checkEmp
            // 
            this.checkEmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEmp.AutoSize = true;
            this.checkEmp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.checkEmp.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.checkEmp.FlatAppearance.BorderSize = 0;
            this.checkEmp.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(31)))), ((int)(((byte)(61)))));
            this.checkEmp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(61)))), ((int)(((byte)(107)))));
            this.checkEmp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(22)))), ((int)(((byte)(48)))));
            this.checkEmp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkEmp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(31)))), ((int)(((byte)(61)))));
            this.checkEmp.Location = new System.Drawing.Point(1006, 24);
            this.checkEmp.Name = "checkEmp";
            this.checkEmp.Size = new System.Drawing.Size(12, 11);
            this.checkEmp.TabIndex = 3;
            this.checkEmp.UseVisualStyleBackColor = false;
            // 
            // pictureRemove
            // 
            this.pictureRemove.BackgroundImage = global::SimpliPay.Properties.Resources.remove;
            this.pictureRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureRemove.Location = new System.Drawing.Point(21, 17);
            this.pictureRemove.Name = "pictureRemove";
            this.pictureRemove.Size = new System.Drawing.Size(25, 22);
            this.pictureRemove.TabIndex = 10;
            this.pictureRemove.TabStop = false;
            this.pictureRemove.Click += new System.EventHandler(this.pictureRemove_Click);
            // 
            // picPayAdd
            // 
            this.picPayAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picPayAdd.BackgroundImage = global::SimpliPay.Properties.Resources.plus;
            this.picPayAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picPayAdd.Location = new System.Drawing.Point(917, 18);
            this.picPayAdd.Name = "picPayAdd";
            this.picPayAdd.Size = new System.Drawing.Size(21, 22);
            this.picPayAdd.TabIndex = 8;
            this.picPayAdd.TabStop = false;
            this.picPayAdd.Click += new System.EventHandler(this.picPayAdd_Click);
            // 
            // ChooseEmpObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureRemove);
            this.Controls.Add(this.checkEmp);
            this.Controls.Add(this.picPayAdd);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtHours);
            this.Controls.Add(this.txtPayRate);
            this.Controls.Add(this.lblEarningType);
            this.Controls.Add(this.lblEmpName);
            this.Name = "ChooseEmpObject";
            this.Size = new System.Drawing.Size(1058, 59);
            ((System.ComponentModel.ISupportInitialize)(this.pictureRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPayAdd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblEmpName;
        public System.Windows.Forms.Label lblEarningType;
        private System.Windows.Forms.PictureBox picPayAdd;
        public System.Windows.Forms.TextBox txtPayRate;
        public System.Windows.Forms.TextBox txtHours;
        public System.Windows.Forms.TextBox txtTotal;
        public System.Windows.Forms.CheckBox checkEmp;
        private System.Windows.Forms.PictureBox pictureRemove;
    }
}
