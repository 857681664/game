using System;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using game.dto;
using game.@event;

namespace game.entity
{
    public class ThreeStarMonster : CardMonster, IEffect
    {
        public delegate bool CanEffectHandle(MEAEventAgrs e, int magicNum, int trapNum);

        public delegate bool EffectHandle(MEAEventAgrs e);

        public event CanEffectHandle CanEffectEvent;

        public event EffectHandle UserEffectEvent;
        

//        public int Attack { get; set; }
//
//        public int Defense { get; set; }
//
//        public int Number { get; set; }
//
        public string Effect { get; set; }

        public int NeedMagic { get; set; }

        public int NeedTrap { get; set; }

//        public int Star { get; } = 3;
//
//        public Const.PropEnum Prop { get; set; }
//
//        public bool IsAttack { get; set; }
//
//        public bool CanAttack { get; set; }
//
//        public bool CanMove { get; set; }
//
//        public bool CanEffective { get; set; }
//
//        public Const.PlayerBelongs Belongs { get; set; }
//
//        public Image MonsterImage { get; set; }

//        public Const.EffectKindEnum EffectKind { get; set; }

//        public Const.PointKindEnum PointKind { get; set; } 

        public ThreeStarMonster(MonsterDto dto, EffectHandle effectDelegate, CanEffectHandle canEffectHandle)
        {
            Number = dto.Number;
            Star = dto.Star;
            Name = dto.Name;
            Descripe = dto.Descripe;
            Effect = dto.Effect;
            Attack = dto.Attack;
            Defense = dto.Defense;
            NeedMagic = dto.NeedMagic;
            NeedTrap = dto.NeedTrap;
            Prop = dto.Prop;
            EffectKind = dto.EffectKind;
            PointKind = dto.PointKind;
            UserEffectEvent += effectDelegate;
            CanEffectEvent += canEffectHandle;
        }

        public ThreeStarMonster()
        { }

        public override string ToString()
        {
            return Name + " " + Star + "星\t" + Const.PropList.ElementAt((int)Prop) + "\r\n\r\n" + "效果：" + Effect + "\r\n\r\n" + Descripe + "\r\n\r\n" + "攻击：" + Attack + "\r\n" + "防御：" + Defense;
        }

        public bool UserEffect(MEAEventAgrs e)
        {
            return UserEffectEvent != null && UserEffectEvent.Invoke(e);
        }

        public bool CanEffect(MEAEventAgrs e, int magicNum, int trapNum)
        {
            return CanEffectEvent != null && CanEffectEvent.Invoke(e, magicNum, trapNum);
        }
    }
}