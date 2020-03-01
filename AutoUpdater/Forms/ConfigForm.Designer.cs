namespace AutoUpdater.Forms
{
    partial class ConfigForm
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
            this.updateIntervalLabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.autoStartCheckBox = new System.Windows.Forms.CheckBox();
            this.save = new System.Windows.Forms.Button();
            this.startMinimizedCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // updateIntervalLabel
            // 
            this.updateIntervalLabel.AutoSize = true;
            this.updateIntervalLabel.Location = new System.Drawing.Point(12, 22);
            this.updateIntervalLabel.Name = "updateIntervalLabel";
            this.updateIntervalLabel.Size = new System.Drawing.Size(95, 13);
            this.updateIntervalLabel.TabIndex = 0;
            this.updateIntervalLabel.Text = "Update Interval (h)";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "12",
            "16",
            "20",
            "24"});
            this.comboBox1.Location = new System.Drawing.Point(123, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // autoStartCheckBox
            // 
            this.autoStartCheckBox.AutoSize = true;
            this.autoStartCheckBox.Location = new System.Drawing.Point(15, 50);
            this.autoStartCheckBox.Name = "autoStartCheckBox";
            this.autoStartCheckBox.Size = new System.Drawing.Size(73, 17);
            this.autoStartCheckBox.TabIndex = 2;
            this.autoStartCheckBox.Text = "Auto Start";
            this.autoStartCheckBox.UseVisualStyleBackColor = true;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(94, 217);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 3;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // startMinimizedCheckBox
            // 
            this.startMinimizedCheckBox.AutoSize = true;
            this.startMinimizedCheckBox.Location = new System.Drawing.Point(15, 82);
            this.startMinimizedCheckBox.Name = "startMinimizedCheckBox";
            this.startMinimizedCheckBox.Size = new System.Drawing.Size(97, 17);
            this.startMinimizedCheckBox.TabIndex = 4;
            this.startMinimizedCheckBox.Text = "Start Minimized";
            this.startMinimizedCheckBox.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 261);
            this.Controls.Add(this.startMinimizedCheckBox);
            this.Controls.Add(this.save);
            this.Controls.Add(this.autoStartCheckBox);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.updateIntervalLabel);
            this.Name = "ConfigForm";
            this.Text = "Config";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label updateIntervalLabel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox autoStartCheckBox;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.CheckBox startMinimizedCheckBox;
    }
}