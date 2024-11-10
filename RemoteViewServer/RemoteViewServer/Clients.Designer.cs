namespace RemoteViewServer
{
    partial class Clients
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
            this.clientList = new System.Windows.Forms.ListBox();
            this.buttonClientNameSave = new System.Windows.Forms.Button();
            this.inputClientName = new System.Windows.Forms.TextBox();
            this.inputClientIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // clientList
            // 
            this.clientList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.clientList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clientList.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.clientList.ForeColor = System.Drawing.Color.LightGray;
            this.clientList.FormattingEnabled = true;
            this.clientList.ItemHeight = 18;
            this.clientList.Location = new System.Drawing.Point(12, 30);
            this.clientList.Name = "clientList";
            this.clientList.Size = new System.Drawing.Size(310, 180);
            this.clientList.TabIndex = 0;
            this.clientList.SelectedIndexChanged += new System.EventHandler(this.clientList_SelectedIndexChanged);
            // 
            // buttonClientNameSave
            // 
            this.buttonClientNameSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(0)))));
            this.buttonClientNameSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonClientNameSave.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonClientNameSave.ForeColor = System.Drawing.Color.White;
            this.buttonClientNameSave.Location = new System.Drawing.Point(242, 304);
            this.buttonClientNameSave.Name = "buttonClientNameSave";
            this.buttonClientNameSave.Size = new System.Drawing.Size(80, 30);
            this.buttonClientNameSave.TabIndex = 5;
            this.buttonClientNameSave.Text = "SAVE";
            this.buttonClientNameSave.UseVisualStyleBackColor = false;
            this.buttonClientNameSave.Click += new System.EventHandler(this.buttonClientNameSave_Click);
            // 
            // inputClientName
            // 
            this.inputClientName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.inputClientName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputClientName.Enabled = false;
            this.inputClientName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.inputClientName.ForeColor = System.Drawing.Color.LightGray;
            this.inputClientName.Location = new System.Drawing.Point(118, 267);
            this.inputClientName.Name = "inputClientName";
            this.inputClientName.Size = new System.Drawing.Size(204, 26);
            this.inputClientName.TabIndex = 7;
            this.inputClientName.TextChanged += new System.EventHandler(this.inputClientName_TextChanged);
            // 
            // inputClientIP
            // 
            this.inputClientIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.inputClientIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputClientIP.Enabled = false;
            this.inputClientIP.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.inputClientIP.ForeColor = System.Drawing.Color.LightGray;
            this.inputClientIP.Location = new System.Drawing.Point(118, 230);
            this.inputClientIP.Name = "inputClientIP";
            this.inputClientIP.ReadOnly = true;
            this.inputClientIP.Size = new System.Drawing.Size(204, 26);
            this.inputClientIP.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "Client List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(35, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "Client IP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.LightGray;
            this.label3.Location = new System.Drawing.Point(9, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Client Name:";
            // 
            // Clients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(334, 351);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputClientName);
            this.Controls.Add(this.inputClientIP);
            this.Controls.Add(this.buttonClientNameSave);
            this.Controls.Add(this.clientList);
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Clients";
            this.ShowIcon = false;
            this.Text = "Clients";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Clients_FormClosed);
            this.Load += new System.EventHandler(this.ClientName_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox clientList;
        private System.Windows.Forms.Button buttonClientNameSave;
        private System.Windows.Forms.TextBox inputClientName;
        private System.Windows.Forms.TextBox inputClientIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}