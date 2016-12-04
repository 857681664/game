using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using game.control;

namespace game
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
//            LoginForm form = new LoginForm();
//            form.Show();

            Form1 form = new Form1();
            form.Show();
            Application.Run();
        }
    }
}
