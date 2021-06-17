namespace SimpliPay
{
    partial class ChooseEmployees
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
            this.lblChoose = new System.Windows.Forms.Label();
            this.panelList = new System.Windows.Forms.Panel();
            this.panelHeading = new System.Windows.Forms.Panel();
            this.checkAll = new System.Windows.Forms.CheckBox();
            this.lblAdd = new System.Windows.Forms.Label();
            this.lblTotalPay = new System.Windows.Forms.Label();
            this.lblHours = new System.Windows.Forms.Label();
            this.lblEarnType = new System.Windows.Forms.Label();
            this.lblPayRateHeading = new System.Windows.Forms.Label();
            this.lblEmpNameHeading = new System.Windows.Forms.Label();
            this.btnReview = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.panelList.SuspendLayout();
            this.panelHeading.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblChoose
            // 
            this.lblChoose.AutoSize = true;
            this.lblChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblChoose.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChoose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblChoose.Location = new System.Drawing.Point(46, 50);
            this.lblChoose.Name = "lblChoose";
            this.lblChoose.Size = new System.Drawing.Size(246, 37);
            this.lblChoose.TabIndex = 3;
            this.lblChoose.Text = "Choose Employees";
            // 
            // panelList
            // 
            this.panelList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelList.AutoScroll = true;
            this.panelList.Controls.Add(this.panelHeading);
            this.panelList.Location = new System.Drawing.Point(53, 140);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(1043, 504);
            this.panelList.TabIndex = 4;
            // 
            // panelHeading
            // 
            this.panelHeading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(31)))), ((int)(((byte)(61)))));
            this.panelHeading.Controls.Add(this.checkAll);
            this.panelHeading.Controls.Add(this.lblAdd);
            this.panelHeading.Controls.Add(this.lblTotalPay);
            this.panelHeading.Controls.Add(this.lblHours);
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
            // checkAll
            // 
            this.checkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkAll.AutoSize = true;
            this.checkAll.Location = new System.Drawing.Point(988, 24);
            this.checkAll.Name = "checkAll";
            this.checkAll.Size = new System.Drawing.Size(15, 14);
            this.checkAll.TabIndex = 5;
            this.checkAll.UseVisualStyleBackColor = true;
            this.checkAll.CheckedChanged += new System.EventHandler(this.checkAll_CheckedChanged);
            // 
            // lblAdd
            // 
            this.lblAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAdd.AutoSize = true;
            this.lblAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdd.ForeColor = System.Drawing.Color.White;
            this.lblAdd.Location = new System.Drawing.Point(894, 21);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(34, 19);
            this.lblAdd.TabIndex = 4;
            this.lblAdd.Text = "Add";
            // 
            // lblTotalPay
            // 
            this.lblTotalPay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalPay.AutoSize = true;
            this.lblTotalPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalPay.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPay.ForeColor = System.Drawing.Color.White;
            this.lblTotalPay.Location = new System.Drawing.Point(740, 20);
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
            this.lblHours.Location = new System.Drawing.Point(604, 20);
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
            this.lblEarnType.Location = new System.Drawing.Point(264, 20);
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
            this.lblPayRateHeading.Location = new System.Drawing.Point(443, 19);
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
            // btnReview
            // 
            this.btnReview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(41)))), ((int)(((byte)(81)))));
            this.btnReview.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.btnReview.FlatAppearance.BorderSize = 0;
            this.btnReview.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(61)))), ((int)(((byte)(107)))));
            this.btnReview.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(22)))), ((int)(((byte)(48)))));
            this.btnReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReview.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReview.ForeColor = System.Drawing.Color.White;
            this.btnReview.Location = new System.Drawing.Point(985, 690);
            this.btnReview.Name = "btnReview";
            this.btnReview.Size = new System.Drawing.Size(111, 33);
            this.btnReview.TabIndex = 5;
            this.btnReview.Text = "Review";
            this.btnReview.UseVisualStyleBackColor = false;
            this.btnReview.Click += new System.EventHandler(this.btnReview_Click);
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
            this.btnBack.Location = new System.Drawing.Point(53, 690);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(111, 33);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ChooseEmployees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnReview);
            this.Controls.Add(this.panelList);
            this.Controls.Add(this.lblChoose);
            this.Name = "ChooseEmployees";
            this.Size = new System.Drawing.Size(1149, 755);
            this.Load += new System.EventHandler(this.ChooseEmployees_Load);
            this.panelList.ResumeLayout(false);
            this.panelHeading.ResumeLayout(false);
            this.panelHeading.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblChoose;
        private System.Windows.Forms.Panel panelHeading;
        private System.Windows.Forms.Label lblTotalPay;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.Label lblPayRateHeading;
        private System.Windows.Forms.Label lblEmpNameHeading;
        private System.Windows.Forms.Button btnReview;
        private System.Windows.Forms.CheckBox checkAll;
        private System.Windows.Forms.Label lblAdd;
        private System.Windows.Forms.Label lblEarnType;
        private System.Windows.Forms.Button btnBack;
        public System.Windows.Forms.Panel panelList;
    }
}
