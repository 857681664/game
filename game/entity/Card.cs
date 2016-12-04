using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    /// <summary>
    /// 卡片类
    /// </summary>
    public abstract class Card
    {
        public String Name { get; set; }

        public Const.KindEnum KindOfEnum { get; set; }

        public String Descripe { get; set; }
    }
}
