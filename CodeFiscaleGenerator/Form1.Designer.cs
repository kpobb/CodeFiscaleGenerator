namespace CodeFiscaleGenerator
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelCbox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.registrationCbox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.subregistrationCbox = new System.Windows.Forms.ComboBox();
            this.fiscaleCodeTbox = new System.Windows.Forms.TextBox();
            this.createBtn = new System.Windows.Forms.Button();
            this.checkBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cloneCBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.copyBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelCbox
            // 
            this.labelCbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.labelCbox.FormattingEnabled = true;
            this.labelCbox.Location = new System.Drawing.Point(17, 28);
            this.labelCbox.Name = "labelCbox";
            this.labelCbox.Size = new System.Drawing.Size(105, 21);
            this.labelCbox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Label:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Registration status:";
            // 
            // registrationCbox
            // 
            this.registrationCbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.registrationCbox.FormattingEnabled = true;
            this.registrationCbox.Location = new System.Drawing.Point(17, 75);
            this.registrationCbox.Name = "registrationCbox";
            this.registrationCbox.Size = new System.Drawing.Size(124, 21);
            this.registrationCbox.TabIndex = 2;
            this.registrationCbox.SelectedIndexChanged += new System.EventHandler(this.RegistrationCbox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Subregistration status:";
            // 
            // subregistrationCbox
            // 
            this.subregistrationCbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.subregistrationCbox.FormattingEnabled = true;
            this.subregistrationCbox.Location = new System.Drawing.Point(17, 123);
            this.subregistrationCbox.Name = "subregistrationCbox";
            this.subregistrationCbox.Size = new System.Drawing.Size(124, 21);
            this.subregistrationCbox.TabIndex = 4;
            // 
            // fiscaleCodeTbox
            // 
            this.fiscaleCodeTbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fiscaleCodeTbox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fiscaleCodeTbox.Location = new System.Drawing.Point(17, 171);
            this.fiscaleCodeTbox.Name = "fiscaleCodeTbox";
            this.fiscaleCodeTbox.Size = new System.Drawing.Size(186, 26);
            this.fiscaleCodeTbox.TabIndex = 6;
            // 
            // createBtn
            // 
            this.createBtn.Location = new System.Drawing.Point(17, 204);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(90, 25);
            this.createBtn.TabIndex = 7;
            this.createBtn.Text = "Generate code";
            this.createBtn.UseVisualStyleBackColor = true;
            this.createBtn.Click += new System.EventHandler(this.Create_Click);
            // 
            // checkBtn
            // 
            this.checkBtn.Location = new System.Drawing.Point(113, 204);
            this.checkBtn.Name = "checkBtn";
            this.checkBtn.Size = new System.Drawing.Size(90, 25);
            this.checkBtn.TabIndex = 8;
            this.checkBtn.Text = "Verify code";
            this.checkBtn.UseVisualStyleBackColor = true;
            this.checkBtn.Click += new System.EventHandler(this.Check_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(128, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "created by -=Tj=-";
            // 
            // cloneCBox
            // 
            this.cloneCBox.AutoSize = true;
            this.cloneCBox.Checked = true;
            this.cloneCBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cloneCBox.Location = new System.Drawing.Point(150, 79);
            this.cloneCBox.Name = "cloneCBox";
            this.cloneCBox.Size = new System.Drawing.Size(52, 17);
            this.cloneCBox.TabIndex = 12;
            this.cloneCBox.Text = "clone";
            this.cloneCBox.UseVisualStyleBackColor = true;
            this.cloneCBox.CheckedChanged += new System.EventHandler(this.CloneCbox_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Code fiscale:";
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(17, 235);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(90, 25);
            this.deleteBtn.TabIndex = 15;
            this.deleteBtn.Text = "Delete code";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // copyBtn
            // 
            this.copyBtn.Location = new System.Drawing.Point(209, 171);
            this.copyBtn.Name = "copyBtn";
            this.copyBtn.Size = new System.Drawing.Size(54, 28);
            this.copyBtn.TabIndex = 16;
            this.copyBtn.Text = "Copy";
            this.copyBtn.UseVisualStyleBackColor = true;
            this.copyBtn.Click += new System.EventHandler(this.CopyBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(270, 272);
            this.Controls.Add(this.copyBtn);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cloneCBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBtn);
            this.Controls.Add(this.createBtn);
            this.Controls.Add(this.fiscaleCodeTbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.subregistrationCbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.registrationCbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelCbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CodeFiscaleGenerator vX.X";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox labelCbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox registrationCbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox subregistrationCbox;
        private System.Windows.Forms.TextBox fiscaleCodeTbox;
        private System.Windows.Forms.Button createBtn;
        private System.Windows.Forms.Button checkBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cloneCBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Button copyBtn;
    }
}

