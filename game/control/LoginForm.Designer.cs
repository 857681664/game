namespace game.control
{
    partial class LoginForm
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
            this.loginBtn = new System.Windows.Forms.Button();
            this.passwordTBox = new System.Windows.Forms.TextBox();
            this.userNameTBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.showRegisterFormBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loginBtn
            // 
            this.loginBtn.Location = new System.Drawing.Point(107, 168);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(75, 23);
            this.loginBtn.TabIndex = 9;
            this.loginBtn.Text = "登录";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // passwordTBox
            // 
            this.passwordTBox.Location = new System.Drawing.Point(107, 113);
            this.passwordTBox.Name = "passwordTBox";
            this.passwordTBox.Size = new System.Drawing.Size(142, 21);
            this.passwordTBox.TabIndex = 8;
            // 
            // userNameTBox
            // 
            this.userNameTBox.Location = new System.Drawing.Point(107, 68);
            this.userNameTBox.Name = "userNameTBox";
            this.userNameTBox.Size = new System.Drawing.Size(142, 21);
            this.userNameTBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "密码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "用户名";
            // 
            // showRegisterFormBtn
            // 
            this.showRegisterFormBtn.Location = new System.Drawing.Point(188, 168);
            this.showRegisterFormBtn.Name = "showRegisterFormBtn";
            this.showRegisterFormBtn.Size = new System.Drawing.Size(75, 23);
            this.showRegisterFormBtn.TabIndex = 10;
            this.showRegisterFormBtn.Text = "没有账号？";
            this.showRegisterFormBtn.UseVisualStyleBackColor = true;
            this.showRegisterFormBtn.Click += new System.EventHandler(this.showRegisterFormBtn_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 254);
            this.Controls.Add(this.showRegisterFormBtn);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.passwordTBox);
            this.Controls.Add(this.userNameTBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.TextBox passwordTBox;
        private System.Windows.Forms.TextBox userNameTBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button showRegisterFormBtn;
    }
}