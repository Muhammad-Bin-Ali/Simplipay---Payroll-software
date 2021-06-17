namespace SimpliPay
{
    partial class FinalizePayRoll
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
            this.panelList = new System.Windows.Forms.Panel();
            this.panelHeading = new System.Windows.Forms.Panel();
            this.lblTotalPay = new System.Windows.Forms.Label();
            this.lblHours = new System.Windows.Forms.Label();
            this.lblEarnType = new System.Windows.Forms.Label();
            this.lblPayRateHeading = new System.Windows.Forms.Label();
            this.lblEmpNameHeading = new System.Windows.Forms.Label();
            this.lblReview = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnFinalize = new System.Windows.Forms.Button();
            this.panelList.SuspendLayout();
            this.panelHeading.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelList
            // 
            this.panelList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelList.AutoScroll = true;
            this.panelList.Controls.Add(this.panelHeading);
            this.panelList.Location = new System.Drawing.Point(56, 138);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(1043, 504);
            this.panelList.TabIndex = 6;
            // 
            // panelHeading
            // 
            this.panelHeading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(31)))), ((int)(((byte)(61)))));
            this.panelHeading.Controls.Add(this.lblTotalPay);
            this.panelHeading.Controls.Add(this.lblHours);
            this.panelHeading.Controls.Add(this.lblID);
            this.panelHeading.Controls.Add(this.lblEarnType);
            this.panelHeading.Controls.Add(this.lblPayRateHeading);
            this.panelHeading.Controls.Add(this.lblEmpNameHeading);
            this.panelHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeading.ForeColor = System.Drawing.Color.AliceBlue;
            this.panelHeading.Location = new System.Drawing.Point(0, 0);
            this.panelHeading.Name = "panelHeading";
            this.panelHeading.Size = new System.Drawing.Size(1043, 59);
            this.panelHeading.TabIndex = 1;
            // 
            // lblTotalPay
            // 
            this.lblTotalPay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalPay.AutoSize = true;
            this.lblTotalPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalPay.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPay.ForeColor = System.Drawing.Color.White;
            this.lblTotalPay.Location = new System.Drawing.Point(926, 20);
            this.lblTotalPay.Name = "lblTotalPay";
            this.lblTotalPay.Size = new System.Drawing.Size(66, 19);
            this.lblTotalPay.TabIndex = 4;
            this.lblTotalPay.Text = "Total Pay";
            // 
            // lblHours
            // 
            this.lblHours.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHours.AutoSize = true;
            this.lblHours.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHours.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHours.ForeColor = System.Drawing.Color.White;
            this.lblHours.Location = new System.Drawing.Point(790, 20);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(46, 19);
            this.lblHours.TabIndex = 4;
            this.lblHours.Text = "Hours";
            // 
            // lblEarnType
            // 
            this.lblEarnType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEarnType.AutoSize = true;
            this.lblEarnType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEarnType.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEarnType.ForeColor = System.Drawing.Color.White;
            this.lblEarnType.Location = new System.Drawing.Point(450, 20);
            this.lblEarnType.Name = "lblEarnType";
            this.lblEarnType.Size = new System.Drawing.Size(95, 19);
            this.lblEarnType.TabIndex = 4;
            this.lblEarnType.Text = "Earnings Type";
            // 
            // lblPayRateHeading
            // 
            this.lblPayRateHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPayRateHeading.AutoSize = true;
            this.lblPayRateHeading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPayRateHeading.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayRateHeading.ForeColor = System.Drawing.Color.White;
            this.lblPayRateHeading.Location = new System.Drawing.Point(629, 19);
            this.lblPayRateHeading.Name = "lblPayRateHeading";
            this.lblPayRateHeading.Size = new System.Drawing.Size(63, 19);
            this.lblPayRateHeading.TabIndex = 4;
            this.lblPayRateHeading.Text = "Pay Rate";
            // 
            // lblEmpNameHeading
            // 
            this.lblEmpNameHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEmpNameHeading.AutoSize = true;
            this.lblEmpNameHeading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEmpNameHeading.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpNameHeading.ForeColor = System.Drawing.Color.White;
            this.lblEmpNameHeading.Location = new System.Drawing.Point(53, 20);
            this.lblEmpNameHeading.Name = "lblEmpNameHeading";
            this.lblEmpNameHeading.Size = new System.Drawing.Size(110, 19);
            this.lblEmpNameHeading.TabIndex = 2;
            this.lblEmpNameHeading.Text = "Employee Name";
            // 
            // lblReview
            // 
            this.lblReview.AutoSize = true;
            this.lblReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblReview.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblReview.Location = new System.Drawing.Point(49, 49);
            this.lblReview.Name = "lblReview";
            this.lblReview.Size = new System.Drawing.Size(254, 37);
            this.lblReview.TabIndex = 5;
            this.lblReview.Text = "Review and Finalize";
            // 
            // lblID
            // 
            this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblID.AutoSize = true;
            this.lblID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblID.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.ForeColor = System.Drawing.Color.White;
            this.lblID.Location = new System.Drawing.Point(310, 19);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(87, 19);
            this.lblID.TabIndex = 4;
            this.lblID.Text = "Employee ID";
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(41)))), ((int)(((byte)(81)))));
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(61)))), ((int)(((byte)(107)))));
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(22)))), ((int)(((byte)(48)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(56, 691);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(111, 33);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnFinalize
            // 
            this.btnFinalize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(41)))), ((int)(((byte)(81)))));
            this.btnFinalize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.btnFinalize.FlatAppearance.BorderSize = 0;
            this.btnFinalize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(61)))), ((int)(((byte)(107)))));
            this.btnFinalize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(22)))), ((int)(((byte)(48)))));
            this.btnFinalize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalize.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalize.ForeColor = System.Drawing.Color.White;
            this.btnFinalize.Location = new System.Drawing.Point(988, 691);
            this.btnFinalize.Name = "btnFinalize";
            this.btnFinalize.Size = new System.Drawing.Size(111, 33);
            this.btnFinalize.TabIndex = 7;
            this.btnFinalize.Text = "Finalize";
            this.btnFinalize.UseVisualStyleBackColor = false;
            this.btnFinalize.Click += new System.EventHandler(this.btnFinalize_Click);
            // 
            // FinalizePayRoll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.panelList);
            this.Controls.Add(this.btnFinalize);
            this.Controls.Add(this.lblReview);
            this.Name = "FinalizePayRoll";
            this.Size = new System.Drawing.Size(1149, 755);
            this.panelList.ResumeLayout(false);
            this.panelHeading.ResumeLayout(false);
            this.panelHeading.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.Panel panelHeading;
        private System.Windows.Forms.Label lblTotalPay;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblEarnType;
        private System.Windows.Forms.Label lblPayRateHeading;
        private System.Windows.Forms.Label lblEmpNameHeading;
        private System.Windows.Forms.Label lblReview;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnFinalize;
    }
}
