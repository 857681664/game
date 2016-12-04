using System;
using System.Windows.Forms;
using game.entity;
using game.@event;

namespace game.monster.threestar
{
    public class YeQiPeng : ThreeStarMonster
    {
        public YeQiPeng()
        {
            Number = 2;
            Star = 3;
            Name = "叶其鹏";
            Descripe = "人间大鸟，翅膀可以覆盖整个大陆，罕见无比";
            Effect = "使用2个魔法印章和5个陷阱印章，选择敌方场上一只怪兽，使其攻击力下降500";
            Attack = 1500;
            Defense = 1200;
            Prop = Const.PropEnum.Fly;
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
//            e.Data.BelongDictionary[e.NowGameLabel.Belongs].CardLinkedList.Find(e.NowGameLabel.Monster).Value.Attack -= 500;
//            player.MagicNumber -= 2;
//            player.TrapNumber -= 5;
//            return true;
//        }
//
//        public override bool CanEffect(MEAEventAgrs e)
//        {
//            Player player = e.Data.BelongDictionary[e.LastGameLabel.Belongs];
//            if (player.MagicNumber >= 2 && player.TrapNumber >= 5)
//            {
//                MessageBox.Show("请选择一只敌方怪兽", "提示");
//                return true;
//            }
//            return false;
//        }
    }
}
