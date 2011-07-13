namespace FFXIMonitor.Interface
{
    partial class Form_Logs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Logs));
            this.tabChat = new System.Windows.Forms.TabControl();
            this.tabChatPageSay = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rtbSay = new System.Windows.Forms.RichTextBox();
            this.tabChatPageTell = new System.Windows.Forms.TabPage();
            this.rtbTell = new System.Windows.Forms.RichTextBox();
            this.tabChatPageParty = new System.Windows.Forms.TabPage();
            this.rtbParty = new System.Windows.Forms.RichTextBox();
            this.tabChatPageLinkshell = new System.Windows.Forms.TabPage();
            this.rtbLinkshell = new System.Windows.Forms.RichTextBox();
            this.tabChatPageShout = new System.Windows.Forms.TabPage();
            this.rtbShout = new System.Windows.Forms.RichTextBox();
            this.tabChatPageOther = new System.Windows.Forms.TabPage();
            this.rtbOther = new System.Windows.Forms.RichTextBox();
            this.tabChatPageAll = new System.Windows.Forms.TabPage();
            this.rtbAll = new System.Windows.Forms.RichTextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabChatPageYell = new System.Windows.Forms.TabPage();
            this.rtbYell = new System.Windows.Forms.RichTextBox();
            this.tabChat.SuspendLayout();
            this.tabChatPageSay.SuspendLayout();
            this.tabChatPageTell.SuspendLayout();
            this.tabChatPageParty.SuspendLayout();
            this.tabChatPageLinkshell.SuspendLayout();
            this.tabChatPageShout.SuspendLayout();
            this.tabChatPageOther.SuspendLayout();
            this.tabChatPageAll.SuspendLayout();
            this.tabChatPageYell.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabChat
            // 
            this.tabChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabChat.Controls.Add(this.tabChatPageSay);
            this.tabChat.Controls.Add(this.tabChatPageTell);
            this.tabChat.Controls.Add(this.tabChatPageParty);
            this.tabChat.Controls.Add(this.tabChatPageLinkshell);
            this.tabChat.Controls.Add(this.tabChatPageShout);
            this.tabChat.Controls.Add(this.tabChatPageYell);
            this.tabChat.Controls.Add(this.tabChatPageOther);
            this.tabChat.Controls.Add(this.tabChatPageAll);
            this.tabChat.Location = new System.Drawing.Point(-1, 0);
            this.tabChat.Margin = new System.Windows.Forms.Padding(0);
            this.tabChat.Name = "tabChat";
            this.tabChat.Padding = new System.Drawing.Point(0, 0);
            this.tabChat.SelectedIndex = 0;
            this.tabChat.Size = new System.Drawing.Size(350, 173);
            this.tabChat.TabIndex = 34;
            this.tabChat.TabStop = false;
            this.tabChat.DoubleClick += new System.EventHandler(this.tabChat_DoubleClick);
            // 
            // tabChatPageSay
            // 
            this.tabChatPageSay.Controls.Add(this.textBox1);
            this.tabChatPageSay.Controls.Add(this.button1);
            this.tabChatPageSay.Controls.Add(this.rtbSay);
            this.tabChatPageSay.Location = new System.Drawing.Point(4, 22);
            this.tabChatPageSay.Name = "tabChatPageSay";
            this.tabChatPageSay.Size = new System.Drawing.Size(342, 147);
            this.tabChatPageSay.TabIndex = 8;
            this.tabChatPageSay.Text = "Say";
            this.tabChatPageSay.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(108, 113);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(78, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "invisible";
            this.textBox1.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "invisible";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // rtbSay
            // 
            this.rtbSay.BackColor = System.Drawing.Color.White;
            this.rtbSay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbSay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbSay.ForeColor = System.Drawing.Color.Black;
            this.rtbSay.Location = new System.Drawing.Point(0, 0);
            this.rtbSay.Margin = new System.Windows.Forms.Padding(0);
            this.rtbSay.Name = "rtbSay";
            this.rtbSay.ReadOnly = true;
            this.rtbSay.Size = new System.Drawing.Size(342, 147);
            this.rtbSay.TabIndex = 2;
            this.rtbSay.Text = "";
            this.rtbSay.WordWrap = false;
            this.rtbSay.VScroll += new System.EventHandler(this.rtbSay_VScroll);
            // 
            // tabChatPageTell
            // 
            this.tabChatPageTell.Controls.Add(this.rtbTell);
            this.tabChatPageTell.Location = new System.Drawing.Point(4, 22);
            this.tabChatPageTell.Name = "tabChatPageTell";
            this.tabChatPageTell.Size = new System.Drawing.Size(380, 146);
            this.tabChatPageTell.TabIndex = 2;
            this.tabChatPageTell.Text = "Tell";
            this.tabChatPageTell.UseVisualStyleBackColor = true;
            // 
            // rtbTell
            // 
            this.rtbTell.BackColor = System.Drawing.Color.White;
            this.rtbTell.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbTell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbTell.ForeColor = System.Drawing.Color.Black;
            this.rtbTell.Location = new System.Drawing.Point(0, 0);
            this.rtbTell.Margin = new System.Windows.Forms.Padding(0);
            this.rtbTell.Name = "rtbTell";
            this.rtbTell.ReadOnly = true;
            this.rtbTell.Size = new System.Drawing.Size(380, 146);
            this.rtbTell.TabIndex = 3;
            this.rtbTell.Text = "";
            this.rtbTell.WordWrap = false;
            this.rtbTell.VScroll += new System.EventHandler(this.rtbTell_VScroll);
            // 
            // tabChatPageParty
            // 
            this.tabChatPageParty.Controls.Add(this.rtbParty);
            this.tabChatPageParty.Location = new System.Drawing.Point(4, 22);
            this.tabChatPageParty.Name = "tabChatPageParty";
            this.tabChatPageParty.Size = new System.Drawing.Size(380, 146);
            this.tabChatPageParty.TabIndex = 3;
            this.tabChatPageParty.Text = "Party";
            this.tabChatPageParty.UseVisualStyleBackColor = true;
            // 
            // rtbParty
            // 
            this.rtbParty.BackColor = System.Drawing.Color.White;
            this.rtbParty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbParty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbParty.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbParty.ForeColor = System.Drawing.Color.Black;
            this.rtbParty.Location = new System.Drawing.Point(0, 0);
            this.rtbParty.Margin = new System.Windows.Forms.Padding(0);
            this.rtbParty.Name = "rtbParty";
            this.rtbParty.ReadOnly = true;
            this.rtbParty.Size = new System.Drawing.Size(380, 146);
            this.rtbParty.TabIndex = 1;
            this.rtbParty.Text = "";
            this.rtbParty.WordWrap = false;
            this.rtbParty.VScroll += new System.EventHandler(this.rtbParty_VScroll);
            // 
            // tabChatPageLinkshell
            // 
            this.tabChatPageLinkshell.Controls.Add(this.rtbLinkshell);
            this.tabChatPageLinkshell.Location = new System.Drawing.Point(4, 22);
            this.tabChatPageLinkshell.Name = "tabChatPageLinkshell";
            this.tabChatPageLinkshell.Size = new System.Drawing.Size(380, 146);
            this.tabChatPageLinkshell.TabIndex = 4;
            this.tabChatPageLinkshell.Text = "Linkshell";
            this.tabChatPageLinkshell.UseVisualStyleBackColor = true;
            // 
            // rtbLinkshell
            // 
            this.rtbLinkshell.BackColor = System.Drawing.Color.White;
            this.rtbLinkshell.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLinkshell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLinkshell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLinkshell.ForeColor = System.Drawing.Color.Black;
            this.rtbLinkshell.Location = new System.Drawing.Point(0, 0);
            this.rtbLinkshell.Margin = new System.Windows.Forms.Padding(0);
            this.rtbLinkshell.Name = "rtbLinkshell";
            this.rtbLinkshell.ReadOnly = true;
            this.rtbLinkshell.Size = new System.Drawing.Size(380, 146);
            this.rtbLinkshell.TabIndex = 1;
            this.rtbLinkshell.TabStop = false;
            this.rtbLinkshell.Text = "";
            this.rtbLinkshell.WordWrap = false;
            this.rtbLinkshell.VScroll += new System.EventHandler(this.rtbLinkshell_VScroll);
            // 
            // tabChatPageShout
            // 
            this.tabChatPageShout.Controls.Add(this.rtbShout);
            this.tabChatPageShout.Location = new System.Drawing.Point(4, 22);
            this.tabChatPageShout.Name = "tabChatPageShout";
            this.tabChatPageShout.Size = new System.Drawing.Size(342, 147);
            this.tabChatPageShout.TabIndex = 5;
            this.tabChatPageShout.Text = "Shout";
            this.tabChatPageShout.UseVisualStyleBackColor = true;
            // 
            // rtbShout
            // 
            this.rtbShout.BackColor = System.Drawing.Color.White;
            this.rtbShout.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbShout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbShout.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbShout.ForeColor = System.Drawing.Color.Black;
            this.rtbShout.Location = new System.Drawing.Point(0, 0);
            this.rtbShout.Margin = new System.Windows.Forms.Padding(0);
            this.rtbShout.Name = "rtbShout";
            this.rtbShout.ReadOnly = true;
            this.rtbShout.Size = new System.Drawing.Size(342, 147);
            this.rtbShout.TabIndex = 2;
            this.rtbShout.Text = "";
            this.rtbShout.WordWrap = false;
            this.rtbShout.VScroll += new System.EventHandler(this.rtbShout_VScroll);
            // 
            // tabChatPageOther
            // 
            this.tabChatPageOther.Controls.Add(this.rtbOther);
            this.tabChatPageOther.Location = new System.Drawing.Point(4, 22);
            this.tabChatPageOther.Name = "tabChatPageOther";
            this.tabChatPageOther.Size = new System.Drawing.Size(380, 146);
            this.tabChatPageOther.TabIndex = 9;
            this.tabChatPageOther.Text = "Other";
            this.tabChatPageOther.UseVisualStyleBackColor = true;
            // 
            // rtbOther
            // 
            this.rtbOther.BackColor = System.Drawing.Color.White;
            this.rtbOther.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbOther.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOther.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbOther.ForeColor = System.Drawing.Color.Black;
            this.rtbOther.Location = new System.Drawing.Point(0, 0);
            this.rtbOther.Margin = new System.Windows.Forms.Padding(0);
            this.rtbOther.Name = "rtbOther";
            this.rtbOther.ReadOnly = true;
            this.rtbOther.Size = new System.Drawing.Size(380, 146);
            this.rtbOther.TabIndex = 2;
            this.rtbOther.Text = "";
            this.rtbOther.WordWrap = false;
            this.rtbOther.VScroll += new System.EventHandler(this.rtbOther_VScroll);
            // 
            // tabChatPageAll
            // 
            this.tabChatPageAll.Controls.Add(this.rtbAll);
            this.tabChatPageAll.Location = new System.Drawing.Point(4, 22);
            this.tabChatPageAll.Name = "tabChatPageAll";
            this.tabChatPageAll.Size = new System.Drawing.Size(342, 147);
            this.tabChatPageAll.TabIndex = 7;
            this.tabChatPageAll.Text = "All";
            this.tabChatPageAll.UseVisualStyleBackColor = true;
            // 
            // rtbAll
            // 
            this.rtbAll.BackColor = System.Drawing.Color.White;
            this.rtbAll.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbAll.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.rtbAll.ForeColor = System.Drawing.Color.Black;
            this.rtbAll.Location = new System.Drawing.Point(0, 0);
            this.rtbAll.Name = "rtbAll";
            this.rtbAll.ReadOnly = true;
            this.rtbAll.Size = new System.Drawing.Size(342, 147);
            this.rtbAll.TabIndex = 0;
            this.rtbAll.Text = "";
            this.rtbAll.VScroll += new System.EventHandler(this.rtbAll_VScroll);
            // 
            // tabChatPageYell
            // 
            this.tabChatPageYell.Controls.Add(this.rtbYell);
            this.tabChatPageYell.Location = new System.Drawing.Point(4, 22);
            this.tabChatPageYell.Name = "tabChatPageYell";
            this.tabChatPageYell.Size = new System.Drawing.Size(342, 147);
            this.tabChatPageYell.TabIndex = 10;
            this.tabChatPageYell.Text = "Yell";
            this.tabChatPageYell.UseVisualStyleBackColor = true;
            // 
            // rtbYell
            // 
            this.rtbYell.BackColor = System.Drawing.Color.White;
            this.rtbYell.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbYell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbYell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbYell.ForeColor = System.Drawing.Color.Black;
            this.rtbYell.Location = new System.Drawing.Point(0, 0);
            this.rtbYell.Margin = new System.Windows.Forms.Padding(0);
            this.rtbYell.Name = "rtbYell";
            this.rtbYell.ReadOnly = true;
            this.rtbYell.Size = new System.Drawing.Size(342, 147);
            this.rtbYell.TabIndex = 3;
            this.rtbYell.Text = "";
            this.rtbYell.WordWrap = false;
            this.rtbYell.VScroll += new System.EventHandler(this.rtbYell_VScroll);
            // 
            // Form_Logs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 172);
            this.Controls.Add(this.tabChat);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Logs";
            this.Text = "FFXI Monitor Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.tabChat.ResumeLayout(false);
            this.tabChatPageSay.ResumeLayout(false);
            this.tabChatPageSay.PerformLayout();
            this.tabChatPageTell.ResumeLayout(false);
            this.tabChatPageParty.ResumeLayout(false);
            this.tabChatPageLinkshell.ResumeLayout(false);
            this.tabChatPageShout.ResumeLayout(false);
            this.tabChatPageOther.ResumeLayout(false);
            this.tabChatPageAll.ResumeLayout(false);
            this.tabChatPageYell.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabChat;
        private System.Windows.Forms.TabPage tabChatPageSay;
        private System.Windows.Forms.TabPage tabChatPageTell;
        private System.Windows.Forms.TabPage tabChatPageParty;
        private System.Windows.Forms.TabPage tabChatPageLinkshell;
        private System.Windows.Forms.TabPage tabChatPageShout;
        private System.Windows.Forms.TabPage tabChatPageOther;
        private System.Windows.Forms.TabPage tabChatPageAll;
        private System.Windows.Forms.RichTextBox rtbSay;
        private System.Windows.Forms.RichTextBox rtbTell;
        private System.Windows.Forms.RichTextBox rtbParty;
        private System.Windows.Forms.RichTextBox rtbLinkshell;
        private System.Windows.Forms.RichTextBox rtbShout;
        private System.Windows.Forms.RichTextBox rtbOther;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox rtbAll;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabPage tabChatPageYell;
        private System.Windows.Forms.RichTextBox rtbYell;
    }
}