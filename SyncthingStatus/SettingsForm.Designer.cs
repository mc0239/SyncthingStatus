﻿namespace SyncthingStatus
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxApiKey = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.linkLabelHomepage = new System.Windows.Forms.LinkLabel();
            this.checkBoxAddress = new System.Windows.Forms.CheckBox();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "API Key";
            // 
            // textBoxApiKey
            // 
            this.textBoxApiKey.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxApiKey.Location = new System.Drawing.Point(10, 31);
            this.textBoxApiKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 7);
            this.textBoxApiKey.MaxLength = 40;
            this.textBoxApiKey.Name = "textBoxApiKey";
            this.textBoxApiKey.Size = new System.Drawing.Size(371, 24);
            this.textBoxApiKey.TabIndex = 1;
            this.textBoxApiKey.Enter += new System.EventHandler(this.TextBoxApiKey_Enter);
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSave.Location = new System.Drawing.Point(249, 120);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(131, 24);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save settings";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelVersion.Location = new System.Drawing.Point(10, 162);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(136, 20);
            this.labelVersion.TabIndex = 3;
            this.labelVersion.Text = "SyncthingStatus 0.1";
            // 
            // linkLabelHomepage
            // 
            this.linkLabelHomepage.AutoSize = true;
            this.linkLabelHomepage.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.linkLabelHomepage.Location = new System.Drawing.Point(161, 162);
            this.linkLabelHomepage.Name = "linkLabelHomepage";
            this.linkLabelHomepage.Size = new System.Drawing.Size(94, 20);
            this.linkLabelHomepage.TabIndex = 4;
            this.linkLabelHomepage.TabStop = true;
            this.linkLabelHomepage.Text = "GitHub page";
            this.linkLabelHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelHomepage_LinkClicked);
            // 
            // checkBoxAddress
            // 
            this.checkBoxAddress.AutoSize = true;
            this.checkBoxAddress.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxAddress.Location = new System.Drawing.Point(10, 64);
            this.checkBoxAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 2);
            this.checkBoxAddress.Name = "checkBoxAddress";
            this.checkBoxAddress.Size = new System.Drawing.Size(191, 23);
            this.checkBoxAddress.TabIndex = 5;
            this.checkBoxAddress.Text = "Custom Syncthing address";
            this.checkBoxAddress.UseVisualStyleBackColor = true;
            this.checkBoxAddress.CheckedChanged += new System.EventHandler(this.CheckBoxAddress_CheckedChanged);
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Enabled = false;
            this.textBoxAddress.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxAddress.Location = new System.Drawing.Point(10, 88);
            this.textBoxAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 7);
            this.textBoxAddress.MaxLength = 40;
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(371, 26);
            this.textBoxAddress.TabIndex = 1;
            this.textBoxAddress.Enter += new System.EventHandler(this.TextBoxApiKey_Enter);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(391, 188);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.checkBoxAddress);
            this.Controls.Add(this.linkLabelHomepage);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxApiKey);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "SyncthingStatus";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxApiKey;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.LinkLabel linkLabelHomepage;
        private System.Windows.Forms.CheckBox checkBoxAddress;
        private System.Windows.Forms.TextBox textBoxAddress;
    }
}