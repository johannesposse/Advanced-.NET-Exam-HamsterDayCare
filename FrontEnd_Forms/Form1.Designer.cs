
namespace FrontEnd_Forms
{
    partial class HamsterDayCare
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HamsterDayCare));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Nav_Pnl = new System.Windows.Forms.Panel();
            this.Settings_Button = new System.Windows.Forms.Button();
            this.Statistics_Button = new System.Windows.Forms.Button();
            this.Reports_Button = new System.Windows.Forms.Button();
            this.simulation_button = new System.Windows.Forms.Button();
            this.Dashboard_button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.UserName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.PnlFormLoader = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panel1.Controls.Add(this.Nav_Pnl);
            this.panel1.Controls.Add(this.Settings_Button);
            this.panel1.Controls.Add(this.Statistics_Button);
            this.panel1.Controls.Add(this.Reports_Button);
            this.panel1.Controls.Add(this.simulation_button);
            this.panel1.Controls.Add(this.Dashboard_button);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 700);
            this.panel1.TabIndex = 0;
            // 
            // Nav_Pnl
            // 
            this.Nav_Pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.Nav_Pnl.Location = new System.Drawing.Point(0, 267);
            this.Nav_Pnl.Name = "Nav_Pnl";
            this.Nav_Pnl.Size = new System.Drawing.Size(3, 240);
            this.Nav_Pnl.TabIndex = 2;
            // 
            // Settings_Button
            // 
            this.Settings_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.Settings_Button.FlatAppearance.BorderSize = 0;
            this.Settings_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Settings_Button.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Settings_Button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.Settings_Button.Image = global::FrontEnd_Forms.Properties.Resources.settings;
            this.Settings_Button.Location = new System.Drawing.Point(0, 640);
            this.Settings_Button.Name = "Settings_Button";
            this.Settings_Button.Size = new System.Drawing.Size(232, 60);
            this.Settings_Button.TabIndex = 1;
            this.Settings_Button.Text = "Settings";
            this.Settings_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Settings_Button.UseVisualStyleBackColor = false;
            this.Settings_Button.Click += new System.EventHandler(this.Settings_Button_Click);
            this.Settings_Button.Leave += new System.EventHandler(this.Settings_Button_Leave);
            // 
            // Statistics_Button
            // 
            this.Statistics_Button.Dock = System.Windows.Forms.DockStyle.Top;
            this.Statistics_Button.FlatAppearance.BorderSize = 0;
            this.Statistics_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Statistics_Button.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Statistics_Button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.Statistics_Button.Image = global::FrontEnd_Forms.Properties.Resources.diagram;
            this.Statistics_Button.Location = new System.Drawing.Point(0, 447);
            this.Statistics_Button.Name = "Statistics_Button";
            this.Statistics_Button.Size = new System.Drawing.Size(231, 60);
            this.Statistics_Button.TabIndex = 1;
            this.Statistics_Button.Text = "Statistics";
            this.Statistics_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Statistics_Button.UseVisualStyleBackColor = true;
            this.Statistics_Button.Click += new System.EventHandler(this.Statistics_Button_Click);
            this.Statistics_Button.Leave += new System.EventHandler(this.Statistics_Button_Leave);
            // 
            // Reports_Button
            // 
            this.Reports_Button.Dock = System.Windows.Forms.DockStyle.Top;
            this.Reports_Button.FlatAppearance.BorderSize = 0;
            this.Reports_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Reports_Button.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Reports_Button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.Reports_Button.Image = global::FrontEnd_Forms.Properties.Resources.Conact;
            this.Reports_Button.Location = new System.Drawing.Point(0, 387);
            this.Reports_Button.Name = "Reports_Button";
            this.Reports_Button.Size = new System.Drawing.Size(231, 60);
            this.Reports_Button.TabIndex = 1;
            this.Reports_Button.Text = "Reports";
            this.Reports_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Reports_Button.UseVisualStyleBackColor = true;
            this.Reports_Button.Click += new System.EventHandler(this.Reports_Button_Click);
            this.Reports_Button.Leave += new System.EventHandler(this.Reports_Button_Leave);
            // 
            // simulation_button
            // 
            this.simulation_button.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulation_button.FlatAppearance.BorderSize = 0;
            this.simulation_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.simulation_button.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.simulation_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.simulation_button.Image = global::FrontEnd_Forms.Properties.Resources.calendar;
            this.simulation_button.Location = new System.Drawing.Point(0, 327);
            this.simulation_button.Name = "simulation_button";
            this.simulation_button.Size = new System.Drawing.Size(231, 60);
            this.simulation_button.TabIndex = 1;
            this.simulation_button.Text = "Simulation";
            this.simulation_button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.simulation_button.UseVisualStyleBackColor = true;
            this.simulation_button.Click += new System.EventHandler(this.simulation_button_Click);
            this.simulation_button.Leave += new System.EventHandler(this.simulation_button_Leave);
            // 
            // Dashboard_button
            // 
            this.Dashboard_button.Dock = System.Windows.Forms.DockStyle.Top;
            this.Dashboard_button.FlatAppearance.BorderSize = 0;
            this.Dashboard_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Dashboard_button.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Dashboard_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.Dashboard_button.Image = global::FrontEnd_Forms.Properties.Resources.home;
            this.Dashboard_button.Location = new System.Drawing.Point(0, 267);
            this.Dashboard_button.Name = "Dashboard_button";
            this.Dashboard_button.Size = new System.Drawing.Size(231, 60);
            this.Dashboard_button.TabIndex = 1;
            this.Dashboard_button.Text = "Dashboard";
            this.Dashboard_button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Dashboard_button.UseVisualStyleBackColor = true;
            this.Dashboard_button.Click += new System.EventHandler(this.Dashboard_button_Click);
            this.Dashboard_button.Leave += new System.EventHandler(this.Dashboard_button_Leave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.UserName);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(231, 267);
            this.panel2.TabIndex = 0;
            // 
            // UserName
            // 
            this.UserName.AutoSize = true;
            this.UserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UserName.ForeColor = System.Drawing.Color.White;
            this.UserName.Location = new System.Drawing.Point(0, 230);
            this.UserName.MinimumSize = new System.Drawing.Size(232, 0);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(232, 16);
            this.UserName.TabIndex = 1;
            this.UserName.Text = "Welcome ";
            this.UserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FrontEnd_Forms.Properties.Resources.logo2;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(208, 193);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 17);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(705, 32);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Welcome to the best Hamster Daycare, in the world";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.PnlFormLoader);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.lblTitle);
            this.panel3.Location = new System.Drawing.Point(238, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1061, 700);
            this.panel3.TabIndex = 1;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // PnlFormLoader
            // 
            this.PnlFormLoader.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlFormLoader.Location = new System.Drawing.Point(0, 100);
            this.PnlFormLoader.Name = "PnlFormLoader";
            this.PnlFormLoader.Size = new System.Drawing.Size(1061, 600);
            this.PnlFormLoader.TabIndex = 4;
            this.PnlFormLoader.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlFormLoader_Paint);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(63)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(91)))), ((int)(((byte)(113)))));
            this.button1.Location = new System.Drawing.Point(1015, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HamsterDayCare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HamsterDayCare";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HamsterDayCare";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label UserName;
        private System.Windows.Forms.Button Settings_Button;
        private System.Windows.Forms.Button Statistics_Button;
        private System.Windows.Forms.Button Reports_Button;
        private System.Windows.Forms.Button simulation_button;
        private System.Windows.Forms.Button Dashboard_button;
        private System.Windows.Forms.Panel Nav_Pnl;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel PnlFormLoader;
    }
}

