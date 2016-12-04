using System;
using System.Drawing;
using System.Linq;
using game.dto;

namespace game.entity
{
    public class TwoStarMonster : CardMonster
    {
//        public int Attack { get; set; }
//
//        public int Defense { get; set; }
//
//        public int Number { get; set; }
//
//        public int Star { get; } = 2;
//
//        public Const.PropEnum Prop { get; set; }
//
//        public bool IsAttack { get; set; }
//
//        public bool CanAttack { get; set; }
//
//        public bool CanMove { get; set; }
//
//        public Const.PlayerBelongs Belongs { get; set; }
//
//        public Image MonsterImage { get; set; }

        public TwoStarMonster(MonsterDto dto)
        {
            Number = dto.Number;
            Star = dto.Star;
            Name = dto.Name;
            Descripe = dto.Descripe;
            Attack = dto.Attack;
            Defense = dto.Defense;
            Prop = dto.Prop;
        }

        public TwoStarMonster()
        {}

        public override string ToString()
        {
            return Name + " " + Star + "星\t" + Const.PropList.ElementAt((int)Prop) + "\r\n\r\n" + Descripe + "\r\n\r\n" + "攻击：" + Attack + "\r\n" + "防御：" + Defense;
        }
    }
}