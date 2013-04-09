namespace TestingWebService
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxuser = new System.Windows.Forms.TextBox();
            this.textBoxpass = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.propertyGridShort = new System.Windows.Forms.PropertyGrid();
            this.propertyGridLong = new System.Windows.Forms.PropertyGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "password";
            // 
            // textBoxuser
            // 
            this.textBoxuser.Location = new System.Drawing.Point(118, 13);
            this.textBoxuser.Name = "textBoxuser";
            this.textBoxuser.Size = new System.Drawing.Size(100, 20);
            this.textBoxuser.TabIndex = 2;
            // 
            // textBoxpass
            // 
            this.textBoxpass.Location = new System.Drawing.Point(118, 41);
            this.textBoxpass.Name = "textBoxpass";
            this.textBoxpass.Size = new System.Drawing.Size(100, 20);
            this.textBoxpass.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(250, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Authenticate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // propertyGridShort
            // 
            this.propertyGridShort.Location = new System.Drawing.Point(19, 67);
            this.propertyGridShort.Name = "propertyGridShort";
            this.propertyGridShort.Size = new System.Drawing.Size(306, 345);
            this.propertyGridShort.TabIndex = 5;
            // 
            // propertyGridLong
            // 
            this.propertyGridLong.Location = new System.Drawing.Point(331, 67);
            this.propertyGridLong.Name = "propertyGridLong";
            this.propertyGridLong.Size = new System.Drawing.Size(283, 345);
            this.propertyGridLong.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(384, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Please refer to the source code for explanation.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 424);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.propertyGridLong);
            this.Controls.Add(this.propertyGridShort);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxpass);
            this.Controls.Add(this.textBoxuser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxuser;
        private System.Windows.Forms.TextBox textBoxpass;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PropertyGrid propertyGridShort;
        private System.Windows.Forms.PropertyGrid propertyGridLong;
        private System.Windows.Forms.Label label3;
    }
}

