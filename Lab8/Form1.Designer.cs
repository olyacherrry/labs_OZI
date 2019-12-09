namespace Lab8
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.infoTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.hashAlgGroupBox = new System.Windows.Forms.GroupBox();
            this.sha256radioButton = new System.Windows.Forms.RadioButton();
            this.asymAlgGroupBox = new System.Windows.Forms.GroupBox();
            this.rsaRadioButton = new System.Windows.Forms.RadioButton();
            this.signTextBox = new System.Windows.Forms.TextBox();
            this.hashTextBox = new System.Windows.Forms.TextBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.signButton = new System.Windows.Forms.Button();
            this.hashAlgGroupBox.SuspendLayout();
            this.asymAlgGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 247);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 17);
            this.label4.TabIndex = 25;
            this.label4.Text = "Info";
            // 
            // infoTextBox
            // 
            this.infoTextBox.Location = new System.Drawing.Point(72, 228);
            this.infoTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.infoTextBox.Multiline = true;
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.Size = new System.Drawing.Size(420, 88);
            this.infoTextBox.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 392);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "Sign";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 168);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Hash";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Text";
            // 
            // hashAlgGroupBox
            // 
            this.hashAlgGroupBox.Controls.Add(this.sha256radioButton);
            this.hashAlgGroupBox.Location = new System.Drawing.Point(363, 71);
            this.hashAlgGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.hashAlgGroupBox.Name = "hashAlgGroupBox";
            this.hashAlgGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.hashAlgGroupBox.Size = new System.Drawing.Size(132, 60);
            this.hashAlgGroupBox.TabIndex = 20;
            this.hashAlgGroupBox.TabStop = false;
            this.hashAlgGroupBox.Text = "Hash Algoritm";
            // 
            // sha256radioButton
            // 
            this.sha256radioButton.AutoSize = true;
            this.sha256radioButton.Checked = true;
            this.sha256radioButton.Location = new System.Drawing.Point(21, 23);
            this.sha256radioButton.Margin = new System.Windows.Forms.Padding(4);
            this.sha256radioButton.Name = "sha256radioButton";
            this.sha256radioButton.Size = new System.Drawing.Size(81, 21);
            this.sha256radioButton.TabIndex = 4;
            this.sha256radioButton.TabStop = true;
            this.sha256radioButton.Text = "SHA512";
            this.sha256radioButton.UseVisualStyleBackColor = true;
            // 
            // asymAlgGroupBox
            // 
            this.asymAlgGroupBox.Controls.Add(this.rsaRadioButton);
            this.asymAlgGroupBox.Location = new System.Drawing.Point(73, 71);
            this.asymAlgGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.asymAlgGroupBox.Name = "asymAlgGroupBox";
            this.asymAlgGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.asymAlgGroupBox.Size = new System.Drawing.Size(173, 60);
            this.asymAlgGroupBox.TabIndex = 19;
            this.asymAlgGroupBox.TabStop = false;
            this.asymAlgGroupBox.Text = "Asymmetric Algorithm";
            // 
            // rsaRadioButton
            // 
            this.rsaRadioButton.AutoSize = true;
            this.rsaRadioButton.Checked = true;
            this.rsaRadioButton.Location = new System.Drawing.Point(20, 23);
            this.rsaRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.rsaRadioButton.Name = "rsaRadioButton";
            this.rsaRadioButton.Size = new System.Drawing.Size(57, 21);
            this.rsaRadioButton.TabIndex = 5;
            this.rsaRadioButton.TabStop = true;
            this.rsaRadioButton.Text = "RSA";
            this.rsaRadioButton.UseVisualStyleBackColor = true;
            // 
            // signTextBox
            // 
            this.signTextBox.Location = new System.Drawing.Point(72, 324);
            this.signTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.signTextBox.Multiline = true;
            this.signTextBox.Name = "signTextBox";
            this.signTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.signTextBox.Size = new System.Drawing.Size(417, 155);
            this.signTextBox.TabIndex = 17;
            // 
            // hashTextBox
            // 
            this.hashTextBox.Location = new System.Drawing.Point(72, 138);
            this.hashTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.hashTextBox.Multiline = true;
            this.hashTextBox.Name = "hashTextBox";
            this.hashTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.hashTextBox.Size = new System.Drawing.Size(420, 82);
            this.hashTextBox.TabIndex = 16;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(73, 23);
            this.textBox.Margin = new System.Windows.Forms.Padding(4);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(420, 40);
            this.textBox.TabIndex = 15;
            this.textBox.Text = "ABCDEFG";
            // 
            // signButton
            // 
            this.signButton.Location = new System.Drawing.Point(255, 71);
            this.signButton.Margin = new System.Windows.Forms.Padding(4);
            this.signButton.Name = "signButton";
            this.signButton.Size = new System.Drawing.Size(100, 28);
            this.signButton.TabIndex = 14;
            this.signButton.Text = "Sign";
            this.signButton.UseVisualStyleBackColor = true;
            this.signButton.Click += new System.EventHandler(this.signButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 492);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.infoTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hashAlgGroupBox);
            this.Controls.Add(this.asymAlgGroupBox);
            this.Controls.Add(this.signTextBox);
            this.Controls.Add(this.hashTextBox);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.signButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.hashAlgGroupBox.ResumeLayout(false);
            this.hashAlgGroupBox.PerformLayout();
            this.asymAlgGroupBox.ResumeLayout(false);
            this.asymAlgGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox infoTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox hashAlgGroupBox;
        private System.Windows.Forms.RadioButton sha256radioButton;
        private System.Windows.Forms.GroupBox asymAlgGroupBox;
        private System.Windows.Forms.RadioButton rsaRadioButton;
        private System.Windows.Forms.TextBox signTextBox;
        private System.Windows.Forms.TextBox hashTextBox;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button signButton;
    }
}

