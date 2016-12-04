using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.@event
{
    /// <summary>
    /// 左键点击信息
    /// </summary>
    public class LeftClickEventArgs
    {
        public Const.LeftClickEnum LeftClick { get; set; }

        public LeftClickEventArgs(Const.LeftClickEnum leftClick)
        {
            LeftClick = leftClick;
        }
    }
}
