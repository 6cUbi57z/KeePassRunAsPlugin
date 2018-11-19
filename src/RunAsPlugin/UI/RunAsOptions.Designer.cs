namespace RunAsPlugin.UI
{
    partial class RunAsOptions
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
            this.runAsGroupBox = new System.Windows.Forms.GroupBox();
            this.argumentsLabel = new System.Windows.Forms.Label();
            this.arguments = new System.Windows.Forms.TextBox();
            this.setIcon = new System.Windows.Forms.Button();
            this.netOnly = new System.Windows.Forms.CheckBox();
            this.applicationBrowse = new System.Windows.Forms.Button();
            this.application = new System.Windows.Forms.TextBox();
            this.applicationLabel = new System.Windows.Forms.Label();
            this.enableRunAs = new System.Windows.Forms.CheckBox();
            this.workingDir = new System.Windows.Forms.TextBox();
            this.workingDirLabel = new System.Windows.Forms.Label();
            this.workingDirBrowse = new System.Windows.Forms.Button();
            this.runAsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // runAsGroupBox
            // 
            this.runAsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.runAsGroupBox.Controls.Add(this.workingDirBrowse);
            this.runAsGroupBox.Controls.Add(this.workingDirLabel);
            this.runAsGroupBox.Controls.Add(this.workingDir);
            this.runAsGroupBox.Controls.Add(this.argumentsLabel);
            this.runAsGroupBox.Controls.Add(this.arguments);
            this.runAsGroupBox.Controls.Add(this.setIcon);
            this.runAsGroupBox.Controls.Add(this.netOnly);
            this.runAsGroupBox.Controls.Add(this.applicationBrowse);
            this.runAsGroupBox.Controls.Add(this.application);
            this.runAsGroupBox.Controls.Add(this.applicationLabel);
            this.runAsGroupBox.Controls.Add(this.enableRunAs);
            this.runAsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.runAsGroupBox.Name = "runAsGroupBox";
            this.runAsGroupBox.Size = new System.Drawing.Size(418, 155);
            this.runAsGroupBox.TabIndex = 0;
            this.runAsGroupBox.TabStop = false;
            // 
            // argumentsLabel
            // 
            this.argumentsLabel.AutoSize = true;
            this.argumentsLabel.Location = new System.Drawing.Point(6, 53);
            this.argumentsLabel.Name = "argumentsLabel";
            this.argumentsLabel.Size = new System.Drawing.Size(57, 13);
            this.argumentsLabel.TabIndex = 7;
            this.argumentsLabel.Text = "Arguments";
            // 
            // arguments
            // 
            this.arguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.arguments.Location = new System.Drawing.Point(104, 50);
            this.arguments.Name = "arguments";
            this.arguments.Size = new System.Drawing.Size(227, 20);
            this.arguments.TabIndex = 6;
            // 
            // setIcon
            // 
            this.setIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.setIcon.Enabled = false;
            this.setIcon.Location = new System.Drawing.Point(6, 126);
            this.setIcon.Name = "setIcon";
            this.setIcon.Size = new System.Drawing.Size(406, 23);
            this.setIcon.TabIndex = 5;
            this.setIcon.Text = "Set Entry Icon from Application";
            this.setIcon.UseVisualStyleBackColor = true;
            this.setIcon.Click += new System.EventHandler(this.setIcon_Click);
            // 
            // netOnly
            // 
            this.netOnly.AutoSize = true;
            this.netOnly.Enabled = false;
            this.netOnly.Location = new System.Drawing.Point(104, 103);
            this.netOnly.Name = "netOnly";
            this.netOnly.Size = new System.Drawing.Size(182, 17);
            this.netOnly.TabIndex = 4;
            this.netOnly.Text = "Use Credentials for Network Only";
            this.netOnly.UseVisualStyleBackColor = true;
            // 
            // applicationBrowse
            // 
            this.applicationBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.applicationBrowse.Enabled = false;
            this.applicationBrowse.Location = new System.Drawing.Point(337, 21);
            this.applicationBrowse.Name = "applicationBrowse";
            this.applicationBrowse.Size = new System.Drawing.Size(75, 23);
            this.applicationBrowse.TabIndex = 3;
            this.applicationBrowse.Text = "Browse...";
            this.applicationBrowse.UseVisualStyleBackColor = true;
            this.applicationBrowse.Click += new System.EventHandler(this.applicationBrowse_Click);
            // 
            // application
            // 
            this.application.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.application.Enabled = false;
            this.application.Location = new System.Drawing.Point(104, 23);
            this.application.Name = "application";
            this.application.Size = new System.Drawing.Size(227, 20);
            this.application.TabIndex = 2;
            // 
            // applicationLabel
            // 
            this.applicationLabel.AutoSize = true;
            this.applicationLabel.Location = new System.Drawing.Point(6, 26);
            this.applicationLabel.Name = "applicationLabel";
            this.applicationLabel.Size = new System.Drawing.Size(59, 13);
            this.applicationLabel.TabIndex = 1;
            this.applicationLabel.Text = "Application";
            // 
            // enableRunAs
            // 
            this.enableRunAs.AutoSize = true;
            this.enableRunAs.Location = new System.Drawing.Point(6, 0);
            this.enableRunAs.Name = "enableRunAs";
            this.enableRunAs.Size = new System.Drawing.Size(97, 17);
            this.enableRunAs.TabIndex = 0;
            this.enableRunAs.Text = "Enable Run As";
            this.enableRunAs.UseVisualStyleBackColor = true;
            this.enableRunAs.CheckedChanged += new System.EventHandler(this.enableRunAs_CheckedChanged);
            // 
            // workingDir
            // 
            this.workingDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workingDir.Location = new System.Drawing.Point(104, 77);
            this.workingDir.Name = "workingDir";
            this.workingDir.Size = new System.Drawing.Size(227, 20);
            this.workingDir.TabIndex = 8;
            // 
            // workingDirLabel
            // 
            this.workingDirLabel.AutoSize = true;
            this.workingDirLabel.Location = new System.Drawing.Point(6, 80);
            this.workingDirLabel.Name = "workingDirLabel";
            this.workingDirLabel.Size = new System.Drawing.Size(92, 13);
            this.workingDirLabel.TabIndex = 9;
            this.workingDirLabel.Text = "Working Directory";
            // 
            // workingDirBrowse
            // 
            this.workingDirBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.workingDirBrowse.Location = new System.Drawing.Point(337, 75);
            this.workingDirBrowse.Name = "workingDirBrowse";
            this.workingDirBrowse.Size = new System.Drawing.Size(75, 23);
            this.workingDirBrowse.TabIndex = 10;
            this.workingDirBrowse.Text = "Browse...";
            this.workingDirBrowse.UseVisualStyleBackColor = true;
            this.workingDirBrowse.Click += new System.EventHandler(this.workingDirBrowse_Click);
            // 
            // RunAsOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.runAsGroupBox);
            this.Name = "RunAsOptions";
            this.Size = new System.Drawing.Size(424, 163);
            this.runAsGroupBox.ResumeLayout(false);
            this.runAsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox runAsGroupBox;
        private System.Windows.Forms.CheckBox netOnly;
        private System.Windows.Forms.Button applicationBrowse;
        private System.Windows.Forms.TextBox application;
        private System.Windows.Forms.Label applicationLabel;
        private System.Windows.Forms.CheckBox enableRunAs;
        private System.Windows.Forms.Button setIcon;
        private System.Windows.Forms.Label argumentsLabel;
        private System.Windows.Forms.TextBox arguments;
        private System.Windows.Forms.Button workingDirBrowse;
        private System.Windows.Forms.Label workingDirLabel;
        private System.Windows.Forms.TextBox workingDir;
    }
}
