using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.entity;

namespace game.monster.twostar
{
    [Serializable]
    public class WaterMonster : TwoStarMonster
    {
        public WaterMonster()
        {
            Number = 1;
            Name = "水元素";
            Descripe = "水中诞生的怪物，使用水枪攻击敌人";
            Attack = 1000;
            Defense = 800;
            Star = 2;
            Prop = Const.PropEnum.Water;
        }
    }
}
