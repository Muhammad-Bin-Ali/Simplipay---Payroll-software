namespace SimpliPay
{
    partial class StartPayRoll
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
            this.lblConductPR = new System.Windows.Forms.Label();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.comboBoxPayPrd = new System.Windows.Forms.ComboBox();
            this.calTO = new System.Windows.Forms.MonthCalendar();
            this.lblTO = new System.Windows.Forms.Label();
            this.calFROM = new System.Windows.Forms.MonthCalendar();
            this.lblPayPrd = new System.Windows.Forms.Label();
            this.lblFROM = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblConductPR
            // 
            this.lblConductPR.AutoSize = true;
            this.lblConductPR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblConductPR.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConductPR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblConductPR.Location = new System.Drawing.Point(43, 43);
            this.lblConductPR.Name = "lblConductPR";
            this.lblConductPR.Size = new System.Drawing.Size(211, 37);
            this.lblConductPR.TabIndex = 2;
            this.lblConductPR.Text = "Conduct Payroll";
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(31)))), ((int)(((byte)(61)))));
            this.panelContainer.Controls.Add(this.comboBoxPayPrd);
            this.panelContainer.Controls.Add(this.calTO);
            this.panelContainer.Controls.Add(this.lblTO);
            this.panelContainer.Controls.Add(this.calFROM);
            this.panelContainer.Controls.Add(this.lblPayPrd);
            this.panelContainer.Controls.Add(this.lblFROM);
            this.panelContainer.Location = new System.Drawing.Point(50, 206);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1039, 340);
            this.panelContainer.TabIndex = 3;
            // 
            // comboBoxPayPrd
            // 
            this.comboBoxPayPrd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPayPrd.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxPayPrd.FormattingEnabled = true;
            this.comboBoxPayPrd.Items.AddRange(new object[] {
            "52/Year",
            "26/Year",
            "24/Year",
            "12/Year"});
            this.comboBoxPayPrd.Location = new System.Drawing.Point(89, 101);
            this.comboBoxPayPrd.Name = "comboBoxPayPrd";
            this.comboBoxPayPrd.Size = new System.Drawing.Size(240, 21);
            this.comboBoxPayPrd.TabIndex = 0;
            // 
            // calTO
            // 
            this.calTO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.calTO.Location = new System.Drawing.Point(709, 101);
            this.calTO.MaxSelectionCount = 1;
            this.calTO.Name = "calTO";
            this.calTO.TabIndex = 2;
            // 
            // lblTO
            // 
            this.lblTO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTO.AutoSize = true;
            this.lblTO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTO.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTO.ForeColor = System.Drawing.Color.White;
            this.lblTO.Location = new System.Drawing.Point(705, 59);
            this.lblTO.Name = "lblTO";
            this.lblTO.Size = new System.Drawing.Size(32, 21);
            this.lblTO.TabIndex = 3;
            this.lblTO.Text = "TO";
            // 
            // calFROM
            // 
            this.calFROM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.calFROM.Location = new System.Drawing.Point(412, 101);
            this.calFROM.MaxSelectionCount = 1;
            this.calFROM.Name = "calFROM";
            this.calFROM.TabIndex = 1;
            this.calFROM.TrailingForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // lblPayPrd
            // 
            this.lblPayPrd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPayPrd.AutoSize = true;
            this.lblPayPrd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPayPrd.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayPrd.ForeColor = System.Drawing.Color.White;
            this.lblPayPrd.Location = new System.Drawing.Point(85, 59);
            this.lblPayPrd.Name = "lblPayPrd";
            this.lblPayPrd.Size = new System.Drawing.Size(106, 21);
            this.lblPayPrd.TabIndex = 3;
            this.lblPayPrd.Text = "PAY PERIOD";
            // 
            // lblFROM
            // 
            this.lblFROM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFROM.AutoSize = true;
            this.lblFROM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFROM.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFROM.ForeColor = System.Drawing.Color.White;
            this.lblFROM.Location = new System.Drawing.Point(408, 59);
            this.lblFROM.Name = "lblFROM";
            this.lblFROM.Size = new System.Drawing.Size(58, 21);
            this.lblFROM.TabIndex = 3;
            this.lblFROM.Text = "FROM";
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(41)))), ((int)(((byte)(81)))));
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(61)))), ((int)(((byte)(107)))));
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(22)))), ((int)(((byte)(48)))));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(978, 691);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(111, 33);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // StartPayRoll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.lblConductPR);
            this.Name = "StartPayRoll";
            this.Size = new System.Drawing.Size(1149, 755);
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblConductPR;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Label lblFROM;
        private System.Windows.Forms.Label lblTO;
        private System.Windows.Forms.ComboBox comboBoxPayPrd;
        private System.Windows.Forms.Label lblPayPrd;
        private System.Windows.Forms.Button btnNext;
        public System.Windows.Forms.MonthCalendar calTO;
        public System.Windows.Forms.MonthCalendar calFROM;
    }
}
