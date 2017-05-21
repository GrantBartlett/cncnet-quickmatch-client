using System;

namespace cncnet_quickmatch_client
{
    partial class QuickMatchClient
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
            this.sendResult = new System.Windows.Forms.Button();
            this.lbGames = new System.Windows.Forms.ListBox();
            this.lblGame = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUserPassword = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbUsernames = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tbLog = new System.Windows.Forms.TextBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sendResult
            // 
            this.sendResult.Location = new System.Drawing.Point(19, 219);
            this.sendResult.Name = "sendResult";
            this.sendResult.Size = new System.Drawing.Size(89, 33);
            this.sendResult.TabIndex = 0;
            this.sendResult.Text = "Send Result";
            this.sendResult.UseVisualStyleBackColor = true;
            this.sendResult.Click += new System.EventHandler(this.SendResultClicked);
            // 
            // lbGames
            // 
            this.lbGames.FormattingEnabled = true;
            this.lbGames.Items.AddRange(new object[] {
            "RA",
            "TS",
            "YR"});
            this.lbGames.Location = new System.Drawing.Point(19, 82);
            this.lbGames.Name = "lbGames";
            this.lbGames.Size = new System.Drawing.Size(209, 43);
            this.lbGames.TabIndex = 1;
            this.lbGames.SelectedValueChanged += new System.EventHandler(this.OnSelectedGameChanged);
            // 
            // lblGame
            // 
            this.lblGame.AutoSize = true;
            this.lblGame.Location = new System.Drawing.Point(16, 66);
            this.lblGame.Name = "lblGame";
            this.lblGame.Size = new System.Drawing.Size(35, 13);
            this.lblGame.TabIndex = 2;
            this.lblGame.Text = "Game";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Username";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(19, 32);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(100, 20);
            this.tbUsername.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Password";
            // 
            // tbUserPassword
            // 
            this.tbUserPassword.Location = new System.Drawing.Point(128, 32);
            this.tbUserPassword.Name = "tbUserPassword";
            this.tbUserPassword.PasswordChar = '*';
            this.tbUserPassword.Size = new System.Drawing.Size(100, 20);
            this.tbUserPassword.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(234, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 21);
            this.button1.TabIndex = 10;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.LoginClicked);
            // 
            // lbUsernames
            // 
            this.lbUsernames.FormattingEnabled = true;
            this.lbUsernames.Location = new System.Drawing.Point(19, 157);
            this.lbUsernames.Name = "lbUsernames";
            this.lbUsernames.Size = new System.Drawing.Size(209, 43);
            this.lbUsernames.TabIndex = 11;
            this.lbUsernames.SelectedValueChanged += new System.EventHandler(this.OnSelectedUsernamedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Player Usernames";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(128, 219);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(195, 33);
            this.tbLog.TabIndex = 15;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLog.Location = new System.Drawing.Point(298, 203);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(25, 13);
            this.lblLog.TabIndex = 16;
            this.lblLog.Text = "Log";
            this.lblLog.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // QuickMatchClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 264);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbUsernames);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbUserPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.lblGame);
            this.Controls.Add(this.lbGames);
            this.Controls.Add(this.sendResult);
            this.Name = "QuickMatchClient";
            this.Text = "Ladder Stats Client";
            this.Load += new System.EventHandler(this.QuickMatchClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button sendResult;
        private System.Windows.Forms.ListBox lbGames;
        private System.Windows.Forms.Label lblGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUserPassword;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lbUsernames;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Label lblLog;
    }
}

