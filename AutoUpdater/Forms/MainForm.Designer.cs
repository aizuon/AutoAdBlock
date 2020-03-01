namespace AutoUpdater.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.status = new System.Windows.Forms.Label();
            this.update = new System.Windows.Forms.Button();
            this.config = new System.Windows.Forms.Button();
            this.lastUpdatedLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "AutoAdBlock";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(21, 22);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(37, 13);
            this.status.TabIndex = 0;
            this.status.Text = "Status";
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(137, 47);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 1;
            this.update.Text = "Update";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // config
            // 
            this.config.Location = new System.Drawing.Point(272, 12);
            this.config.Name = "config";
            this.config.Size = new System.Drawing.Size(75, 23);
            this.config.TabIndex = 4;
            this.config.Text = "Config";
            this.config.UseVisualStyleBackColor = true;
            this.config.Click += new System.EventHandler(this.config_Click);
            // 
            // lastUpdatedLabel
            // 
            this.lastUpdatedLabel.AutoSize = true;
            this.lastUpdatedLabel.Location = new System.Drawing.Point(21, 80);
            this.lastUpdatedLabel.Name = "lastUpdatedLabel";
            this.lastUpdatedLabel.Size = new System.Drawing.Size(106, 13);
            this.lastUpdatedLabel.TabIndex = 5;
            this.lastUpdatedLabel.Text = "Last Updated: Never";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 106);
            this.Controls.Add(this.lastUpdatedLabel);
            this.Controls.Add(this.config);
            this.Controls.Add(this.update);
            this.Controls.Add(this.status);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button config;
        private System.Windows.Forms.Label lastUpdatedLabel;
    }
}