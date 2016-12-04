using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.entity;

namespace game.monster.twostar
{
    [Serializable]
    public class StoneMonster : TwoStarMonster
    {
        public StoneMonster()
        {
            Number = 0;
            Name = "石头人";
            Descripe = "石头人，拥有坚固的身体，不知疲惫地攻击它眼中的敌人";
            Attack = 1000;
            Defense = 500;
            Star = 2;
            Prop = Const.PropEnum.Ground;
        }
    }
}
