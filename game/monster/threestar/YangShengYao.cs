using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.entity;
using game.@event;

namespace game.monster.threestar
{
    public class YangShengYao : ThreeStarMonster
    {
        public YangShengYao()
        {
            Number = 3;
            Name = "杨盛尧";
            Descripe = "宇宙第一土豪，没有什么是用钱解决不了的，就像没有什么是他解决不了的";
            Attack = 2200;
            Defense = 1800;
            Effect = "支付500点LP点,下回合内获得的印章数量加倍";
            Star = 3;
            Prop = Const.PropEnum.Angel;
        }

//        public override bool UserEffect(MEAEventAgrs e)
//        {
//            return true;
//        }
//
//        public override bool CanEffect(MEAEventAgrs e)
//        {
//            return true;
//        }
    }
}
