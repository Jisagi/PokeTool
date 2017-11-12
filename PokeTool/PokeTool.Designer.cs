namespace PokeTool
{
    partial class PokeTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PokeTool));
            this.btnBackup = new System.Windows.Forms.Button();
            this.lblBackupValidator = new System.Windows.Forms.Label();
            this.btnRomFs = new System.Windows.Forms.Button();
            this.lblRomFsValidator = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.cboGameSelection = new System.Windows.Forms.ComboBox();
            this.lblGameSelection = new System.Windows.Forms.Label();
            this.lblBackup = new System.Windows.Forms.Label();
            this.lblRomFs = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(87, 33);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(68, 23);
            this.btnBackup.TabIndex = 1;
            this.btnBackup.Text = "Select";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // lblBackupValidator
            // 
            this.lblBackupValidator.AutoSize = true;
            this.lblBackupValidator.ForeColor = System.Drawing.Color.Red;
            this.lblBackupValidator.Location = new System.Drawing.Point(161, 38);
            this.lblBackupValidator.Name = "lblBackupValidator";
            this.lblBackupValidator.Size = new System.Drawing.Size(37, 13);
            this.lblBackupValidator.TabIndex = 2;
            this.lblBackupValidator.Text = "invalid";
            // 
            // btnRomFs
            // 
            this.btnRomFs.Location = new System.Drawing.Point(87, 62);
            this.btnRomFs.Name = "btnRomFs";
            this.btnRomFs.Size = new System.Drawing.Size(68, 23);
            this.btnRomFs.TabIndex = 3;
            this.btnRomFs.Text = "Select";
            this.btnRomFs.UseVisualStyleBackColor = true;
            this.btnRomFs.Click += new System.EventHandler(this.btnRomFs_Click);
            // 
            // lblRomFsValidator
            // 
            this.lblRomFsValidator.AutoSize = true;
            this.lblRomFsValidator.ForeColor = System.Drawing.Color.Red;
            this.lblRomFsValidator.Location = new System.Drawing.Point(161, 67);
            this.lblRomFsValidator.Name = "lblRomFsValidator";
            this.lblRomFsValidator.Size = new System.Drawing.Size(37, 13);
            this.lblRomFsValidator.TabIndex = 4;
            this.lblRomFsValidator.Text = "invalid";
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(87, 102);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(68, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cboGameSelection
            // 
            this.cboGameSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGameSelection.FormattingEnabled = true;
            this.cboGameSelection.Items.AddRange(new object[] {
            "X",
            "Y",
            "Omega Ruby",
            "Alpha Sapphire",
            "Sun",
            "Moon",
            "Ultra Sun",
            "Ultra Moon"});
            this.cboGameSelection.Location = new System.Drawing.Point(87, 6);
            this.cboGameSelection.Name = "cboGameSelection";
            this.cboGameSelection.Size = new System.Drawing.Size(102, 21);
            this.cboGameSelection.TabIndex = 6;
            // 
            // lblGameSelection
            // 
            this.lblGameSelection.AutoSize = true;
            this.lblGameSelection.Location = new System.Drawing.Point(12, 9);
            this.lblGameSelection.Name = "lblGameSelection";
            this.lblGameSelection.Size = new System.Drawing.Size(35, 13);
            this.lblGameSelection.TabIndex = 7;
            this.lblGameSelection.Text = "Game";
            // 
            // lblBackup
            // 
            this.lblBackup.AutoSize = true;
            this.lblBackup.Location = new System.Drawing.Point(12, 38);
            this.lblBackup.Name = "lblBackup";
            this.lblBackup.Size = new System.Drawing.Size(76, 13);
            this.lblBackup.TabIndex = 8;
            this.lblBackup.Text = "Backup Folder";
            // 
            // lblRomFs
            // 
            this.lblRomFs.AutoSize = true;
            this.lblRomFs.Location = new System.Drawing.Point(12, 68);
            this.lblRomFs.Name = "lblRomFs";
            this.lblRomFs.Size = new System.Drawing.Size(72, 13);
            this.lblRomFs.TabIndex = 9;
            this.lblRomFs.Text = "RomFs Folder";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 135);
            this.Controls.Add(this.lblRomFs);
            this.Controls.Add(this.lblBackup);
            this.Controls.Add(this.lblGameSelection);
            this.Controls.Add(this.cboGameSelection);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblRomFsValidator);
            this.Controls.Add(this.btnRomFs);
            this.Controls.Add(this.lblBackupValidator);
            this.Controls.Add(this.btnBackup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PokeTool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Label lblBackupValidator;
        private System.Windows.Forms.Button btnRomFs;
        private System.Windows.Forms.Label lblRomFsValidator;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cboGameSelection;
        private System.Windows.Forms.Label lblGameSelection;
        private System.Windows.Forms.Label lblBackup;
        private System.Windows.Forms.Label lblRomFs;
    }
}

