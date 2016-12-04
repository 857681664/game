using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using game.entity;
using game.@event;

namespace game.monster.fourstar
{
    [Serializable]
    public class SkyKnight : FourStarMonster
    {
        private static int skyKnightCount = 3;
        public SkyKnight()
        {
            Number = 0;
            Name = "天空骑士";
            Descripe = "骑乘着巨龙的骑士，拥有无比的勇气，能战胜一切敌人";
            Attack = 2500;
            Defense = 2000;
            Effect = "使用9个陷阱印章可以延伸己方边界3格";
            Star = 4;
            Prop = Const.PropEnum.Fly;
        }

//        public override bool UserEffect(MEAEventAgrs e)
//        {
//            if (MonsterEffect.SkyKnightEffect(e))
//                skyKnightCount--;
//            else
//            {
//                e.LastGameLabel.LeftClickEventArgs.LeftClick = Const.LeftClickEnum.SelectMonster;
//                MessageBox.Show("点击的格子周围无己方格子，请重新点击","提示");
//            }
//            if (skyKnightCount == 0)
//            {
//                MessageBox.Show("点击完毕", "提示");
//                skyKnightCount = 3;
//                e.LastGameLabel.LeftClickEventArgs.LeftClick = Const.LeftClickEnum.None;
//                return true;
//            }
//            return false;
//        }
//
//        public override bool CanEffect(MEAEventAgrs e)
//        {
//            Player player = e.Data.BelongDictionary[e.LastGameLabel.Belongs];
//            if (player.TrapNumber >= 3)
//            {
//                MessageBox.Show("请点击3处与己方相连的格子", "提示");
//                return true;
//            }
//            return false;
//        }
    }
}
