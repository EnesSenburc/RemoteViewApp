namespace RemoteViewServer
{
    partial class Config
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
            this.label1 = new System.Windows.Forms.Label();
            this.inputServerIP = new System.Windows.Forms.TextBox();
            this.inputServerPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonServerSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(30, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // inputServerIP
            // 
            this.inputServerIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.inputServerIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputServerIP.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.inputServerIP.ForeColor = System.Drawing.Color.LightGray;
            this.inputServerIP.Location = new System.Drawing.Point(66, 30);
            this.inputServerIP.Name = "inputServerIP";
            this.inputServerIP.ReadOnly = true;
            this.inputServerIP.Size = new System.Drawing.Size(194, 26);
            this.inputServerIP.TabIndex = 1;
            // 
            // inputServerPort
            // 
            this.inputServerPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.inputServerPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputServerPort.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.inputServerPort.ForeColor = System.Drawing.Color.LightGray;
            this.inputServerPort.Location = new System.Drawing.Point(66, 68);
            this.inputServerPort.Name = "inputServerPort";
            this.inputServerPort.Size = new System.Drawing.Size(194, 26);
            this.inputServerPort.TabIndex = 3;
            this.inputServerPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputServerPort_KeyPress);
            this.inputServerPort.Validating += new System.ComponentModel.CancelEventHandler(this.inputServerPort_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(16, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port:";
            // 
            // buttonServerSave
            // 
            this.buttonServerSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(0)))));
            this.buttonServerSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonServerSave.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonServerSave.ForeColor = System.Drawing.Color.LightGray;
            this.buttonServerSave.Location = new System.Drawing.Point(180, 108);
            this.buttonServerSave.Name = "buttonServerSave";
            this.buttonServerSave.Size = new System.Drawing.Size(80, 30);
            this.buttonServerSave.TabIndex = 4;
            this.buttonServerSave.Text = "SAVE";
            this.buttonServerSave.UseVisualStyleBackColor = false;
            this.buttonServerSave.Click += new System.EventHandler(this.buttonServerSave_Click);
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.buttonServerSave);
            this.Controls.Add(this.inputServerPort);
            this.Controls.Add(this.inputServerIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Config";
            this.Text = "Server Config";
            this.Load += new System.EventHandler(this.Config_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputServerIP;
        private System.Windows.Forms.TextBox inputServerPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonServerSave;
    }
}