namespace SimpliPay
{
    partial class EmployeeList
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
            this.panelEmpList = new System.Windows.Forms.Panel();
            this.panelHeading = new System.Windows.Forms.Panel();
            this.lblEmpIDHeading = new System.Windows.Forms.Label();
            this.lblPayRateHeading = new System.Windows.Forms.Label();
            this.lblPayPeriodHeading = new System.Windows.Forms.Label();
            this.lblEmpTypeHeading = new System.Windows.Forms.Label();
            this.lblEmpNameHeading = new System.Windows.Forms.Label();
            this.lblEmpList = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.picAdd = new System.Windows.Forms.PictureBox();
            this.panelEmpList.SuspendLayout();
            this.panelHeading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEmpList
            // 
            this.panelEmpList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEmpList.AutoScroll = true;
            this.panelEmpList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.panelEmpList.Controls.Add(this.panelHeading);
            this.panelEmpList.Location = new System.Drawing.Point(47, 94);
            this.panelEmpList.Name = "panelEmpList";
            this.panelEmpList.Size = new System.Drawing.Size(1053, 615);
            this.panelEmpList.TabIndex = 0;
            // 
            // panelHeading
            // 
            this.panelHeading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(31)))), ((int)(((byte)(61)))));
            this.panelHeading.Controls.Add(this.lblEmpIDHeading);
            this.panelHeading.Controls.Add(this.lblPayRateHeading);
            this.panelHeading.Controls.Add(this.lblPayPeriodHeading);
            this.panelHeading.Controls.Add(this.lblEmpTypeHeading);
            this.panelHeading.Controls.Add(this.lblEmpNameHeading);
            this.panelHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeading.ForeColor = System.Drawing.Color.AliceBlue;
            this.panelHeading.Location = new System.Drawing.Point(0, 0);
            this.panelHeading.Name = "panelHeading";
            this.panelHeading.Size = new System.Drawing.Size(1053, 59);
            this.panelHeading.TabIndex = 0;
            // 
            // lblEmpIDHeading
            // 
            this.lblEmpIDHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmpIDHeading.AutoSize = true;
            this.lblEmpIDHeading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEmpIDHeading.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpIDHeading.ForeColor = System.Drawing.Color.White;
            this.lblEmpIDHeading.Location = new System.Drawing.Point(413, 20);
            this.lblEmpIDHeading.Name = "lblEmpIDHeading";
            this.lblEmpIDHeading.Size = new System.Drawing.Size(35, 19);
            this.lblEmpIDHeading.TabIndex = 5;
            this.lblEmpIDHeading.Text = "ID #";
            // 
            // lblPayRateHeading
            // 
            this.lblPayRateHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPayRateHeading.AutoSize = true;
            this.lblPayRateHeading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPayRateHeading.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayRateHeading.ForeColor = System.Drawing.Color.White;
            this.lblPayRateHeading.Location = new System.Drawing.Point(804, 20);
            this.lblPayRateHeading.Name = "lblPayRateHeading";
            this.lblPayRateHeading.Size = new System.Drawing.Size(63, 19);
            this.lblPayRateHeading.TabIndex = 4;
            this.lblPayRateHeading.Text = "Pay Rate";
            // 
            // lblPayPeriodHeading
            // 
            this.lblPayPeriodHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPayPeriodHeading.AutoSize = true;
            this.lblPayPeriodHeading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPayPeriodHeading.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayPeriodHeading.ForeColor = System.Drawing.Color.White;
            this.lblPayPeriodHeading.Location = new System.Drawing.Point(649, 20);
            this.lblPayPeriodHeading.Name = "lblPayPeriodHeading";
            this.lblPayPeriodHeading.Size = new System.Drawing.Size(75, 19);
            this.lblPayPeriodHeading.TabIndex = 3;
            this.lblPayPeriodHeading.Text = "Pay Period";
            // 
            // lblEmpTypeHeading
            // 
            this.lblEmpTypeHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmpTypeHeading.AutoSize = true;
            this.lblEmpTypeHeading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEmpTypeHeading.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpTypeHeading.ForeColor = System.Drawing.Color.White;
            this.lblEmpTypeHeading.Location = new System.Drawing.Point(526, 20);
            this.lblEmpTypeHeading.Name = "lblEmpTypeHeading";
            this.lblEmpTypeHeading.Size = new System.Drawing.Size(38, 19);
            this.lblEmpTypeHeading.TabIndex = 3;
            this.lblEmpTypeHeading.Text = "Type";
            // 
            // lblEmpNameHeading
            // 
            this.lblEmpNameHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEmpNameHeading.AutoSize = true;
            this.lblEmpNameHeading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEmpNameHeading.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpNameHeading.ForeColor = System.Drawing.Color.White;
            this.lblEmpNameHeading.Location = new System.Drawing.Point(33, 20);
            this.lblEmpNameHeading.Name = "lblEmpNameHeading";
            this.lblEmpNameHeading.Size = new System.Drawing.Size(110, 19);
            this.lblEmpNameHeading.TabIndex = 2;
            this.lblEmpNameHeading.Text = "Employee Name";
            // 
            // lblEmpList
            // 
            this.lblEmpList.AutoSize = true;
            this.lblEmpList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEmpList.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblEmpList.Location = new System.Drawing.Point(40, 31);
            this.lblEmpList.Name = "lblEmpList";
            this.lblEmpList.Size = new System.Drawing.Size(185, 37);
            this.lblEmpList.TabIndex = 1;
            this.lblEmpList.Text = "Employee List";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(41)))), ((int)(((byte)(81)))));
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(61)))), ((int)(((byte)(107)))));
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(22)))), ((int)(((byte)(48)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(989, 35);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(111, 33);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // picAdd
            // 
            this.picAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picAdd.BackgroundImage = global::SimpliPay.Properties.Resources.add;
            this.picAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picAdd.Location = new System.Drawing.Point(929, 31);
            this.picAdd.Name = "picAdd";
            this.picAdd.Size = new System.Drawing.Size(32, 37);
            this.picAdd.TabIndex = 3;
            this.picAdd.TabStop = false;
            this.picAdd.Click += new System.EventHandler(this.picAdd_Click);
            // 
            // EmployeeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.picAdd);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblEmpList);
            this.Controls.Add(this.panelEmpList);
            this.Name = "EmployeeList";
            this.Size = new System.Drawing.Size(1149, 755);
            this.Load += new System.EventHandler(this.EmployeeList_Load);
            this.panelEmpList.ResumeLayout(false);
            this.panelHeading.ResumeLayout(false);
            this.panelHeading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelEmpList;
        private System.Windows.Forms.Panel panelHeading;
        private System.Windows.Forms.Label lblEmpList;
        private System.Windows.Forms.Label lblPayRateHeading;
        private System.Windows.Forms.Label lblPayPeriodHeading;
        private System.Windows.Forms.Label lblEmpTypeHeading;
        private System.Windows.Forms.Label lblEmpNameHeading;
        private System.Windows.Forms.Label lblEmpIDHeading;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.PictureBox picAdd;
    }
}
