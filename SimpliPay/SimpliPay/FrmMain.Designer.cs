namespace SimpliPay
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Button btnEmpList;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.imgListIcons = new System.Windows.Forms.ImageList(this.components);
            this.panelNav = new System.Windows.Forms.Panel();
            this.btnPayRoll = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            btnEmpList = new System.Windows.Forms.Button();
            this.panelNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEmpList
            // 
            btnEmpList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(31)))), ((int)(((byte)(61)))));
            btnEmpList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            btnEmpList.Cursor = System.Windows.Forms.Cursors.Default;
            btnEmpList.Dock = System.Windows.Forms.DockStyle.Top;
            btnEmpList.FlatAppearance.BorderSize = 0;
            btnEmpList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(41)))), ((int)(((byte)(79)))));
            btnEmpList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(22)))), ((int)(((byte)(48)))));
            btnEmpList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnEmpList.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnEmpList.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnEmpList.ImageIndex = 1;
            btnEmpList.ImageList = this.imgListIcons;
            btnEmpList.Location = new System.Drawing.Point(0, 123);
            btnEmpList.Margin = new System.Windows.Forms.Padding(0);
            btnEmpList.Name = "btnEmpList";
            btnEmpList.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            btnEmpList.Size = new System.Drawing.Size(275, 72);
            btnEmpList.TabIndex = 1;
            btnEmpList.Text = "Employee List";
            btnEmpList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnEmpList.UseVisualStyleBackColor = false;
            btnEmpList.Click += new System.EventHandler(this.btnEmpList_Click);
            // 
            // imgListIcons
            // 
            this.imgListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListIcons.ImageStream")));
            this.imgListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListIcons.Images.SetKeyName(0, "hand.png");
            this.imgListIcons.Images.SetKeyName(1, "user.png");
            // 
            // panelNav
            // 
            this.panelNav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(31)))), ((int)(((byte)(61)))));
            this.panelNav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelNav.Controls.Add(this.btnPayRoll);
            this.panelNav.Controls.Add(btnEmpList);
            this.panelNav.Controls.Add(this.panel);
            this.panelNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelNav.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelNav.Location = new System.Drawing.Point(0, 0);
            this.panelNav.Name = "panelNav";
            this.panelNav.Size = new System.Drawing.Size(275, 763);
            this.panelNav.TabIndex = 0;
            // 
            // btnPayRoll
            // 
            this.btnPayRoll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(31)))), ((int)(((byte)(61)))));
            this.btnPayRoll.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPayRoll.FlatAppearance.BorderSize = 0;
            this.btnPayRoll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(41)))), ((int)(((byte)(79)))));
            this.btnPayRoll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(22)))), ((int)(((byte)(48)))));
            this.btnPayRoll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayRoll.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayRoll.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPayRoll.ImageIndex = 0;
            this.btnPayRoll.ImageList = this.imgListIcons;
            this.btnPayRoll.Location = new System.Drawing.Point(0, 195);
            this.btnPayRoll.Margin = new System.Windows.Forms.Padding(0);
            this.btnPayRoll.Name = "btnPayRoll";
            this.btnPayRoll.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.btnPayRoll.Size = new System.Drawing.Size(275, 72);
            this.btnPayRoll.TabIndex = 2;
            this.btnPayRoll.Text = "Conduct a Payroll";
            this.btnPayRoll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPayRoll.UseVisualStyleBackColor = false;
            this.btnPayRoll.Click += new System.EventHandler(this.btnPayRoll_Click);
            // 
            // panel
            // 
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(275, 123);
            this.panel.TabIndex = 0;
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.panelMain.Location = new System.Drawing.Point(273, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1154, 763);
            this.panelMain.TabIndex = 1;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1427, 763);
            this.Controls.Add(this.panelNav);
            this.Controls.Add(this.panelMain);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SimpliPay";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SimpliPay_FormClosed);
            this.Shown += new System.EventHandler(this.SimpliPay_Shown);
            this.panelNav.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button btnPayRoll;
        private System.Windows.Forms.ImageList imgListIcons;
        public System.Windows.Forms.Panel panelMain;
    }
}

