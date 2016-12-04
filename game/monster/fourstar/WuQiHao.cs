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
    public class WuQiHao : FourStarMonster
    {
        public WuQiHao()
        {
            Number = 1;
            Name = "吴大仙";
            Descripe = "传说中的存在，一招‘代码论断术’使得闻者闻风丧胆";
            Attack = 2800;
            Defense = 2500;
            Effect = "使用10个魔法印章和10个陷阱印章，使敌方全体怪兽攻击力下降500点";
            Prop = Const.PropEnum.Angel;
            Star = 4;
        }

//        public override bool UserEffect(MEAEventAgrs e)
//        {
//            Player player;
//            player = e.LastGameLabel.Belongs == Const.PlayerBelongs.PlayerOne ? e.Data.PlayerTwo : e.Data.PlayerOne;
//            foreach (CardMonster card in player.CardLinkedList)
//            {
//                card.Attack -= 500;
//            }
//            player.MagicNumber -= 10;
//            player.TrapNumber -= 10;
//            MessageBox.Show("发动成功，敌方全体怪兽攻击力下降了500点", "提示");
//            return true;
//        }
//
//        public override bool CanEffect(MEAEventAgrs e)
//        {
//            Player player = e.Data.BelongDictionary[e.LastGameLabel.Belongs];
//            return player.MagicNumber >= 10 && player.TrapNumber >= 10;
//        }
    }
}
