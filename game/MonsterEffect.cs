using System;
using System.Linq;
using System.Windows.Forms;
using game.control;
using game.entity;
using game.@event;

namespace game
{
    /// <summary>
    /// 怪兽发动效果的类
    /// </summary>
    public class MonsterEffect
    {
        public static bool CanEffect(MEAEventAgrs e, int magicNum, int trapNum)
        {
            Player player = e.Data.BelongDictionary[e.LastGameLabel.Monster.Belongs];
            if (player.MagicNumber >= magicNum && player.TrapNumber >= trapNum)
            {
                player.MagicNumber -= magicNum;
                player.TrapNumber -= trapNum;
                player.LabelLinkedList.ElementAt(3).Text = player.MagicNumber.ToString();
                player.LabelLinkedList.ElementAt(2).Text = player.TrapNumber.ToString();
                player.LabelLinkedList.ElementAt(1).Text = player.MoveNumber.ToString();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 北郡牧师效果
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool BeiJunEffect(MEAEventAgrs e)
        {
            Player player = e.LastGameLabel.Belongs == Const.PlayerBelongs.PlayerOne ? e.Data.PlayerTwo : e.Data.PlayerOne;
            foreach (CardMonster monster in player.CardLinkedList)
            {
                if (!monster.IsEffected)
                {
                    monster.Attack -= 200;
                    monster.IsEffected = true;
                    monster.EffectNumber = 200;
                    monster.EffectTurn = 3;
                    monster.PointKind = Const.PointKindEnum.DecreaseAttack;
                    
                }
                
            }
            return true;
        }
        /// <summary>
        /// 死亡领主效果
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool SiWangEffect(MEAEventAgrs e)
        {
            Player player = e.LastGameLabel.Belongs == Const.PlayerBelongs.PlayerOne ? e.Data.PlayerTwo : e.Data.PlayerOne;
            player.LifePoint -= 500;
            player.LabelLinkedList.ElementAt(0).Text = player.LifePoint.ToString();
            return true;
        }
        
        /// <summary>
        /// 宴会技师效果
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool YanHuiEffect(MEAEventAgrs e)
        {
            if (e.NowGameLabel.Monster.IsEffected)
                return false;
            if(e.NowGameLabel.Monster.Belongs == e.LastGameLabel.Monster.Belongs)
                return false;
            e.NowGameLabel.Monster.IsEffected = true;
            e.NowGameLabel.Monster.EffectTurn = 1;
            e.NowGameLabel.Monster.PointKind = Const.PointKindEnum.DecreaseAttack;
            e.NowGameLabel.Monster.EffectNumber = 300;
            e.NowGameLabel.Monster.Attack -= 300;
            return true;
        }

        /// <summary>
        /// 叫嚣的中士效果
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool JiaoXiaoEffect(MEAEventAgrs e)
        {
            if (e.NowGameLabel.Monster.IsEffected)
                return false;
            if (e.NowGameLabel.Monster.Belongs != e.LastGameLabel.Monster.Belongs)
                return false;
            e.NowGameLabel.Monster.IsEffected = true;
            e.NowGameLabel.Monster.EffectTurn = 1;
            e.NowGameLabel.Monster.EffectNumber = 200;
            e.NowGameLabel.Monster.Attack += 200;
            e.NowGameLabel.Monster.PointKind = Const.PointKindEnum.IncreaseAttack;
            return true;
        }

        /// <summary>
        /// 密教暗影祭祀效果
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool MiJiaoEffect(MEAEventAgrs e)
        {
            if (e.NowGameLabel.Monster.Belongs == e.LastGameLabel.Monster.Belongs)
                return false;
            Player player = e.LastGameLabel.Monster.Belongs == Const.PlayerBelongs.PlayerOne
                ? e.Data.PlayerOne
                : e.Data.PlayerTwo;
            e.Data.BelongDictionary[e.NowGameLabel.Monster.Belongs].CardLinkedList.Remove(e.NowGameLabel.Monster);
            e.NowGameLabel.Monster.Belongs = e.LastGameLabel.Monster.Belongs;
            e.NowGameLabel.Monster.MonsterImage =
                e.Data.ImageDictionary[player].ElementAt((int) e.NowGameLabel.Monster.Prop);
            e.NowGameLabel.Monster.CanAttack = false;
            e.NowGameLabel.Monster.CanEffective = false;
            e.NowGameLabel.Monster.CanMove = false;
            player.CardLinkedList.AddLast(e.NowGameLabel.Monster);
            return true;
        }

        /// <summary>
        /// 黑龙领主效果
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool HeiLongEffect(MEAEventAgrs e)
        {
            Player player = e.LastGameLabel.Monster.Belongs == Const.PlayerBelongs.PlayerOne
                ? e.Data.PlayerOne
                : e.Data.PlayerTwo;
            foreach (CardMonster m in player.CardLinkedList)
            {
                if (m.Prop == Const.PropEnum.Dark)
                    e.LastGameLabel.Monster.Attack += 200;
            }
            return true;
        }

        /// <summary>
        /// 丛林猎手效果
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool CongLinEffect(MEAEventAgrs e)
        {
            if (e.LastGameLabel.Monster.Belongs == e.NowGameLabel.Monster.Belongs)
                return false;
            if (e.NowGameLabel.Monster.Prop != Const.PropEnum.Water)
                return false;
            Player player = e.NowGameLabel.Monster.Belongs == Const.PlayerBelongs.PlayerOne
                ? e.Data.PlayerOne
                : e.Data.PlayerTwo;
            player.CardLinkedList.Remove(e.NowGameLabel.Monster);
            player.DeathMonsters.AddLast(e.NowGameLabel.Monster);
            e.NowGameLabel.Monster = null;
            e.NowGameLabel.HasMonster = false;
            return true;
        }

        /// <summary>
        /// 恩佐斯的副官效果
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool FuGuanEffect(MEAEventAgrs e)
        {
            Player player = e.LastGameLabel.Monster.Belongs == Const.PlayerBelongs.PlayerOne
                ? e.Data.PlayerOne
                : e.Data.PlayerTwo;
            Random random = new Random();
            int index = random.Next(player.DeathMonsters.Count);
            int i, j;
            CardMonster monster = player.DeathMonsters.ElementAt(index);
            do
            {
                i = random.Next(Const.LABEL_ROW);
                j = random.Next(Const.LABEL_COL);
            } while (e.Data.GameLabels[i,j].Belongs == Const.PlayerBelongs.None || e.Data.GameLabels[i,j].Belongs != e.Data.PlayerDictionary[player] || e.Data.GameLabels[i,j].HasMonster);
            e.Data.GameLabels[i, j].Monster = monster;
            e.Data.GameLabels[i, j].HasMonster = true;
            return true;
        }
    }
}