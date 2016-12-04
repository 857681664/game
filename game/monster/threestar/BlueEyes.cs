using System;
using System.Windows.Forms;
using game.entity;
using game.@event;

namespace game.monster.threestar
{
    [Serializable]
    public class BlueEyes : ThreeStarMonster
    {
        /// <summary>
        /// 青眼白龙卡
        /// </summary>
        public BlueEyes()
        {
            Number = 0;
            Name = "青眼白龙";
            Descripe = "它是拥有无比强大力量的龙，毁灭的喷射白光可以融化世间一切";
            Attack = 2000;
            Defense = 1000;
            Effect = "使用5个魔法印章和5个陷阱印章，可以消灭场上一只暗属性怪兽";
            Star = 3;
            Prop = Const.PropEnum.Light;
        }

//        public override bool UserEffect(MEAEventAgrs e)
//        {
//            Player player = e.Data.BelongDictionary[e.LastGameLabel.Belongs];
//            if (e.NowGameLabel.HasMonster == false)
//            {
//                MessageBox.Show("请点击有怪兽的格子", "提示");
//                return false;
//            }
//            if (e.NowGameLabel.Belongs == e.LastGameLabel.Belongs)
//            {
//                MessageBox.Show("请选择敌方怪兽", "提示");
//                return false;
//            }
//            if (e.NowGameLabel.Monster.Prop != Const.PropEnum.Dark)
//            {
//                MessageBox.Show("点击的怪兽不是暗属性怪兽", "提示");
//                return false;
//            }
//            e.Data.BelongDictionary[e.NowGameLabel.Belongs].CardLinkedList.Remove(e.NowGameLabel.Monster);
//            player.MagicNumber -= 5;
//            player.TrapNumber -= 5;
//            return true;
//        }
//
//        public override bool CanEffect(MEAEventAgrs e)
//        {
//            Player player = e.Data.BelongDictionary[e.LastGameLabel.Belongs];
//            return player.MagicNumber >= 5 && player.TrapNumber >= 5;
//        }
    }
}
