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
    public partial class RegisterForm : Form
    {
        private readonly SqlOperate operate;
        public RegisterForm()
        {
            InitializeComponent();
            operate = new SqlOperate();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string userName = userNameTBox.Text;
            string password = passwordTBox.Text;
            operate.RegisterUser(userName,password);
            MessageBox.Show("注册成功");
            Close();
        }
    }
}
