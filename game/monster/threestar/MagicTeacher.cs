using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game;
using game.entity;
using game.@event;

namespace game.monster.threestar
{
    /// <summary>
    /// 魔导师卡
    /// </summary>
    [Serializable]
    public class MagicTeacher : ThreeStarMonster
    {
        public MagicTeacher()
        {
            Number = 1;
            Name = "魔导师";
            Descripe = "魔导学院首席魔导师，拥有惊人的魔法力量";
            Effect = "使用7个魔法印章，在本回合提升自己场上任意一只怪兽500点攻击力";
            Attack = 2000;
            Defense = 1200;
            Star = 3;
            Prop = Const.PropEnum.Dark;
        }

        public void UserEffect(MEAEventAgrs e)
        {
            
        }
    }
}
