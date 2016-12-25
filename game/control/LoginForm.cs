using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game.control
{
    public partial class LoginForm : Form
    {
        private SqlOperate operate;
        private RegisterForm form;
        private Form1 mainForm;
        public LoginForm()
        {
            InitializeComponent();
            operate = new SqlOperate();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string name = userNameTBox.Text;
            string password = passwordTBox.Text;
            if (operate.LoginUser(name, password))
            {
                operate = null;
                mainForm = new Form1();
                mainForm.Show();
                Close();
            }
        }

        private void showRegisterFormBtn_Click(object sender, EventArgs e)
        {
            if (form == null)
            {
                form = new RegisterForm();
                form.Show();
            }
        }
    }
}
