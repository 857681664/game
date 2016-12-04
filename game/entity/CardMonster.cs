using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using game.@event;

namespace game.entity
{

    /// <summary>
    /// 怪兽卡类
    /// </summary>
    public class CardMonster : Card, ICloneable
    {
//        public delegate bool CanEffectHandle(MEAEventAgrs e);
//
//        public delegate bool EffectHandle(MEAEventAgrs e);
//
//        public event CanEffectHandle CanEffectEvent;
//
//        public event EffectHandle UserEffectEvent;

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Number { get; set; }

//        public string Effect { get; set; }

        public int Star { get; set; }

        public Const.PropEnum Prop { get; set; }

        public bool IsAttack { get; set; }

        public bool IsMove { get; set; }

        public bool CanAttack { get; set; }

        public bool CanMove { get; set; }

        public bool CanEffective { get; set; }

        public Const.PlayerBelongs Belongs { get; set; }

        public Const.EffectKindEnum EffectKind { get; set; }

        public Const.PointKindEnum PointKind { get; set; }

        public Image MonsterImage { get; set; }

        public CardMonster()
        {
            KindOfEnum = Const.KindEnum.Monster;
        }

//        public override string ToString()
//        {
//            if(this.Star > 2)
//                return Name + " " + Star + "星\t" + Const.PropList.ElementAt((int)Prop) + "\r\n\r\n" + "效果：" + Effect + "\r\n\r\n" + Descripe + "\r\n\r\n" + "攻击：" + Attack + "\r\n" + "防御：" + Defense;
//            else
//                return Name + " " + Star + "星\t" + Const.PropList.ElementAt((int)Prop) + "\r\n\r\n" + Descripe + "\r\n\r\n" + "攻击：" + Attack + "\r\n" + "防御：" + Defense;
//
//        }

        public object Clone()
        {
            return MemberwiseClone();
        }

//        public virtual bool UserEffect(MEAEventAgrs e)
//        {
//            return UserEffectEvent != null && UserEffectEvent.Invoke(e);
//        }
//
//        public virtual bool CanEffect(MEAEventAgrs e)
//        {
//            return CanEffectEvent != null && CanEffectEvent.Invoke(e);
//        }

        
    }
}
